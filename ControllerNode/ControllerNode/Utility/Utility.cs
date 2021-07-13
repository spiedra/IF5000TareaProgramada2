using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing;
using System.Threading;

namespace IF500_tftp_server.Utility
{
    class Utility
    {
        public static string splitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split('*');
            return messaje[index];
        }

        public static string CreateFolderNode(int nodeCount)
        {
            string folderPath = @"../../../Nodes/" + "Node"+nodeCount;
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
    }

}
