using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IF500_tftp_client.Client
{
    class Cliente
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;
        Socket s_Client;

        static Cliente SINGLETONCLIENTE = null;

        public Cliente(string ip, int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);
            s_Client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public static Cliente GetSingletonCliente()
        {
            if (SINGLETONCLIENTE == null)
            {
                SINGLETONCLIENTE=new Cliente("localhost", 4404);
            }
            return SINGLETONCLIENTE;
        }

        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        public void Close()
        {
            s_Client.Shutdown(SocketShutdown.Both);
            s_Client.Close();
        }

        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);
            s_Client.Send(byteMsg);
        }

        public void sendBytesMsg(byte[] byteMsg)
        {
            s_Client.Send(byteMsg);
        }

        public string Receive()
        {
            byte[] buffer = new byte[30000000];
            s_Client.Receive(buffer);
            return byte2string(buffer);
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
    }
}