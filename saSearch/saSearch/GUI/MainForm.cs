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
using saSearch.GUI;

namespace saSearch
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void toolStripVer_Click(object sender, EventArgs e)
        {

        }

<<<<<<< HEAD
        private void toolStripConfig_Click(object sender, EventArgs e)
        {
            ConfigServidorForm configServidorForm = new();
            configServidorForm.Show();
=======
        private void mainMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SubirArchivoForm form = new SubirArchivoForm();
            form.Show();
>>>>>>> 037581997252c5b28326a07dba60a56e62f2ad73
        }
    }
}
