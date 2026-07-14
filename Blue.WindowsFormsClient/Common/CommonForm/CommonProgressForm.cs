using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blue.WindowsFormsClient
{
    public partial class CommonProgressForm : Form
    {
        #region 属性

        /// <summary>
        /// 是否程序关闭
        /// </summary>
        public bool FormalClosed
        {
            get;set;
        }

        /// <summary>
        /// 取消测试操作
        /// </summary>
        public MethodInvoker CancelToTest
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonProgressForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件方法

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            FormalClosed = false;
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormalClosed = false;
            this.Close();
        }

        /// <summary>
        /// 关闭后调用中断服务操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!FormalClosed && CancelToTest != null)
            {
                CancelToTest();
            }
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!FormalClosed && MessageBox.Show("确定取消吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        #endregion
    }
}
