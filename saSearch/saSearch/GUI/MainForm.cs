using IF500_tftp_client.Client;
using saSearch.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saSearch
{
    public partial class mainForm : Form
    {
        readonly Cliente c;
        public mainForm()
        {
            c = Cliente.GetSingletonCliente();
            c.Start();
            c.Send("setId*1");
            InitializeComponent();
        }
  
        private void toolStripVer_Click(object sender, EventArgs e)
        {
            VerArchivosForm verArchivosForm = new();
            verArchivosForm.Show();
        }

        private void toolStripSubir_Click(object sender, EventArgs e)
        {
            SubirArchivoForm form = new();
            form.Show();
        }

        private void toolStripMenuDisponibilidad_Click(object sender, EventArgs e)
        {
            DisponibilidadForm disponibilidad = new();
            disponibilidad.Show();
        }

        private void toolStripMenuCantidad_Click(object sender, EventArgs e)
        {
            ConfigServidorForm configServidorForm = new();
            configServidorForm.Show();
        }
    }
}
