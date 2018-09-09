using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Scheme
{
    public partial class Frmsceme : Form
    {
        
        string fileUrl = null;
        TSchem CurrentTSchem = null;
        private FolderService folderService;//关联
        private FileService fileService;
        public Frmsceme()
        {
            InitializeComponent();
            folderService = new FolderService();
            fileService = new FileService();
        }

        /// <summary>
        /// Remove the folder
        /// while treeClass is empty it will throw a NullReferenceException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void delFolder_Click(object sender, EventArgs e)
        {
            folderService.remove_Folder(this.treeClass);
        }

        /// <summary>
        /// Add a new folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void addFolder_Click(object sender, EventArgs e)
        {
            //add folders
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            //set the default folder path
            folderBrowserDialog.SelectedPath = @"F:\GitSync\IHIL\test";
            folderBrowserDialog.ShowDialog();
            string path = folderBrowserDialog.SelectedPath;
            folderService.add_Folder(path, treeClass);
        }

      
        /// <summary>
        /// 界面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frmsceme_Load(object sender, EventArgs e)
        {
            ToolHeight();
            //初始化文件夹列表和文件列表
            TreeListNode FileNode = treeClass.AppendNode(null, null);
            FileNode.SetValue(treeClass.Columns["FolderName"], "scheme");
            FileNode.Tag = folderService.SchemeTopPath;
            folderService.traverse_Folder(FileNode, treeClass);
            treeClass.FocusedNode = FileNode;
            fileService.traverse_File(FileNode.Tag.ToString(),treeFile);
            gridControlTest.DataSource = null;
        }
        private void ToolHeight()
        {
            scemePanel.Height = toolboxControl1.Height - 39;
        }
        private void Frmsceme_Resize(object sender, EventArgs e)
        {
            ToolHeight();
        }
        private void tabNNarmalTest_Paint(object sender, PaintEventArgs e)
        {

        }
        private void contextMenuStripResult_Opening(object sender, CancelEventArgs e)
        {

        }
        private void buttonResult_Click(object sender, EventArgs e)
        {


        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void treeClass_AfterExpand(object sender, NodeEventArgs e)
        {

        }
        private void treeClass_RowStateImageClick(object sender, RowClickEventArgs e)
        {

        }
        private void treeClass_RowSelectImageClick(object sender, RowClickEventArgs e)
        {

        }
        private void treeClass_CustomDrawNodeButton(object sender, CustomDrawNodeButtonEventArgs e)
        {

        }


        /// <summary>
        /// sort the nodes by filename when fucused node is changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeClass_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            treeFile.ClearNodes();
            if (treeClass.AllNodesCount == 0) return;
            try
            {
                TreeListNode node = treeClass.FocusedNode;
                if (node.Tag == null) return;
                string path = node.Tag.ToString();
                fileService.traverse_File(path,treeFile);
                treeFile.Columns["FileName"].SortOrder = SortOrder.Ascending;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("There is no node focused in the TreeClass!");
            }
        }
        /// <summary>
        /// what's this ?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFile_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
           
            if (treeFile.AllNodesCount == 0) return;
            try
            {
                TreeListNode fnode = treeFile.FocusedNode;
                if (fnode.Tag == null) return;
                fileUrl = fnode.Tag.ToString();
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("There is no node focused in the TreeFile!");
                return;
            }

        }
        /// <summary>
        /// create a new xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddclass_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeClass.FocusedNode != null)
                {
                    //string folderpath = treeClass.FocusedNode.Tag.ToString();
                    fileService.add_File(this.treeFile);
                }

            }
            catch (NullReferenceException)
            {
                Console.WriteLine("There're some unreasonable problems!");
                return;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("File created filed,no such folder!");
                return;
            }

        }
        /// <summary>
        /// add a listener to response the hidden of editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFile_HiddenEditor(object sender, EventArgs e)
        {
            fileService.opreator_File(this.treeFile,this.treeClass);
        }
        /// <summary>
        /// delete the node in the treeFile and delete the local file in the meantime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelClass_Click(object sender, EventArgs e)
        {
            fileService.remove_File(this.treeFile);
        }
        /// <summary>
        /// create a dialog
        /// and copy the current file to a new file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnSaveasclass_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            TreeListNode node = treeClass.FocusedNode;
            TreeListNode fnode = treeFile.FocusedNode;
            string path = null;
            string name = null;
            if (node != null && fnode != null)
            {
                path = node.Tag.ToString();
                name = fnode.GetValue(treeFile.Columns["FileName"]).ToString();
            }
            dialog.OverwritePrompt = true;
            dialog.InitialDirectory = path;
            dialog.FileName = name;
            dialog.Filter = "All files|*.*|Xml file|*.xml";
            dialog.ShowDialog();
            string sourceFile = path + "//" + name;
            string targetFile = null;
            if (dialog.FileName != "")
            {
                //FileStream fs = (FileStream)dialog.OpenFile();
                targetFile = dialog.FileName;
                File.Copy(sourceFile, targetFile);
            }
        }

        /// <summary>
        /// rename the focused file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReName_Click(object sender, EventArgs e)
        {
            this.fileService.rename_File(this.treeFile);
        }

        /// <summary>
        /// show menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFile_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = (sender as TreeList).CalcHitInfo(new Point(e.X, e.Y));
            TreeListNode node = hitInfo.Node;
            if (e.Button == MouseButtons.Right)
            {
                if (node != null)
                {
                    node.TreeList.FocusedNode = node;
                    node.TreeList.ContextMenuStrip = this.FileMenuStripResult;
                }
            }
        }
        /// <summary>
        /// show file in the explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_in_explorer_Click(object sender, EventArgs e)
        {
            TreeListNode fileNode = treeFile.FocusedNode;
            if (fileNode != null)
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + fileNode.Tag.ToString());
            }
        }
        /// <summary>
        /// open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            TreeListNode fileNode = treeFile.FocusedNode;
            if (fileNode == null)
            {
                MessageBox.Show("Error, no file selected!");
                return;
            }
            else
            {
                try
                {
                    string path = fileNode.Tag.ToString();
                    XmlSerializer serializer = new XmlSerializer(typeof(TSchem));
                    FileStream fs1 = new FileStream(path, FileMode.Open);
                    XmlReader reader = XmlReader.Create(fs1);
                    CurrentTSchem = (TSchem)serializer.Deserialize(reader);
                    fs1.Close();
                    //show information included in xml file
                    setAllPage();
                }
                catch(InvalidOperationException) {
                    MessageBox.Show("File format error", "Error!");
                }
                return;
            }
        }

        private void setAllPage()
        {
            if (CurrentTSchem == null)
            {
                MessageBox.Show("No file opened!", "Warning");
                return;
            }
            else
            {
                //set CAN page checkbox
                chkCan0.Checked = CurrentTSchem.setCanlist.TSetCans[0].Check == "1" ? true : false;
                chkCan1.Checked = CurrentTSchem.setCanlist.TSetCans[1].Check == "1" ? true : false;
                chkCan2.Checked = CurrentTSchem.setCanlist.TSetCans[2].Check == "1" ? true : false;
                chkCan3.Checked = CurrentTSchem.setCanlist.TSetCans[3].Check == "1" ? true : false;
                //set file name
                beditCan0.Text = CurrentTSchem.setCanlist.TSetCans[0].AgreeMentFile;
                beditCan1.Text = CurrentTSchem.setCanlist.TSetCans[1].AgreeMentFile;
                beditCan2.Text = CurrentTSchem.setCanlist.TSetCans[2].AgreeMentFile;
                beditCan3.Text = CurrentTSchem.setCanlist.TSetCans[3].AgreeMentFile;
                //set baut
                cboxCanbtl11.SelectedIndex = Int32.Parse(CurrentTSchem.setCanlist.TSetCans[0].Baut);
                cboxCanbtl12.SelectedIndex = Int32.Parse(CurrentTSchem.setCanlist.TSetCans[1].Baut);
                cboxCanbtl13.SelectedIndex = Int32.Parse(CurrentTSchem.setCanlist.TSetCans[2].Baut);
                cboxCanbtl14.SelectedIndex = Int32.Parse(CurrentTSchem.setCanlist.TSetCans[3].Baut);
                //set ethernet page
                checkBox1.Checked = CurrentTSchem.SetEthList.TSetEths[0].Check == "1" ? true : false;
                checkBox2.Checked = CurrentTSchem.SetEthList.TSetEths[1].Check == "1" ? true : false;
                buttonEdit1.Text = CurrentTSchem.SetEthList.TSetEths[0].AgreeMentFile;
                buttonEdit2.Text = CurrentTSchem.SetEthList.TSetEths[1].AgreeMentFile;
                NettextEdit1.Text = CurrentTSchem.SetEthList.TSetEths[0].IP;
                NettextEdit2.Text = CurrentTSchem.SetEthList.TSetEths[1].IP;
                TEPortOne1.Text = CurrentTSchem.SetEthList.TSetEths[0].Port;
                TEPortOne2.Text = CurrentTSchem.SetEthList.TSetEths[1].Port;
                //set normal test
                //set test project
                //warning: datasource is a list rather than an array!
                setTestProj();
                //set Project cmd
                setProjCMD(CurrentTSchem.StepList.TSteps.FirstOrDefault().CmdList);
                //set init omited...
                //set Set omited...
                //set Result judge
                //set save panel
                setOther(CurrentTSchem.StepList.TSteps.FirstOrDefault().CmdList.TCMDs.FirstOrDefault());
            }
        }
        private void setTestProj() {
            List<TStep> steps = CurrentTSchem.StepList.TSteps;
            gridControlTest.DataSource = steps;
        }
        private void setProjCMD(cmdList list) {
            List<TCMD> tCMDs = list.TCMDs;
            gridControlProject.DataSource = tCMDs;
        }
        private void setOther(TCMD tcmd) {
            gridControlJudge.DataSource = tcmd.Judgelist.tconditions;
            gridControlSave.DataSource = tcmd.Savelist.TConditions;
        }
        private void GetAllPage()
        {
            if (CurrentTSchem == null) return;
        }

        public void SetValueFromXmlFile(XmlDocument xDoc)
        {
            //set CAN
            XmlNodeList list = xDoc.GetElementsByTagName("canitem");
            string DeviceIndex = null;
            if (list.Count != 0)
            {
                foreach (XmlNode node in list)
                {
                    string chindex = node.Attributes["chindex"].Value;
                    string canFile = node.Attributes["file"].Value;
                    DeviceIndex = node.Attributes["machineindex"].Value;
                    switch (chindex)
                    {
                        case "0":
                            chkCan0.Checked = true;
                            beditCan0.Text = canFile;
                            break;
                        case "1":
                            chkCan1.Checked = true;
                            beditCan1.Text = canFile;
                            break;
                        case "2":
                            chkCan2.Checked = true;
                            beditCan2.Text = canFile;
                            break;
                        case "3":
                            chkCan3.Checked = true;
                            beditCan3.Text = canFile;
                            break;
                    }
                }
                cboxMachineindex.SelectedIndex = Int32.Parse(DeviceIndex);
            }
        }

        private void beditCan0_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                beditCan0.Text = info.Name;
            }
            else
            {
                beditCan0.Text = "";
            }
        }

        private void beditCan1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                beditCan1.Text = info.Name;
            }
            else
            {
                beditCan1.Text = "";
            }
        }

        private void beditCan2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                beditCan2.Text = info.Name;
            }
            else
            {
                beditCan2.Text = "";
            }
        }

        private void beditCan3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                beditCan3.Text = info.Name;
            }
            else
            {
                beditCan3.Text = "";
            }
        }
        private FileInfo GetFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            FileInfo info;
            dialog.ShowDialog();
            try
            {
                string path = Path.GetFullPath(dialog.FileName);
                info = new FileInfo(path);
            }
            catch(ArgumentException)
            {
                info = null;
            }
            return info;
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                buttonEdit1.Text = info.Name;
            }
            else
            {
                buttonEdit1.Text = "";
            }
        }

        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FileInfo info = GetFile();
            if (info != null)
            {
                buttonEdit2.Text = info.Name;
            }
            else
            {
                buttonEdit2.Text = "";
            }
        }

        private void gridView3_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //set project cmd
            //to do
            int[] handle = gridView3.GetSelectedRows();
            int h = gridView3.GetDataSourceRowIndex(handle[0]);
            List<TStep> list = (List<TStep>)gridControlTest.DataSource;
            TStep nextTstep = list[h];
            setProjCMD(nextTstep.CmdList);
        }

        private void gridView6_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //no use
        }

        private void gridView6_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //set init & set & result judge & save
            //get cmd selected row
            int[] handle = gridView6.GetSelectedRows();
            int h = gridView6.GetDataSourceRowIndex(handle[0]);
            List<TCMD> tcmds = (List<TCMD>)gridControlProject.DataSource;
            TCMD nextTCMD = tcmds[h];
            setOther(nextTCMD);
        }


    }

}
