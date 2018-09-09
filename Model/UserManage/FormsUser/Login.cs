using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nebula_db;
//using HILConst;
using Nebula_SysComm_String;
//using HIL;
//using HIL.Forms;
using System.Text.RegularExpressions;
using iinterface;
namespace Nebule_login
{
    public partial class FrmLogin : Form
    {
        private ILoginUser floginUser;
        public FrmLogin(ILoginUser gloginuser)
        {
            floginUser = gloginuser;
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            //RewriteTextBox.SetControlRoundRectRgn(textBox1, 30);
            //RewriteTextBox.SetControlRoundRectRgn(textBox2, 70);
            //RewriteTextBox.SetControlRoundRectRgn(btnLogin, 10);
            RewriteTextBox.SetFormRoundRectRgn(this, 10);

            string tmpSQL = "";
            tmpSQL = "select * from UsersManage";
            DataSet ds = new DataSet();
            ds = MyDBHelper.GetMyDBData(tmpSQL, "UsersManage");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBoxUser.Items.Add(ds.Tables[0].Rows[i]["LoginUsers"].ToString());
            }
            if (comboBoxUser.Items.Count>0)
            {
                comboBoxUser.SelectedIndex = 0;
            }
            if (comboBoxUser.CanFocus)
            {
                comboBoxUser.Focus();
            }

        }

        private void btnLogin_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnLogin.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\blue_b.png");
        }

        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogin.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\blue_F.png");
        }

        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\green_b.png");
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Images\\green_F.png");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //FrmMain FrmMain;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin();
        }
        void UserLogin()
        {
            try
            {
                string tmpSQL = "";
                //tmpSQL = "select * from UsersManage where LoginUsers='" + txtUser.Text.Trim().ToString() + "' and LoginPassWord='" + txtPassWord.Text.Trim().ToString() + "'";
                tmpSQL = "select * from UsersManage where LoginUsers='" + comboBoxUser.Text.Trim().ToString() + "' and LoginPassWord='" + txtPassWord.Text.Trim().ToString() + "'";
                DataSet ds = new DataSet();
                ds = MyDBHelper.GetMyDBData(tmpSQL, "UsersManage");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    floginUser.LoginName = ds.Tables[0].Rows[0]["LoginUsers"].ToString();
                    floginUser.LoginState = (UserState)(StringUtil.strtoint(ds.Tables[0].Rows[0]["LoginUsers"].ToString()) - 1);
                    //MessageBox.Show("登录成功！");
                    floginUser.Loginpermission = int.Parse(ds.Tables[0].Rows[0]["Permission"].ToString());
                    switch (floginUser.Loginpermission)
                    {
                        case 3:
                            {
                                this.Visible = false;
                                //frmHilTest frmEolTest = new frmHilTest();
                                //frmEolTest.WindowState = FormWindowState.Maximized;
                                //frmEolTest.Show();
                                //if (this.FrmMain != null)
                                //    this.FrmMain.Close();
                                this.DialogResult = DialogResult.OK;
                                break;
                            }
                        case 2: goto case 1;
                        case 1:
                            {
                                this.Visible = false;
                                //if (this.FrmMain == null)
                                //{
                                //    this.FrmMain = new FrmMain();
                                //    this.FrmMain.Show();
                                //    this.Visible = false;
                                //}
                                //else
                                //{
                                //    this.FrmMain.Visible = true;
                                //}
                                this.DialogResult = DialogResult.OK;
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("密码错误，登录失败！");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void txtPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                btnLogin_Click(sender, e);
            }
            string patten = "\\r";
            Regex r = new Regex(patten);
            Match m = r.Match(e.KeyChar.ToString());
            if (m.Success)
            {
                UserLogin();
            }
        }

        private void txtPassWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            string patten = "\\r";
            Regex r = new Regex(patten);
            Match m = r.Match(e.KeyChar.ToString());
            if (m.Success)
            {
                UserLogin();
            }
        }
    }
}
