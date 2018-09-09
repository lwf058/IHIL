using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace iinterface
{
    public interface Ioptions
    {
        /// <summary>
        /// 设备端口
        /// </summary>
         int Port { get; set; }
        /// <summary>
        /// 设备IP
        /// </summary>
         string Ip { get; set; }
        /// <summary>
        /// 设备协议文件
        /// </summary>
         string Device { get; set; }
        /// <summary>
        /// 保存路径,测试数据的保存路径
        /// </summary>
         string SavePath { get; set; }
        /// <summary>
        /// 原则是对文件名一赋值 ，就实例化好了，但中间可能会有一定的修改。所以还是要开放这个接口；
        /// </summary>
        /// <param name="FileNode"></param>
         void Case(XmlNode FileNode);

         void Save();
         string SysOptionFile {   get; set; }
    }
}
