using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    public partial class fmInput : Form
    {
        public string sResult;
        public fmInput()
        {
            InitializeComponent();
        }

        private void tbNum_Click(object sender, EventArgs e)
        {
            InputNum num = new InputNum();
            if (num.ShowDialog() != DialogResult.OK)
                return;
            tbNum.Text = num.cResult;
            sResult = tbNum.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}