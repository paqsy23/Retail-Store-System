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
    public partial class pembeli : Form
    {
        OracleConnection conn;
        public pembeli(String path)
        {
            InitializeComponent();
            conn = new OracleConnection(path);
            comboBox1.Items.Add("pribadi");
            comboBox1.Items.Add("perusahaan");
            comboBox1.SelectedIndex = 0;
        }

        private void btntambah_Click(object sender, EventArgs e)
        {

        }

        private void btnupdate_Click(object sender, EventArgs e)
        {

        }
    }
}
