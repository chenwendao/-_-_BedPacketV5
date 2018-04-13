using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CWD.DAL;

namespace CWD
{
    public partial class fmSetPrinter : Form
    {
        public SqlConnection Conn;
        public string OpName;
        public fmSetPrinter()
        {
            InitializeComponent();
        }

        private void fmSetPrinter_Load(object sender, EventArgs e)
        {
            foreach (string iprt in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbA5.Items.Add(iprt.ToString());
                cbBox.Items.Add(iprt.ToString());             
            }
            List<C_PrinterSet> lstPrinter = C_PrinterSet.getModelPrinter(OpName, Public.userCode, Conn);
            foreach (C_PrinterSet prn in lstPrinter)
            {
                if (prn.Function.ToUpper() == "A5")
                {
                    cbA5.SelectedText = prn.Printer;
                }
                if (prn.Function.ToUpper() == "BARCODE")
                {
                    cbBox.SelectedText = prn.Printer;
                }                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<C_PrinterSet> lstPrinter = new List<C_PrinterSet>(); 

            if (cbA5.Text != "")
            {
                C_PrinterSet printer = new C_PrinterSet();
                printer.Printer = cbA5.Text;
                printer.Module = OpName;
                printer.User = Public.userCode;
                printer.Function = "A5";
                lstPrinter.Add(printer);
            }
            if (cbBox.Text != "")
            {
                C_PrinterSet printer1 = new C_PrinterSet();
                printer1.Printer = cbBox.Text;
                printer1.Module = OpName;
                printer1.User = Public.userCode;
                printer1.Function = "BARCODE";
                lstPrinter.Add(printer1);
            }
            
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                for (int i = 0; i < lstPrinter.Count; i++)
                {
                    if (lstPrinter[i].Exists(lstPrinter[i].Module, lstPrinter[i].User, lstPrinter[i].Function, tran))
                    {
                        lstPrinter[i].ID = lstPrinter[i].getId(lstPrinter[i].Module, lstPrinter[i].User, lstPrinter[i].Function, tran);
                        lstPrinter[i].Update(tran);
                    }
                    else
                        lstPrinter[i].Add(tran);
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Common.ShowMessage(ex.Message,2);
                return;
            }
            this.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}