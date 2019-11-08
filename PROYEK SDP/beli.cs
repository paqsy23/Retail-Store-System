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
        OracleConnection conn;
        public beli(string path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
        }

        private void beli_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            tampilbarang();

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
            if (checkform() == true)
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand("select stock from barang where id_barang='" + textBox2.Text + "'", conn);
                int stock_awal = Int32.Parse(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "select harga_beli from barang where id_barang='" + textBox2.Text + "'";
                int harga_awal = Int32.Parse(cmd.ExecuteScalar().ToString());
                int harga_barang_baru = (int)numericUpDown2.Value;
                int stock_tambahan = (int)numericUpDown1.Value;
                int stock_total = stock_awal + stock_tambahan;
                int hargabaru = (stock_awal * harga_awal + harga_barang_baru * stock_tambahan) / stock_total;
                cmd.CommandText = "update barang set stock=" + stock_total + ", harga_beli=" + hargabaru + " where id_barang='" + textBox2.Text + "'";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into temp_hpp(id_barang, id_nota, stock_masuk, stock_awal, stock_total, harga_beli_baru, harga_beli_awal , harga_baru) values(:id_barang, :id_nota, :stock_masuk, :stock_awal, :stock_total, :harga_beli_baru, :harga_beli_awal , :harga_baru)";
                cmd.Parameters.Add("id_barang", textBox2.Text);
                cmd.Parameters.Add("id_nota", textBox1.Text);
                cmd.Parameters.Add("stock_masuk", stock_tambahan);
                cmd.Parameters.Add("stock_awal", stock_awal);
                cmd.Parameters.Add("stock_total", stock_total);
                cmd.Parameters.Add("harga_beli_baru", harga_barang_baru);
                cmd.Parameters.Add("harga_beli_awal", harga_awal);
                cmd.Parameters.Add("harga_baru", hargabaru);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampilbarang();
            }
            else
            {
                MessageBox.Show("Pastikan Semua Form Terisi Dengan Benar");
            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox2.Text= bunifuCustomDataGrid1[0,e.RowIndex].Value.ToString();
        }
    }
}
