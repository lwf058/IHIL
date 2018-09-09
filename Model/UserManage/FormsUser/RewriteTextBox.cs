using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Controls;


namespace Nebule_login
{
    class RewriteTextBox
    {
        #region  设置窗体弧度
        [DllImport("gdi32.dll")]
        public static extern int CreateRoundRectRgn(int x1, int y1, int x2, int y2, int x3, int y3);
        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject", CharSet = CharSet.Ansi)]
        public static extern int DeleteObject(int hObject);

        // <summary>         
        /// 设置窗体的圆角矩形         
        /// </summary>          
        /// <param name="form">需要设置的窗体</param>         
        /// <param name="rgnRadius">圆角矩形的半径</param>          
        public static void SetFormRoundRectRgn(Form _form, int _rgnRadius)
        {
            int hRgn = 0;
            hRgn = CreateRoundRectRgn(0, 0, _form.Width + 1, _form.Height + 1, _rgnRadius, _rgnRadius);
            SetWindowRgn(_form.Handle, hRgn, true);
            DeleteObject(hRgn);
        }
        /// <summary>
        /// 设置控件的圆角矩形
        /// </summary>
        /// <param name="control">需要设置的控件</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        public static void SetControlRoundRectRgn(System.Windows.Forms.Control _control, int _rgnRadius)
        {
            int hRgn = 0;
            hRgn = CreateRoundRectRgn(0, 0, _control.Width + 1, _control.Height + 1, _rgnRadius, _rgnRadius);
            SetWindowRgn(_control.Handle, hRgn, true);
            DeleteObject(hRgn);
        }
        #endregion

        /// <summary>
        /// 多行时，垂直居中
        /// </summary>
        /// <param name="_textBox"></param>
        public static void SetCenterText(TextBox _textBox)
        {
            if (_textBox.Multiline == true)
            {
                //TODO：计算文本框文字框高度，并居中
            }
        }

    }
}
