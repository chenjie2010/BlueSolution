using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Office.Services;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsControls;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.CustomLibrary;
using Blue.Model.GeneralAffairModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class SendNoticeForm : Form
    {
        #region 私有常量

        private const int ATTACHEMENT_MAX_COUNT = 5;

        #endregion

        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserAccountContract userAccountContract = null;
        private readonly ICustomRoleContract customRoleContract = null;
        private readonly IPriavteAttachmentContract priavteAttachmentContract = null;
        private readonly IUserMessageContract userMessageContract = null;

        #endregion

        #region 私有变量
        

        #endregion

        #region 内部成员变量

        private decimal _messageId = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 加载数据的方法委托
        /// </summary>
        public LoadDataDelegate LoadData
        {
            get;
            set;
        }

        /// <summary>
        /// 编辑状态
        /// </summary>
        public EditState EditState
        {
            get;
            set;
        }

        /// <summary>
        /// 消息编号
        /// </summary>
        public decimal MessageId
        {
            get
            {
                return _messageId;
            }
            set
            {
                _messageId = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SendNoticeForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
            userMessageContract = SystemChannelFactory.CreateUserMessageContract();
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendMailForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            UserControlHelper.InitImageComboBoxEdit(icmbMessageType, typeof(MessageType));
            dtStartTime.EditValue = DateTime.Now;
            dtEndTime.EditValue = DateTime.Now.AddMonths(1);
            openFileDialog.Filter = AppSettingHelper.DefaultDocFormat;
            switch (EditState)
            {
                case EditState.Add:
                    this.Text = "增加";
                    break;

                case EditState.Edit:
                    this.Text = "编辑";
                    if (_messageId > 0)
                    {
                        IList<CommonNode> commonNodes = userMessageContract.GetRoles(_messageId);
                        btxtReceiver.Tag = commonNodes;
                        btxtReceiver.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
                        UserMessageInfo userMessageInfo = userMessageContract.GetModelInfo(_messageId);
                        txtMessageTitle.Text = userMessageInfo.MessageTitle;
                        icmbMessageType.EditValue = userMessageInfo.MessageType;
                        dtStartTime.EditValue = userMessageInfo.InitalTime;
                        dtEndTime.EditValue = userMessageInfo.ExpiredTime;
                        chkIsDraft.Checked = userMessageInfo.IsDraft;
                        chkPriority.Checked = (userMessageInfo.MessagePriority > 0) ? true : false;
                        richEditControl.HtmlText = userMessageInfo.MessageContent;
                        MessageType messageType = (MessageType)userMessageInfo.MessageType;
                        AttachmentCategory attachmentCategory = AttachmentCategory.Notice;
                        if (messageType == MessageType.SystemMessage)
                        {
                            attachmentCategory = AttachmentCategory.Message;
                        }
                        IList<PriavteAttachmentInfo> priavteAttachmentInfos = priavteAttachmentContract.GetModelInfos(_messageId, (byte)attachmentCategory);
                        foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                        {
                            ExtendedUpLoadFileInfo upLoadFileInfo = new ExtendedUpLoadFileInfo(priavteAttachmentInfo.AttachmentName, priavteAttachmentInfo.AttachmentSourceName,
                                        null, AttachmentType.Attachment);
                            chkAttachment.Items.Add(upLoadFileInfo);
                        }
                    }
                    break;
            }
            txtMessageTitle.Focus();
        }
        
        /// <summary>
        /// 屏蔽部分右键菜单项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richEditControl_PopupMenuShowing(object sender, DevExpress.XtraRichEdit.PopupMenuShowingEventArgs e)
        {
            e.Menu.Items[12].Visible = false;
            e.Menu.Items[13].Visible = false;
            e.Menu.Items[14].Visible = false;
            e.Menu.Items[15].Visible = false;

        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnSendMail_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                string receiver = btxtReceiver.Text.Trim();               
                string messageTitle = txtMessageTitle.Text.Trim();
                if (string.IsNullOrEmpty(receiver))
                {
                    Cursor = Cursors.Default;
                    btxtReceiver.Focus();
                    MessageBox.Show("发送对象不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(messageTitle))
                {
                    Cursor = Cursors.Default;
                    txtMessageTitle.Focus();
                    MessageBox.Show("标题不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(richEditControl.HtmlText))
                {
                    Cursor = Cursors.Default;
                    richEditControl.Focus();
                    MessageBox.Show("消息内容不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (richEditControl.HtmlText.Length > AppSettingHelper.DefaultMailCharactersSize)
                {
                    Cursor = Cursors.Default;
                    richEditControl.Focus();
                    MessageBox.Show(string.Format("消息内容不能超过{0}个字符。", AppSettingHelper.DefaultMailCharactersSize), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                IList<CommonNode> roleCommonNodes = btxtReceiver.Tag as IList<CommonNode>;
                IList<decimal> roleIds = CommonObjHelper.GetCommonNodeIds(roleCommonNodes);                
                byte priority = (byte)(chkPriority.Checked ? 1 : 0);
                if (dtStartTime.EditValue == null || DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dtStartTime.EditValue, DateTime.MinValue)))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("起始时间不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtEndTime.EditValue == null || DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dtEndTime.EditValue, DateTime.MinValue)))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("截止时间不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime initalTime = Convert.ToDateTime(DataConvertionHelper.GetConvertedDateTime(dtStartTime.EditValue).ToString("d"));
                DateTime expiredTime = Convert.ToDateTime(DataConvertionHelper.GetConvertedDateTime(dtEndTime.EditValue).ToString("d"));
                if (expiredTime < initalTime)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("起始时间不能大于截止时间！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageType messageType = (MessageType)Convert.ToByte(icmbMessageType.EditValue);
                AttachmentCategory attachmentCategory = AttachmentCategory.Notice;
                if (messageType == MessageType.SystemMessage)
                {
                    attachmentCategory = AttachmentCategory.Message;
                }
                byte messagePriority = Convert.ToByte(chkPriority.Checked ? 1 : 0);
                bool isAttach = chkAttachment.Items.Count > 0 ? true : false;
                UserMessageInfo userMessageInfo = new UserMessageInfo(_messageId, CurrentUser.Instance.UserId, messageTitle, richEditControl.HtmlText, Convert.ToByte(icmbMessageType.EditValue),
                            chkIsDraft.Checked, isAttach, messagePriority, initalTime, expiredTime, DateTime.Now);
                string verifyResult = string.Empty;
                bool result = ValidationHelper.Validate<UserMessageInfo>(userMessageInfo, out verifyResult);
                if (result)
                {
                    IList<ExtendedUpLoadFileInfo> upLoadFileInfos = new List<ExtendedUpLoadFileInfo>();
                    foreach (CheckedListBoxItem item in chkAttachment.Items)
                    {
                        ExtendedUpLoadFileInfo file = item.Value as ExtendedUpLoadFileInfo;
                        if (file != null)
                        {
                            upLoadFileInfos.Add(file);
                        }
                    }
                    if (_messageId > 0)
                    {
                        userMessageContract.UpdateUserMessageInfo(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
                    }
                    else
                    {
                        userMessageContract.InsertUserMessage(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
                    }
                    LoadData?.Invoke();
                    Cursor = Cursors.Default;
                    MessageBox.Show("信息保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    this.Close();
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
     
        #endregion

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnAddAttachement_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(openFileDialog.FileName) && Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (chkAttachment.Items.Count >= AppSettingHelper.DefaultMailAttachments)
                {
                    MessageBox.Show(string.Format("附件数量不能超过{0}个。", AppSettingHelper.DefaultMailAttachments), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (FileFormatHelper.VerfiyFileFormat(openFileDialog.FileName))
                {
                    byte[] data = null;
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader r = new BinaryReader(fs);
                        data = r.ReadBytes((int)fs.Length);
                    }
                    if (data.Length == 0)
                    {
                        MessageBox.Show("附件的文件大小不能为0。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (data.Length > AppSettingHelper.DefaultFileSize)
                    {
                        MessageBox.Show(string.Format("单个附件大小不能超过 {0} MB.", AppSettingHelper.DefaultFileSize / (1024 * 1024)),
                            "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ExtendedUpLoadFileInfo upLoadFileInfo = new ExtendedUpLoadFileInfo(Path.GetFileName(openFileDialog.FileName), data, AttachmentType.Attachment);
                    if (upLoadFileInfo.UpLoadSourceFileName.Length > 256)
                    {
                        upLoadFileInfo.UpLoadSourceFileName = upLoadFileInfo.UpLoadSourceFileName.Substring(0, 256);
                    }
                    foreach (CheckedListBoxItem item in chkAttachment.Items)
                    {
                        ExtendedUpLoadFileInfo file = item.Value as ExtendedUpLoadFileInfo;
                        if (file != null && upLoadFileInfo.UpLoadSourceFileName.Equals(file.UpLoadSourceFileName)
                            && (upLoadFileInfo.UpLoadFileData.Length == file.UpLoadFileData.Length))
                        {
                            MessageBox.Show("附件已存在，不能重复添加。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    CheckedListBoxItem checkedListBoxItem = new CheckedListBoxItem(upLoadFileInfo, upLoadFileInfo.UpLoadSourceFileName);
                    chkAttachment.Items.Add(checkedListBoxItem);
                }
                else
                {
                    MessageBox.Show("不允许上传该文件格式的文件", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void sbtnRemoveAttachement_Click(object sender, EventArgs e)
        {
            if (chkAttachment.CheckedItemsCount == 0)
            {
                MessageBox.Show("请先选择要移除的附件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认移除所选择的附件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                for (int i = chkAttachment.Items.Count - 1; i >= 0; i--)
                {
                    if (chkAttachment.Items[i].CheckState == CheckState.Checked)
                    {
                        chkAttachment.Items.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 选择接受人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtReceiver_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> nodes = btxtReceiver.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
            {
                btxtReceiver.Tag = commonNodes;
                btxtReceiver.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
            };
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(nodes);
            frmMultiSelectedItems.ShowDialog();
        }
    }
}
