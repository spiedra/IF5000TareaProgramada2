using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using IF500_tftp_server.Utility;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using ControllerNode.Cliente;
using System.Collections;
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
    public string byte2string(byte[] buffer)
    {
        string message;
        int endIndex;
        message = Encoding.ASCII.GetString(buffer);
        endIndex = message.IndexOf('\0');
        if (endIndex > 0)
        {
            message = message.Substring(0, endIndex);
        }
        return message;
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
            message = byte2string(buffer);

            switch (Utility.SplitTheClientRequest(message, 0))
            {
                case "infoArchivo":
                    //
                    //aca se recibe el archivo y metadatos
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
        for(int i=1; i < this.listaClientes.Count; i++)
        {
            
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
}
