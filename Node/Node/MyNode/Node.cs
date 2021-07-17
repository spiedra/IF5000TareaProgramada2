using Node.Client;
using Node.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Node.MyNode
{
    class Node
    {
        Cliente c;
        Thread t;
        public Node()
        {
            c = new Cliente("localhost", 4404);
            c.Start();
            c.Send("setId*2");
            t = new Thread(this.escucha);
            t.Start();
        }
        /// <summary>
        /// Escucha mensajes recibidos del controllerNode
        /// </summary>
        public void escucha()
        {
            try
            {
                while (true)
                {
                    String message = c.Receive();

                    switch (MyUtility.splitTheClientRequest(message, 0))
                    {
                        case "restoreNode":
                            this.RestoreNode(Convert.ToInt32(MyUtility.splitTheClientRequest(message, 1)));
                            break;

                        case "saveFragment":
                            string fragmentName = MyUtility.splitTheClientRequest(message, 1);
                            string nodeName = MyUtility.splitTheClientRequest(message, 2);
                            Byte[] fragmentFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, fragmentName, fragmentFile);
                            break;

                        case "saveMetaDataFile":
                            string FileName = MyUtility.splitTheClientRequest(message, 1);
                            nodeName = MyUtility.splitTheClientRequest(message, 2);
                            Byte[] metaFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, FileName, metaFile);
                            break;

                        case "saveParity":
                            string ParityName = MyUtility.splitTheClientRequest(message, 1);
                            nodeName = MyUtility.splitTheClientRequest(message, 2);
                            Byte[] parityFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, ParityName, parityFile);
                            break;

                        case "getFragment":
                            nodeName = MyUtility.splitTheClientRequest(message, 1);
                            fragmentName = MyUtility.splitTheClientRequest(message, 2);
                            c.Send("fragFile*");
                            c.sendBytesMsg(this.GetFile(nodeName, fragmentName));
                            break;

                        case "getParity":
                            nodeName = MyUtility.splitTheClientRequest(message, 1);
                            string parityName = MyUtility.splitTheClientRequest(message, 2);
                            c.Send("parity*");
                            c.sendBytesMsg(this.GetParity(nodeName, parityName));
                            break;

                        case "getMetaData":
                            nodeName = MyUtility.splitTheClientRequest(message, 1);
                            string metaDataName =MyUtility.splitTheClientRequest(message, 2);
                            c.Send("fragMetaData*");
                            c.sendBytesMsg(this.GetFile(nodeName, metaDataName));
                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                var error = se.SocketErrorCode;
            }
        }
        public void prueba()
        {
            Console.WriteLine("hola");
        }
        /// <summary>
        /// Elimina la carpeta si existe, crea un directorio nuevo de nodo
        /// <param name="nodeNumber">número del nodo al que se desea ubicar</param>
        /// </summary>
        private void RestoreNode(int nodeNumber)
        {
            string directory = @"../../../Nodes/Node" + nodeNumber;
            if (Directory.Exists(directory))
            {
                System.IO.Directory.Delete(directory, true);
            }
            Directory.CreateDirectory(directory);
        }
        /// <summary>
        /// Guarda archivo en el nodo especificado
        /// <param name="nodeName">nombre de nodo a buscar</param>
        /// <param name="fileName">nombre de archivo a buscar</param>
        /// <param name="bytes">archivo en bytes</param>
        /// </summary>
        private void SaveFileNode(string nodeName, string fileName, Byte[] bytes)
        {
            string rutaNombreArchivo = @"../../../Nodes/" + nodeName + "/" + fileName;
            using (FileStream newFile = new FileStream(rutaNombreArchivo, FileMode.Create, FileAccess.Write))
            {
                newFile.Write(bytes, 0, bytes.Length);
                newFile.Flush();
                newFile.Close();
            }
        }
        /// <summary>
        /// obtiene archivo de nodo segun nombre de nodo y archivo
        /// <param name="nodeName">nombre de nodo a buscar</param>
        /// <param name="fileName">nombre de archivo a buscar</param>
        /// </summary>
        private Byte[] GetFile(string nodeName, string fileName)
        {
            return MyUtility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/" + fileName);
        }
        /// <summary>
        /// Obtiene la paridad del nodo y archivo especificado 
        /// <param name="fileName">nombre de archivo a buscar</param>
        /// <param name="nodeName">nombre de nodo a buscar</param>
        /// </summary>
        private Byte[] GetParity(string nodeName, string fileName)
        {
            return MyUtility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/Parity/" + fileName);
        }
    }
}
