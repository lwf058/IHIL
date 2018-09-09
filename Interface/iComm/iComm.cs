using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iinterface
{
    /// <summary>
    /// 串口的变量从通讯类的基类继承下来。
    /// </summary>
    public interface iCommVariables :iCommunicationBase
    {
        
    }
    /// <summary>
    /// 串口的接口，
    /// </summary>
    public interface iComm
    {
        /// <summary>
        /// 端口号
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        string BTL { get; set; }
        /// <summary>
        /// 协议文件
        /// </summary>
        string AgreementFile { get; set; }
        /// <summary>
        /// 根据索引号，取对应的值
        /// </summary>
        /// <param name="sBlMc">变量名称</param>
        /// <returns></returns>
        iCommVariables FindByIndex(int idx);
        /// <summary>
        /// 根据名称找变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        iCommVariables FindByName(string sName);
        
        /// <summary>
        /// 关闭串口
        /// </summary>
        void CloseComm();
        /// <summary>
        /// 打开串口；
        /// </summary>
        bool OpenComm();
    }
}
