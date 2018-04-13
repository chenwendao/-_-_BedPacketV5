using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CWD
{
    public partial class FmFilter : Form
    {
        public DataTable dt = null;
        //public SqlConnection conn = null;//传过来的conn
        //public OleDbConnection oleconn = null;
        //public string strSql = "";//传过来的sql
        public string strCol = "";//默认选择的列
        DataView dv = null;
        public int iCurRow = -1;//返回的列

        public FmFilter()
        {
            InitializeComponent();
        }

        private void FmFilter_Load(object sender, EventArgs e)
        {
            Init();
        }

        //初始化
        private void Init()
        {

            dv = dt.DefaultView;
            dgv1.DataSource = dv;
            tbCol.Text = strCol;
        }

        //查询
        private void Query()
        {
            if (tbCol.Text.ToString() != "")
            {
                dv.RowFilter = string.Format("{0} like '%{1}%'",
                    tbCol.Text.ToString(),
                    tbNum.Text.ToString());
            }
        }

        //点击选中列
        private void dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            tbCol.Text = dgv1.Columns[e.ColumnIndex].Name;
        }

        //退出
        private void tbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //刷新
        private void tbQuery_Click(object sender, EventArgs e)
        {
            dv.RowFilter = "";
        }

        //返回列
        private void ExitReturn()
        {
            if (dgv1.Rows.Count < 1)
                return;
            iCurRow = dgv1.CurrentRow.Index;
        }

        //确定
        private void tbOK_Click(object sender, EventArgs e)
        {
            ExitReturn();
            this.Close();
        }

        //双击
        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ExitReturn();
            this.Close();
        }

        //输入查询条件
        private void tbNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Query();
            }
        }

        //选中单元格
        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;
            tbCol.Text = dgv1.Columns[e.ColumnIndex].Name;
        }


    }
}