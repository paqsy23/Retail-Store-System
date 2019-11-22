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
        int jum = 0;
        public pembeli(String path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
            cbjenis.Items.Add("pribadi");
            cbjenis.Items.Add("perusahaan");
            cbjenis.SelectedIndex = 0;
            edid.Enabled = false;
            refresh();
        }
        public void refresh()
        {
            tampilbuyer();
        }
        public void tampilbuyer()
        {
            OracleCommand cmd = new OracleCommand("select * from buyer", conn);
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
        private void btntambah_Click(object sender, EventArgs e)
        {
            bool email = IsValidEmail(edemail.Text);
            if(ednama.Text!="" && edalamat.Text !="" && edemail.Text != "" && email == true)
            {
                conn.Open();
                OracleCommand cmds = new OracleCommand("select count(id_buyer) from buyer", conn);
                string tempangka = cmds.ExecuteScalar().ToString();
                String id = "BY";
                int temp = Convert.ToInt32(tempangka) + 1;
                if(temp < 10){id = id+ "000" + temp.ToString();}
                else if(temp < 100){id = id + "00" + temp.ToString();}
                else if (temp < 1000){id = id + "0" + temp.ToString(); }
                else{id += temp.ToString(); }
                OracleCommand cmd2 = new OracleCommand(); 
                String insert = "insert into buyer(id_buyer,nama_buyer,alamat_buyer,email_buyer,jenis_buyer) values(:id_buyer,:nama_buyer,:alamat_buyer,:email_buyer,:jenis_buyer)";
                cmd2.Parameters.Add("id_buyer", id);
                cmd2.Parameters.Add("nama_buyer", ednama.Text);
                cmd2.Parameters.Add("alamat_buyer", edalamat.Text);
                cmd2.Parameters.Add("email_buyer", edemail.Text);
                cmd2.Parameters.Add("jenis_buyer", cbjenis.SelectedIndex.ToString());
                cmd2.Connection = conn;
                cmd2.CommandText = insert;
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("semua field harus terisi");
            }
            refresh();
        } 

        private void btnupdate_Click(object sender, EventArgs e)
        {
            bool email = IsValidEmail(edemail.Text);
            if (index > -1 && email == true)
            {
                conn.Open();
                OracleCommand cmds = new OracleCommand("update buyer set nama_buyer=:nama_buyer,alamat_buyer=:alamat_buyer,email_buyer=:email_buyer,jenis_buyer=:status where id_buyer='"+edid.Text+"'", conn);
                cmds.Parameters.Add("nama_buyer", ednama.Text);
                cmds.Parameters.Add("alamat_buyer", edalamat.Text);
                cmds.Parameters.Add("email_buyer", edemail.Text);
                cmds.Parameters.Add("status",cbjenis.SelectedIndex.ToString());
                cmds.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                MessageBox.Show("pilih field untuk di update");
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
            if(a == "0")
            {
                cbjenis.SelectedIndex = 0;
            }
            else
            {
                cbjenis.SelectedIndex = 1;
            }
            btntambah.Enabled = false;
        }

        private void pembeli_Load(object sender, EventArgs e)
        {

        }
    }
}
