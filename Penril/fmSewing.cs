using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    public partial class fmSewing : Form
    {
        public SqlConnection conn;
        public bool SRFlag = true;
        public fmSewing()
        {
            InitializeComponent();
        }

        private void fmSewing_Load(object sender, EventArgs e)
        {
            ucSewing1.SetSRFlag(SRFlag);
            ucSewing1.Init();
        }
    }
}