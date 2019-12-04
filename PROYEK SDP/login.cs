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
        public Master parent;
        public login(string path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
            conn.Open();
            this.Location = new Point(0, 0);
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, this.Height / 2 - panel1.Height / 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool cek = false;
            OracleCommand cmd = new OracleCommand("select id_pegawai,password from pegawai where id_pegawai='"+textBox1.Text+"'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                
                if (textBox1.Text== row["id_pegawai"].ToString()&&textBox2.Text== row["password"].ToString())
                {
                    logins.username = textBox1.Text;
                    parent.showPostLogin();
                    this.Close();
                    cek = true;
                }
                
            }
            if (!cek) MessageBox.Show("Password Salah!");
        }

        private void textBox2_OnValueChanged(object sender, EventArgs e)
        {
            textBox2.isPassword = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
