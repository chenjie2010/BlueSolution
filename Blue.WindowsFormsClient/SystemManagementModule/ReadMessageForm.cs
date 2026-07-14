using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class ReadMessageForm : Form
    {
        #region 常量

        /// <summary>
        /// 每个附件的行高
        /// </summary>
        private const int ROW_HEIGHT_EACH_ATTACHMENT = 22;

        /// <summary>
        /// 离上边缘距离
        /// </summary>
        private const int MARGIN_TOP = 30;

        #endregion

        #region 契约接口

        private readonly IPriavteAttachmentContract priavteAttachmentContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 属性

        /// <summary>
        /// 消息对象
        /// </summary>
        public UserMessageInfo UserMessageInfo
        {
            get;
            set;
        }

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReadMessageForm()
        {
            InitializeComponent();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadMessageForm_Load(object sender, EventArgs e)
        {
            if (UserMessageInfo != null)
            {
                CommonUserInfo commonUserInfo =  userAccountContract.GetCommonUserInfo(UserMessageInfo.UserId);
                txtSender.Text = string.Format("{0}({1},{2})", commonUserInfo.UserActualName, commonUserInfo.UserName, commonUserInfo.DepName);
                txtTitle.Text = UserMessageInfo.MessageTitle;
                txtSendTime.Text = UserMessageInfo.DeliveredTime.ToString("F");
                richEditControl.HtmlText = UserMessageInfo.MessageContent;
                MessageType messageType = (MessageType)UserMessageInfo.MessageType;
                AttachmentCategory attachmentCategory = AttachmentCategory.Notice;
                if (messageType == MessageType.SystemMessage)
                {
                    attachmentCategory = AttachmentCategory.Message;
                }
                gpAttachment.Controls.Clear();
                LabelControl lblAttachmentTip = new LabelControl
                {
                    Location = new Point(5, MARGIN_TOP)
                };
                gpAttachment.Controls.Add(lblAttachmentTip);
                IList < PriavteAttachmentInfo > priavteAttachmentInfos = priavteAttachmentContract.GetModelInfos(UserMessageInfo.MessageId, (byte)attachmentCategory);
                int index = 0;
                foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                {
                    AttachmentType attachmentType = (AttachmentType)priavteAttachmentInfo.AttachmentType;
                    switch (attachmentType)
                    {
                        case AttachmentType.Attachment:
                            ExtendedUpLoadFileInfo upLoadFileInfo = new ExtendedUpLoadFileInfo(priavteAttachmentInfo.AttachmentName, priavteAttachmentInfo.AttachmentSourceName,
                                null, AttachmentType.Attachment);
                            LinkLabel linkLabel = new LinkLabel
                            {
                                Text = string.Format("{0}({1:F}KB)", priavteAttachmentInfo.AttachmentSourceName, priavteAttachmentInfo.AttachmenSize / 1024.0),
                                Tag = priavteAttachmentInfo,
                                Width = gpAttachment.Width - 10,
                                Location = new Point(5, MARGIN_TOP + (index + 1) * ROW_HEIGHT_EACH_ATTACHMENT)
                            };
                            index++;
                            linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_LinkClicked);
                            gpAttachment.Controls.Add(linkLabel);
                            break;

                        case AttachmentType.InLine:
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
                if (index > 0)
                {
                    lblAttachmentTip.Text = string.Format("该消息共有{0}个附件：", index);
                }
                else
                {
                    lblAttachmentTip.Text = "该消息没有附件。";
                }
                gpAttachment.Height = (index + 1) * ROW_HEIGHT_EACH_ATTACHMENT + 40;
            }
        }

        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PriavteAttachmentInfo messageAttachmentInfo = ((LinkLabel)sender).Tag as PriavteAttachmentInfo;
            if (messageAttachmentInfo != null)
            {
                byte[] data = priavteAttachmentContract.GetAttachmentData(messageAttachmentInfo.AttachmentId, messageAttachmentInfo.AttachmentCategory, messageAttachmentInfo.Sorting);
                if (data != null)
                {
                    if (string.IsNullOrWhiteSpace(saveFileDialog.InitialDirectory))
                    {
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName) && Directory.Exists(Path.GetDirectoryName(saveFileDialog.FileName)))
                        {
                            saveFileDialog.InitialDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                        }
                    }
                    saveFileDialog.FileName = messageAttachmentInfo.AttachmentSourceName;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fileStream.Write(data, 0, data.Length);
                            fileStream.Close();
                        }
                    }
                }
            }

        }

        #endregion
    }
}
