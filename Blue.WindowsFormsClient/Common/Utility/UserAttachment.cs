using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    public class UserAttachment
    {
        #region 私有常量

        /// <summary>
        /// 每个附件的行高
        /// </summary>
        private const int ROW_HEIGHT_EACH_ATTACHMENT = 22;

        #endregion

        #region 私有变量

        private readonly PanelControl pnlAttachment;
        private readonly IPriavteAttachmentContract priavteAttachmentContract;
        private readonly SaveFileDialog saveFileDialog;

        #endregion

        #region 属性

        public decimal AttachmentId
        {
            get;
            set;
        }

        public AttachmentCategory AttachmentCategory
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="priavteAttachmentContract"></param>
        /// <param name="pnlAttachment"></param>
        /// <param name="saveFileDialog"></param>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        public UserAttachment(IPriavteAttachmentContract priavteAttachmentContract, PanelControl pnlAttachment, SaveFileDialog saveFileDialog,
            decimal attachmentId, AttachmentCategory attachmentCategory)
        {
            this.priavteAttachmentContract = priavteAttachmentContract;
            this.pnlAttachment = pnlAttachment;
            this.saveFileDialog = saveFileDialog;
            AttachmentId = attachmentId;
            AttachmentCategory = attachmentCategory;
        }

        #endregion


        #region 属性

        /// <summary>
        /// 加载附件数据
        /// </summary>
        /// <param name="visibleByZero"></param>
        /// <param name="caption"></param>
        public void LoadAttachements(bool visibleByZero, string caption)
        {
            pnlAttachment.Controls.Clear();
            LabelControl lblAttachmentTip = new LabelControl
            {
                Location = new Point(5, 5)
            };
            pnlAttachment.Controls.Add(lblAttachmentTip);
            IList<PriavteAttachmentInfo> priavteAttachmentInfos = priavteAttachmentContract.GetModelInfos(AttachmentId, (byte)AttachmentCategory);
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
                lblAttachmentTip.Text = string.Format("该{0}共有{1}个附件：", caption, index);
            }
            else
            {
                lblAttachmentTip.Text = string.Format("该{0}没有附件。", caption);
                pnlAttachment.Visible = visibleByZero;
            }
            pnlAttachment.Height = (index + 1) * ROW_HEIGHT_EACH_ATTACHMENT + 10;
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
