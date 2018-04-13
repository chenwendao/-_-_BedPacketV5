using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CWD.DAL;

namespace CWD
{
    public partial class fmPrint : Form
    {       
        public SqlConnection Conn;
        public fmPrint()
        {
            InitializeComponent();
        }

       


        private void fmSoPrn_Resize(object sender, EventArgs e)
        {
            crv1.Height = this.ClientRectangle.Height - plTop.Height;
            crv1.Top = plTop.Height;
        }

        

       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}