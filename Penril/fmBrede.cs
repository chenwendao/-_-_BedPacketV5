using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CWD
{
    public partial class fmBrede : Form
    {
        public SqlConnection conn;
        public bool SRFlag = true;//Send=true;Receive=false;
        public fmBrede()
        {
            InitializeComponent();
        }

        private void fmBrede_Load(object sender, EventArgs e)
        {
            ucBrede1.SetSRFlag(SRFlag);
            ucBrede1.Init();
        }

        private void fmBrede_Resize(object sender, EventArgs e)
        {
            ucBrede1.ChangeSize();
        }

       
    }
}