﻿using System;
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
    
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent(); Master m = new Master();
            m.Visible = true;
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
