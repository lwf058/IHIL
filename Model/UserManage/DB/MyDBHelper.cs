using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace Nebula_db
{
    class MyDBHelper
    {
        public static DataSet GetMyDBData(string _sql, string _tableName)
        {
            string ConStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data source='{0}\MyDB.accdb'", Application.StartupPath + "\\sysdata");
            OleDbConnection oleCon = new OleDbConnection(ConStr);
            OleDbDataAdapter oleDap = new OleDbDataAdapter(_sql, oleCon);
            DataSet ds = new DataSet();
            oleDap.Fill(ds, _tableName);
            oleCon.Close();
            oleCon.Dispose();
            return ds;

        }

        static string ConStr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data source='{0}\MyDB.accdb'", Application.StartupPath+"\\sysdata");
        private static OleDbConnection oleCon = new OleDbConnection(ConStr);
        public static OleDbConnection Con
        {
            get
            {
                if (oleCon == null)
                {
                    oleCon = new OleDbConnection(ConStr);
                    oleCon.Open();
                } 
                if (oleCon.State == ConnectionState.Closed)
                {
                    oleCon.Open();
                } 
                if (oleCon.State == ConnectionState.Broken)
                {
                    oleCon.Close();
                    oleCon.Open();
                }
                return oleCon;
            }
        }
        //根据传进的SQL语句 逐条显示 往前只读

        public static OleDbDataReader GetReader(string sql)
        {
            OleDbCommand cmd = Con.CreateCommand();
            cmd.CommandText = sql;
            OleDbDataReader sdr = cmd.ExecuteReader();
            return sdr;
        }

        //执行增删改等操作
        public static int ExecuteNonQuery(string sql, OleDbTransaction myTran)//参数带事务
        {
            OleDbCommand cmd = Con.CreateCommand();
            cmd.Transaction = myTran;
            cmd.CommandText = sql;
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        //执行增删改等操作
        //public static int ExecuteNonQuery(string sql)
        //{
        //    OleDbCommand cmd = Con.CreateCommand();
        //    cmd.CommandText = sql;
        //    int result = cmd.ExecuteNonQuery();
        //    return result;
        //}

        public static int ExecuteNonQuery(string sqlStr)
        {
            using (OleDbConnection conn = new OleDbConnection(ConStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sqlStr, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //返回第一行第一列 统计用 带事务
        public static object GetScalar(string sql, OleDbTransaction myTran, params OleDbParameter[] values)
        {
            OleDbCommand cmd = Con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = myTran;
            cmd.Parameters.AddRange(values);
            Object obj = cmd.ExecuteScalar();
            return obj;
        }
        //返回第一行第一列 统计用
        public static object GetScalar(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = Con.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(values);
            Object obj = cmd.ExecuteScalar();
            return obj;
        }
        //利用数据适配器填充数据集
        public static DataSet Fill(string sql, DataSet ds)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Con);
            OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }

        public static DataSet Fill(string sql, DataSet ds, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Con);
            cmd.Parameters.AddRange(values);
            OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
            sda.Fill(ds);
            return ds;
        }
        //返回表
        public static DataTable FillTable(string sql)
        {
            DataTable dt = new DataTable();
            OleDbCommand cmd = new OleDbCommand(sql, Con);
            OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
        //执行存储过程
        public static OleDbDataReader ExecStoreProc(string ProcName, params OleDbParameter[] values)
        {
            OleDbCommand cmd = Con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Parameters.AddRange(values);
            OleDbDataReader oddr = cmd.ExecuteReader();
            return oddr;
        }
        //执行存储过程
        //OleDbParameter[] paras = new OleDbParameter[] { new OleDbParameter("@name", "Pudding"), new OleDbParameter("@ID", "1") }; 
        public static DataTable ExecStoreProcDt(string ProcName, params OleDbParameter[] values)
        {
            DataTable dt = new DataTable();
            OleDbCommand cmd = Con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Parameters.AddRange(values);
            OleDbDataAdapter sda = new OleDbDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }
    }
}
