using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iinterface
{
    /// <summary>
    /// 以太网 通讯
    /// </summary>
    public interface iEthVariables:iCommunicationBase
    {

    }
    
    public interface iEthCmd
    { 
        //<cmd caption="VOLT" id="VOLT" sendlen="0" recelen="0" waittime="0" isreturnval="0" checktype="CRLF" submethod="Sub">
        
        string Caption {get;set;}
        string Name { get; set; }
        int SendLen { get; set; }
        int ReceLen { get; set; }
        int WaitTime { get; set; }
        int Isreturnval { get; set; }

        string CheckType{ get;set;}
        string SubMethod {get;set;}

        //checktype="CRLF" submethod="Sub"
    }

    /// <summary>
    /// 以太网的通讯类
    /// </summary>
    public interface iEthernet

    {
        /// <summary>
        /// 端口号
        /// </summary>
        int Port { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        string IP { get; set; }
        /// <summary>
        /// 协议文件
        /// </summary>
        string AgreeMentFile { get; set; }
        /// <summary>
        /// 根据变量名称，取对应的值
        /// </summary>
        /// <param name="sBlMc">变量名称</param>
        /// <returns></returns>
        string GetByBlMc(string sBlMc);
        /// <summary>
        /// 打开以太网口，成功为True;否则为 False;
        /// </summary>
        bool OpenEth();
        /// <summary>
        /// 关闭以太网口
        /// </summary>
        void CloseEth();

        /// <summary>
        /// 找到对应序号的变量
        /// </summary>
        /// <param name="idx">索引位置</param>
        /// <returns></returns>
        iEthVariables FindByIndex(int idx);

        /// <summary>
        /// 找到对应名称指令的变量
        /// </summary>
        /// <param name="sName">对应的名称</param>
        /// <returns></returns>
        iEthVariables FindByName(string sName);

    }
}
