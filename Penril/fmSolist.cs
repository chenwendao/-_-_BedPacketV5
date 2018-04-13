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
    public partial class fmSolist : Form
    {
        public SqlConnection Conn;
        public int SoDID;
        public string InvSort;
        public string depCode;
        List<string> lstSocode = new List<string>();
        int idx = 0;
        public fmSolist()
        {
            InitializeComponent();
        }

        private void fmSolist_Resize(object sender, EventArgs e)
        {
            tbMemo.Width = this.ClientRectangle.Width -tbSo.Width;
            dgv.Height = this.ClientRectangle.Height - tbMemo.Height;            
        }
         private string GetSoMemo(string socode,int sodid)
        {           
            string sql = "select ccusabbname from customer a left join so_somain b on a.ccuscode= b.ccuscode where b.id in (select id from so_sodetails where csocode='"+socode+"')";
            string sCusabbname = Convert.ToString(SqlAccess.ExecuteScalar(sql, Conn));
            if (sodid > 0)
            {
                sql = "SELECT LTRIM(RTRIM(isnull(cDefine14,'')))+RTRIM(RTRIM(isnull(cDefine13,'')))+LTRIM(RTRIM(isnull(cDefine12,'')))+LTRIM(RTRIM(isnull(cDefine11,'')))+LTRIM(RTRIM(isnull(cDefine10,''))) FROM SO_SOMain WHERE id in (select id from so_sodetails where isosid=" + sodid + ")";
                sCusabbname += " " + Convert.ToString(SqlAccess.ExecuteScalar(sql, Conn));
            }
            return  sCusabbname;
            //sql = "SELECT iQuantity,cDefine22,SO_SODetails.cMemo,cDefine24,cDefine25,dPredate,cMaker FROM SO_SODetails  LEFT JOIN so_somain ON SO_SODetails.csocode=so_somain.csocode WHERE cInvCode='" + m_Room.cInvCode + "' AND so_somain.CSOCode='" + socode + "' AND iRowno=" + RowNo;
            //DataTable dt = SqlAccess.ExecuteSqlDataTable(sql, UFconn);
            //if (dt.Rows.Count == 0)
            //    return;
            //decimal dbtmp = decimal.Parse(dt.Rows[0]["iQuantity"].ToString());
            //string sQuantity = string.Format("{0:0.0}", dbtmp);
            //DateTime predate = (DateTime)dt.Rows[0]["dPredate"];
            //string sNumwarp = dt.Rows[0]["cDefine22"].ToString() == "" ? "0" : dt.Rows[0]["cDefine22"].ToString();
            //string sWeiwarp = dt.Rows[0]["cDefine25"].ToString() == "" ? "0" : dt.Rows[0]["cDefine25"].ToString();
            //string cPdesc = rtRoomdesc.Text;
            //if (socode != "")
            //{
            //    cPdesc = cPdesc + "订单详情　单号 " + socode + "-" + RowNo.ToString() + " 客户　" + sCusabbname + " 订购数量：" + sQuantity + " 预发日期：" + predate.ToShortDateString();
            //    cPdesc = cPdesc + " 数量偏差：" + sNumwarp + " 重量偏差：" + sWeiwarp + "\r\n";
            //    cPdesc = cPdesc + "产品备注：" + dt.Rows[0]["cMemo"].ToString();
            //    cPdesc = cPdesc + " 订单备注：" + sSOmemo + "\r\n";
            //    cPdesc = cPdesc + "制单：" + dt.Rows[0]["cMaker"].ToString();
            //}
            //rtRoomdesc.Text = cPdesc;    
        }
        private void fmSolist_Load(object sender, EventArgs e)
        {
            if (SoDID == 0)
                return;
            StringBuilder sb = new StringBuilder();
            sb.Append("select csocode from so_sodetails where isosid=" + SoDID);
            object oj = SqlAccess.ExecuteScalar(sb.ToString(), Conn);
            if (oj != null)
                tbSo.Text = oj.ToString();
            else
            {
                tbSo.Text = "";
                return;
            }
            
            showSo(tbSo.Text,SoDID);
            sb.Remove(0, sb.Length);            
        }

        private void showSo(string socode,int sodid)
        {
            if(sodid>0)
                tbMemo.Text = GetSoMemo(socode,SoDID);
            dgv.AutoGenerateColumns = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("select isosid,mos.modid,irowno,sd.cinvcode,inv.cinvname,cinvstd,cinvdefine1 as color,cast(iquantity as numeric(10,1)) iQuantity,(select sum(iqty) from ekk..c_packets where sodid=sd.isosid) completeqty,(select top 1 iqty from ekk..c_packets where sodid=sd.isosid) as packetnum,ccomunitname,dpredate,cdefine22,cdefine25,cmemo from so_sodetails sd left join inventory inv on sd.cinvcode=inv.cinvcode left join computationunit unit on inv.cComunitCode =unit.cComunitCode left join mom_orderdetail mos on sd.isosid=mos.orderdid where isnull(cSCloser,'')=''  and  csocode='" + socode + "'");
            if (depCode == "3001")
                sb.Append("  and (sd.cinvcode like '32%' or sd.cinvcode like '33%') order by autoid ");
            else if (depCode == "2305" || depCode == "2306")
                sb.Append("  and sd.cinvcode like '31%' order by autoid ");
            else
                sb.Append("  order by autoid ");
            DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), Conn);
            dgv.DataSource = dt;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Common.GetInt(dt.Rows[i]["isosid"]) == sodid && sodid > 0)
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                if (Common.GetNum(dt.Rows[i]["CompleteQty"]) == Common.GetNum(dt.Rows[i]["iQuantity"]))
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            }
        }
        
        private void tbSo_Click(object sender, EventArgs e)
        {
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            if (input.cResult.Length >= 3)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select top 10 csocode from so_somain where charindex('S',csocode)=0  and csocode like '%" + input.cResult + "%' order by dDate desc");
                DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), Conn);
                lstSocode.Clear();
                idx = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lstSocode.Add(dt.Rows[i][0].ToString());
                }
                if (lstSocode.Count > 0)
                {
                    tbSo.Text = lstSocode[idx];
                    showSo(tbSo.Text, 0);
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            idx++;
            if (idx >= lstSocode.Count)
                idx = 0;
            tbSo.Text = lstSocode[idx];
            showSo(tbSo.Text, 0);

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            idx--;
            if (idx <0)
                idx =lstSocode.Count-1;
            tbSo.Text = lstSocode[idx];
            showSo(tbSo.Text, 0);
        }
    }
}