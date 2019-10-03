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
        OracleConnection conn;
        public login()
        {
            InitializeComponent(); Master m = new Master();
            m.Visible = true;
            try
            {
                conn = new OracleConnection("user id=system;password=maximillian99");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;

            try
            {
                string query = "select * from pegawai where id_pegawai = '" + user + "'";
                OracleDataAdapter oda = new OracleDataAdapter(query, conn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Selamat Datang " + dt.Rows[0][1].ToString());
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
