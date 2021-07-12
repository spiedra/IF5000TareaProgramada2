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

public class Server
{
    IPHostEntry host;
    IPAddress ipAddr;
    IPEndPoint endPoint;
    Socket s_Server;
    Socket s_Client;

    public Server(string ip, int port)
    {
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
            switch (Utility.splitTheClientRequest(message, 0))
            {
                case "jaja":
                    break;
            }
        }
    }
}
