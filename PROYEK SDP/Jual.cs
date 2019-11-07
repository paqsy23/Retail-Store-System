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
        private void Jual_Load(object sender, EventArgs e)
        {
            refresh();
        }
    }

}
