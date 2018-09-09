using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nebule_login;
using Nebula_People;
using iinterface;

namespace iHilBms.iFrom
{
    public partial class FrmMain : Form
    {
       // private CreateNewDllInstance fdll = new CreateNewDllInstance("ModelDLL");
       // private THilConfig ghil = new THilConfig();
       // private SysOptionObject SysOption ;
        public FrmMain()
        {
            InitializeComponent();
            
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //ghil.CreateLoginUserByReflection();

            //THilConfig.gOption.SysOptionFile = Application.StartupPath + @"\options\Hilconfig";

            //NeMessage.MsgDlg.ShowMsg(THilConfig.gOption.Port.ToString());

            if (THilConfig.giLoginUser.LoginName == null)
            {
                FrmLogin Frm = new FrmLogin(THilConfig.giLoginUser);
                if (Frm.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        private void menu_login_Click(object sender, EventArgs e)
        {
            FrmLogin Frm = new FrmLogin(THilConfig.giLoginUser);
            Frm.ShowDialog();
            
        }

        private void menu_people_Click(object sender, EventArgs e)
        {
            FrmUsersManagement frm = new FrmUsersManagement();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ILoginUser il1, il2;
            //il1 = THilConfig.gDll.CreateByContainer<ILoginUser>("TLoginUser");
            //il2 = THilConfig.gDll.CreateByContainer<ILoginUser>("TLoginUser");
            //il1.LoginName = "abc";
            //il2.LoginName = "abc222";
            //MessageBox.Show(il1.LoginName);
            //MessageBox.Show(il2.LoginName);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOptions frm = new FrmOptions();
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //ILoginUser il1, il2;
            //il1 = THilConfig.gDll.CreateByContainer<ILoginUser>("TLoginUser");
            //il2 = THilConfig.gDll.CreateByContainer<ILoginUser>("TLoginUser");
            //il1.LoginName = "abc";
            //il2.LoginName = "abc222";
            //MessageBox.Show(il1.LoginName);
            //MessageBox.Show(il2.LoginName);
            //NeMessage.MsgDlg.ShowPupUp("a", "b", "c");
            

        }

        private void bMSHILTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            FrmTest newFrm = new FrmTest();
            newFrm.MdiParent = this;
            newFrm.Show();
        }



//        添加引用的地址，修改config文件

//在根目录中打开“软件名.exe.config”文件，添加<runtime>中的语句。

//其中 probing privatePath 中的地址为子文件的名称。

//如果有多个子文件夹，两个地址用“;”隔开，如<probing privatePath="lib;bin;config/user"/>

//复制代码
// 1 <?xml version="1.0" encoding="utf-8" ?>
// 2 <configuration>
// 3     <startup> 
// 4         <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
// 5     </startup>
// 6 
// 7     <runtime>
// 8     <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
// 9     <probing privatePath="lib"/>
//10     </assemblyBinding>
//11     </runtime>
//12 
//13 </configuration>
//复制代码
    }
}
