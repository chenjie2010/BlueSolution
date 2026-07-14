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
using Blue.WCFContracts.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class TextForm : Form
    {
        #region 契约接口

        private readonly IPriavteAttachmentContract priavteAttachmentContract;

        #endregion

        #region 私有变量

        private Dictionary<string, string> inlineImageCache;

        #endregion

        #region 成员变量

        private AttachmentCategory _attachmentCategory;

        #endregion

        #region 属性

        /// <summary>
        /// 文本编号
        /// </summary>
        public decimal TextId
        {
            get; set;
        }

        /// <summary>
        /// 附件分类
        /// </summary>
        public AttachmentCategory AttachmentCategory
        {
            get
            {
               return  _attachmentCategory;
            }
            set
            {
                if (value == AttachmentCategory.None)
                {
                    pnlAttachment.Visible = false;
                }
                else
                {
                    pnlAttachment.Visible = true;
                }
                _attachmentCategory = value;
            }
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string HtmlText
        {
            get { return richEditControl.HtmlText.Trim(); }
            set { richEditControl.HtmlText = value; }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        public GetRichTextAndAttachmentsDelegate GetRichTextAndAttachments
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TextForm()
        {
            InitializeComponent();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
            TextId = 0;
            inlineImageCache = new Dictionary<string, string>();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextForm_Load(object sender, EventArgs e)
        {
            this.changeFontNameItem1.EditValue = "新宋体";
            this.changeFontSizeItem1.EditValue = 10;
            if (_attachmentCategory != AttachmentCategory.None)
            {
                LoadAttachments();
            }
        }

        /// <summary>
        /// 菜单设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richEditControl_PopupMenuShowing(object sender, DevExpress.XtraRichEdit.PopupMenuShowingEventArgs e)
        {
            for (int i = 12; i <= 20; i++)
            {
                e.Menu.Items[i].Visible = false;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = new List<ExtendedUpLoadFileInfo>();
            CustomUriProvider customUriProvider = new CustomUriProvider(inlineImageCache);
            string htmlText = richEditControl.Document.GetHtmlText(richEditControl.Document.Range, customUriProvider);

            if (richEditControl.Text.Length > AppSettingHelper.DefaultDocCharactersSize)
            {
                Cursor = Cursors.Default;
                richEditControl.Focus();
                MessageBox.Show(string.Format("文本内容不能超过{0}个字符。", AppSettingHelper.DefaultDocCharactersSize), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (richEditControl.Document.Images.Count > AppSettingHelper.DefaultDocPictureCount)
            {
                Cursor = Cursors.Default;
                richEditControl.Focus();
                MessageBox.Show(string.Format("文本中图片数量不能超过{0}个。", AppSettingHelper.DefaultDocPictureCount), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
                            MessageBox.Show(string.Format("文本中单张图片大小不能超过{0}MB。", AppSettingHelper.DefaultDocPictureSize / (1024 * 1024)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        string fileName = Path.GetFileName(documentImage.Uri);
                        string newFileName = string.Empty;
                        if (inlineImageCache.ContainsKey(fileName))
                        {                       
                            /* 图片文件已存在，例如含有图片的文本再次保存时，这类图片保存不变 */
                            upLoadFileInfos.Add(new ExtendedUpLoadFileInfo(inlineImageCache[fileName], fileName, bytes, AttachmentType.InLine));                            
                        }
                        else
                        {
                            if (customUriProvider.Relation.ContainsKey(documentImage.Uri))
                            {
                                /* 编辑草稿箱中的邮件时，新增加的图片 */
                                upLoadFileInfos.Add(new ExtendedUpLoadFileInfo(customUriProvider.Relation[documentImage.Uri], bytes, AttachmentType.InLine));
                                CreateLocalImage(bytes, customUriProvider.Relation[documentImage.Uri]);
                            }
                        }                        
                    }
                    else
                    {
                        richEditControl.Focus();
                        MessageBox.Show("文本中单张图片大小为0。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            GetRichTextAndAttachments?.Invoke(htmlText, upLoadFileInfos);
            this.Close();
        }

        /// <summary>
        /// 字数统计：不能超过指定的长度。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richEditControl_TextChanged(object sender, EventArgs e)
        {
            bsiCharactersCount.Caption = string.Format("当前字符共{0}个，内容不能超过{1}个字符。", richEditControl.Text.Length, AppSettingHelper.DefaultDocCharactersSize);
        }              

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 移除附件
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载附件
        /// </summary>
        private void LoadAttachments()
        {
            try
            {
                IList<PriavteAttachmentInfo> priavteAttachmentInfos = priavteAttachmentContract.GetModelInfos(TextId, (byte)AttachmentCategory);
                foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                {
                    AttachmentType attachmentType = (AttachmentType)priavteAttachmentInfo.AttachmentType;
                    switch (attachmentType)
                    {
                        case AttachmentType.Attachment:
                            ExtendedUpLoadFileInfo upLoadFileInfo = new ExtendedUpLoadFileInfo(priavteAttachmentInfo.AttachmentName, priavteAttachmentInfo.AttachmentSourceName,
                                null, AttachmentType.Attachment);
                            chkAttachment.Items.Add(upLoadFileInfo);
                            break;

                        case AttachmentType.InLine:
                            if (!inlineImageCache.ContainsKey(priavteAttachmentInfo.AttachmentSourceName))
                            {
                                inlineImageCache.Add(priavteAttachmentInfo.AttachmentSourceName, priavteAttachmentInfo.AttachmentName);
                            }
                            byte[] data = priavteAttachmentContract.GetAttachmentData(priavteAttachmentInfo.AttachmentId, priavteAttachmentInfo.AttachmentCategory, priavteAttachmentInfo.Sorting);
                            CreateLocalImage(data, priavteAttachmentInfo.AttachmentSourceName);
                            break;
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

        /// <summary>
        /// 创建本地图片
        /// </summary>
        /// <param name="data"></param>
        /// <param name="attachmentSourceName"></param>
        private void CreateLocalImage(byte[] data, string attachmentSourceName)
        {
            string directory = Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string fullPath = Path.Combine(Application.StartupPath, AppSettingHelper.DefaultSubDirOfSavedMessageFiles, attachmentSourceName);
            if (!File.Exists(fullPath) && data != null)
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                }
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
