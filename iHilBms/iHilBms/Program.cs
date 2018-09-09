using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeSplash;
using NeMessage;
namespace iHilBms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        //            private void DoSplash()
        //{
            //Frmsplash sp = new Frmsplash();
            //sp.Show();
            THilConfig.gOption.SysOptionFile = Application.StartupPath + @"\options\Hilconfig";

            //NeMessage.MsgDlg.ShowMsg(THilConfig.gOption.Port.ToString());
            //WpfMessage.Helper.ShowPupUp("a", "b", "c");
            //  .MsgDlg.ShowMsgInfo(THilConfig.gOption.Ip);

       // }
            Application.Run(new iFrom.FrmMain());
        }
    }
}
