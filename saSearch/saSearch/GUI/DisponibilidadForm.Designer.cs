namespace saSearch.GUI
{
    partial class DisponibilidadForm
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
            this.caja1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // caja1
            // 
            this.caja1.Location = new System.Drawing.Point(12, 12);
            this.caja1.Name = "caja1";
            this.caja1.Size = new System.Drawing.Size(1041, 578);
            this.caja1.TabIndex = 0;
            this.caja1.TabStop = false;
            this.caja1.Text = "Disponibilidad";
            this.caja1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // DisponibilidadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 602);
            this.Controls.Add(this.caja1);
            this.Name = "DisponibilidadForm";
            this.Text = "Disponibilidad";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox caja1;
    }
}