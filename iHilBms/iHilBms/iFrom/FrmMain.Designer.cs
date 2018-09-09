namespace iHilBms.iFrom
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.StripMenuItemUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_login = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_people = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSel = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FindtsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bMSHILTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bmsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ComMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Close.ico");
            this.imageList2.Images.SetKeyName(1, "test.ico");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bg.jpg");
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItemUser,
            this.方案ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.ToolMItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(623, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // StripMenuItemUser
            // 
            this.StripMenuItemUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.menu_login,
            this.toolStripMenuItem2,
            this.menu_people,
            this.toolStripMenuItem3,
            this.menu_exit});
            this.StripMenuItemUser.Name = "StripMenuItemUser";
            this.StripMenuItemUser.Size = new System.Drawing.Size(54, 24);
            this.StripMenuItemUser.Text = "User";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
            // 
            // menu_login
            // 
            this.menu_login.Name = "menu_login";
            this.menu_login.Size = new System.Drawing.Size(192, 26);
            this.menu_login.Text = "Login...";
            this.menu_login.Click += new System.EventHandler(this.menu_login_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 6);
            // 
            // menu_people
            // 
            this.menu_people.Name = "menu_people";
            this.menu_people.Size = new System.Drawing.Size(192, 26);
            this.menu_people.Text = "User Manage...";
            this.menu_people.Click += new System.EventHandler(this.menu_people_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 6);
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(192, 26);
            this.menu_exit.Text = "Exit";
            // 
            // 方案ToolStripMenuItem
            // 
            this.方案ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem,
            this.ToolStripMenuItemSel});
            this.方案ToolStripMenuItem.Name = "方案ToolStripMenuItem";
            this.方案ToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.方案ToolStripMenuItem.Text = "scheme";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.ToolStripMenuItem.Text = "Design";
            // 
            // ToolStripMenuItemSel
            // 
            this.ToolStripMenuItemSel.Name = "ToolStripMenuItemSel";
            this.ToolStripMenuItemSel.Size = new System.Drawing.Size(181, 26);
            this.ToolStripMenuItemSel.Text = "Select...";
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindtsMenuItem,
            this.bMSHILTestToolStripMenuItem});
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.测试ToolStripMenuItem.Text = "HIl Test";
            // 
            // FindtsMenuItem
            // 
            this.FindtsMenuItem.Name = "FindtsMenuItem";
            this.FindtsMenuItem.Size = new System.Drawing.Size(182, 26);
            this.FindtsMenuItem.Text = "数据查看";
            // 
            // bMSHILTestToolStripMenuItem
            // 
            this.bMSHILTestToolStripMenuItem.Name = "bMSHILTestToolStripMenuItem";
            this.bMSHILTestToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.bMSHILTestToolStripMenuItem.Text = "BMS-HIL Test";
            this.bMSHILTestToolStripMenuItem.Click += new System.EventHandler(this.bMSHILTestToolStripMenuItem_Click);
            // 
            // ToolMItem
            // 
            this.ToolMItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bmsMenuItem,
            this.ComMenuItem,
            this.NetMenuItem,
            this.OptionToolStripMenuItem});
            this.ToolMItem.Name = "ToolMItem";
            this.ToolMItem.Size = new System.Drawing.Size(61, 24);
            this.ToolMItem.Text = "Tools";
            // 
            // bmsMenuItem
            // 
            this.bmsMenuItem.Name = "bmsMenuItem";
            this.bmsMenuItem.Size = new System.Drawing.Size(182, 26);
            this.bmsMenuItem.Text = "BMS edit";
            // 
            // ComMenuItem
            // 
            this.ComMenuItem.Name = "ComMenuItem";
            this.ComMenuItem.Size = new System.Drawing.Size(182, 26);
            this.ComMenuItem.Text = "Comm Edit";
            // 
            // NetMenuItem
            // 
            this.NetMenuItem.Name = "NetMenuItem";
            this.NetMenuItem.Size = new System.Drawing.Size(182, 26);
            this.NetMenuItem.Text = "Ethernet  Edit";
            // 
            // OptionToolStripMenuItem
            // 
            this.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem";
            this.OptionToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.OptionToolStripMenuItem.Text = "Option...";
            this.OptionToolStripMenuItem.Click += new System.EventHandler(this.OptionToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 469);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItemUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menu_login;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menu_people;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem 方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSel;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FindtsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bMSHILTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolMItem;
        private System.Windows.Forms.ToolStripMenuItem bmsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ComMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionToolStripMenuItem;

    }
}