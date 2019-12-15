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
        OracleConnection conn;
        public Master parent;
        public Barang(string path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
        }

        private void Hover_MouseEnter(object sender, EventArgs e)
        {
            PictureBox ini = (PictureBox)sender;
            ini.Cursor = Cursors.Hand;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Barang_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
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
            isisupplier();
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
        private void isisupplier()
        {
            OracleCommand cmd = new OracleCommand("select ID_SUPPLIER,NAMA_SUPPLIER from SUPPLIER where status_delete=0", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            combosupplier.DataSource = ds.AsDataView();
            combosupplier.DisplayMember = "NAMA_SUPPLIER";
            combosupplier.ValueMember = "ID_SUPPLIER";
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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
                    string temp = nama.Substring(nama.IndexOf(" ") + ctr, 1);
                    while (temp == "'" || temp == " ")
                    {
                        ctr++;
                        temp = nama.Substring(nama.IndexOf(" ") + ctr, 1);
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
                    cmd.Parameters.Add("id_jenis_barang", combojenis.SelectedValue);
                    cmd.Parameters.Add("id_gudang", combogudang.SelectedValue);
                    cmd.Parameters.Add("nama_barang", namatext.Text);
                    cmd.Parameters.Add("warna_barang", warnatext.Text);
                    cmd.Parameters.Add("ukuran", comboukuran.SelectedItem.ToString());
                    cmd.Parameters.Add("stock", numericstock.Value);
                    cmd.Parameters.Add("harga_beli", numericbeli.Value);
                    cmd.Parameters.Add("harga_jual", numericjual.Value);
                    MessageBox.Show(comboukuran.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                }
                DateTime dateTime = DateTime.UtcNow.Date;
                int total = (int)numericbeli.Value * (int)numericstock.Value;
                string id_htrans = "HI" + (dateTime.ToString("ddMMyyyy"));
                OracleCommand cmds = new OracleCommand("select count(id_htrans_in)+1 from htrans_in where id_htrans_in LIKE '%" + id_htrans + "%'", conn);
                string indexkosongs = cmds.ExecuteScalar().ToString();
                for (int i = indexkosongs.Length; i < 2; i++)
                {
                    indexkosongs = "0" + indexkosongs;
                }
                id_htrans += indexkosongs;
                cmds.CommandText = "insert into htrans_in(id_htrans_in, id_supplier, id_gudang, tanggal_trans, total_harga) values(:id_htrans_in, :id_supplier, :id_gudang, CURRENT_TIMESTAMP, :total_harga)";
                cmds.Parameters.Add("id_htrans_in", id_htrans);
                cmds.Parameters.Add("id_supplier", combosupplier.SelectedValue);
                cmds.Parameters.Add("id_gudang", combogudang.SelectedValue);
                cmds.Parameters.Add("total_harga", (int)numericbeli.Value * (int)numericstock.Value);
                cmds.ExecuteNonQuery();
                cmds.CommandText = "insert into dtrans_in values('" + id_htrans + "','" + id + "'," + numericstock.Value + "," + numericbeli.Value + "," + total + ","+numericstock.Value+",'" + logins.username + "')";
                cmds.ExecuteNonQuery();
                OracleCommand cmd2 = new OracleCommand();
                string inserthtrans = "insert into history_perubahan(id_barang, tanggal_perubahan,jenis_perubahan, stock_awal, stock_baru,harga_beli_awal,harga_beli_baru, harga_jual_awal, harga_jual_baru,deskripsi,id_pegawai) values(:id_barang, current_timestamp ,:jenis_perubahan, :stock_awal, :stock_baru,:harga_beli_awal,:harga_beli_baru, :harga_jual_awal, :harga_jual_baru, :deskripsi,:id_pegawai)";
                cmd2.Parameters.Add("id_barang", id);
                cmd2.Parameters.Add("jenis_perubahan", "Beli".ToString());
                cmd2.Parameters.Add("stock_awal", "0".ToString());
                cmd2.Parameters.Add("stock_baru", numericstock.Value);
                cmd2.Parameters.Add("harga_beli_awal", "0".ToString());
                cmd2.Parameters.Add("harga_beli_baru", numericbeli.Value);
                cmd2.Parameters.Add("harga_jual_awal", "0".ToString());
                cmd2.Parameters.Add("harga_jual_baru", numericjual.Value);
                cmd2.Parameters.Add("deskripsi", id_htrans.ToString());
                cmd2.Parameters.Add("id_pegawai", logins.username);
                cmd2.Connection = conn;
                cmd2.CommandText = inserthtrans;
                cmd2.ExecuteNonQuery();
                // cmds.CommandText = "insert into dtrans_in (id_htrans_in, id_barang, stock_masuk, harga_beli, subtotal,total_stock, id_penanggungjawab) values ( :id_htrans_in, :id_barang, :stock_masuk, :harga_beli, :subtotal,:totalstock, :id_penanggungjawab)";
                //cmds.Parameters.Add("id_htrans_in", id_htrans);
                //cmds.Parameters.Add("id_barang", id);
                //cmds.Parameters.Add("stock_masuk", numericstock.Value);
                //cmds.Parameters.Add("harga_beli", numericbeli.Value);
                // cmds.Parameters.Add("subtotal", total);
                // cmds.Parameters.Add("totalstock", numericstock.Value);
                //cmds.Parameters.Add("id_penanggungjawab", logins.username);
                //cmds.ExecuteNonQuery();



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
        private void btnBack (object sender, EventArgs e)
        {
            parent.showPostLogin();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (checksearch() == true)
            {
                string query = "select * from barang where upper(" + keysearch.Text + ") LIKE upper('%" + valuetext.text + "%')";
                OracleCommand cmd = new OracleCommand(query, conn);
                //MessageBox.Show(query);

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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            reportbarang rb = new reportbarang();
            rb.ShowDialog();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            valuetext.text = "";
            conn.Open();
            tampilbarang();
            conn.Close();
            
        }
    }
}
