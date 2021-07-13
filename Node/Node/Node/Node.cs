using IF500_tftp_client.Client;
using IF500_tftp_client.Utility;
using System;
using System.Collections.Generic;
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
                        case "envio":

                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                var error = se.SocketErrorCode;
            }
        }
    }
}
