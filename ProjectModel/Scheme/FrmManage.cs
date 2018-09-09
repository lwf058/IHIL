using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EOL.eolUI;
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Nebula_SysComm_language;
using Nebula_SysComm_Direct;
using DevExpress.XtraEditors;
using eolConst;
using Nebula_SysComm_Dlg;
using EOL.Forms;
using EOL.units;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.Devices;
using automation.Forms;
using Aspose.Cells;


namespace EOL.Demo
{
    public partial class FrmManage : Form
    {
        packeol currentpackeol;
        step currentstep;
        itemsend currentitemsend;
        DataTable StepTable = new DataTable();
        DataTable CmdTable = new DataTable();
        /// <summary>
        /// 跨方案复制工歩
        /// </summary>
        private List<step> ListSCopy = new List<step>();
        /// <summary>
        /// 跨方案复制指令
        /// </summary>
        private List<itemsend> ListCmdCopy = new List<itemsend>();
        eolSystem tSysSet = new eolSystem();
        public FrmManage()
        {
            InitializeComponent();
            this.text.Width = (int)((this.gridStep.Width - this.check.Width) * 0.55);
            this.steptype.Width = (int)((this.gridStep.Width - this.check.Width) * 0.32);
            this.gridviewStep.Appearance.OddRow.BackColor = Color.White;
            this.gridviewStep.Appearance.EvenRow.BackColor = Color.WhiteSmoke;
            this.gridviewStep.OptionsView.EnableAppearanceEvenRow = true;
            this.gridviewStep.OptionsView.EnableAppearanceOddRow = true;

            this.gridViewCmd.Appearance.OddRow.BackColor = Color.White;
            this.gridViewCmd.Appearance.EvenRow.BackColor = Color.WhiteSmoke;
            this.gridViewCmd.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewCmd.OptionsView.EnableAppearanceOddRow = true;

            #region 20170117zoe
            try
            {
                if (File.Exists(tSysSet.SysPath))
                {
                    tSysSet = xmlSerialize.DeSerialization<eolSystem>(tSysSet.SysPath);
                    gControlCode.DataSource = MyMethod.ToDataTable<CodeItem>(tSysSet.CodeList);

                    gControlSet.DataSource = MyMethod.ToDataTable<SystemItem>(tSysSet.SetList);

                    gridDtcControl.DataSource = MyMethod.ToDataTable<BmsDTC>(tSysSet.DTCList);
                }
            }
            catch
            {
                MessageBox.Show("文件加载失败！");
            }
            #endregion
        }
        object Clone(object obj)
        {
            BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, obj);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }

