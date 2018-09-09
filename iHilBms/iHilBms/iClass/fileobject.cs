using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using System.IO;
using System.Xml;

namespace iHilBms.iClass
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class SysOptionObject:Ioptions
    {

        public  SysOptionObject()
        {
           iSysOption= THilConfig.gDll.CreateByContainer<Ioptions>   ("TOptions");
        }

        private XmlDocument xdoc = new XmlDocument();
        private string sysOptionFile;
        public Ioptions iSysOption { get; set; }
        public string SysOptionFile {
            get            
            {
                return sysOptionFile;
            }
           
            set
            {
               

                if (!File.Exists(sysOptionFile))
                {
                    return;
                }
                sysOptionFile = value;
                
                xdoc.Load(sysOptionFile);

                iSysOption.Case(xdoc.DocumentElement.SelectSingleNode("machine"));

            }
            
            }

    }
}
