using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.LookAndFeel.Design;
using DevExpress.Utils;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    public partial class DevExpressUpdatedUploadFile : UserControl
    {
        #region 静态私有变量

        /// <summary>
        /// 文件路径
        /// </summary>
        private static string filePathSelected = string.Empty;

        /// <summary>
        /// PDF阅读器
        /// </summary>
        private static PDFViewerForm frmPDFViewer = new PDFViewerForm();

        #endregion

        #region 私有变量

        /// <summary>
        /// 文本改变是否生效
        /// </summary>
        private bool textChanged = true;

        #endregion

        #region 事件

        #region 定义"浏览"事件

        private event EventHandler<EventArgs> _OnBrowseClick;

        /// <summary>
        /// 定义"浏览"事件访问器
        /// </summary>
        [
        Description("浏览"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnBrowseClick
        {
            add
            {
                _OnBrowseClick += value;
            }
            remove
            {
                _OnBrowseClick -= value;
            }
        }

        /// <summary>
        /// 定义"浏览"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BrowseClick(EventArgs e)
        {
            if (_OnBrowseClick != null) _OnBrowseClick(this, e);
        }

        #endregion       

        #region 定义"文本内容改变"事件

        private event EventHandler<EventArgs> _OnTextChanged;

        /// <summary>
        /// 定义"文本内容改变"事件访问器
        /// </summary>
        [
        Description("文本内容改变"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public new event EventHandler<EventArgs> OnTextChanged
        {
            add
            {
                _OnTextChanged += value;
            }
            remove
            {
                _OnTextChanged -= value;
            }
        }

        /// <summary>
        /// 定义"文本内容改变"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private new void TextChanged(EventArgs e)
        {
            if (_OnTextChanged != null) _OnTextChanged(this, e);
        }

        #endregion

        #region 定义"鼠标单击"事件

        private event EventHandler<ActionEventArgs> _OnViewLinkClicked;

        /// <summary>
        /// 定义"鼠标单击"事件访问器
        /// </summary>
        [
        Description("鼠标单击"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<ActionEventArgs> OnViewLinkClicked
        {
            add
            {
                _OnViewLinkClicked += value;
            }
            remove
            {
                _OnViewLinkClicked -= value;
            }
        }

        /// <summary>
        /// 定义"鼠标单击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ViewLinkClicked(ActionEventArgs e)
        {
            if (_OnViewLinkClicked != null) _OnViewLinkClicked(this, e);
        }

        #endregion

        #endregion        

        #region 内部成员变量

        private DocType _docType = DocType.ArbitraryAttachment;
        private byte[] _customData = null;

        #endregion

        #region 属性

        [DefaultValue("DevExpress Style")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("获得或是设置皮肤样式")]
        [TypeConverter(typeof(SkinNameTypeConverter))]
        public string SkinName
        {
            set
            {
                textEdit.Properties.LookAndFeel.SkinName = value;
                sbtnBrowse.LookAndFeel.SkinName = value;
            }
            get
            {
                return textEdit.Properties.LookAndFeel.SkinName;
            }
        }

        ///// <summary>
        ///// 是否显示查看按钮
        ///// </summary>
        //[Description("是否显示查看按钮"),
        //Category("自定义杂项"),
        //DefaultValue(true)
        //]
        //public bool ShowView
        //{
        //    get
        //    {
        //        return hlnkView.Visible;
        //    }
        //    set
        //    {
        //        hlnkView.Visible = value;
        //    }
        //}

        /// <summary>
        /// 提示
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public string ToolTipTitle
        {
            get
            {
                return textEdit.ToolTipTitle;
            }
            set
            {
                textEdit.ToolTipTitle = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [
        Description("文档类型"),
        Category("自定义杂项"),
        DefaultValue("*|*"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public string Filter
        {
            set
            {
                openFileDialog.Filter = value;
                saveFileDialog.Filter = value;
            }
            get
            {
                return openFileDialog.Filter;
            }
        }

        [
        Description("文档路径"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public string FileName
        {
            get
            {
                return textEdit.Text; ;
            }
            set
            {
                textEdit.Text = value;
            }
        }

        [
        Description("文档名称"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public string TextContent
        {
            get
            {
                return textEdit.Text; ;
            }
            set
            {
                textChanged = false;
                textEdit.Text = value;
                textChanged = true;
            }
        }

        [
        Description("文档名称"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public override string Text
        {
            get
            {
                return textEdit.Text.Trim();
            }
        }

        [
        Description("是否是图片"),
        Category("自定义杂项")
        ]
        public DocType DocType
        {
            get
            {
                return _docType;
            }
            set
            {
                switch (value)
                {
                    case DocType.PicAttachment:
                        imageEdit.Visible = true;
                        lblTip.Text = "提示：查看、删除或是保存图片。";
                        break;

                    case DocType.PDFAttachment:
                        imageEdit.Visible = false;
                        lblTip.Text = "提示：查看、删除或是保存PDF文件。";
                        break;

                    case DocType.DocAttachment:
                        imageEdit.Visible = false;
                        lblTip.Text = "提示：查看、删除或是保存文件。";
                        break;
                }
                _docType = value;
            }
        }

        [Description("设置图像")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("自定义杂项")]
        [Browsable(false)]
        public Image Image
        {
            get
            {
                return imageEdit.Image;
            }
            set
            {
                imageEdit.Image = value;
            }
        }

        [Description("数据")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("自定义杂项")]
        [Browsable(false)]
        public byte[] CustomData
        {
            get
            {
                string fileName = textEdit.Text.Trim();
                if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        int len = (int)fs.Length;
                        _customData = new byte[len];
                        fs.Read(_customData, 0, _customData.Length);
                        fs.Close();
                        fs.Dispose();
                    }
                }

                return _customData;
            }
            set
            {
                _customData = value;
                if (_docType == DocType.PicAttachment && _customData != null)
                {
                    using (MemoryStream ms = new MemoryStream(_customData))
                    {
                        imageEdit.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    if (_docType == DocType.PicAttachment)
                    {
                        imageEdit.Image = null;
                    }
                }
            }
        }

        [Description("获得或是设置图像的高度")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("自定义杂项")]
        public int ImageWidth
        {
            get
            {
                return imageEdit.Width;
            }
            set
            {
                imageEdit.Width = value;
            }
        }

        [Description("获得或是设置图像的宽度")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("自定义杂项")]
        public int ImageHeight
        {
            get
            {
                return imageEdit.Height;
            }
            set
            {
                imageEdit.Height = value; ;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        [
        Description("是否只读"),
        Category("自定义杂项"),
        DefaultValue(true),
        ]
        public bool ReadOnly
        {
            set
            {
                imageEdit.ReadOnly = value;
                sbtnBrowse.Enabled = !value;

            }
            get
            {
                return imageEdit.ReadOnly;
            }
        }

        [
       Description("自定义数据"),
       Category("自定义杂项"),
       DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
       Browsable(false),
       ]
        public Object UserData
        {
            get
            {
                return textEdit.Tag;
            }
            set
            {
                textEdit.Tag = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DevExpressUpdatedUploadFile()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体加载方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressUploadFile_Load(object sender, EventArgs e)
        {
            imageEdit.Width = this.Width - 33;
            textEdit.Width = this.Width - 33;
            sbtnBrowse.Location = new Point(textEdit.Width + 5, 0);
        }

        private void sbtnBrowse_Click(object sender, EventArgs e)
        {
            if (_docType == DocType.PicAttachment)
            {
                imageEdit.ClosePopup();
                imageEdit.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(filePathSelected))
            {
                if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
                {
                    openFileDialog.InitialDirectory = Application.StartupPath;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(openFileDialog.FileName) && Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                    {
                        openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    }
                }
            }
            else
            {
                if (Directory.Exists(filePathSelected))
                {
                    openFileDialog.InitialDirectory = filePathSelected;
                }
                else
                {
                    openFileDialog.InitialDirectory = Application.StartupPath;
                }
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePathSelected = Path.GetDirectoryName(openFileDialog.FileName);
                switch (_docType)
                {
                    case DocType.PicAttachment:
                        if (FileFormatHelper.VerfiyImageFormat(openFileDialog.FileName))
                        {
                            imageEdit.Image = Image.FromFile(openFileDialog.FileName);
                            textEdit.Text = openFileDialog.FileName;
                        }
                        else
                        {
                            textEdit.Text = string.Empty;
                        }
                        break;

                    case DocType.DocAttachment:
                    case DocType.ArbitraryAttachment:
                        textEdit.Text = openFileDialog.FileName;
                        break;

                    case DocType.PDFAttachment:
                        if (FileFormatHelper.VerfiyPDFFormat(openFileDialog.FileName))
                        {
                            textEdit.Text = openFileDialog.FileName;
                        }
                        else
                        {
                            textEdit.Text = string.Empty;
                        }
                        break;
                }
                BrowseClick(e);
            }
        }

        /// <summary>
        /// 文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEdit_TextChanged(object sender, EventArgs e)
        {
            if (textChanged)
            {
                TextChanged(e);
            }
        }

        /// <summary>
        /// 单击文件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 图片关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (_docType == DocType.PicAttachment)
            {
                imageEdit.Visible = false;
            }
        }


        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkView_Click(object sender, EventArgs e)
        {
            UserHandler(UserAction.View);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkSave_Click(object sender, EventArgs e)
        {
            UserHandler(UserAction.Save);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDelete_Click(object sender, EventArgs e)
        {
            UserHandler(UserAction.Delete);
        }


        #endregion

        #region 私有方法


        #endregion

        /// <summary>
        /// 用户操作
        /// </summary>
        /// <param name="userAction"></param>
        private void UserHandler(UserAction userAction)
        {
            ActionEventArgs actionEventArgs = new ActionEventArgs()
            {
                UserAction = userAction
            };            
            switch (userAction)
            {
                case UserAction.View:
                    Cursor = Cursors.WaitCursor;
                    ViewLinkClicked(actionEventArgs);
                    switch (_docType)
                    {
                        case DocType.PicAttachment:
                            imageEdit.Visible = true;
                            imageEdit.ShowPopup();
                            break;

                        case DocType.PDFAttachment:
                            if (_customData != null && _customData.Length > 0)
                            {                                
                                using (MemoryStream ms = new MemoryStream(_customData))
                                {
                                    frmPDFViewer.LoadDocument(ms);
                                    frmPDFViewer.ShowDialog();
                                }
                            }
                            break;
                    }
                    Cursor = Cursors.Default;
                    break;

                case UserAction.Save:
                    ViewLinkClicked(actionEventArgs);
                    saveFileDialog.FileName = textEdit.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName) && saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (_customData != null && _customData.Length > 0)
                        {
                            using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                fileStream.Write(_customData, 0, _customData.Length);
                                fileStream.Close();
                            }
                            MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("文件内容为空，无法保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    break;

                case UserAction.Delete:
                    if (MessageBox.Show(string.Format("确认删除该{0}？", UserEnumHelper.GetEnumText(_docType)), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        FileName = string.Empty;
                        _customData = null;
                        ViewLinkClicked(actionEventArgs);
                    }
                    break;
            }
        }
    }
}
