using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blue.WindowsFormsClient;

namespace Blue.WindowsFormsClient.MyWorkModule
{
    public partial class ReviewedCommentForm : Form
    {
        #region 契约接口

        /// <summary>
        /// 获得审核意见
        /// </summary>
        public GetCommentDelegate GetComment
        {
            set;
            get;
        }

        /// <summary>
        /// 审核意见标签内容
        /// </summary>
        public string CommnetName
        {
            set
            {
                gcComment.Text = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReviewedCommentForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewedCommentForm_Load(object sender, EventArgs e)
        {

        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string comment = meComment.Text.Trim();
            GetComment?.Invoke(comment);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
