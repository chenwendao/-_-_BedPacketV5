using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    public partial class CWDUD : UserControl
    {
        public int iMaxval;
        public int iMinval;
        private int _ivalue;
        public int Value
        {
            get { return _ivalue; }
            set
            {
                _ivalue = value;
                tbMain.Text = _ivalue.ToString();
                Invalidate();
            }
        }
        public CWDUD()
        {
            InitializeComponent();
            _ivalue = 0;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(tbMain.Text) < iMaxval)
                tbMain.Text = Convert.ToString(Convert.ToInt32(tbMain.Text) + 1);
            _ivalue = Convert.ToInt32(tbMain.Text);
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(tbMain.Text) > iMinval)
                tbMain.Text = Convert.ToString(Convert.ToInt32(tbMain.Text) - 1);
            _ivalue = Convert.ToInt32(tbMain.Text);
        }

        private void tbMain_Click(object sender, EventArgs e)
        {
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            tbMain.Text = input.cResult;  
            _ivalue = Convert.ToInt32(tbMain.Text);
        }
    }
}
