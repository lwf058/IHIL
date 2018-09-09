using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using System.ComponentModel.Composition;
using System.Xml;
namespace instance
{
    [Export("TCommuniationBase", typeof(iCommunicationBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class TCommuniationBase : iCommunicationBase
    {
        private XmlElement fileNodeXe;
        public XmlElement FileNodeXe
        {
            get
            { return fileNodeXe; }
            set
            {
                fileNodeXe = value;
            }
        }
        /// <summary>
        /// 变量名称
        /// </summary>
        private string varName;
        public string VarName 
        { get 
            {return varName;}
            set
            {
                varName = value;
                if (fileNodeXe != null)
                    fileNodeXe.SetAttribute("id", value);
            }
        }
        /// <summary
        /// 变量显示值
        /// </summary>
        private string varCaption;
        public string VarCaption 
        { get 
            {return varCaption;}
            set
            {
                varCaption = value;
                if (fileNodeXe != null)
                    fileNodeXe.SetAttribute("caption", value);
            }
        }

        /// <summary>
        /// 变量单位
        /// </summary>
        private string varUnit;
        public string VarUnit
        {
            get
            { return varUnit; }
            set
            {
                varUnit = value;
                if (fileNodeXe != null)
                    fileNodeXe.SetAttribute("dw", value);
            }
        }
        /// <summary>
        ///变量值
        /// </summary>
        public string VarValue { get; set; }
        /// <summary>
        /// 变量的前辍；
        /// </summary>
        private string prev;
        public string Prev
        {
            get
            { return prev; }
            set
            {
                prev = value;

            }
        }


        public void CaseNode(XmlNode filenode)
        {
            fileNodeXe = (XmlElement) filenode;
            varName = fileNodeXe.GetAttribute("id");
            varCaption = fileNodeXe.GetAttribute("caption");
            varUnit = fileNodeXe.GetAttribute("unit");

        }

    }
}
