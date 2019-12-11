using Oracle.DataAccess.Client;
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
    public partial class reportSuratJalan : Form
    {
        OracleConnection conn;
        public reportSuratJalan()
        {
            InitializeComponent();
            conn = new OracleConnection("user id=" + logins.user + ";password=" + logins.pass + ";");
            SuratJalan cr = new SuratJalan();
            cr.SetDatabaseLogon(logins.user, logins.pass);
            conn.Open();
            DateTime dateTime = DateTime.UtcNow.Date;
            string id_hpengiriman = "";
            OracleCommand cmds = new OracleCommand("select h.ID from (select id_hpengiriman as ID from pengiriman order by tanggal_pengiriman desc) h WHERE rownum=1", conn);
            id_hpengiriman = cmds.ExecuteScalar().ToString();
            cr.SetParameterValue("idHpengiriman", id_hpengiriman);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Zoom(90);
            this.MinimumSize = new Size(this.Width, this.Height);
        }
    }
}
