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
            tampilbarang();
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

        }
    }
}
