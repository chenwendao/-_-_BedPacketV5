using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CWD;

namespace CWD
{
    public partial class fmRegister : Form
    {
        public SqlConnection ufconn;
        public fmRegister()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbUser.Value==null || tbUser.Value == "")
            {
                MessageBox.Show("请选择一个正确的用户！", "提示");
                return;
            }
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Num FROM ekk..c_ModelUser where Model='" +
                Public.ModelName + "' and cPsn_Num='" + tbUser.Value + "'", ufconn);
            if (oj != null)
            {
                MessageBox.Show("已有此用户，不能再新增！", "提示");
                return;
            }
            if (tbPass.Text.Trim()=="" || tbPass.Text.Trim()!=tbConfirm.Text.Trim())
            {
                MessageBox.Show("密码不能为空，且密码和确认密码必须相同！", "提示");
                return;
            }
            SqlAccess.ExecuteSql(" insert into ekk..c_ModelUser (cPsn_num,Password,Model,Oper) values ('"
                + tbUser.Value + "','" + Common.GetMd5Str(tbPass.Text.Trim()) + "','" + Public.ModelName + "','CRUD')", ufconn);
            MessageBox.Show("已经成功添加新用户！", "提示");
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
        }
    }
}