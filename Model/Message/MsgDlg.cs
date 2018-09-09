using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Message;


namespace NeMessage
{
    public static class MsgDlg 
    {
        public static void ShowMsg(string mes)
        {
            ShowMsgInfo(mes);
        }
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="mess"></param>
        public static void ShowMsgInfo(string mess )
        {
           MessageBox.Show(mess, "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 循问的方式
        /// </summary>
        /// <param name="aCaption"></param>
        /// <returns></returns>
        public static bool MsgQuestion(string aCaption)
        {
           return  MessageBox.Show(aCaption, "Infomation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.Yes ;
            
        }

        public static void  MsgError(string aCaption)
        {
            MessageBox.Show(aCaption, "Infomation", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }



        #region 弹出Pup窗口提醒
        /// <summary>
        /// 弹出Pup窗口提醒
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="subject">提醒主题</param>
        /// <param name="msg">信息</param>
        public static void ShowPupUp(string title, string subject, string msg)
        {
            //string msg = "ss";
            int intervalTime = 6000;
            MessageForm messageform = new MessageForm(title,msg, intervalTime) { ShowInTaskbar = false };
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - messageform.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            Point p = new Point(x, y - messageform.Height);
            messageform.PointToScreen(p);
            messageform.Location = p;
            messageform.Show();
        }
        #endregion


    }
}
