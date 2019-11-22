namespace PROYEK_SDP
{
    partial class pembeli
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pembeli));
            this.bunifuCustomDataGrid1 = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.edid = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.ednama = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.edalamat = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.edemail = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbjenis = new System.Windows.Forms.ComboBox();
            this.btntambah = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btnupdate = new Bunifu.Framework.UI.BunifuFlatButton();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuCustomDataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuCustomDataGrid1
            // 
            this.bunifuCustomDataGrid1.AllowUserToAddRows = false;
            this.bunifuCustomDataGrid1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bunifuCustomDataGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.bunifuCustomDataGrid1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.bunifuCustomDataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuCustomDataGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuCustomDataGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.bunifuCustomDataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bunifuCustomDataGrid1.DoubleBuffered = true;
            this.bunifuCustomDataGrid1.EnableHeadersVisualStyles = false;
            this.bunifuCustomDataGrid1.HeaderBgColor = System.Drawing.Color.SeaGreen;
            this.bunifuCustomDataGrid1.HeaderForeColor = System.Drawing.Color.SeaGreen;
            this.bunifuCustomDataGrid1.Location = new System.Drawing.Point(288, 12);
            this.bunifuCustomDataGrid1.Name = "bunifuCustomDataGrid1";
            this.bunifuCustomDataGrid1.ReadOnly = true;
            this.bunifuCustomDataGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuCustomDataGrid1.Size = new System.Drawing.Size(500, 334);
            this.bunifuCustomDataGrid1.TabIndex = 0;
            this.bunifuCustomDataGrid1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bunifuCustomDataGrid1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "nama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "alamat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "email";
            // 
            // edid
            // 
            this.edid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edid.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.edid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.edid.HintForeColor = System.Drawing.Color.Empty;
            this.edid.HintText = "";
            this.edid.isPassword = false;
            this.edid.LineFocusedColor = System.Drawing.Color.Blue;
            this.edid.LineIdleColor = System.Drawing.Color.Gray;
            this.edid.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.edid.LineThickness = 3;
            this.edid.Location = new System.Drawing.Point(72, 34);
            this.edid.Margin = new System.Windows.Forms.Padding(4);
            this.edid.Name = "edid";
            this.edid.Size = new System.Drawing.Size(197, 34);
            this.edid.TabIndex = 6;
            this.edid.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // ednama
            // 
            this.ednama.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ednama.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.ednama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ednama.HintForeColor = System.Drawing.Color.Empty;
            this.ednama.HintText = "";
            this.ednama.isPassword = false;
            this.ednama.LineFocusedColor = System.Drawing.Color.Blue;
            this.ednama.LineIdleColor = System.Drawing.Color.Gray;
            this.ednama.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.ednama.LineThickness = 3;
            this.ednama.Location = new System.Drawing.Point(72, 76);
            this.ednama.Margin = new System.Windows.Forms.Padding(4);
            this.ednama.Name = "ednama";
            this.ednama.Size = new System.Drawing.Size(197, 34);
            this.ednama.TabIndex = 7;
            this.ednama.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // edalamat
            // 
            this.edalamat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edalamat.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.edalamat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.edalamat.HintForeColor = System.Drawing.Color.Empty;
            this.edalamat.HintText = "";
            this.edalamat.isPassword = false;
            this.edalamat.LineFocusedColor = System.Drawing.Color.Blue;
            this.edalamat.LineIdleColor = System.Drawing.Color.Gray;
            this.edalamat.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.edalamat.LineThickness = 3;
            this.edalamat.Location = new System.Drawing.Point(72, 118);
            this.edalamat.Margin = new System.Windows.Forms.Padding(4);
            this.edalamat.Name = "edalamat";
            this.edalamat.Size = new System.Drawing.Size(197, 34);
            this.edalamat.TabIndex = 8;
            this.edalamat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // edemail
            // 
            this.edemail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edemail.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.edemail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.edemail.HintForeColor = System.Drawing.Color.Empty;
            this.edemail.HintText = "";
            this.edemail.isPassword = false;
            this.edemail.LineFocusedColor = System.Drawing.Color.Blue;
            this.edemail.LineIdleColor = System.Drawing.Color.Gray;
            this.edemail.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.edemail.LineThickness = 3;
            this.edemail.Location = new System.Drawing.Point(72, 160);
            this.edemail.Margin = new System.Windows.Forms.Padding(4);
            this.edemail.Name = "edemail";
            this.edemail.Size = new System.Drawing.Size(197, 34);
            this.edemail.TabIndex = 9;
            this.edemail.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "jenis";
            // 
            // cbjenis
            // 
            this.cbjenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbjenis.FormattingEnabled = true;
            this.cbjenis.Location = new System.Drawing.Point(72, 215);
            this.cbjenis.Name = "cbjenis";
            this.cbjenis.Size = new System.Drawing.Size(197, 21);
            this.cbjenis.TabIndex = 11;
            // 
            // btntambah
            // 
            this.btntambah.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btntambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btntambah.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntambah.BorderRadius = 0;
            this.btntambah.ButtonText = "tambah";
            this.btntambah.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btntambah.DisabledColor = System.Drawing.Color.Gray;
            this.btntambah.Iconcolor = System.Drawing.Color.Transparent;
            this.btntambah.Iconimage = ((System.Drawing.Image)(resources.GetObject("btntambah.Iconimage")));
            this.btntambah.Iconimage_right = null;
            this.btntambah.Iconimage_right_Selected = null;
            this.btntambah.Iconimage_Selected = null;
            this.btntambah.IconMarginLeft = 0;
            this.btntambah.IconMarginRight = 0;
            this.btntambah.IconRightVisible = true;
            this.btntambah.IconRightZoom = 0D;
            this.btntambah.IconVisible = true;
            this.btntambah.IconZoom = 90D;
            this.btntambah.IsTab = false;
            this.btntambah.Location = new System.Drawing.Point(34, 253);
            this.btntambah.Name = "btntambah";
            this.btntambah.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btntambah.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btntambah.OnHoverTextColor = System.Drawing.Color.White;
            this.btntambah.selected = false;
            this.btntambah.Size = new System.Drawing.Size(118, 49);
            this.btntambah.TabIndex = 12;
            this.btntambah.Text = "tambah";
            this.btntambah.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btntambah.Textcolor = System.Drawing.Color.White;
            this.btntambah.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntambah.Click += new System.EventHandler(this.btntambah_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnupdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnupdate.BorderRadius = 0;
            this.btnupdate.ButtonText = "update";
            this.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnupdate.DisabledColor = System.Drawing.Color.Gray;
            this.btnupdate.Iconcolor = System.Drawing.Color.Transparent;
            this.btnupdate.Iconimage = ((System.Drawing.Image)(resources.GetObject("btnupdate.Iconimage")));
            this.btnupdate.Iconimage_right = null;
            this.btnupdate.Iconimage_right_Selected = null;
            this.btnupdate.Iconimage_Selected = null;
            this.btnupdate.IconMarginLeft = 0;
            this.btnupdate.IconMarginRight = 0;
            this.btnupdate.IconRightVisible = true;
            this.btnupdate.IconRightZoom = 0D;
            this.btnupdate.IconVisible = true;
            this.btnupdate.IconZoom = 90D;
            this.btnupdate.IsTab = false;
            this.btnupdate.Location = new System.Drawing.Point(158, 253);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnupdate.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btnupdate.OnHoverTextColor = System.Drawing.Color.White;
            this.btnupdate.selected = false;
            this.btnupdate.Size = new System.Drawing.Size(110, 49);
            this.btnupdate.TabIndex = 13;
            this.btnupdate.Text = "update";
            this.btnupdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnupdate.Textcolor = System.Drawing.Color.White;
            this.btnupdate.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // pembeli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnupdate);
            this.Controls.Add(this.btntambah);
            this.Controls.Add(this.cbjenis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.edemail);
            this.Controls.Add(this.edalamat);
            this.Controls.Add(this.ednama);
            this.Controls.Add(this.edid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bunifuCustomDataGrid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "pembeli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pembeli";
            this.Load += new System.EventHandler(this.pembeli_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuCustomDataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomDataGrid bunifuCustomDataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox edid;
        private Bunifu.Framework.UI.BunifuMaterialTextbox ednama;
        private Bunifu.Framework.UI.BunifuMaterialTextbox edalamat;
        private Bunifu.Framework.UI.BunifuMaterialTextbox edemail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbjenis;
        private Bunifu.Framework.UI.BunifuFlatButton btntambah;
        private Bunifu.Framework.UI.BunifuFlatButton btnupdate;
    }
}