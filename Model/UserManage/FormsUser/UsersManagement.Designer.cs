namespace Nebula_People
{
    partial class FrmUsersManagement
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
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbCheck1 = new System.Windows.Forms.ComboBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lkDelete = new System.Windows.Forms.LinkLabel();
            this.cbCheck2 = new System.Windows.Forms.ComboBox();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUpdata = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewPassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvShow
            // 
            this.dgvShow.AllowUserToAddRows = false;
            this.dgvShow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShow.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.Location = new System.Drawing.Point(7, 15);
            this.dgvShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvShow.MultiSelect = false;
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.ReadOnly = true;
            this.dgvShow.RowHeadersVisible = false;
            this.dgvShow.RowTemplate.Height = 23;
            this.dgvShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShow.Size = new System.Drawing.Size(760, 472);
            this.dgvShow.TabIndex = 5;
            this.dgvShow.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShow_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.cbCheck1);
            this.groupBox2.Controls.Add(this.txtPassWord);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(773, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(321, 226);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新增用户";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(132, 170);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 29);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbCheck1
            // 
            this.cbCheck1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCheck1.FormattingEnabled = true;
            this.cbCheck1.Location = new System.Drawing.Point(99, 121);
            this.cbCheck1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCheck1.Name = "cbCheck1";
            this.cbCheck1.Size = new System.Drawing.Size(160, 23);
            this.cbCheck1.TabIndex = 2;
            this.cbCheck1.SelectedIndexChanged += new System.EventHandler(this.cbCheck1_SelectedIndexChanged);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(99, 80);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(160, 25);
            this.txtPassWord.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 125);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "权  限";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(99, 38);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(160, 25);
            this.txtUser.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "用户名";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lkDelete);
            this.groupBox1.Controls.Add(this.cbCheck2);
            this.groupBox1.Controls.Add(this.cbUser);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnUpdata);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNewPassWord);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(773, 251);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(321, 236);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "修改用户";
            // 
            // lkDelete
            // 
            this.lkDelete.AutoSize = true;
            this.lkDelete.Location = new System.Drawing.Point(269, 42);
            this.lkDelete.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lkDelete.Name = "lkDelete";
            this.lkDelete.Size = new System.Drawing.Size(37, 15);
            this.lkDelete.TabIndex = 4;
            this.lkDelete.TabStop = true;
            this.lkDelete.Text = "删除";
            this.lkDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lkDelete_LinkClicked);
            // 
            // cbCheck2
            // 
            this.cbCheck2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCheck2.FormattingEnabled = true;
            this.cbCheck2.Location = new System.Drawing.Point(99, 119);
            this.cbCheck2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCheck2.Name = "cbCheck2";
            this.cbCheck2.Size = new System.Drawing.Size(160, 23);
            this.cbCheck2.TabIndex = 2;
            this.cbCheck2.SelectedIndexChanged += new System.EventHandler(this.cbCheck2_SelectedIndexChanged);
            // 
            // cbUser
            // 
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(99, 38);
            this.cbUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(160, 23);
            this.cbUser.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 122);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "权  限";
            // 
            // btnUpdata
            // 
            this.btnUpdata.Location = new System.Drawing.Point(132, 169);
            this.btnUpdata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdata.Name = "btnUpdata";
            this.btnUpdata.Size = new System.Drawing.Size(93, 29);
            this.btnUpdata.TabIndex = 3;
            this.btnUpdata.Text = "修改";
            this.btnUpdata.UseVisualStyleBackColor = true;
            this.btnUpdata.Click += new System.EventHandler(this.btnUpdata_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名";
            // 
            // txtNewPassWord
            // 
            this.txtNewPassWord.Location = new System.Drawing.Point(99, 78);
            this.txtNewPassWord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNewPassWord.Name = "txtNewPassWord";
            this.txtNewPassWord.PasswordChar = '*';
            this.txtNewPassWord.Size = new System.Drawing.Size(160, 25);
            this.txtNewPassWord.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "新密码";
            // 
            // FrmUsersManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 504);
            this.Controls.Add(this.dgvShow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmUsersManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UsersManagement";
            this.Load += new System.EventHandler(this.UsersManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbCheck1;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lkDelete;
        private System.Windows.Forms.ComboBox cbCheck2;
        private System.Windows.Forms.ComboBox cbUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUpdata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewPassWord;
        private System.Windows.Forms.Label label2;

    }
}