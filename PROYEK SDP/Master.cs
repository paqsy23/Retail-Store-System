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
             logins.user = baca.ReadElementString("user");
            logins.pass = baca.ReadElementString("pass");
            path = "user id=" + logins.user + ";password=" + logins.pass + ";";
            baca.ReadEndElement();
            baca.Close();
        }

        private void Master_Load(object sender, EventArgs e)
        {
            showLogin();
        }

        public void showLogin()
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
        public void showPostSuratJalan()
        {
            suratjalan ps = new suratjalan(path);
            ps.MdiParent = this;
            ps.parent = this;
            ps.Location = new Point(0, 0);
            this.Width = ps.Width + 20;
            this.Height = ps.Height + 44;
            ps.Show();

        }
        public void showReportLaba()
        {
            ReportLaba p1 = new ReportLaba();
            p1.MdiParent = this;
            p1.parent = this;
            p1.Location = new Point(0, 0);
            p1.Show();
        }
    }
}
class logins
{
    private static string h_username="ADMIN";
    private static string h_user="admin1";
    private static string h_pass="admin";
    private static string h_jabatan = "admin";
    public static string username
    {
        get { return h_username; }
        set { h_username = value; }
    }
    public static string jabatan
    {
        get { return h_jabatan; }
        set { h_jabatan = value; }
    }
    public static string user
    {
        get { return h_user; }
        set { h_user= value; }
    }
    public static string pass
    {
        get { return h_pass; }
        set { h_pass = value; }
    }
}