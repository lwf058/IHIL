using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfMessage.Tool
{
    class MyTimer
    {
        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="millsecond"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DispatcherTimer setInterval(int millsecond, Action action)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millsecond);
            timer.Tick += (s, e) =>
            {
                action();
            };
            timer.Start();
            return timer;
        }
        /// <summary>
        /// timeout 
        /// </summary>
        /// <param name="millsecond"></param>
        /// <param name="action"></param>
        public static void setTimeout(int millsecond, Action action)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(millsecond);
            timer.Tick += (s, e) =>
            {
                action();
            };
            timer.Start();
            timer.Stop();
        }

    }
}
