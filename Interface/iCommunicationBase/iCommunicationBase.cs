using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace iinterface
{
    /// <summary>
    /// 通讯的变量的基础类，各个类值，从这继承。
    /// </summary>
    public interface iCommunicationBase
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        string VarName { get; set; }
        /// <summary
        /// 变量显示值
        /// </summary>
        string VarCaption { get; set; }
        /// <summary>
        /// 变量单位
        /// </summary>
        string VarUnit { get; set; }
        /// <summary>
        ///变量值
        /// </summary>
        string VarValue { get; set; }
        /// <summary>
        /// 变量的前辍；
        /// </summary>
        string Prev { get; set; }

        


    }
}
