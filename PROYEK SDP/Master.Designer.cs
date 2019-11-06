namespace PROYEK_SDP
{
    partial class Master
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterJualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterPenyesuaianBarangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegawaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterBeliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterToolStripMenuItem,
            this.masterJualToolStripMenuItem,
            this.masterPenyesuaianBarangToolStripMenuItem,
            this.pegawaiToolStripMenuItem,
            this.masterBeliToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.masterToolStripMenuItem.Text = "Master Barang";
            this.masterToolStripMenuItem.Click += new System.EventHandler(this.masterToolStripMenuItem_Click);
            // 
            // masterJualToolStripMenuItem
            // 
            this.masterJualToolStripMenuItem.Name = "masterJualToolStripMenuItem";
            this.masterJualToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.masterJualToolStripMenuItem.Text = "Master Jual";
            // 
            // masterPenyesuaianBarangToolStripMenuItem
            // 
            this.masterPenyesuaianBarangToolStripMenuItem.Name = "masterPenyesuaianBarangToolStripMenuItem";
            this.masterPenyesuaianBarangToolStripMenuItem.Size = new System.Drawing.Size(164, 20);
            this.masterPenyesuaianBarangToolStripMenuItem.Text = "Master Penyesuaian barang";
            // 
            // pegawaiToolStripMenuItem
            // 
            this.pegawaiToolStripMenuItem.Name = "pegawaiToolStripMenuItem";
            this.pegawaiToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.pegawaiToolStripMenuItem.Text = "Pegawai";
            this.pegawaiToolStripMenuItem.Click += new System.EventHandler(this.pegawaiToolStripMenuItem_Click);
            // 
            // masterBeliToolStripMenuItem
            // 
            this.masterBeliToolStripMenuItem.Name = "masterBeliToolStripMenuItem";
            this.masterBeliToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.masterBeliToolStripMenuItem.Text = "Master Beli";
            this.masterBeliToolStripMenuItem.Click += new System.EventHandler(this.masterBeliToolStripMenuItem_Click);
            // 
            // Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.Master_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterJualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterPenyesuaianBarangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pegawaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterBeliToolStripMenuItem;
    }
}