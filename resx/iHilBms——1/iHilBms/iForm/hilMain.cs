using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iHilBms.iForm
{
    public partial class hilMain : Form
    {
        public hilMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ghil.giLoginUser.LoginName = "abc";
            textBox1.Text = ghil.giLoginUser.LoginName;
        }
    }
}
