﻿using IF500_tftp_client.Client;
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
            if (this.files.Count != 0)
            {
                this.rtb_contenido.Text = this.files.ElementAt(e.RowIndex);
            }
            else
            {
                MessageBox.Show("No hay texto que mostrar");
            }
        }

        public void escucha()
        {
            c.Send("getMetaData*");
            try
            {
                while (true)
                {
                    String message = c.Receive();
                    MessageBox.Show(message);
                    switch (Utility.splitTheClientRequest(message, 0))
                    {
                        case "metaDataResponse":
                            this.SetText(Encoding.Default.GetString(c.ReceiveByteMsg(Convert.ToInt32(Utility.splitTheClientRequest(message, 1)))));
                            break;

                        case "fileResponse":
                            //this.files.Add(Encoding.Default.GetString(c.ReceiveByteMsg()));
                            break;
                    }
                }
            }
            catch (SocketException se)
            {
                var error = se.SocketErrorCode;
            }
        }

        //public void ByteArrayToFile(byte[] metaData)
        //{
        //    string directory = Path.GetTempFileName();
        //    try
        //    {
        //        using (var fs = new FileStream(directory, FileMode.Open))
        //        {
        //            fs.Write(metaData, 0, metaData.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception caught in process: {0}", ex);
        //    }
        //    this.SetText(directory);
        //    File.Delete(directory);
        //}

        delegate void SetTextCallback(string text);

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
                this.dgvListaArchivos.Rows[currentRow].Cells[0].Value =metaData[0];
                this.dgvListaArchivos.Rows[currentRow].Cells[1].Value = metaData[1] + metaData[2];
                this.dgvListaArchivos.Rows[currentRow].Cells[2].Value = metaData[3]+ metaData[4];
                this.dgvListaArchivos.Rows[currentRow].Cells[3].Value = metaData[5];
            }
        }

        //public void PutMetaData()
        //{
        //    string FileToRead = Path.GetTempFileName();
        //    List<string> line = File.ReadLines(FileToRead).ToList();
        //    int currentRow = this.dgvListaArchivos.Rows.Count - 1;
        //    //
        //    this.dgvListaArchivos.Rows.Add(1); //posible error
        //    this.dgvListaArchivos.Rows[currentRow].Cells[0].Value = line.ElementAt(0);
        //    this.dgvListaArchivos.Rows[currentRow].Cells[1].Value = line.ElementAt(1);
        //    this.dgvListaArchivos.Rows[currentRow].Cells[2].Value = line.ElementAt(2);
        //    this.dgvListaArchivos.Rows[currentRow].Cells[3].Value = line.ElementAt(3);
        //    //Console.WriteLine(String.Join(Environment.NewLine, line));
        //}
    }
}