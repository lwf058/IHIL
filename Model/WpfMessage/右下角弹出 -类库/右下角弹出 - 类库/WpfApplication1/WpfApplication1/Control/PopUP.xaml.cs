using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApplication1.Control
{
    /// <summary>
    /// PopUP.xaml 的交互逻辑
    /// </summary>
    public partial class PopUP : Window
    {
        private DispatcherTimer _timer;//时钟
        private DateTime _startTime;//开始计时时间

        /// <summary> 消息主题 </summary>
        public string Subject { get; set; }
        /// <summary>  消息内容 </summary>
        public string Msg { get; set; }
        /// <summary> 窗口标题 </summary>
        public string PopTitle { get; set; }
        public PopUP()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.WorkArea.Width;//屏幕宽度
            double screenHeight = SystemParameters.WorkArea.Height;//屏幕高度
            this.Top = screenHeight - this.Height + 40;//左上角坐标
            this.Left = screenWidth - this.Width;//左上角坐标

            AnimationShow();//动画显示
            this._startTime = DateTime.Now;
            //1秒执行一次
            this._timer = Tool.MyTimer.setInterval(1000, () =>
            {
                //5秒关闭
                if (((TimeSpan)(DateTime.Now - this._startTime)).Seconds >= 5)
                {
                    this._timer.Stop();
                    AnimationHidden();
                }
            });
        }

        /// <summary>
        /// 动画显示
        /// </summary>
        private void AnimationShow()
        {
            DoubleAnimation dax = new DoubleAnimation();
            DoubleAnimation day = new DoubleAnimation();

            //指定起点  
            //长和宽一起变化
            //dax.From = this.Width;
            //day.From = this.Height;
            //只变化高度（从下往上）
            dax.From = 0;
            day.From = this.Height;
            //只变化宽度(从左往右)
            //dax.From = this.Width;
            //day.From = 0;

            //指定终点  
            dax.To = 0;
            day.To = 0;
            //指定时长  
            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            dax.Duration = duration;
            day.Duration = duration;
            //动画主体是TranslatTransform变形，而非Button  
            this.tt.BeginAnimation(TranslateTransform.XProperty, dax);
            this.tt.BeginAnimation(TranslateTransform.YProperty, day);
        }
        /// <summary>
        /// 动画隐藏
        /// </summary>
        private void AnimationHidden()
        {
            DoubleAnimation dax = new DoubleAnimation();
            DoubleAnimation day = new DoubleAnimation();
            //指定起点  
            dax.From = 0;
            day.From = 0;
            //指定终点  
            dax.To = 0;
            day.To = this.Height;
            //指定时长  
            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            dax.Duration = duration;
            day.Duration = duration;
            //动画结束后事件
            dax.Completed += dax_Completed;

            //动画主体是TranslatTransform变形，而非Button  
            this.tt.BeginAnimation(TranslateTransform.XProperty, dax);
            this.tt.BeginAnimation(TranslateTransform.YProperty, day);
        }

        //动画执行完后的事件
        void dax_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
        //窗体加载时候赋值
        private void PopWin_Loaded(object sender, RoutedEventArgs e)
        {
            this.Msg = this.Msg == null ? "" : this.Msg;
            this.Subject = this.Subject == null ? "" : this.Subject;
            this.lblTitle.Content = this.PopTitle == null ? "系统信息" : this.PopTitle;

            if (this.Subject.Length == 0)
            {
                this.txtInfo.Inlines.Remove(this.txtSubject);
            }
            else
            {
                this.txtSubject.Text = this.Subject.Length > 18 ? this.Subject.Substring(0, 18) + "..." : this.Subject;//超过18个换行
                this.txtSubject.Text += "\r\n";
            }
            this.txtContent.Text = (this.Msg.Length > 58 ? this.Msg.Substring(0, 58) + "..." : this.Msg);//消息多余58个就用省略号替代
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            AnimationHidden();
        }
    }
}
