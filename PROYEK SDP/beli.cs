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
    public partial class beli : Form
    {
        DataTable tempcheckin = new DataTable();
        OracleConnection conn;
        public Master parent;
        public beli(string path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
            this.Width = 1000;
            this.Height = 600;
            this.Location = new Point(0, 0);

            tempcheckin.Columns.Add("ID Barang");
            tempcheckin.Columns.Add("Nama Barang");
            tempcheckin.Columns.Add("Harga Beli Barang");
            tempcheckin.Columns.Add("Jumlah Barang");
            tempcheckin.Columns.Add("Subtotal");
        }
        public void isi_checkin()
        {
            bunifuCustomDataGrid2.DataSource = tempcheckin;
        }


        private void beli_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            refresh();
        }
        public void refresh()
        {
            tampilbarang();
            isisupplier();
            isi_checkin();
        }
        private void isisupplier()
        {
            OracleCommand cmd = new OracleCommand("select ID_SUPPLIER,NAMA_SUPPLIER from SUPPLIER where status_delete=0 ", conn);
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
            bunifuCustomDataGrid1.DataSource = ds.Tables[0];
        }
        private bool checkform()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == "" && textBox.Name != "idtext")
                    {
                        return false;
                    }
                }
                else if (c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    if (ComboBox.Text == "" || ComboBox.SelectedIndex == -1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (tempcheckin.Rows.Count > 0)
            {
                
                    conn.Open();
                    DateTime dateTime = DateTime.UtcNow.Date;
                    string id_htrans = "HI" + (dateTime.ToString("ddMMyyyy"));
                    OracleCommand cmds = new OracleCommand("select count(id_htrans_in)+1 from htrans_in where id_htrans_in LIKE '%" + id_htrans + "%'", conn);
                    string indexkosongs = cmds.ExecuteScalar().ToString();
                    for (int i = indexkosongs.Length; i < 5; i++)
                    {
                        indexkosongs = "0" + indexkosongs;
                    }
                    id_htrans += indexkosongs;
                    cmds.CommandText = "select id_gudang from barang where id_barang='" + bunifuCustomDataGrid1.Rows[0].Cells[0].Value.ToString() + "'";
                    OracleCommand cmd2 = new OracleCommand();
                    string inserthtrans = "insert into htrans_in(id_htrans_in, id_supplier, id_gudang, tanggal_trans,total_harga,id_nota) values(:id_htrans_in, :id_supplier, :id_gudang,  current_timestamp,:total_harga,:id_nota)";
                    cmd2.Parameters.Add("id_htrans_in", id_htrans);
                    cmd2.Parameters.Add("id_supplier", combosupplier.SelectedValue);
                    cmd2.Parameters.Add("id_gudang", cmds.ExecuteScalar().ToString());
                    cmd2.Parameters.Add("total_harga", 0.ToString());
                    cmd2.Parameters.Add("id_nota", textBox1.Text);
                    cmd2.Connection = conn;
                    cmd2.CommandText = inserthtrans;
                    cmd2.ExecuteNonQuery();
                    int acclaba = 0;
                    for (int i = tempcheckin.Rows.Count - 1; i > -1; i--)
                    {
                    OracleCommand command3 = new OracleCommand();
                    command3.Connection = conn;
                    String getstok = "select stock from barang where id_barang = '" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'";
                    command3.CommandText = getstok;
                    int stocksekarang = Convert.ToInt32(command3.ExecuteScalar().ToString());

                    String update = "update barang set stock=stock+" + bunifuCustomDataGrid2.Rows[i].Cells[3].Value.ToString() + " where id_barang = '" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'";
                    command3.CommandText = update;
                    command3.ExecuteNonQuery();
                    command3.CommandText = "select  harga_beli from barang where id_barang='" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'";
                    int hargabeli = Int32.Parse(command3.ExecuteScalar().ToString());

                    int harga = Convert.ToInt32(bunifuCustomDataGrid2.Rows[i].Cells[4].Value.ToString());
                    acclaba += harga;
                    int totalstock = stocksekarang + Int32.Parse(bunifuCustomDataGrid2.Rows[i].Cells[3].Value.ToString());
                    int hargabelibaru = ((Int32.Parse(command3.ExecuteScalar().ToString()) * stocksekarang) + Int32.Parse(bunifuCustomDataGrid2.Rows[i].Cells[3].Value.ToString()) * Int32.Parse(bunifuCustomDataGrid2.Rows[i].Cells[2].Value.ToString())) / totalstock;
                    OracleCommand cmd3 = new OracleCommand();

                    ///blm d ubah
                    string insert = "insert into dtrans_in values('" + id_htrans + "','" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'," + bunifuCustomDataGrid2.Rows[i].Cells[3].Value.ToString() + "," + bunifuCustomDataGrid2.Rows[i].Cells[2].Value.ToString() + "," + bunifuCustomDataGrid2.Rows[i].Cells[4].Value.ToString() + "," + totalstock + ",'" + logins.username + "')";
                    cmd3.Connection = conn;
                    combosupplier.Text = textBox1.Text;
                    cmd3.CommandText = insert;
                    cmd3.ExecuteNonQuery();
                    string idperubahan = "HT" + (dateTime.ToString("ddMMyyyy"));
                    OracleCommand cmd1 = new OracleCommand("select count(id_perubahan)+1 from history_perubahan where id_perubahan LIKE '%" + idperubahan + "%'", conn);
                    string indexkosongperubahan = cmd1.ExecuteScalar().ToString();

                    for (int j = indexkosongperubahan.Length; j < 5; j++)
                    {
                        indexkosongperubahan = "0" + indexkosongperubahan;
                    }
                    command3.CommandText = "select harga_jual from barang where id_barang = '" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'";
                    idperubahan += indexkosongperubahan;
                    inserthtrans = "insert into history_perubahan(id_perubahan ,id_barang, tanggal_perubahan,jenis_perubahan, stock_awal, stock_baru,harga_beli_awal,harga_beli_baru, harga_jual_awal, harga_jual_baru,deskripsi,id_pegawai) values(:id_perubahan,:id_barang, current_timestamp ,:jenis_perubahan,:stock_awal, :stock_baru,:harga_beli_awal,:harga_beli_baru, :harga_jual_awal, :harga_jual_baru, :deskripsi,:id_pegawai)";
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.Add("id_perubahan", idperubahan);
                    cmd2.Parameters.Add("id_barang", bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString());
                    cmd2.Parameters.Add("jenis_perubahan", "Jual".ToString());
                    cmd2.Parameters.Add("stock_awal", stocksekarang);
                    cmd2.Parameters.Add("stock_baru", totalstock);
                    cmd2.Parameters.Add("harga_beli_awal", hargabeli);
                    cmd2.Parameters.Add("harga_beli_baru", hargabelibaru);
                    cmd2.Parameters.Add("harga_jual_awal", command3.ExecuteScalar().ToString());
                    cmd2.Parameters.Add("harga_jual_baru", command3.ExecuteScalar().ToString());
                    cmd2.Parameters.Add("deskripsi", id_htrans.ToString());
                    cmd2.Parameters.Add("id_pegawai", logins.username);
                    cmd2.CommandText = inserthtrans;
                    cmd2.ExecuteNonQuery();
                    String updateharga = "update barang set harga_beli=" + hargabelibaru + " where id_barang = '" + bunifuCustomDataGrid2.Rows[i].Cells[0].Value.ToString() + "'";
                    tempcheckin.Rows[i].Delete();                   
                    command3.CommandText = updateharga;
                    command3.ExecuteNonQuery();

                    }
                cmds.CommandText = "update htrans_in set total_harga=" + acclaba + "where id_htrans_in='" + id_htrans + "'";
                cmds.ExecuteNonQuery();
                conn.Close();
                refresh();
                conn.Close();



            }
            else
            {
                MessageBox.Show("Tambah Barang pada list terlebih dahulu");
            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string nama = "";
        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = bunifuCustomDataGrid1[0, e.RowIndex].Value.ToString();
                nama = bunifuCustomDataGrid1[1, e.RowIndex].Value.ToString();
            }catch(Exception ex)
            {

            }
            
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
        private bool checksearch()
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is ComboBox)
                {
                    ComboBox ComboBox = c as ComboBox;
                    if (ComboBox.Text == "" || ComboBox.Text == string.Empty || ComboBox.SelectedIndex == -1)
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

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (checksearch() == true)
            {
                string query = "select * from barang where upper(" + keysearch.Text + ") LIKE upper('%'|| :isi || '%')";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.Parameters.Add("isi", valuetext.text);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                bunifuCustomDataGrid1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Pastikan Form Terisi Dengan Benar");
            }
            conn.Close();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            conn.Open();
            tampilbarang();
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                int total = Convert.ToInt32(numericUpDown1.Value.ToString()) * Convert.ToInt32(numericUpDown2.Value.ToString());
                int index = -1;
                for (int i = 0; i < tempcheckin.Rows.Count; i++)
                {
                    if (tempcheckin.Rows[i].ItemArray[0].ToString() == textBox2.Text)
                    {
                        index = i;
                    }
                    MessageBox.Show(textBox2.Text);
                }

                if (index > -1)
                {
                    int tempstock = Convert.ToInt32(tempcheckin.Rows[index].ItemArray[3].ToString()) + (int)numericUpDown1.Value;
                    tempcheckin.Rows[index][3] = tempstock.ToString();

                    int tempharga = (Convert.ToInt32(tempcheckin.Rows[index].ItemArray[4].ToString()) + (int)numericUpDown1.Value * (int)numericUpDown2.Value) / tempstock;
                    tempcheckin.Rows[index][2] = tempharga.ToString();
                    int subtotal = tempharga * tempstock;
                    tempcheckin.Rows[index][4] = subtotal.ToString();
                    index = -1;
                }
                else
                {
                    tempcheckin.Rows.Add(textBox2.Text, nama, numericUpDown2.Value.ToString(), numericUpDown1.Value.ToString(), total.ToString());
                }
                isi_checkin();

            }
            else
            {
                MessageBox.Show("Pilih Barang Yang Di Beli");
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
