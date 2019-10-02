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
        public Master()
        {
            InitializeComponent();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Barang b = new Barang();
            b.MdiParent = this;
            b.Size = new Size(this.Width,this.Height);
            b.Visible = true;
        }
    }
}
