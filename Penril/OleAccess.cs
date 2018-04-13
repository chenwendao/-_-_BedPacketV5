using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace CWD
{
    class OleAccess
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="oleconnstr"></param>
        /// <returns></returns>
        public static OleDbConnection OleConn(string oleconnstr)
        {
            //oracle连接方法
            //"Provider=OraOLEDB.Oracle;Persist Security Info=True;User ID=nari55;Password=1;Data Source=ORCL" 
            //excel连接方法
            //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';
            OleDbConnection oleconn = null;
            try
            {
                oleconn = new OleDbConnection(oleconnstr);
                if (oleconn.State.ToString().ToLower() != "open")
                {
                    oleconn.Open();
                }
                return oleconn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        public static void ExecuteSql(string sqlcmd, string Account)
        {
            OleDbConnection conn = new OleDbConnection(Account);
            OleDbCommand cmd = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        public static void ExecuteSql(string sqlcmd, OleDbConnection conn)
        {
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        public static void ExecuteSql(string sqlcmd, OleDbTransaction tran)
        {
            OleDbConnection conn = tran.Connection;
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn, tran);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlcmd, string Account)
        {
            OleDbConnection conn = new OleDbConnection(Account);
            OleDbCommand cmd = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                cmd.Dispose();
                conn.Close();
                return obj;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlcmd, OleDbTransaction tran)
        {
            OleDbConnection conn = tran.Connection;
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn, tran);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                cmd.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlcmd, OleDbConnection conn)
        {
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                cmd.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static DataTable ExecuteDT(string sqlcmd, string Account)
        {
            OleDbConnection conn = new OleDbConnection(Account);
            OleDbDataAdapter da;
            DataTable dt = new DataTable();
            OleDbCommand cmd = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static DataTable ExecuteDT(string sqlcmd, OleDbTransaction tran)
        {
            OleDbConnection conn = tran.Connection;
            OleDbDataAdapter da;
            DataTable dt = new DataTable();
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn, tran);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable ExecuteDT(string sqlcmd, OleDbConnection conn)
        {
            OleDbDataAdapter da;
            DataTable dt = new DataTable();
            OleDbCommand cmd = null;
            try
            {
                cmd = new OleDbCommand(sqlcmd, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                throw ex;
            }
        }

    }
}
