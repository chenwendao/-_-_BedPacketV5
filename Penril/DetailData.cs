using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CWD.DAL;
using CWD.Report;

namespace CWD
{
    public partial class DetailData : Form
    {
        public SqlConnection ufconn;
        public int? iMoDID;
        public int? iSoDID;
        public int? iSaDiD;//Ԥ����id
        public int? iMoroutingDID;
        public string cInvcode ;
        public string cSocode;
        public string cSacode;
        public string cMocode;
        public string cPname;
        public decimal dQty;
        public string cItem;
        

        public DetailData()
        {            
            InitializeComponent();
            dQty = 0.0M;
        }

        private void cwMoDID_Click(object sender, EventArgs e)
        {
            cwMoDID.Text = "0";
            iMoroutingDID = 0;

            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            if (input.cResult.Length >= 5)
            {
                cwMoDID.Text = input.cResult;
                iMoDID = Convert.ToInt32(cwMoDID.Text);
                ShowMo((int)iMoDID);
                //btnOK.Enabled = true;
            }
        }

        private void DetailData_Load(object sender, EventArgs e)
        {
            if (ufconn == null)
            {
                MessageBox.Show("�Ҳ��������������ӣ�","��ʾ");
                return;
            }
            //dQty = null;
            iMoDID = null;
            iSoDID = null;
            iMoroutingDID = null;
            cInvcode = "";
            lbMo.Text = "";
            cwTeam.Text = "1";
        }
        private void ShowMo(int iMoDID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"select top 1  e.MoDID,b.MoRoutingDId,f.MoCode +'_'+cast(e.sortSeq as varchar(3)) ��������,g.cInvCode �������,
                        g.cInvName �������,g.cInvStd ����ͺ�,e.Qty ��������,j.description ��������,b.StartDate,b.DueDate
                        from sfc_moroutingdetail b
                        left join sfc_morouting c ON b.MoRoutingId = c.MoRoutingId
                        left join mom_morder d on c.MoDId = d.MoDId
                        left join mom_orderdetail e on d.MoDId = e.MoDId  
                        left join mom_order f on e.MoId = f.MoId  
                        LEFT JOIN Inventory g ON e.InvCode = g.cInvCode
                        LEFT JOIN sfc_workcenter j ON b.WcId = j.WcId--��������
                        where b.OperationID =" + Public.OperID + " and e.Modid=" + iMoDID + " and e.CloseUser is null");
            DataTable dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("�Ҳ������Ʒ�֣����ܶ����ѹرջ�û�����ɹ��򶩵���", "��ʾ");
                btnOK.Enabled = false;
                return;
            }
            cMocode = dt.Rows[0]["��������"].ToString();
            cPname = dt.Rows[0]["�������"].ToString().Trim() + " " + dt.Rows[0]["����ͺ�"].ToString().Trim();
            iMoroutingDID = Convert.ToInt32(dt.Rows[0]["MoRoutingDId"]);
            cInvcode = Convert.ToString(dt.Rows[0]["�������"]);
            iMoDID = Convert.ToInt32(dt.Rows[0]["MoDID"]);
            lbMo.Text = cMocode +" "+ cInvcode;
            lbMo.Text = lbMo.Text + "\n" + cPname;
            lbMo.Text=lbMo.Text+"\n����������"+String.Format("{0:0.0}",Convert.ToDecimal(dt.Rows[0]["��������"]));
            lbMo.Text=lbMo.Text+"\n������"+Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToShortDateString()+" �깤��"+ Convert.ToDateTime(dt.Rows[0]["DueDate"]).ToShortDateString();
            
