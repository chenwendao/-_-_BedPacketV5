using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using Printers;
using CWD.DAL;
using CWD.Report;

namespace CWD
{
    public partial class ucSewing : UserControl
    {
        public SqlConnection conn;
        public hy_issue Issue = null;
        public hy_receive Receive = null;
        public string Status;
        public bool SRFlag = false;//Send=true,Receive=false;
        private sfc_operation Op;
        public ucSewing()
        {
            InitializeComponent();
        }
        private bool ValidData()
        {
            if (cwDep.Value == null || cwDep.Value == "")
            {
                return false;
            }
            if (cwMaker.Text=="")
            {
                return false;
            }
            if (cwVendor.Value == null || cwVendor.Value == "")
            {
                return false;
            }
            return true;        
        }
        private void tsbAddrow_Click(object sender, EventArgs e)
        {
            if (!ValidData())
            {
                Common.ShowMessage("请输入部门、制单人、供应商参数！",1);
                return;
            }
            dgv.AutoGenerateColumns = false;
            fmNewRow row = new fmNewRow();
            row.conn = conn;
            row.SRFlag = SRFlag;
            row.DepCode= cwDep.Value;
            row.VenCode = cwVendor.Value;
            row.Op =Op;
            if (row.SRFlag)
                row.Text = string.Format("{0}外发",Op.Description);
            else
                row.Text = string.Format("{0}收料", Op.Description);
            if (row.ShowDialog() != DialogResult.OK)
                return;

            int TotalAvaiOut = 0, TotalAvaiIn = 0,TotalIn=0;
            DataTable dt = SqlAccess.ExecuteSqlDataTable(string.Format("select autoid,iQuantity-isnull(fOutQuantity,0) as PlanOut,isnull(fOutQuantity,0)-isnull(fInQuantity,0) as PlanIn,isnull(fInQuantity,0) as InQty from hy_modetails where isourceautoid ={0} and iQuantity-isnull(fOutQuantity,0)>0", row.MoroutingDID), conn);
            if (dt.Rows.Count == 0)
            {
                Common.ShowMessage("找不到对应工序委外加工单！", 2);
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TotalAvaiOut += Common.GetInt(dt.Rows[i]["PlanOut"]);
                    TotalAvaiIn += Common.GetInt(dt.Rows[i]["PlanIn"]);
                    TotalIn += Common.GetInt(dt.Rows[i]["InQty"]);
                }
            }
            if (SRFlag)
            {
                if (row.Qty > 0 && TotalAvaiOut < row.Qty)
                {
                    Common.ShowMessage("发料数量不能多于可发料数量！", 2);
                    return;
                }
                if (row.Qty < 0 && TotalIn+row.Qty < 0)
                {
                    Common.ShowMessage("退料数量不能多于可收料数量！", 2);
                    return;
                }
                if (Issue == null)
                {
                    Vendor ven = new Vendor(cwVendor.Value, conn);
                    Issue = new hy_issue();
                    Issue.cDepCode = cwDep.Value;
                    Issue.cPersonCode = ven.cVenPPerson;
                    Issue.cVenCode = cwVendor.Value;
                    Issue.VenAbbName = ven.cVenAbbName;
                    Issue.Maker = cwMaker.Text;
                    Issue.cBusType = "工序委外";
                    Issue.iBusSTyle = 1;
                    Issue.dDate = DateTime.Today;
                    
                    Issue.iVTid = 30920;
                    Issue.dCreateDate = DateTime.Today;
                    Issue.dCreateTime = DateTime.Now;
                    Issue.cCreateUser = Public.userCode;

                }
                int RemainQty = (int)row.Qty;
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if ((row.Qty>0 && RemainQty <= 0) ||(row.Qty<0 && RemainQty>0))
                        break;
                    int MaxAvai = RemainQty > 0 ? Common.GetInt(dt.Rows[i]["PlanOut"]) : Common.GetInt(dt.Rows[i]["PlanIn"]);
                    HY_MODetails hd = new HY_MODetails(Common.GetInt(dt.Rows[i]["AutoId"]), conn);
                    HY_MOMain hh = new HY_MOMain(hd.ID, conn);
                    hy_issuedetail id = new hy_issuedetail();
                    id.iSortSeq = Issue.Items.Count + 1;
                    id.iOrderId = hd.ID;
                    id.iOrderDId = hd.AUTOID;
                    id.iMoRoutingDId = row.MoroutingDID;
                    id.bGenOrderFlag = true;
                    if (row.Qty > 0)
                    {
                        id.iQuantity = RemainQty < MaxAvai ? RemainQty : MaxAvai;
                        RemainQty -= (int)id.iQuantity;
                    }
                    else
                    {
                        id.iQuantity = RemainQty + MaxAvai > 0 ? RemainQty : 0 - MaxAvai;
                        RemainQty -= (int)id.iQuantity;
                    }
                    
                    id.isFirstOP = false;
                    id.isLastOP = false;
                    id.bGenOrderFlag = false;
                    StringBuilder sb=new StringBuilder();
                    sb.Append(@" select mo.mocode,sortseq,mos.invcode,cinvname,cinvstd,cinvdefine1,ccomunitname from mom_orderdetail mos 
                        left join mom_order mo on mo.moid=mos.moid left join inventory inv on mos.invcode=inv.cinvcode 
                        left join computationunit unit on inv.ccomunitcode=unit.ccomunitcode where mos.modid="+row.MoDID);
                    DataTable dt1 = SqlAccess.ExecuteSqlDataTable(sb.ToString(),conn);
                    if(dt.Rows.Count>0)
                    {
                        if (dt1.Rows[0]["invcode"] != null && dt1.Rows[0]["invcode"].ToString() != "")
                        {
                            id.InvCode = dt1.Rows[0]["invcode"].ToString();
                        }
                        if (dt1.Rows[0]["mocode"] != null && dt1.Rows[0]["mocode"].ToString() != "")
                        {
                            id.MoCode = dt1.Rows[0]["mocode"].ToString();
                        }
                        if (dt1.Rows[0]["sortseq"] != null && dt1.Rows[0]["sortseq"].ToString() != "")
                        {
                            id.MoSeq =Common.GetInt(dt1.Rows[0]["sortseq"]);
                        }
                        if (dt1.Rows[0]["cinvname"] != null && dt1.Rows[0]["cinvname"].ToString() != "")
                        {
                            id.InvName = dt1.Rows[0]["cinvname"].ToString();
                        }
                        if (dt1.Rows[0]["cinvstd"] != null && dt1.Rows[0]["cinvstd"].ToString() != "")
                        {
                            id.InvStd = dt1.Rows[0]["cinvstd"].ToString();
                        }
                        if (dt1.Rows[0]["cinvdefine1"] != null && dt1.Rows[0]["cinvdefine1"].ToString() != "")
                        {
                            id.Color = dt1.Rows[0]["cinvdefine1"].ToString();
                        }
                        if (dt1.Rows[0]["ccomunitname"] != null && dt1.Rows[0]["ccomunitname"].ToString() != "")
                        {
                            id.UnitName = dt1.Rows[0]["ccomunitname"].ToString();
                        }
                    }
                    Issue.Items.Add(id);
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = Issue.Items;
                dgv.DataSource = bs;
            }
            else
            {
                if (row.Qty > 0 && TotalAvaiIn < row.Qty)
                {
                    Common.ShowMessage("收料数量不能多于可收料数量！", 2);
                    return;
                }
                if (row.Qty < 0 && TotalIn + row.Qty < 0)
                {
                    Common.ShowMessage("退料数量不能多于已收料数量！", 2);
                    return;
                }
                if (Receive == null)
                {
                    Vendor ven = new Vendor(cwVendor.Value, conn);
                    Receive = new hy_receive();
                    Receive.cDepCode = cwDep.Value;
                    Receive.cPersonCode = ven.cVenPPerson;
                    Receive.cVenCode = cwVendor.Value;
                    Receive.cBusType = "工序委外";
                    
                    Receive.dDate = DateTime.Today;
                    //Receive.cCode = Receive.GetNewVouCode(DateTime.Today, conn);
                    Receive.iVTid = 30922;
                    Receive.dCreateDate = DateTime.Today;
                    Receive.dCreateTime = DateTime.Now;
                    Receive.cCreateUser = Public.userCode;
                }
                int RemainQty = (int)row.Qty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((row.Qty > 0 && RemainQty <= 0) || (row.Qty < 0 && RemainQty > 0))
                        break;
                    int MaxAvai = Common.GetInt(dt.Rows[i]["PlanIn"]);
                    HY_MODetails hd = new HY_MODetails(Common.GetInt(dt.Rows[i]["AutoId"]), conn);
                    HY_MOMain hh = new HY_MOMain(hd.ID, conn);
                    hy_receivedetail id = new hy_receivedetail();
                    id.iSortSeq = Receive.Items.Count + 1;
                    id.iOrderId = hd.ID;
                    id.iOrderDId = hd.AUTOID;

                    id.iMoOrderId = row.MoID;
                    id.iMoOrderDId = row.MoDID;
                    id.iMoRoutingDId = row.MoroutingDID;
                    id.cHyOrderCode = hh.cCode;
                    if (row.Qty > 0)
                    {
                        id.iQuantity = RemainQty < MaxAvai ? RemainQty : MaxAvai;
                        RemainQty -= (int)id.iQuantity;
                    }
                    else
                    {
                        id.iQuantity = RemainQty + MaxAvai > 0 ? RemainQty : 0 - MaxAvai;
                        RemainQty -= (int)id.iQuantity;
                    }
                   
                    id.QcFlag = false;
                    id.QcCompletion = false;
                    id.iQualifiedQty = row.Qty;
                    id.isFirstOP = false;
                    id.isLastOP = false;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(@" select mo.mocode,sortseq,mos.invcode,cinvname,cinvstd,cinvdefine1,ccomunitname from mom_orderdetail mos 
                        left join mom_order mo on mo.moid=mos.moid left join inventory inv on mos.invcode=inv.cinvcode left join 
                        computationunit unit on inv.ccomunitcode=unit.ccomunitcode where mos.modid=" + row.MoDID);
                    DataTable dt1 = SqlAccess.ExecuteSqlDataTable(sb.ToString(), conn);
                    if (dt.Rows.Count > 0)
                    {
                        id.cMoOrderCode = dt.Rows[0]["mocode"].ToString();
                        id.iMoOrderSeq = Common.GetInt(dt.Rows[0]["sortseq"]);
                        if (dt1.Rows[0]["invcode"] != null && dt1.Rows[0]["invcode"].ToString() != "")
                        {
                            id.InvCode = dt1.Rows[0]["invcode"].ToString();
                        }
                        if (dt1.Rows[0]["mocode"] != null && dt1.Rows[0]["mocode"].ToString() != "")
                        {
                            id.MoCode = dt1.Rows[0]["mocode"].ToString();
                        }
                        if (dt1.Rows[0]["sortseq"] != null && dt1.Rows[0]["sortseq"].ToString() != "")
                        {
                            id.MoSeq = Common.GetInt(dt1.Rows[0]["sortseq"]);
                        }
                        if (dt1.Rows[0]["cinvname"] != null && dt1.Rows[0]["cinvname"].ToString() != "")
                        {
                            id.InvName = dt1.Rows[0]["cinvname"].ToString();
                        }
                        if (dt1.Rows[0]["cinvstd"] != null && dt1.Rows[0]["cinvstd"].ToString() != "")
                        {
                            id.InvStd = dt1.Rows[0]["cinvstd"].ToString();
                        }
                        if (dt1.Rows[0]["cinvdefine1"] != null && dt1.Rows[0]["cinvdefine1"].ToString() != "")
                        {
                            id.Color = dt1.Rows[0]["cinvdefine1"].ToString();
                        }
                        if (dt1.Rows[0]["ccomunitname"] != null && dt1.Rows[0]["ccomunitname"].ToString() != "")
                        {
                            id.UnitName = dt1.Rows[0]["ccomunitname"].ToString();
                        }
                    }
                    Receive.Items.Add(id);
                }
                BindingSource bs = new BindingSource();
                bs.DataSource= Receive.Items;
                dgv.DataSource = bs;
            }
            //ShowSR(lbSheetID.Text);
            tsbSave.Enabled = true;
        }

