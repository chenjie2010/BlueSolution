using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.GeneralAffairModule;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 查看邮件
    /// </summary>
    public partial class MailViewForm : Form
    {
        #region 属性

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get { return mailControl.UserAccountContract; }
            set { mailControl.UserAccountContract = value; }
        }

        /// <summary>
        /// 邮件契约
        /// </summary>
        public IPrivateMailContract PrivateMailContract
        {
            get { return mailControl.PrivateMailContract; }
            set { mailControl.PrivateMailContract = value; }
        }

        /// <summary>
        /// 附件契约
        /// </summary>
        public IPriavteAttachmentContract PriavteAttachmentContract
        {
            get { return mailControl.PriavteAttachmentContract; }
            set { mailControl.PriavteAttachmentContract = value; }
        }

        /// <summary>
        /// 邮件编号
        /// </summary>
        public decimal MailId
        {
            get;set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MailViewForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件的方法

        private void MailViewForm_Load(object sender, EventArgs e)
        {
            if (MailId > 0)
            {
                mailControl.LoadData(MailId);
            }
        }

        #endregion
    }
}
