using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IF500_tftp_client.Utility
{
    class Utility
    {
        public static string splitTheClientRequest(string request, int index)
        {
            string[] messaje = request.Split(';');
            return messaje[index];
        }
    }
}
