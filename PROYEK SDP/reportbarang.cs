﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions;
namespace PROYEK_SDP
{
    public partial class reportbarang : Form
    {
        public reportbarang()
        {
            InitializeComponent();
        }
        CrystalReport1 cr = new CrystalReport1();
        private void reportbarang_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            
            cr.SetDatabaseLogon(logins.user, logins.pass);
            cr.SetParameterValue("tanggal", "");
            cr.SetParameterValue("tanggal1", dateTimePicker1.Value);
            cr.SetParameterValue("tanggal2", dateTimePicker1.Value);
            dateTimePicker1.Value = new DateTime(2000, 1, 1, 00, 00, 0);
            dateTimePicker2.Value = new DateTime(2019, 1, 1, 23, 59, 59);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Zoom(90);
            this.MinimumSize = new Size(this.Width, this.Height);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cr.SetParameterValue("tanggal", "isi");
            cr.SetParameterValue("tanggal1", dateTimePicker1.Value);
            cr.SetParameterValue("tanggal2", dateTimePicker2.Value);
            crystalReportViewer1.ReportSource = cr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cr.SetParameterValue("tanggal", "");
            cr.SetParameterValue("tanggal1", dateTimePicker1.Value);
            cr.SetParameterValue("tanggal2", dateTimePicker2.Value);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
