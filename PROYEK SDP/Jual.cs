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
        int temppembeli = 0;
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
            isi_supir();
            isi_mobil();
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
        public void isi_supir()
        {
            comboBox2.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from pegawai where jabatan='Supir'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                comboBox2.Items.Add(item[1].ToString());
            }
        }
        public void isi_mobil()
        {
            comboBox3.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from mobil", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                comboBox3.Items.Add(item[1].ToString());
            }
        }
        private void Jual_Load(object sender, EventArgs e)
        {
            refresh();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            OracleCommand cmd = new OracleCommand("select * from barang where id_barang = '"+comboBox1.SelectedItem.ToString()+"'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int stok = 0;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                stok = Int16.Parse(item[6].ToString());
            }
            stok = stok - (int)numericUpDown1.Value;
            if (stok<0)
            {
                MessageBox.Show("Stok Tidak Mencukupi");
            }
            else
            {
                conn.Open();
                OracleCommand command = new OracleCommand();
                command.Connection = conn;
                String update = "update barang set stock=" + stok + "where id_barang = '" + comboBox1.Text + "'";
                command.CommandText = update;
                command.ExecuteNonQuery();
                String nama = "select nama_barang from barang where id_barang='"+comboBox1.Text+"'";
                command.CommandText = nama;
                String tempnama = command.ExecuteScalar().ToString();
                String harga = "select harga_jual from barang where id_barang='"+comboBox1.Text+"'";
                command.CommandText = harga;
                String tempharga = command.ExecuteScalar().ToString();
                int total = Convert.ToInt32(numericUpDown1.Value.ToString()) * Convert.ToInt32(tempharga);
                tempcheckout.Rows.Add(comboBox1.Text, tempnama, tempharga, numericUpDown1.Value.ToString(),total.ToString());
                refresh();
                conn.Close();
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
            conn.Open();
            if(tempcheckout.Rows.Count > 0)
            {
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
                String inserthtrans = "insert into htrans_out(id_htrans_out, id_buyer, tanggal_trans, total_harga) values(:id_htrans_out,:id_buyer, CURRENT_TIMESTAMP,:total_harga)";
                cmd2.Parameters.Add("id_htrans_out", id_htrans);
                cmd2.Parameters.Add("id_buyer", cbpembeli.Text.ToString());
                cmd2.Parameters.Add("total_harga", total.Text);
                cmd2.Connection = conn;
                cmd2.CommandText = inserthtrans;
                cmd2.ExecuteNonQuery();
                tempcheckout.Clear();
                total.Text = "0";

                for (int i = 0; i < tempcheckout.Rows.Count; i++)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.Parameters.Add("id_htrans_out", id_htrans);
                    cmd.Parameters.Add("id_barang",bunifuCustomDataGrid1.Rows[i].Cells[0].ToString());

                }




                conn.Close();
                cbpembeli.Enabled = true;
            }
            else
            {
                MessageBox.Show("silahkan add barang ke cart terlebih dahulu");
            }
            
        }

    }

}
