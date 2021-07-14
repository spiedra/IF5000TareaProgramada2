using IF500_tftp_client.Client;
using IF500_tftp_client.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saSearch.GUI
{
    public partial class VerArchivosForm : Form
    {
        //Cliente c;
        //Thread t;

        public VerArchivosForm()
        {
            //c = new Cliente("localhost", 4404);
            //c.Start();
            //t = new Thread(this.escucha);
            //t.Start();
            InitializeComponent();
        }

        //public void escucha()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            String message = c.Receive();

        //            switch (Utility.splitTheClientRequest(message, 0))
        //            {
        //                case "jajaja":
        //                    break;
        //            }
        //        }
        //    }
        //    catch (SocketException se)
        //    {
        //        var error = se.SocketErrorCode;
        //    }
        //}
    }
}