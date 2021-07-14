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
    NodeConnectionBusiness nodeBusiness;
    public Server(string ip, int port)
    {
        nodeBusiness = new NodeConnectionBusiness();
        listaClientes = new List<Cliente>();
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
            this.listaClientes.Add(new Cliente(s_Client));
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
                case "identificador":
                    SetIndentificador(s_Client, Convert.ToInt32(Utility.SplitTheClientRequest(message, 1)));
                    break;

                case "infoArchivo":
                    SendMetaDataBufferToNode(message, nodeBusiness.GetNumberNodes());
                    break;

                case "archivo":


                    buffer = new byte[30000000];
                    s_Client.Receive(buffer);


                    break;

                case "cantidadNodos":
                    int cantidadNodos = Convert.ToInt32(Utility.SplitTheClientRequest(message, 1));
                    Console.WriteLine("Cantidad de nodos configurados: " + cantidadNodos);
                    this.DeleteNodes();
                    this.CreateNodes(cantidadNodos);
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
        for (int i = 1; i <= cantidadNodos; i++)
        {
            string direc = Utility.CreateFolderNode(i);
            nodeBusiness.RegisterNode(direc);
        }
    }

    public void SendPartition(string bytes)
    {
        for (int i = 1; i < this.listaClientes.Count; i++)
        {

        }
    }

    /// <summary>
    /// Asigna el identificador(cliente, nodo) a cada cliente
    /// </summary>
    private void SetIndentificador(Socket socket, int identificador)
    {
        foreach (Cliente cliente in listaClientes)
        {
            if (cliente.Socket.Equals(socket))
            {
                cliente.Identificador = identificador;
            }
        }
    }

    /// <summary>
    /// elimina todos los nodos (carpetas)
    /// </summary>
    public void DeleteNodes()
    {
        for (int i = 1; i <= 20; i++)
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
        for (int i = 1; i < listBufferMetaData.Count; i++)
        {
            listaClientes[i].SendMetaDataFile(listBufferMetaData[i], fileName);
        }
    }

    private void SendBufferFileToNode(byte[] bufferFile, string fileName)
    {
        nodeBusiness.InsertFile(fileName);

    }

    private void SendBufferParityToNode()
    {

    }
}
