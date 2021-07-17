using IF500_tftp_client.Client;
using IF500_tftp_client.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Cliente c;
        Thread t;
        List<string> archivos;
        public VerArchivosForm()
        {
            archivos = new List<string>();
            c = Cliente.GetSingletonCliente();
            //t = new Thread(this.escucha);
            //t.Start();
            InitializeComponent();
        }

        private void dgvListaArchivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("ajajajja");
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
                        case "metaDataResponse":
                            this.ByteArrayToFile(c.ReceiveByteMsg());
                            break;

                        case "fileResponse":

                            this.archivos.Add(Encoding.Default.GetString(c.ReceiveByteMsg()));

                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                var error = se.SocketErrorCode;
            }
        }
        public void ByteArrayToFile(byte[] byteArray)
        {
            string tempPath = Path.GetTempFileName();
            try
            {
                using (var fs = new FileStream(Path.GetTempFileName(), FileMode.Open))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
            }
            this.PutMetaData();
            File.Delete(tempPath);
        }

        public void PutMetaData()
        {
            string FileToRead = Path.GetTempFileName();
            List<string> line = File.ReadLines(FileToRead).ToList();
            int currentRow = this.dgvListaArchivos.Rows.Count - 1;
            //
            this.dgvListaArchivos.Rows.Add(1); //posible error
            this.dgvListaArchivos.Rows[currentRow].Cells[0].Value = line.ElementAt(0);
            this.dgvListaArchivos.Rows[currentRow].Cells[1].Value = line.ElementAt(1);
            this.dgvListaArchivos.Rows[currentRow].Cells[2].Value = line.ElementAt(2);
            this.dgvListaArchivos.Rows[currentRow].Cells[3].Value = line.ElementAt(3);
            //Console.WriteLine(String.Join(Environment.NewLine, line));
        }
    }
}