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
    public partial class Barang : Form
    {
        OracleConnection conn = new OracleConnection("data source=orcl; user id=admin1;password=admin;");
        public Barang()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Barang_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            idtext.Enabled = false;
            groupBox1.Paint += PaintBorderlessGroupBox;
            groupBox2.Paint += PaintBorderlessGroupBox;
            ///ID tak perlu input(autogen)
            this.Top = 0;
            this.Left = 0;
            combopaten();
            refresh();
        }
        private void combopaten()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;                    
                }
            }
            foreach (Control c in groupBox2.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                }
            }

        }
        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Highlight);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.Black, 0, 0);
        }
        private void refresh()
        {
            conn.Open();
            isigudang();
            tampilbarang();
            isijenis();
            conn.Close();
        }
        private void isigudang()
        {  
            OracleCommand cmd = new OracleCommand("select ID_GUDANG from gudang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            combogudang.DataSource = ds.AsDataView();
            combogudang.DisplayMember = "ID_GUDANG";
            combogudang.ValueMember = "ID_GUDANG";
        }


        private void isijenis()
        {
            OracleCommand cmd = new OracleCommand("select ID_JENIS_BARANG,NAMA_JENIS_BARANG from JENIS_BARANG", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            combojenis.DataSource = ds.AsDataView();
            combojenis.DisplayMember = "NAMA_JENIS_BARANG";
            combojenis.ValueMember = "ID_JENIS_BARANG";
        }
        private void tampilbarang()
        {
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private bool checkform()
        {
            foreach(Control c in groupBox2.Controls)
            {
                if(c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == ""&&textBox.Name!="idtext")
                    {
                        return false;
                    }
                }
                else if(c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    if (ComboBox.Text == "" ||ComboBox.SelectedIndex==-1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool checksearch()
        {
            foreach (Control c in groupBox1.Controls)
            {                
                 if (c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    if (ComboBox.Text == ""||ComboBox.Text==string.Empty||ComboBox.SelectedIndex==-1)
                    {                       
                        return false;
                    }
                }
                else if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void Inset_Click(object sender, EventArgs e)
        {
            conn.Open();
            string id;
            string warna = warnatext.Text;
            string nama = this.namatext.Text;
            if (checkform() == true)
            {
                
                if (nama.Contains(" "))
                {
                    int ctr = 1;
                    string temp = nama.Substring(nama.IndexOf(" ") + ctr,1);
                    while (temp=="'" ||temp==" ")
                    {
                        ctr++;
                        temp = nama.Substring(nama.IndexOf(" ") + ctr,1);
                    }
                    MessageBox.Show(temp);

                    id = (nama.Substring(0, 1) + temp + warna.Substring(0, 2)).ToUpper() + comboukuran.SelectedIndex;
                    OracleCommand cmd = new OracleCommand("select count(id_barang)+1 from barang where id_barang LIKE '%" + id + "%'", conn);
                    string indexkosong = cmd.ExecuteScalar().ToString();
                    for (int i = indexkosong.Length; i < 3; i++)
                    {
                        indexkosong = "0" + indexkosong;
                    }
                    id = id + indexkosong;
                    cmd.CommandText = "insert into barang(id_barang, id_jenis_barang, id_gudang, nama_barang, warna_barang, ukuran, stock ,harga_beli, harga_jual) values(:id_barang, :id_jenis_barang, :id_gudang, :nama_barang, :warna_barang, :ukuran, :stock, :harga_beli, :harga_jual)";
                    cmd.Parameters.Add("id_barang", id);
                    cmd.Parameters.Add("id_jenis_barang", combojenis.SelectedValue);
                    cmd.Parameters.Add("id_gudang", combogudang.SelectedValue);
                    cmd.Parameters.Add("nama_barang", namatext.Text);
                    cmd.Parameters.Add("warna_barang", warnatext.Text);
                    cmd.Parameters.Add("ukuran", comboukuran.SelectedValue);
                    cmd.Parameters.Add("stock", numericstock.Value);
                    cmd.Parameters.Add("harga_beli", numericbeli.Value);
                    cmd.Parameters.Add("harga_jual", numericjual.Value);
                    MessageBox.Show(cmd.CommandText);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    
                    id = (nama.Substring(0, 2) + warna.Substring(0, 2)).ToUpper() + comboukuran.SelectedIndex;
                    OracleCommand cmd = new OracleCommand("select count(id_barang)+1 from barang where id_barang LIKE '%" + id + "%'", conn);
                    string indexkosong = cmd.ExecuteScalar().ToString();
                    for (int i = indexkosong.Length; i < 3; i++)
                    {
                        indexkosong = "0" + indexkosong;
                    }
                    id = id + indexkosong;
                    cmd.CommandText = "insert into barang(id_barang, id_jenis_barang, id_gudang, nama_barang, warna_barang, ukuran, stock ,harga_beli, harga_jual) values(:id_barang, :id_jenis_barang, :id_gudang, :nama_barang, :warna_barang, :ukuran, :stock, :harga_beli, :harga_jual)";
                    cmd.Parameters.Add("id_barang", id);
                    cmd.Parameters.Add("id_jenis_barang", combojenis);
                    cmd.Parameters.Add("id_gudang", combogudang);
                    cmd.Parameters.Add("nama_barang", namatext);
                    cmd.Parameters.Add("warna_barang", warnatext);
                    cmd.Parameters.Add("ukuran", comboukuran);
                    cmd.Parameters.Add("stock", numericstock);
                    cmd.Parameters.Add("harga_beli", numericbeli);
                    cmd.Parameters.Add("harga_jual", numericjual);
                    MessageBox.Show(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                }
                
                
            }
            else
            {
                MessageBox.Show("Pastikan Semua Form Terisi Dengan Benar");
            }
            conn.Close();
            refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (checksearch() == true)
            {
                OracleCommand cmd = new OracleCommand("select * from barang where " + keysearch.Text + "='" + valuetext.Text + "'", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];               
            }
            else
            {
                MessageBox.Show("Pastikan Form Terisi Dengan Benar");
            }
            conn.Close();

        }
    }
}
