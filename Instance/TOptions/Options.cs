using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using iinterface;
using System.Xml;
using Nebula_SysComm_String;
using System.IO;
namespace Options
{
    [Export("TOptions", typeof(Ioptions))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class TOptions:Ioptions
    {
        
        private XmlNode ffileNode = null;
        private XmlDocument xdoc = new XmlDocument();
        /// <summary>E:\work\iHil\Instance\TOptions\Options.cs
        /// 设备端口
        /// </summary>
        private int port;
        public int Port
        {
            get
            {
                return port;
            }              
            set
            {
                port = value;
                if (ffileNode != null)
                    ((XmlElement)ffileNode).SetAttribute("port", value.ToString());
            }
        }
        /// <summary>
        /// 设备IP
        /// </summary>
        private string ip;
        public string Ip
        {
            get
            { return ip; }
            set       

        {
            ip = value;
                if (ffileNode!=null)
                 ((XmlElement)ffileNode).SetAttribute("ip", value);}
        }
        private string device;
        /// <summary>
        /// 设备协议文件
        /// </summary>
        public string Device
        {
            get
            { return device; }
            set
            {
                device = value;
                if (ffileNode!=null)
                 ((XmlElement)ffileNode).SetAttribute("device", value);          
            }
    
        }
        /// <summary>
        /// 保存路径
        /// </summary>
        private string savePath;
        public string SavePath { get
            {
                return savePath;

            }
            set
            {
                savePath = value;
             if (ffileNode!=null)
                 ((XmlElement)ffileNode).SetAttribute("savepath", value);
            } 
            }


        private string sysOptionFile;
        public string SysOptionFile 
        {
            get            
            {
                return sysOptionFile;
            }
           
            set
            {


                if (!File.Exists(value))
                {
                    return;
                }

                sysOptionFile = value;
                xdoc.Load(sysOptionFile);

                Case(xdoc.DocumentElement.SelectSingleNode("machine"));

            }
            
            }
        public void Save()
        {
            xdoc.Save(sysOptionFile);
        }
    
        public void Case(XmlNode FileNode)
        {
            ffileNode = FileNode;
            XmlElement  FileNodeXe= (XmlElement)FileNode;
            Port = StringUtil.strtoint(FileNodeXe.GetAttribute("port").ToString());
            Ip = FileNodeXe.GetAttribute("ip");
            SavePath = FileNodeXe.GetAttribute("savepath");
            device = FileNodeXe.GetAttribute("device");
            savePath = FileNodeXe.GetAttribute("savepath");
        }

    }

}
