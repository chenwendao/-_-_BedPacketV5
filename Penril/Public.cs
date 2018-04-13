using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using CWD.DAL;

namespace CWD
{
    class Public
    {
        public static string ufconnstr = "Data Source=192.168.50.9;Initial Catalog=UFDATA_001_2018;User ID=sa;Password=mmiouC3";
        //public static string sysconnstr = "Data Source=192.168.50.8;Initial Catalog=UFSystem;User ID=sa;Password=mmiouC3";
        //public static string EKKconnstr = "Data Source=192.168.50.8;Initial Catalog=EKK;User ID=sa;Password=mmiouC3";

        public static string userCode = "00019";
        public static string userName = "³ÂÎÄµÀ";
        public static string ddate = "2017-07-19";
        public static string cacc_id = "001";
        public static string strufdata = "UFDATA_001_2018";
        public static Boolean IsAdmin = false;
        public static string cStatus = "U8";
        public static Boolean bAudit = true;
        public static int OperID = 0;
        public static string ModelName = "";
        public static string passcode = "";
        public static string passname = "";
        public static string passuser = "";


        
        public static string getVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        public static string getProgramName()
        {
            string name = System.Reflection.Assembly.GetExecutingAssembly().GetName().ToString();
            name = name.Substring(0, name.IndexOf(","));
            return name;
        }
        public static string GetAssemblyShortName()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }
        //public static string getU8Str()
        //{
        //    C_ExeDbConStr exedbstr = new C_ExeDbConStr(Public.GetAssemblyShortName(), SqlAccess.SqlConn(ufconnstr));
        //    return exedbstr.DbConStr;
        //}
    }
}
