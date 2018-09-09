using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Nebula
{
    public class xmlSerialize
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Serialization<T>(T t, string FilePath)
        {   //写
            try
            {
                string str = null;
                str += FilePath;
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.Encoding = Encoding.GetEncoding("gb2312");
                var encoding=Encoding.GetEncoding("gb2312");
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                if (File.Exists(str))
                {
                    using (StreamWriter sw = new StreamWriter(str,false,encoding))
                    {
                        using (XmlWriter xmlWriter = XmlWriter.Create(sw, settings))
                        {
                            // 强制指定命名空间，覆盖默认的命名空间  
                            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                            namespaces.Add(string.Empty, string.Empty);
                            xmlFormat.Serialize(xmlWriter, t, namespaces);
                            xmlWriter.Close();
                        };
                    }
                }
                else
                {
                    FileStream saveFile = new FileStream(str, FileMode.CreateNew, FileAccess.ReadWrite);
                    using (XmlWriter xmlWriter = XmlWriter.Create(saveFile, settings))
                    {
                        // 强制指定命名空间，覆盖默认的命名空间  
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                        namespaces.Add(string.Empty, string.Empty);
                        xmlFormat.Serialize(xmlWriter, t, namespaces);
                        xmlWriter.Close();
                    };
                    saveFile.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <typeparam name="T">放回类型</typeparam>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static T DeSerialization<T>(string FilePath)
        {  //读取
            try
            {
                string str = null;
                str = FilePath ;
                XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
                using (StreamReader sr = new StreamReader(str, Encoding.GetEncoding("gb2312")))
                {
                    T t = (T)xmlFormat.Deserialize(sr);
                    return t;
                }
            }
            catch
            {
                //throw ex;
                return default(T);
            }
        }
    }
}
