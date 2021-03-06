
namespace saSearch.GUI
{
    partial class VerArchivosForm
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
            this.gbxListaArchivos = new System.Windows.Forms.GroupBox();
            this.dgvListaArchivos = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ult_acceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ult_modificación = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tamano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.abrir = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tamaño = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ult_Modificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtb_contenido = new System.Windows.Forms.RichTextBox();
            this.gbxListaArchivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxListaArchivos
            // 
            this.gbxListaArchivos.Controls.Add(this.dgvListaArchivos);
            this.gbxListaArchivos.Location = new System.Drawing.Point(13, 13);
            this.gbxListaArchivos.Name = "gbxListaArchivos";
            this.gbxListaArchivos.Size = new System.Drawing.Size(795, 317);
            this.gbxListaArchivos.TabIndex = 2;
            this.gbxListaArchivos.TabStop = false;
            this.gbxListaArchivos.Text = "Lista de archivos subidos";
            // 
            // dgvListaArchivos
            // 
            this.dgvListaArchivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaArchivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Ult_acceso,
            this.ult_modificación,
            this.tamano,
            this.abrir});
            this.dgvListaArchivos.Location = new System.Drawing.Point(65, 26);
            this.dgvListaArchivos.Name = "dgvListaArchivos";
            this.dgvListaArchivos.RowHeadersWidth = 51;
            this.dgvListaArchivos.RowTemplate.Height = 29;
            this.dgvListaArchivos.Size = new System.Drawing.Size(701, 268);
            this.dgvListaArchivos.TabIndex = 0;
            this.dgvListaArchivos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListaArchivos_CellContentClick);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 125;
            // 
            // Ult_acceso
            // 
            this.Ult_acceso.HeaderText = "Ult.acceso";
            this.Ult_acceso.MinimumWidth = 6;
            this.Ult_acceso.Name = "Ult_acceso";
            this.Ult_acceso.Width = 125;
            // 
            // ult_modificación
            // 
            this.ult_modificación.HeaderText = "Ult.modificación";
            this.ult_modificación.MinimumWidth = 6;
            this.ult_modificación.Name = "ult_modificación";
            this.ult_modificación.Width = 125;
            // 
            // tamano
            // 
            this.tamano.HeaderText = "Tamaño";
            this.tamano.MinimumWidth = 6;
            this.tamano.Name = "tamano";
            this.tamano.Width = 125;
            // 
            // abrir
            // 
            this.abrir.HeaderText = "Ver";
            this.abrir.MinimumWidth = 6;
            this.abrir.Name = "abrir";
            this.abrir.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.abrir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.abrir.Width = 125;
            // 
            // Name2
            // 
            this.Name2.MinimumWidth = 6;
            this.Name2.Name = "Name2";
            this.Name2.Width = 125;
            // 
            // Tamaño
            // 
            this.Tamaño.MinimumWidth = 6;
            this.Tamaño.Name = "Tamaño";
            this.Tamaño.Width = 125;
            // 
            // Ult_Modificacion
            // 
            this.Ult_Modificacion.MinimumWidth = 6;
            this.Ult_Modificacion.Name = "Ult_Modificacion";
            this.Ult_Modificacion.Width = 125;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtb_contenido);
            this.groupBox1.Location = new System.Drawing.Point(13, 354);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 223);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contenido de archivo";
            // 
            // rtb_contenido
            // 
            this.rtb_contenido.Location = new System.Drawing.Point(65, 26);
            this.rtb_contenido.Name = "rtb_contenido";
            this.rtb_contenido.Size = new System.Drawing.Size(701, 179);
            this.rtb_contenido.TabIndex = 0;
            this.rtb_contenido.Text = "";
            // 
            // VerArchivosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 590);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxListaArchivos);
            this.Name = "VerArchivosForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ver archivos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VerArchivosForm_FormClosed);
            this.gbxListaArchivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxListaArchivos;
        private System.Windows.Forms.DataGridView dgvListaArchivos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tamaño;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ult_Modificacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ult_acceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn ult_modificación;
        private System.Windows.Forms.DataGridViewTextBoxColumn tamano;
        private System.Windows.Forms.DataGridViewButtonColumn abrir;
        private System.Windows.Forms.RichTextBox rtb_contenido;
    }
}