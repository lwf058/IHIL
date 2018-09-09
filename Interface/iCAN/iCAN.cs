using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iinterface
{




    /// <summary>
    /// 某一路的CAN.
    /// </summary>
    public interface iCan
    {
        /// <summary>
        /// 协议文件
        /// </summary>
        string AgreementFile{get;set;}
        /// <summary>
        /// 启动CAN卡,相关的配置，都在startCan中完成。
        /// </summary>
        /// <param name="Baut">波特率</param>
        /// <returns>正常为TRUE</returns>
        bool StartCan(int Baut);
        /// <summary>
        /// 停止CAN
        /// </summary>
        /// <returns>成功主TRUE</returns>
        bool StopCan();
        /// <summary>
        /// 根据变量名，获取对应变量的值
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        string GetVariableValue(string sName);
        /// <summary>
        /// 根据变量获取对应变量的说明；
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        string GetVariableCaption(string sName);
        /// <summary>
        /// 根据索引 号获取对应的变量
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        iCANID FindByIndex(int idx);
        /// <summary>
        /// 根据变量名称获取对应的变量
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        iCANID FindByName(string sName);

        ///// <summary>
        ///// 根据变量名称获取对应的变量
        ///// </summary>
        ///// <param name="sName"></param>
        ///// <returns></returns>
        //iCANVariables FindVarByName(string  sName);



    }
    /// <summary>
    /// 某一类型 整个CAN卡的 连接；
    /// </summary>
    public interface iCANDevice
    {
        /// <summary>
        /// CAN的类型
        /// </summary>
        int CANKind { get; set; }
        /// <summary>
        /// CAN的路总数
        /// </summary>
        int CANNum { get; set; }

        /// <summary>
        /// CAN的连接
        /// </summary>
        bool LinkCanDevice();
        /// <summary>
        /// 获取某一路的CAN
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        iCan CANByIndex(int idx);

       
    }

}
