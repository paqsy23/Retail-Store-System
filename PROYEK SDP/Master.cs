using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PROYEK_SDP
{
    public partial class Master : Form
    {
        Barang b;
        login f1;
        Pegawai p1;
        string path = "";
        public Master()
        {
            InitializeComponent();

            //Load Login.xml
            XmlTextReader baca = new XmlTextReader(Application.StartupPath + "\\login.xml");
            baca.ReadStartElement("data");
            string user = baca.ReadElementString("user");
            string pass = baca.ReadElementString("pass");
            path = "user id="+user+";password="+pass+";";
            baca.ReadEndElement();
            baca.Close();
            b = new Barang(path);
            f1 = new login(path);
            p1 = new Pegawai(path);
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            b = new Barang(path);
            b.MdiParent = this;
            this.Width = b.Width + 20;
            this.Height = b.Height + 67;
            b.Show();
            f1.Close();
            p1.Close();
            masterToolStripMenuItem.Enabled = false;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = true;

        }

        private void Master_Load(object sender, EventArgs e)
        {
            f1 = new login(path);
            f1.MdiParent = this;
            b.Close();
            f1.Show();
            p1.Close();
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1 = new Pegawai(path);
            p1.MdiParent = this;
            this.Width = p1.Width + 20;
            this.Height = p1.Height + 67;
            b.Close();
            f1.Close();
            p1.Show();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = false;
        }
    }
}
