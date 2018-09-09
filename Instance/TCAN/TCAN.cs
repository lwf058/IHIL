using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iinterface;
using instance;
using System.IO;
using System.Xml;
using System.ComponentModel.Composition;
namespace TCAN
{



    [Export("TCan", typeof(iCan))]
    [PartCreationPolicy(CreationPolicy.NonShared)] 
    /// <summary>
    /// 某一路的CAN.
    /// </summary>
    public class  TCan:iCan
    {

        /// <summary>
        /// 用于装载协议文件中的BW。
        /// </summary>
        private List<TCANID> CanBwList = new List<TCANID>();
        /// <summary>
        /// 协议文件 绝对路径
        /// </summary>
        private string agreementFile;
        private XmlDocument xDoc = new XmlDocument();
        public string AgreementFile
        {
            get
            {
                return agreementFile;
            }
            set
            {
                if (File.Exists(value))
                {
                    agreementFile = value;
                    CanBwList.Clear();//                  
                    xDoc.Load(agreementFile);
                    //报文实例
                    foreach (XmlNode bmsCan in xDoc.DocumentElement.SelectSingleNode("bms").ChildNodes)
                    {
                        TCANID newCanid = new TCANID();
                        CanBwList.Add(newCanid);
                        newCanid.CaseNode(bmsCan);
                    }
                }
            }
        }
        /// <summary>
        /// 启动CAN卡,相关的配置，都在startCan中完成。
        /// </summary>
        /// <param name="Baut">波特率</param>
        /// <returns>正常为TRUE</returns>
        public bool StartCan(int Baut)
        {
            return true;
        }
        /// <summary>
        /// 停止CAN
        /// </summary>
        /// <returns>成功主TRUE</returns>
        public bool StopCan()
        {
            return true;
        }
        /// <summary>
        /// 根据变量名，获取对应变量的值
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public string GetVariableValue(string sName)
        { return ""; }
        /// <summary>
        /// 根据变量获取对应变量的说明；
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public string GetVariableCaption(string sName)
        { return ""; }
       

        /// <summary>
        /// 根据变量名称获取对应的变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        //public iCANVariables FindVarByName(string sName)
        //{

        //    iCANID aCanid = FindByName(sName);
        //    if (aCanid !=null)
        //    {
        //       aCanid.FindByName()
        //    }
        //    else
        //    return null;
        //}

        public iCANID FindByIndex(int idx)
        { return CanBwList[idx]; }
        /// <summary>
        /// 根据变量名称获取对应的变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public iCANID FindByName(string sName)
        {

            iCANID reCanid= CanBwList.Find( t  => ((iCANID)t).CANName  == sName   );

            return reCanid;

        }


    }
    [Export("TCANDevice", typeof(iCANDevice))]
    [PartCreationPolicy(CreationPolicy.NonShared)] 
    /// <summary>
    /// 某一类型 整个CAN卡的 连接；
    /// </summary>
    public class  TCANDevice:iCANDevice
    {
        private List<TCan> CANList=new List<TCan>() ;
        /// <summary>
        /// CAN的类型
        /// </summary>
        public int CANKind { get; set; }
        /// <summary>
        /// CAN的路总数
        /// </summary>
        public int CANNum { get; set; }

        /// <summary>
        /// CAN的连接
        /// </summary>
        public bool LinkCanDevice()
        { return true; }


        public iCan CANByIndex(int idx)
        {
            return CANList[idx];
        }


    }
}
