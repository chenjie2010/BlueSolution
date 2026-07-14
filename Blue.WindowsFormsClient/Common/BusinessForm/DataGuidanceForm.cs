using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using Blue.WCFContracts.GeneralAffairModule;
using Blue.Model.UserModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataGuidanceForm : Form
    {     
        #region 契约接口

        private readonly IPriavteAttachmentContract priavteAttachmentContract;

        #endregion       

        #region 属性

        /// <summary>
        /// 附件编号
        /// </summary>
        public decimal AttachmentId
        {
            get;
            set;
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public AttachmentCategory AttachmentCategory
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string HelpCaption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = string.Format("{0}_阅读指南", value);
            }
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 填报开始
        /// </summary>
        public DataSumbittedDelegate DataSumbitted
        {
            get;
            set;
        }

        /// <summary>
        /// 填报按钮是否可见
        /// </summary>
        public bool BottomVisible
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataGuidanceForm()
        {
            InitializeComponent();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
        }

        #endregion

        #region 窗体加载方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGuidanceForm_Load(object sender, EventArgs e)
        {
            pnlBottom.Visible = BottomVisible;
            UserAttachment userAttachment = new UserAttachment(priavteAttachmentContract, pnlAttachment, saveFileDialog, AttachmentId, AttachmentCategory);
            userAttachment.LoadAttachements(false, UserEnumHelper.GetEnumText(AttachmentCategory));
            richEditControl.HtmlText = Content;
        }

        /// <summary>
        /// 阅读指南
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRead_CheckedChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = chkRead.Checked;
        }

        /// <summary>
        /// 开始填报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            DataSumbitted?.Invoke(chkRead.Checked);
        }

        #endregion
    }
}
