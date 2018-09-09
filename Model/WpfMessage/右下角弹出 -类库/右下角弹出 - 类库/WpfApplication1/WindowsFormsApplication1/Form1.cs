using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

         ShowPupUp("标题3333", "内容22222", "");
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
            if (String.IsNullOrEmpty(title))
                title = "提醒";

            //System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            //{
                WpfApplication1.Control.PopUP pop = new WpfApplication1.Control.PopUP();
                pop.Subject = subject;
                pop.Msg = msg;
                pop.PopTitle = title;
                pop.Show();
            //}));
        }
        #endregion
    }
}
