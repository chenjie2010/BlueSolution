using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.Model.GeneralAffairModule;


namespace Blue.WindowsFormsClient.Common.CommonControls
{
    public partial class MailControl : UserControl
    {
        /// <summary>
        /// 每个附件的行高
        /// </summary>
        private const int ROW_HEIGHT_EACH_ATTACHMENT = 22;

        #region 属性

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件契约
        /// </summary>
        public IPrivateMailContract PrivateMailContract
        {
            get;
            set;
        }

        /// <summary>
        /// 附件契约
        /// </summary>
        public IPriavteAttachmentContract PriavteAttachmentContract
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public MailControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailControl_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 清除控件上的数据
        /// </summary>
        public void ClearDataOnControls()
        {
            txtTitle.Text = string.Empty;
            txtSender.Text = string.Empty;
            txtReceiver.Text = string.Empty;
            txtCopy.Text = string.Empty;
            txtSendTime.Text = string.Empty;
            chkCritical.Checked = false;
            pnlAttachment.Controls.Clear();
            richEditControl.HtmlText = string.Empty;
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
                byte[] data = PriavteAttachmentContract.GetAttachmentData(messageAttachmentInfo.AttachmentId, messageAttachmentInfo.AttachmentCategory, messageAttachmentInfo.Sorting);
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

        /// <summary>
        /// 回复邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblReply_Click(object sender, EventArgs e)
        {
            MailDeliveryForm frmMailDelivery = new MailDeliveryForm();
            frmMailDelivery.Receiver = txtSender.Text.Trim();
            frmMailDelivery.MailTitle = string.Format("Re:{0}", txtTitle.Text.Trim());
            frmMailDelivery.Show();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载邮件详细数据
        /// </summary>
        /// <param name="mailId"></param>
        public void LoadData(decimal mailId)
        {
            try
            {
                if (mailId > 0)
                {
                    lblReply.Enabled = true;
                    PrivateMailInfo privateMailInfo = PrivateMailContract.GetModelInfo(mailId);
                    if (privateMailInfo != null)
                    {
                        UserAccountInfo userAccountInfo = UserAccountContract.GetModelInfo(privateMailInfo.UserId);
                        if (userAccountInfo != null)
                        {
                            txtSender.Text = string.Format("{0}@{1}", userAccountInfo.UserName, userAccountInfo.UserActualName);
                        }
                        else
                        {
                            txtSender.Text = "提示：该发件人用户信息不存在。";
                        }
                        txtReceiver.Text = privateMailInfo.Receiver;
                        txtCopy.Text = privateMailInfo.Copyer;
                        txtTitle.Text = privateMailInfo.MailTitle;
                        txtSendTime.Text = privateMailInfo.SendTime.ToString("F");
                        MailPriority mailPriority = (MailPriority)privateMailInfo.MailPriority;
                        if (mailPriority == MailPriority.Critical)
                        {
                            chkCritical.Checked = true;
                        }
                        pnlAttachment.Controls.Clear();
                        LabelControl lblAttachmentTip = new LabelControl
                        {
                            Location = new Point(5, 5)
                        };
                        pnlAttachment.Controls.Add(lblAttachmentTip);
                        IList<PriavteAttachmentInfo> priavteAttachmentInfos = PriavteAttachmentContract.GetModelInfos(privateMailInfo.MailId, (byte)AttachmentCategory.Mail);
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
                                        Width = pnlAttachment.Width - 10,
                                        Location = new Point(5, 5 + (index + 1) * ROW_HEIGHT_EACH_ATTACHMENT)
                                    };
                                    index++;
                                    linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_LinkClicked);
                                    pnlAttachment.Controls.Add(linkLabel);
                                    break;

                                case AttachmentType.InLine:
                                    byte[] data = PriavteAttachmentContract.GetAttachmentData(priavteAttachmentInfo.AttachmentId, priavteAttachmentInfo.AttachmentCategory, priavteAttachmentInfo.Sorting);
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
                            lblAttachmentTip.Text = string.Format("该邮件共有{0}个附件：", index);
                        }
                        else
                        {
                            lblAttachmentTip.Text = "该邮件没有附件。";
                        }
                        pnlAttachment.Height = (index + 1) * ROW_HEIGHT_EACH_ATTACHMENT + 10;
                        richEditControl.HtmlText = privateMailInfo.MailContent;
                    }
                    else
                    {
                        lblReply.Enabled = false;
                        ClearDataOnControls();
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
    }
}
