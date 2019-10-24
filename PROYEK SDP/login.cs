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
        OracleConnection conn = new OracleConnection(" user id=admin1;password=admin;");
        public login()
        {
            InitializeComponent();
            conn.Open();   
        }

        private void login_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Parent.Width / 2 - this.Width / 2, this.Parent.Height / 2 - this.Height / 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OracleCommand cmd = new OracleCommand("select id_pegawai,password from pegawai where id_pegawai='"+textBox1.Text+"'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
                if (textBox1.Text== row["id_pegawai"].ToString()&&textBox2.Text== row["password"].ToString())
                {
                    PostLogin p1;
                    Master m = new Master();
                    p1 = new PostLogin();
                    p1.MdiParent = (Form)m;
                    this.Width = p1.Width;
                    this.Height = p1.Height + 24;
                    this.Close();
                    p1.Show();
                    MessageBox.Show("true");
                    this.Close();
                }
                
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select id_pegawai,password from pegawai where id_pegawai='" + textBox1.Text + "'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                if (textBox1.Text == row["id_pegawai"].ToString() && textBox2.Text == row["password"].ToString())
                {
                    PostLogin p1;
                    Master m = new Master();
                    p1 = new PostLogin();
                    p1.MdiParent = (Form)m;
                    this.Width = p1.Width;
                    this.Height = p1.Height + 24;
                    this.Close();
                    p1.Show();
                    MessageBox.Show("true");
                    this.Close();
                }

            }
        }
    }
}
