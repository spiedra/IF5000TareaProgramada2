
namespace saSearch
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripSubir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripVer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSubir,
            this.toolStripVer,
            this.toolStripConfig});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(800, 28);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // toolStripSubir
            // 
            this.toolStripSubir.Name = "toolStripSubir";
            this.toolStripSubir.Size = new System.Drawing.Size(57, 24);
            this.toolStripSubir.Text = "&Subir";
            this.toolStripSubir.Click += new System.EventHandler(this.toolStripSubir_Click);
            // 
            // toolStripVer
            // 
            this.toolStripVer.Name = "toolStripVer";
            this.toolStripVer.Size = new System.Drawing.Size(44, 24);
            this.toolStripVer.Text = "&Ver";
            this.toolStripVer.Click += new System.EventHandler(this.toolStripVer_Click);
            // 
            // toolStripConfig
            // 
            this.toolStripConfig.Name = "toolStripConfig";
            this.toolStripConfig.Size = new System.Drawing.Size(67, 24);
            this.toolStripConfig.Text = "&Config";
            this.toolStripConfig.Click += new System.EventHandler(this.toolStripConfig_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu principal";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripSubir;
        private System.Windows.Forms.ToolStripMenuItem toolStripVer;
        private System.Windows.Forms.ToolStripMenuItem toolStripConfig;
    }
}

