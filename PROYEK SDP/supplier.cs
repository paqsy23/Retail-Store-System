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
    public partial class supplier : Form
    {
        OracleConnection conn;
        public Master parent;
        public supplier(String path)
        {
            InitializeComponent();
            this.conn = new OracleConnection(path);
            this.Width = 1000;
            this.Height = 600;
            this.Location = new Point(0, 0);
        }

        private void btnBack(object sender, EventArgs e)
        {
            parent.showPostLogin();
            this.Close();
        }

        private void Hover_MouseEnter(object sender, EventArgs e)
        {
            PictureBox ini = (PictureBox)sender;
            ini.Cursor = Cursors.Hand;
        }
        private void tampilsupplier()
        {
            OracleCommand cmd = new OracleCommand("select * from supplier order by id_supplier", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
           bunifuCustomDataGrid1.DataSource = ds.Tables[0];
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {   

            if (bunifuMaterialTextbox2.Text != ""&& bunifuMaterialTextbox3.Text != ""&& bunifuMaterialTextbox4.Text != "")
            {
                bool cekEmail = IsValidEmail(bunifuMaterialTextbox4.Text);
                if (cekEmail)
                {

            
                    conn.Open();
                    string id = "SUP";
                    OracleCommand cmd = new OracleCommand("insert into supplier(id_supplier,nama_supplier,alamat_supplier,email_supplier) values(:id_supplier,:nama_supplier,:alamat_supplier,:email_supplier)", conn);
                    OracleCommand cmds = new OracleCommand("select count(*) from supplier", conn);
                    string temp = Int32.Parse(cmds.ExecuteScalar().ToString()) + 1 + "";
                    for (int i = temp.Length; i < 3; i++)
                    {
                        id += "0";
                    }
                    id += temp;
                    cmd.Parameters.Add("id_supplier", id);
                    cmd.Parameters.Add("nama_supplier", bunifuMaterialTextbox2.Text);
                    cmd.Parameters.Add("alamat_supplier", bunifuMaterialTextbox3.Text);
                    cmd.Parameters.Add("email_supplier", bunifuMaterialTextbox4.Text);
                    cmd.ExecuteNonQuery();
                    tampilsupplier();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Pastikan Format Email Benar");
                }
            }
            else
            {
                MessageBox.Show("Pastikan Semua Form Terisis");
            }
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            conn.Open();
            tampilsupplier();
            conn.Close();
        }
    }
}
