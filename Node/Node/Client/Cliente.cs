using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Node.Client
{
    /// <summary>
    /// Clase que permite la comunicación con el servidor
    /// </summary>
    class Cliente
    {
        /// <summary>
        /// Referencia de IPHostEntry
        /// </summary>
        readonly IPHostEntry host;

        /// <summary>
        /// Referencia de IPAddress
        /// </summary>
        readonly IPAddress ipAddr;

        /// <summary>
        /// Referencia de IPEndPoint
        /// </summary>
        readonly IPEndPoint endPoint;

        /// <summary>
        /// Referencia de Socket
        /// </summary>
        readonly Socket s_Client;

        /// <summary>
        /// Constructor de <b>Cliente</b>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public Cliente(string ip, int port)
        {
            host = Dns.GetHostEntry(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);
            s_Client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Inicia la conexión con el servidor
        /// </summary>
        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        /// <summary>
        /// Cierra la conexión con el servidor
        /// </summary>
        public void Close()
        {
            s_Client.Shutdown(SocketShutdown.Both);
            s_Client.Close();
        }

        /// <summary>
        /// Envia un mensaje en forma de arreglo de bytes al servidor
        /// </summary>
        /// <param name="msg">Mensaje que se quiere enviar al servidor</param>
        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);
            s_Client.Send(byteMsg);
        }

        /// <summary>
        /// Envia un mensaje en forma de arreglo de bytes al servidor sin tener que hacer alguna conversión 
        /// </summary>
        /// <param name="byteMsg">Mensaje que se quiere enviar al servidor</param>
        public void SendBytesMsg(byte[] byteMsg)
        {
            s_Client.Send(byteMsg);
        }

        /// <summary>
        /// Convierte el mensaje recibido del servidor de arreglo de bytes a cadena
        /// </summary>
        /// <returns>Devuelve el mensaje recibido del servidor convertido a cadena</returns>
        public string Receive()
        {
            byte[] buffer = new byte[30000000];
            s_Client.Receive(buffer);
            return Byte2string(buffer);
        }

        /// <summary>
        /// Convierte un arreglo de bytes a cadena
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>Devuelve un arreglo de bytes convertido en una cadena</returns>
        public static string Byte2string(byte[] buffer)
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