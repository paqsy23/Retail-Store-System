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
        beli be;
        Barang b;
        login f1;
        Pegawai p1;
        Jual j1;
        supplier s;
        formpenyesuaianbarang fp;
        string path = "";
        public Master()
        {
            InitializeComponent();

            //Load Login.xml
            XmlTextReader baca = new XmlTextReader(Application.StartupPath + "\\login.xml");
            baca.ReadStartElement("data");
            string user = baca.ReadElementString("user");
            string pass = baca.ReadElementString("pass");
            path = "user id=" + user + ";password=" + pass + ";";
            baca.ReadEndElement();
            baca.Close();
            b = new Barang(path);
            f1 = new login(path);
            p1 = new Pegawai(path);
            j1 = new Jual(path);
            be = new beli(path);
            fp = new formpenyesuaianbarang(path);
            s = new supplier(path);
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
            j1.Close();
            s.Close();
            masterToolStripMenuItem.Enabled = false;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = true;
            masterBeliToolStripMenuItem.Enabled = true;

        }

        private void Master_Load(object sender, EventArgs e)
        {
            f1 = new login(path);
            f1.MdiParent = this;
            b.Close();
            f1.Show();
            p1.Close();
            j1.Close();
            fp.Close();
            s.Close();
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1 = new Pegawai(path);
            p1.MdiParent = this;
            this.Width = p1.Width + 20;
            this.Height = p1.Height + 67;
            b.Close();
            f1.Close();
            j1.Close();
            p1.Show();
            fp.Close();
            s.Close();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = false;
            masterBeliToolStripMenuItem.Enabled = true;
        }

        private void masterBeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            be = new beli(path);
            be.MdiParent = this;
            this.Width = p1.Width + 20;
            this.Height = p1.Height + 67;
            b.Close();
            f1.Close();
            j1.Close();
            be.Show();
            fp.Close();
            s.Close();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = true;
            masterBeliToolStripMenuItem.Enabled = false;
        }

        private void masterJualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            j1 = new Jual(path);
            j1.MdiParent = this;
            this.Width = p1.Width + 20;
            this.Height = p1.Height + 67;
            b.Close();
            f1.Close();
            be.Close();
            j1.Show();
            fp.Close();
            s.Close();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = false;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = true;
            masterBeliToolStripMenuItem.Enabled = true;
        }

        private void masterPenyesuaianBarangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fp = new formpenyesuaianbarang(path);
            fp.MdiParent = this;
            b.Close();
            f1.Close();
            be.Close();
            j1.Close();
            fp.Show();
            s.Close();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = false;
            pegawaiToolStripMenuItem.Enabled = true;
            masterBeliToolStripMenuItem.Enabled = true;

        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s = new supplier(path);
            s.MdiParent = this;
            s.Show();
            b.Close();
            f1.Close();
            be.Close();
            j1.Close();
            fp.Close();
            masterToolStripMenuItem.Enabled = true;
            masterJualToolStripMenuItem.Enabled = true;
            masterPenyesuaianBarangToolStripMenuItem.Enabled = true;
            pegawaiToolStripMenuItem.Enabled = true;
            masterBeliToolStripMenuItem.Enabled = true;
            supplierToolStripMenuItem.Enabled = false;
        }
    }
}
class logins
{
    private static string h_username="ADMIN";
    public static string username
    {
        get { return h_username; }
        set { h_username = value; }
    }
}