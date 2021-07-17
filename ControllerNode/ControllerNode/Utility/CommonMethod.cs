using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ControllerNode.Utility
{
    /// <summary>
    /// Clase estatica que contienen los metodos comunmente usados para el funcionamiento del sistema
    /// </summary>
    static class CommonMethod
    {
        /// <summary>
        /// Permite separar la cadena enviada por el cliente
        /// </summary>
        /// <param name="request">Mensaje a dividir</param>
        /// <param name="index">Indice de la pieza de la cadena</param>
        /// <returns>Devuelve la pieza solicitada de la cadena</returns>
        public static string SplitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split('*');
            return messaje[index];
        }

        /// <summary>
        /// Convierte un arreglo de bytes a cadena
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>Arreglo de bytes convertido a cadena</returns>
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

        /// <summary>
        /// Crea un directorio en el nodo
        /// </summary>
        /// <param name="nodeCount"></param>
        /// <returns></returns>
        public static string CreateFolderNode(int nodeCount)
        {
            string folderPath = @"../../../Nodes/" + "Node" + nodeCount;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                return folderPath;
            }
            return null;
        }

        /// <summary>
        /// Elimina los nodos del
        /// </summary>
        /// <param name="nodeCount"></param>
        public static void DeleteDirectories(int nodeCount)
        {
            string folderPath = @"../../../Nodes/" + "Node" + nodeCount;
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath);
            }
        }

        /// <summary>
        /// Convierte los archivos .txt en un arreglo de bytes
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Devulve un archivo .txt convertido en un arreglo de bytes</returns>
        public static byte[] ConvertFileToByteArray(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            return buffer;
        }

        /// <summary>
        /// Obtiene todos los arreglos de bytes
        /// </summary>
        /// <param name="pathList"></param>
        /// <returns>Devuelve una lista de bytes</returns>
        public static List<byte[]> GetAllByteArrays(List<string> pathList)
        {
            List<byte[]> byteList = new();
            foreach (string path in pathList)
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byteList.Add(ConvertFileToByteArray(path));
            }
            return byteList;
        }

        /// <summary>
        /// Parte en pedazos un arreglos de bytes segun la cantidad de nodos en el sistema
        /// </summary>
        /// <param name="buffer">Archivo convertido en un arreglo de bytes</param>
        /// <param name="numNodes">Cantidad de nodos en el sistema</param>
        /// <returns>Devuelve una lista con todos los pedazos del archivo</returns>
        public static List<byte[]> GetListByteArrays(byte[] buffer, int numNodes)
        {
            List<byte[]> listByte = new();
            int pieces = buffer.Length / numNodes;
            int remainder = buffer.Length - (pieces * numNodes);
            byte[] bufferTemp;

            for (int i = 0; i < numNodes; i++)
            {
                if (i == numNodes - 1)
                {
                    bufferTemp = buffer.Take(pieces + remainder).ToArray();
                }
                else
                {
                    bufferTemp = buffer.Take(pieces).ToArray();
                }
                buffer = buffer.Skip(pieces).ToArray();
                listByte.Add(bufferTemp);
            }
            return listByte;
        }

        /// <summary>
        /// Concatena arreglos de bytes
        /// </summary>
        /// <param name="listByte">Lista con arreglos de bytes</param>
        /// <returns>Devuelve un solo arreglo de bytes</returns>
        public static byte[] ConcatByteArrays(List<byte[]> listByte)
        {
            byte[] bytes = Array.Empty<byte>();
            foreach (byte[] b in listByte)
            {
                bytes = bytes.Concat(b).ToArray();
            }
            return bytes;
        }


        /// <summary>
        /// Convierte un arreglo de cadenas en un arreglo de bytes
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Devuelve un arreglo de bytes</returns>
        public static byte[] GetMetaDataBuffer(string[] values)
        {
            string tempFilePath = Path.GetTempFileName();
            using (FileStream fs = new(tempFilePath, FileMode.Open))
            {
                foreach (string metaData in values)
                {
                    fs.Write(new UTF8Encoding(true).GetBytes(metaData), 0, metaData.Length);
                }
            }
            byte[] buffer = ConvertFileToByteArray(tempFilePath);
            File.Delete(tempFilePath);
            return buffer;
        }

        /// <summary>
        /// Obtiene una lista de arreglos de bytes
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cantNodes"></param>
        /// <param name="fileName"></param>
        /// <returns>Lista de arreglos de bytes</returns>
        public static List<byte[]> GetListBufferMetaData(string message, int cantNodes, string fileName)
        {
            return GetListByteArrays(CommonMethod.GetMetaDataBuffer(new string[] {
                             fileName
                           , SplitTheClientRequest(message, 2)
                           , SplitTheClientRequest(message, 3)
                           , SplitTheClientRequest(message, 4)
                    }), cantNodes);
        }
    }
}
