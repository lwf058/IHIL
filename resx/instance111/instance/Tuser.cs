using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using System.ComponentModel.Composition;
namespace instance
{

    [Export("TLoginUser", typeof(ILoginUser))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  

    public class TLoginUser : ILoginUser
    {
        string loginName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        /// <summary>
        /// 用户类型 
        /// </summary>
        private UserState loginState;

        public UserState LoginState
        {
            get { return loginState; }
            set { loginState = value; }
        }

        private int loginpermission;

        public int Loginpermission
        {
            get { return loginpermission; }
            set { loginpermission = value; }
        }
    }
    
}
