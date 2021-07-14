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
        Cliente c;
        public mainForm()
        {
            c = Cliente.GetSingletonCliente();
            c.Start();
            InitializeComponent();
        }

        private void toolStripVer_Click(object sender, EventArgs e)
        {
            VerArchivosForm verArchivosForm = new();
            verArchivosForm.Show();
        }

        private void toolStripConfig_Click(object sender, EventArgs e)
        {
            ConfigServidorForm configServidorForm = new();
            configServidorForm.Show();
        }

        private void toolStripSubir_Click(object sender, EventArgs e)
        {
            SubirArchivoForm form = new();
            form.Show();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
