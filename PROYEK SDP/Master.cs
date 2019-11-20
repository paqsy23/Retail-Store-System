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
            login f1 = new login(path);
            f1.MdiParent = this;
            f1.parent = this;
            this.Width = f1.Width + 20;
            this.Height = f1.Height + 44;
            f1.Location = new Point(0, 0);
            f1.Show();
        }

        public void showBarang()
        {
            Barang b = new Barang(path);
            b.MdiParent = this;
            b.parent = this;
            b.Location = new Point(0, 0);
            b.Show();
        }

        public void showPegawai()
        {
            Pegawai p1 = new Pegawai(path);
            p1.MdiParent = this;
            p1.parent = this;
            p1.Location = new Point(0, 0);
            p1.Show();
        }

        public void showBeli()
        {
            beli be = new beli(path);
            be.MdiParent = this;
            be.parent = this;
            be.Location = new Point(0, 0);
            this.Width = be.Width + 20;
            this.Height = be.Height + 44;
            be.Show();
        }

        public void showJual()
        {
            Jual j1 = new Jual(path);
            j1.MdiParent = this;
            j1.parent = this;
            j1.Location = new Point(0, 0);
            this.Width = j1.Width + 20;
            this.Height = j1.Height + 44;
            j1.Show();
        }

        public void showPenyesuaian()
        {
            formpenyesuaianbarang fp = new formpenyesuaianbarang(path);
            fp.MdiParent = this;
            fp.parent = this;
            fp.Location = new Point(0, 0);
            this.Width = fp.Width + 20;
            this.Height = fp.Height + 44;
            fp.Show();
        }

        public void showSupplier()
        {
            supplier s = new supplier(path);
            s.MdiParent = this;
            s.parent = this;
            s.Location = new Point(0, 0);
            this.Width = s.Width + 20;
            this.Height = s.Height + 44;
            s.Show();
        }

        public void showPostLogin()
        {
            PostLogin ps = new PostLogin();
            ps.MdiParent = this;
            ps.parent = this;
            ps.Location = new Point(0, 0);
            this.Width = ps.Width + 20;
            this.Height = ps.Height + 44;
            ps.Show();

        }

        private void pembeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pem = new pembeli(path);
            pem.MdiParent = this;
            pem.Show();
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