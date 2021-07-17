using IF500_tftp_server.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerNode.Cliente
{
    /// <summary>
    /// Clase donde se encuentran todos los metodos para la comunicacion entre servidor-cliente
    /// </summary>
    class Cliente
    {
        /// <summary>
        /// Contiene la instancia del socket que se asigno en el momento que se hizo conexión 
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Permite verificar si un nodo esta apagado o encendido 
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Constructor por defecto de la clase <b>Cliente</b>
        /// </summary>
        /// <param name="socket">Instancia del socket al que el cliente esta conectado</param>
        public Cliente(Socket socket)
        {
            this.Socket = socket;
        }

        /// <summary>
        /// Envia al nodo correspondiente la información necesaria para guardar los fragmentos de los archivos
        /// </summary>
        /// <param name="buffer">Fragmento del archivo</param>
        /// <param name="fragName">Nombre del fragmento del archivo</param>
        /// <param name="nodeName">Nombre del nodo en donde se van a guardar los fragmentos</param>
        public void SaveFilePartition(byte[] buffer, string fragName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveFragment*" + fragName + "*" + nodeName));
            Socket.Send(buffer);
        }

        /// <summary>
        /// Envia al nodo correspondiente la información necesaria para guardar los fragmentos de los archivos realizando redundancia de datos
        /// </summary>
        /// <param name="buffer">Fragmento del archivo</param>
        /// <param name="fragName">Nombre del fragmento del archivo</param>
        /// <param name="nodeName">Nombre del nodo en donde se van a guardar los fragmentos</param>
        public void SaveParity(byte[] buffer, string fragName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveParity*" + fragName + "*" + nodeName));
            Socket.Send(buffer);
        }

        /// <summary>
        /// Envia al nodo los fragmentos de los meta datos del archivo asociado
        /// </summary>
        /// <param name="buffer">Fragmento del archivo</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="nodeName">Nombre del nodo en donde se van a guardar los fragmentos</param>
        public void SendMetaDataFileToNode(byte[] buffer, string fileName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveMetaDataFile*" + fileName + "*" + nodeName));
            Socket.Send(buffer);
        }

        /// <summary>
        /// Envia los archivos y metadatos solicitados al saSearch 
        /// </summary>
        /// <param name="buffer">Fragmento del archivo</param>
        /// <param name="protocol">Protocolo por donde el saSearch va a recibir los datos</param>
        public void SendTheRequestedToSaSearch(byte[] buffer, string protocol)
        {
            Socket.Send(Encoding.ASCII.GetBytes(protocol + "*"));
            Socket.Send(buffer);
        }

        /// <summary>
        /// Le solicita al nodo correspondiente los fragmetos de los archivos o de los meta datos
        /// </summary>
        /// <param name="protocol">Protocolo por donde el saSearch va a recibir los datos</param>
        /// <param name="fileName">Nombre del archivo</param>
        /// <param name="index">Indice que permite identificar los archivos</param>
        public void RequesFragmentToNode(string protocol, string fileName, int index)
        {
            Socket.Send(Encoding.ASCII.GetBytes(protocol + "*Node" + index + "*" + fileName + "MetaData" + index));
        }

        /// <summary>
        /// Envia al saSearch el indentificador del nodo que esta apagando 
        /// </summary>
        /// <param name="nodeIndex">Identificador del nodo</param>
        public void SendIsAvailabilityNode(int nodeIndex)
        {
            Socket.Send(Encoding.ASCII.GetBytes(Convert.ToString(nodeIndex)));
        }
    }
}
