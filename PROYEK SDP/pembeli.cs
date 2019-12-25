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
    public partial class pembeli : Form
    {
        OracleConnection conn;
        public pembeli(String path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
            cbjenis.Items.Add("pribadi");
            cbjenis.Items.Add("perusahaan");
            cbjenis.SelectedIndex = 0;
            edid.Enabled = false;
            refresh();
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btncancel.Visible = false;
            conn.Close();
        }
        public void refresh()
        {
            tampilbuyer();
        }
        public void tampilbuyer()
        {
            String tampil = "id_buyer as ID,nama_buyer as nama,alamat_buyer as alamat,email_buyer as email,jenis_buyer as jenis";
            OracleCommand cmd = new OracleCommand("select "+tampil+" from buyer where status_buyer=1", conn);
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
        bool isValidName(String name)
        {
            conn.Open();
            OracleCommand cmds = new OracleCommand("select count(nama_buyer) from buyer where nama_buyer='"+name+"'", conn);
            int tempangka =  Convert.ToInt32(cmds.ExecuteScalar().ToString());
            conn.Close();
            if (tempangka > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btntambah_Click(object sender, EventArgs e)
        {
            try
            {
                bool email = IsValidEmail(edemail.Text);
                bool ada = isValidName(ednama.Text);
                if (ednama.Text != "" && edalamat.Text != "" && edemail.Text != "" && email == true && ada == true)
                {
                    conn.Open();
                    OracleCommand cmds = new OracleCommand("select count(id_buyer) from buyer", conn);
                    string tempangka = cmds.ExecuteScalar().ToString();
                    String id = "BY";
                    int temp = Convert.ToInt32(tempangka) + 1;
                    if (temp < 10) { id = id + "000" + temp.ToString(); }
                    else if (temp < 100) { id = id + "00" + temp.ToString(); }
                    else if (temp < 1000) { id = id + "0" + temp.ToString(); }
                    else { id += temp.ToString(); }
                    OracleCommand cmd2 = new OracleCommand();
                    String insert = "insert into buyer(id_buyer,nama_buyer,alamat_buyer,email_buyer,jenis_buyer,status_buyer) values(:id_buyer,:nama_buyer,:alamat_buyer,:email_buyer,:jenis_buyer,1)";
                    cmd2.Parameters.Add("id_buyer", id);
                    cmd2.Parameters.Add("nama_buyer", ednama.Text);
                    cmd2.Parameters.Add("alamat_buyer", edalamat.Text);
                    cmd2.Parameters.Add("email_buyer", edemail.Text);
                    cmd2.Parameters.Add("jenis_buyer", cbjenis.SelectedItem.ToString());
                    cmd2.Connection = conn;
                    cmd2.CommandText = insert;
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("semua field harus terisi");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("pastikan semua terisi dan nama tidak boleh kembar");
                MessageBox.Show(ex.Message);
                conn.Close();
            }
            
            refresh();
        } 

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                bool email = IsValidEmail(edemail.Text);
                if (index > -1 && email == true)
                {
                    conn.Open();
                    OracleCommand cmds = new OracleCommand("update buyer set nama_buyer=:nama_buyer,alamat_buyer=:alamat_buyer,email_buyer=:email_buyer,jenis_buyer=:status where id_buyer='" + edid.Text + "'", conn);
                    cmds.Parameters.Add("nama_buyer", ednama.Text);
                    cmds.Parameters.Add("alamat_buyer", edalamat.Text);
                    cmds.Parameters.Add("email_buyer", edemail.Text);
                    cmds.Parameters.Add("status", cbjenis.SelectedItem.ToString());
                    cmds.ExecuteNonQuery();
                    conn.Close();
                    index = -1;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("pilih field untuk di update");
                }
            }
            catch
            {
                conn.Close();
                MessageBox.Show("pastikan format terisi dengan benar");
            }
            refresh();
        }
        int index = -1;
        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            edid.Text = bunifuCustomDataGrid1.Rows[index].Cells[0].Value.ToString();
            ednama.Text = bunifuCustomDataGrid1.Rows[index].Cells[1].Value.ToString();
            edalamat.Text = bunifuCustomDataGrid1.Rows[index].Cells[2].Value.ToString();
            edemail.Text = bunifuCustomDataGrid1.Rows[index].Cells[3].Value.ToString();
            String a = bunifuCustomDataGrid1.Rows[index].Cells[4].Value.ToString();
            if(a == "pribadi")
            {
                cbjenis.SelectedIndex = 0;
            }
            else
            {
                cbjenis.SelectedIndex = 1;
            }
            btntambah.Visible = false;
            btnupdate.Visible = true;
            btndelete.Visible = true;
            btncancel.Visible = true;
        }


        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (index > -1)
                {
                    conn.Open();
                    OracleCommand cmds = new OracleCommand("update buyer set status_buyer=0 where id_buyer='"+edid.Text+"'", conn);
                    cmds.ExecuteNonQuery();
                    conn.Close();
                    index = -1;
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("pilih field untuk di delete");
                }
            }
            catch
            {
                conn.Close();
                MessageBox.Show("pilih field terlebih dahulu");
            }
            refresh();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            btntambah.Visible = true;
            btnupdate.Visible = false;
            btndelete.Visible = false;
            btncancel.Visible = false;
            index = -1;
            edid.Text = "";
            ednama.Text = "";
            edalamat.Text = "";
            edemail.Text = "";
            cbjenis.SelectedIndex = 0;
        }

        private void pembeli_Load(object sender, EventArgs e)
        {

        }
    }
}
