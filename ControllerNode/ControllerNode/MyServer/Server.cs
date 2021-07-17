using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ControllerNode.Utility;
using System.Collections.Generic;
using ControllerNode.Business;
using ControllerNode.MyClient;

namespace ControllerNode.MyServer
{
    /// <summary>
    /// Clase que cumple la función del servidor del sistema
    /// </summary>
    class Server
    {
        /// <summary>
        /// Referencia de <b>IPHostEntry</b>
        /// </summary>
        private readonly IPHostEntry host;

        /// <summary>
        /// Referencia de IPAddress
        /// </summary>
        private readonly IPAddress ipAddr;

        /// <summary>
        /// Referencia de IPEndPoint
        /// </summary>
        private readonly IPEndPoint endPoint;

        /// <summary>
        /// Referencia de Socket del servidor
        /// </summary>
        private readonly Socket s_Server;

        /// <summary>
        /// Referencia de Socket cliente
        /// </summary>
        private Socket s_Client;

        /// <summary>
        /// Referencia de la lista de clientes (saSearch)
        /// </summary>
        private readonly List<Client> listaClientes;

        /// <summary>
        /// Referencia de la lista de cliente (node)
        /// </summary>
        private readonly List<Client> listNodes;

        /// <summary>
        /// Referencia de la lista de los fragmentos de los archivos
        /// </summary>
        private List<byte[]> listFragments;

        /// <summary>
        /// Referencia a la clase <b>NodeConnectionBusiness</b>
        /// </summary>
        private readonly NodeConnectionBusiness nodeBusiness;

        /// <summary>
        /// Contiene los nodos ingresados
        /// </summary>
        private int contNodes;

        /// <summary>
        /// Constructor de la clase <b>Server</b>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public Server(string ip, int port)
        {
            nodeBusiness = new NodeConnectionBusiness();
            listaClientes = new List<Client>();
            listNodes = new List<Client>();
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);
            s_Server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            s_Server.Bind(endPoint);
            s_Server.Listen(10);
            contNodes = 0;
        }

        /// <summary>
        /// Escucha las conexiones de los clientes
        /// </summary>
        public void Start()
        {
            Thread t;
            while (true)
            {
                Console.Write("Esperando conexiones...\n");
                s_Client = s_Server.Accept();
                t = new Thread(ClientConnection);
                t.Start(s_Client);
                Console.Write("Se ha conectado un cliente...\n");
            }
        }

        /// <summary>
        /// Permite la ejecucion del hilo del cliente
        /// </summary>
        /// <param name="s"></param>
        public void ClientConnection(object s)
        {
            Socket s_Client = (Socket)s;
            string message;
            byte[] buffer;
            bool conectado = true;
            while (conectado)
            {
                buffer = new byte[30000000];
                s_Client.Receive(buffer);
                message = CommonMethod.Byte2string(buffer);

                switch (CommonMethod.SplitTheClientRequest(message, 0))
                {
                    case "setId":
                        SetId(s_Client, Convert.ToInt32(CommonMethod.SplitTheClientRequest(message, 1)));
                        break;

                    case "infoArchivo":
                        //SendMetaDataBufferToNode(message, nodeBusiness.GetNumberNodes());
                        break;

                    case "getMetaData":
                        RequestMetaDataFragmentsToNode(s_Client);
                        break;

                    case "getFile":
                        RequestFileFragmentsToNode(s_Client);
                        break;

                    case "archivo":
                        nodeBusiness.UpdateCongfigFlag();
                        var fileName = CommonMethod.SplitTheClientRequest(message, 1);
                        buffer = new byte[Convert.ToInt32(CommonMethod.SplitTheClientRequest(message, 2))];
                        s_Client.Receive(buffer);
                        Console.WriteLine("Recibiendo el archivo: " + fileName);
                        SendBufferFileToNode(buffer, fileName, nodeBusiness.GetNumberNodes());
                        break;

                    case "fragMetaData":
                        buffer = new byte[30000000];
                        s_Client.Receive(buffer);
                        listFragments.Add(buffer);
                        break;


                    case "fragFile":
                        buffer = new byte[30000000];
                        s_Client.Receive(buffer);
                        listFragments.Add(buffer);
                        break;

                    case "parity":
                        buffer = new byte[30000000];
                        s_Client.Receive(buffer);
                        listFragments.Add(buffer);
                        break;

                    case "cantidadNodos":
                        int cantidadNodos = Convert.ToInt32(CommonMethod.SplitTheClientRequest(message, 1));
                        Console.WriteLine("Cantidad de nodos configurados: " + cantidadNodos);
                        //DeleteNodes();
                        CreateNodes(cantidadNodos);
                        break;

                    case "setAvailability":
                        SetAvailability(Convert.ToBoolean(CommonMethod.SplitTheClientRequest(message, 1)), Convert.ToInt32(CommonMethod.SplitTheClientRequest(message, 2)));
                        break;

                    case "isAvailable":
                        IsAvailable(s_Client);
                        break;
                }
            }
        }

