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
        List<string> files;
        public VerArchivosForm()
        {
            files = new List<string>();
            c = Cliente.GetSingletonCliente();
            t = new Thread(this.escucha);
            t.Start();
            InitializeComponent();
        }

        private void dgvListaArchivos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            c.Send("getFile*" + this.dgvListaArchivos.Rows[e.RowIndex].Cells[0].Value);
        }

        public void escucha()
        {
            c.Send("getMetaData*");
            try
            {
                while (true)
                {
                    String message = c.Receive();
                    //MessageBox.Show(message);
                    switch (Utility.splitTheClientRequest(message, 0))
                    {
                        case "metaDataResponse":
                            this.SetText(Encoding.Default.GetString(c.ReceiveByteMsg(Convert.ToInt32(Utility.splitTheClientRequest(message, 1)))));
                            break;

                        case "fileResponse":
                            MessageBox.Show("Archivo recibido");
                            SetFileContentText(Encoding.Default.GetString(c.ReceiveByteMsg(Convert.ToInt32(Utility.splitTheClientRequest(message, 1)))));
                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                var error = se.SocketErrorCode;
            }
        }
        delegate void SetTextCallback(string text);
        delegate void SetContentCallback(string text);

        private void SetText(string message)
        {
            if (this.dgvListaArchivos.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                int currentRow = this.dgvListaArchivos.Rows.Count - 1;
                //
                // List<string> list = File.ReadLines(message).ToList();
                string[] metaData = message.Split(' ');
                this.dgvListaArchivos.Rows.Add(1); //posible error
                this.dgvListaArchivos.Rows[currentRow].Cells[0].Value = metaData[0];
                this.dgvListaArchivos.Rows[currentRow].Cells[1].Value = metaData[1] + metaData[2];
                this.dgvListaArchivos.Rows[currentRow].Cells[2].Value = metaData[3] + metaData[4];
                this.dgvListaArchivos.Rows[currentRow].Cells[3].Value = metaData[5];

            }
        }

        private void SetFileContentText(string message)
        {
            if (this.dgvListaArchivos.InvokeRequired)
            {
                SetContentCallback d = new SetContentCallback(SetFileContentText);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.rtb_contenido.Text = message;
            }
        }

        private void VerArchivosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}