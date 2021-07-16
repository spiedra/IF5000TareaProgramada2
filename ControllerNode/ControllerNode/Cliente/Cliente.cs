using IF500_tftp_server.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControllerNode.Cliente
{
    class Cliente
    {
        public Socket Socket { get; set; }
        public int Identificador { get; set; }
        public bool IsAvailable { get; set; }

        public Cliente(Socket socket)
        {
            this.Socket = socket;
        }

        public void SaveFilePartition(byte[] buffer, string fragName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveFragment*" + fragName + "*" + nodeName));
            Socket.Send(buffer);
        }

        public void MakeParity(byte[] buffer, string fragName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveParity*" + fragName + "*" + nodeName));
            Socket.Send(buffer);
        }

        public void SendMetaDataFileToNode(byte[] buffer, string fileName, string nodeName)
        {
            Socket.Send(Encoding.ASCII.GetBytes("saveMetaDataFile*" + fileName + "*" + nodeName));
            Socket.Send(buffer);
        }

        public void SendTheRequestedToSaSearch(byte[] buffer, string protocol)
        {
            Socket.Send(Encoding.ASCII.GetBytes(protocol + "*"));
            Socket.Send(buffer);
        }

        public void RequesFragmentToNode(string protocol, string fileName, int index)
        {
            Socket.Send(Encoding.ASCII.GetBytes(protocol + "*Node" + index + "*" + fileName + "MetaData" + index));
        }

        public void SendAvailabilityNode(int nodeIndex)
        {
            Socket.Send(Encoding.ASCII.GetBytes(Convert.ToString(nodeIndex)));
        }
    }
}
