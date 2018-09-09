using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iinterface
{
    public interface iCANVariables : iCommunicationBase
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        string VarName { get; set; }
        /// <summary>
        /// 变量备注说明
        /// </summary>
        string VarCaption { get; set; }

        /// <summary>
        /// 起始位
        /// </summary>
        int StartBit { get; set; }
        /// <summary>
        /// 位长度
        /// </summary>
        int BitLength { get; set; }
        /// <summary>
        /// Intel OR  Motorola模式
        /// </summary>
        int Model { get; set; }

        //其他的还有...
    }



    public interface iCANID
    {
        /// <summary>
        /// 报文ID
        /// </summary>
        string CANID { get; set; }
        /// <summary>
        /// 报文名称
        /// </summary>
        string CANName { get; set; }
        /// <summary>
        /// 报文备注
        /// </summary>
        string CANMemo { get; set; }


        iCANVariables FindByIndex(int idx);
        /// <summary>
        /// 根据变量名称获取对应的变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        iCANVariables FindByName(string sName);

    }

    public interface iBMS
    {
        string  FileName { get; set; }

        iCANID FindCANID(string sStringID);


        iCANVariables FindCANVar(string iVARname);


    }
    
}
