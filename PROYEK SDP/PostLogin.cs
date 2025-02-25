﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYEK_SDP
{
    public partial class PostLogin : Form
    {
        public Master parent;
        
        public PostLogin()
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            panelAdmin.Location = new Point(this.Width / 2 - panelAdmin.Width / 2, this.Height / 2 - panelAdmin.Height / 2);
            panelPegawai.Location = new Point(this.Width / 2 - panelPegawai.Width / 2, this.Height / 2 - panelPegawai.Height / 2);
            if (logins.jabatan == "Admin" || logins.jabatan == "Manager")
            {
                panelPegawai.Hide();
            }
            else
            {
                panelAdmin.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.showBarang();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.showPegawai();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parent.showPenyesuaian();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.showJual();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            parent.showBeli();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            parent.showSupplier();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            parent.showLogin();
            this.Close();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            parent.showPostSuratJalan();
            this.Close();
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            ReportLaba report = new ReportLaba();
            report.ShowDialog();
        }

        private void PostLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
