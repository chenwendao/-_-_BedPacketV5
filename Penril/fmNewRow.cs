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
    public partial class fmNewRow : Form
    {
        public SqlConnection conn;
        public bool SRFlag = false;//Send=true;Receive=false;
        public string DepCode;
        public string VenCode;
        public decimal Qty;
        public string InvCode;
        public int MoID;
        public int MoDID;
        public int MoroutingDID;
        public int AvaiQty;
        public sfc_operation Op;
        public fmNewRow()
        {
            InitializeComponent();
        }

       

        private void tbModid_Click(object sender, EventArgs e)
        {
            tbModid.Text = "";
            tbMocode.Text = "";
            tbSortseq.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            int modid=Common.GetInt(input.cResult);
            object oj = SqlAccess.ExecuteScalar("select count(1) from mom_orderdetail where ISNULL(CloseUser,'')<>'' and modid=" + modid, conn);
            if (oj != DBNull.Value && oj != null && Common.GetInt(oj) != 0)
            {
                Common.ShowMessage("当前订单行已关闭，不能发（收）料！", 1);
                tbQty.ReadOnly = true;
                return;
            }
            else
                tbQty.ReadOnly = false;
            MoroutingDID = Common.GetInt(SqlAccess.ExecuteScalar(string.Format(@"select moroutingdid from sfc_moroutingdetail where OperationId={0} and modid={1}", Op.OperationId, modid), conn));
            DataTable dt = null;
            if (SRFlag)
            {//关于毛巾退料允许补料的修改               
                dt = SqlAccess.ExecuteSqlDataTable(string.Format(@"select  sum(iQuantity) [plan],sum(isnull(foutquantity,0)) out,sum(iQuantity-isnull(foutquantity,0)) avai from hy_modetails hd left join hy_momain hm on hd.id=hm.id where isourceautoid={0} and hm.cVenCode='{1}' and iQuantity-isnull(foutquantity,0)>0", MoroutingDID, VenCode), conn);
                if (dt.Rows.Count > 0)
                {
                    AvaiQty = Common.GetInt(dt.Rows[0]["avai"]);
                    tbPlanQty.Text = Common.GetInt(dt.Rows[0]["plan"]).ToString();
                    tbSend.Text = Common.GetInt(dt.Rows[0]["out"]).ToString();
                    if (Common.GetInt(dt.Rows[0]["plan"]) <= 0)
                    {//关于退料的修改
                        if (ckBack.Checked == false)
                        {
                            Common.ShowMessage("找不到与当前供应商匹配的加工单，不能发料！", 2);
                            tbQty.ReadOnly = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                dt = SqlAccess.ExecuteSqlDataTable(string.Format(@"select sum(isnull(fOutQuantity,0)) out,sum(isnull(fInQuantity,0)) inqty, sum(isnull(fOutQuantity,0)-isnull(fInQuantity,0)) avai from hy_modetails hd left join hy_momain hm on hd.id=hm.id where isourceautoid={0} and hm.cVenCode='{1}'and isnull(fOutQuantity,0)-isnull(fInQuantity,0)>0", MoroutingDID, VenCode), conn);
                if (dt.Rows.Count > 0)
                {
                    AvaiQty = Common.GetInt(dt.Rows[0]["avai"]);
                    tbPlanQty.Text = Common.GetInt(dt.Rows[0]["out"]).ToString();
                    tbSend.Text = Common.GetInt(dt.Rows[0]["inqty"]).ToString();
                    if (Common.GetInt(dt.Rows[0]["out"]) <= 0)
                    {
                        Common.ShowMessage("找不到与当前供应商的发料记录，不能收料！", 2);
                        tbQty.ReadOnly = true;
                        return;
                    }
                }
            }

           dt = SqlAccess.ExecuteSqlDataTable(@"select mo.mocode,mo.moID,mos.modid,sortseq,mos.qty,mos.invcode,inv.cinvname,cinvstd,
                cinvdefine1,cinvdefine11,qty,duedate,ccomunitname,isnull(cus.ccusabbname,'') cusabbname,mos.mDeptcode
                from mom_order mo 
                left join mom_orderdetail mos on mo.moid=mos.moid
                left join inventory inv on mos.invcode=inv.cinvcode
                left join mom_morder mom on mos.modid=mom.modid
                left join so_sodetails sos on mos.orderdid=sos.isosid
                left join so_somain so on sos.id=so.id
                left join computationunit unit on inv.ccomunitcode=unit.ccomunitcode
                left join customer cus on so.ccuscode=cus.ccuscode where mos.modid=" + input.cResult , conn);
            if (dt.Rows.Count>0)
            {                
                tbModid.Text = input.cResult;
                MoID =Common.GetInt(dt.Rows[0]["moid"]);
                MoDID = Common.GetInt(dt.Rows[0]["modid"]);
                tbMocode.Text = dt.Rows[0]["mocode"].ToString();
                tbSortseq.Text = dt.Rows[0]["sortseq"].ToString();
                InvCode = dt.Rows[0]["Invcode"].ToString();
                string desc = String.Format(@"定制客户：{0} 编码：{1} 名称：{2} 规格：{3} 颜色：{4} 数量：{5}条 是否绣花：{6} 入库日期：{7:yyyy-MM-dd}", dt.Rows[0]["cusabbname"].ToString(), dt.Rows[0]["invcode"].ToString(), dt.Rows[0]["cinvname"].ToString(), dt.Rows[0]["cinvstd"].ToString(),dt.Rows[0]["cinvdefine1"].ToString(), Common.GetInt(dt.Rows[0]["qty"]),  Common.GetInt(dt.Rows[0]["cinvdefine11"]) == 0 ? "不绣花" : "绣花", Convert.ToDateTime(dt.Rows[0]["duedate"]));
                
                tbUnit1.Text = dt.Rows[0]["ccomunitname"].ToString();
                tbUnit2.Text = tbUnit1.Text;
                tbUnit3.Text = tbUnit1.Text;

                DataTable dtm=SqlAccess.ExecuteSqlDataTable(@"select invcode,inv.cinvname,inv.cinvstd,inv.cinvdefine1 
                    from mom_moallocate allo 
                    left join inventory inv on allo.invcode=inv.cinvcode 
                    where substring(invcode,1,2)='13' and modid="+ input.cResult , conn);
                if (dtm.Rows.Count > 0)
                    desc += " 使用面料：";
                for (int i = 0; i < dtm.Rows.Count; i++)
                {
                    desc += String.Format("{0} {1} {2} {3}", dtm.Rows[i]["invcode"].ToString(), dtm.Rows[i]["cinvname"].ToString(), dtm.Rows[i]["cinvstd"].ToString(), dtm.Rows[i]["cinvdefine1"].ToString());
                }
                tbDesc.Text = desc;
                if (DepCode != dt.Rows[0]["mDeptcode"].ToString())
                {
                    Common.ShowMessage("当前订单生产部门与指定部门不符,输入可能有误,请检查!", 1);
                    tbQty.ReadOnly = true;
                    return;
                }
                dt.Clear();
            }
        }

        private void fmNewRow_Load(object sender, EventArgs e)
        {
            if (conn == null)
                return;
            if(Op==null && Public.OperID>0)
                Op = new sfc_operation(Public.OperID, conn);
            
            if (SRFlag)
            {
                lbQtyed.Text = "已发数量";
                lbThisqty.Text = "本次发出";
            }
            else
            {
                lbQtyed.Text = "已收数量";
                lbThisqty.Text = "本次收入";
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string  ValidData()
        {
            decimal thisQty = Common.GetNum(tbQty.Text);
            decimal planQty=Common.GetNum(tbPlanQty.Text);
            decimal sendQty=Common.GetNum(tbSend.Text);
            decimal leftQty =  planQty-sendQty;
            if (thisQty<=0)
                return "发收料数量不能小于零。";
            if (thisQty > AvaiQty || thisQty > planQty)
                return "发收料数量不能多于加工单允许数量！";
            if (Op.Description.IndexOf("绣花") >= 0)
            {
                if (InvCode.Substring(0, 2) == "32" && planQty <= 1000 && (sendQty + thisQty - planQty > 2)) //1000以下可以多发2条
                    return "违反了1000条以下只可多发2条的规定。";
                if ((InvCode.Substring(0, 2) == "32" && planQty > 1000 && (InvCode.Substring(3, 1) == "1" || InvCode.Substring(3, 1) == "2") && planQty > 1000 && (sendQty + thisQty - planQty > 4)) || (InvCode.Substring(0, 2) == "32" && planQty > 1000 && InvCode.Substring(3, 1) == "3" && planQty > 1000 && (sendQty + thisQty - planQty > 3))) //1000条以上方面可以多发4条
                    return "违反了1000条以上方面巾只可多发4条，浴巾只可多发3条的规定";                
            }
            return "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string Msg = ValidData();
            if (Msg!="" && MessageBox.Show(Msg+"您确认要继续吗?", "数据检查", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Qty =Common.GetNum(tbQty.Text);

            if (ckBack.Checked)
                Qty = 0 - Qty;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbQty_Click(object sender, EventArgs e)
        {
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            tbQty.Text = input.cResult;
        }
    }
}