        private void ShowSR(string Code)
        {
            StringBuilder sb = new StringBuilder();            
            dgv.AutoGenerateColumns = false;
            if (SRFlag)
            {
                sb.Append("select issueid from hy_issue where ccode=" + Common.SqlParm(Code));
                object oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj == null || oj==DBNull.Value)
                    return;
                Issue = new hy_issue(Common.GetInt(oj), conn);
                lbSheetID.Text = Issue.cCode;
                cwDep.Value = Issue.cDepCode;
                cwDep.Text = Issue.DepName;
                cwMaker.Value = Issue.cCreateUser;
                cwMaker.Text = Issue.Maker;
                cwVendor.Value = Issue.cVenCode;
                cwVendor.Text = Issue.VenAbbName;
                BindingSource bs = new BindingSource();
                bs.DataSource = Issue.Items;
                dgv.DataSource = bs;
                if (Issue.cRelsUser != null && Issue.cRelsUser!="")
                    SetbtnState("审核");
                else
                    SetbtnState("保存");
            }
            else
            {
                sb.Append("select receiveid from hy_receive where ccode=" + Common.SqlParm(Code));
                object oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj == null || oj==DBNull.Value)
                    return;
                Receive = new hy_receive(Common.GetInt(oj), conn);
                lbSheetID.Text = Receive.cCode;
                cwDep.Value = Receive.cDepCode;
                cwDep.Text = Receive.DepName;
                cwMaker.Value = Receive.cCreateUser;
                cwMaker.Text = Receive.Maker;
                cwVendor.Value = Receive.cVenCode;
                cwVendor.Text = Receive.VenAbbName;
                BindingSource bs = new BindingSource();
                bs.DataSource =Receive.Items;
                dgv.DataSource = bs;
                if (Receive.cRelsUser != null &&Receive.cRelsUser!="")
                    SetbtnState("审核");
                else
                    SetbtnState("保存");
            }
            dgv.Refresh();
        }
        public void SetSRFlag(bool flag)
        {
            SRFlag=flag;
            if (flag)
            {
                lbTitle.Text = string.Format("{0}委外发料",Op.Description);
            }
            else
            {
                lbTitle.Text = string.Format("{0}委外收料", Op.Description);
            }
            lbTitle.Left=(this.ClientRectangle.Width-lbTitle.Width)/2;
            cwMaker.Text=Public.userName;
        }
        public void ChangeSize()
        {
            dgv.Width = this.ClientRectangle.Width-4;
            dgv.Left = 2;
            dgv.Height = this.ClientRectangle.Height - 132;
        }
        public void Init()
        {
            StringBuilder sb = new StringBuilder();
            if (SRFlag)
            {
                sb.Append(string.Format(@"  select max(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid 
                    left join sfc_moroutingdetail md on red.imoroutingdid=md.moroutingdid 
                    where md.description = '{0}'  and substring(ccode,1,6)={1} and cCreateUser='{2}'",
                    Op.Description, Common.SqlParm(String.Format("WF{0}", DateTime.Now.ToString("yyMM"))),Public.userCode));
                object oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                }
            }
            else
            {
                sb.Append(string.Format(@" select max(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid left join sfc_moroutingdetail md 
                on red.imoroutingdid=md.moroutingdid where md.description = '{0}' and substring(ccode,1,6)='WS{1}' and cCreateUser={2}",Op.Description, DateTime.Now.ToString("yyMM") , Common.SqlParm(Public.userCode)));
                object oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                }
            }
        }
        private void ucBrede_Load(object sender, EventArgs e)
        {
            try
            {
                conn = SqlAccess.SqlConn(Public.ufconnstr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，可能的原因是：" + ex.Message.ToString(), "提示");
                return;
            }
            ChangeSize();
            cwDep.Text = "床品车间";
            cwDep.Value = "3001";
            cwMaker.Text = Public.userName;
            cwMaker.Value = Public.userCode;
            cwVendor.Text = "";
            cwVendor.Value = "";
            lbSheetID.Text = "";
            Op = new sfc_operation(Public.OperID, conn);
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            ChangeSize();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {

            fmSewing brede = (fmSewing)this.Parent;
            brede.Close();           
        }

        private void SetbtnState(string Mode)
        {
            for (int i = 0; i < tsp1.Items.Count; i++)
            {
                tsp1.Items[i].Enabled = false;
            }
            switch (Mode)
            {
                case "新单":                    
                    tsbExit.Enabled = true;
                    tsbAddrow.Enabled = true;
                    Status = "新单";
                    break;
                case "保存":
                    tsbNewsheet.Enabled = true;
                    tsbPrev.Enabled = true;
                    tsbNext.Enabled = true;
                    tsbExit.Enabled = true;
                    tsbAudit.Enabled = true;
                    tsbAddrow.Enabled = true;
                    //tsbPrint.Enabled = true;
                    Status = "保存";
                    break;
                case "审核":
                    tsbNewsheet.Enabled = true;
                    tsbPrev.Enabled = true;
                    tsbNext.Enabled = true;
                    tsbExit.Enabled = true;
                    tsbUnAudit.Enabled = true;
                    tsbPrint.Enabled = true;
                    Status = "审核";
                    break;
            }
        }

        private void cwVendor_Click(object sender, EventArgs e)
        {
            cwVendor.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            DataTable dt = SqlAccess.ExecuteSqlDataTable(@"SELECT Vendor.cVenCode,cVenAbbName  FROM Vendor 
                INNER JOIN EKK..C_SewPlant plant ON Vendor.cVencode=plant.cVencode where plant.AutoID=" + input.cResult, conn);
            if (dt.Rows.Count>0)
            {
                cwVendor.Text = dt.Rows[0]["cVenAbbName"].ToString();
                cwVendor.Value = dt.Rows[0]["cVenCode"].ToString();
            }
        }

        private void cwDep_Click(object sender, EventArgs e)
        {
            cwDep.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cDepName FROM Department where cDepCode='" + input.cResult + "'", conn);
            if (oj != null)
            {
                cwDep.Text = oj.ToString();
                cwDep.Value = input.cResult;
            }
        }

       

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (SRFlag &&  Issue.IssueID == 0)
            {
                Issue.cCode = Issue.GetNewVouCode(DateTime.Today, conn);
            }
            else if (!SRFlag && Receive.receiveID == 0)
            {
                Receive.cCode = Receive.GetNewVouCode(DateTime.Today, conn);
            }
            if (conn.State.ToString().Trim().ToLower() != "open")
                conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (SRFlag)
                {
                    if (Issue.IssueID == 0)
                    {                       
                        Issue.Add(tran);
                    }
                    else
                    {
                        Issue.cModifyUser = Public.userCode;
                        Issue.dModifyDate = DateTime.Today;
                        Issue.dModifyTime = DateTime.Now;
                        Issue.Update(tran);
                    }                   
                }
                else
                {
                    if (Receive.receiveID == 0)
                    {                        
                        Receive.Add(tran);
                    }
                    else
                    {
                        Receive.cModifyUser = Public.userCode;
                        Receive.dModifyDate = DateTime.Today;
                        Receive.dModifyTime = DateTime.Now;
                        Receive.Update(tran);
                    }                    
                }
                tran.Commit();
                SetbtnState("保存");
                if(SRFlag)
                    ShowSR(Issue.cCode);
                else
                    ShowSR(Receive.cCode);
            }
            catch (Exception ex)
            { 
                tran.Rollback();
                Common.ShowMessage(ex.Message, 2);
                return;
            }
        }
       
        private void tsbAudit_Click(object sender, EventArgs e)
        {
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (SRFlag)
                {
                    Issue.cRelsUser = Public.userCode;
                    Issue.dRelsDate = DateTime.Today;
                    //ModiMrs(Issue, true,tran);
                    Issue.Update(tran);                   
                }
                else
                {
                    Receive.cRelsUser = Public.userCode;
                    Receive.dRelsDate = DateTime.Today;
                    //ModiMrs(Receive, true, tran);
                    Receive.Update(tran);
                }
                tran.Commit();
                SetbtnState("审核");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Common.ShowMessage(ex.Message, 2);
                return;
            }
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            int rowno=dgv.CurrentCell.RowIndex;
            if (SRFlag)
            {
                hy_issuedetail thisissue= Issue.Items[rowno];
                Issue.Items.Remove(thisissue);
                BindingSource bs = (BindingSource)dgv.DataSource;
                bs.DataSource = Issue.Items;
                dgv.DataSource = null;
                dgv.DataSource = bs;
            }
            else
            {
                hy_receivedetail thisre =Receive.Items[rowno];
                Receive.Items.Remove(thisre);
                BindingSource bs = (BindingSource)dgv.DataSource;
                bs.DataSource = Receive.Items;
                dgv.DataSource = null;
                dgv.DataSource = bs;
            }
            tsbDel.Enabled = false;
            tsbSave.Enabled = true;
        }

        private void tsbUnAudit_Click(object sender, EventArgs e)
        {
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (SRFlag)
                {
                    if(Issue.cRelsUser!=Public.userCode)
                    {
                        tran.Rollback();
                        Common.ShowMessage("您不是当前单据审核人，不能弃审！",1);
                        return;
                    }
                    Issue.cRelsUser =null;
                    Issue.dRelsDate = null;
                    //ModiMrs(Issue, false, tran);
                    Issue.Update(tran);                    
                }
                else
                {
                     if(Receive.cRelsUser!=Public.userCode)
                    {
                        tran.Rollback();
                        Common.ShowMessage("您不是当前单据审核人，不能弃审！",1);
                        return;
                    }
                    Receive.cRelsUser =null;
                    Receive.dRelsDate = null;
                    //ModiMrs(Receive, false, tran);
                    Receive.Update(tran);
                }
                tran.Commit();
                SetbtnState("保存");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Common.ShowMessage(ex.Message, 2);
                return;
            }
        }

        private void tsbPrev_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            object oj = null;
            if (SRFlag)
            {
                if (lbSheetID.Text.Trim() == "")
                    sb.Append(string.Format(@" select max(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                        left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}' 
                         and substring(ccode,1,6)='WF{1}' and cCreateUser={2}",Op.Description,DateTime.Now.ToString("yyMM"),Common.SqlParm(Public.userCode)));                
                else
                    sb.Append(string.Format(@" select max(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                        left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}' 
                        and substring(ccode,1,6)={1} and substring(ccode,7,4)<{2} and cCreateUser={3}",Op.Description,Common.SqlParm(lbSheetID.Text.Trim().Substring(0, 6)),Common.SqlParm(lbSheetID.Text.Trim().Substring(6, 4)) , Common.SqlParm(Public.userCode)));                
                oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                    tsbNext.Enabled = true;
                }
                else if(lbSheetID.Text.Trim() != "")
                {
                    sb.Append(string.Format(@" select max(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                        left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}'  
                        and substring(ccode,3,8)<{1} and cCreateUser={2}", Op.Description, Common.SqlParm(lbSheetID.Text.Trim().Substring(2, 8)) ,Common.SqlParm(Public.userCode)));                    
                    oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                    if (oj != null && oj != DBNull.Value)
                    {
                        ShowSR(oj.ToString());
                        tsbNext.Enabled = true;
                    }
                    else
                        tsbPrev.Enabled = false;
                }
            }
            else
            {
                if (lbSheetID.Text.Trim() == "")
                    sb.Append(string.Format(@" select max(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid left join sfc_moroutingdetail md 
                        on red.imoroutingdid=md.moroutingdid where md.description = '{0}' and substring(ccode,1,6)='WS{1}' and cCreateUser={2}" ,Op.Description,DateTime.Now.ToString("yyMM"), Common.SqlParm(Public.userCode)));
                
                else
                    sb.Append(string.Format(@" select max(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid 
                        left join sfc_moroutingdetail md on red.imoroutingdid=md.moroutingdid where md.description = '{0}' and substring(ccode,1,6)={1} and substring(ccode,7,4)<{2} and cCreateUser={3}"
                        ,Op.Description,Common.SqlParm(lbSheetID.Text.Trim().Substring(0, 6)), Common.SqlParm(lbSheetID.Text.Trim().Substring(6, 4)) ,Common.SqlParm(Public.userCode)));                
                oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                    tsbNext.Enabled = true;
                }
                else if(lbSheetID.Text.Trim() != "")
                {
                    sb.Append(string.Format(@"  select max(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid 
                        left join sfc_moroutingdetail md on red.imoroutingdid=md.moroutingdid where md.description = '{0}' and substring(ccode,3,8)<{1}  and cCreateUser={2}"
                        ,Op.Description, Common.SqlParm(lbSheetID.Text.Trim().Substring(2, 8)), Common.SqlParm(Public.userCode)));
                    oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                    if (oj != null && oj != DBNull.Value)
                    {
                        ShowSR(oj.ToString());
                        tsbNext.Enabled = true;
                    }
                    else
                        tsbPrev.Enabled = false;
                }
            }
        }

        private void tsbNext_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            object oj = null;
            if (SRFlag)
            {
                if (lbSheetID.Text.Trim() == "")
                    sb.Append(string.Format(@" select min(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                        left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}' 
                        and substring(ccode,1,6)='WF{1}' and cCreateUser={2}",Op.Description,DateTime.Now.ToString("yyMM"),Common.SqlParm(Public.userCode)));
                else
                    sb.Append(string.Format(@" select min(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                        left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}'
                        and substring(ccode,1,6)={1} and substring(ccode,7,4)>{2} and cCreateUser={3}",Op.Description,
                     Common.SqlParm(lbSheetID.Text.Trim().Substring(0, 6)),Common.SqlParm(lbSheetID.Text.Trim().Substring(6, 4)), Common.SqlParm(Public.userCode)));
                oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                    tsbPrev.Enabled=true;
                }
                else if(lbSheetID.Text.Trim() != "")
                {
                    sb.Append(string.Format(@" select min(ccode) from hy_issue ish left join hy_issuedetail isd on ish.issueid=isd.issueid 
                    left join sfc_moroutingdetail md on isd.imoroutingdid=md.moroutingdid where md.description = '{0}' 
                    and substring(ccode,3,8)>{1} and cCreateUser={2}",Op.Description,Common.SqlParm(lbSheetID.Text.Trim().Substring(2, 8)) , Common.SqlParm(Public.userCode)));
                    oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                    if (oj != null && oj != DBNull.Value)
                    {
                        ShowSR(oj.ToString());
                        tsbPrev.Enabled=true;
                    }
                    else
                        tsbNext.Enabled = false;
                }
            }
            else
            {
                if (lbSheetID.Text.Trim() == "")
                    sb.Append(string.Format(@" select min(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid 
                        left join sfc_moroutingdetail md on red.imoroutingdid=md.moroutingdid where md.description = '{0}'
                        and substring(ccode,1,6)='WS{1}' and cCreateUser=",Op.Description, DateTime.Now.ToString("yyMM"),Common.SqlParm(Public.userCode)));
                else
                    sb.Append(string.Format(@" select min(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid left join sfc_moroutingdetail 
                        md on red.imoroutingdid=md.moroutingdid where md.description = '{0}'  and substring(ccode,1,6)={1} and substring(ccode,7,4)>{2} and cCreateUser={3}"
                        ,Op.Description,Common.SqlParm(lbSheetID.Text.Trim().Substring(0, 6)),Common.SqlParm(lbSheetID.Text.Trim().Substring(6, 4)), Common.SqlParm(Public.userCode)));                
                oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                if (oj != null && oj != DBNull.Value)
                {
                    ShowSR(oj.ToString());
                    tsbPrev.Enabled=true;
                }
                else if(lbSheetID.Text.Trim() != "")
                {
                    sb.Append(string.Format(@" select min(ccode) from hy_receive reh left join hy_receivedetail red on reh.receiveid=red.receiveid left join sfc_moroutingdetail md 
                        on red.imoroutingdid=md.moroutingdid where md.description = '{0}' and substring(ccode,3,8)>{1} and cCreateUser={2}"
                        ,Op.Description,Common.SqlParm(lbSheetID.Text.Trim().Substring(2, 8)) ,Common.SqlParm(Public.userCode)));
                    oj = SqlAccess.ExecuteScalar(sb.ToString(), conn);
                    if (oj != null && oj != DBNull.Value)
                    {
                        ShowSR(oj.ToString());
                        tsbPrev.Enabled=true;
                    }
                    else
                        tsbNext.Enabled = false;
                }
            }
        }

        private void tsbNewsheet_Click(object sender, EventArgs e)
        {
            cwDep.Text = "";
            cwDep.Value = "";
            cwMaker.Text = Public.userName;
            cwMaker.Value = Public.userCode;
            cwVendor.Text = "";
            cwVendor.Value = "";
            lbSheetID.Text = "";
            BindingSource bs = null;
            if (dgv.DataSource != null)
                bs = (BindingSource)dgv.DataSource;
            else
                bs = new BindingSource();
            bs.DataSource = null;
            dgv.DataSource = bs;
            if (SRFlag)
                Issue = null;
            else            
                Receive = null;
            SetbtnState("新单");
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {     
            try
            {
                string prnName="";
                List<C_PrinterSet> lstPrinter = C_PrinterSet.getModelPrinter("缝纫", Public.userCode, conn);
                foreach (C_PrinterSet prn in lstPrinter)
                {
                    if (prn.Function == "BARCODE")
                    {
                        prnName = prn.Printer;
                        break;
                    }
                }
                if (prnName == "")
                {
                    if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0)
                        prnName = System.Drawing.Printing.PrinterSettings.InstalledPrinters[0];
                    else
                    {
                        Common.ShowMessage("请安装打印机!",2);
                        return;
                    }
                }
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = prnName;
                
                int paperHeight = 270;
                if (SRFlag)
                    paperHeight = paperHeight + 30 * Issue.Items.Count;
                else
                    paperHeight = paperHeight + 30 * Receive.Items.Count;
                pd.DefaultPageSettings.PaperSize = new PaperSize("mypage",300, paperHeight);

                pd.PrintPage += new PrintPageEventHandler(PrintInOut);
                pd.Print();
            }
            catch
            {
                MessageBox.Show("打印失败！请检查打印机是否可用....", "系统提示");
                return;
            } 
        }

        private void PrintInOut(object sender, PrintPageEventArgs ev)
        {
            Font printFont;
            string str;
            float yPos = 0;
            int count = 0;
            float leftMargin = 5;   //ev.MarginBounds.Left;   
            float topMargin = 20;   //ev.MarginBounds.Top;   

            string Drafter = "";
            string Cutoper = "";
            printFont = new Font("黑体", 12);
            if (SRFlag)
            {
                str = "缝 纫 加 工 发 料 单";
                object oj = SqlAccess.ExecuteScalar("select cpersonname from mom_order mo left join person on mo.createuser=person.cpersoncode where mocode=" + Common.SqlParm(Issue.Items[0].MoCode), conn);
                Drafter = oj.ToString();
                oj = SqlAccess.ExecuteScalar(@"select top 1 cpsn_name from ekk..C_BedCut_Details bd left join ekk..C_BedCut_Details_Employ bde on bd.autoid=bde.id left join sfc_moroutingdetail mrs on bd.moroutingdid=mrs.moroutingdid left join mom_orderdetail mos on mrs.modid=mos.modid left join mom_order mo on mo.moid=mos.moid left join hr_hi_person per on bde.empcode=per.cpsn_num where mocode=" + Common.SqlParm(Issue.Items[0].MoCode) + " and mos.sortseq=" + Issue.Items[0].MoSeq, conn);
                if (oj != null) Cutoper = oj.ToString();
            }
            else
                str = "缝 纫 加 工 收 料 单";
            str = "      "+ str;
            yPos = topMargin +  printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            printFont = new Font("新宋体", 10);
            str = "";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            if (SRFlag)
            {
                string cusname = Issue.Items[0].InvName.IndexOf('-') > 0 ? Issue.Items[0].InvName.Split('-')[0] : "";
                str = String.Format("单号{0}加工商 {1}客户 {2}", Issue.cCode,Issue.VenAbbName, cusname);
            }
            else
            {
                string cusname = Receive.Items[0].InvName.IndexOf('-') > 0 ? Receive.Items[0].InvName.Split('-')[0] : "";
                str = String.Format("单号{0}加工商 {1}  客户 {2}", Receive.cCode,Receive.VenAbbName, cusname);
            }
            yPos = topMargin +printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            
            str = "========================================";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            str = " 生产订单 行  品名   规格   数量 单位";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;


            str = "----------------------------------------";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            if (SRFlag)
            {
                for (int i = 0; i < Issue.Items.Count; i++)
                {
                    sfc_moroutingdetail mrd = new sfc_moroutingdetail((int)Issue.Items[i].iMoRoutingDId,conn);
                    string InvName = Issue.Items[i].InvName.IndexOf('-') < 0 ? Issue.Items[i].InvName : Issue.Items[i].InvName.Split('-')[1];
                    str = String.Format("{0} {1}{2}{3}", Issue.Items[i].MoCode, Issue.Items[i].MoSeq, InvName, Issue.Items[i].InvStd);
                    yPos = topMargin + printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    topMargin = yPos;

                    str = String.Format("条形码：{0}                  {1}{2}", mrd.MoDId, Issue.Items[i].iQuantity, Issue.Items[i].UnitName);
                    //SizeF szStr= ev.Graphics.MeasureString(str,printFont);
                    //SizeF szSpace=ev.Graphics.MeasureString(" ",printFont);
                    //str = str.PadLeft((int)((300 - szStr.Width)/szSpace.Width), ' ');
                    yPos = topMargin + printFont.GetHeight(ev.Graphics);
                    
                    ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    topMargin = yPos;
                }
            }
            else
            {
                for (int i = 0; i < Receive.Items.Count; i++)
                {
                    string InvName = Receive.Items[i].InvName.IndexOf('-') < 0 ? Receive.Items[i].InvName : Receive.Items[i].InvName.Split('-')[1];
                    str = String.Format("{0} {1}{2}{3}", Receive.Items[i].MoCode, Receive.Items[i].MoSeq, InvName, Receive.Items[i].InvStd);
                    yPos = topMargin + printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    topMargin = yPos;

                    str = String.Format("{0}{1}", Receive.Items[i].iQuantity, Receive.Items[i].UnitName);
                    str = str.PadLeft(40 - str.Length, ' ');
                    yPos = topMargin + printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    topMargin = yPos;
                }
            }            

            str = "----------------------------------------";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            if (SRFlag)
                str = "制单:"+Issue.Maker+" 发料:     工艺:"+Drafter+" 审核:";
            else
                str = "制单: " + Receive.Maker + " 收料:     运输:     审核:"; ;
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;
            if (SRFlag)
                str = "开剪"+Cutoper+" 复核:                       ";
            else
                str = "                     复核:                       ";
            yPos = topMargin + printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

            count++;     //加一空行   

            str = DateTime.Now.ToString("yyyy.MM.dd   HH:mm:ss");
            yPos = topMargin + ((count++) * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

            //滚动6行   
            
            str = "   ";
            yPos = topMargin + printFont.GetHeight(ev.Graphics)*5;
            ev.Graphics.DrawString(str, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
            topMargin = yPos;

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Status != "审核")
                tsbDel.Enabled = true;
        }

        private void tsbSet_Click(object sender, EventArgs e)
        {
            fmSetPrinter prnset = new fmSetPrinter();
            prnset.OpName = "缝纫";
            prnset.Conn = conn;
            prnset.ShowDialog();
        }
    }
}
