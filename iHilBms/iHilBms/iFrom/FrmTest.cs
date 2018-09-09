using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iHilBms.iFrom
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            treeViewKInd.Height = treeViewKInd.Parent.Height - 30;
            tabPaneTest.SelectedPageIndex = 0;
            this.WindowState = FormWindowState.Maximized;
            treeViewKInd.Height = Height - 30 - 35;
   

        }

        private void FrmTest_Resize(object sender, EventArgs e)
        {
            treeViewKInd.Height = treeViewKInd.Parent.Height - 30;
        }

        private void FrmTest_SizeChanged(object sender, EventArgs e)
        {
           // treeViewKInd.Height = treeViewKInd.Parent.Height - 30;
        }

        private void FrmTest_ResizeEnd(object sender, EventArgs e)
        {
            treeViewKInd.Height = treeViewKInd.Parent.Height - 30;
        }



        private void buttonEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
