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
        
        public Jual(string path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
        }

        public void refresh()
        {
            isi_id();
            tampilBarang();
            isi_supir();
            isi_mobil();
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
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox1.DataSource = ds.AsDataView();
            comboBox1.DisplayMember = "ID_BARANG";
            comboBox1.ValueMember = "ID_BARANG";
        }
        public void isi_supir()
        {
            OracleCommand cmd = new OracleCommand("select * from pegawai where jabatan='Supir'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox2.DataSource = ds.AsDataView();
            comboBox2.DisplayMember = "NAMA_PEGAWAI";
            comboBox2.ValueMember = "ID_PEGAWAI";
        }
        public void isi_mobil()
        {
            OracleCommand cmd = new OracleCommand("select * from mobil", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            comboBox3.DataSource = ds.AsDataView();
            comboBox3.DisplayMember = "NAMA_MOBIL";
            comboBox3.ValueMember = "ID_MOBIL";
        }
        private void Jual_Load(object sender, EventArgs e)
        {
            refresh();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            conn.Open();
            OracleCommand cmd2 = new OracleCommand("select stock from barang where id_barang = '" + comboBox1.Text + "'", conn);
            int stok = Convert.ToInt32(cmd2.ExecuteScalar().ToString());
            stok = stok - (int)numericUpDown1.Value;
            if (stok<0)
            {
                MessageBox.Show("Stok Tidak Mencukupi");
            }
            else
            {
                OracleCommand command = new OracleCommand();
                command.Connection = conn;
                String update = "update barang set stock=" + stok + "where id_barang = '" + comboBox1.Text + "'";
                command.CommandText = update;
                command.ExecuteNonQuery();
                refresh();
                conn.Close();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select * from barang where id_barang='"+comboBox1.Text+"'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }

}
