
namespace saSearch.GUI
{
    partial class SubirArchivoForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_ubicacion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_mod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_acceso = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_tamano = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(40, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 368);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Subir un archivo";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(85, 307);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(515, 46);
            this.button2.TabIndex = 4;
            this.button2.Text = "Enviar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_ubicacion);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_mod);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tb_acceso);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_tamano);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tb_nombre);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(33, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 209);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivo seleccionado";
            // 
            // tb_ubicacion
            // 
            this.tb_ubicacion.Enabled = false;
            this.tb_ubicacion.Location = new System.Drawing.Point(142, 57);
            this.tb_ubicacion.Name = "tb_ubicacion";
            this.tb_ubicacion.Size = new System.Drawing.Size(420, 30);
            this.tb_ubicacion.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "Ubicación:";
            // 
            // tb_mod
            // 
            this.tb_mod.Enabled = false;
            this.tb_mod.Location = new System.Drawing.Point(142, 129);
            this.tb_mod.Name = "tb_mod";
            this.tb_mod.Size = new System.Drawing.Size(420, 30);
            this.tb_mod.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ult. Mod:";
            // 
            // tb_acceso
            // 
            this.tb_acceso.Enabled = false;
            this.tb_acceso.Location = new System.Drawing.Point(142, 92);
            this.tb_acceso.Name = "tb_acceso";
            this.tb_acceso.Size = new System.Drawing.Size(420, 30);
            this.tb_acceso.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Accesado:";
            // 
            // tb_tamano
            // 
            this.tb_tamano.Enabled = false;
            this.tb_tamano.Location = new System.Drawing.Point(142, 165);
            this.tb_tamano.Name = "tb_tamano";
            this.tb_tamano.Size = new System.Drawing.Size(420, 30);
            this.tb_tamano.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tamaño:";
            // 
            // tb_nombre
            // 
            this.tb_nombre.Enabled = false;
            this.tb_nombre.Location = new System.Drawing.Point(142, 23);
            this.tb_nombre.Name = "tb_nombre";
            this.tb_nombre.Size = new System.Drawing.Size(420, 30);
            this.tb_nombre.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(85, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(515, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Seleccionar archivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SubirArchivoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "SubirArchivoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubirArchivoForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_tamano;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_nombre;
        private System.Windows.Forms.TextBox tb_mod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_acceso;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ubicacion;
        private System.Windows.Forms.Label label5;
    }
}