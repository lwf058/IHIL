using DevExpress.XtraTreeList.Nodes;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraTreeList;
using System;

namespace Scheme
{
    class FolderService
    {
        /// 方案的顶级路径
        private string schemeTopPath = "";

        public string SchemeTopPath { get => schemeTopPath; set => schemeTopPath = value; }

        public FolderService() {
            SchemeTopPath = Application.StartupPath + @"\category";
        }

        public FolderService(string spath) {//有参构造，传入路径
            SchemeTopPath = spath;
        }

        /// <summary>
        /// traverse the folders and its childern
        /// </summary>
        /// <param name="ParentNode"></param>
        public void traverse_Folder(TreeListNode parentNode,TreeList treeList)
        {
            if (parentNode.Tag == null) return;
            string path = parentNode.Tag.ToString();
            DirectoryInfo DirInfo = new DirectoryInfo(path);
            if (!DirInfo.Exists) return;
            foreach (DirectoryInfo childFolder in DirInfo.GetDirectories())
            {
                TreeListNode ChildNode = treeList.AppendNode(null, parentNode);
                ChildNode.SetValue(treeList.Columns["FolderName"], childFolder.Name);
                ChildNode.Tag = childFolder.FullName;
                traverse_Folder(ChildNode,treeList);
            }
        }
        /// <summary>
        /// Add a new folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void add_Folder(string path,TreeList treeList) {
            if (path != null)
            {
                DirectoryInfo info = new DirectoryInfo(path);
                TreeListNode node = treeList.AppendNode(null, null);
                node.Tag = path;
                node.SetValue(treeList.Columns["FolderName"], info.Name);
                traverse_Folder(node,treeList);
                treeList.Columns["FolderName"].SortOrder = SortOrder.Ascending;
                //sorting completed!
            }
        }


        /// <summary>
        /// Remove the folder
        /// while treeClass is empty it will throw a NullReferenceException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void remove_Folder(TreeList treeList) {
            //remove the focused folder
            TreeListNode treeListNode = treeList.FocusedNode;
            try
            {
                treeListNode.Remove();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("can't remove the folder because the list is empty!");
                return;
            }
        }
        /// <summary>
        /// 获取选中文件夹的路径
        /// </summary>
        /// <param name="node"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public string get_Folder(TreeListNode node, string folder, TreeList treeList)
        {
            if (node.ParentNode == null)
            {
                return this.SchemeTopPath + folder;
            }
            else
            {
                string newfolder = node.GetValue(treeList.Columns["FolderName"]).ToString() + "\\" + folder;
                return get_Folder(node.ParentNode, newfolder, treeList);
            }
        }
    }
}
