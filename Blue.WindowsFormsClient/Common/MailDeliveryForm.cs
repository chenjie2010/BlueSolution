using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.Office.Services;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class MailDeliveryForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly IPrivateMailContract privateMailContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly IPriavteAttachmentContract priavteAttachmentContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有常量

        private const int ATTACHEMENT_MAX_SIZE = 30;

        #endregion

        #region 私有变量

        private Dictionary<string, UserAccountInfo> userInfoCache;
        Dictionary<string, string> inlineImageCache;
        private bool changedInReceiver = false;
        private bool changedInCopy = false;
        private bool changedInBlind = false;

        #endregion

        #region 属性

        /// <summary>
        /// 邮件编号
        /// </summary>
        public decimal MailId
        {
            get; set;
        }

        /// <summary>
        /// 是否是转发
        /// </summary>
        public bool IsTransferred
        {
            get;
            set;
        }

        /// <summary>
        /// 收件人信息
        /// </summary>
        public string Receiver
        {
            get { return txtReceiver.Text.Trim(); }
            set { txtReceiver.Text = value; }
        }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string MailTitle
        {
            get { return txtTitle.Text.Trim(); }
            set { txtTitle.Text = value; }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        public ThreadMethodInvoker RefreshData
        {
            get;set;
        }

        #endregion

        #region 构造函数

        public MailDeliveryForm()
        {
            InitializeComponent();
            MailId = 0;
            IsTransferred = false;
            userInfoCache = new Dictionary<string, UserAccountInfo>();
            inlineImageCache = new Dictionary<string, string>();
            userAccountContract = UserChannelFactory.CreateUserAccount();            
            privateMailContract = GeneralAffairChannelFactory.CreatePrivateMailContract();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            openFileDialog.Filter = AppSettingHelper.DefaultDocFormat;
            //bool mailInGroup = AuthorityHelper.CheckAuthority(CurrentUser.Instance.DataFieldAuthority,
            //    Convert.ToByte(SystemDataFieldPermission.MailInGroup));
            //btnRoles.Visible = mailInGroup;             
            bsiToolTip.Caption = string.Format("提示：附件个数不能超过{0}个，每个大小不能错过{1}MB；插入的图片数不能超过{0}，每个大小不能超过{2}MB。",
                AppSettingHelper.DefaultMailAttachments, AppSettingHelper.DefaultDocSize / (1024*1024), AppSettingHelper.DefaultMailPictureSize / (1024 * 1024));
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailDeliveryForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.changeFontNameItem1.EditValue = "新宋体";
            this.changeFontSizeItem1.EditValue = 12;
            LoadData();
        }
        private void richEditControl_PopupMenuShowing(object sender, DevExpress.XtraRichEdit.PopupMenuShowingEventArgs e)
        {
            for (int i = 12; i <= 20; i++)
            {
                e.Menu.Items[i].Visible = false;
            }
        }

        /// <summary>
        /// 点击角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoles_Click(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = btnRoles.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetRoleList;
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 获得选择的角色
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetRoleList(IList<CommonNode> commonNodes)
        {
            btnRoles.Tag = commonNodes;
            StringBuilder sb = new StringBuilder();
            string roleNames = CommonObjHelper.GetCommonNodeNamesWithSemicolon(commonNodes);
            string receiver = txtReceiver.Text.Trim();
            sb.AppendFormat(receiver);
            if (receiver.EndsWith(";"))
            {
                sb.Append(roleNames);
            }
            else
            {
                sb.AppendFormat("{0}; ", roleNames);
            }
            txtReceiver.Text = sb.ToString();
        }

        private void btnAttachment_Click(object sender, EventArgs e)
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
                    if(data.Length == 0)
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

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (chkAttachment.CheckedItemsCount == 0)
            {
                MessageBox.Show("请先选择要移除的附件。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!changedInReceiver)
                {
                    SendKeys.Send("{TAB}");// 发送“TAB”键，切换到下一控件。
                    return;
                }
                string receiver = txtReceiver.Text.Trim();
                if (!string.IsNullOrWhiteSpace(receiver))
                {
                    txtReceiver.Text = FormatReceiver(receiver);
                    txtReceiver.Select(txtReceiver.Text.Length, 0);
                    changedInReceiver = false;
                }
            }
        }

        /// <summary>
        /// 收件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReceiver_Leave(object sender, EventArgs e)
        {
            string receiver = txtReceiver.Text.Trim();
            if (!string.IsNullOrWhiteSpace(receiver))
            {
                txtReceiver.Text = FormatReceiver(receiver);
            }
        }

        /// <summary>
        /// 收件人文本内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReceiver_EditValueChanged(object sender, EventArgs e)
        {
            changedInReceiver = true;
        }
        
        /// <summary>
        /// 抄送人文本内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCopy_EditValueChanged(object sender, EventArgs e)
        {
            changedInCopy = true;
        }

        /// <summary>
        /// 密送文本内容更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlind_EditValueChanged(object sender, EventArgs e)
        {
            changedInBlind = true;
        }

        /// <summary>
        /// 抄送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCopy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!changedInCopy)
                {
                    SendKeys.Send("{TAB}");// 发送“TAB”键，切换到下一控件。
                    return;
                }
                string copy = txtCopy.Text.Trim();
                if (!string.IsNullOrWhiteSpace(copy))
                {
                    txtCopy.Text = FormatReceiver(copy);
                    txtCopy.Select(txtCopy.Text.Length, 0);
                    changedInCopy = false;
                }
            }
        }

        /// <summary>
        /// 密送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!changedInBlind)
                {
                    SendKeys.Send("{TAB}");// 发送“TAB”键，切换到下一控件。
                    return;
                }
                string blind = txtBlind.Text.Trim();
                if (!string.IsNullOrWhiteSpace(blind))
                {
                    txtBlind.Text = FormatReceiver(blind);
                    txtBlind.Select(txtBlind.Text.Length, 0);
                    changedInBlind = false;
                }
            }
        }

        /// <summary>
        /// 抄送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCopy_Leave(object sender, EventArgs e)
        {
            string copy = txtCopy.Text.Trim();
            if (!string.IsNullOrWhiteSpace(copy))
            {
                txtCopy.Text = FormatReceiver(copy);
            }
        }

        /// <summary>
        /// 密送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBlind_Leave(object sender, EventArgs e)
        {
            string blind = txtBlind.Text.Trim();
            if (!string.IsNullOrWhiteSpace(blind))
            {
                txtBlind.Text = FormatReceiver(blind);
            }
        }
        private void bbiDraft_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendMail(true);
        }

        private void bbiDelivery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendMail(false);
        }

        private void richEditControl_TextChanged(object sender, EventArgs e)
        {
            bsiCharactersCount.Caption = string.Format("当前字符共{0}个，邮件内容不能超过{1}个字符。", richEditControl.Text.Length, AppSettingHelper.DefaultMailCharactersSize);
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUser_Click(object sender, EventArgs e)
        {
            UserListForm frmUserList = new UserListForm();
            frmUserList.GetIdentifier = (userId) =>
            {
                if (userId > 0)
                {
                    UserAccountInfo userAccountInfo = userAccountContract.GetModelInfo(userId);
                    if (userAccountInfo != null)
                    {
                        txtReceiver.Text = string.Format("{0}@{1}", userAccountInfo.UserName, userAccountInfo.UserActualName);
                    }
                    else
                    {
                        txtReceiver.Text = string.Empty;
                        txtReceiver.Tag = null;
                    }
                }
            };
            frmUserList.ShowDialog();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 格化式收件人
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string FormatReceiver(string content)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            content = content.Replace("；", ";");
            if (content.EndsWith(";"))
            {
                content = content.Remove(content.Length - 1, 1);
            }
            string[] receivedNames = content.Split(';');
            foreach (string name in receivedNames)
            {
                int userIndex = name.IndexOf('@');
                if (userIndex > 0)
                {
                    commonNodes.Add(new CommonNode(0, name, 0));
                }
                else
                {
                    UserAccountInfo userAccountInfo = null;
                    if (userInfoCache.ContainsKey(name))
                    {
                        userAccountInfo = userInfoCache[name];
                    }
                    else
                    {
                        userAccountInfo = userAccountContract.GetModelInfo(name);
                        userInfoCache.Add(name, userAccountInfo);
                    }
                    if (userAccountInfo != null)
                    {
                        commonNodes.Add(new CommonNode(userAccountInfo.UserId,
                            string.Format("{0}@{1}", userAccountInfo.UserName, userAccountInfo.UserActualName), 0));
                    }
                    else
                    {
                        commonNodes.Add(new CommonNode(0, name, 0));
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (CommonNode commonNode in commonNodes)
            {
                sb.AppendFormat("{0}; ", commonNode.NodeName);
            }

            return sb.ToString().Trim();
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="isDraft"></param>
        private void SendMail(bool isDraft)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                IList<ExtendedUpLoadFileInfo> upLoadFileInfos = new List<ExtendedUpLoadFileInfo>();
                string receiver = txtReceiver.Text.Trim();
                string copy = txtCopy.Text.Trim();
                string blind = txtBlind.Text.Trim();
                string emailTitle = txtTitle.Text.Trim();

                CustomUriProvider customUriProvider = new CustomUriProvider(inlineImageCache);
                string htmlText = richEditControl.Document.GetHtmlText(richEditControl.Document.Range, customUriProvider);

                if (string.IsNullOrWhiteSpace(receiver))
                {
                    Cursor = Cursors.Default;
                    txtReceiver.Focus();
                    MessageBox.Show("收件人不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(emailTitle))
                {
                    Cursor = Cursors.Default;
                    txtTitle.Focus();
                    MessageBox.Show("邮件标题不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(htmlText))
                {
                    Cursor = Cursors.Default;
                    richEditControl.Focus();
                    MessageBox.Show("邮件内容不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (richEditControl.Text.Length > AppSettingHelper.DefaultMailCharactersSize)
                {
                    Cursor = Cursors.Default;
                    richEditControl.Focus();
                    MessageBox.Show(string.Format("邮件内容不能超过{0}个字符。", AppSettingHelper.DefaultMailCharactersSize), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (richEditControl.Document.Images.Count > AppSettingHelper.DefaultMailPictureCount)
                {
                    Cursor = Cursors.Default;
                    richEditControl.Focus();
                    MessageBox.Show(string.Format("邮件中图片数量不能超过{0}个。", AppSettingHelper.DefaultMailPictureCount), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<decimal, MailDeliveryMode> userInfos = new Dictionary<decimal, MailDeliveryMode>();
                IList<decimal> roleInfos = new List<decimal>();

                if (!string.IsNullOrWhiteSpace(receiver))
                {
                    receiver = FormatReceiver(receiver);
                    if (receiver.EndsWith(";"))
                    {
                        receiver = receiver.Remove(receiver.Length - 1, 1);
                    }
                    string[] receivedNames = receiver.Split(';');
                    if (receivedNames.Length > 10)
                    {
                        Cursor = Cursors.Default;
                        txtReceiver.Focus();
                        MessageBox.Show("收件人中对象不能超过10个。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    /* 发件人才群发（包含角色） */
                    foreach (string receivedName in receivedNames)
                    {
                        string name = receivedName.Trim();
                        int userIndex = name.IndexOf('@');
                        if (userIndex > 0)
                        {
                            string userName = name.Substring(0, userIndex);
                            decimal userId = decimal.MinValue;
                            if (userInfoCache.ContainsKey(userName))
                            {
                                userId = userInfoCache[userName].UserId;
                            }
                            else
                            {
                                userId = userAccountContract.GetUserIdByUserName(userName);
                            }
                            if (userId > 0 && !userInfos.ContainsKey(userId))
                            {
                                userInfos.Add(userId, MailDeliveryMode.Delivery);
                            }
                            else
                            {
                                Cursor = Cursors.Default;
                                txtReceiver.Focus();
                                MessageBox.Show(string.Format("收件人中名称 {0} 不存在，请检查发送的用户名。", userName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            /* 先从缓存中查询，然后从数据库中查询 */
                            IList<CommonNode> roles = btnRoles.Tag as IList<CommonNode>;
                            bool exist = false;
                            if (roles != null)
                            {
                                foreach (CommonNode commonNode in roles)
                                {
                                    if (commonNode.NodeName.Equals(name))
                                    {
                                        exist = true;
                                        roleInfos.Add(commonNode.NodeId);
                                        break;
                                    }
                                }
                            }
                            if (!exist)
                            {
                                decimal roleId = customRoleContract.GetRoleIdByRoleName(name);
                                if (roleId > 0)
                                {
                                    roleInfos.Add(roleId);
                                    exist = true;
                                }
                            }
                            if (!exist)
                            {
                                Cursor = Cursors.Default;
                                txtReceiver.Focus();
                                MessageBox.Show(string.Format("收件人中名称 {0} 不存在，请检查发送的角色名称。", name), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
                string information = string.Empty;
                bool validatedResult = ValidateUserNames(copy, userInfos, "抄送", ref information);
                if (!validatedResult)
                {
                    Cursor = Cursors.Default;
                    txtCopy.Focus();
                    MessageBox.Show(information, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                validatedResult = ValidateUserNames(blind, userInfos, "密送", ref information);
                if (!validatedResult)
                {
                    Cursor = Cursors.Default;
                    txtBlind.Focus();
                    MessageBox.Show(information, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasAttachment = (chkAttachment.Items.Count > 0) ? true : false;
                MailPriority mailPriority = MailPriority.Normal;
                if (chkCritical.Checked)
                {
                    mailPriority = MailPriority.Critical;
                }
                PrivateMailInfo privateMailInfo = new PrivateMailInfo(MailId, CurrentUser.Instance.UserId, emailTitle, htmlText, (byte)mailPriority,
                    hasAttachment, DateTime.Now, isDraft, receiver, copy, blind, false);
                string verifyResult = string.Empty;
                bool result = ValidationHelper.Validate<PrivateMailInfo>(privateMailInfo, out verifyResult);
                if (result)
                {
                    foreach (DocumentImage documentImage in richEditControl.Document.Images)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            byte[] bytes = documentImage.Image.GetImageBytes(documentImage.Image.RawFormat);
                            if (bytes.Length > 0)
                            {
                                if (bytes.Length > AppSettingHelper.DefaultMailPictureSize)
                                {
                                    richEditControl.Focus();
                                    MessageBox.Show(string.Format("邮件中单张图片大小不能超过{0}MB。", AppSettingHelper.DefaultMailPictureSize / (1024 * 1024)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }                                
                                string fileName = Path.GetFileName(documentImage.Uri);
                                if (privateMailInfo.MailId > 0 && inlineImageCache.ContainsKey(fileName))
                                {
                                    /* 图片文件已存在，例如含有图片的邮件在草稿箱中再次保存时，这类图片保存不变 */
                                    upLoadFileInfos.Add(new ExtendedUpLoadFileInfo(inlineImageCache[fileName], fileName, bytes, AttachmentType.InLine));
                                }
                                else
                                {
                                    if (customUriProvider.Relation.ContainsKey(documentImage.Uri))
                                    {
                                        /* 编辑草稿箱中的邮件时，新增加的图片 */
                                        upLoadFileInfos.Add(new ExtendedUpLoadFileInfo(customUriProvider.Relation[documentImage.Uri], bytes, AttachmentType.InLine));
                                    }
                                }
                            }
                            else
                            {
                                richEditControl.Focus();
                                MessageBox.Show("邮件中单张图片大小为0。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    foreach (CheckedListBoxItem item in chkAttachment.Items)
                    {
                        ExtendedUpLoadFileInfo file = item.Value as ExtendedUpLoadFileInfo;
                        if (file != null)
                        {
                            upLoadFileInfos.Add(file);
                        }
                    }
                    /* 四种情况：新邮件发送，草稿箱邮件发送，保存为草稿箱，重新保存为草稿箱 */
                    if (isDraft)
                    {
                        if (IsTransferred)
                        {
                            privateMailInfo.MailId = 0;
                            privateMailContract.Insert(privateMailInfo, upLoadFileInfos);
                        }
                        else
                        {
                            if (privateMailInfo.MailId > 0)
                            {
                                privateMailContract.Update(privateMailInfo, upLoadFileInfos);
                            }
                            else
                            {
                                privateMailContract.Insert(privateMailInfo, upLoadFileInfos);
                            }
                        }
                        RefreshData?.Invoke();
                        MessageBox.Show("邮件已保存到草稿箱。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (IsTransferred)
                        {
                            privateMailInfo.MailId = 0;
                            privateMailContract.Insert(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);
                        }
                        else
                        {
                            if (privateMailInfo.MailId > 0)
                            {
                                privateMailContract.Update(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);
                            }
                            else
                            {
                                privateMailContract.Insert(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);
                            }
                        }
                        RefreshData?.Invoke();
                        MessageBox.Show("邮件发送成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }          
                    this.Close();
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
        /// 校验邮箱收件人相关信息
        /// </summary>
        /// <param name="userNames"></param>
        /// <param name="userInfos"></param>
        /// <param name="caption"></param>
        /// <param name="information"></param>
        /// <returns></returns>
        private bool ValidateUserNames(string userNames, Dictionary<decimal, MailDeliveryMode> userInfos, string caption, ref string information)
        {
            information = string.Empty;
            userNames = FormatReceiver(userNames);
            if (userNames.EndsWith(";"))
            {
                userNames = userNames.Remove(userNames.Length - 1, 1);
            }
            string[] receivedNames = userNames.Split(';');
            if (receivedNames.Length > 5)
            {
                information = string.Format("{0}中对象不能超过5个。", caption);
                return false;
            }
            foreach (string receivedName in receivedNames)
            {
                string name = receivedName.Trim();
                int userIndex = name.IndexOf('@');
                if (userIndex > 0)
                {
                    string userName = name.Substring(0, userIndex);
                    decimal userId = decimal.MinValue;
                    if (userInfoCache.ContainsKey(userName))
                    {
                        userId = userInfoCache[userName].UserId;
                    }
                    else
                    {
                        userId = userAccountContract.GetUserIdByUserName(userName);
                    }
                    if (userId > 0)
                    {
                        if (!userInfos.ContainsKey(userId))
                        {
                            userInfos.Add(userId, MailDeliveryMode.Delivery);
                        }
                        else
                        {
                            information = string.Format("{0}中名称 {1} 在收件人中已存在，请在{0}的移除该用户名。", caption, userName);
                        }           
                    }
                    else
                    {
                        information = string.Format("{0}中名称 {1} 不存在，请检查{0}的用户名。", caption, userName);
                        return false;
                    }
                }
            }

            return true;
        }
        
        /// <summary>
        /// 加载邮件详细数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                if (MailId > 0)
                {                    
                    inlineImageCache.Clear();
                    PrivateMailInfo privateMailInfo = privateMailContract.GetModelInfo(MailId);
                    if (privateMailInfo != null)
                    {
                        if (IsTransferred)
                        {
                            txtReceiver.Text = string.Empty;
                            txtCopy.Text = string.Empty;
                            txtBlind.Text = string.Empty;
                            txtTitle.Text = string.Format("Fwd: {0}", privateMailInfo.MailTitle);
                        }
                        else
                        {
                            txtReceiver.Text = privateMailInfo.Receiver;
                            txtCopy.Text = privateMailInfo.Copyer;
                            txtBlind.Text = privateMailInfo.Blind;
                            txtTitle.Text = privateMailInfo.MailTitle;
                        }
                        MailPriority mailPriority = (MailPriority)privateMailInfo.MailPriority;
                        if (mailPriority == MailPriority.Critical)
                        {
                            chkCritical.Checked = true;
                        }
                        IList<PriavteAttachmentInfo> priavteAttachmentInfos = priavteAttachmentContract.GetModelInfos(privateMailInfo.MailId, (byte)AttachmentCategory.Mail);
                        foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                        {
                            AttachmentType attachmentType = (AttachmentType)priavteAttachmentInfo.AttachmentType;
                            switch (attachmentType)
                            {
                                case AttachmentType.Attachment:
                                    string filePath = string.Format("{0}{1}", priavteAttachmentInfo.AttachmentPath, priavteAttachmentInfo.AttachmentName);
                                    ExtendedUpLoadFileInfo upLoadFileInfo = new ExtendedUpLoadFileInfo(priavteAttachmentInfo.AttachmentName, priavteAttachmentInfo.AttachmentSourceName,
                                        null, filePath, AttachmentType.Attachment);
                                    chkAttachment.Items.Add(upLoadFileInfo);
                                    break;

                                case AttachmentType.InLine:                                    
                                    if (!inlineImageCache.ContainsKey(priavteAttachmentInfo.AttachmentSourceName))
                                    {
                                        inlineImageCache.Add(priavteAttachmentInfo.AttachmentSourceName, priavteAttachmentInfo.AttachmentName);
                                    }
                                    byte[] data = priavteAttachmentContract.GetAttachmentData(priavteAttachmentInfo.AttachmentId, priavteAttachmentInfo.AttachmentCategory, priavteAttachmentInfo.Sorting);
                                    string directory = Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles);
                                    if (!Directory.Exists(directory))
                                    {
                                        Directory.CreateDirectory(directory);
                                    }
                                    string fullPath = Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles, priavteAttachmentInfo.AttachmentSourceName);
                                    if (!File.Exists(fullPath) && data != null)
                                    {
                                        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
                                        {
                                            fileStream.Write(data, 0, data.Length);
                                            fileStream.Close();
                                        }
                                    }
                                    break;
                            }
                        }
                        richEditControl.HtmlText = privateMailInfo.MailContent;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region #customuriprovider

        public class CustomUriProvider : IUriProvider
        {
            private Dictionary<string, string> _relation;

            public Dictionary<string, string> Relation
            {
                get
                {
                    return _relation;
                }
            }

            private Dictionary<string, string> inlineImages;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="sourceNames"></param>
            public CustomUriProvider(Dictionary<string, string> sourceNames)
            {
                _relation = new Dictionary<string, string>();
                inlineImages = sourceNames;
            }

            #region IUriProvider Members

            public string CreateCssUri(string rootUri, string styleText, string relativeUri)
            {
                return String.Empty;
            }

            public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
            {
                if (_relation.ContainsKey(image.Uri))
                {
                    _relation.Remove(image.Uri);
                }
                string fileName = string.Empty;
                string sourceFileName = Path.GetFileName(image.Uri);
               
                if (inlineImages.ContainsKey(sourceFileName))
                {
                    /* 草稿箱中邮件中已经存在的图片不需要缓存对应关系 */
                    fileName = sourceFileName;
                }
                else
                { 
                    fileName = fileName = string.Format("{0}{1}{2}", CurrentUser.Instance.UserName, DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(image.Uri));
                    _relation.Add(image.Uri, fileName);
                }
                                                
                return Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles, fileName);
            }

            #endregion
        }

        #endregion #customuriprovider
        
    }
}
