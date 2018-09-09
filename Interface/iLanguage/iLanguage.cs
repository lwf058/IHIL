using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iinterface
{
    public interface iLanguage
    {
        /// <summary>
        /// 文件名
        /// </summary>
        string LanguageFile { get; set; }

        /// <summary>
        /// 根据ID 显示不同的 建议ID为 6位数，前二位为员工代号，后4位为顺号。保证不冲突;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string Text(int id);

        

    }


}
