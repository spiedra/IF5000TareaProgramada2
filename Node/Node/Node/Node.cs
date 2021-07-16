using IF500_tftp_client.Client;
using IF500_tftp_client.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Node.Node
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

        public void escucha()
        {
            try
            {
                while (true)
                {
                    String message = c.Receive();

                    switch (Utility.splitTheClientRequest(message, 0))
                    {
                        case "restore":
                            //elimina la carpeta vieja si existe, y crea una nueva
                            this.RestoreNode(Utility.splitTheClientRequest(message, 1));
                            break;

                        case "saveFragment":
                            string fragmentName = Utility.splitTheClientRequest(message, 1);
                            string nodeName = Utility.splitTheClientRequest(message, 2);
                            Byte[] fragmentFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, fragmentName, fragmentFile);
                            break;

                        case "saveMetaDataFile":
                            string FileName = Utility.splitTheClientRequest(message, 1);
                            nodeName = Utility.splitTheClientRequest(message, 2);
                            Byte[] metaFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, FileName, metaFile);
                            break;

                        case "saveParity":
                            string ParityName = Utility.splitTheClientRequest(message, 1);
                            nodeName = Utility.splitTheClientRequest(message, 2);
                            Byte[] parityFile = Encoding.ASCII.GetBytes(c.Receive());
                            this.SaveFileNode(nodeName, ParityName, parityFile);
                            break;

                        case "getFragment":
                            nodeName = Utility.splitTheClientRequest(message, 1);
                            fragmentName = Utility.splitTheClientRequest(message, 2);
                            c.Send("fragFile*");
                            c.sendBytesMsg(this.GetFile(nodeName, fragmentName));
                            break;

                        case "getParity":
                            nodeName = Utility.splitTheClientRequest(message, 1);
                            string parityName = Utility.splitTheClientRequest(message, 2);
                            c.Send("parity*");
                            c.sendBytesMsg(this.GetParity(nodeName, parityName));
                            break;

                        case "getMetaData":
                            nodeName = Utility.splitTheClientRequest(message, 1);
                            string metaDataName = Utility.splitTheClientRequest(message, 2);
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

        private void RestoreNode(string nodo)
        {
            string ruta = @"../../../Nodes/" + nodo;
            if (Directory.Exists(ruta))
            {
                Directory.Delete(ruta);
            }
            Directory.CreateDirectory(ruta);
        }

        private void SaveFileNode(string nodeName, string fileName, Byte[] b)
        {
            string rutaNombreArchivo = @"../../../Nodes/" + nodeName + "/" + fileName;
            using (FileStream newFile = new FileStream(rutaNombreArchivo, FileMode.Create, FileAccess.Write))
            {
                newFile.Write(b, 0, b.Length);
                newFile.Flush();
                newFile.Close();
            }
        }

        private Byte[] GetFile(string nodeName, string fileName)
        {
            return Utility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/" + fileName);
        }

        private Byte[] GetParity(string nodeName, string fileName)
        {
            return Utility.ConvertFileToByteArray(@"../../../Nodes/" + nodeName + "/Parity/" + fileName);
        }
    }
}