        /// <summary>
        /// Registra la ruta de los nodos en la base de datos 
        /// </summary>
        /// <param name="cantidadNodos">Cantidad de nodos</param>
        public void CreateNodes(int cantidadNodos)
        {
            nodeBusiness.DeleteNodes();
            for (int i = 0; i < cantidadNodos; i++)
            {
                nodeBusiness.RegisterNode("Node" + i);
            }
        }

        /// <summary>
        /// Elimina la ruta de los nodos guardados en la base de datos
        /// </summary>
        public void DeleteNodes()
        {
            for (int i = 0; i <= 20; i++)
            {
                CommonMethod.DeleteDirectories(i);
            }
            nodeBusiness.DeleteNodes();
        }

        /// <summary>
        /// Envia a cada nodo los meta datos del archivo
        /// </summary>
        /// <param name="message">Mensaje recibido del cliente</param>
        /// <param name="cantNodes">Cantidad de nodos</param>
        public void SendMetaDataBufferToNode(string message, int cantNodes)
        {
            string fileName = CommonMethod.SplitTheClientRequest(message, 1);
            Console.WriteLine("Recibiendo los meta datos del archivo: " + fileName);
            List<byte[]> listBufferMetaData = CommonMethod.GetListBufferMetaData(message, cantNodes, fileName);
            for (int i = 0; i < listBufferMetaData.Count; i++)
            {
                listNodes[i].SendMetaDataFileToNode(listBufferMetaData[i], "MetaData" + i + fileName, "Node" + i);
            }
        }

        /// <summary>
        /// Asigna la disponibilidad de los nodos
        /// </summary>
        /// <param name="available"></param>
        /// <param name="nodeId"></param>
        public void SetAvailability(bool available, int nodeId)
        {
            listNodes[nodeId - 1].IsAvailable = available;
            Console.WriteLine("\nDisponibilidad del nodo: " + nodeId + " es " + listNodes[nodeId - 1].IsAvailable);
        }

        /// <summary>
        /// Envia el fragmento (buffer) del archivo correspondiente
        /// </summary>
        /// <param name="bufferFile">Fragmento del archivo convertido en byte</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="nodesAmount">Cantidad de nodos en el sistema</param>
        private void SendBufferFileToNode(byte[] bufferFile, string fileName, int nodesAmount)
        {
            Console.WriteLine("Cargando...");
            nodeBusiness.InsertFile(fileName);
            List<byte[]> listBuffersFile = CommonMethod.GetListByteArrays(bufferFile, nodesAmount);
            Console.WriteLine("\nCantidad de pedazos de la lista de buffers file: " + listBuffersFile.Count);
            for (int i = 0; i < listBuffersFile.Count; i++)
            {
                string fragName = "Frag" + i + fileName, nodeName = "Node" + i;
                Console.WriteLine("\nEnviando el fragmento: " + fragName + " al nodo: " + nodeName);
                nodeBusiness.InsertFragment(fileName, fragName, nodeName);
                Console.WriteLine("\nGuardando fragmento del archivo en el nodo de la lista: " + listNodes[i].name);
                Console.WriteLine("\nTamaño del pedazo " + i + " del archivo es: " + listBuffersFile[i].Length);
                listNodes[i].SaveFilePartition(listBuffersFile[i], fragName, nodeName);
                Thread.Sleep(40);
            }
            //SendBufferParityToNode(listBuffersFile, fileName);
        }

        /// <summary>
        /// Envia los fragmentos (buffer) de la paridad del archivo correspondiente
        /// </summary>
        /// <param name="listBuffersFile">Lista de los fragmentos de los archivos</param>
        /// <param name="fileName">Nombre del archivo</param>
        private void SendBufferParityToNode(List<byte[]> listBuffersFile, string fileName)
        {
            foreach (Client node in listNodes)
            {
                for (int i = 0; i < listBuffersFile.Count; i++)
                {
                    Console.WriteLine("\nGuardando la paridad en el nodo: " + node.name);
                    Console.WriteLine("Paridad -> FragName: " + "Frag" + i + fileName + " Node" + i);
                    Thread.Sleep(30);
                    node.SaveParity(listBuffersFile[i], "Frag" + i + fileName, "Node" + i);
                }
                Thread.Sleep(35);
            }
        }

