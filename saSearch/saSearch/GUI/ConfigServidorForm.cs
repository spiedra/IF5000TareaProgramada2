﻿using IF500_tftp_client.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace saSearch.GUI
{
    public partial class ConfigServidorForm : Form
    {

        Cliente c;
        public ConfigServidorForm()
        {
            c = Cliente.GetSingletonCliente();
            InitializeComponent();
        } 

        private void btnConfirmCantidad_Click(object sender, EventArgs e)
        {
            c.Send("cantidadNodos*" + this.tbxCantidadNodos.Text);
        }
    }
}
