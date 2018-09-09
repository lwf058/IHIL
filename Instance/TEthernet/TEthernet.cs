using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using System.IO;
using System.Xml;
using System.ComponentModel.Composition;
using instance;
namespace TEthernet
{

    [Export("TEthVariables", typeof(iEthVariables))]
    [PartCreationPolicy(CreationPolicy.NonShared)] 
    public class TEthVariables:TCommuniationBase, iEthVariables
    {
        //<item id="VOLT" startbit="-1" caption="有效数字" defaultvalue="" valuetype="string" index="0" symbol="E" />
        
        //其他的属性,待定义
        public void CaseNode(XmlNode EthNode)
        {
            base.CaseNode( EthNode);
            
            //其他的继续实例
            
        }
    }


    [Export("TEthCmd", typeof(iEthCmd))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  

    public class TEthCmd :iEthCmd
    {
        XmlElement fileNodeXe;
        private List<TEthVariables> sendList = new List<TEthVariables>();
        private List<TEthVariables> receList = new List<TEthVariables>();
        //<cmd caption="VOLT" id="VOLT" sendlen="0" recelen="0" waittime="0" isreturnval="0" checktype="CRLF" submethod="Sub">
        private string caption;
        private string name;
        private int sendLen;
        private int receLen;
        private int waitTime;
        private int isreturnval;
        private string checkType;
        private string subMethod;
        /// <summary>
        /// 备注
        /// </summary>
        public string Caption 
        {
            get{ return caption;}
            set
            {
                caption = value;
                if ( fileNodeXe !=null)
                {
                    fileNodeXe.SetAttribute("caption", value);
                }
            }
        }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (fileNodeXe != null)
                {
                    fileNodeXe.SetAttribute("id", value);
                }
            }
        }
        /// <summary>
        /// 发送长度
        /// </summary>
        public int SendLen
        {
            get { return sendLen; }
            set
            {
                sendLen = value;
                if (fileNodeXe != null)
                {
                    fileNodeXe.SetAttribute("sendlen", value.ToString());
                }
            }
        }
        /// <summary>
        /// 接收长度
        /// </summary>
        public int ReceLen
        {
            get { return receLen; }
            set
            {
                receLen = value;
                if (fileNodeXe != null)
                {
                    fileNodeXe.SetAttribute("recelen", value.ToString());
                }
            }
        }
        /// <summary>
        /// 等待时间
        /// </summary>
        public int WaitTime
        {
            get { return waitTime; }
            set
            {
                waitTime = value;
                if (fileNodeXe != null)
                {
                    fileNodeXe.SetAttribute("waittime", value.ToString());
                }
            }
        }
        /// <summary>
        /// 有回复
        /// </summary>
        public int Isreturnval
        {
            get { return isreturnval; }
            set
            {
                isreturnval = value;
                if (fileNodeXe != null)
                {
                    fileNodeXe.SetAttribute("isreturnval", value.ToString());
                }
            }
        }
        /// <summary>
        /// 检查
        /// </summary>
        public string CheckType
        {
            get;
            set;
        }
        
        public string SubMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 对发送，或接收的实例
        /// </summary>
        /// <param name="aList"></param>
        /// <param name="fileNode"></param>
        private void caselist(List<TEthVariables> aList,XmlNode fileNode)
        {
            if (fileNode != null)
                foreach (XmlNode aVarNode in fileNode.ChildNodes)
                {
                    TEthVariables newVar = new TEthVariables();
                    aList.Add(newVar);
                    newVar.CaseNode(aVarNode);
                }
        }

        /// <summary>
        /// 对指令实例化
        /// </summary>
        /// <param name="fileNode"></param>
        public void CaseNode(XmlNode fileNode)
        {
            fileNodeXe = (XmlElement)fileNode;
            caption = fileNodeXe.GetAttribute("caption");
            name = fileNodeXe.GetAttribute("id");
            sendLen = Convert.ToInt32( fileNodeXe.GetAttribute("sendlen"));
            receLen = Convert.ToInt32(fileNodeXe.GetAttribute("recelen"));
            XmlNode aNode= fileNode.SelectSingleNode("sendlist");
            caselist(sendList, aNode);
            aNode = fileNode.SelectSingleNode("recelist");
            caselist(sendList, aNode);
        }

        //checktype="CRLF" submethod="Sub"
    }

    [Export("TEthernet", typeof(iEthernet))]
    [PartCreationPolicy(CreationPolicy.NonShared)] 
    public class TEthernet:iEthernet

    {
        private List<TEthCmd> EthList = new List<TEthCmd>();
        private XmlElement DocumentElement;
        private XmlDocument xDoc=new XmlDocument ();
        /// <summary>
        /// 端口号
        /// </summary>
        private int port;
        public int Port
        { get {return port;}
            set
            {
                port = value;
                if (DocumentElement!=null)
                {
                    DocumentElement.SetAttribute("port", value.ToString());
                }
            } }
        /// <summary>
        /// IP地址
        /// </summary>
        private string iP;
        public string IP
        {
            get { return iP; }
            set 
                {
                    iP = value;
                    if(DocumentElement!=null )
                    {
                        DocumentElement.SetAttribute("ip", value);
                    }
                }
        }
        /// <summary>
        /// 协议文件
        /// </summary>
        private string agreeMentFile;
        public string AgreeMentFile 
        {
            get { return agreeMentFile; }
            set 
            {
                if ( File.Exists(value))
                {
                    agreeMentFile = value;
                    xDoc.Load(agreeMentFile);
                    XmlNode cmdNode= xDoc.DocumentElement.SelectSingleNode("cmdlist");
                    DocumentElement = (XmlElement)cmdNode;
                    
                    iP = DocumentElement.GetAttribute("ipnumber");
                    port = Convert.ToInt32(DocumentElement.GetAttribute("port"));
                    foreach (XmlNode iNode in cmdNode.ChildNodes)
                    {
                        TEthCmd newcmd = new TEthCmd();
                        EthList.Add(newcmd);
                        newcmd.CaseNode(iNode);
                    }
                }
            } 
        }
        /// <summary>
        /// 根据变量名称，取对应的值
        /// </summary>
        /// <param name="sBlMc">变量名称</param>
        /// <returns></returns>
        public string GetByBlMc(string sBlMc)
        {
            return "";
        }
        /// <summary>
        /// 打开以太网口，成功为True;否则为 False;
        /// </summary>
        public bool OpenEth()
        {
            return true;
        }
        /// <summary>
        /// 关闭以太网口
        /// </summary>
        public void CloseEth()
        {
        }

        /// <summary>
        /// 找到对应序号的变量
        /// </summary>
        /// <param name="idx">索引位置</param>
        /// <returns></returns>
        public iEthVariables FindByIndex(int idx)
        { return null; }

        /// <summary>
        /// 找到对应名称指令的变量
        /// </summary>
        /// <param name="sName">对应的名称</param>
        /// <returns></returns>
        public iEthVariables FindByName(string sName)
        { return null; }



    }
}
