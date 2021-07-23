using IF500_tftp_client.Client;
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
    /// <summary>
    /// Clase de form que se encarga de configurar los nodos solicitados
    /// </summary>
    public partial class ConfigServidorForm : Form
    {

        Cliente c;
        public ConfigServidorForm()
        {
            c = Cliente.GetSingletonCliente();
            InitializeComponent();
        } 

        /// <summary>
        /// metodo que envia la cantidd de nodos a utilizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmCantidad_Click(object sender, EventArgs e)
        {
            c.Send("cantidadNodos*" + this.tbxCantidadNodos.Text);
        }
    }
}
