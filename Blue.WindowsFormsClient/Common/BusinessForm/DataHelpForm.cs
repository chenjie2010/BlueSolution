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
    public partial class DataHelpForm : Form
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
        /// 文本内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataHelpForm()
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
            UserAttachment userAttachment = new UserAttachment(priavteAttachmentContract, pnlAttachment, saveFileDialog, AttachmentId, AttachmentCategory);
            userAttachment.LoadAttachements(false, UserEnumHelper.GetEnumText(AttachmentCategory));
            richEditControl.HtmlText = Content;
        }

        #endregion
    }
}
