using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using IF500_tftp_server.Utility;
using System.Collections.Generic;
using ControllerNode.Cliente;
using IF500_tftp_server.Business;

public class Server
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;
    Socket s_Server;
    Socket s_Client;
    List<Cliente> listaClientes;
    List<Cliente> listNodes;
    List<byte[]> listFragments;
    NodeConnectionBusiness nodeBusiness;
    public Server(string ip, int port)
    {
        nodeBusiness = new NodeConnectionBusiness();
        listaClientes = new List<Cliente>();
        listNodes = new List<Cliente>();
        host = Dns.GetHostEntry(ip);
        ipAddr = host.AddressList[0];
        endPoint = new IPEndPoint(ipAddr, port);
        s_Server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        s_Server.Bind(endPoint);
        s_Server.Listen(10);
    }

    public void Start()
    {
        Thread t;
        while (true)
        {
            Console.Write("Esperando conexiones...");
            s_Client = s_Server.Accept();
            t = new Thread(clientConnection);
            t.Start(s_Client);
            Console.Write("Se ha conectado un cliente...");
        }
    }

    public void clientConnection(object s)
    {
        Socket s_Client = (Socket)s;
        string message;
        byte[] buffer;
        bool conectado = true;
        while (conectado)
        {
            buffer = new byte[30000000];
            s_Client.Receive(buffer);
            message = Utility.Byte2string(buffer);

            switch (Utility.SplitTheClientRequest(message, 0))
            {
                case "setId":
                    SetId(s_Client, Convert.ToInt32(Utility.SplitTheClientRequest(message, 1)));
                    break;

                case "infoArchivo":
                    SendMetaDataBufferToNode(message, nodeBusiness.GetNumberNodes());
                    break;

                case "getMetaData": // obtains each metadata of each node and send them
                    RequestMetaDataFragmentsToNode(s_Client);
                    break;

                case "getFile":
                    RequestFileFragmentsToNode(s_Client);
                    break;

                case "archivo": // posible error en la sincronizacion del mensaje
                    buffer = new byte[30000000];
                    s_Client.Receive(buffer);

                    SendBufferFileToNode(buffer, Utility.SplitTheClientRequest(message, 1), nodeBusiness.GetNumberNodes());
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
                    int cantidadNodos = Convert.ToInt32(Utility.SplitTheClientRequest(message, 1));
                    Console.WriteLine("Cantidad de nodos configurados: " + cantidadNodos);
                    this.DeleteNodes();
                    this.CreateNodes(cantidadNodos);
                    break;

                case "setAvailability":
                    SetAvailability(Convert.ToBoolean(Utility.SplitTheClientRequest(message, 1)), Convert.ToInt32(Utility.SplitTheClientRequest(message, 2)));
                    break;
            }
        }
    }

    /// <summary>
    /// Crea carpetas que sirven de nodos
    /// </summary>
    /// <param name="cantidadNodos">cantidad de nodos a crear</param>
    public void CreateNodes(int cantidadNodos)
    {
        for (int i = 0; i <= cantidadNodos; i++)
        {
            string direc = Utility.CreateFolderNode(i);
            nodeBusiness.RegisterNode(direc);
        }
    }

    /// <summary>
    /// Agrega en las listas cliente los nodos y los saSearch respectivamente
    /// </summary>
    private void SetId(Socket s_Client, int identificador)
    {
        if (identificador == 1)
        {
            listaClientes.Add(new Cliente(s_Client));
        }
        else
        {
            listNodes.Add(new Cliente(s_Client));
        }
    }

    /// <summary>
    /// elimina todos los nodos (carpetas)
    /// </summary>
    public void DeleteNodes()
    {
        for (int i = 0; i <= 20; i++)
        {
            Utility.DeleteDirectories(i);
        }
        this.nodeBusiness.DeleteNodes();
    }

    /// <summary>
    /// Envia a cada nodo los meta datos del archivo
    /// </summary>
    public void SendMetaDataBufferToNode(string message, int cantNodes)
    {
        string fileName = Utility.SplitTheClientRequest(message, 1);
        List<byte[]> listBufferMetaData = Utility.GetListBufferMetaData(message, cantNodes, fileName);
        for (int i = 0; i < listBufferMetaData.Count; i++)
        {
            listNodes[i].SendMetaDataFileToNode(listBufferMetaData[i], fileName, "Node" + i);
        }
    }

    /// <summary>
    /// Asigna la disponibilidad de los nodos
    /// </summary>
    public void SetAvailability(bool available, int nodeId)
    {
        listNodes[nodeId].IsAvailable = available;
    }

    /// <summary>
    /// Envia el fragmento (buffer) del archivo correspondiente
    /// </summary>
    private void SendBufferFileToNode(byte[] bufferFile, string fileName, int nodesAmount)
    {
        nodeBusiness.InsertFile(fileName);
        List<byte[]> listBuffersFile = Utility.GetListByteArrays(bufferFile, nodesAmount);
        for (int i = 0; i < listBuffersFile.Count; i++)
        {
            string fragName = "frag" + i + fileName + ".txt", nodeName = "Node" + i;
            nodeBusiness.InsertFragment(fileName, fragName, nodeName);
            listNodes[i].SaveFilePartition(listBuffersFile[i], fragName, nodeName);
        }
        SendBufferParityToNode(listBuffersFile, fileName);
    }

    /// <summary>
    /// Envia los fragmentos (buffer) de la paridad del archivo correspondiente
    /// </summary>
    private void SendBufferParityToNode(List<byte[]> listBuffersFile, string fileName)
    {
        foreach (Cliente node in listNodes)
        {
            for (int i = 0; i < listBuffersFile.Count; i++)
            {
                node.MakeParity(listBuffersFile[i], "frag" + i + fileName + ".txt", "Node" + i);
            }
        }
    }

    /// <summary>
    /// Solicita a los nodos los fragmentos de los meta datos
    /// </summary>
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
                    RequestDataToAvailableNodes(j, listFileNames[i] + "MetaData");
                }
                else
                {
                    listNodes[j].RequesFragmentToNode("getMetaData", listFileNames[i] + "MetaData", i);
                }
            }
            SendFragmentsToSaSearch(s_client, "metaDataResponse");
        }
    }

    /// <summary>
    /// Solicita a los nodos los fragmentos de los archivos
    /// </summary>
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
    public void SendFragmentsToSaSearch(Socket s_client, string protocol)
    {
        foreach (Cliente cliente in listaClientes)
        {
            if (cliente.Socket.Equals(s_client))
            {
                cliente.SendTheRequestedToSaSearch(Utility.ConcatByteArrays(listFragments), protocol);
            }
        }
    }
}
