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
    public partial class fmPassword : Form
    {
        public string UserID;
        public SqlConnection ufconn;
        public fmPassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(ufconn==null)
                return;
            if (UserID == null || UserID.Trim()=="")
                return;
            object oj = SqlAccess.ExecuteScalar("select password from ekk..c_ModelUser where Model='" +
                Public.ModelName + "' and cpsn_num='" + UserID + "'", ufconn);
            string oldpass=oj==null?"":oj.ToString();
            if (oldpass != Common.GetMd5Str(tbOld.Text.Trim()))
            {
                MessageBox.Show("原密码不对！", "提示");
                return;
            }
            if(tbNew.Text.Trim()=="" || tbNew.Text.Trim()!=tbConfirm.Text.Trim())
            {
                MessageBox.Show("新密码和确认密码不符，不能保存！","提示");
                return;
            }
            SqlAccess.ExecuteSql(" update ekk..c_ModelUser set Password='" + Common.GetMd5Str(tbNew.Text.Trim()) + "' where Model='" +
                Public.ModelName + "' and  cPsn_num='" + UserID + "'", ufconn);
            MessageBox.Show("密码修改成功！","提示");
        }

        private void fmPassword_Load(object sender, EventArgs e)
        {

        }
    }
}