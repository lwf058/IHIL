using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace WpfApplication1
{
    public class Helper
    {
        #region 弹出Pup窗口提醒
        /// <summary>
        /// 弹出Pup窗口提醒
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="subject">提醒主题</param>
        /// <param name="msg">信息</param>
        public static void ShowPupUp(string title, string subject, string msg)
        {
            if (String.IsNullOrEmpty(title))
                title = "提醒";

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Control.PopUP pop = new Control.PopUP();
                pop.Subject = subject;
                pop.Msg = msg;
                pop.PopTitle = title;
                pop.Show();
            }));
        }
        #endregion
    }
}
