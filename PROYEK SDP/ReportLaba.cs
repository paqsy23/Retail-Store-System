using System;
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
    public partial class ReportLaba : Form
    {
        public ReportLaba()
        {
            InitializeComponent();
        }

        private void ReportLaba_Load(object sender, EventArgs e)
        {

            InitializeComponent();
            laba cr = new laba();
            cr.SetDatabaseLogon(logins.user, logins.pass);
            crystalReportViewer1.ReportSource = cr;
        } 
    }
}
