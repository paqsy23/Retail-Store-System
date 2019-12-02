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
    public partial class reportNota : Form
    {
        OracleConnection conn;
        public reportNota()
        {
            conn = new OracleConnection("user id=" + logins.user + ";password=" + logins.pass + ";");
            InitializeComponent();
            Nota cr = new Nota();
            cr.SetDatabaseLogon(logins.user, logins.pass);
            conn.Open();
            DateTime dateTime = DateTime.UtcNow.Date;
            string id_htrans = "";
            OracleCommand cmds = new OracleCommand("select h.ID from (select id_htrans_out as ID from htrans_out order by tanggal_trans desc) h WHERE rownum=1", conn);
            id_htrans = cmds.ExecuteScalar().ToString();
            cr.SetParameterValue("Htrans_out", id_htrans);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
