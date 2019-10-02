using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace PROYEK_SDP
{
    
    public partial class login : Form
    {
        OracleConnection conn = new OracleConnection("data source = orcl; user id = n217116624; password=217116624");
        public login()
        {
            InitializeComponent(); Master m = new Master();
            m.Visible = true;
            
        }

        private void login_Load(object sender, EventArgs e)
        {
            conn.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select id_pegawai,password from pegawai", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
                if (textBox1.Text== row["id_pegawai"].ToString()&&textBox2.Text== row["password"].ToString())
                {
                    MessageBox.Show("true");
                }
                
            }

        }
    }
}