            sb.Remove(0, sb.Length);
            sb.Append(@"  select a.cSoCode +'_'+ cast(a.irowno as varchar(3)) code,cCusAbbName,isosid from so_sodetails a left join so_somain b on a.csocode=b.csocode 
                left join Customer c on b.cCuscode=c.cCuscode where isosid in (select orderdid from mom_orderdetail where modid=" + iMoDID + " and ordertype=1 )  order by isosid desc");
            dt.Clear();
            dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
            if (dt.Rows.Count > 0)
            {
                iSoDID = Convert.ToInt32(dt.Rows[0]["isosid"]);
                lbMo.Text = lbMo.Text + "\n���۶�����" + dt.Rows[0]["code"].ToString();
                lbMo.Text = lbMo.Text + "\n�ͻ���ƣ�" + dt.Rows[0]["cCusAbbName"].ToString();
                cSocode = dt.Rows[0]["code"].ToString();
            }
                //20130807 begin ����Ԥ����
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append(@"  select b.cCode +'_'+ cast(a.irowno as varchar(3)) code,cCusName,autoid from sa_PreOrderDetails a left join SA_PreOrderMain b on a.id=b.id
                 where autoid in (select orderdid from mom_orderdetail where modid=" + iMoDID + " )  order by autoid desc");
                dt.Clear();
                dt = SqlAccess.ExecuteSqlDataTable(sb.ToString(), ufconn);
                if (dt.Rows.Count > 0)
                {
                    iSaDiD = Convert.ToInt32(dt.Rows[0]["autoid"]);//Ԥ����id
                    lbMo.Text = lbMo.Text + "\n���۶�����" + dt.Rows[0]["code"].ToString();
                    lbMo.Text = lbMo.Text + "\n�ͻ���ƣ�" + dt.Rows[0]["cCusName"].ToString();
                    cSacode = dt.Rows[0]["code"].ToString();
                }
                iSoDID = 0;
                cSocode = "";
            }
            //end
            btnOK.Enabled = true;
            btnPrnSo.Enabled = true;
            btnSoall.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cwTeam.Text.Trim() == "")
            {
                Common.ShowMessage("�����������飡",2);
                return;
            }
            cItem = cwTeam.Text.Trim();
            this.DialogResult = DialogResult.OK;            
        }

        private void cwQty_Click(object sender, EventArgs e)
        {
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            if (input.cResult.Length > 0)
            {
                cwQty.Text = input.cResult;
                dQty = Convert.ToDecimal(cwQty.Text);
                btnOK.Enabled = true;
            }
        }

        private void cwTeam_Click(object sender, EventArgs e)
        {
            InputNum input = new InputNum();
            if (input.ShowDialog() != DialogResult.OK)
                return;
            if (input.cResult.Length > 0)
            {
                cwTeam.Text = input.cResult;
                cItem = cwTeam.Text.Trim();
                btnOK.Enabled = true;
            }
        }

        private void btnPrnSo_Click(object sender, EventArgs e)
        {
            fmPrint print = new fmPrint();
            if (iSoDID == null)
            {
                Common.ShowMessage("��ǰ�����������Ǵ����۶������ɣ��ް�װ��Ϣ��",2);
                return;
            }
            DSPrint ds = GetDSPrint((int)iSoDID);
            if (cInvcode.Substring(0,2) == "31")
            {
                CROrderMJ crt = new CROrderMJ();
                crt.SetDataSource(ds);
                print.crv1.ReportSource = crt;
            }
            else if (cInvcode.Substring(0, 2) == "32" || cInvcode.Substring(0, 2) == "33")
            {
                CROrderOther crt = new CROrderOther();
                crt.SetDataSource(ds);
                print.crv1.ReportSource = crt;
            }
            else
            {
                CROrderAll crt = new CROrderAll();
                crt.SetDataSource(ds);
                print.crv1.ReportSource = crt;
            }
            this.Cursor = Cursors.Default;
            print.ShowDialog();
        }

