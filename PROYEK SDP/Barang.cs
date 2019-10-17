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
        OracleConnection conn = new OracleConnection("data source=orcl; user id=n217116624;password=217116624;");
        public Barang()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Barang_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            ///ID tak perlu input(autogen)
            conn.Open();
            this.Top = 0;
            this.Left = 0;
            refresh();
        }
        private void refresh()
        {
            isigudang();
            tampilbarang();
            isijenis();
            isinama();
        }
        private void isigudang()
        {
            OracleCommand cmd = new OracleCommand("select ID_GUDANG from gudang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox7.DataSource = ds.AsDataView();
            comboBox7.DisplayMember = "ID_GUDANG";
            comboBox7.ValueMember = "ID_GUDANG";
        }
        private void isinama()
        {
            OracleCommand cmd = new OracleCommand("select nama_barang from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox2.DataSource = ds.AsDataView();
            comboBox2.DisplayMember = "nama_barang";
            comboBox2.ValueMember = "nama_barang";
        }

        private void isijenis()
        {
            OracleCommand cmd = new OracleCommand("select ID_JENIS_BARANG,NAMA_JENIS_BARANG from JENIS_BARANG", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox5.DataSource = ds.AsDataView();
            comboBox5.DisplayMember = "NAMA_JENIS_BARANG";
            comboBox5.ValueMember = "ID_JENIS_BARANG";
        }
        private void tampilbarang()
        {
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text=="NAMA_BARANG")
            {
                OracleCommand cmd = new OracleCommand("select nama_barang from barang", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                comboBox2.DataSource = ds.AsDataView();
                comboBox2.DisplayMember = "nama_barang";
                comboBox2.ValueMember = "nama_barang";
            }
            else if (comboBox1.Text == "ID_BARANG")
            {
                OracleCommand cmd = new OracleCommand("select id_barang from barang", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                comboBox2.DataSource = ds.AsDataView();
                comboBox2.DisplayMember = "id_barang";
                comboBox2.ValueMember = "id_barang";
            }
            else if (comboBox1.Text == "WARNA_BARANG")
            {
                OracleCommand cmd = new OracleCommand("select warna_barang from barang group by warna_barang", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                comboBox2.DataSource = ds.AsDataView();
                comboBox2.DisplayMember = "warna_barang";
                comboBox2.ValueMember = "warna_barang";
            }
            else if (comboBox1.Text == "UKURAN")
            {
                OracleCommand cmd = new OracleCommand("select ukuran from barang group by ukuran", conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);
                comboBox2.DataSource = ds.AsDataView();
                comboBox2.DisplayMember = "ukuran";
                comboBox2.ValueMember = "ukuran";
            }
        }

        private void Inset_Click(object sender, EventArgs e)
        {
            string id;
            string warna = textBox3.Text;
            string nama = textBox2.Text;
            if (nama.Contains(" "))
            {

                id = (nama.Substring(0, 1) + nama.Substring(nama.IndexOf(" ") + 1, 1) + warna.Substring(0, 2)).ToUpper() + comboBox6.SelectedIndex;
                OracleCommand cmd = new OracleCommand("select count(id_barang)+1 from barang where id_barang LIKE '%" + id + "%'", conn);
                string indexkosong = cmd.ExecuteScalar().ToString();
                for (int i = indexkosong.Length; i < 3; i++)
                {
                    indexkosong = "0" + indexkosong;
                }
                id = id + indexkosong;
                cmd.CommandText = "insert into barang values('"+id+"', '"+comboBox5.SelectedValue+"', '"+comboBox7.Text+"', '"+textBox2.Text+"', '"+textBox3.Text+"', '"+comboBox6.Text+"',"+textBox4.Text+","+textBox5.Text+","+textBox6.Text+")";
                cmd.ExecuteNonQuery();
                
            }
            refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select stock from barang where id_barang='"+textBox7.Text+"'", conn);
            int stock_awal = Int32.Parse(cmd.ExecuteScalar().ToString());
            cmd.CommandText = "select harga_beli from barang where id_barang='" + textBox7.Text + "'";
            int harga_awal = Int32.Parse(cmd.ExecuteScalar().ToString());
            int harga_barang_baru = Int32.Parse(textBox9.Text);
            int stock_tambahan = Int32.Parse(textBox8.Text);
            int stock_total = stock_awal + stock_tambahan;
            int hargabaru = (stock_awal * harga_awal + harga_barang_baru* stock_tambahan)/stock_total;
            cmd.CommandText = "update barang set stock="+stock_total+", harga_beli="+hargabaru+" where id_barang='"+textBox7.Text+"'";
            cmd.ExecuteNonQuery();
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select * from barang where "+comboBox1.Text+"='"+comboBox2.Text+"'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
