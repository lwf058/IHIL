using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Scheme
{
    class FileService
    {
        /// 方案的顶级路径
        private string schemeTopPath = "";
        private DirectoryInfo theFolder;
        private string hiddenEditorType = "";//文件编辑状态

        public FileService() {
            schemeTopPath = Application.StartupPath + @"\category";
        }
        public FileService(string path)
        {
            schemeTopPath = path;
        }
 

        /// <summary>
        /// 遍历文件
        /// </summary>
        /// <param name="filepath"></param>
        public void traverse_File(string filepath, TreeList treeList)
        {
            //this.clearAll();
            //this.currentpackeol = null;
           theFolder = new DirectoryInfo(filepath);
            if (!theFolder.Exists) return;
            foreach (FileInfo childFile in theFolder.GetFiles())
            {
                TreeListNode fileNode = treeList.AppendNode(null, null);
                fileNode.Tag = childFile.FullName;
                //add node tag to bulid assosication between the filenode and the filepath
                fileNode.SetValue(treeList.Columns["FileName"], childFile.Name);
            }

        }
        /// <summary>
        /// 点击新增一个文件
        /// </summary>
        public void add_File(TreeList treeList) {
            TreeListNode newFileNode = treeList.AppendNode(null, null);
            newFileNode.Selected = true;
            hiddenEditorType = "addFile";
            treeList.ShowEditor();
        }/// <summary>
        /// 
        /// 点击重命名时调用
        /// </summary>
        /// <param name="treeList"></param>
        public void rename_File(TreeList treeList) {
            TreeListNode fileNode = treeList.FocusedNode;
            if (fileNode == null) return;
            fileNode.Selected = true;
            hiddenEditorType = "renameFile";
            treeList.ShowEditor();
        }
        /// <summary>
        /// 判断对文件操作，匹配各自操作
        /// </summary>
        public void opreator_File(TreeList treeFileList, TreeList treeFolderList) {
            if (hiddenEditorType == "addFile")
            {   
                add_XmlFile(treeFileList, treeFolderList);//未封装XMl类
            }
            if (hiddenEditorType == "renameFile")
            {
                rename_XmlFile(treeFileList, treeFolderList);//未封装XMl类
            }

        }
        private void add_XmlFile(TreeList treeFileList, TreeList treeFolderList) {
            TreeListNode currentFileNode = treeFileList.FocusedNode;
            TreeListNode parentFolerNode = treeFolderList.FocusedNode;
            if (currentFileNode == null || parentFolerNode == null) return;
            try
            {
                string filename = currentFileNode.GetValue(treeFileList.Columns["FileName"]).ToString();
                string filepath = parentFolerNode.Tag.ToString();
                string fullname = filepath + '\\' + filename;
                currentFileNode.Tag = fullname;
                //Console.WriteLine(fullname);
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "gb2312", "yes");
                xmlDoc.AppendChild(declaration);
                XmlElement RootElement = xmlDoc.CreateElement("chHIL");
                xmlDoc.AppendChild(RootElement);
                xmlDoc.Save(fullname);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("file name cannot be empty!", "Warning!");
                currentFileNode.Remove();
            }
            catch (IOException)
            {
                MessageBox.Show("Failed to create the file!", "Error");
                currentFileNode.Remove();
            }
            finally
            {
                hiddenEditorType = null;
            }
            return;
        }
        /// <summary>
        /// 给xml文档重命名操作
        /// </summary>
        /// <param name="treeFileList"></param>
        /// <param name="treeFolderList"></param>
        private void rename_XmlFile(TreeList treeFileList, TreeList treeFolderList)
        {
            TreeListNode node = treeFileList.FocusedNode;
            if (node == null) return;
            string sourceFile = node.Tag.ToString();
            string targetFile = node.GetValue(treeFileList.Columns["FileName"]).ToString();
            if (sourceFile != null)
            {
                FileInfo sFile = new FileInfo(sourceFile);
                DirectoryInfo dirInfo = sFile.Directory;
                targetFile = dirInfo.FullName + "\\" + targetFile;
                FileInfo tFile = new FileInfo(targetFile);
                if (sourceFile == targetFile) return;
                if (tFile.Exists)
                {
                    MessageBox.Show("Target file already exists!");
                    node.SetValue(treeFileList.Columns["FileName"], sFile.Name);
                }
                else
                {
                    sFile.MoveTo(targetFile);
                }
            }
            hiddenEditorType = null;
            return;
        }
        /// <summary>
        /// 点击删除文件
        /// </summary>
        public void remove_File(TreeList treeFileList) {
            try
            {
                TreeListNode deleteNode = treeFileList.FocusedNode;
                string deleteFile = deleteNode.Tag.ToString();
                treeFileList.DeleteNode(deleteNode);
                if (File.Exists(deleteFile))
                {
                    File.Delete(deleteFile);
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
            catch (IOException)
            {
                return;
            }
        }

    }

}
