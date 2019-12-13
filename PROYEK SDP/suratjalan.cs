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
        DataTable tempcheckout = new DataTable();
        public suratjalan(String path)
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            conn = new OracleConnection(path);
            refresh();
            tempcheckout.Columns.Add("id transaksi");
            tempcheckout.Columns.Add("id barang");
            tempcheckout.Columns.Add("nama barang");
        }
        public void isi_checkout()
        {
            GridCart.DataSource = tempcheckout;
        }
        public void tampilBarang()
        {
            OracleCommand cmd = new OracleCommand("select d.id_htrans_out,d.id_barang,b.nama_barang from dtrans_out d,barang b where id_htrans_out='"+cbid.SelectedItem.ToString()+"' and id_hpengiriman is null and b.id_barang = d.id_barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGrid1.DataSource = ds.Tables[0];
        }
        public void isi_supir()
        {
            cbsupir.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from pegawai where jabatan='Supir'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cbsupir.DataSource = ds.AsDataView();
            cbsupir.DisplayMember = "NAMA_PEGAWAI";
            cbsupir.ValueMember = "ID_PEGAWAI";
            
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
            cbid.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from htrans_out where id_buyer='" + cbpembeli.SelectedItem.ToString() + "'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                cbid.Items.Add(item[0].ToString());
            }
        }
        public void isi_mobil()
        {
            cbkendaraan.Items.Clear();
            OracleCommand cmd = new OracleCommand("select * from mobil", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cbkendaraan.DataSource = ds.AsDataView();
            cbkendaraan.DisplayMember = "NAMA_MOBIL";
            cbkendaraan.ValueMember = "ID_MOBIL";
        }
        public void isi_barang()
        {
            cbbarang.Items.Clear();
            OracleCommand cmd = new OracleCommand("select id_barang from dtrans_out where id_htrans_out ='"+cbid.SelectedItem.ToString()+"' and id_hpengiriman is null", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                cbbarang.Items.Add(item[0].ToString());
            }
        }

        public void refresh()
        {
            isi_supir();
            isi_mobil();
            isi_pembeli();
            
        }

        private void cbpembeli_SelectedIndexChanged(object sender, EventArgs e)
        {
            isi_trans();
            cbbarang.Items.Clear();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isi_barang();
            tampilBarang();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                bool ada = false;
                for (int i = 0; i < tempcheckout.Rows.Count; i++)
                {
                    if(tempcheckout.Rows[i].ItemArray[1].ToString() == cbbarang.Text && tempcheckout.Rows[i].ItemArray[0].ToString() == cbid.SelectedItem.ToString())
                    {
                        ada = true;
                    }
                }
                if(ada == true)
                {
                    MessageBox.Show("barang sudah tersimpan di cart");
                }
                else
                {
                    int terpilih = -1;
                    for (int i = 0; i < dataGrid1.Rows.Count; i++)
                    {
                        if (dataGrid1.Rows[i].Cells[1].Value.ToString() == cbbarang.Text)
                        {
                            terpilih = i;
                        }
                    }
                    tempcheckout.Rows.Add(dataGrid1.Rows[terpilih].Cells[0].Value.ToString(), dataGrid1.Rows[terpilih].Cells[1].Value.ToString(), dataGrid1.Rows[terpilih].Cells[2].Value.ToString());
                    isi_checkout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbpembeli.Enabled = false;
        }

        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            cbbarang.Text = dataGrid1.Rows[index].Cells[1].Value.ToString();
            
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
                conn.Open();
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
                if (tglkirim >= tsekarang && cbsupir.SelectedItem.ToString() != "" && cbkendaraan.SelectedItem.ToString() != "")
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = conn;
                    String nama = "insert into pengiriman values('a','"+dateTimePicker1.Value.Date.ToString("dd/MMM/yyyy") +"','"+cbsupir.SelectedValue.ToString()+"','"+cbkendaraan.SelectedValue.ToString()+"')";
                    command.CommandText = nama;
                    command.ExecuteNonQuery();


                    for (int i = 0; i < GridCart.Rows.Count; i++)
                    {
                        OracleCommand command2 = new OracleCommand();
                        command2.Connection = conn;
                        String nama2 = "update dtrans_out set id_hpengiriman='a' where id_htrans_out='" + GridCart.Rows[i].Cells[0].Value.ToString() + "' and id_barang='" + GridCart.Rows[i].Cells[1].Value.ToString() + "'";
                        command2.CommandText = nama;
                        command2.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("tanggal pengiriman harus diatas tanggal sekarang");
                }
                conn.Close();
            //}
            //catch(Exception ex)
            //{
            //    conn.Close();
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
