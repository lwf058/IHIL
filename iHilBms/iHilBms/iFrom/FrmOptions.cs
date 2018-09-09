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
    public partial class FrmOptions : Form
    {
        private void SaveSet()
        {
            THilConfig.gOption.Ip = EdtIP.Text;
            THilConfig.gOption.Port = Convert.ToInt16(sEdtPort.Text);
            THilConfig.gOption.SavePath = bEdtSavePath.Text;
            THilConfig.gOption.Device = bEdtDevice.Text;
            THilConfig.gOption.Save();

        }
        private void InitSet()
        {
            EdtIP.Text = THilConfig.gOption.Ip;
            sEdtPort.Value = THilConfig.gOption.Port;
            bEdtSavePath.Text = THilConfig.gOption.SavePath;
            bEdtDevice.Text = THilConfig.gOption.Device;

        }
        public FrmOptions()
        {
            InitializeComponent();
            InitSet();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveSet();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void bEdtDevice_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "Select Folder";

            if( dialog.ShowDialog()== DialogResult.OK)
                bEdtDevice.Text = dialog.SelectedPath;
        }

        private void bEdtSavePath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            dialog.Description = "Select Folder";

            if (dialog.ShowDialog() == DialogResult.OK)
                bEdtSavePath.Text = dialog.SelectedPath;
        }
    }
}
