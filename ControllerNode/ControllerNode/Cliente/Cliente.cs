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
        public Socket socket { get; set; }

        public Cliente(Socket socket)
        {
            this.socket = socket;
        }

        public void SaveFilePartition(string filePartition)
        {
            
            this.socket.Send(Utility.);
        }
    }
}
