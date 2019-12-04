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
    public partial class Jual : Form
    {
        OracleConnection conn;
        public Master parent;
        String path;
        DataTable tempcheckout = new DataTable();
        public Jual(string path)
        {
            InitializeComponent();
            this.path = path;
            this.Location = new Point(0, 0);
            conn = new OracleConnection(path);
            tempcheckout.Columns.Add("id barang");
            tempcheckout.Columns.Add("nama barang");
            tempcheckout.Columns.Add("harga barang");
            tempcheckout.Columns.Add("jumlah barang");
            tempcheckout.Columns.Add("subtotal");
        }
        public void isi_checkout()
        {
            bunifuCustomDataGrid1.DataSource = tempcheckout;
        }

        public void refresh()
        {
            
            isi_checkout();
            isi_id();
            tampilBarang();
            isi_pembeli();
            int totalharga = 0;
            for (int i = 0; i < tempcheckout.Rows.Count; i++)
            {
                totalharga += Convert.ToInt32(tempcheckout.Rows[i].ItemArray[4].ToString());
            }
            total.Text = totalharga.ToString();
        }
        public void tampilBarang()
        {
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        
        public void isi_id()
        {
            comboBox1.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                comboBox1.Items.Add(item[0].ToString());
            }
        }
        public void isi_pembeli()
        {
            cbpembeli.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from buyer", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                cbpembeli.Items.Add(item[0].ToString());
            }
        }
        
        private void Jual_Load(object sender, EventArgs e)
        {
            refresh();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("select * from barang where id_barang = '" + comboBox1.Text.ToString() + "'", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int stok = 0;
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    stok = Int16.Parse(item[6].ToString());
                }
                stok = stok - (int)numericUpDown1.Value;
                if (stok < 0)
                {
                    MessageBox.Show("Stok Tidak Mencukupi");
                }
                else if (numericUpDown1.Value > 0)
                {
                    conn.Open();
                    OracleCommand command = new OracleCommand();
                    command.Connection = conn;
                    String nama = "select nama_barang from barang where id_barang='" + comboBox1.Text + "'";
                    command.CommandText = nama;
                    String tempnama = command.ExecuteScalar().ToString();
                    String harga = "select harga_jual from barang where id_barang='" + comboBox1.Text + "'";
                    command.CommandText = harga;
                    String tempharga = command.ExecuteScalar().ToString();
                    int total = Convert.ToInt32(numericUpDown1.Value.ToString()) * Convert.ToInt32(tempharga);
                    int index = -1;
                    for (int i = 0; i < tempcheckout.Rows.Count; i++)
                    {
                        if (tempcheckout.Rows[i].ItemArray[0].ToString() == comboBox1.Text)
                        {
                            index = i;
                        }
                    }
                    if(index > -1)
                    {
                        int tempstock = Convert.ToInt32(tempcheckout.Rows[index].ItemArray[3].ToString()) +(int) numericUpDown1.Value;
                        tempcheckout.Rows[index][3] = tempstock.ToString();
                        index = -1;
                    }
                    else
                    {
                        tempcheckout.Rows.Add(comboBox1.Text, tempnama, tempharga, numericUpDown1.Value.ToString(), total.ToString());
                    }
                    refresh();
                    conn.Close();
                }
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("pilih id barang terlebih dahulu");
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

        private void button1_Click(object sender, EventArgs e)
        {
            pembeli p = new pembeli(path);
            p.ShowDialog();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (tempcheckout.Rows.Count > 0)
            {
                try
                {
                    conn.Open();
                    DateTime dateTime = DateTime.UtcNow.Date;
                    string id_htrans = "HO" + (dateTime.ToString("ddMMyyyy"));
                    OracleCommand cmds = new OracleCommand("select count(id_htrans_out)+1 from htrans_out where id_htrans_out LIKE '%" + id_htrans + "%'", conn);
                    string indexkosongs = cmds.ExecuteScalar().ToString();
                    for (int i = indexkosongs.Length; i < 2; i++)
                    {
                        indexkosongs = "0" + indexkosongs;
                    }
                    id_htrans += indexkosongs;
                        OracleCommand cmd2 = new OracleCommand();
                        string inserthtrans = "insert into htrans_out(id_htrans_out, id_buyer, tanggal_trans, total_harga) values(:id_htrans_out,:id_buyer, current_timestamp,:total_harga)";
                        cmd2.Parameters.Add("id_htrans_out", id_htrans);
                        cmd2.Parameters.Add("id_buyer", cbpembeli.Text.ToString());
                        cmd2.Parameters.Add("total_harga", total.Text);
                        cmd2.Connection = conn;
                        cmd2.CommandText = inserthtrans;
                        cmd2.ExecuteNonQuery();
                        int acclaba = 0;
                        for (int i = tempcheckout.Rows.Count-1; i > -1; i--)
                        {
                            OracleCommand command3 = new OracleCommand();
                            command3.Connection = conn;
                            String getstok = "select stock from barang where id_barang = '" + bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString() + "'";
                            command3.CommandText = getstok;
                            int stocksekarang = Convert.ToInt32(command3.ExecuteScalar().ToString());
                            int tempsisa = stocksekarang - Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[3].Value.ToString());
                            if (tempsisa > -1)
                            {
                                String update = "update barang set stock=" + tempsisa + "where id_barang = '" + bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString() + "'";
                                command3.CommandText = update;
                                command3.ExecuteNonQuery();
                                command3.CommandText = "select  harga_beli from barang where id_barang='" + bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString() + "'";
                                int hargabeli = Int32.Parse( command3.ExecuteScalar().ToString());
                                int laba = Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[4].Value.ToString()) - (hargabeli * Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[3].Value.ToString()));
                                acclaba += laba;
                                OracleCommand cmd3 = new OracleCommand();
                                string insert = "insert into dtrans_out(id_htrans_out, id_barang, stock_keluar, harga_jual,subtotal,laba,id_penanggungjawab) values(:id_htrans_out,:id_barang, :stock_keluar ,:harga_jual,:laba,:subtotal,:id_kasir)";
                                cmd3.Connection = conn;
                                cmd3.Parameters.Add("id_htrans_out", id_htrans);
                                cmd3.Parameters.Add("id_barang", bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString());
                                cmd3.Parameters.Add("jumlah", Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[3].Value.ToString()));
                                cmd3.Parameters.Add("harga_jual", Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString()));
                                cmd3.Parameters.Add("subtotal", Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[4].Value.ToString()));
                                cmd3.Parameters.Add("laba",laba);
                                cmd3.Parameters.Add("id_kasir", logins.username);
                                cmd3.CommandText = insert;
                                cmd3.ExecuteNonQuery();
                            inserthtrans = "insert into history_perubahan(id_barang, tanggal_perubahan,jenis_perubahan, stock_awal, stock_baru,harga_beli_awal,harga_beli_baru, harga_jual_awal, harga_jual_baru,deskripsi) values(:id_barang, current_timestamp ,:jenis_perubahan,:stock_awal, :stock_baru,:harga_beli_awal,:harga_beli_baru, :harga_jual_awal, :harga_jual_baru, :deskripsi)";
                            cmd2.Parameters.Clear();
                            cmd2.Parameters.Add("id_barang", bunifuCustomDataGrid1.Rows[i].Cells[0].Value.ToString());
                            cmd2.Parameters.Add("jenis_perubahan", "Jual".ToString());
                            cmd2.Parameters.Add("stock_awal", stocksekarang);
                            cmd2.Parameters.Add("stock_baru", tempsisa);
                            cmd2.Parameters.Add("harga_beli_awal", hargabeli);
                            cmd2.Parameters.Add("harga_beli_baru", hargabeli);
                            cmd2.Parameters.Add("harga_jual_awal", Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString()));
                            cmd2.Parameters.Add("harga_jual_baru", Convert.ToInt32(bunifuCustomDataGrid1.Rows[i].Cells[2].Value.ToString()));
                            cmd2.Parameters.Add("deskripsi", id_htrans.ToString());
                            cmd2.CommandText = inserthtrans;
                            cmd2.ExecuteNonQuery();
                            tempcheckout.Rows[i].Delete();
                        }
                        else
                        {
                            MessageBox.Show("Stock Tidak Cukup");
                        }

                        }
                        cmds.CommandText = "update htrans_out set total_laba="+acclaba+"where id_htrans_out='"+id_htrans+"'";
                        cmds.ExecuteNonQuery();
                        conn.Close();
                        total.Text = "0";
                        refresh();
                    
                    conn.Close();
                   // reportNota nota = new reportNota();
                   // nota.ShowDialog();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("pastikan semua field terisi");
                }
            }
            else
            {
                MessageBox.Show("silahkan add barang ke cart terlebih dahulu");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }
    }

}
