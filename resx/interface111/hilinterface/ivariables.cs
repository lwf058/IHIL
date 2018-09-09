using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
namespace iinterface
{
    public interface ivariables
    {
        //名称
        string blmc { get; set; }
        //单位
        string bldw { get; set; }
        //值 ,统一为字符型,根据 blkind进行转换
        string blvalue { get; set; }
        string blkind { get; set; }
        string  blcaption { get; set; }
    }

    /// <summary>
    /// 串口
    /// </summary>
    public interface iCOMvariables : ivariables
    { }

    /// <summary>
    /// cCAN通讯
    /// </summary>
    public interface iCANvariables : ivariables
    { }
    /// <summary>
    ///ethernet 通讯
    /// </summary>
    public interface iETHvariables : ivariables
    { 
    }

}