        private DSPrint GetDSPrint(int Sodid)
        {
            object oj=SqlAccess.ExecuteScalar("select ID from so_sodetails where isosid=" + Sodid, ufconn);
            if(oj==null)
                return null;
            int SoID=Common.GetInt(oj);
            
            SO_SOMain so = new SO_SOMain(SoID, ufconn);
            DSPrint ds = new DSPrint();
            DataTable dtSoMain = ds.Tables["SoMain"];
            DataRow drm = dtSoMain.NewRow();
            drm["cSoCode"] = so.cSOCode;
            drm["dDate"] = so.dDate;
            drm["cBusType"] = so.cBusType;
            drm["cSTName"] = so.cSTCode;
            drm["cCusAbbName"] = SqlAccess.ExecuteScalar("select cCusAbbName from customer where ccuscode=" + Common.SqlParm(so.cCusCode), ufconn).ToString();
            drm["cDepName"] = SqlAccess.ExecuteScalar("select cdepname from department where cdepcode=" + Common.SqlParm(so.cDepCode), ufconn);
            drm["cPersonName"] = SqlAccess.ExecuteScalar("select cpsn_name from hr_hi_person where cpsn_num=" + Common.SqlParm(so.cPersonCode), ufconn);
            drm["iTaxRate"] = so.iTaxRate;
            drm["cExch_Name"] = so.cexch_name;
            drm["iExchRate"] = so.iExchRate;
            drm["DYKH"] = so.cDefine3;
            drm["cMaker"] = so.cMaker;
            drm["dcreatesystime"] = so.dcreatesystime;
            drm["cVerifier"] = so.cVerifier;
            drm["dverifysystime"] = so.dverifysystime;
            drm["BZFS"] = "��װ��ʽ��" + so.cDefine2;            
            
            drm["MoDepName"] = "ȱʡ";
            
            StringBuilder strbz = new StringBuilder();
            if (so.cDefine14 != null && so.cDefine14.Length > 0)
            {
                strbz.Append("1��" + so.cDefine14.ToString());
            }
            if (so.cDefine13 != null && so.cDefine13.Length > 0)
            {
                strbz.Append("1��" + so.cDefine13.ToString());
            }
            if (so.cDefine12 != null && so.cDefine12.Length > 0)
            {
                strbz.Append("1��" + so.cDefine12.ToString());
            }
            if (so.cDefine11 != null && so.cDefine11.Length > 0)
            {
                strbz.Append("1��" + so.cDefine11.ToString());
            }
            if (so.cDefine10 != null && so.cDefine10.Length > 0)
            {
                strbz.Append("1��" + so.cDefine10.ToString());
            }


            if (so.cDefine2 != null && so.cDefine2.Length > 0)
            {
                strbz.Append("����װ��ʽ��" + so.cDefine2);
            }
            drm["BZ"] = "������ע��" + strbz.ToString();
            ds.Tables["SoMain"].Rows.Add(drm);
           
            for (int i = 0; i < so.Items.Count; i++)
            {
                if (so.Items[i].iSOsID != Sodid)
                    continue;
                Inventory inv = new Inventory(so.Items[i].cInvCode, ufconn);               
               
                DataTable dtDetailInv = ds.Tables["DetailInv"];
                DataRow dr = dtDetailInv.NewRow();
                dr["cSoCode"] = so.Items[i].cSOCode;
                dr["cInvCode"] = so.Items[i].cInvCode;
                dr["cInvName"] = so.Items[i].cInvName;
                dr["cInvStd"] = so.Items[i].cInvStd;
                dr["iQuantity"] = so.Items[i].iQuantity;
                dr["cComUnitName"] = so.Items[i].ComUnitName;
                dr["dPreDate"] = ((DateTime)so.Items[i].dPreDate).ToString("yyyy-MM-dd");
                dr["cMemo"] = so.Items[i].cMemo;
                dr["APWarp"] = so.Items[i].cDefine22;
                dr["Wnwarp"] = so.Items[i].cDefine25;
                dr["isCheckPin"] = so.Items[i].cDefine26 == 1 ? "��" : "��";
                dr["isWG"] = Common.GetInt(so.Items[i].cDefine27) == 1 ? "��" : "��";
                dr["cInvDefine1"] = inv.cInvDefine1;
                dr["cInvDefine2"] = inv.cInvDefine2;
                dr["cInvDefine3"] = inv.cInvDefine3;
                dr["cInvDefine5"] = inv.cInvDefine5;
                dr["cInvDefine6"] = inv.cInvDefine6;
                dr["cInvDefine7"] = inv.cInvDefine7;
                dr["cInvDefine8"] = inv.cInvDefine8;
                dr["cInvDefine9"] = inv.cInvDefine9;
                dr["cInvDefine10"] = inv.cInvDefine10;
                dr["cInvDefine11"] = inv.cInvDefine11 == 1 ? "��" : "��";
                dr["cInvDefine12"] = inv.cInvDefine12 == 1 ? "��" : "��";
                dr["cInvAddCode"] = inv.cInvAddCode;
                oj = SqlAccess.ExecuteScalar("select picid from Ekk..t_pictureguid where guid='" + inv.PictureGUID + "'", ufconn);
                if (oj != null)
                    dr["Picture"] = GetByte(oj.ToString());
                dr["iRowNo"] = so.Items[i].iRowNo;
                dr["cDefine29"] = so.Items[i].cDefine29;
                dr["xh"] = i + 1;
                dr["RRJ"] = so.Items[i].cDefine31;
                ds.Tables["DetailInv"].Rows.Add(dr);
            }
            return ds;
        }

        private byte[] GetByte(string picid)
        {
            string sql = "select pic from Ekk..T_ProductPic where id='" + picid + "'";
            byte[] b = null;
            SqlDataReader dr = SqlAccess.GetExecuteReader(sql, ufconn);
            if (dr.Read())
            {
                b = (byte[])dr["pic"];
            }
            return b;
        }

        private void btnSoall_Click(object sender, EventArgs e)
        {
            if (iSoDID == null)
            {
                Common.ShowMessage("��ǰ�����������Ǵ����۶������ɣ����ܲ鿴���۶��������",2);
                return;
            }
            fmSolist solist = new fmSolist();
            solist.Conn = ufconn;
            solist.SoDID =(int) iSoDID;
            solist.InvSort = cInvcode.Substring(0, 2);
            solist.ShowDialog();
        }
    }
}