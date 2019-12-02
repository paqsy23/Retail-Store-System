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
    public partial class suratjalan : Form
    {
        OracleConnection conn;
        public suratjalan(String path)
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            conn = new OracleConnection(path);
            refresh();
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
        public void isi_trans()
        {
            comboBox1.Items.Clear();
            MessageBox.Show(cbpembeli.SelectedItem.ToString());
            OracleCommand cmd = new OracleCommand("select * from htrans_out where id_buyer='" + cbpembeli.SelectedItem.ToString() + "'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                comboBox1.Items.Add(item[0].ToString());
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
        public void refresh()
        {
            isi_supir();
            isi_mobil();
            isi_pembeli();
            
        }
        public void temp()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            String temptgl = dateTimePicker1.Value.Date.ToString("dd/MMM/yyyy");
            int tgl = Convert.ToInt32(dateTimePicker1.Value.Date.ToString("dd"));
            int bulan = Convert.ToInt32(dateTimePicker1.Value.Date.ToString("MM"));
            int tahun = Convert.ToInt32(dateTimePicker1.Value.Date.ToString("yyyy"));
            int tglsekarang = Convert.ToInt32(dateTime.ToString("dd"));
            int bulansekarang = Convert.ToInt32(dateTime.ToString("MM"));
            int tahunsekarang = Convert.ToInt32(dateTime.ToString("yyyy"));
            int tglkirim = tgl + (bulan * 30) + (tahun * 365);
            int tsekarang = tglsekarang + (bulansekarang * 30) + (tahunsekarang * 365);
            if (tglkirim >= tsekarang)
            {
            }
            else
            {
                MessageBox.Show("tanggal pengiriman harus diatas tanggal sekarang");
            }
}

        private void cbpembeli_SelectedIndexChanged(object sender, EventArgs e)
        {
            isi_trans();
        }
    }
}
