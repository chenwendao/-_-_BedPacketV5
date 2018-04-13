using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CWD;
using CWD.DAL;
using CWD.Report;

namespace CWD
{
    public partial class ucBedPacket : UserControl
    {
        public string cSheetID;
        public int? iID = -1;
        public string cState;

        private SqlConnection ufconn;

        public ucBedPacket()
        {
            InitializeComponent();
            cSheetID = "";
            iID = null;
            cState = "";
        }       

        private void cwDepartment_Click(object sender, EventArgs e)
        {
            cwDepartment.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            //object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            object oj = SqlAccess.ExecuteScalar("SELECT cDepName FROM department where dDepEndDate is null and cDepCode='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                cwDepartment.Text = oj.ToString();
                cwDepartment.Value = input.cResult;
            }
        }

        private void cwOper1_Click(object sender, EventArgs e)
        {
            cwOper1.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                cwOper1.Text = oj.ToString();
                cwOper1.Value = input.cResult;
            }
        }

        private void cwOper2_Click(object sender, EventArgs e)
        {
            if (cwOper1.Text == "")
            {
                MessageBox.Show("请首先填充第一操作员！", "提示");
                return;
            }
            cwOper2.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                cwOper2.Text = oj.ToString();
                cwOper2.Value = input.cResult;
                if (cwOper2.Text == cwOper1.Text)
                {
                    MessageBox.Show("第二操作员不能和第一操作员相同！","提示");
                    cwOper2.Text = ""; cwOper2.Value = "";
                    return;
                }
            }
        }

        private void BedPacket_Load(object sender, EventArgs e)
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

            //Init();
            //EditSet(true);
            lbSheetID.Text = "";
            cwDepartment.Text = "";
            cwDepartment.Value = "";
            cwOper1.Text = Public.userName;
            cwOper1.Value = Public.userCode;
            cwOper2.Value = "";
            tsbVerion.Text = String.Format("版本 {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            SetbtnState("增加");

            
            ChangeSize();

            if (Public.cStatus != "独立程序")
                tsbExit.Visible = false;
            if (!AssemblyVersion.IsNewVer(Public.GetAssemblyShortName(),Public.getVersion(),ufconn) && (Public.IsAdmin || Public.cStatus == "独立程序"))
            {
                tsbVersion.Visible = true;
                //tsp.Enabled = true;
            }
            else if (!AssemblyVersion.IsNewVer(Public.GetAssemblyShortName(), Public.getVersion(), ufconn))
            {
                //tsp.Enabled = false;
                tsbVersion.Visible = false;
                MessageBox.Show("您的程序版本太旧，请更新程序！", "提示");
            }
            else
                tsbVersion.Visible = false;
        }

        private void SetbtnState(string Mode)
        {
            switch (Mode)
            {
                case "新单":
                    tsbNewsheet.Enabled = false;
                    tsbSave.Enabled = false;
                    tsbDel.Enabled = false;                    
                    tsbPrev.Enabled = false;
                    tsbNext.Enabled = false;
                    tsbCopy.Enabled = false;
                    tsbExit.Enabled = true;
                    //tsbAudit.Enabled = false;
                    //tsbUnAudit.Enabled = false;
                    btnAdd.Enabled = true;
                    break;
                case "保存":
                    tsbNewsheet.Enabled = true;
                    tsbSave.Enabled = false;
                    tsbDel.Enabled = false;
                    tsbPrev.Enabled = true;
                    tsbNext.Enabled = true;
                    tsbCopy.Enabled = false;
                    tsbExit.Enabled = true;
                    //tsbAudit.Enabled = true;
                    //tsbUnAudit.Enabled = false;
                    break;
                case "审核":
                    tsbNewsheet.Enabled = true;
                    tsbSave.Enabled = false;
                    tsbDel.Enabled = false;
                    tsbPrev.Enabled = true;
                    tsbNext.Enabled = true;
                    tsbCopy.Enabled = false;
                    tsbExit.Enabled = true;
                    //tsbAudit.Enabled = false;
                    //tsbUnAudit.Enabled = true;
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    break;
                //case "弃审":
                //    tsbNewsheet.Enabled = false;
                //    tsbSave.Enabled = false;
                //    tsbDel.Enabled = false;
                //    tsbPrev.Enabled = false;
                //    tsbNext.Enabled = false;
                //    tsbExit.Enabled = true;
                //    tsbAudit.Enabled = true;
                //    tsbUnAudit.Enabled = false;
                //    break;
            }
        }

        

        private void tsbNewsheet_Click(object sender, EventArgs e)
        {
            DataTable dt = SqlAccess.ExecuteSqlDataTable(@" SELECT b.IDS,b.PacketNo 包号,b.Sortseq 序号,i.cCusAbbName 客户简称,c.cInvDefine8 主面料,b.cInvcode 存货编码,c.cInvname+' '+c.cInvStd as 名称规格,
                cast(iQty as numeric(10,1)) 数量,
                d.cComUnitName 计量,e.cSocode+'_'+cast(e.irowno as varchar(3)) 销售订单,g.Mocode+'_'+cast(f.sortseq as varchar(3)) 生产订单,cast(f.Qty as numeric(10,1)) 计划,
                cast((select sum(iQty) from ekk..c_packets where modid=b.modid) as numeric(10,1)) 累计,'' as 整理组
                FROM EKK..C_Packet a LEFT JOIN EKK..C_Packets b ON a.ID=b.ID 
                LEFT JOIN Inventory c ON b.cInvcode=c.cInvcode 
                LEFT JOIN ComputationUnit d on c.cComUnitcode =d.cComUnitcode 
                LEFT JOIN So_Sodetails e ON b.SoDID=e.autoid
                left join Mom_orderdetail f ON b.MoDID=f.MoDID
                left join Mom_order g ON f.MoID=g.MoID
                left join So_SoMain h ON e.cSoCode=h.cSoCode
                left join Customer i ON h.cCuscode=i.cCuscode
                WHERE a.SheetID='' order by b.PacketNo,b.Sortseq", ufconn);
            dgv.DataSource=dt;
            dgvPacket.DataSource=null;
            cSheetID=null;
            lbSheetID.Text = "";
            iID=null;
            cState="新建";
            SetbtnState("新单");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cState != "新建")
                return;
            if (cwDepartment.Value == null || cwWhouse.Value == null)
            {
                MessageBox.Show("请完整填充部门和仓库名称！","提示");
                return;
            }

            if (cwDepartment.Value == "3001" || cwDepartment.Value == "3002" || cwDepartment.Value == "3003" ||cwDepartment.Value=="2104")//床品分公司
                Public.OperID=11;
            else if (cwDepartment.Value == "2305")//整理一车间
                Public.OperID = 12;
            else if (cwDepartment.Value == "2306")//整理二车间
                Public.OperID = 12;
            else if (cwDepartment.Value == "25")//制衣车间
                Public.OperID = 32;
            DetailData ddata = new DetailData();
            ddata.ufconn = ufconn;
            
            if (ddata.ShowDialog() != DialogResult.OK)
                return;
            //ddata.Dispose();
            string cMsg=CheckMom(ddata.iMoDID,ddata.dQty);
            if (cMsg!="")
            {
                Common.ShowMessage(cMsg+"不能再入库！", 2);
                return;                
            }
            dgvPacket.Rows.Add();
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["序号1"].Value=dgvPacket.Rows.Count;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["存货编码1"].Value=ddata.cInvcode;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["名称规格1"].Value=ddata.cPname;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["数量1"].Value=String.Format("{0:0.0}", ddata.dQty);
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["计量1"].Value=
                  SqlAccess.ExecuteScalar(@"select cComUnitName from ComputationUnit a left join Inventory b on a.cComUnitCode=b.cComUnitCode where cInvcode=" 
                  + Common.SqlParm(dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["存货编码1"].Value), ufconn);
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["生产订单1"].Value=ddata.cMocode;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["销售订单1"].Value=ddata.cSocode;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["预订单1"].Value = ddata.cSacode;//预订单
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["MoDID1"].Value=ddata.iMoDID;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["SoDID1"].Value = ddata.iSoDID;
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["sadid1"].Value = ddata.iSaDiD;//预订单id
            dgvPacket.Rows[dgvPacket.Rows.Count - 1].Cells["整理组1"].Value = ddata.cItem;
            tsbSave.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvPacket.CurrentCell.RowIndex < 0)
                return;
            dgvPacket.Rows.RemoveAt(dgvPacket.CurrentCell.RowIndex);
            btnDel.Enabled = false;
        }

        private void dgvPacket_MouseClick(object sender, MouseEventArgs e)
        {
            btnDel.Enabled = true;
        }
        private void ReflashSheet(string SheetID)
        {
            ClearCtrl();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select sheetid,id,cstate,a.Depcode,cDepName,oper1,a.Whcode,e.cWhName,c.cPsn_Name as Name1,oper2,d.cPsn_Name as Name2,dDate
                from ekk..c_packet a 
                left join Department b on a.Depcode=b.cDepcode 
                left join hr_hi_person c on a.oper1=c.cPsn_num 
                left join hr_hi_person d on a.oper2=d.cPsn_num 
                left join warehouse e on a.Whcode=e.cWhcode 
                where sheetid='" + SheetID + "'");
            DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);

            if (dt.Rows.Count == 1)
            {
                cSheetID = dt.Rows[0]["sheetid"].ToString();
                lbSheetID.Text = cSheetID;
                cwDepartment.Text = dt.Rows[0]["cDepName"].ToString();
                cwDepartment.Value = dt.Rows[0]["DepCode"].ToString();
                cwWhouse.Text = dt.Rows[0]["cWhName"].ToString();
                cwWhouse.Value = dt.Rows[0]["WhCode"].ToString();
                cwDepartment.Enabled = false;
                dtpCreate.Value = Convert.ToDateTime(dt.Rows[0]["dDate"]);
                cwOper1.Text = dt.Rows[0]["Name1"].ToString();
                cwOper1.Value = dt.Rows[0]["Oper1"].ToString();
                if (dt.Rows[0]["Oper2"] != DBNull.Value && dt.Rows[0]["Oper2"].ToString() != "")
                {
                    cwOper2.Text = dt.Rows[0]["Name2"].ToString();
                    cwOper2.Value = dt.Rows[0]["Oper2"].ToString();
                }
                iID = Convert.ToInt32(dt.Rows[0]["id"]);
                cState = dt.Rows[0]["cState"].ToString().Trim();
            }
            sb.Remove(0, sb.Length);
            sb.Append(@"SELECT b.IDS,b.PacketNo 包号,b.Sortseq 序号,i.cCusAbbName 客户简称,c.cInvDefine8 主面料,b.cInvcode 存货编码,c.cInvname+' '+c.cInvStd as 名称规格,
                cast(iQty as numeric(10,1)) 数量,b.sodid,b.modid,b.sadid,
                d.cComUnitName 计量,e.cSocode+'_'+cast(e.irowno as varchar(3)) 销售订单,g.Mocode+'_'+cast(f.sortseq as varchar(3)) 生产订单,cast(f.Qty as numeric(10,1)) 计划,
                cast((select sum(iQty) from ekk..c_packets where modid=b.modid) as numeric(10,1)) 累计,b.Item 整理组,k.ccode+'_'+cast(j.irowno as varchar(3))  预订单
                FROM EKK..C_Packet a LEFT JOIN EKK..C_Packets b ON a.ID=b.ID 
                LEFT JOIN Inventory c ON b.cInvcode=c.cInvcode 
                LEFT JOIN ComputationUnit d on c.cComUnitcode =d.cComUnitcode 
                LEFT JOIN So_Sodetails e ON b.SoDID=e.iSOsID 
                left join Mom_orderdetail f ON b.MoDID=f.MoDID
                left join Mom_order g ON f.MoID=g.MoID
                left join So_SoMain h ON e.cSoCode=h.cSoCode
                left join Customer i ON h.cCuscode=i.cCuscode
left join sa_PreOrderDetails j on b.sadid=j.autoid
left join SA_PreOrderMain k on j.id=k.id
                WHERE a.SheetID='" + SheetID + "' order by b.PacketNo,b.Sortseq");
            //dt.Clear();
            dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
            dgv.DataSource = dt;
            dgv.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["累计"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["计划"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.ReadOnly = true;
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    if (dt.Columns[i].DataType == System.Type.GetType("System.Decimal"))
            //        dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //}
            if (cState == "新建")
            {
                SetbtnState("保存");
                btnAdd.Enabled = true;
            }
            else if (cState == "审核")
            {
                SetbtnState("审核");
                btnAdd.Enabled = false;
            }
            if (cwDepartment.Value == "3001" || cwDepartment.Value == "3002" || cwDepartment.Value == "3003")//床品分公司
                Public.OperID = 11;
            else if (cwDepartment.Value == "2305" || cwDepartment.Value == "2306")//整理车间
                Public.OperID = 12;
            tsbPrintBIG.Enabled = false;
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {            
            if (dgv.CurrentCell.RowIndex < 0)
                return;
            if (cState == "审核")
            {
                MessageBox.Show("单据已经审核，不能删除！","提示");
                return;
            }
            if (!dgv.Focused)
            {
                MessageBox.Show("删除前请选择主表行！", "提示");
                return;
            }
            //DialogResult cResult = MessageBox("系统将删除当前记录，您确认要继续吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Show();
            if (MessageBox.Show("系统将删除当前记录，您确认要继续吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            StringBuilder sb = new StringBuilder();

            sb.Append(" delete from ekk..c_packets where ids="+dgv.Rows[dgv.CurrentCell.RowIndex].Cells["IDS"].Value.ToString());
            if (dgv.Rows.Count == 1)
                sb.Append(" delete from ekk..c_packet where sheetid='" + cSheetID + "'");
            SqlTransaction tran = ufconn.BeginTransaction();
            try
            {
                SqlAccess.ExecuteSql(sb.ToString(), tran);
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Common.ShowMessage(ex.Message,2);
                return;
            }
            tsbDel.Enabled = false;
            if (dgv.Rows.Count == 1)
                cSheetID = GetNextSheet(cSheetID);
            ReflashSheet(cSheetID);
        }

        private string GetNextSheet(string SheetID)
        {
            StringBuilder sb = new StringBuilder();
            if(SheetID!="")
                sb.Append(" select min(sheetid) from ekk..c_packet where sheetid>'" + SheetID + "'");
            else
                sb.Append(" select max(sheetid) from ekk..c_packet where substring(sheetid,1,8)="+Common.SqlParm(dtpCreate.Value.ToString("yyyyMMdd")));
            object oj=SqlAccess.ExecuteScalar(sb.ToString(), ufconn);
            if (oj != null)
                return oj.ToString();
            else
                return "";
        }
        private string GetPrevSheet(string SheetID)
        {
            StringBuilder sb = new StringBuilder();
            if (SheetID != "")
                sb.Append(" select max(sheetid) from ekk..c_packet where sheetid<'" + SheetID + "'");
            else
                sb.Append(" select min(sheetid) from ekk..c_packet where substring(sheetid,1,8)=" + Common.SqlParm(dtpCreate.Value.ToString("yyyyMMdd")));

            object oj = SqlAccess.ExecuteScalar(sb.ToString(), ufconn);
            if (oj != null)
                return oj.ToString();
            else
                return "";
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (cSheetID != null && GetVouchState(cSheetID) == "审核")
            //if (cState == "审核")
            {
                MessageBox.Show("单据已经审核，不能增加！", "提示");
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (cSheetID == null)
            {
                string cResult = ValidData();
                if (cResult != "")
                {
                    MessageBox.Show(cResult, "数据检查");
                    return;
                }
                cSheetID = GetNewSheetID();
            }
            C_Packet packet = new C_Packet();
            packet.SheetID = cSheetID;
            if(iID!=null)
                packet.ID = (int)iID;
            packet.Creater = Public.userName;
            packet.dDate = DateTime.Today;
            packet.cState = "新建";
            packet.Oper1 = cwOper1.Value;
            packet.Oper2 = cwOper2.Value;
            packet.Oper3 = cwOper3.Value;
            packet.DepCode = cwDepartment.Value;
            packet.Whcode = cwWhouse.Value;
           
            string cPacketno = GetPacketNo(cwDepartment.Value);
            List<C_Packets> lstPackets = new List<C_Packets>();
            for (int i = 0; i < dgvPacket.Rows.Count; i++)
            {
                C_Packets pack = new C_Packets();
                pack.cInvcode = dgvPacket.Rows[i].Cells["存货编码1"].Value.ToString();
                pack.iQty = Common.GetNum(dgvPacket.Rows[i].Cells["数量1"].Value);                
                pack.SoDID = Common.GetInt(dgvPacket.Rows[i].Cells["SoDID1"].Value);
                pack.MoDID = Common.GetInt(dgvPacket.Rows[i].Cells["MoDID1"].Value);
                pack.PacketNo = cPacketno;
                pack.SortSeq = i + 1;
                pack.Item = dgvPacket.Rows[i].Cells["整理组1"].Value.ToString();
                pack.sadid = Common.GetInt(dgvPacket.Rows[i].Cells["SaDID1"].Value);
                
                lstPackets.Add(pack);
            }
            
            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }
            SqlTransaction tran = ufconn.BeginTransaction();
            try
            {
                if (packet.ID <= 0)
                    packet.Add(tran);
                for (int i = 0; i < lstPackets.Count; i++)
                {
                    lstPackets[i].ID = packet.ID;
                    lstPackets[i].Add(tran);
                }
               
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show(ex.Message, "提示");
                return;
            }
            SetbtnState("保存");
            int icount = dgvPacket.Rows.Count;
            for(int i=0;i<icount;i++)
                dgvPacket.Rows.RemoveAt(0);
            ReflashSheet(cSheetID);
            if (rdA5.Checked)
                printA5(cPacketno);
            else if (rdPaperBox.Checked)
                printPaperBox(cPacketno);
            dgv.Rows[dgv.Rows.Count - 1].Selected=true;
            dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells["包号"];
            tsbPrintBIG.Enabled = true;
        }
        private string GetVouchState(string sheetid)
        {
            C_Packet packet = new C_Packet(sheetid, ufconn);
            return packet.cState;
        }
        private string GetNewSheetID()
        {
            StringBuilder sb = new StringBuilder();
            string cDate = DateTime.Now.ToString("yyyyMMdd");
            sb.Append(" select top 1 sheetid from ekk..c_packet where substring(sheetid,1,8)='"+cDate+"' order by sheetid desc");
            object oj=SqlAccess.ExecuteScalar(sb.ToString(),ufconn);
            if (oj != null)
            {
                sb.Remove(0, sb.Length);
                sb.Append(" select max(sheetid) from ekk..c_packet where substring(sheetid,1,8)='" + cDate + "' ");
                string cID = SqlAccess.ExecuteScalar(sb.ToString(), ufconn).ToString();
                return cID.Substring(0, 8) + String.Format("{0:00}", Convert.ToInt16(cID.Substring(8, 2)) + 1);
            }
            else
                return cDate + "01";
        }
        private string GetPacketNo(string cDepID)
        {
            StringBuilder sb = new StringBuilder();
            string cDepcode = "";
            if (cDepID == "3001" || cDepID == "3002" || cDepID == "3003")//床品分公司
                cDepcode = "B";
            else if (cDepID == "2305")//整理一车间
                cDepcode = "A";
            else if (cDepID == "2306")//整理二车间
                cDepcode = "C";
            else if (cDepID == "2104")//餐饮部
                cDepcode = "D";
            else if (cDepID == "25")//部门
                cDepcode = "E";
            else
                return "";

            sb.Append("select max(packetno) from ekk..c_packets a left join ekk..c_packet b on a.id=b.id where substring(packetno,1,5)='" + 
                DateTime.Now.Year.ToString().Substring(3, 1) + getMonth(DateTime.Now.Month) + 
                String.Format("{0:00}", DateTime.Now.Day) + cDepcode + "'");
            object oj = SqlAccess.ExecuteScalar(sb.ToString(), ufconn);
            if (oj != DBNull.Value)
                return oj.ToString().Substring(0, 5) + String.Format("{0:000}", Convert.ToInt16(oj.ToString().Substring(5, 3)) + 1);
            else
            {
                return DateTime.Now.Year.ToString().Substring(3, 1) + getMonth(DateTime.Now.Month) +String.Format("{0:00}",DateTime.Now.Day)+ cDepcode+"001";
            }
        }
        private string getMonth(int Month)
        {
            if(Month<1)
                return "";
            switch(Month)
            {
                case 1:case 2: case 3: case 4:case 5:case 6:case 7:case 8:case 9:
                    return Month.ToString();
                    
                case 10:
                    return "A";
                    
                case 11:
                    return "B";
                    
                case 12:
                    return "C";
                    
            }
            return "";
        }

        private void tsbPrev_Click(object sender, EventArgs e)
        {
            ReflashSheet(GetPrevSheet(cSheetID));
        }

        private void tsbNext_Click(object sender, EventArgs e)
        {
            ReflashSheet(GetNextSheet(cSheetID));
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            fmPacket test = (fmPacket)this.Parent;

            if (ufconn.State.ToString().ToLower() != "close")
                ufconn.Close();
            this.Dispose();
            test.Close();
        }
        private string ValidData()
        {
            if (cwDepartment.Value == "")
                return "请录入部门！";
            if (cwOper1.Value == "")
                return "请录入操作员！";
            else
                return "";
            //if (dgv.Rows.Count == 0)
            //    return "请录入成包数据！";
            //else
            //    return "";
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            int Rowno = dgv.CurrentCell.RowIndex;
            string cPacketCode = Convert.ToString(dgv.Rows[Rowno].Cells["包号"].Value).Trim();
            printPaperBox(cPacketCode);            
        }

        private void printPaperBox(string packetNo)
        {
            string prnName = "";
            List<C_PrinterSet> lstPrinter = C_PrinterSet.getModelPrinter("打包", Public.userCode, ufconn);
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
                Common.ShowMessage("请设置打印机!", 2);
                return;
            }
            DBTab ds = new DBTab();
            try
            {
                ds.Tables["Tab"].Clear();

                StringBuilder sb = new StringBuilder();
                sb.Append(@" select cus.ccusabbname,cus.ccusaddress,so.csocode,sd.irowno, a.cinvcode,inv.cinvname,
                    inv.cinvstd,cast(a.iqty as numeric(10,0)) as iQty,unit.ccomunitname,inv.cInvDefine1,
                    (select top 1 cinvname from inventory inv left join mom_moallocate loc on inv.cinvcode=loc.invcode where substring(cinvcode,1,2)='13' and loc.modid=a.modid) Material
                    from ekk..c_packets a 
                    left join  ekk..c_packet b on a.id=b.id
                    left join inventory inv on a.cinvcode=inv.cinvcode
                    left join computationunit unit on inv.ccomunitcode=unit.ccomunitcode
                    left join so_sodetails sd on a.sodid=sd.autoid
                    left join so_somain so on sd.csocode=so.csocode
                    left join customer cus on so.ccuscode=cus.ccuscode
                    where packetno=" + Common.SqlParm(packetNo));
                DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
                if (dt.Rows.Count == 0)
                    return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = ds.Tables["Tab"].NewRow();
                    dr["Tabcode"] = "*" + packetNo + "*";
                    dr["cusname"] = dt.Rows[i]["ccusabbname"].ToString();
                    dr["Address"] = dt.Rows[i]["ccusaddress"].ToString();
                    dr["Ordercode"] = dt.Rows[i]["csocode"].ToString() + "-" + dt.Rows[i]["irowno"].ToString();
                    string cInvname = dt.Rows[i]["cinvname"].ToString();
                    if (cInvname.IndexOf("-") > 0)
                    {
                        cInvname = cInvname.Substring(cInvname.IndexOf("-") + 1, cInvname.Length - cInvname.IndexOf("-") - 1);
                    }
                    dr["cInvCode1"] = dt.Rows[i]["cinvcode"].ToString();
                    dr["cInvName1"] = cInvname;
                    dr["cInvstd1"] = dt.Rows[i]["cinvstd"].ToString().Trim() + " " + dt.Rows[i]["cInvDefine1"].ToString().Trim();
                    dr["Qty1"] = dt.Rows[i]["iqty"].ToString() + " 条";
                    dr["unit1"] = dt.Rows[i]["ccomunitname"].ToString();
                    dr["Material"] = dt.Rows[i]["Material"].ToString();
                    if (dt.Rows.Count > 1)
                    {
                        int x = i + 1;
                        dr["Rowno"] = "拼包" + x.ToString();
                    }
                    if (dr == null)
                        return;
                    ds.Tables["Tab"].Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("提取数据出错，可能的原因是：" + ex.Message.ToString(), "提示");
            }

            if (ds.Tables["Tab"].Rows.Count < 1)
            {
                MessageBox.Show("没有数据！", "提示");
                return;
            }

            CRTab5015 crtab = new CRTab5015();
            
            crtab.SetDataSource(ds);

            //crtab.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
            //System.Drawing.Printing.PageSettings df = new System.Drawing.Printing.PageSettings();
            //df.PaperSize = new crtab.PrintOptions.PaperSize("new size", 394, 285);
            //CrystalDecisions.Shared.PaperSize.DefaultPaperSize = df;


            ////新增打印纸张 来源打印机默认大小   需要设置打印机默认大小为：100mm * 100mm
            //crtab.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

            //frmReport rep = new frmReport();
            //rep.crystalReportViewer1.ReportSource = crtab;
            //rep.ShowDialog();            
            crtab.PrintOptions.PrinterName = prnName;
            crtab.PrintToPrinter(1, true, 1, ds.Tables["Tab"].Rows.Count);
        }

        private void tsbAudit_Click(object sender, EventArgs e)
        {
             C_ModelUser user=new C_ModelUser(Public.userCode,Public.GetAssemblyShortName(), ufconn);
             if (user.Role != 9)
             {
                 Common.ShowMessage("您没有审核打包单权限！", 2);
                 return;
             }
               
            if (cState != "新建")
                return;
            //if (Public.bAudit)
            {
                string cMsg=Audit(cSheetID);
                if (cMsg != "")
                {
                    MessageBox.Show(cMsg, "提示");
                    return;
                }
                else
                {
                    MessageBox.Show("操作已完成，请到系统中打印最新产成品入库单（未审核）！", "提示");
                }
                SetbtnState("审核");
            }
        }

        private void tsbUnAudit_Click(object sender, EventArgs e)
        {
            if (cState != "审核")
                return;
            C_ModelUser user = new C_ModelUser(Public.userCode, Public.GetAssemblyShortName(), ufconn);
            if (user.Role != 9)
            {
                Common.ShowMessage("您没有审核打包单权限！", 2);
                return;
            }
            //if (Public.bAudit)
            {

                string cMsg = UnAudit(cSheetID);
                if (cMsg != "")
                {
                    MessageBox.Show(cMsg, "提示");
                    return;
                }
                SetbtnState("保存");
            }
        }

        private int getResId(string empcode, SqlTransaction tran)
        {
            int resId = 0;
            object oj = SqlAccess.ExecuteScalar("select ResId from sfc_resource where Rescode='" + empcode + "' ", tran);
            if (oj != null)
                return Common.GetInt(oj);
            else
            {
                sfc_resource res = new sfc_resource();
                res.ResCode = empcode;
                res.ResType = 0;
                res.VTid = 30375;
                res.CreateDate = DateTime.Today;
                res.CreateTime = DateTime.Now;
                res.CreateUser = Public.userName;
                res.ResId = sfc_resource.GetMaxId(tran) + 1;
                res.Add(tran);
                return res.ResId;
            }
        }
        private string Audit(string SheetID)
        {
            //填写报工单
            //填写入库单
            StringBuilder sb = new StringBuilder();
            
            C_Packet packet = new C_Packet(SheetID, ufconn);
            if (packet.cState == "审核")
                return string.Format("当前单据已经被{0}审核！", packet.Auditer);
            if (packet.Oper2 == "" && !Common.ShowConfirm("当前包装单只有一名操作员，您确认要继续审核吗？"))
            {
                return "审核放弃！";
            }


            RdRecord rd = new RdRecord();
            fc_MoRoutingBill roubill= new fc_MoRoutingBill();
            
            List<int> RdID = new List<int>();
            int iMID = roubill.GetNewId(ufconn);

            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }
            rd.bRdFlag = 1;
            rd.cVouchType = "10";
            rd.cBusType = "成品入库";
            rd.cSource = "生产订单";
            rd.cBusCode = SheetID;
            rd.cWhCode = cwWhouse.Value;
            rd.dDate = DateTime.Today;
            string rdSheetID = rd.GetCode("10", ufconn);
            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }

            rd.GetMaxId(ufconn, packet.Items.Count, RdID);            
            rd.ID = RdID[0]+1;                                 
            rd.cCode = rdSheetID;
            if (cwDepartment.Value == "3001" || cwDepartment.Value == "3002" || cwDepartment.Value == "3003")
                rd.cRdCode = "16";
            else if (cwDepartment.Value == "2305" || cwDepartment.Value == "2306")
                rd.cRdCode = "13";
            else if (cwDepartment.Value == "2104")
                rd.cRdCode = "15";


            rd.cDepCode = cwDepartment.Value;
            rd.cMemo = "包装实时生成";
            rd.bTransFlag = false;
            rd.cMaker = Public.userName;
            rd.dnmaketime = DateTime.Now;
            rd.iNetLock = 0;
            rd.VT_ID = 31094;

            roubill.MID = iMID;
            roubill.cVouchDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            roubill.cVouchTime = DateTime.Now;
            roubill.CreateUser = Public.userName;
            roubill.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            roubill.CreateTime = DateTime.Now;
            string mrSheetID = roubill.GetCode(ufconn);
            roubill.cVouchCode = mrSheetID;
            roubill.IsSingle = 1;
            if (cwDepartment.Value == "3001" || cwDepartment.Value == "3002" || cwDepartment.Value == "3003" )
            roubill.WcId = 4;
            else if (cwDepartment.Value == "2305" || cwDepartment.Value == "2306")
            roubill.WcId = 5;
            else if (cwDepartment.Value == "25")
            roubill.WcId = 3;
            roubill.Remark = "包装实时录入";
            roubill.VT_ID = 31062;
            if (roubill.Items == null)
            roubill.Items = new List<fc_MoRoutingBilldetail>();

            //int i = 0;
            //foreach(DictionaryEntry var in moscount)
            Hashtable hashMos = new Hashtable();
            Hashtable hashMo = new Hashtable();
            Hashtable hashSos = new Hashtable();
            Hashtable hashMrs = new Hashtable();
            Hashtable hashSas = new Hashtable();
            Hashtable hashSa = new Hashtable();
            for(int i=0;i<packet.Items.Count;i++)
            {
                if(hashMos[packet.Items[i].MoDID]==null)
                    hashMos.Add((int)packet.Items[i].MoDID,new mom_orderdetail((int)packet.Items[i].MoDID,ufconn));
                mom_orderdetail mos = (mom_orderdetail)hashMos[(int)packet.Items[i].MoDID];
                if(hashMo[(int)mos.MoId]==null)
                    hashMo.Add((int)mos.MoId,new mom_order((int)mos.MoId,ufconn));
                
                mom_order mo =  (mom_order)hashMo[(int)mos.MoId];
                SO_SODetails sos=null;
                SA_PreOrderDetails sas=null;
                SA_PreOrderMain sa=null;
                if(Common.GetInt(mos.SoDId)>0)
                {
                    if(hashSas[mos.SoDId.ToString()]==null)
                        hashSas.Add(mos.SoDId.ToString(),new SA_PreOrderDetails(Common.GetInt(mos.SoDId),ufconn));
                    sas=(SA_PreOrderDetails)hashSas[mos.SoDId.ToString()];
                    if(hashSa[sas.autoid]==null)
                        hashSa.Add((int)sas.autoid,new SA_PreOrderMain((int)sas.autoid,ufconn));
                    sa=(SA_PreOrderMain)hashSa[(int)sas.autoid];
                }
                else
                {
                    int sodid=Common.GetInt(SqlAccess.ExecuteScalar("select autoid from so_sodetails where isosid="+mos.OrderDId,ufconn));
                    if(hashSos[sodid]==null)
                        hashSos.Add(sodid,new SO_SODetails(sodid,ufconn));
                    sos=(SO_SODetails)hashSos[sodid];
                }                
                if(hashMrs[(int)mos.MoDId]==null)
                    hashMrs.Add((int)mos.MoDId,new sfc_moroutingdetail("打包",(int)mos.MoDId,ufconn));
                sfc_moroutingdetail mrs=(sfc_moroutingdetail)hashMrs[(int)mos.MoDId];
                //sfc_moroutingdetail mrs = new sfc_moroutingdetail("打包", (int)mos.MoDId, ufconn);
                RdRecords rds = new RdRecords();
                fc_MoRoutingBilldetail bills = new fc_MoRoutingBilldetail();
                Boolean bLastflag =mrs.LastFlag;
                rds.AutoID =RdID[1];
                RdID[1]++;
                rds.ID = rd.ID;
                rds.iQuantity = packet.Items[i].iQty;//这里是本次入库数量
                rds.cInvCode = mos.InvCode;

                if (sos != null && sos.cSOCode!=null && sos.cSOCode!="")
                { rds.cBatch =sos.cSOCode.Trim() + "-" +sos.iRowNo; }
                else if (sas!=null)                        
                {                         
                    rds.cBatch =sa.cCode + "-" +sas.iRowNo;
                }
                else
                {                        
                    rds.cBatch =mo.MoCode + "-" +mos.SortSeq;
                }
                rds.cmocode =mo.MoCode;
                rds.imoseq =mos.SortSeq;
                rds.iordercode = sos != null ? sos.cSOCode : (sas == null ? "" : sa.cCode);
                rds.iorderdid = sos == null ? 0 : sos.iSOsID;
                rds.iMPoIds =mos.MoDId;
                rds.iorderseq =sos!=null?sos.iRowNo:0;
                rds.cDefine22 = mos.OrgQty.ToString();
                rds.cDefine23 +=hr_hi_person.GetName(packet.Oper1,ufconn)+ "," +hr_hi_person.GetName(packet.Oper2,ufconn);
                rds.cDefine23 = rds.cDefine23.TrimEnd(',');
                rds.ID = rd.ID;
                rds.iFlag = 0;
                rds.iTax = 0;
                rds.iMoney = 0;
                rds.iSQuantity = 0;
                rds.iSendQuantity = 0;
                rds.iSendNum = 0;
                rds.iTaxRate = 0;
                rds.iMaterialFee = 0;
                rds.iSumBillQuantity = 0;
                rds.bVMIUsed = false;
                rds.cBatchProperty6=packet.Items[i].PacketNo;
                rds.irowno = i + 1;
                rd.rdrecords.Add(rds);

                bills.MID = roubill.MID;
                bills.MoDId = mos.MoDId;
                bills.MoId = mos.MoId;
                bills.WcId = mrs.WcId;
                bills.MoRoutingDId = mrs.MoRoutingDId;
                bills.MoRoutingShiftId = 0;
                bills.OpSeq = mrs.OpSeq;
                bills.OpCode =new sfc_operation((int)mrs.OperationId,ufconn).OpCode;
                bills.opDescription =mrs.Description;
                bills.EmployCode = cwOper1.Value.ToString();
                bills.DeptCode = cwDepartment.Value.ToString();
                bills.ScrapQty = 0;
                bills.fAvaQuantity = (decimal)packet.Items[i].iQty;
                bills.InChangeRate = 1;
                bills.QualifiedQty = (decimal)packet.Items[i].iQty;
                bills.ActualStartDate = DateTime.Now;
                bills.ActualStartTime = DateTime.Now;
                bills.ActualDueDate= DateTime.Now;
                bills.ActualDueTime = DateTime.Now;
                bills.WsCode = "日班";
                bills.WorkShiftId = 0;
                bills.Define26 = 1.75M;
                bills.VerifierDate = DateTime.Now;
                bills.VerifierTime = DateTime.Now;
                bills.Status = 1;

                roubill.Items.Add(bills);                     
            }
            int opernum=0;
            if (Common.GetStr(packet.Oper1) != "")
                opernum ++;
            if(Common.GetStr(packet.Oper2)!="")
                opernum++;
            if (Common.GetStr(packet.Oper3) != "")
                opernum++;
            SqlTransaction tran = ufconn.BeginTransaction();
            try
            {
                rd.Add(tran);
                roubill.Add(tran);
                for (int j = 0; j < roubill.Items.Count; j++)
                {
                    fc_MoRoutingBilldetail.ModiMorout((int)roubill.Items[j].MoRoutingDId, roubill.Items[j].QualifiedQty, true, tran);
                    fc_MoRoutingbillRes res = new fc_MoRoutingbillRes();
                    res.MRID = res.GetMaxId(tran) + 1;
                    res.MDId = roubill.Items[j].MDId;
                    res.ResId = getResId(packet.Oper1, tran);
                    res.ResWorkHr = Common.GetNum(roubill.Items[j].QualifiedQty)/opernum;
                    res.Add(tran);
                    if (opernum == 2)
                    {
                        res.MRID = res.GetMaxId(tran) + 1;
                        res.MDId = roubill.Items[j].MDId;
                        res.ResId = getResId(packet.Oper2, tran);
                        res.ResWorkHr = Common.GetNum(roubill.Items[j].QualifiedQty) / opernum;
                        res.Add(tran);
                    }
                    if (opernum == 3)
                    {
                        res.MRID = res.GetMaxId(tran) + 1;
                        res.MDId = roubill.Items[j].MDId;
                        res.ResId = getResId(packet.Oper3, tran);
                        res.ResWorkHr = Common.GetNum(roubill.Items[j].QualifiedQty) / opernum;
                        res.Add(tran);
                    }
                }
                object oj = SqlAccess.ExecuteScalar("select max(iFatherId) from UFSystem..UA_Identity where cAcc_Id = '" + Public.cacc_id + "' and cVouchType='rd'", tran);
                int MaxID = Common.GetInt(oj);
                if (MaxID < rd.ID)
                    SqlAccess.ExecuteSql("update UFSystem..UA_Identity set iFatherId = " + rd.ID + " where cAcc_Id='" + Public.cacc_id + "' and cVouchType='rd'", tran);
                oj = SqlAccess.ExecuteScalar("select max(iChildId) from UFSystem..UA_Identity where cAcc_Id = '" + Public.cacc_id + "' and cVouchType='rd'", tran);
                int MaxAutoID = Common.GetInt(oj);

                if (MaxAutoID < rd.MaxAutoID())
                    SqlAccess.ExecuteSql("update UFSystem..UA_Identity set iChildId = " + rd.MaxAutoID() + " where cAcc_Id='" + Public.cacc_id + "' and cVouchType='rd'", tran);
                //取得CardNumbe
                string cardnumber = Common.GetStr(SqlAccess.ExecuteScalar("select CardNumber from VoucherNumber where CardName='产成品入库单' and AppName='库存管理'", tran));

                if (Common.GetInt(SqlAccess.ExecuteScalar("select count(1) from VoucherHistory where CardNumber='" + cardnumber + "' and cSeed='" + rd.dDate.ToString("yyMM") + "'", tran)) == 0)
                    SqlAccess.ExecuteSql(@" INSERT INTO VoucherHistory (CardNumber,cContent,cContentrule,cSeed,cNumber,bEmpty) 
                        VALUES ('" + cardnumber + "','日期','月','" + rd.dDate.ToString("yyMM") + "','1',0)", tran);
                else
                    SqlAccess.ExecuteSql(" update VoucherHistory SET cNumber='" + Common.GetInt(rd.cCode.Substring(rd.cCode.Length - 4, 4)) + "' where CardNumber='" + cardnumber + "' and cSeed='" + rd.dDate.ToString("yyMM") + "'", tran);
                packet.MID = roubill.MID;
                packet.RDID = rd.ID;
                packet.Auditer = Public.userName;
                packet.AuditDate = DateTime.Today;
                packet.cState = "审核";
                packet.Update(tran);                
                tran.Commit();
                cState = "审核";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return ex.Message;
            }
            return "";
        }
        private string UnAudit(string SheetID)
        {
            C_Packet packet = new C_Packet(SheetID, ufconn);
            if (packet.Items.Count == 0)
                return "单据无表体";
            if (packet.Auditer==null && packet.Auditer=="")
                return "单据未审核，不能弃审！";
            int iMid = (int)packet.MID;
            int iRdid = (int)packet.RDID;
            RdRecord rd = new RdRecord(iRdid, ufconn);
            if (rd.cHandler != null && rd.cHandler != "")
                return "对应入库单已审核，不能弃审！";
            fc_MoRoutingBill bill = new fc_MoRoutingBill(iMid, ufconn);
            
            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }

            SqlTransaction tran = ufconn.BeginTransaction();
            try
            {
                for (int i = 0; i < bill.Items.Count; i++)
                {
                    SqlAccess.ExecuteSql("delete from fc_moroutingbillres where mdid=" + bill.Items[i].MDId, tran);
                    fc_MoRoutingBilldetail.ModiMorout((int)bill.Items[i].MoRoutingDId, bill.Items[i].QualifiedQty, false, tran);
                    bill.Items[i].Delete(tran);
                }
                
                rd.Delete(tran);
                bill.Delete(tran);
                packet.MID = null;
                packet.RDID = null;
                packet.Auditer = null;
                packet.AuditDate = null;
                packet.cState = "新建";
                packet.Update(tran);
                tran.Commit();
                cState = "新建";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return ex.Message;
            }
            return "";
        }

        private void tsbVersion_Click(object sender, EventArgs e)
        {
            if (!AssemblyVersion.IsNewVer(Public.GetAssemblyShortName(), Public.getVersion(), ufconn))
            {
                AssemblyVersion.AddNewVer(Public.GetAssemblyShortName(),Public.getVersion(),Public.userName,ufconn);
                tsbVersion.Visible = false;
            }
        }

        private void cwWhouse_Click(object sender, EventArgs e)
        {
            cwWhouse.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            //object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            object oj = SqlAccess.ExecuteScalar("SELECT cWhName FROM WareHouse where cWhCode='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                cwWhouse.Text = oj.ToString();
                cwWhouse.Value = input.cResult;
            }
        }
        private void ClearCtrl()
        {
            cSheetID ="";
            lbSheetID.Text ="";
            cwDepartment.Text ="";
            cwDepartment.Value = "";
            cwWhouse.Text = "";
            cwWhouse.Value = "";
            cwDepartment.Enabled = false;
            dtpCreate.Value = DateTime.Now;
            cwOper1.Text = "";
            cwOper1.Value = "";
            cwOper2.Text = "";
            cwOper2.Value = "";
            
            iID = null;
            cState = "";
            //dgv.DataSource = null;
            dgv.ReadOnly = true;            
        }
        private string CheckMom(int? modid, decimal qty)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select isnull(define22,'') define22,isnull(QualifiedInQty ,0) inqty from mom_orderdetail where modid=" + modid);
            DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
            decimal inqty = Common.GetNum(dt.Rows[0]["inqty"]);
            string warp=Common.GetStr(dt.Rows[0]["define22"]).Trim().Replace("%","");
            int maxval=0;
            if(warp!="" && (warp.Substring(0,1)=="+" || warp.Substring(0,1)=="±") && Common.IsDecimal(warp.Substring(1,warp.Length-1)))
                maxval=Common.GetInt(warp.Substring(1,warp.Length-1));
            else if (warp != "" && warp.Substring(0, 1) == "-" && Common.IsDecimal(warp.Substring(1, warp.Length - 1)))
            {
                maxval = 0;
            }
            else if (Common.IsDecimal(warp))
                maxval = Common.GetInt(warp);
            sb.Remove(0, sb.Length);
            sb.Append("select qty from mom_orderdetail where modid=" + modid);
            decimal planqty = Convert.ToDecimal(SqlAccess.ExecuteScalar(sb.ToString(), ufconn));
            sb.Remove(0, sb.Length);
            sb.Append("select sum(iQty) from ekk..c_packets a left join ekk..c_packet b on a.id=b.id where b.cstate='新建' and a.modid=" + modid);
            decimal uninqty =Common.GetNum(SqlAccess.ExecuteScalar(sb.ToString(), ufconn));
            for (int i = 0; i < dgvPacket.Rows.Count; i++)
            {
                if (Common.GetInt(dgvPacket.Rows[i].Cells["modid1"].Value) == modid)
                    uninqty += Common.GetNum(dgvPacket.Rows[i].Cells["数量1"].Value);
            }
            if (planqty*(1.0M+(decimal)maxval/100) - inqty - uninqty-qty < 0)
            {
                string Msg=string.Format("订单计划{0:0}，上偏差{1}，已入库{2:0}，今天入库{3:0}，本次入库{4:0}，已超入库范围",planqty,maxval,inqty,uninqty,qty);
                return Msg;
            }
            return "";
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            if (cState != "新建")
                return;
            int iRow = dgv.CurrentCell.RowIndex;
            string oldPacketno=dgv.Rows[iRow].Cells["包号"].Value.ToString();
            
            StringBuilder sb = new StringBuilder();            
            string cPacketno = GetPacketNo(cwDepartment.Value);
            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells["包号"].Value.ToString() == oldPacketno)
                {
                    string Msg = CheckMom(Common.GetInt(dgv.Rows[iRow].Cells["MoDID"].Value), Common.GetNum(dgv.Rows[iRow].Cells["数量"].Value));
                    if (Msg != "")
                    {
                        Common.ShowMessage(Msg + ",不能复制！", 2);
                        SetbtnState("保存");
                        ReflashSheet(cSheetID);
                        dgv.Rows[dgv.Rows.Count - 1].Selected = true;
                        dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells["包号"];
                        tsbPrintBIG.Enabled = true;
                        return;
                    }
                    SqlTransaction tran = ufconn.BeginTransaction();
                    try
                    {
                        sb.Append(" INSERT INTO EKK..C_Packets (ID,cInvcode,iQty,SoDiD,MoDID,PacketNo,Item,Sortseq,sadid) VALUES (");
                        sb.Append(iID + "," + Common.SqlParm(dgv.Rows[i].Cells["存货编码"].Value));
                        sb.Append("," + dgv.Rows[i].Cells["数量"].Value.ToString());
                        sb.Append("," + Common.GetNum(dgv.Rows[i].Cells["SoDID"].Value));
                        sb.Append("," + Common.GetNum(dgv.Rows[i].Cells["MoDID"].Value));
                        sb.Append("," + Common.SqlParm(cPacketno));
                        sb.Append("," + Common.GetInt(dgv.Rows[i].Cells["整理组"].Value));
                        sb.Append("," +(Common.GetInt(dgv.Rows[i].Cells["序号"].Value)+1));
                        sb.Append("," + Common.GetNum(dgv.Rows[i].Cells["sadid"].Value) + ")");
                        SqlAccess.ExecuteSql(sb.ToString(), tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show(ex.Message, "提示");
                    }
                    SetbtnState("保存");
                    ReflashSheet(cSheetID);
                    dgv.Rows[dgv.Rows.Count - 1].Selected = true;
                    dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells["包号"];
                    tsbPrintBIG.Enabled = true;
                    if (rdA5.Checked)
                        printA5(cPacketno);
                    else if (rdPaperBox.Checked)
                        printPaperBox(cPacketno);
                    break;
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (cState == "新建")
            {
                tsbDel.Enabled = true;
                tsbCopy.Enabled = true;
                tsbMuitcopy.Enabled = true;
            }
            tsbPrintA5.Enabled = true;
            tsbPrintBIG.Enabled = true;
        }

        private void tsbPrint214_Click(object sender, EventArgs e)
        {
            int Rowno = dgv.CurrentCell.RowIndex;
            string cPacketCode = Convert.ToString(dgv.Rows[Rowno].Cells["包号"].Value).Trim();
            DBTab ds = getTabDb(cPacketCode);            

            CRTab crtab = new CRTab();
            crtab.PrintOptions.PrinterName = "TSC TDP-245";
            crtab.SetDataSource(ds);
            crtab.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
            //frmReport rep = new frmReport();
            //rep.crystalReportViewer1.ReportSource = crtab;
            //rep.ShowDialog();

            crtab.PrintToPrinter(1, true, 1, ds.Tables["Tab"].Rows.Count);   
        }
        private DBTab getTabDb(string cPacketCode)
        {
            DBTab ds = new DBTab();
            try
            {
                int row = dgv.CurrentCell.RowIndex;
                ds.Tables["Tab"].Clear();

                StringBuilder sb = new StringBuilder();
               
                sb.Append(@"select isnull(soex.cdefine5,cus.ccusabbname) printcustom,cus.ccusabbname ccusabbname,
cus.ccusaddress,case when soex.cdefine1 is null then so.csocode+'-'+cast(sd.irowno as varchar(5)) 
else soex.cdefine1+'('+so.csocode+'-'+cast(sd.irowno as varchar(5))+')' end as csocode,cus.ccusdefine4,
sd.isosid,sd.irowno,a.cinvcode,isnull(soex.cdefine2,inv.cinvname) as cinvname,
                    inv.cinvstd,sum(a.iqty) qty
                    from ekk..c_packets a
                    left join  ekk..c_packet b on a.id=b.id
                    left join inventory inv on a.cinvcode=inv.cinvcode
                    left join mom_orderdetail mos on a.modid=mos.modid                 
                    left join so_sodetails sd on mos.orderdid=sd.isosid                    
                    left join so_somain so on sd.csocode=so.csocode
                    left join customer cus on so.ccuscode=cus.ccuscode
                    left join EKK..T_SO_ExtraFd soex on sd.isosid=soex.isosid
                    where packetno=");
                sb.Append(Common.SqlParm(cPacketCode));
                sb.Append(@" group by  isnull(soex.cdefine5,cus.ccusabbname),cus.ccusabbname,cus.ccusaddress,case when soex.cdefine1 is null then so.csocode+'-'+cast(sd.irowno as varchar(5)) 
else soex.cdefine1+'('+so.csocode+'-'+cast(sd.irowno as varchar(5))+')' end,cus.ccusdefine4,sd.isosid,sd.irowno,a.cinvcode,isnull(soex.cdefine2,inv.cinvname),inv.cinvstd order by sd.irowno");
                DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
                string material = "";
               
                if (dt.Rows.Count == 1)
                {
                    material = getMaterialName(dt.Rows[0]["cinvcode"].ToString());
                }
                
                DataRow dr = ds.Tables["Tab"].NewRow();
                dr["Tabcode"] = "*" + cPacketCode + "*";
                dr["printcus"] = dt.Rows[0]["printcustom"].ToString();
                dr["cusname"] = dt.Rows[0]["ccusabbname"].ToString();
                string bHotel = dt.Rows[0]["ccusdefine4"].ToString().Trim();
                //if (bHotel != "是")
                //    dr["cusname"] = "";
                dr["Address"] = dt.Rows[0]["ccusaddress"].ToString();
                dr["Material"] = material;
                if (dt.Rows.Count == 1)
                    dr["Ordercode"] = dt.Rows[0]["csocode"].ToString() ;
                else
                    dr["Ordercode"] = dt.Rows[0]["csocode"].ToString();
                if (dt.Rows.Count > 1)
                    dr["bSingle"] = false;                    
                else
                    dr["bSingle"] = true;                   

                for (int i = 0; i < (dt.Rows.Count > 9 ? 9 : dt.Rows.Count); i++)
                {
                    string cInvname = dt.Rows[i]["cinvname"].ToString();
                    if (cInvname.IndexOf("-") > 0)
                    {
                        cInvname = cInvname.Substring(cInvname.IndexOf("-") + 1, cInvname.Length - cInvname.IndexOf("-") - 1);
                    }
                    dr["cInvCode" + (i + 1)] = dt.Rows[i]["cinvcode"].ToString();
                    if (dt.Rows.Count == 1)
                        dr["cInvName" + (i + 1)] = cInvname;
                    else
                        dr["cInvName" + (i + 1)] = String.Format("{0}", cInvname);
                    dr["cInvstd" + (i + 1)] = dt.Rows[i]["cinvstd"].ToString();
                    if(dt.Rows.Count==1)
                        dr["Qty" + (i + 1)] =String.Format("{0}",CWD.Common.GetNum(dt.Rows[i]["qty"]));
                    else
                        dr["Qty" + (i + 1)] = String.Format("{0}条", CWD.Common.GetNum(dt.Rows[i]["qty"]));
                }
                if (dt.Rows.Count == 1)
                {
                    int ids = Common.GetInt(SqlAccess.ExecuteScalar("select ids from ekk..c_packets where packetno='"+cPacketCode+"'",ufconn));
                    int sodid = Common.GetInt(dt.Rows[0]["isosid"]);
                    int qty = Common.GetInt(SqlAccess.ExecuteScalar("select top 1 iqty from ekk..c_packets where sodid=" + sodid+"order by ids" , ufconn));
                    decimal soQty = Common.GetNum(SqlAccess.ExecuteScalar("select iquantity from so_sodetails where isosid=" + sodid, ufconn));
                    int packAll = (int)soQty / qty;
                    int packIdx = Common.GetInt(SqlAccess.ExecuteScalar("select count(1) from ekk..c_packets where sodid=" + sodid + " and ids<="+ids, ufconn));
                    if (packIdx <= packAll)
                    {
                        dr["PackAll"] = packAll;
                        dr["PackIdx"] = packIdx;
                    }
                    else
                    {
                        dr["PackAll"] = "零包";
                        dr["PackIdx"] = packIdx;
                    }
                }
                if (dt.Rows.Count > 5)
                    dr["Desc"] = "其它产品未完全列出，以包装中产品为准";
                ds.Tables["Tab"].Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("提取数据出错，可能的原因是：" + ex.Message.ToString(), "提示");
            }

            if (ds.Tables["Tab"].Rows.Count < 1)
            {
                MessageBox.Show("没有数据！", "提示");
                return null;
            }
            return ds;
        }

        private void tsbPrintA5_Click(object sender, EventArgs e)
        {
            int Rowno = dgv.CurrentCell.RowIndex;
            string cPacketCode = Convert.ToString(dgv.Rows[Rowno].Cells["包号"].Value).Trim();
            printA5(cPacketCode);
        }

        private void printA5(string packetNo)
        {
            DBTab ds = getTabDb(packetNo);
            string prnName = "";
            List<C_PrinterSet> lstPrinter = C_PrinterSet.getModelPrinter("打包", Public.userCode, ufconn);
            foreach (C_PrinterSet prn in lstPrinter)
            {
                if (prn.Function == "A5")
                {
                    prnName = prn.Printer;
                    break;
                }
            }
            if (prnName == "")
            {
               Common.ShowMessage("请设置打印机!",2);
               return;
            }
            
            if (ds.Tables["Tab"].Rows[0]["bSingle"]!=null && ds.Tables["Tab"].Rows[0]["bSingle"].ToString().ToLower() == "true")
            {
                if (ds.Tables["Tab"].Rows[0]["cusname"].ToString() == "江苏驿之家")
                {
                    try
                    {
                        CRTabA5yzj crtab = new CRTabA5yzj();
                        crtab.SetDataSource(ds);
                        crtab.PrintOptions.PrinterName = prnName;
                        crtab.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                        frmReport rep = new frmReport();
                        rep.crystalReportViewer1.ReportSource = crtab;
                        //rep.ShowDialog();
                        crtab.PrintToPrinter(1, true, 1, ds.Tables["Tab"].Rows.Count);
                    }
                    catch (Exception ex)
                    {
                        Common.ShowMessage(ex.Message, 2);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        CRTabA5 crtab = new CRTabA5();
                        crtab.SetDataSource(ds);
                        crtab.PrintOptions.PrinterName = prnName;
                        crtab.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                        frmReport rep = new frmReport();
                        rep.crystalReportViewer1.ReportSource = crtab;
                        //rep.ShowDialog();
                        crtab.PrintToPrinter(1, true, 1, ds.Tables["Tab"].Rows.Count);
                    }
                    catch (Exception ex)
                    {
                        Common.ShowMessage(ex.Message, 2);
                        return;
                    }
                }
            }
            else
            {
                try
                {
                    CRTabA5m crtab = new CRTabA5m();
                    crtab.SetDataSource(ds);
                    crtab.PrintOptions.PrinterName = prnName;
                    crtab.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait;
                    frmReport rep = new frmReport();
                    rep.crystalReportViewer1.ReportSource = crtab;                
                    //rep.ShowDialog();
                    crtab.PrintToPrinter(1, true, 1, ds.Tables["Tab"].Rows.Count);
                }
                catch (Exception ex)
                {
                    Common.ShowMessage(ex.Message, 2);
                    return;
                }
            }
        }

        private string getMaterialName(string invcode)
        {
            int partid = bas_part.GetPartID(invcode,ufconn);
            int bomid = bom_bom.GetMaxBOMId(partid, ufconn);
            bom_bom bom = new bom_bom(bomid, ufconn);
            for (int i = 0; i < bom.Items.Count; i++)
            { 
                string minvcode=bas_part.GetInvCode(bom.Items[i].ComponentId,ufconn);
                if (minvcode.Substring(0, 2) == "13")
                {
                    return new Inventory(minvcode, ufconn).cInvName;
                }
            }
            return "";
        }
        private void tsbMuitcopy_Click(object sender, EventArgs e)
        {
            if (cState != "新建")
                return;
            InputNum num = new InputNum();
            num.Text = "请输入重复包数";
            if (num.ShowDialog() != DialogResult.OK)
                return;
            if (Common.GetNum(num.cResult) > 20)
            {
                MessageBox.Show("重复包数不得大于20件！", "数据检查", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int iRow = dgv.CurrentCell.RowIndex;
            string oldPacketno = dgv.Rows[iRow].Cells["包号"].Value.ToString();
            StringBuilder sb = new StringBuilder();
            if (ufconn.State == ConnectionState.Closed)
            {
                ufconn.Open();
            }
            string sBefore="";

            sb.Append(" INSERT INTO EKK..C_Packets (ID,cInvcode,iQty,SoDiD,MoDID,sadid,Item,Sortseq,PacketNo) VALUES (");
            sb.Append(iID + "," + Common.SqlParm(dgv.Rows[iRow].Cells["存货编码"].Value));
            sb.Append("," + dgv.Rows[iRow].Cells["数量"].Value.ToString());
            sb.Append("," + Common.GetNum(dgv.Rows[iRow].Cells["SoDID"].Value));
            sb.Append("," + Common.GetNum(dgv.Rows[iRow].Cells["MoDID"].Value));
            sb.Append("," + Common.GetNum(dgv.Rows[iRow].Cells["sadid"].Value));
            sb.Append("," + Common.GetInt(dgv.Rows[iRow].Cells["整理组"].Value) + ",");
            sBefore = sb.ToString();
                
            for (int i = 0; i < (int)Common.GetNum(num.cResult); i++)
            {
                string Msg = CheckMom(Common.GetInt(dgv.Rows[iRow].Cells["MoDID"].Value), Common.GetNum(dgv.Rows[iRow].Cells["数量"].Value));
                if (Msg != "")
                {
                    Common.ShowMessage(Msg + ",不能复制！", 2);
                    SetbtnState("保存");
                    ReflashSheet(cSheetID);       
                    return;
                }
                string cPacketno = GetPacketNo(cwDepartment.Value);
                if (cPacketno == "")
                {
                    MessageBox.Show("找不到部门代码，可能未设置部门代码！", "数据检查", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string sSql = "";
                sSql=sBefore + (dgv.Rows.Count+i+1) + ",'" + cPacketno + "')";
                SqlAccess.ExecuteSql(sSql, ufconn);
                if (rdA5.Checked)
                    printA5(cPacketno);
                else if (rdPaperBox.Checked)
                    printPaperBox(cPacketno);
            }               
            SetbtnState("保存");
            ReflashSheet(cSheetID);            
        }

        private void tsbBredeOut_Click(object sender, EventArgs e)
        {
            fmBrede brede = new fmBrede();
            brede.conn = ufconn;
            brede.SRFlag = true;
            brede.ShowDialog();            
        }

        private void tsbBredeIn_Click(object sender, EventArgs e)
        {
            fmBrede brede = new fmBrede();
            brede.conn = ufconn;
            brede.SRFlag = false;
            brede.ShowDialog();          
        }
        public void ChangeSize()
        {
            dgv.Width = this.ClientRectangle.Width - 4;
            dgv.Height = (this.ClientRectangle.Height-140)*3/5;
            dgv.Left = 2;
            dgv.Top = 140;
            btnAdd.Top = dgv.Bottom;
            btnAdd.Left = (this.ClientRectangle.Width) * 2 / 6-btnAdd.Width;
            btnDel.Left = (this.ClientRectangle.Width) * 3 / 6;
            btnDel.Top = btnAdd.Top;
            cbHandler.Top = btnAdd.Top+(btnAdd.Height-cbHandler.Height)/2;
            lbHandler.Top = btnAdd.Top + (btnAdd.Height - lbHandler.Height) / 2;
            dtpHandler.Top = btnAdd.Top + (btnAdd.Height - dtpHandler.Height) / 2;
            lbHandlerdate.Top = btnAdd.Top + (btnAdd.Height - lbHandlerdate.Height) / 2;
            lbHandler.Left = btnDel.Right + 20;
            cbHandler.Left = lbHandler.Right + 5;
            lbHandlerdate.Left = cbHandler.Right + 20;
            dtpHandler.Left = lbHandlerdate.Right + 5;
            dgvPacket.Top = btnAdd.Bottom ;
            dgvPacket.Width = this.ClientRectangle.Width-4;
            dgvPacket.Left = 2;
            dgvPacket.Height = this.ClientRectangle.Height - dgv.Top-dgv.Height-btnAdd.Height - 2;
        }
        private Hashtable GetMosCount(C_Packet packet)
        {
            Hashtable hashMos = new Hashtable();
            for (int i = 0; i < packet.Items.Count; i++)
            {
                if (hashMos[packet.Items[i].MoDID] == null)
                {
                    mom_orderdetail mos = new mom_orderdetail((int)packet.Items[i].MoDID,ufconn);
                    //mos.MoDId = packet.Items[i].MoDID;
                    //mos.InvCode = packet.Items[i].cInvcode;
                    if (packet.Items[i].SoDID > 0)
                        mos.SoDId = packet.Items[i].SoDID.ToString();
                    else
                        mos.SoDId = packet.Items[i].sadid.ToString();           
                    mos.OrgQty=1;
                    mos.QualifiedInQty = (decimal)packet.Items[i].iQty;//这里用作本次入库数量
                    hashMos.Add(mos.MoDId, mos);
                }
                else
                {
                    ((mom_orderdetail)hashMos[packet.Items[i].MoDID]).QualifiedInQty += (int)packet.Items[i].iQty;
                    ((mom_orderdetail)hashMos[packet.Items[i].MoDID]).OrgQty += 1;
                }
            }
            return hashMos;
        }
        private void btnDel_Resize(object sender, EventArgs e)
        {
            ChangeSize();
        }

        private void cwOper3_Click(object sender, EventArgs e)
        {
            if (cwOper1.Text == "" || cwOper2.Text == "")
            {
                MessageBox.Show("请首先填充第一、第二操作员！", "提示");
                return;
            }
            cwOper3.Text = "";
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            object oj = SqlAccess.ExecuteScalar("SELECT cPsn_Name FROM Hr_hi_person where cPsn_Num='" + input.cResult + "'", ufconn);
            if (oj != null)
            {
                cwOper3.Text = oj.ToString();
                cwOper3.Value = input.cResult;
                if (cwOper3.Text == cwOper1.Text || cwOper2.Text == cwOper3.Text)
                {
                    MessageBox.Show("第三操作员不能和第一、第二操作员相同！", "提示");
                    cwOper3.Text = ""; cwOper3.Value = "";
                    return;
                }
            }
        }

        private void tsbFind_Click(object sender, EventArgs e)
        {
            fmSolist solist = new fmSolist();
            solist.Conn = ufconn;
            solist.depCode = cwDepartment.Value;
            solist.ShowDialog();
        }

        private void btnSetPrn_Click(object sender, EventArgs e)
        {
            fmSetPrinter prnset = new fmSetPrinter();
            prnset.OpName = "打包";
            prnset.Conn = ufconn;
            prnset.ShowDialog();
        }
    }
}
