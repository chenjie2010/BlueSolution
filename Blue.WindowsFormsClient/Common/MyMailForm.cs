using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class MyMailForm : Form
    {
        #region 契约接口

        private readonly IPrivateMailContract privateMailContract;        
        private readonly IPriavteAttachmentContract priavteAttachmentContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 私有变量

        private readonly DevExpress.Utils.ToolTipControllerShowEventArgs toolTipArgs;
        private MailBoxType currentMailBoxType;
        private string currentCondition;

        #endregion

        #region 构造函数

        public MyMailForm()
        {
            InitializeComponent();
            privateMailContract = GeneralAffairChannelFactory.CreatePrivateMailContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();

            toolTipArgs = toolTipController.CreateShowArgs();
            toolTipArgs.IconType = DevExpress.Utils.ToolTipIconType.Information;
            toolTipArgs.IconSize = DevExpress.Utils.ToolTipIconSize.Small;
            toolTipArgs.ImageIndex = 0;            
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyMailForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            mailControl.PrivateMailContract = privateMailContract;
            mailControl.PriavteAttachmentContract = priavteAttachmentContract;
            mailControl.UserAccountContract = userAccountContract;
            drgMail.DevExpressGridView.RowStyle += DevExpressGridView_RowStyle;
            this.BeginInvoke(new MethodInvoker(LoadDefaultData));
            UserControlHelper.InitImageComboBoxEdit(icmbBox, typeof(MailBoxType));
        }

        private void DevExpressGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {           
            if (e.RowHandle >= 0 && currentMailBoxType == MailBoxType.InBox)
            {
                object obj = drgMail.DevExpressGridView.GetRowCellValue(e.RowHandle, "ReadStatus");
                if (obj != null)
                {
                    MailState mailState = (MailState)DataConvertionHelper.GetByte(obj);
                    switch (mailState)
                    {
                        case MailState.New:
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            break;
                        case MailState.Pending:
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            e.Appearance.ForeColor = Color.OrangeRed;
                            break;
                    }
                }
            }
        }

        private void nbiInBox_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            currentMailBoxType = MailBoxType.InBox;
            currentCondition = string.Empty;
            LoadData(MailBoxType.InBox, string.Empty);
        }

        private void nbiOutBox_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            currentMailBoxType = MailBoxType.OutBox;
            currentCondition = string.Empty;
            LoadData(MailBoxType.OutBox, string.Empty);
        }

        private void nbiDraft_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            currentMailBoxType = MailBoxType.DraftBox;
            currentCondition = string.Empty;
            LoadData(MailBoxType.DraftBox, string.Empty);
        }

        private void nbiRecycleBin_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            currentMailBoxType = MailBoxType.RecycleBin;
            currentCondition = string.Empty;
            LoadData(MailBoxType.RecycleBin, string.Empty);
        }

        /// <summary>
        /// 提示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drgMail_OnGridMouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hint = drgMail.DevExpressGridView.CalcHitInfo(e.X, e.Y);
            string toolTip = string.Empty;
            /*记录数大于0, 有效的单元格 在指定的列显示*/
            if ((drgMail.DevExpressGridView.DataRowCount > 0) && hint.InRowCell && 
                (hint.Column.FieldName == "MailPriority" || hint.Column.FieldName == "HasAttachment" || hint.Column.FieldName == "ReadStatus" || hint.Column.FieldName == "DeliveryMode"))
            {
                //取出当前行
                DataRow hintRow = drgMail.DevExpressGridView.GetDataRow(hint.RowHandle);
                toolTipArgs.Title = hint.Column.ToString();
                if (hintRow[hint.Column.FieldName] != null && hintRow[hint.Column.FieldName] != DBNull.Value)
                {
                    if (hint.Column.FieldName == "MailPriority")
                    {
                        MailPriority mailPriority = (MailPriority)(byte)hintRow[hint.Column.FieldName];
                        toolTip = UserEnumHelper.GetEnumText(mailPriority);
                    }
                    else if(hint.Column.FieldName == "HasAttachment")
                    {
                        bool result = (bool)hintRow[hint.Column.FieldName];                        
                        toolTip = result ? "有附件" : "无附件";
                    }
                    else if (hint.Column.FieldName == "ReadStatus")
                    {
                        MailState mailState = (MailState)(byte)hintRow[hint.Column.FieldName];
                        toolTip = UserEnumHelper.GetEnumText(mailState);
                    }
                    else if (hint.Column.FieldName == "DeliveryMode")
                    {
                        MailDeliveryMode deliveryMode = (MailDeliveryMode)(byte)hintRow[hint.Column.FieldName];
                        toolTip = UserEnumHelper.GetEnumText(deliveryMode);
                    }
                }
            }
            toolTipArgs.ToolTip = toolTip;
            if (!string.IsNullOrWhiteSpace(toolTip))
            {                
                toolTipController.ShowHint(toolTipArgs, drgMail.DevExpressGridControl.PointToScreen(new Point(e.X, e.Y)));
            }
            else
            {
                toolTipController.HideHint();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            currentCondition = txtCondition.Text.Trim();
            currentMailBoxType = (MailBoxType)Convert.ToByte(icmbBox.EditValue);
            LoadData(currentMailBoxType, currentCondition);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCondition.Text = string.Empty;
            icmbBox.SelectedIndex = 0;
            currentCondition = string.Empty;
            drgMail.CurrentPageIndex = 0;
            LoadData(currentMailBoxType, string.Empty);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drgMail_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            drgMail.CurrentPageIndex = e.NewPageIndex;
            LoadData(currentMailBoxType, currentCondition);
        }

        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (drgMail.MultiSelectedValues.Count == 0)
                {
                    if (drgMail.FocusedRowHandle >= 0)
                    {
                        if (MessageBox.Show("确认删除所选择的邮件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                            privateMailContract.Delete(CurrentUser.Instance.UserId, mailId, currentMailBoxType);
                            drgMail.CurrentPageIndex = 0;
                            LoadData(currentMailBoxType, currentCondition);
                            Cursor = Cursors.Default;
                            MessageBox.Show("邮件删除成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择需要删除的邮件。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (MessageBox.Show("确认删除所选择的邮件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        IList<decimal> mailIds = new List<decimal>(drgMail.MultiSelectedValues.Count);
                        foreach (RowEvent rowEvent in drgMail.MultiSelectedValues)
                        {
                            decimal mailId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                            mailIds.Add(mailId);
                        }
                        Cursor = Cursors.WaitCursor;
                        privateMailContract.Delete(CurrentUser.Instance.UserId, mailIds, currentMailBoxType);
                        drgMail.CurrentPageIndex = 0;
                        LoadData(currentMailBoxType, currentCondition);
                        Cursor = Cursors.Default;
                        MessageBox.Show("邮件删除成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        private void bbiWrite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MailDeliveryForm frmMailDelivery = new MailDeliveryForm();
            frmMailDelivery.MdiParent = this.MdiParent;
            frmMailDelivery.Show();
        }

        /// <summary>
        /// 行单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drgMail_OnRowClick(object sender, RowEvent e)
        {
            if (drgMail.FocusedRowHandle >= 0)
            {
                decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                if (currentMailBoxType == MailBoxType.InBox)
                {
                    byte readStatus = (byte)DataConvertionHelper.GetByte(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "ReadStatus"));
                    if ((MailState)readStatus == MailState.New)
                    {
                        privateMailContract.UpdateReadStatus(mailId, CurrentUser.Instance.UserId, MailState.HasAlreadyRead);
                        drgMail.SetRowCellValue(drgMail.FocusedRowHandle, "ReadStatus", (byte)MailState.HasAlreadyRead);
                    }
                }
                mailControl.LoadData(mailId);
            }
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drgMail_OnRowDoubleClick(object sender, RowEvent e)
        {
            if (drgMail.FocusedRowHandle >= 0)
            {
                decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                switch (currentMailBoxType)
                {
                    case MailBoxType.DraftBox:                        
                        MailDeliveryForm frmMailDelivery = new MailDeliveryForm();
                        frmMailDelivery.MailId = mailId;
                        frmMailDelivery.MdiParent = this.MdiParent;
                        frmMailDelivery.RefreshData = () => {
                            LoadData(currentMailBoxType, currentCondition);
                        };
                        frmMailDelivery.Show();
                        break;

                    case MailBoxType.InBox:
                    case MailBoxType.OutBox:
                    case MailBoxType.RecycleBin:
                        if (currentMailBoxType == MailBoxType.InBox)
                        {
                            byte readStatus = (byte)DataConvertionHelper.GetByte(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "ReadStatus"));
                            if ((MailState)readStatus == MailState.New)
                            {
                                privateMailContract.UpdateReadStatus(mailId, CurrentUser.Instance.UserId, MailState.HasAlreadyRead);
                                drgMail.SetRowCellValue(drgMail.FocusedRowHandle, "ReadStatus", (byte)MailState.HasAlreadyRead);
                            }
                        }
                        MailViewForm frmMailView = new MailViewForm();
                        frmMailView.UserAccountContract = userAccountContract;
                        frmMailView.PrivateMailContract = privateMailContract;
                        frmMailView.PriavteAttachmentContract = priavteAttachmentContract;
                        frmMailView.MailId = mailId;
                        frmMailView.ShowDialog();
                        break;
                }
            }
        }

        /// <summary>
        /// 回复邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiReply_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (drgMail.FocusedRowHandle >= 0 && currentMailBoxType == MailBoxType.InBox)
            {
                decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                PrivateMailInfo privateMailInfo = privateMailContract.GetModelInfo(mailId);
                MailDeliveryForm frmMailDelivery = new MailDeliveryForm();
                frmMailDelivery.IsTransferred = false;
                if (privateMailInfo != null)
                {
                    UserAccountInfo userAccountInfo = userAccountContract.GetModelInfo(privateMailInfo.UserId);
                    if (userAccountInfo != null)
                    {
                        frmMailDelivery.Receiver = string.Format("{0}@{1}", userAccountInfo.UserName, userAccountInfo.UserActualName);
                    }
                    frmMailDelivery.MailTitle = string.Format("Re:{0}", privateMailInfo.MailTitle);
                }
                frmMailDelivery.MdiParent = this.MdiParent;
                frmMailDelivery.Show();
            }
        }

        /// <summary>
        /// 转发邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (drgMail.FocusedRowHandle >= 0 && (currentMailBoxType == MailBoxType.InBox || currentMailBoxType == MailBoxType.OutBox))
            {
                decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                PrivateMailInfo privateMailInfo = privateMailContract.GetModelInfo(mailId);
                MailDeliveryForm frmMailDelivery = new MailDeliveryForm()
                {
                    MailId = mailId,
                    IsTransferred = true,
                    MdiParent = this.MdiParent
                };
                frmMailDelivery.Show();
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string path = Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles);
            DirectoryInfo directory = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                FileInfo[] files = directory.GetFiles();
                foreach (var item in files)
                {
                    try
                    {
                        File.Delete(item.FullName);
                    }
                    catch { }
                }
            }
            LoadData(currentMailBoxType, string.Empty);
        }

        private void bbiOld_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentMailBoxType == MailBoxType.InBox)
            {
                SetMailState(MailState.HasAlreadyRead);
            }
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMailState(MailState.New);

        }

        private void bbiPending_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMailState(MailState.Pending);
        }

        private void bbiComplete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMailState(MailState.Completed);
        }
        
        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRecovery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentMailBoxType == MailBoxType.RecycleBin)
            {
                if (drgMail.MultiSelectedValues.Count == 0)
                {
                    if (drgMail.FocusedRowHandle >= 0)
                    {
                        if (MessageBox.Show("确认还原所选择的邮件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                            privateMailContract.RecoverMail(CurrentUser.Instance.UserId, mailId);
                            drgMail.CurrentPageIndex = 0;
                            LoadData(currentMailBoxType, currentCondition);
                            Cursor = Cursors.Default;
                            MessageBox.Show("邮件还原成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先选择需要还原的邮件。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (MessageBox.Show("确认还原所选择的邮件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        IList<decimal> mailIds = new List<decimal>(drgMail.MultiSelectedValues.Count);
                        foreach (RowEvent rowEvent in drgMail.MultiSelectedValues)
                        {
                            decimal mailId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                            mailIds.Add(mailId);
                        }
                        Cursor = Cursors.WaitCursor;
                        privateMailContract.RecoverMail(CurrentUser.Instance.UserId, mailIds);
                        drgMail.CurrentPageIndex = 0;
                        LoadData(currentMailBoxType, currentCondition);
                        Cursor = Cursors.Default;
                        MessageBox.Show("邮件还原成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载默认数据
        /// </summary>
        private void LoadDefaultData()
        {
            currentMailBoxType = MailBoxType.InBox;
            currentCondition = string.Empty;
            LoadData(MailBoxType.InBox, string.Empty);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="mailBoxType"></param>
        /// <param name="condition"></param>
        private void LoadData(MailBoxType mailBoxType, string condition)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                progressPanel.Show();

                int totalCount = 0;
                switch (mailBoxType)
                {
                    case MailBoxType.OutBox:
                        drgMail.DataSource = privateMailContract.GetEmailsInBox(CurrentUser.Instance.UserId, condition, false, drgMail.PageSize * drgMail.CurrentPageIndex,
                            drgMail.PageSize, ref totalCount).Tables[0];                        
                        break;

                    case MailBoxType.DraftBox:
                        drgMail.DataSource = privateMailContract.GetEmailsInBox(CurrentUser.Instance.UserId, condition, true, drgMail.PageSize * drgMail.CurrentPageIndex,
                            drgMail.PageSize, ref totalCount).Tables[0];
                        break;

                    case MailBoxType.InBox:
                    case MailBoxType.RecycleBin:
                        if (mailBoxType == MailBoxType.InBox)
                        {
                            drgMail.DataSource = privateMailContract.GetReceivedEmails(CurrentUser.Instance.UserId, condition, drgMail.PageSize * drgMail.CurrentPageIndex,
                                drgMail.PageSize, ref totalCount).Tables[0];
                        }
                        else
                        {
                            drgMail.DataSource = privateMailContract.GetEmailsInRecycleBin(CurrentUser.Instance.UserId, condition, drgMail.PageSize * drgMail.CurrentPageIndex,
                            drgMail.PageSize, ref totalCount).Tables[0];
                        }
                        IList<EnumItem> deliveryModes = UserEnumHelper.GetEnumItems(typeof(MailDeliveryMode));
                        IList<EnumItem> mailStates = UserEnumHelper.GetEnumItems(typeof(MailState));
                        drgMail.Columns["DeliveryMode"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(deliveryModes, icMailDeliveryMode);
                        drgMail.Columns["ReadStatus"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(mailStates, icMailState);
                        drgMail.Columns["DeliveryMode"].Width = 50;
                        drgMail.Columns["ReadStatus"].Width = 50;
                        drgMail.Columns["DeliveryMode"].OptionsColumn.FixedWidth = true;
                        drgMail.Columns["ReadStatus"].OptionsColumn.FixedWidth = true;
                        break;
                }
                if (!string.IsNullOrWhiteSpace(condition))
                {
                    gcList.Text = string.Format("{0}_查询结果", UserEnumHelper.GetEnumText(mailBoxType));
                }
                else
                {
                    gcList.Text = string.Format("{0}_邮件列表", UserEnumHelper.GetEnumText(mailBoxType));
                }
                IList<EnumItem> priorities = UserEnumHelper.GetEnumItems(typeof(MailPriority));
                BooleanItem firstItem = new BooleanItem("无", false);
                BooleanItem secondItem = new BooleanItem("有", true);
                drgMail.Columns["MailPriority"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(priorities, icPriority);
                drgMail.Columns["HasAttachment"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(firstItem, secondItem, icAttach);
                drgMail.Columns["SendTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                drgMail.Columns["SendTime"].DisplayFormat.FormatString = "G";
                drgMail.Columns["MailPriority"].Width = 50;
                drgMail.Columns["HasAttachment"].Width = 50;
                drgMail.Columns["SendTime"].Width = 150;
                drgMail.Columns["MailPriority"].OptionsColumn.FixedWidth = true;
                drgMail.Columns["HasAttachment"].OptionsColumn.FixedWidth = true;
                drgMail.Columns["SendTime"].OptionsColumn.FixedWidth = true;

                drgMail.RecordCount = totalCount;
                drgMail.FocusedRowHandle = -1;
                if (drgMail.RowCount > 0)
                {
                    switch (currentMailBoxType)
                    {
                        case MailBoxType.InBox:
                            bbiReply.Enabled = true;
                            bbiTransfer.Enabled = true;
                            blcMark.Enabled = true;
                            bbiRecovery.Enabled = false;
                            break;

                        case MailBoxType.OutBox:
                            bbiReply.Enabled = false;
                            bbiTransfer.Enabled = true;
                            blcMark.Enabled = false;
                            bbiRecovery.Enabled = false;
                            break;

                        case MailBoxType.RecycleBin:
                            bbiReply.Enabled = false;
                            bbiTransfer.Enabled = false;
                            blcMark.Enabled = false;
                            bbiRecovery.Enabled = true;
                            break;

                        default:
                            bbiReply.Enabled = false;
                            bbiTransfer.Enabled = false;
                            blcMark.Enabled = false;
                            bbiRecovery.Enabled = false;
                            break;
                    }
                }
                else
                {
                    bbiReply.Enabled = false;
                    bbiTransfer.Enabled = false;
                    blcMark.Enabled = false;
                    bbiRecovery.Enabled = false;
                }
                progressPanel.Hide();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
        
        /// <summary>
        /// 设置邮件状态
        /// </summary>
        /// <param name="mailState"></param>
        private void SetMailState(MailState mailState)
        {
            try
            {
                if (drgMail.MultiSelectedValues.Count == 0)
                {
                    if (drgMail.FocusedRowHandle >= 0 && currentMailBoxType == MailBoxType.InBox)
                    {
                        Cursor = Cursors.WaitCursor;
                        decimal mailId = DataConvertionHelper.GetDecimal(drgMail.GetRowCellValue(drgMail.FocusedRowHandle, "MailId"));
                        privateMailContract.UpdateReadStatus(mailId, CurrentUser.Instance.UserId, mailState);
                        drgMail.SetRowCellValue(drgMail.FocusedRowHandle, "ReadStatus", (byte)mailState);
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("请先选择需要设置状态的邮件。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    IList<decimal> mailIds = new List<decimal>(drgMail.MultiSelectedValues.Count);
                    foreach (RowEvent rowEvent in drgMail.MultiSelectedValues)
                    {
                        decimal mailId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                        mailIds.Add(mailId);
                    }
                    Cursor = Cursors.WaitCursor;
                    privateMailContract.UpdateReadStatus(mailIds, CurrentUser.Instance.UserId, mailState);
                    foreach (RowEvent rowEvent in drgMail.MultiSelectedValues)
                    {
                        drgMail.SetRowCellValue(rowEvent.RowHandle, "ReadStatus", (byte)mailState);
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
           
        }

        #endregion
    }
}
