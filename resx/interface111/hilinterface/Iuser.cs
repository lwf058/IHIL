using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace iinterface
{
    /// <summary>
    ///用户名的接口
    /// </summary>
    public interface ILoginUser
    {
        //string loginName;
        /// <summary>
        /// 用户名
        /// </summary>
        string LoginName
        {
            get;// { return loginName; }
            set;//{ loginName = value; }
        }
        /// <summary>
        /// 用户类型 
        /// </summary>
        UserState LoginState
        {
            get;
            set;
        }

        int Loginpermission
        {
            get;
            set;
        }



    }




}
