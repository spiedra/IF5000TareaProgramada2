using IF500_tftp_client.Client;
using saSearch.Business;
using saSearch.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Xaml;

namespace saSearch.GUI
{
    public partial class DisponibilidadForm : Form
    {
        private NodoBusiness business;
        private Cliente c;
        private NodoApagadoSingleton nodoSingleton;
        public DisponibilidadForm()
        {
            InitializeComponent();
            this.business = new();
            c = Cliente.GetSingletonCliente();
            this.nodoSingleton = NodoApagadoSingleton.getApagadoSingleton();
            this.GenerateCheckBox();
        }

        public void GenerateCheckBox()
        {
            int posx = 10, posy = 30;
            int ancho = this.caja1.Width / 7;
            int alto = this.caja1.Height / 6;
            int nodo = -1;
            // nodo= this.getOffNode();
            if (this.business.GetNodeCount() == 0)
            {
                Label mensaje = new Label();
                mensaje.Location = new Point(posx, posy);
                mensaje.Width = 500;
                mensaje.Text = "No se ha encontrado ningún nodo disponible.";
                this.caja1.Controls.Add(mensaje);
            }
            for (int i = 1; i <= this.business.GetNodeCount(); i++)
            {
                if (this.caja1.Controls.Count != 0 && this.caja1.Controls.Count % 7 == 0)
                {
                    posx = 10;
                    posy = posy + alto;
                }
                CheckBox cb = new CheckBox();
                cb.Location = new Point(posx, posy);
                cb.Text = "Nodo " + i;
                cb.Visible = true;
                cb.Checked = true;
                if (i == nodo)
                {
                    cb.Checked = false;
                    this.nodoSingleton.EsNodoApagado = true;
                }
                cb.AutoCheck = false;
                cb.Click += cb_CheckState;
                cb.Name = i + "";
                this.caja1.Controls.Add(cb);
                posx = posx + ancho;
            }
        }

        public int getOffNode()
        {
            c.Send("isAvailable*");
            return Convert.ToInt32(c.Receive());
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cb_CheckState(object sender, EventArgs e)
        {
            CheckBox aux = (CheckBox)sender;
            if (!aux.Checked && this.nodoSingleton.EsNodoApagado)
            {
                this.nodoSingleton.EsNodoApagado = false;
                aux.Checked = true;
                c.Send("setAvailability*" + true + "*" + aux.Name);
            }
            else if (this.nodoSingleton.EsNodoApagado && aux.Checked)
            {
                MessageBox.Show("Ya un nodo está apagado");
            }
            else
            {
                this.nodoSingleton.EsNodoApagado = true;
                aux.Checked = false;
                MessageBox.Show("Nodo: " + aux.Name + " apagado");
                c.Send("setAvailability*" + false + "*" + aux.Name);
            }
        }
    }
}