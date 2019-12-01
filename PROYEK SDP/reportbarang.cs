using System;
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

        private void reportbarang_Load(object sender, EventArgs e)
        {
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDatabaseLogon(logins.user, logins.pass);
            crystalReportViewer1.ReportSource = cr;

        }
    }
}
