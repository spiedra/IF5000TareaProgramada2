
namespace saSearch.GUI
{
    partial class ConfigServidorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxConfigServidor = new System.Windows.Forms.GroupBox();
            this.btnConfirmCantidad = new System.Windows.Forms.Button();
            this.tbxCantidadNodos = new System.Windows.Forms.TextBox();
            this.lblCantidadNodos = new System.Windows.Forms.Label();
            this.gbxConfigServidor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxConfigServidor
            // 
            this.gbxConfigServidor.Controls.Add(this.btnConfirmCantidad);
            this.gbxConfigServidor.Controls.Add(this.tbxCantidadNodos);
            this.gbxConfigServidor.Controls.Add(this.lblCantidadNodos);
            this.gbxConfigServidor.Location = new System.Drawing.Point(12, 12);
            this.gbxConfigServidor.Name = "gbxConfigServidor";
            this.gbxConfigServidor.Size = new System.Drawing.Size(380, 154);
            this.gbxConfigServidor.TabIndex = 4;
            this.gbxConfigServidor.TabStop = false;
            this.gbxConfigServidor.Text = "Config Servidor";
            // 
            // btnConfirmCantidad
            // 
            this.btnConfirmCantidad.Location = new System.Drawing.Point(169, 96);
            this.btnConfirmCantidad.Name = "btnConfirmCantidad";
            this.btnConfirmCantidad.Size = new System.Drawing.Size(94, 29);
            this.btnConfirmCantidad.TabIndex = 7;
            this.btnConfirmCantidad.Text = "Confirmar";
            this.btnConfirmCantidad.UseVisualStyleBackColor = true;
            // 
            // tbxCantidadNodos
            // 
            this.tbxCantidadNodos.Location = new System.Drawing.Point(169, 37);
            this.tbxCantidadNodos.Name = "tbxCantidadNodos";
            this.tbxCantidadNodos.Size = new System.Drawing.Size(94, 27);
            this.tbxCantidadNodos.TabIndex = 6;
            // 
            // lblCantidadNodos
            // 
            this.lblCantidadNodos.AutoSize = true;
            this.lblCantidadNodos.Location = new System.Drawing.Point(21, 40);
            this.lblCantidadNodos.Name = "lblCantidadNodos";
            this.lblCantidadNodos.Size = new System.Drawing.Size(142, 20);
            this.lblCantidadNodos.TabIndex = 5;
            this.lblCantidadNodos.Text = "Cantidad de nodos: ";
            // 
            // ConfigServidorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 183);
            this.Controls.Add(this.gbxConfigServidor);
            this.Name = "ConfigServidorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración servidor";
            this.gbxConfigServidor.ResumeLayout(false);
            this.gbxConfigServidor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxConfigServidor;
        private System.Windows.Forms.Button btnConfirmCantidad;
        private System.Windows.Forms.TextBox tbxCantidadNodos;
        private System.Windows.Forms.Label lblCantidadNodos;
    }
}