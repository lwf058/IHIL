using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using iinterface;
using Nebula_SysComm_Ini;
namespace TLanguage
{
    [Export("TLanguage", typeof(iLanguage))]
    [PartCreationPolicy(CreationPolicy.NonShared)]  
    public class TLanguage:iLanguage
    {
        private IniFileTools.IniFile inifile;
        private string languageFile;
        /// <summary>
        /// 文件名
        /// </summary>
        public string LanguageFile
        {
            get
            {
                return languageFile;
            }

            set
            {
                languageFile = value;
                inifile = new IniFileTools.IniFile(@languageFile);//创建INI文件实例

            }
        }

        public TLanguage (string afile)
        {
            LanguageFile = afile;
        }

        /// <summary>
        /// 根据ID 显示不同的 建议ID为 6位数，前二位为员工代号，后4位为顺号。保证不冲突;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Text(int id)
        {
            return inifile.ReadString("language",id.ToString(),"");  
        }
    }
}
