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
        int a;
        public formpenyesuaianbarang(String path)
        {
            InitializeComponent(); 
            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(0, 0);
            this.Top = 0;
            this.Left = 0;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
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
                int stock = Convert.ToInt32(numstock.Value);
                String gudang = cbgudang.Text;
                int hargabeli = Convert.ToInt32(numbeli.Value);
                int hargajual = Convert.ToInt32(numjual.Value);
                String query = "update barang set stock='" + stock + "',harga_jual='" + hargajual + "',harga_beli='" + hargabeli + "',id_gudang='" + gudang + "' where id_barang='" + edid.Text + "'";
                OracleCommand cmd = new OracleCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampilbarang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
