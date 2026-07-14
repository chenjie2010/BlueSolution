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
    public partial class UnhandledExceptionForm : Form
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionInfo
        {
            set
            {
                meException.Text = value;
            }
        }

        /// <summary>
        /// 未处理异常界面初始化
        /// </summary>
        public UnhandledExceptionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 退出异常提示界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnhandledExceptionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
