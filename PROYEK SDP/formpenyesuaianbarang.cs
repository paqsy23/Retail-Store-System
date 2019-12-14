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
    public partial class formpenyesuaianbarang : Form
    {
        OracleConnection conn;
        public Master parent;
        public formpenyesuaianbarang(String path)
        {
            InitializeComponent(); 
            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(0, 0);
            conn = new OracleConnection(path);
            tampilbarang();
            isicbgudang();
        }
        private void tampilbarang()
        {
            OracleCommand cmd = new OracleCommand("select * from barang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void isicbgudang()
        {
            OracleCommand cmd = new OracleCommand("select ID_GUDANG from gudang", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            cbgudang.DataSource = ds.AsDataView();
            cbgudang.DisplayMember = "ID_GUDANG";
            cbgudang.ValueMember = "ID_GUDANG";
        }
        int index;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            edid.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            numstock.Value = Convert.ToInt32(dataGridView1.Rows[index].Cells[6].Value.ToString());
            cbgudang.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            numbeli.Value = Convert.ToInt32(dataGridView1.Rows[index].Cells[7].Value.ToString());
            numjual.Value = Convert.ToInt32(dataGridView1.Rows[index].Cells[8].Value.ToString());
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int stocklama = Convert.ToInt32(dataGridView1.Rows[index].Cells[6].Value.ToString());
                int hargabelilama = Convert.ToInt32(dataGridView1.Rows[index].Cells[7].Value.ToString());
                int hargajuallama = Convert.ToInt32(dataGridView1.Rows[index].Cells[8].Value.ToString());
                String gudang = cbgudang.Text;
                int hargabeli = Convert.ToInt32(numbeli.Value);
                int hargajual = Convert.ToInt32(numjual.Value);
                if (hargabeli < hargajual && richTextBox1.Text != "")
                {
                    MessageBox.Show("Test");
                    OracleCommand cmd2 = new OracleCommand();
                    string inserthtrans = "insert into history_perubahan(id_barang, tanggal_perubahan,jenis_perubahan, stock_awal, stock_baru,harga_beli_awal,harga_beli_baru, harga_jual_awal, harga_jual_baru,deskripsi,id_pegawai) values(:id_barang, current_timestamp ,:jenis_perubahan, :stock_awal, :stock_baru,:harga_beli_awal,:harga_beli_baru, :harga_jual_awal, :harga_jual_baru, :deskripsi,:id_pegawai)";
                    cmd2.Parameters.Add("id_barang", dataGridView1.Rows[index].Cells[0].Value.ToString());
                    cmd2.Parameters.Add("jenis_perubahan", "Penyesuaian".ToString());
                    cmd2.Parameters.Add("stock_awal", stocklama);
                    cmd2.Parameters.Add("stock_baru", numstock.Value);
                    cmd2.Parameters.Add("harga_beli_awal", hargabelilama);
                    cmd2.Parameters.Add("harga_beli_baru", hargabeli);
                    cmd2.Parameters.Add("harga_jual_awal", hargajuallama);
                    cmd2.Parameters.Add("harga_jual_baru", hargajual);
                    cmd2.Parameters.Add("deskripsi", richTextBox1.Text+" ");
                    cmd2.Parameters.Add("id_pegawai", logins.username);
                    cmd2.Connection = conn;
                    cmd2.CommandText = inserthtrans;
                    cmd2.ExecuteNonQuery();

                    String query = "update barang set stock='" + numstock.Value + "',harga_jual='" + hargajual + "',harga_beli='" + hargabeli + "',id_gudang='" + gudang + "' where id_barang='" + edid.Text + "'";
                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                tampilbarang();
                //kosong semua
                index = -1;
                edid.Text = "";
                numstock.Value = 0;
                cbgudang.Text = "";
                numbeli.Value = 0;
                numjual.Value = 0;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("pilih barang terlebih dahulu");
                MessageBox.Show(ex.Message);
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

        private void formpenyesuaianbarang_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ReportPerubahanHarga report = new ReportPerubahanHarga();
            report.ShowDialog();
        }
    }
}
