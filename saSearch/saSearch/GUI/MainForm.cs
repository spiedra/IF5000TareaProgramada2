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
        public mainForm()
        {
            InitializeComponent();
        }

        private void toolStripVer_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSubir_Click(object sender, EventArgs e)
        {
            SubirArchivoForm form = new SubirArchivoForm();
            form.Show();
        }
    }
}