        /// <summary>
        /// Solicita a los nodos los fragmentos de los meta datos
        /// </summary>
        /// <param name="s_client">Socket asociado al cliente</param>
        public void RequestMetaDataFragmentsToNode(Socket s_client)
        {
            List<string> listFileNames = nodeBusiness.GetListFile();
            for (int i = 0; i < listFileNames.Count; i++)
            {
                listFragments = new();
                for (int j = 0; j < listNodes.Count; j++)
                {
                    if (!listNodes[j].IsAvailable)
                    {
                        RequestDataToAvailableNodes(j, "MetaData" + i + listFileNames[i]);
                    }
                    else
                    {
                        listNodes[j].RequesFragmentToNode("getMetaData", "MetaData" + i + listFileNames[i], i);
                    }
                }
                SendFragmentsToSaSearch(s_client, "metaDataResponse");
            }
        }

        /// <summary>
        /// Agrega en las listas cliente los nodos y los saSearch respectivamente
        /// </summary>
        /// <param name="s_Client"></param>
        /// <param name="identificador"></param>
        private void SetId(Socket s_Client, int identificador)
        {
            if (identificador == 1)
            {
                Console.WriteLine("\nCliente (saSearch) agregado");
                listaClientes.Add(new Client(s_Client));
            }
            else
            {
                Client client = new(s_Client);
                client.name = "Node " + contNodes;
                if (nodeBusiness.IsNewConfigFlag() == 1)
                {
                    client.RestoreNode(contNodes);
                    Console.WriteLine("Se ha restaurado el nodo: " + contNodes);
                    contNodes++;
                }
                Console.WriteLine("\nCliente (Node) agregado");
                listNodes.Add(client);
            }
        }

        /// <summary>
        /// Solicita a los nodos los fragmentos de los archivos
        /// </summary>
        /// <param name="s_client">Socket asociado al cliente</param>
        public void RequestFileFragmentsToNode(Socket s_client)
        {
            List<string> listFileNames = nodeBusiness.GetListFile();
            for (int i = 0; i < listFileNames.Count; i++)
            {
                listFragments = new();
                for (int j = 0; j < listNodes.Count; j++)
                {
                    if (!listNodes[j].IsAvailable)
                    {
                        RequestDataToAvailableNodes(j, listFileNames[i]);
                    }
                    else
                    {
                        listNodes[j].RequesFragmentToNode("getFragment", listFileNames[i], i);
                    }
                }
                SendFragmentsToSaSearch(s_client, "fileResponse");
            }
        }

        /// <summary>
        /// Verfica cuales nodos estan disponibles y les hace las solicitud de los fragmentos solicitados en los nodos
        /// que estan disponibles en ese momente
        /// </summary>
        /// <param name="index">Indice del nodo que no esta disponible</param>
        /// <param name="fileName">Nombre del archivo</param>
        public void RequestDataToAvailableNodes(int index, string fileName)
        {
            for (int i = 0; i < listNodes.Count; i++)
            {
                if (i != index)
                {
                    listNodes[i].RequesFragmentToNode("getParity", fileName, i);
                }
            }
        }

        /// <summary>
        /// Envia al saSearch los archivos o los metadatos
        /// </summary>
        /// <param name="s_client">Socket asociado al cliente</param>
        /// <param name="protocol">Protocolo con el que se comunica con el cliente</param>
        public void SendFragmentsToSaSearch(Socket s_client, string protocol)
        {
            foreach (Client cliente in listaClientes)
            {
                if (cliente.Socket.Equals(s_client))
                {
                    cliente.SendTheRequestedToSaSearch(CommonMethod.ConcatByteArrays(listFragments), protocol);
                }
            }
        }

        /// <summary>
        /// Verifica si existe algun nodo apago. Devuelve un int > 1 del nodo apagado y -1 si no hay nodos apagados
        /// </summary>
        /// <param name="s_client">Socket asociado al cliente</param>
        private void IsAvailable(Socket s_client)
        {
            foreach (Client cliente in listaClientes)
            {
                if (cliente.Socket.Equals(s_client))
                {
                    for (int i = 0; i < listNodes.Count; i++)
                    {
                        if (!listNodes[i].IsAvailable)
                        {
                            Console.WriteLine("Nodo apagado: " + i + 1);
                            cliente.SendIsAvailabilityNode(i + 1);
                        }
                        else
                        {
                            cliente.SendIsAvailabilityNode(-1);
                        }
                    }
                }
            }
        }
    }
}