        void AddComToCombox()
        {
            Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
            foreach (string str in pc.Ports.SerialPortNames)
            {
                ch1comboBox1.Items.Add(str);
                ch1comboBox2.Items.Add(str);
                ch1comboBox3.Items.Add(str);
                ch1comboBox4.Items.Add(str);
                ch1comboBox5.Items.Add(str);

                ch2comboBox1.Items.Add(str);
                ch2comboBox2.Items.Add(str);
                ch2comboBox3.Items.Add(str);
                ch2comboBox4.Items.Add(str);
                ch2comboBox5.Items.Add(str);
            }
        }
        private void btnAddSchems_Click(object sender, EventArgs e)
        {
            Frminput frm = new Frminput(glanguage.ReadCaption(310011), glanguage.ReadCaption(310019));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string Dirs = frm.Res;
                if (!DirectoryOperate.IsValidFileName(Dirs))
                {//非法字符
                    Nebula_SysComm_Dlg.MsgDlg.ShowMsg(glanguage.ReadCaption(310020));
                    return;
                }
                string newdirs;
                if (treeClass.FocusedNode.ParentNode != null)
                    newdirs = getfolder(treeClass.FocusedNode, "");
                else
                    newdirs = this.FilePath;
                newdirs = newdirs + Dirs;
                if (Directory.Exists(newdirs))
                {//已存在
                    Nebula_SysComm_Dlg.MsgDlg.ShowMsg(glanguage.ReadCaption(310021));
                    return;
                }
                Directory.CreateDirectory(newdirs);
                TreeListNode newnode = this.treeClass.AppendNode(new object[] { Dirs }, treeClass.FocusedNode);
                newnode.Selected = true;
            }
        }
        string FilePath = @"category\";
        private void FrmManage_Load(object sender, EventArgs e)
        {
            AddComToCombox();
            TreeListNode FileNode = this.treeClass.AppendNode(null, null);
            FileNode.SetValue(this.treeClass.Columns["FolderName"], "类别");
            TraverseFolder(this.FilePath, FileNode);
            treeClass.FocusedNode = FileNode;
            TraverseFild(getfolder(FileNode, ""));
            TraverseStep(treeFile.FocusedNode);
        }
        /// <summary>
        /// 遍历文件夹
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="treelist"></param>
        void TraverseFolder(string filepath, TreeListNode treelist = null)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(filepath);
            if (!TheFolder.Exists) return;
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                TreeListNode FileNode = this.treeClass.AppendNode(null, treelist);
                FileNode.SetValue(this.treeClass.Columns["FolderName"], NextFolder.Name);
                TraverseFolder(filepath + NextFolder.Name + "\\", FileNode);
            }
        }
        /// <summary>
        /// 遍历文件
        /// </summary>
        /// <param name="filepath"></param>
        void TraverseFild(string filepath)
        {
            this.treeFile.ClearNodes();
            this.clearAll();
            this.currentpackeol = null;
            DirectoryInfo TheFolder = new DirectoryInfo(filepath);
            if (!TheFolder.Exists) return;
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                TreeListNode ParentNode = this.treeFile.AppendNode(null, null);
                ParentNode.SetValue(this.treeFile.Columns["FileName"], NextFile.Name.Replace(".eol", ""));
            }
            if (treeFile.Nodes.Count > 0)
            {
                TraverseStep(treeFile.Nodes[0]);
            }
        }
        /// <summary>
        /// 获取选中文件夹的路径
        /// </summary>
        /// <param name="node"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        string getfolder(TreeListNode node, string folder)
        {
            if (node.ParentNode == null)
            {
                return this.FilePath + folder;
            }
            else
            {
                string newfolder = node.GetValue(this.treeClass.Columns["FolderName"]).ToString() + "\\" + folder;
                return getfolder(node.ParentNode, newfolder);
            }
        }
        private void treeClass_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (e.Node != null)
                TraverseFild(getfolder(e.Node, ""));
        }

        private void btnDelScheme_Click(object sender, EventArgs e)
        {
            if (this.treeClass.FocusedNode == null || this.treeClass.FocusedNode.GetValue(this.treeClass.Columns["FolderName"]) == "类别")
                return;
            DialogResult dr = XtraMessageBox.Show("您确定要删除吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DirectoryInfo di = new DirectoryInfo(getfolder(treeClass.FocusedNode, ""));
                di.Delete(true);
                clearAll();
                treeClass.DeleteNode(this.treeClass.FocusedNode);

            }
        }

        private void btnAddclass_Click(object sender, EventArgs e)
        {
            Frminput frm = new Frminput(glanguage.ReadCaption(310011), glanguage.ReadCaption(310019));
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string Dirs = frm.Res;
            if (!DirectoryOperate.IsValidFileName(Dirs))
            {//非法字符
                Nebula_SysComm_Dlg.MsgDlg.ShowMsg(glanguage.ReadCaption(310020));
                return;
            }
            string path = getfolder(treeClass.FocusedNode, "");
            if (File.Exists(path + Dirs + uconst.c_cate_ext))
            {//方案已存在
                if (!MsgDlg.MsgQuestion(glanguage.ReadCaption(310026)))
                {//不替换
                    return;
                }
            }
            packeol packeol = new eolUI.packeol();
            packeol.steplist.step.Add(new step()
            {
                text = "初始化",
                kind = "",
                check = "-1"
            });
            packeol.steplist.step.Add(new step()
            {
                text = "结束测试",
                kind = "endtest",
                check = "-1"
            });

            for (int i = 0; i < 8; i++)
            {
                packeol.machine.cannetList.Add(new cannet()
                {
                    machineindex = 0,
                    chindex = i % 4,
                    check = "0",
                    file = "",
                    btl = "3",
                    chip = "",
                    chport = (4000 + i % 4).ToString()
                });
            }
            for (int i = 0; i < 5; i++)
            {
                packeol.machine.comset.Add(new comitem()
                {
                    key = "",
                    bz = "",
                    text = "",
                    check = "0",
                    file = "",
                    btl = "",
                    ch1index = "",
                    ch2index = ""
                });
            }
            for (int i = 0; i < 2; i++)
            {
                packeol.machine.telect.Add(new netitem()
                {
                    bz = "",
                    text = "",
                    check = "0",
                    file = "",
                    IP = "169.254.4.10",
                    PORT = "5025",
                    ip = "169.254.4.10",
                    port = "5025",
                    porttwo = "5025"
                });
            }
            packeol.custbllist.custbl.Add(new custitem()
            {
                id = "1",
                blmc = "NOT_EXIST",
                dw = "",
                caption = "过渡",
                blgs = ""
            });
            packeol.custbllist.custbl.Add(new custitem()
            {
                id = "2",
                blmc = "gkdh",
                dw = "",
                caption = "工况项目编码",
                blgs = ""
            });
            packeol.custbllist.custbl.Add(new custitem()
            {
                id = "3",
                blmc = "ShowForm",
                dw = "",
                caption = "提示信息请自行修改",
                blgs = ""
            });
            packeol.custbllist.custbl.Add(new custitem()
            {
                id = "4",
                blmc = "ShowPassword",
                dw = "",
                caption = "密码校验",
                blgs = ""
            });
            xmlSerialize.Serialization<packeol>(packeol, path + Dirs + uconst.c_cate_ext);
            TreeListNode newnode = this.treeFile.AppendNode(new object[] { Dirs }, null);
            newnode.Selected = true;
            if (this.currentpackeol == null)
            {
                this.currentpackeol = packeol;
                TraverseStep(newnode);
            }
        }

        private void btnDelClass_Click(object sender, EventArgs e)
        {
            if (treeFile.FocusedNode == null || this.currentpackeol == null) return;
            DialogResult dr = XtraMessageBox.Show("您确定要删除吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                string path = getfolder(treeClass.FocusedNode, "");
                FileInfo di = new FileInfo(path + treeFile.FocusedNode.GetValue(this.treeFile.Columns["FileName"]) + uconst.c_cate_ext);
                di.Delete();
                treeFile.DeleteNode(treeFile.FocusedNode);
                clearAll();
                if (treeFile.Nodes.Count > 0)
                {
                    treeFile.FocusedNode = treeFile.Nodes[0];
                    TraverseStep(treeFile.FocusedNode);
                }
            }
        }
        /// <summary>
        /// 遍历工步
        /// </summary>
        /// <param name="node"></param>
        void TraverseStep(TreeListNode node)
        {
            clearAll();
            if (node == null) return;
            this.savepath = Application.StartupPath + "\\" + getfolder(treeClass.FocusedNode, "") + treeFile.FocusedNode.GetValue(this.treeFile.Columns["FileName"]) + uconst.c_cate_ext;
            try
            {
                this.currentpackeol = xmlSerialize.DeSerialization<packeol>(savepath);
                if (this.currentpackeol.machine.comset.Count <= 0 || this.currentpackeol.machine.telect.Count <= 0 || this.currentpackeol.custbllist.custbl.Count <= 0 || this.currentpackeol.custbllist.inibl.Count <= 0)
                {
                    this.currentpackeol = MyMethod.getpackeol(savepath);
                }
            }
            catch
            {

            }
            this.StepTable = MyMethod.ToDataTable<step>(this.currentpackeol.steplist.step);
            this.gridStep.DataSource = this.StepTable;
            if (StepTable.Rows.Count > 0)
            {
                this.gridviewStep.FocusedRowHandle = 0;
                this.currentstep = this.currentpackeol.steplist.step[gridviewStep.FocusedRowHandle];
                TraverseSend();
            }
            try
            {
                chkCan1.Checked = this.currentpackeol.machine.cannetList[0].check == "-1";
                chkCan2.Checked = this.currentpackeol.machine.cannetList[1].check == "-1";
                chkCan3.Checked = this.currentpackeol.machine.cannetList[2].check == "-1";
                chkCan4.Checked = this.currentpackeol.machine.cannetList[3].check == "-1";

                beditCan1.Text = this.currentpackeol.machine.cannetList[0].file;
                beditCan2.Text = this.currentpackeol.machine.cannetList[1].file;
                beditCan3.Text = this.currentpackeol.machine.cannetList[2].file;
                beditCan4.Text = this.currentpackeol.machine.cannetList[3].file;

                chkcom1.Checked = this.currentpackeol.machine.comset[0].check == "-1";
                chkcom2.Checked = this.currentpackeol.machine.comset[1].check == "-1";
                chkcom3.Checked = this.currentpackeol.machine.comset[2].check == "-1";
                chkcom4.Checked = this.currentpackeol.machine.comset[3].check == "-1";
                chkcom5.Checked = this.currentpackeol.machine.comset[4].check == "-1";

                beditCom1.Text = this.currentpackeol.machine.comset[0].file;
                beditCom2.Text = this.currentpackeol.machine.comset[1].file;
                beditCom3.Text = this.currentpackeol.machine.comset[2].file;
                beditCom4.Text = this.currentpackeol.machine.comset[3].file;
                beditCom5.Text = this.currentpackeol.machine.comset[4].file;

                txtEidtCom1.Text = this.currentpackeol.machine.comset[0].btl;
                txtEidtCom2.Text = this.currentpackeol.machine.comset[1].btl;
                txtEidtCom3.Text = this.currentpackeol.machine.comset[2].btl;
                txtEidtCom4.Text = this.currentpackeol.machine.comset[3].btl;
                txtEidtCom5.Text = this.currentpackeol.machine.comset[4].btl;

                checkBox1.Checked = this.currentpackeol.machine.telect[0].check == "-1";
                checkBox2.Checked = this.currentpackeol.machine.telect[1].check == "-1";

                buttonEdit1.Text = this.currentpackeol.machine.telect[0].file;
                buttonEdit2.Text = this.currentpackeol.machine.telect[1].file;

                ch1comboBox1.Text = this.currentpackeol.machine.comset[0].ch1com;
                ch1comboBox2.Text = this.currentpackeol.machine.comset[1].ch1com;
                ch1comboBox3.Text = this.currentpackeol.machine.comset[2].ch1com;
                ch1comboBox4.Text = this.currentpackeol.machine.comset[3].ch1com;
                ch1comboBox5.Text = this.currentpackeol.machine.comset[4].ch1com;

                ch2comboBox1.Text = this.currentpackeol.machine.comset[0].ch2com;
                ch2comboBox2.Text = this.currentpackeol.machine.comset[1].ch2com;
                ch2comboBox3.Text = this.currentpackeol.machine.comset[2].ch2com;
                ch2comboBox4.Text = this.currentpackeol.machine.comset[3].ch2com;
                ch2comboBox5.Text = this.currentpackeol.machine.comset[4].ch2com;

                NettextEdit1.Text = this.currentpackeol.machine.telect[0].ip;
                NettextEdit2.Text = this.currentpackeol.machine.telect[1].ip;

                TEPortOne1.Text = this.currentpackeol.machine.telect[0].port;
                TEPortOne2.Text = this.currentpackeol.machine.telect[1].port;

                TEPortTwo1.Text = this.currentpackeol.machine.telect[0].porttwo;
                TEPortTwo2.Text = this.currentpackeol.machine.telect[1].porttwo;

                cboxCanbtl11.SelectedIndex = int.Parse(this.currentpackeol.machine.cannetList[0].btl);
                cboxCanbtl12.SelectedIndex = int.Parse(this.currentpackeol.machine.cannetList[1].btl);
                cboxCanbtl13.SelectedIndex = int.Parse(this.currentpackeol.machine.cannetList[2].btl);
                cboxCanbtl14.SelectedIndex = int.Parse(this.currentpackeol.machine.cannetList[3].btl);

                txtEidtCom1.Text = this.currentpackeol.machine.comset[0].btl;
                txtEidtCom2.Text = this.currentpackeol.machine.comset[1].btl;
                txtEidtCom3.Text = this.currentpackeol.machine.comset[2].btl;
                txtEidtCom4.Text = this.currentpackeol.machine.comset[3].btl;
                txtEidtCom5.Text = this.currentpackeol.machine.comset[4].btl;

                txt1TargitIP.Text = this.currentpackeol.machine.can.ch1IP;
                txt2TargitIP.Text = this.currentpackeol.machine.cannetList[4].chip;
                txtTargitPort11.Text = this.currentpackeol.machine.can.ch1Port1;
                txtTargitPort12.Text = this.currentpackeol.machine.can.ch1Port2;
                txtTargitPort21.Text = this.currentpackeol.machine.can.Ch2Port1;
                txtTargitPort22.Text = this.currentpackeol.machine.can.ch2Port2;

                cboxMachineindex.SelectedIndex = this.currentpackeol.machine.cannetList[0].machineindex;
                cboxcan1ch1index1.SelectedIndex = this.currentpackeol.machine.cannetList[0].chindex;
                txtTargitPort11.Text = this.currentpackeol.machine.cannetList[0].chport;

                cboxMachineindex.SelectedIndex = this.currentpackeol.machine.cannetList[1].machineindex;
                cboxcan1ch1index2.SelectedIndex = this.currentpackeol.machine.cannetList[1].chindex;
                txtTargitPort12.Text = this.currentpackeol.machine.cannetList[1].chport;

                cboxMachineindex.SelectedIndex = this.currentpackeol.machine.cannetList[2].machineindex;
                cboxcan1ch1index3.SelectedIndex = this.currentpackeol.machine.cannetList[2].chindex;
                txtTargitPort13.Text = this.currentpackeol.machine.cannetList[2].chport;

                cboxMachineindex.SelectedIndex = this.currentpackeol.machine.cannetList[3].machineindex;
                cboxcan1ch1index4.SelectedIndex = this.currentpackeol.machine.cannetList[3].chindex;
                txtTargitPort14.Text = this.currentpackeol.machine.cannetList[3].chport;
                //二通道
                cboxMachinech2.SelectedIndex = this.currentpackeol.machine.cannetList[4].machineindex;
                cboxcan2ch2index1.SelectedIndex = this.currentpackeol.machine.cannetList[4].chindex;
                txtTargitPort21.Text = this.currentpackeol.machine.cannetList[4].chport;

                cboxMachinech2.SelectedIndex = this.currentpackeol.machine.cannetList[5].machineindex;
                cboxcan2ch2index2.SelectedIndex = this.currentpackeol.machine.cannetList[5].chindex;
                txtTargitPort22.Text = this.currentpackeol.machine.cannetList[5].chport;


                cboxMachinech2.SelectedIndex = this.currentpackeol.machine.cannetList[6].machineindex;
                cboxcan2ch2index3.SelectedIndex = this.currentpackeol.machine.cannetList[6].chindex;
                txtTargitPort23.Text = this.currentpackeol.machine.cannetList[6].chport;

                cboxMachinech2.SelectedIndex = this.currentpackeol.machine.cannetList[7].machineindex;
                cboxcan2ch2index4.SelectedIndex = this.currentpackeol.machine.cannetList[7].chindex;
                txtTargitPort24.Text = this.currentpackeol.machine.cannetList[7].chport;

                IsStart.Checked = currentpackeol.machine.isAutoTest == "True";
                IsCKBindCode.Checked = currentpackeol.machine.isBindCode == "True";
            }
            catch
            {
                var index = this.currentpackeol.machine.cannetList.Count;
                for (int i = index; i < 8; i++)
                {
                    this.currentpackeol.machine.cannetList.Add(new cannet()
                    {
                        machineindex = 0,
                        chindex = 0,
                        check = "0",
                        file = "",
                        btl = "3",
                        chip = "",
                        chport = ""
                    });
                }
                index = currentpackeol.machine.comset.Count;
                for (int i = index; i < 5; i++)
                {
                    currentpackeol.machine.comset.Add(new comitem()
                    {
                        key = "",
                        bz = "",
                        text = "",
                        check = "0",
                        file = "",
                        btl = "",
                        ch1index = "",
                        ch2index = ""
                    });
                }
                index = currentpackeol.machine.telect.Count;
                for (int i = index; i < 2; i++)
                {
                    currentpackeol.machine.telect.Add(new netitem()
                    {
                        bz = "",
                        text = "",
                        check = "0",
                        file = "",
                        IP = "169.254.4.10",
                        PORT = "5025",
                        ip = "169.254.4.10",
                        port = "5025",
                        porttwo = "5025"
                    });
                }
                SetClear();
            }
        }
        void SetClear()
        {
            chkCan1.Checked = false;
            chkCan2.Checked = false;
            chkCan3.Checked = false;
            chkCan4.Checked = false;

            beditCan1.Text = "";
            beditCan2.Text = "";
            beditCan3.Text = "";
            beditCan4.Text = "";

            chkcom1.Checked = false;
            chkcom2.Checked = false;
            chkcom3.Checked = false;
            chkcom4.Checked = false;
            chkcom5.Checked = false;

            beditCom1.Text = "";
            beditCom2.Text = "";
            beditCom3.Text = "";
            beditCom4.Text = "";
            beditCom5.Text = "";

            txtEidtCom1.Text = "";
            txtEidtCom2.Text = "";
            txtEidtCom3.Text = "";
            txtEidtCom4.Text = "";
            txtEidtCom5.Text = "";

            checkBox1.Checked = false;
            checkBox2.Checked = false;

            buttonEdit1.Text = "";
            buttonEdit2.Text = "";

            ch1comboBox1.Text = "";
            ch1comboBox2.Text = "";
            ch1comboBox3.Text = "";
            ch1comboBox4.Text = "";
            ch1comboBox5.Text = "";

            ch2comboBox1.Text = "";
            ch2comboBox2.Text = "";
            ch2comboBox3.Text = "";
            ch2comboBox4.Text = "";
            ch2comboBox5.Text = "";

            NettextEdit1.Text = "";
            NettextEdit2.Text = "";

            TEPortOne1.Text = "";
            TEPortOne2.Text = "";

            TEPortTwo1.Text = "";
            TEPortTwo2.Text = "";

            cboxCanbtl11.Text = "";
            cboxCanbtl12.Text = "";
            cboxCanbtl13.Text = "";
            cboxCanbtl14.Text = "";

            txtEidtCom1.Text = "";
            txtEidtCom2.Text = "";
            txtEidtCom3.Text = "";
            txtEidtCom4.Text = "";
            txtEidtCom5.Text = "";
        }
        string savepath;
        private void treeFile_AfterFocusNode(object sender, NodeEventArgs e)
        {
            TraverseStep(e.Node);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            List<string> Tlist = new List<string>();
            foreach (TreeListNode node in treeFile.Nodes)
            {
                Tlist.Add(node.GetValue(this.treeFile.Columns["FileName"]).ToString());
            }
            var name = CopyName(Tlist, treeFile.FocusedNode.GetValue(this.treeFile.Columns["FileName"]).ToString());
            string path = getfolder(treeClass.FocusedNode, "") + name + uconst.c_cate_ext;
            xmlSerialize.Serialization<packeol>(this.currentpackeol, path);

            this.treeFile.AppendNode(new object[] { name }, null);
        }
        public static string CopyName(List<string> Tlist, string t)
        {
            List<string> Tnamelist = Tlist;
            int a = 0; bool bo = true;
            for (int i = 1; i < (Tnamelist.Count + 1); i++)
            {
                for (int j = 0; j < Tnamelist.Count; j++)
                {
                    if (Tnamelist[j].Equals(t.ToString() + " 副本 " + i.ToString()))
                    {
                        bo = false;
                        break;
                    }
                }
                if (bo)
                {
                    a = i;
                    break;
                }
                bo = true;
                a = i + 1;
            }
            string str = t.ToString() + " 副本 " + a.ToString();
            return str;
        }

        private void gridViewStep_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewStep_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.StepTable.Rows.Count <= 0) return;
            this.currentstep = this.currentpackeol.steplist.step[e.FocusedRowHandle];
            TraverseSend();
        }
        /// <summary>
        /// 遍历步骤
        /// </summary>
        void TraverseSend()
        {
            this.CmdTable.Clear();
            treeListInit.ClearNodes();
            treelistRun.ClearNodes();
            treeComING.ClearNodes();
            treeCmdJudge.ClearNodes();
            treeListSave.ClearNodes();
            this.currentitemsend = null;
            if (this.currentstep == null) return;
            this.CmdTable = MyMethod.ToDataTable<itemsend>(this.currentstep.send);
            this.gridcmd.DataSource = CmdTable;
            if (this.CmdTable.Rows.Count > 0)
            {
                this.currentitemsend = this.currentstep.send[0];
                this.gridViewCmd.FocusedRowHandle = 0;
                this.Traverseconitem(this.currentitemsend.initlist, treeListInit);
                this.Traverseconitem(this.currentitemsend.runlist, treelistRun);
                this.Traverseconitem(this.currentitemsend.runinglist, treeComING);
                this.Traverseconitem(this.currentitemsend.judgelist, treeCmdJudge);
                this.Traverseconitem(this.currentitemsend.savelist, treeListSave, true);
            }
        }
        private void gridviewStep_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Type type = this.currentstep.GetType();
            string attribute = e.Column.FieldName;
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(attribute);
            propertyInfo.SetValue(this.currentstep, e.Value);
            savepackeol();
        }
        /// <summary>
        /// 保存
        /// </summary>
        void savepackeol()
        {
            if (this.currentpackeol == null) return;
            xmlSerialize.Serialization<packeol>(this.currentpackeol, this.savepath);
        }

        private void btnStepAdd_Click(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            FrmnewStep frmnew = new FrmnewStep();
            if (frmnew.ShowDialog() != DialogResult.OK) return;
            step step = new step()
            {
                check = "-1",
                text = frmnew.stepmc(),
                kind = frmnew.kind()
            };
            DataRow dr = this.StepTable.NewRow();
            dr["check"] = "-1";
            dr["text"] = step.text;
            dr["kind"] = step.kind;
            dr["steptype"] = step.steptype;
            if (this.gridviewStep.FocusedRowHandle > 0)
            {
                this.currentpackeol.steplist.step.Insert(this.gridviewStep.FocusedRowHandle + 1, step);
                this.StepTable.Rows.InsertAt(dr, this.gridviewStep.FocusedRowHandle + 1);
            }
            else
            {
                this.currentpackeol.steplist.step.Add(step);
                this.StepTable.Rows.Add(dr);
            }
            if (this.currentstep == null) this.currentstep = step;
            this.savepackeol();
        }

        private void btnStepDel_Click(object sender, EventArgs e)
        {
            this.currentitemsend = null;
            this.CmdTable.Clear();
            treeListInit.ClearNodes();
            treelistRun.ClearNodes();
            treeComING.ClearNodes();
            treeCmdJudge.ClearNodes();
            treeListSave.ClearNodes();
            if (this.currentstep == null) return;
            this.currentpackeol.steplist.step.Remove(this.currentstep);
            this.currentstep = null;
            var index = this.gridviewStep.FocusedRowHandle;
            this.StepTable.Rows.RemoveAt(index);
            this.savepackeol();
            if (this.StepTable.Rows.Count > 0)
            {
                this.gridviewStep.FocusedRowHandle = index == this.StepTable.Rows.Count ? index - 1 : index;
                this.currentstep = this.currentpackeol.steplist.step[this.gridviewStep.FocusedRowHandle];
                TraverseSend();
            }
        }

        private void btnStepUp_Click(object sender, EventArgs e)
        {
            if (this.currentstep == null || this.gridviewStep.FocusedRowHandle <= 0) return;
            this.currentpackeol.steplist.step.Remove(this.currentstep);
            this.currentpackeol.steplist.step.Insert(this.gridviewStep.FocusedRowHandle - 1, this.currentstep);
            DataRow dr = this.StepTable.NewRow();
            dr["check"] = currentstep.check;
            dr["text"] = currentstep.text;
            dr["kind"] = currentstep.kind;
            dr["steptype"] = currentstep.steptype;
            var index = this.gridviewStep.FocusedRowHandle;
            this.StepTable.Rows.RemoveAt(index);
            this.StepTable.Rows.InsertAt(dr, index - 1);
            this.savepackeol();
            this.gridviewStep.FocusedRowHandle = index - 1;

        }

        private void btnStepDown_Click(object sender, EventArgs e)
        {
            if (this.currentstep == null || this.gridviewStep.FocusedRowHandle >= this.StepTable.Rows.Count - 1) return;
            this.currentpackeol.steplist.step.Remove(this.currentstep);
            this.currentpackeol.steplist.step.Insert(this.gridviewStep.FocusedRowHandle + 1, this.currentstep);
            DataRow dr = this.StepTable.NewRow();
            dr["check"] = currentstep.check;
            dr["text"] = currentstep.text;
            dr["kind"] = currentstep.kind;
            dr["steptype"] = currentstep.steptype;
            var index = this.gridviewStep.FocusedRowHandle;
            this.StepTable.Rows.RemoveAt(index);
            this.StepTable.Rows.InsertAt(dr, index + 1);
            this.savepackeol();
            this.gridviewStep.FocusedRowHandle = index + 1;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            var temp = this.packoltoTStepManage();
            FrmSetCustomBl frmCustBl = new FrmSetCustomBl(temp, temp.CustBlList);
            // frmCustBl.ShowDialog();
            if (frmCustBl.ShowDialog() == DialogResult.OK)
            {
                var manage = frmCustBl.FSelStepManage;
                this.currentpackeol.custbllist.inibl.Clear();
                for (int i = 0; i < manage.IniCustBllist.Count; i++)
                {
                    var item = manage.IniCustBllist.Item(i);
                    this.currentpackeol.custbllist.inibl.Add(new iniitem()
                    {
                        blmc = item.Blmc,
                        chapter = item.Chapter,
                        property = item.Property,
                        caption = item.Caption
                    });
                }
                this.currentpackeol.custbllist.custbl.Clear();
                for (int i = 0; i < manage.CustBlList.Count; i++)
                {
                    var item = manage.CustBlList.Item(i);
                    this.currentpackeol.custbllist.custbl.Add(new custitem()
                    {
                        id = item.Id.ToString(),
                        blmc = item.Blmc,
                        dw = item.Dw,
                        caption = item.Caption,
                        blgs = item.Blgs
                    });
                }
                this.savepackeol();
            }
        }


        private void gridViewCmd_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.gridViewCmd.FocusedRowHandle < 0) return;
            this.currentitemsend = this.currentstep.send[this.gridViewCmd.FocusedRowHandle];
            this.Traverseconitem(this.currentitemsend.initlist, treeListInit);
            this.Traverseconitem(this.currentitemsend.runlist, treelistRun);
            this.Traverseconitem(this.currentitemsend.runinglist, treeComING);
            this.Traverseconitem(this.currentitemsend.judgelist, treeCmdJudge);
            this.Traverseconitem(this.currentitemsend.savelist, treeListSave, true);
        }

        private void gridViewCmd_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Type type = this.currentitemsend.GetType();
            string attribute = e.Column.FieldName;
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(attribute);
            propertyInfo.SetValue(this.currentitemsend, e.Value);
            savepackeol();
        }
        TStepManage packoltoTStepManage()
        {
            TStepManage SelStepManage = new TStepManage();
            for (int i = 0; i < 4; i++)
            {
                SelStepManage.CannetSetList.Add(new TCannetSet() { File = this.currentpackeol.machine.cannetList[i].file });
            }
            for (int i = 0; i < 5; i++)
            {
                SelStepManage.ComSetlist.Add(new TComSetObject() { File = this.currentpackeol.machine.comset[i].file });
            }
            for (int i = 0; i < 2; i++)
            {
                SelStepManage.EthSetlist.Add(new TEthSetOjbect() { File = this.currentpackeol.machine.telect[i].file });
            }
            foreach (var ini in this.currentpackeol.custbllist.inibl)
            {
                SelStepManage.IniCustBllist.Add(new TiniCustBl() { Caption = ini.caption, Blmc = ini.blmc, Chapter = ini.chapter, Property = ini.property });
            }
            foreach (var cus in this.currentpackeol.custbllist.custbl)
            {
                SelStepManage.CustBlList.Add(new TCustBl() { Caption = cus.caption, Blmc = cus.blmc, Id = int.Parse(cus.id), Blgs = cus.blgs, Dw = cus.dw });
            }
            return SelStepManage;
        }
        private void btncmdadd_Click(object sender, EventArgs e)
        {
            //TStepManage SelStepManage, TStepClass SelStep
            if (this.currentstep == null) return;
            FrmSelCmd afrmSelCmd = new FrmSelCmd(packoltoTStepManage(), new TStepClass());
            var index = this.gridViewCmd.FocusedRowHandle > 0 ? this.gridViewCmd.FocusedRowHandle : 0;
            if (afrmSelCmd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < afrmSelCmd.dtSelCmd.Rows.Count; i++)
                {
                    DataRow arow = afrmSelCmd.dtSelCmd.Rows[i];

                    itemsend newsend = new itemsend()
                    {
                        kind = arow["kind"].ToString(),
                        cmd = arow["cmd"].ToString(),
                        waittime = Nebula_SysComm_String.StringUtil.strtoint(arow["waittime"].ToString()),
                        runtime = 0
                    };
                    DataRow dr = this.CmdTable.NewRow();
                    dr["kind"] = newsend.kind;
                    dr["cmd"] = newsend.cmd;
                    dr["waittime"] = newsend.waittime;
                    dr["runtime"] = newsend.runtime;
                    if (index != 0)
                    {
                        this.currentstep.send.Insert(index + i + 1, newsend);
                        this.CmdTable.Rows.InsertAt(dr, index + i + 1);
                    }
                    else
                    {
                        this.currentstep.send.Add(newsend);
                        this.CmdTable.Rows.Add(dr);
                    }
                }
                this.savepackeol();
            }
        }
        void clearAll()
        {
            this.currentpackeol = null;
            this.currentstep = null;
            this.currentitemsend = null;
            this.StepTable.Clear();
            this.CmdTable.Clear();
            treeListInit.ClearNodes();
            treelistRun.ClearNodes();
            treeComING.ClearNodes();
            treeCmdJudge.ClearNodes();
            treeListSave.ClearNodes();
        }
        private void btncmddel_Click(object sender, EventArgs e)
        {
            treeListInit.ClearNodes();
            treelistRun.ClearNodes();
            treeComING.ClearNodes();
            treeCmdJudge.ClearNodes();
            treeListSave.ClearNodes();
            if (this.currentitemsend == null) return;
            this.currentstep.send.Remove(currentitemsend);
            this.currentitemsend = null;
            var index = this.gridViewCmd.FocusedRowHandle;
            this.CmdTable.Rows.RemoveAt(index);
            this.savepackeol();
            if (this.CmdTable.Rows.Count > 0)
            {
                this.gridViewCmd.FocusedRowHandle = index == this.CmdTable.Rows.Count ? index - 1 : index;
                this.currentitemsend = this.currentstep.send[this.gridViewCmd.FocusedRowHandle];
            }


        }

        private void btnCmdUp_Click(object sender, EventArgs e)
        {
            if (this.currentitemsend == null || this.gridViewCmd.FocusedRowHandle <= 0) return;
            this.currentstep.send.Remove(this.currentitemsend);
            this.currentstep.send.Insert(this.gridViewCmd.FocusedRowHandle - 1, this.currentitemsend);
            DataRow dr = this.CmdTable.NewRow();
            dr["kind"] = currentitemsend.kind;
            dr["cmd"] = currentitemsend.cmd;
            dr["waittime"] = currentitemsend.waittime;
            dr["runtime"] = currentitemsend.runtime;
            var index = this.gridViewCmd.FocusedRowHandle;
            this.CmdTable.Rows.RemoveAt(index);
            this.CmdTable.Rows.InsertAt(dr, index - 1);
            this.savepackeol();
            this.gridViewCmd.FocusedRowHandle = index - 1;
        }

        private void btncmdDown_Click(object sender, EventArgs e)
        {
            if (this.currentitemsend == null || this.gridViewCmd.FocusedRowHandle >= this.CmdTable.Rows.Count - 1) return;
            this.currentstep.send.Remove(this.currentitemsend);
            this.currentstep.send.Insert(this.gridViewCmd.FocusedRowHandle + 1, this.currentitemsend);
            DataRow dr = this.CmdTable.NewRow();
            dr["kind"] = currentitemsend.kind;
            dr["cmd"] = currentitemsend.cmd;
            dr["waittime"] = currentitemsend.waittime;
            dr["runtime"] = currentitemsend.runtime;
            var index = this.gridViewCmd.FocusedRowHandle;
            this.CmdTable.Rows.RemoveAt(index);
            this.CmdTable.Rows.InsertAt(dr, index + 1);
            this.savepackeol();
            this.gridViewCmd.FocusedRowHandle = index + 1;
        }

        private void simBtnCopyB_Click(object sender, EventArgs e)
        {

            if (!ListCmdCopy.Contains(this.currentitemsend))
            {
                ListCmdCopy.Add(this.currentitemsend);
            }
            simBtnCopyB.Text = "复制" + ListCmdCopy.Count.ToString();


        }
        void Traverseconitem(List<conitem> conitemlist, TreeList list, bool isSave = false)
        {
            list.ClearNodes();
            if (this.currentitemsend == null) return;
            foreach (var item in conitemlist)
            {
                TreeListNode itemNode = list.AppendNode(item, null);
                itemNode.SetValue(list.Columns[0], item.text + (isSave ? "" : (item.con + ((item.strvalue == "" || item.strvalue == null) ? item.value.ToString() : item.strvalue))));
                foreach (var con in item.subconitem)
                {
                    TreeListNode conNode = list.AppendNode(con, itemNode);
                    conNode.SetValue(list.Columns[0], con.text + con.con + ((con.strvalue == "" || con.strvalue == null) ? con.value.ToString() : con.strvalue));
                }
                itemNode.Expanded = true;
            }
        }
        int selecttree;
        private void treeListInit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selecttree = 0;
                contextMenuStrip1.Show(p);
            }
        }

        private void treelistRun_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selecttree = 1;
                contextMenuStrip1.Show(p);
            }
        }

        private void treeComING_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selecttree = 2;
                contextMenuStrip1.Show(p);
            }
        }

        private void treeCmdJudge_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selecttree = 3;
                contextMenuStrip1.Show(p);
            }
        }

        private void treeListSave_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selecttree = 4;
                contextMenuStrip1.Show(p);
            }
        }
        TSetConList conitemtoconlist(List<conitem> conitemlist)
        {
            TSetConList tset = new TSetConList();
            foreach (var con in conitemlist)
            {
                var t = new TSetConObject()
                 {
                     Isparent = con.isparent == "1",
                     Text = con.text,
                     Blmc = con.blmc,
                     Dw = con.dw,
                     DefaultVal = con.defaultVal,
                     Contition = con.con,
                     ConValue = (con.strvalue == "" || con.strvalue == null) ? con.value.ToString() : con.strvalue
                 };
                foreach (var sub in con.subconitem)
                {
                    t.Subconlist.Add(new TSetConObject()
                    {
                        Isparent = sub.isparent == "1",
                        Text = sub.text,
                        Blmc = sub.blmc,
                        Dw = sub.dw,
                        DefaultVal = sub.defaultVal,
                        Contition = sub.con,
                        ConValue = (sub.strvalue == "" || sub.strvalue == null) ? sub.value.ToString() : sub.strvalue
                    });
                }
                tset.Add(t);
            };
            return tset;
        }
        void conlisttoconitem(TSetConList temp, ref List<conitem> conitemlist)
        {
            conitemlist.Clear();
            for (int i = 0; i < temp.Count; i++)
            {
                var tcon = temp.Item(i);
                conitemlist.Add(new conitem()
                {
                    isparent = tcon.Isparent ? "1" : "0",
                    text = tcon.Text,
                    blmc = tcon.Blmc,
                    dw = tcon.Dw,
                    defaultVal = tcon.DefaultVal,
                    con = tcon.Contition,
                });
                double dou;
                if (!double.TryParse(tcon.ConValue, out dou))
                    conitemlist[i].strvalue = tcon.ConValue;
                else
                    conitemlist[i].value = dou;
                for (int j = 0; j < tcon.Subconlist.Count; j++)
                {
                    var tsub = tcon.Subconlist.Item(j);
                    conitemlist[i].subconitem.Add(new subconitem()
                    {
                        isparent = tsub.Isparent ? "1" : "0",
                        text = tsub.Text,
                        blmc = tsub.Blmc,
                        dw = tsub.Dw,
                        defaultVal = tsub.DefaultVal,
                        con = tsub.Contition,
                    });
                    double dou2;
                    if (!double.TryParse(tsub.ConValue, out dou2))
                        conitemlist[i].subconitem[j].strvalue = tsub.ConValue;
                    else
                        conitemlist[i].subconitem[j].value = dou2;
                }
            }
        }
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region MyRegion
            //if (this.currentitemsend == null) return;
            //FrmSet aFrmSelbl ;
            //switch (selecttree)
            //{
            //    case 0:
            //        aFrmSelbl = new FrmSet(this.currentpackeol.machine, this.currentitemsend.initlist, this.currentpackeol.custbllist);
            //        if (aFrmSelbl.ShowDialog() == DialogResult.OK)
            //        {
            //            this.currentitemsend.initlist = aFrmSelbl.conitem;
            //            this.Traverseconitem(this.currentitemsend.initlist, treeListInit);
            //        }
            //        break;
            //    case 1:
            //        aFrmSelbl = new FrmSet(this.currentpackeol.machine, this.currentitemsend.runlist, this.currentpackeol.custbllist);
            //        if (aFrmSelbl.ShowDialog() == DialogResult.OK)
            //        {
            //            this.currentitemsend.runlist = aFrmSelbl.conitem;
            //            this.Traverseconitem(this.currentitemsend.runlist, treelistRun);
            //        }
            //        break;
            //    case 2:
            //        aFrmSelbl = new FrmSet(this.currentpackeol.machine, this.currentitemsend.runinglist, this.currentpackeol.custbllist);
            //        if (aFrmSelbl.ShowDialog() == DialogResult.OK)
            //        {
            //            this.currentitemsend.runinglist = aFrmSelbl.conitem;
            //            this.Traverseconitem(this.currentitemsend.runinglist, treeComING);
            //        }
            //        break;
            //    case 3:
            //        aFrmSelbl =new FrmSet(this.currentpackeol.machine, this.currentitemsend.judgelist, this.currentpackeol.custbllist);
            //        if (aFrmSelbl.ShowDialog() == DialogResult.OK)
            //        {
            //            this.currentitemsend.judgelist = aFrmSelbl.conitem;
            //            this.Traverseconitem(this.currentitemsend.judgelist, treeCmdJudge);
            //        }
            //        break;
            //    case 4:
            //        aFrmSelbl = new FrmSet(this.currentpackeol.machine, this.currentitemsend.savelist, this.currentpackeol.custbllist);
            //        if (aFrmSelbl.ShowDialog() == DialogResult.OK)
            //        {
            //            this.currentitemsend.savelist = aFrmSelbl.conitem;
            //            this.Traverseconitem(this.currentitemsend.savelist, treeListSave,true);
            //        }
            //        break;
            //    default:
            //        break;
            //}
            //this.savepackeol(); 
            #endregion

            if (this.currentitemsend == null) return;
            FrmSelbl aFrmSelbl;
            switch (selecttree)
            {
                case 0:
                    aFrmSelbl = new FrmSelbl(this.packoltoTStepManage(), conitemtoconlist(this.currentitemsend.initlist), "initlist");
                    if (aFrmSelbl.ShowDialog() == DialogResult.OK)
                    {
                        var temp = aFrmSelbl.FSelSetConList;
                        conlisttoconitem(temp, ref this.currentitemsend.initlist);
                        this.Traverseconitem(this.currentitemsend.initlist, treeListInit);
                    }
                    break;
                case 1:
                    aFrmSelbl = new FrmSelbl(this.packoltoTStepManage(), conitemtoconlist(this.currentitemsend.runlist));
                    if (aFrmSelbl.ShowDialog() == DialogResult.OK)
                    {
                        var temp = aFrmSelbl.FSelSetConList;
                        conlisttoconitem(temp, ref this.currentitemsend.runlist);
                        this.Traverseconitem(this.currentitemsend.runlist, treelistRun);
                    }
                    break;
                case 2:
                    aFrmSelbl = new FrmSelbl(this.packoltoTStepManage(), conitemtoconlist(this.currentitemsend.runinglist));
                    if (aFrmSelbl.ShowDialog() == DialogResult.OK)
                    {
                        var temp = aFrmSelbl.FSelSetConList;
                        conlisttoconitem(temp, ref this.currentitemsend.runinglist);
                        this.Traverseconitem(this.currentitemsend.runinglist, treeComING);
                    }
                    break;
                case 3:
                    aFrmSelbl = new FrmSelbl(this.packoltoTStepManage(), conitemtoconlist(this.currentitemsend.judgelist));
                    if (aFrmSelbl.ShowDialog() == DialogResult.OK)
                    {
                        var temp = aFrmSelbl.FSelSetConList;
                        conlisttoconitem(temp, ref this.currentitemsend.judgelist);
                        this.Traverseconitem(this.currentitemsend.judgelist, treeCmdJudge);
                    }
                    break;
                case 4:
                    aFrmSelbl = new FrmSelbl(this.packoltoTStepManage(), conitemtoconlist(this.currentitemsend.savelist), "savelist");
                    if (aFrmSelbl.ShowDialog() == DialogResult.OK)
                    {
                        var temp = aFrmSelbl.FSelSetConList;
                        conlisttoconitem(temp, ref this.currentitemsend.savelist);
                        this.Traverseconitem(this.currentitemsend.savelist, treeListSave, true);
                    }
                    break;
                default:
                    break;
            }
            this.savepackeol();
        }

        private void chkCan1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[0].check = chkCan1.Checked ? "-1" : "0";
            this.currentpackeol.machine.cannetList[4].check = this.currentpackeol.machine.cannetList[0].check;
            this.savepackeol();
        }


        private void chkCan2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[1].check = chkCan2.Checked ? "-1" : "0";
            this.currentpackeol.machine.cannetList[5].check = this.currentpackeol.machine.cannetList[1].check;
            this.savepackeol();
        }

        private void chkCan3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[2].check = chkCan3.Checked ? "-1" : "0";

            this.currentpackeol.machine.cannetList[6].check = this.currentpackeol.machine.cannetList[2].check;
            this.savepackeol();
        }

        private void chkCan4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[3].check = chkCan4.Checked ? "-1" : "0";
            this.currentpackeol.machine.cannetList[7].check = this.currentpackeol.machine.cannetList[3].check;
            this.savepackeol();
        }

        private void beditCan1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\bms";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCan1.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.cannetList[0].file = this.beditCan1.Text;
                this.currentpackeol.machine.cannetList[4].file = this.currentpackeol.machine.cannetList[0].file;
                this.savepackeol();
            }
        }

        private void beditCan2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\bms";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCan2.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.cannetList[1].file = this.beditCan2.Text;
                this.currentpackeol.machine.cannetList[5].file = this.currentpackeol.machine.cannetList[1].file;
                this.savepackeol();
            }
        }

        private void beditCan3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\bms";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCan3.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.cannetList[2].file = this.beditCan3.Text;
                this.currentpackeol.machine.cannetList[6].file = this.currentpackeol.machine.cannetList[2].file;
                this.savepackeol();
            }
        }

        private void beditCan4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\bms";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCan4.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.cannetList[3].file = this.beditCan4.Text;
                this.currentpackeol.machine.cannetList[7].file = this.currentpackeol.machine.cannetList[3].file;
                this.savepackeol();
            }
        }

        private void chkcom1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[0].check = chkcom1.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void chkcom2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[1].check = chkcom2.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void chkcom3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[2].check = chkcom3.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void chkcom4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[3].check = chkcom4.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void chkcom5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[4].check = chkcom5.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void beditCom1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\com";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCom1.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.comset[0].file = this.beditCom1.Text;
                this.savepackeol();
            }
        }

        private void beditCom2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\com";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCom2.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.comset[1].file = this.beditCom2.Text;
                this.savepackeol();
            }
        }

        private void beditCom3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\com";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCom3.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.comset[2].file = this.beditCom3.Text;
                this.savepackeol();
            }
        }

        private void beditCom4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\com";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCom4.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.comset[3].file = this.beditCom4.Text;
                this.savepackeol();
            }
        }

        private void beditCom5_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\com";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.beditCom5.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.comset[4].file = this.beditCom5.Text;
                this.savepackeol();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[0].check = checkBox1.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[1].check = checkBox2.Checked ? "-1" : "0";
            this.savepackeol();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\net";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonEdit1.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.telect[0].file = this.buttonEdit1.Text;
                this.savepackeol();
            }

        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.currentpackeol == null) return;
            openFileDialog1.InitialDirectory = Application.StartupPath + @"\net";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.buttonEdit2.Text = Path.GetFileName(openFileDialog1.FileName);
                this.currentpackeol.machine.telect[1].file = this.buttonEdit2.Text;
                this.savepackeol();
            }
        }

        private void ch1comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[0].ch1com = ch1comboBox1.Text;
            this.savepackeol();
        }


        private void ch1comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[1].ch1com = ch1comboBox2.Text;
            this.savepackeol();
        }


        private void ch1comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[2].ch1com = ch1comboBox3.Text;
            this.savepackeol();
        }

        private void ch1comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[3].ch1com = ch1comboBox4.Text;
            this.savepackeol();
        }

        private void ch1comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[4].ch1com = ch1comboBox5.Text;
            this.savepackeol();
        }

        private void ch2comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[0].ch2com = ch2comboBox1.Text;
            this.savepackeol();
        }

        private void ch2comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[1].ch2com = ch2comboBox2.Text;
            this.savepackeol();
        }

        private void ch2comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[2].ch2com = ch2comboBox3.Text;
            this.savepackeol();
        }

        private void ch2comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[3].ch2com = ch2comboBox4.Text;
            this.savepackeol();
        }

        private void ch2comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[4].ch2com = ch2comboBox5.Text;
            this.savepackeol();
        }

        private void NettextEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[0].ip = NettextEdit1.Text;
            this.savepackeol();
        }

        private void NettextEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[1].ip = NettextEdit2.Text;
            this.savepackeol();
        }

        private void TEPortOne1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[0].port = TEPortOne1.Text;
            this.savepackeol();
        }

        private void TEPortOne2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[1].port = TEPortOne2.Text;
            this.savepackeol();
        }

        private void TEPortTwo1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[0].porttwo = TEPortTwo1.Text;
            this.savepackeol();
        }

        private void TEPortTwo2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.telect[1].porttwo = TEPortTwo2.Text;
            this.savepackeol();
        }

        private void cboxCanbtl11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[0].btl = cboxCanbtl11.SelectedIndex.ToString();
            this.currentpackeol.machine.cannetList[4].btl = this.currentpackeol.machine.cannetList[0].btl;
            this.savepackeol();
        }

        private void cboxCanbtl12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[1].btl = cboxCanbtl12.SelectedIndex.ToString();
            this.currentpackeol.machine.cannetList[5].btl = this.currentpackeol.machine.cannetList[1].btl;
            this.savepackeol();
        }

        private void cboxCanbtl13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[2].btl = cboxCanbtl13.SelectedIndex.ToString();
            this.currentpackeol.machine.cannetList[6].btl = this.currentpackeol.machine.cannetList[2].btl;
            this.savepackeol();
        }

        private void cboxCanbtl14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.cannetList[3].btl = cboxCanbtl14.SelectedIndex.ToString();
            this.currentpackeol.machine.cannetList[7].btl = this.currentpackeol.machine.cannetList[3].btl;
            this.savepackeol();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;

            this.currentpackeol.machine.can.ch1IP = txt1TargitIP.Text;
            this.currentpackeol.machine.can.ch1Port1 = txtTargitPort11.Text;
            this.currentpackeol.machine.can.ch1Port2 = txtTargitPort12.Text;
            this.currentpackeol.machine.can.Ch2Port1 = txtTargitPort21.Text;
            this.currentpackeol.machine.can.ch2Port2 = txtTargitPort22.Text;

            this.currentpackeol.machine.cannetList[0].machineindex = Convert.ToInt32(cboxMachineindex.SelectedIndex);
            this.currentpackeol.machine.cannetList[0].chindex = Convert.ToInt32(cboxcan1ch1index1.SelectedIndex);
            this.currentpackeol.machine.cannetList[0].chip = txt1TargitIP.Text;
            this.currentpackeol.machine.cannetList[0].chport = txtTargitPort11.Text;

            this.currentpackeol.machine.cannetList[1].machineindex = Convert.ToInt32(cboxMachineindex.SelectedIndex);
            this.currentpackeol.machine.cannetList[1].chindex = Convert.ToInt32(cboxcan1ch1index2.SelectedIndex);
            this.currentpackeol.machine.cannetList[1].chip = txt1TargitIP.Text;
            this.currentpackeol.machine.cannetList[1].chport = txtTargitPort12.Text;

            this.currentpackeol.machine.cannetList[2].machineindex = Convert.ToInt32(cboxMachineindex.SelectedIndex);
            this.currentpackeol.machine.cannetList[2].chindex = Convert.ToInt32(cboxcan1ch1index3.SelectedIndex);
            this.currentpackeol.machine.cannetList[2].chip = txt1TargitIP.Text;
            this.currentpackeol.machine.cannetList[2].chport = txtTargitPort13.Text;

            this.currentpackeol.machine.cannetList[3].machineindex = Convert.ToInt32(cboxMachineindex.SelectedIndex);
            this.currentpackeol.machine.cannetList[3].chindex = Convert.ToInt32(cboxcan1ch1index4.SelectedIndex);
            this.currentpackeol.machine.cannetList[3].chip = txt1TargitIP.Text;
            this.currentpackeol.machine.cannetList[3].chport = txtTargitPort14.Text;
            //二通道
            this.currentpackeol.machine.cannetList[4].machineindex = Convert.ToInt32(cboxMachinech2.SelectedIndex);
            this.currentpackeol.machine.cannetList[4].chindex = Convert.ToInt32(cboxcan2ch2index1.SelectedIndex);
            this.currentpackeol.machine.cannetList[4].chip = txt2TargitIP.Text;
            this.currentpackeol.machine.cannetList[4].chport = txtTargitPort21.Text;

            this.currentpackeol.machine.cannetList[5].machineindex = Convert.ToInt32(cboxMachinech2.SelectedIndex);
            this.currentpackeol.machine.cannetList[5].chip = txt2TargitIP.Text;
            this.currentpackeol.machine.cannetList[5].chindex = Convert.ToInt32(cboxcan2ch2index2.SelectedIndex);
            this.currentpackeol.machine.cannetList[5].chport = txtTargitPort22.Text;


            this.currentpackeol.machine.cannetList[6].machineindex = Convert.ToInt32(cboxMachinech2.SelectedIndex);
            this.currentpackeol.machine.cannetList[6].chip = txt2TargitIP.Text;
            this.currentpackeol.machine.cannetList[6].chindex = Convert.ToInt32(cboxcan2ch2index3.SelectedIndex);
            this.currentpackeol.machine.cannetList[6].chport = txtTargitPort23.Text;

            this.currentpackeol.machine.cannetList[7].machineindex = Convert.ToInt32(cboxMachinech2.SelectedIndex);
            this.currentpackeol.machine.cannetList[7].chip = txt2TargitIP.Text;
            this.currentpackeol.machine.cannetList[7].chindex = Convert.ToInt32(cboxcan2ch2index4.SelectedIndex);
            this.currentpackeol.machine.cannetList[7].chport = txtTargitPort24.Text;

            this.currentpackeol.machine.isAutoTest = IsStart.Checked == true ? "True" : "False";
            this.currentpackeol.machine.isBindCode = IsCKBindCode.Checked == true ? "True" : "False";


            try
            {
                #region 获取公用仪器端口
                List<string> LockLink = new List<string>();
                LockLink.Clear();
                int i = 1;
                currentpackeol.machine.comset.ForEach(t =>
                {
                    if (t.check == "-1" && t.ch1com == t.ch2com)
                        LockLink.Add("COM" + i.ToString());
                    i++;
                });
                int k = 1;
                currentpackeol.machine.telect.ForEach(t =>
                {
                    if (t.check == "-1" && !string.IsNullOrEmpty(t.ip) && t.port == t.porttwo)
                        LockLink.Add("NET" + k.ToString());
                    k++;
                });
                #endregion
                if (LockLink.Count > 0)
                {
                    foreach (var item in currentpackeol.steplist.step)
                    {
                        item.isLock = "";
                        if (item.send.Find(t => String.Join("",LockLink.ToArray()).Contains(t.kind)) != null)
                            item.isLock = "1";
                        else
                            item.isLock = "";
                    }
                }
            }
            catch
            {

            }
            this.savepackeol();
            MessageBox.Show("保存成功！");
        }

        private void txtEidtCom1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[0].btl = txtEidtCom1.Text;
            this.savepackeol();
        }

        private void txtEidtCom2_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[1].btl = txtEidtCom2.Text;
            this.savepackeol();
        }

        private void txtEidtCom3_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[2].btl = txtEidtCom3.Text;
            this.savepackeol();
        }

        private void txtEidtCom4_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[3].btl = txtEidtCom4.Text;
            this.savepackeol();
        }

        private void txtEidtCom5_EditValueChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.comset[4].btl = txtEidtCom5.Text;
            this.savepackeol();
        }

        private void txt1TargitIP_TextChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            this.currentpackeol.machine.can.ch1IP = txt1TargitIP.Text;
            for (int i = 0; i < 4; i++)
            {
                this.currentpackeol.machine.cannetList[i].chip = txt1TargitIP.Text;
            }
            this.savepackeol();
        }

        private void txt2TargitIP_TextChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null) return;
            for (int i = 4; i < 8; i++)
            {
                this.currentpackeol.machine.cannetList[i].chip = txt2TargitIP.Text;
            }
            this.savepackeol();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentpackeol == null)
                return;
            for (int i = 0; i < this.StepTable.Rows.Count; i++)
            {
                this.StepTable.Rows[i]["check"] = this.checkBox3.Checked ? "-1" : "0";
                this.currentpackeol.steplist.step[i].check = this.checkBox3.Checked ? "-1" : "0";
            }
            this.savepackeol();
        }

        private void gridviewStep_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == this.gridviewStep.FocusedRowHandle)
                e.Appearance.BackColor = Color.DeepSkyBlue;
        }

        private void gridViewCmd_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == this.gridViewCmd.FocusedRowHandle)
                e.Appearance.BackColor = Color.DeepSkyBlue;
        }

        private void btnReName_Click(object sender, EventArgs e)
        {
            try
            {
                Frminput frm = new Frminput(glanguage.ReadCaption(310011), glanguage.ReadCaption(310019), treeFile.FocusedNode.GetDisplayText(0));
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                string Dirs = frm.Res;
                Computer MyComputer = new Computer();
                string sPath = getfolder(treeFile.FocusedNode, "");
                string fileName = sPath + treeFile.FocusedNode.GetDisplayText(0) + uconst.c_cate_ext;
                MyComputer.FileSystem.RenameFile(fileName, Dirs + uconst.c_cate_ext);

                treeFile.FocusedNode.SetValue(0, Dirs);
                TraverseStep(treeFile.FocusedNode);
            }
            catch (Exception)
            {

            }
        }

        #region 20170117zoe条码筛选方案
        private void btnAddCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (tSysSet != null)
                {
                    tSysSet.CodeList.Add(new CodeItem() { aaCodeId = Guid.NewGuid().ToString() });
                    tSysSet.Save();
                    gControlCode.DataSource = MyMethod.ToDataTable<CodeItem>(tSysSet.CodeList);
                }
            }
            catch
            {

            }

        }
        private void btnDelCode_Click(object sender, EventArgs e)
        {
            if (gViewCode.FocusedRowHandle < 0 || tSysSet == null) return;
            DialogResult dr = XtraMessageBox.Show("您确定要删除吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (tSysSet != null && tSysSet.CodeList.Count > 0)
                {
                    tSysSet.CodeList.RemoveAt(gViewCode.FocusedRowHandle);
                    tSysSet.Save();
                    gControlCode.DataSource = MyMethod.ToDataTable<CodeItem>(tSysSet.CodeList);
                }
            }
        }
        private void gViewCode_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (tSysSet != null && tSysSet.CodeList.Count > 0)
            {
                var m = tSysSet.CodeList.Find(t => t.aaCodeId == gViewCode.GetDataRow(e.RowHandle)["aaCodeId"].ToString());
                m.GetType().GetProperty(e.Column.FieldName).SetValue(m, e.Value);
                tSysSet.Save();
            }
        }
        #endregion

        #region 20170117zoe测试数据文件保存指定路径
        private void btnSetAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (tSysSet != null)
                {
                    tSysSet.SetList.Add(new SystemItem() { SystemID = Guid.NewGuid().ToString(), CheckType = "字符串" });
                    tSysSet.Save();
                    gControlSet.DataSource = MyMethod.ToDataTable<SystemItem>(tSysSet.SetList);
                }
            }
            catch
            {

            }
        }

        private void btnSetDel_Click(object sender, EventArgs e)
        {
            if (gViewSet.FocusedRowHandle < 0 || tSysSet == null) return;
            DialogResult dr = XtraMessageBox.Show("您确定要删除吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                if (tSysSet != null && tSysSet.SetList.Count > 0)
                {
                    tSysSet.SetList.RemoveAt(gViewSet.FocusedRowHandle);
                    tSysSet.Save();
                    gControlSet.DataSource = MyMethod.ToDataTable<SystemItem>(tSysSet.SetList);
                }
            }
        }

        private void gViewSet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (tSysSet != null && tSysSet.SetList.Count > 0)
            {
                var m = tSysSet.SetList.Find(t => t.SystemID == gViewSet.GetDataRow(e.RowHandle)["SystemID"].ToString());
                m.GetType().GetProperty(e.Column.FieldName).SetValue(m, e.Value);
                tSysSet.Save();
            }
        }
        #endregion

        private void bEditDTCFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    bEditDTCFile.Text = openFileDialog1.FileName;
                    List<BmsDTC> dtcList = DTCExcelToXml(bEditDTCFile.Text);
                    gridDtcControl.DataSource = MyMethod.ToDataTable<BmsDTC>(dtcList);
                }
            }
            catch
            {

            }
        }

        public List<BmsDTC> DTCExcelToXml(string dtcExcelFilePath)
        {
            tSysSet.DTCList.Clear();
            if (!File.Exists(dtcExcelFilePath)) return null;

            Workbook worbook = new Workbook(dtcExcelFilePath);
            Cells cells = worbook.Worksheets[0].Cells;
            for (int i = 1; i < cells.MaxDataRow + 1; i++)
            {
                tSysSet.DTCList.Add(new BmsDTC()
                {
                    DtcId = !string.IsNullOrEmpty(cells[i, 0].StringValue.Trim()) ? cells[i, 0].StringValue.Trim() : Convert.ToInt32(cells[i, 1].StringValue.Trim(), 16).ToString(),
                    DtcCode = cells[i, 1].StringValue.Trim(),
                    Comment = cells[i, 2].StringValue.Trim(),
                    DTCDispose = cells[i, 3].StringValue.Trim()
                });
            }
            tSysSet.Save();
            return tSysSet.DTCList;
        }

        #region 工歩复制
        //复制
        private void simBtnCopy_Click(object sender, EventArgs e)
        {
            if (this.currentstep == null) return;
            this.currentpackeol.steplist.step.Insert(this.gridviewStep.FocusedRowHandle + 1, (step)this.Clone(this.currentstep));
            DataRow dr = this.StepTable.NewRow();
            dr["check"] = currentstep.check;
            dr["text"] = currentstep.text;
            dr["kind"] = currentstep.kind;
            dr["steptype"] = currentstep.steptype;
            this.StepTable.Rows.InsertAt(dr, this.gridviewStep.FocusedRowHandle + 1);
            this.savepackeol();
        }

        private void btnStepToStep_Click(object sender, EventArgs e)
        {
            if (!ListSCopy.Contains(this.currentstep))
            {
                ListSCopy.Add(this.currentstep);
            }
            btnStepToStep.Text = "复制" + ListSCopy.Count.ToString();
        }

        private void btnCopyTo_Click(object sender, EventArgs e)
        {
            if (this.ListSCopy.Count == 0) return;
            int i = 1;
            foreach (var item in ListSCopy)
            {
                this.currentpackeol.steplist.step.Insert(this.gridviewStep.FocusedRowHandle + i, (step)this.Clone(item));
                DataRow dr = this.StepTable.NewRow();
                dr["check"] = item.check;
                dr["text"] = item.text;
                dr["kind"] = item.kind;
                dr["steptype"] = item.steptype;
                this.StepTable.Rows.InsertAt(dr, this.gridviewStep.FocusedRowHandle + i);
                i++;
            }
            this.savepackeol();
            ListSCopy.Clear();
            btnStepToStep.Text = "复制";
        }
        #endregion

        private void btnCmdCopy_Click(object sender, EventArgs e)
        {
            if (this.ListCmdCopy.Count == 0 || this.currentstep.send.Count == 0) return;
            int i = 1;
            foreach (var item in ListCmdCopy)
            {
                var index = this.gridViewCmd.FocusedRowHandle;
                this.currentstep.send.Insert(index + i, (itemsend)this.Clone(item));
                DataRow dr = this.CmdTable.NewRow();
                dr["kind"] = item.kind;
                dr["cmd"] = item.cmd;
                dr["waittime"] = item.waittime;
                dr["runtime"] = item.runtime;
                this.CmdTable.Rows.InsertAt(dr, index + i);
                i++;
            }
            this.savepackeol();
            ListCmdCopy.Clear();
            simBtnCopyB.Text = "复制";
        }

        private void btnSetLock_Click(object sender, EventArgs e)
        {
            try
            {
                #region 获取公用仪器端口
                List<string> LockLink = new List<string>();
                LockLink.Clear();
                int i = 1;
                currentpackeol.machine.comset.ForEach(t =>
                {
                    if (t.check == "-1" && t.ch1com == t.ch2com)
                    {
                        LockLink.Add("COM" + i.ToString());
                    }
                    i++;
                });
                int k = 1;
                currentpackeol.machine.telect.ForEach(t =>
                {
                    if (t.check == "-1" && !string.IsNullOrEmpty(t.ip) && t.port == t.porttwo)
                    {
                        LockLink.Add("NET" + k.ToString());
                    }
                    k++;
                });
                #endregion

                if (LockLink.Count > 0)
                {
                    foreach (var itemLink in LockLink)
                    {
                        foreach (var item in currentpackeol.steplist.step)
                        {
                            item.isLock = "";
                            if (item.send.Find(t => itemLink.Contains(t.kind)) != null)
                                item.isLock = "1";
                            else
                                item.isLock = "";
                        }
                    }
                    this.savepackeol();
                    MessageBox.Show("保存成功！");
                }
            }
            catch
            {

            }
            //string LockStr = txtLock.Text;
            //if (currentpackeol != null && !string.IsNullOrEmpty(LockStr))
            //{
            //    foreach (var item in currentpackeol.steplist.step)
            //    {
            //        item.isLock = "";
            //        if (item.send.Find(t => LockStr.Contains(t.kind)) != null)
            //            item.isLock = "1";
            //        else
            //            item.isLock = "";
            //    }
            //    this.savepackeol();
            //    MessageBox.Show("保存成功！");
            //}
        }
    }
}
