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

        public Cliente(Socket socket)
        {
            this.Socket = socket;
        }

        public void SaveFilePartition(string filePartition)
        {
            
            //this.socket.Send(Utility.);
        }
    }
}
