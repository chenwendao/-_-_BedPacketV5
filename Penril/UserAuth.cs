using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CWD;

namespace CWD
{
    public partial class UserAuth : Form
    {
        private SqlConnection ufconn;
      
        public UserAuth()
        {
            InitializeComponent();
        }

        private void UserAuth_Load(object sender, EventArgs e)
        {
            try
            {
                ufconn = SqlAccess.SqlConn(Public.ufconnstr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，可能的原因是：" + ex.Message.ToString(), "提示");
                return;
            }

            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            object oj = SqlAccess.ExecuteScalar("SELECT password FROM ekk..C_ModelUser where Model='" + Public.ModelName + "' and  cPsn_Num='" + tbUser.Value + "'", ufconn);
            if (oj != null && oj.ToString() == Common.GetMd5Str(Public.passcode))
            {
                Public.userName = tbUser.Text;
                Public.userCode = tbUser.Value;
                this.DialogResult = DialogResult.OK;
                oj = SqlAccess.ExecuteScalar("SELECT count(*) FROM ekk..C_ModelUser where Model='" + Public.ModelName + "' and  cPsn_Num='" + tbUser.Value + "' and Oper='Audit'", ufconn);
                if (Convert.ToInt16(oj) > 0)
                    Public.bAudit = true;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            tbUser.Text = "";
            InputNum input = new InputNum();
            input.Left = 1000;
            //input.StartPosition =new Point(100,200);
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                tbUser.Text = oj.ToString();
                tbUser.Value = input.cResult;
            }
        }
        

        private void tbPassword_Click(object sender, EventArgs e)
        {
            tbPassword.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT password FROM ekk..C_ModelUser where Model='"+Public.ModelName+"' and  cPsn_Num='" + tbUser.Value + "'", ufconn);
            if (oj != null && oj.ToString() == Common.GetMd5Str(input.cResult))
            {
                Public.userName = tbUser.Text;
                Public.userCode = tbUser.Value;
                Public.passcode = input.cResult;
                Public.passname = tbUser.Text;
                Public.passuser = tbUser.Value;
                this.DialogResult = DialogResult.OK;
                oj = SqlAccess.ExecuteScalar("SELECT count(*) FROM ekk..C_ModelUser where Model='" + Public.ModelName + "' and  cPsn_Num='" + tbUser.Value + "' and Oper='Audit'", ufconn);
                if (Convert.ToInt16(oj) > 0)
                    Public.bAudit = true;
                this.DialogResult = DialogResult.OK;                
            }
        }

        private void tbUser_Click(object sender, EventArgs e)
        {
            tbUser.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                tbUser.Text = oj.ToString();
                tbUser.Value = input.cResult;
            }
            btnModi.Visible = true;
            if (tbUser.Value == "00019")
                btnRegister.Visible = true;
        }

        private void btnModi_Click(object sender, EventArgs e)
        {
            fmPassword Newpass = new fmPassword();
            Newpass.UserID = tbUser.Value;
            Newpass.ufconn = ufconn;
            Newpass.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            fmRegister register = new fmRegister();
            register.ufconn = ufconn;
            register.Show();
        }

        
    }
}