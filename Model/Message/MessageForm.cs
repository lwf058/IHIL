using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace Message
{
    public partial class MessageForm : Form
    {  
        public MessageForm(string title,string msg,int intervalTime)
        {
            InitializeComponent();
            this.msg = msg;
            msgLabel.Text = msg;
            msgLabel.Refresh();
            this.Text = title;
            this.intervalTime = intervalTime;
            ftitle=title;
        }

        private int intervalTime;

        private string msg;

        private string ftitle = "";

        private void timer1_Tick(object sender, EventArgs e)//执行的频率为2000，当弹出框为显示后2秒开始工作
        {
            OutTime = OutTime - 1;
            if (OutTime <1)
            {
                timer1.Enabled = false;
                Thread t = new Thread(FormDownAnimation) { IsBackground = true };
                t.Start();
            }
            else
            {
                Text = ftitle +  "  ("  + OutTime.ToString()+")";
            }
        }

        private delegate void UpdataDel();
        private Point p;
        private int i;
        private int OutTime;
        private void FormDownAnimation()
        {
            for ( i = 0; i <= this.Height; i++)
            {
                p = new Point(this.Location.X, this.Location.Y + 1);
                Animate();
                Thread.Sleep(10);
            }

        }
   
        private void Animate()
        {
            if (this.InvokeRequired)
            {
                UpdataDel d = new UpdataDel(Animate);
                this.Invoke(d);
            }
            else
            {
                try
                {
                    this.PointToScreen(p);
                    this.Location = p;
                    if (i > Height)
                        Close();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
            //timer1.Interval = intervalTime;
            //timer1.Interval 
            OutTime = 10;
            timer1.Enabled = true;    
        }
    }
}
