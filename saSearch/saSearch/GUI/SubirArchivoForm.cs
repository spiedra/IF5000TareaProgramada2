using IF500_tftp_client.Client;
using IF500_tftp_client.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saSearch.GUI
{
    public partial class SubirArchivoForm : Form
    {
        private Cliente c;
        private FileInfo fileInfo;
        private string name, fileDirectory, tamano, ult_acceso, ult_mod;

        public SubirArchivoForm()
        {
            c = Cliente.GetSingletonCliente();
            InitializeComponent();
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            Byte[] bytes = Utility.ConvertFileToByteArray(fileDirectory);
            c.Send("archivo*"+name+"*"+tamano);//envio
            Thread.Sleep(30);
            c.sendBytesMsg(bytes);
            //
            Thread.Sleep(30);
            c.Send("infoArchivo*" + name + "*" + ult_acceso + "*" + ult_mod + "*" + tamano);
            //
        }

        private void btn_seleccionar_archivo_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var fileStream = openFileDialog.OpenFile();
                fileInfo = new(openFileDialog.FileName);
                SetFileInfoInTbx(fileInfo);
                using StreamReader reader = new(fileStream);
                string fileContent = reader.ReadToEnd();
            }
        }

        private void SetFileInfoInTbx(FileInfo fileInfo)
        {
            this.fileDirectory = fileInfo.FullName;
            tb_nombre.Text = this.name = fileInfo.Name;
            tb_tamano.Text = this.tamano = Convert.ToString(fileInfo.Length) + " bytes";
            tb_acceso.Text = this.ult_acceso = fileInfo.LastAccessTime.ToString();
            tb_mod.Text = this.ult_mod = fileInfo.LastWriteTime.ToString();
            tb_ubicacion.Text = fileInfo.DirectoryName;
        }


    }
}
