using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using instance;
namespace TComm
{
    public class TCommVariables : TCommuniationBase,iCommVariables
    {

    }
    public class TComm:iComm
    {

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public string BTL { get; set; }
        /// <summary>
        /// 协议文件
        /// </summary>
        public string AgreementFile { get; set; }
        /// <summary>
        /// 根据索引号，取对应的值
        /// </summary>
        /// <param name="sBlMc">变量名称</param>
        /// <returns></returns>
        public iCommVariables FindByIndex(int idx)
        {
            return null;
        }
        /// <summary>
        /// 根据名称找变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public iCommVariables FindByName(string sName)
        {
            return null;    
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void CloseComm()
        {

        }
        /// <summary>
        /// 打开串口；
        /// </summary>
        public bool OpenComm()
        {
            return true;
        }

    }
}
