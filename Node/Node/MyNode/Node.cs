using Node.Client;
using Node.Utility;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Node.MyNode
{
    /// <summary>
    /// Clase que contiene todos los metodos necesarios para la comunicación cliente-servidor
    /// </summary>
    class Node
    {
        /// <summary>
        /// Referencia de <b>Cliente</b>
        /// </summary>
        readonly Cliente c;

        /// <summary>
        /// Referencia de <b>Thread</b>
        /// </summary>
        private readonly Thread t;


        /// <summary>
        /// Constructor de la clase <b>Node</b>
        /// </summary>
        public Node()
        {
            c = new Cliente("localhost", 4404);
            c.Start();
            c.Send("setId*2");
            t = new Thread(this.Escucha);
            t.Start();
            Console.WriteLine("Conectado al controllerNode");
        }

        /// <summary>
        /// Escucha mensajes recibidos del servidor (controllerNode)
        /// </summary>
        public void Escucha()
        {
            try
            {
                while (true)
                {
                    String message = c.Receive();
                    switch (MyUtility.SplitTheClientRequest(message, 0))
                    {
                        case "restoreNode":
                            RestoreNode(Convert.ToInt32(MyUtility.SplitTheClientRequest(message, 1)));
                            break;

                        case "saveFragment":
                            string fragmentName = MyUtility.SplitTheClientRequest(message, 1);
                            string nodeName = MyUtility.SplitTheClientRequest(message, 2);
                            Thread.Sleep(30);
                            Byte[] fragmentFile = Encoding.ASCII.GetBytes(c.Receive());
                            SaveFileNode(nodeName, fragmentName, fragmentFile);
                            break;

                        case "saveMetaDataFile":
                            string FileName = MyUtility.SplitTheClientRequest(message, 1);
                            nodeName = MyUtility.SplitTheClientRequest(message, 2);
                            Byte[] metaFile = Encoding.ASCII.GetBytes(c.Receive());
                            SaveFileNode(nodeName, FileName, metaFile);
                            break;

                        case "saveParity":
                            string ParityName = MyUtility.SplitTheClientRequest(message, 1);
                            nodeName = MyUtility.SplitTheClientRequest(message, 2);
                            Console.WriteLine("\n(Case SaveParity) Parity Name: " + ParityName + " nodeName: " + nodeName);
                            //Thread.Sleep(200);
                            Byte[] parityFile = Encoding.ASCII.GetBytes(c.Receive());
                            Console.WriteLine("\n(Case saveParity) Tamanio del parityFile es: " + parityFile.Length);
                            SaveFileParity(nodeName, ParityName, parityFile);
                            break;

                        case "getFragment":
                            nodeName = MyUtility.SplitTheClientRequest(message, 1);
                            fragmentName = MyUtility.SplitTheClientRequest(message, 2);
                            byte[] bufferTemp1 = GetFile(nodeName, fragmentName);
                            c.Send("fragFile*" + bufferTemp1.Length);
                            c.SendBytesMsg(bufferTemp1);
                            break;

                        case "getParity":
                            nodeName = MyUtility.SplitTheClientRequest(message, 1);
                            string parityName = MyUtility.SplitTheClientRequest(message, 2);
                            byte[] bufferParity = GetParity(nodeName, parityName);
                            c.Send("parity*"+bufferParity.Length);
                            c.SendBytesMsg(bufferParity);
                            break;

                        case "getMetaData":
                            nodeName = MyUtility.SplitTheClientRequest(message, 1);
                            string metaDataName = MyUtility.SplitTheClientRequest(message, 2);
                            byte[]bufferTemp2 = GetFile(nodeName, metaDataName);
                            c.Send("fragMetaData*" + bufferTemp2.Length);
                            c.SendBytesMsg(bufferTemp2);
                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                _ = se.SocketErrorCode;
            }
        }

        /// <summary>
        /// Elimina la carpeta si existe, crea un directorio nuevo de nodo
        /// <param name="nodeNumber">Número del nodo al que se desea ubicar</param>
        /// </summary>
        private static void RestoreNode(int nodeNumber)
        {
            Console.WriteLine("Restaurando el nodo: " + nodeNumber);
            string directory = @"../../../Nodes/Node" + nodeNumber;
            string parityDirectory = directory + "/Parity";
            if (Directory.Exists(directory))
            {
                Console.WriteLine("Eliminando el directorio: " + directory);
                System.IO.Directory.Delete(directory, true);
            }
            Console.WriteLine("Creando el directorio: " + directory);
            Directory.CreateDirectory(directory);
            Directory.CreateDirectory(parityDirectory);
        }

        /// <summary>
        /// Guarda archivo en el nodo especificado
        /// <param name="nodeName">Nombre de nodo a buscar</param>
        /// <param name="fileName">Nombre de archivo a buscar</param>
        /// <param name="bytes">Archivo en bytes</param>
        /// </summary>
        private static void SaveFileNode(string nodeName, string fileName, Byte[] bytes)
        {
            Console.WriteLine("\n+++++Guardando el fragmento del archivo: " + fileName + " en el nodo: " + nodeName);
            string rutaNombreArchivo = @"../../../Nodes/" + nodeName + "/" + fileName;
            using var newFile = new FileStream(rutaNombreArchivo, FileMode.Create, FileAccess.Write);
            newFile.Write(bytes, 0, bytes.Length);
            newFile.Flush();
            newFile.Close();
        }

        /// <summary>
        /// 
        /// <param name="nodeName">Nombre de nodo a buscar</param>
        /// <param name="fileName">Nombre de archivo a buscar</param>
        /// <param name="bytes">Archivo en bytes</param>
        /// </summary>
        private static void SaveFileParity(string nodeName, string fileName, Byte[] bytes)
        {
            Console.WriteLine("*** Guardando el fragmento de paridad del archivo: " + fileName + " en el nodo: " + nodeName);
            string rutaNombreArchivo = @"../../../Nodes/" + nodeName + "/Parity/" + "/" + fileName;
            using var newFile = new FileStream(rutaNombreArchivo, FileMode.Create, FileAccess.Write);
            newFile.Write(bytes, 0, bytes.Length);
            newFile.Flush();
            newFile.Close();
        }

        /// <summary>
        /// obtiene archivo de nodo segun nombre de nodo y archivo
        /// <param name="nodeName">Nombre de nodo a buscar</param>
        /// <param name="fileName">Nombre de archivo a buscar</param>
        /// </summary>
        /// <returns>Retorna un arreglo de byte</returns>
        private static Byte[] GetFile(string nodeName, string fileName)
        {
            return MyUtility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/" + fileName);
        }

        /// <summary>
        /// Obtiene la paridad del nodo y archivo especificado 
        /// <param name="fileName">nombre de archivo a buscar</param>
        /// <param name="nodeName">nombre de nodo a buscar</param>
        /// </summary>
        /// <returns>Retorna un arreglo de byte</returns>
        private static Byte[] GetParity(string nodeName, string fileName)
        {
            return MyUtility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/Parity/" + fileName);
        }
    }
}
