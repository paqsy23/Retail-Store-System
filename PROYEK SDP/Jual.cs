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
            bunifuDropdown5.AddItem("budi");
            bunifuDropdown5.AddItem("andi");
            bunifuDropdown5.AddItem("kevin");
            bunifuDropdown5.AddItem("lala");
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
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                bunifuDropdown1.AddItem(item[0].ToString());
            }
        }
        public void isi_supir()
        {
            OracleCommand cmd = new OracleCommand("select * from pegawai where jabatan='Supir'", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                bunifuDropdown5.AddItem(item[1].ToString());
            }
        }
        public void isi_mobil()
        {
            OracleCommand cmd = new OracleCommand("select * from mobil", conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                bunifuDropdown6.AddItem(item[2].ToString());
            }
        }
        private void Jual_Load(object sender, EventArgs e)
        {
            refresh();
        }


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("select * from barang where id_barang = '"+bunifuDropdown1.selectedValue.ToString()+"'", conn);
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
                String update = "update barang set stock=" + stok + "where id_barang = '" + bunifuDropdown1.selectedValue.ToString() + "'";
                command.CommandText = update;
                command.ExecuteNonQuery();
                refresh();
                conn.Close();
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }

}
