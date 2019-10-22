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
    public partial class Master : Form
    {
        Barang b = new Barang();
        login f1 = new login();
        Pegawai p1 = new Pegawai();
        public Master()
        {
            InitializeComponent();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b = new Barang();
            b.MdiParent = this;
            this.Width = b.Width + 20;
            this.Height = b.Height + 67;
            b.Show();
            f1.Close();
            p1.Close();
        }

        private void Master_Load(object sender, EventArgs e)
        {
            f1 = new login();
            f1.MdiParent = this;
            b.Close();
            f1.Show();
            p1.Close();
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1 = new Pegawai();
            p1.MdiParent = this;
            this.Width = p1.Width;
            this.Height = p1.Height + 24;
            b.Close();
            f1.Close();
            p1.Show();
        }
    }
}
