using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using iinterface;
namespace instance
{
    /// <summary>
    /// 通讯变量的 基础类.可延伸 串口\以太网\CAN,加上各自的属性.
    /// </summary>
    [Export("Tvariables", typeof(ivariables))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class Tvariables : ivariables
    {
        //名称
        public string blmc { get; set; }
        //单位
        public string bldw { get; set; }
        //值 ,统一为字符型,根据 blkind进行转换
        public string blvalue { get; set; }
        public string blkind { get; set; }
        public string blcaption { get; set; }
    }
    /// <summary>
    /// 串口的类
    /// </summary>
    [Export("TComVariables", typeof(ivariables))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class TComVariables:Tvariables, iCOMvariables
    {

    }


    /// <summary>
    /// telnet的类
    /// </summary>
    [Export("TNetVariables", typeof(iETHvariables))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TETHVariables : Tvariables,iETHvariables
    {
        public void AB()
        {
           
        }

    }

    /// <summary>
    /// can的类
    /// </summary>
    [Export("TCANVariables", typeof(iCANvariables))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TCANVariables : Tvariables,iCANvariables
    {

    }



}
