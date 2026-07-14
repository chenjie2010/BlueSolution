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
    public partial class SnapshotForm : Form
    {
        #region 契约接口

        private readonly ICustomSnapshotContract customSnapshotContract;

        #endregion

        #region 属性

        /// <summary>
        /// 打开快照委托
        /// </summary>
        public OpenSnapshotDelegate OpenSnapshot
        {
            get;
            set;
        }

        /// <summary>
        /// 表套编号
        /// </summary>
        public decimal ReportId
        {
            get;
            set;
        }

        #endregion

        #region 契约接口

        public SnapshotForm()
        {
            InitializeComponent();
            customSnapshotContract = BusinessDesignerChannelFactory.CreateCustomSnapshotContract();
        }

        #endregion

        #region 窗体及控件方法

        private void SnapshotForm_Load(object sender, EventArgs e)
        {
            IList<CommonItem<decimal>> downListItems = customSnapshotContract.GetCommonItems(ReportId);
            lbcCoverList.Items.AddRange(downListItems.ToArray());
        }

         #endregion

        private void lbcCoverList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbcCoverList.SelectedItem != null)
            {
                CommonItem<decimal> downListItem = (CommonItem<decimal>)lbcCoverList.SelectedItem;
                CustomSnapshotInfo customSnapshotInfo = customSnapshotContract.GetModelInfo(downListItem.Value);
                txtSnapshotName.Text = customSnapshotInfo.SnapshotName;
                dateExpireDate.EditValue = customSnapshotInfo.ExpireDate;
                etxtNotes.Text = customSnapshotInfo.Notes;
            }
        }

        private void btnItmConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lbcCoverList.SelectedItem == null)
            {
                MessageBox.Show("未选择快照！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (OpenSnapshot != null)
            {
                CommonItem<decimal> downListItem = (CommonItem<decimal>)lbcCoverList.SelectedItem;
                OpenSnapshot(downListItem.Value);
            }
            this.Close(); 
        }

        private void btnItmClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnItmDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lbcCoverList.SelectedItem == null)
            {
                MessageBox.Show("未选择快照！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (MessageBox.Show("确认要删除当前快照？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    CommonItem<decimal> downListItem = (CommonItem<decimal>)lbcCoverList.SelectedItem;
                    customSnapshotContract.Delete(downListItem.Value);
                    int selectedIndex = lbcCoverList.SelectedIndex;
                    lbcCoverList.Items.RemoveAt(selectedIndex);
                    if (lbcCoverList.ItemCount > 0)
                    {
                        lbcCoverList.SelectedIndex = selectedIndex > 0 ? selectedIndex - 1 : 0;
                    }
                    else
                    {
                        txtSnapshotName.Text = string.Empty;
                        dateExpireDate.EditValue = null;
                        etxtNotes.Text = string.Empty;
                    }
                    MessageBox.Show("快照删除成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
    }
}
