using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CWD
{
    public partial class InputNum : Form
    {
        public string cResult;
        public InputNum()
        {
            InitializeComponent();
        }

        private void btNum_Click(object sender, EventArgs e)
        {       
            Button btn=(Button)sender;
            switch (btn.Text)
            {
                case "1":
                    tbDisp.Text += "1";
                    break;
                case "2":
                    tbDisp.Text += "2";
                    break;
                case "3":
                    tbDisp.Text += "3";
                    break;
                case "4":
                    tbDisp.Text += "4";
                    break;
                case "5":
                    tbDisp.Text += "5";
                    break;
                case "6":
                    tbDisp.Text += "6";
                    break;
                case "7":
                    tbDisp.Text += "7";
                    break;
                case "8":
                    tbDisp.Text += "8";
                    break;
                case "9":
                    tbDisp.Text += "9";
                    break;
                case "0":
                    tbDisp.Text += "0";
                    break;
                case ".":
                    if(tbDisp.Text.IndexOf(".") == -1)
                        tbDisp.Text += ".";
                    break;
                default:
                    break;
            }
            
        }

        private void btClear_Click(object sender, EventArgs e)
        {
           
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if(tbDisp.Text.Length>0)
                tbDisp.Text = tbDisp.Text.Substring(0, tbDisp.Text.Length - 1);
            
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            cResult = tbDisp.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void InputNum_Load(object sender, EventArgs e)
        {
           
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }

}