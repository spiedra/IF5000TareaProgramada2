using System.Text;
using System.Net;
using System.Net.Sockets;

namespace IF500_tftp_client.Client
{
    /// <summary>
    /// Clase cliente encargada de comunicarse con el servidor
    /// </summary>
    class Cliente
    {
        /// <summary>
        /// Referencia al IPHostEntry
        /// </summary>
        private readonly IPHostEntry host;

        /// <summary>
        /// Referencia al IPAddress
        /// </summary>
        private readonly IPAddress ipAddr;

        /// <summary>
        /// Referencia al IPEndPoint
        /// </summary>
        private readonly IPEndPoint endPoint;

        /// <summary>
        /// Referencia al Socket
        /// </summary>
        private readonly Socket s_Client;

        /// <summary>
        /// Referencia para el patron de diseño <b>Singleton</b>
        /// </summary>
        static Cliente SINGLETONCLIENTE = null;

        /// <summary>
        /// Constructor de la clase <b>Cliente</b>
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
        /// Obtiene el singleton del cliente
        /// </summary>
        /// <returns>Retorna una instancia singleton</returns>
        public static Cliente GetSingletonCliente()
        {
            if (SINGLETONCLIENTE == null)
            {
                SINGLETONCLIENTE=new Cliente("localhost", 4404);
            }
            return SINGLETONCLIENTE;
        }

        /// <summary>
        /// Empieza la conexion con el socket
        /// </summary>
        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        /// <summary>
        /// Cierra conexion con el socket
        /// </summary>
        public void Close()
        {
            s_Client.Shutdown(SocketShutdown.Both);
            s_Client.Close();
        }

        /// <summary>
        /// Envia al servidor (ControllerNode) mensajes
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string msg)
        {
            byte[] byteMsg = Encoding.ASCII.GetBytes(msg);
            s_Client.Send(byteMsg);
        }

        /// <summary>
        /// Envia mensajes al servidor (ControllerNode) mensajes en formato arreglo de bytes
        /// </summary>
        /// <param name="byteMsg"></param>
        public void SendBytesMsg(byte[] byteMsg)
        {
            s_Client.Send(byteMsg);
        }

        /// <summary>
        /// Retornar un buffer con el mensaje recibo desde el servidor
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Mensaje recibido del servidor en formato de arreglo de bytes</returns>
        public byte[] ReceiveByteMsg(int length)
        {
            byte[] buffer = new byte[length];
            s_Client.Receive(buffer);
            return buffer;
        }

        /// <summary>
        /// Retornar un buffer con el mensaje recibo desde el servidor
        /// </summary>
        /// <param name="length"></param>
        /// <returns>Mensaje recibido del servidor en formato de cadena</returns>
        public string Receive()
        {
            byte[] buffer = new byte[30000000];
            s_Client.Receive(buffer);
            return Byte2string(buffer);
        }

        /// <summary>
        /// Convierte un arreglo de bytes en cadena
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>Arreglo de bytes convertido en cadena</returns>
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