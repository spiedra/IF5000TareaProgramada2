using IF500_tftp_client.Client;
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

        public SubirArchivoForm()
        {
            InitializeComponent();
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            c = new Cliente("localhost", 4404);
            c.Start();
            c.Send("");
            c.Close();
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
            tb_nombre.Text = fileInfo.Name;
            tb_tamano.Text = Convert.ToString(fileInfo.Length) + " bytes";
            tb_acceso.Text = fileInfo.LastAccessTime.ToString();
            tb_mod.Text = fileInfo.LastWriteTime.ToString();
            tb_ubicacion.Text = fileInfo.DirectoryName;
        }
    }
}
