using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.WinFormsLibrary;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class SaveSnapshotForm : Form
    {
        public SaveSnapshotDelegate SaveCoverSnapshot
        {
            get;
            set;
        }

        public SaveSnapshotForm()
        {
            InitializeComponent();
        }

        private void SaveSnapshotForm_Load(object sender, EventArgs e)
        {

        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string snapshotName = etxtSnapshotName.Text.Trim();
                DateTime expireDate = dateExpireDate.DateTime;
                string notes = txtNotes.Text.Trim();

                if (string.IsNullOrWhiteSpace(snapshotName))
                {
                    etxtSnapshotName.Focus();
                    MessageBox.Show("快照名称不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (DataConvertionHelper.IsNullValue(expireDate))
                {
                    MessageBox.Show("快照时间不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (SaveCoverSnapshot != null)
                {
                    SaveCoverSnapshot(snapshotName, expireDate, notes);
                }
                MessageBox.Show("保存快照成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
            this.Close();
        }

        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
