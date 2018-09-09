using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nebula_db;
namespace Nebula_People
{
    public partial class FrmUsersManagement : Form
    {
        string Identity = "";
        string UpDateIdentity = "";
        string UsersID = "";
        public FrmUsersManagement()
        {
            InitializeComponent();
        }

        private void UsersManagement_Load(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void ResetControls()//初始化控件
        {
            try
            {
                cbCheck1.Items.Clear();
                cbCheck2.Items.Clear();
                cbCheck1.Items.Add("生产管理员");
                cbCheck1.Items.Add("生产操作员");
                cbCheck1.Items.Add("设备调试员");
                cbCheck2.Items.Add("生产管理员");
                cbCheck2.Items.Add("生产操作员");
                cbCheck2.Items.Add("设备调试员");
                //DataTable dtUserName = new DataTable();
                //dtUserName = MyDBCommand.GetUsersName();
                //if (dtUserName.Rows.Count>0)
                //{
                //    for (int i = 0; i < dtUserName.Rows.Count; i++)
                //    {
                //        cbUser.Items.Add(dtUserName.Rows[i]["LoginUsers"].ToString());
                //    }
                //}


                DataTable dt = new DataTable();
                dt=MyDBCommand.GetUsers();
                dgvShow.DataSource = dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex);
            }
        }

        private void lkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("警告：删除后无法恢复！请确定是否删除？\r\n用户：" + cbUser.Text, "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (UsersID!="")
                    {
                        if (MyDBCommand.DeleteUser(int.Parse(UsersID)))
                        {
                            MessageBox.Show("删除用户成功");
                        }
                        else
                        { 
                            MessageBox.Show("删除用户失败"); 
                        }
                    }
                }
                catch 
                {
                    MessageBox.Show("删除用户失败");
                }
                finally
                { ResetControls(); }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpIdentityID="";
                bool tmpSucced = false;
                if (Identity!="")
                {
                    DataTable dtId = new DataTable();
                    dtId = MyDBCommand.GetIdentity(Identity);
                    if (dtId.Rows.Count>0)
                    {
                        tmpIdentityID = dtId.Rows[0]["id"].ToString();  
                    }
                }
                tmpSucced = MyDBCommand.InsertUsers(txtUser.Text.Trim().ToString(), txtPassWord.Text.Trim().ToString(), tmpIdentityID);
                //tmpSucced = MyDBCommand.UpdateUser(txtUser.Text.Trim().ToString(), txtPassWord.Text.Trim().ToString(), 1);
                //tmpSucced = MyDBCommand.DeleteUser(3);
                if (tmpSucced)
                {
                    MessageBox.Show("用户信息新增成功！");
                    ResetControls();
                }
                else
                {
                    MessageBox.Show("用户信息新增失败！");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        private void cbCheck1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((ComboBox)sender).SelectedIndex;
                switch (index.ToString())
                {
                    case "0":
                        Identity = "生产管理员";
                        break;
                    case "1":
                        Identity = "生产操作员";
                        break;
                    default:
                        Identity = "设备调试员";
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvShow.SelectedRows.Count > 0)
            {
                UsersID = dgvShow.SelectedRows[0].Cells[0].Value.ToString().Trim();
                cbUser.Text = dgvShow.SelectedRows[0].Cells[1].Value.ToString().Trim();
                cbUser.Enabled = false;
            }
        }

        private void btnUpdata_Click(object sender, EventArgs e)
        {
            try
            {
                string tmpIdentityID = "";
                bool tmpSucced = false;
                if (UsersID != "")
                {
                    if (UpDateIdentity != "")
                    {
                        DataTable dtId = new DataTable();
                        dtId = MyDBCommand.GetIdentity(UpDateIdentity);
                        if (dtId.Rows.Count > 0)
                        {
                            tmpIdentityID = dtId.Rows[0]["id"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("不存在用户的身份信息！");
                        }
                        tmpSucced = MyDBCommand.UpdateUser(cbUser.Text.Trim().ToString(), txtNewPassWord.Text.Trim().ToString(), int.Parse(tmpIdentityID), int.Parse(UsersID));
                        if (tmpSucced)
                        {
                            MessageBox.Show("用户信息修改成功！");
                            ResetControls();
                        }
                        else
                        {
                            MessageBox.Show("用户信息修改失败！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选中需要修改的用户身份信息！");
                    }
                }
                else
                {
                    MessageBox.Show("请选中需要修改的用户！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cbCheck2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = ((ComboBox)sender).SelectedIndex;
                switch (index.ToString())
                {
                    case "0":
                        UpDateIdentity = "生产管理员";
                        break;
                    case "1":
                        UpDateIdentity = "生产操作员";
                        break;
                    default:
                        UpDateIdentity = "设备调试员";
                        break;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
