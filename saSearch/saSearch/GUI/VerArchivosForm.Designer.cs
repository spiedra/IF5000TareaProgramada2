
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
            this.gbxListaArchivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxListaArchivos
            // 
            this.gbxListaArchivos.Controls.Add(this.dgvListaArchivos);
            this.gbxListaArchivos.Location = new System.Drawing.Point(13, 13);
            this.gbxListaArchivos.Name = "gbxListaArchivos";
            this.gbxListaArchivos.Size = new System.Drawing.Size(749, 300);
            this.gbxListaArchivos.TabIndex = 2;
            this.gbxListaArchivos.TabStop = false;
            this.gbxListaArchivos.Text = "Lista de archivos subidos";
            // 
            // dgvListaArchivos
            // 
            this.dgvListaArchivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaArchivos.Location = new System.Drawing.Point(6, 26);
            this.dgvListaArchivos.Name = "dgvListaArchivos";
            this.dgvListaArchivos.RowHeadersWidth = 51;
            this.dgvListaArchivos.RowTemplate.Height = 29;
            this.dgvListaArchivos.Size = new System.Drawing.Size(736, 268);
            this.dgvListaArchivos.TabIndex = 0;
            // 
            // VerArchivosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 328);
            this.Controls.Add(this.gbxListaArchivos);
            this.Name = "VerArchivosForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ver archivos";
            this.Load += new System.EventHandler(this.VerArchivosForm_Load);
            this.gbxListaArchivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxListaArchivos;
        private System.Windows.Forms.DataGridView dgvListaArchivos;
    }
}