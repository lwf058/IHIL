using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Nebula_db
{
    class MyDBCommand
    {
        /// <summary>
        /// 查找所有用户
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetUsers()
        {
            DataTable dt = null;
            try
            {
                string sqlStr = "select id AS ID, LoginUsers AS 用户名,AddDate AS 添加时间 from UsersManage ";
                dt = MyDBHelper.FillTable(sqlStr);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetUsersName()
        {
            DataTable dt = null;
            try
            {
                string sqlStr = "select LoginUsers from UsersManage ";
                dt = MyDBHelper.FillTable(sqlStr);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="_users"></param>
        /// <param name="_passWord"></param>
        /// <returns></returns>
        internal static bool InsertUsers(string _users, string _passWord,string _permission)
        {
            bool result = false;
            try
            {
                string sqlStr = "insert into UsersManage(AddDate,LoginUsers,LoginPassWord,Permission) values('" + System.DateTime.Today + "','" + _users + "','" + _passWord + "','" + _permission + "')";
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 判断用户存在性
        /// </summary>
        /// <param name="_users"></param>
        /// <returns></returns>
        internal static bool ContainsStation(string _users)
        {
            bool result = false;
            try
            {
                string sqlStr = "select count(*) from UsersManage where LoginUsers='" + _users + "'";
                if (Convert.ToInt32(MyDBHelper.GetScalar(sqlStr)) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="_users"></param>
        /// <param name="_passWord"></param>
        /// <returns></returns>
        internal static bool UpdateUser(string _users, string _passWord,int _identity,int selectId)
        {
            bool result = false;
            try
            {
                string sqlStr = "update UsersManage set LoginUsers='" + _users + "' ,LoginPassWord='" + _passWord + "',Permission='"+ _identity +"' where id="+selectId;
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                string ms = ex.ToString();
                throw;
            }
            return result;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="selectId"></param>
        /// <returns></returns>
        internal static bool DeleteUser(int selectId)
        {
            bool result = false;
            try
            {
                string sqlStr = "delete  from UsersManage where id=" + selectId;
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 根据身份名称查找身份的id
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetIdentity(string _identity)
        {
            DataTable dt = null;
            try
            {
                string sqlStr = "select id from PermissionManage where Permission='" + _identity+"'";
                dt = MyDBHelper.FillTable(sqlStr);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        /// <summary>
        /// 判断PackPN是否存在
        /// </summary>
        /// <param name="packPN"></param>
        /// <returns></returns>
        internal static bool ContainsBattery(string packPN)
        {
            bool result = false;
            try
            {
                string sqlStr = "select count(*) from battery where packPN='" + packPN + "'";
                if (Convert.ToInt32(MyDBHelper.GetScalar(sqlStr)) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 插入产品型号
        /// </summary>
        /// <param name="packPN"></param>
        /// <param name="packName"></param>
        /// <param name="useTime"></param>
        /// <returns></returns>
        internal static bool InsertBattery(string packPN, string packName, int useTime)
        {
            bool result = false;
            try
            {
                string sqlStr = "insert into battery values('" + packPN + "','" + packName + "','" + useTime + "')";
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 修改型号
        /// </summary>
        /// <param name="packPN"></param>
        /// <param name="packName"></param>
        /// <param name="useTime"></param>
        /// <param name="selectId"></param>
        /// <returns></returns>
        internal static bool UpdateBattery(string packPN, string packName, int useTime, int selectId)
        {
            bool result = false;
            try
            {
                string sqlStr = "update battery set packPN='" + packPN + "',packName='" + packName + "' ,useTime=" + useTime + " where id=" + selectId;
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 删除产品型号
        /// </summary>
        /// <param name="selectId"></param>
        /// <returns></returns>
        internal static bool DeleteBatatery(int selectId)
        {
            bool result = false;
            try
            {
                string sqlStr = "delete  from battery where id=" + selectId;
                if (MyDBHelper.ExecuteNonQuery(sqlStr) > 0)
                    result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 获取所有配方
        /// </summary>
        /// <param name="batteryIdInts"></param>
        /// <param name="stationIdStrs"></param>
        /// <returns></returns>

    }
}
