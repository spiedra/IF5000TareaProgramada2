using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace IF500_tftp_server.Utility
{
    class Utility
    {
        public static string SplitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split('*');
            return messaje[index];
        }

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

        public static void DeleteDirectories(int nodeCount)
        {
            string folderPath = @"../../../Nodes/" + "Node" + nodeCount;
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath);
            }
        }

        public static byte[] ConvertFileToByteArray(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            return buffer;
        }

        public static List<byte[]> GetAllByteArrays(string pathFileName, List<string> pathList)
        {
            List<byte[]> byteList = new();
            foreach (string path in pathList)
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byteList.Add(ConvertFileToByteArray(path));
            }
            return byteList;
        }

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

        public static string GetValidatedDirectoryPath(string directoryPath, int directoryExtesion)
        {
            string newDirectoryPath = directoryPath + directoryExtesion;
            if (!Directory.Exists(newDirectoryPath))
            {
                Directory.CreateDirectory(newDirectoryPath);
            }
            return newDirectoryPath;
        }

        public static List<string> CreateFileFragments(List<byte[]> listByte, string fileName)
        {
            int count = 0;
            string pathfileName;
            List<string> pathList = new();
            foreach (byte[] b in listByte)
            {
                pathfileName = GetValidatedDirectoryPath("***aqui va la ruta raiz***", count) + @"\" + fileName + count + ".txt";
                using (FileStream newFile = new FileStream(pathfileName, FileMode.Create, FileAccess.Write))
                {
                    pathList.Add(pathfileName);
                    newFile.Write(b, 0, b.Length);
                    newFile.Flush();
                    newFile.Close();
                }
                count++;
            }
            return pathList;
        }

        public static void CreateParity(List<string> pathList, string nodeDirectoryPath, string fileName)
        {
            foreach (string path in pathList)
            {
                if (!Directory.Exists(nodeDirectoryPath + @"\paridad")) { Directory.CreateDirectory(nodeDirectoryPath + @"\paridad"); };
                for (int i = 0; i < pathList.Count; i++)
                {
                    File.Copy(path, nodeDirectoryPath + @"\paridad\" + fileName + i + ".txt", true);
                }
            }
        }

        public static byte[] ConcatByteArrays(List<byte[]> listByte)
        {
            byte[] bytes = Array.Empty<byte>();
            foreach (byte[] b in listByte)
            {
                bytes = bytes.Concat(b).ToArray();
            }
            return bytes;
        }

        public static void WriteInsideFile(string directoryPath, string fileName, byte[] byteArray)
        {
            using FileStream newFile = new(directoryPath + @"\" + fileName, FileMode.Create, FileAccess.Write);
            newFile.Write(byteArray, 0, byteArray.Length);
            newFile.Flush();
            newFile.Close();
        }

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

        public static List<byte[]> GetListBufferMetaData(string message, int cantNodes, string fileName)
        {
            return GetListByteArrays(Utility.GetMetaDataBuffer(new string[] {
                             fileName
                           , SplitTheClientRequest(message, 2)
                           , SplitTheClientRequest(message, 3)
                           , SplitTheClientRequest(message, 4)
                    }), cantNodes);
        }
    }
}
