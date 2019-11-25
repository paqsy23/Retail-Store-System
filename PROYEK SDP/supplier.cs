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
            OracleCommand cmd = new OracleCommand("select * from supplier where status_delete=0 order by id_supplier", conn);
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
                    OracleCommand cmd = new OracleCommand("insert into supplier(id_supplier,nama_supplier,alamat_supplier,email_supplier,status_delete) values(:id_supplier,:nama_supplier,:alamat_supplier,:email_supplier,:status_delete)", conn);
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
                    cmd.Parameters.Add("status_delete", "0");
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
            bunifuMaterialTextbox1.Enabled = false;
            tampilsupplier();
            conn.Close();
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bunifuMaterialTextbox1.Text = bunifuCustomDataGrid1[0, e.RowIndex].Value.ToString();
            bunifuMaterialTextbox2.Text = bunifuCustomDataGrid1[1, e.RowIndex].Value.ToString();
            bunifuMaterialTextbox3.Text = bunifuCustomDataGrid1[2, e.RowIndex].Value.ToString();
            bunifuMaterialTextbox4.Text = bunifuCustomDataGrid1[3, e.RowIndex].Value.ToString();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            conn.Open();
            string id = bunifuMaterialTextbox1.Text;
            if (id != "")
            {
                OracleCommand cmd = new OracleCommand("update supplier set status_delete=1 where id_supplier='" + id + "'", conn);
                cmd.ExecuteNonQuery();
                tampilsupplier();
                conn.Close();
            }
            else
            {
                MessageBox.Show("pastikan semua form terisi");
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            conn.Open();
            string id = bunifuMaterialTextbox1.Text;
            if (id != "" && IsValidEmail(bunifuMaterialTextbox4.Text)&&bunifuMaterialTextbox2.Text != "" && bunifuMaterialTextbox3.Text != "" && bunifuMaterialTextbox4.Text != ""){
                OracleCommand cmd = new OracleCommand("update supplier set nama_supplier= :nama_supplier,email_supplier= :emailsupplier,alamat_supplier= :alamat_supplier where id_supplier='"+id+"'", conn);
                cmd.Parameters.Add("nama_supplier", bunifuMaterialTextbox2.Text);
                cmd.Parameters.Add("email_supplier", bunifuMaterialTextbox4.Text);
                cmd.Parameters.Add("alamat_supplier", bunifuMaterialTextbox3.Text);
                cmd.ExecuteNonQuery();
                tampilsupplier();
            }
            else
            {
                MessageBox.Show("Pastikan Semua from Terisi");
            }
            conn.Close();
        }
    }
}
