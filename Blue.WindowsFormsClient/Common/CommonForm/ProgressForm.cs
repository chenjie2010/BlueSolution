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
    public partial class ProgressForm : Form
    {
        #region  私有静态只读变量

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object locker = new object();

        #endregion

        #region 私有变量

        /// <summary>
        /// 窗体关闭确认
        /// </summary>
        private bool formConfirmed = false;

        #endregion

        #region 属性

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tip
        {
            get
            {
                return lblTip.Text.Trim();
            }
            set
            {
                lblTip.Text = value;
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get; set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue
        {
            get; set;
        }

        /// <summary>
        /// 取消标志位
        /// </summary>
        public bool Cancel
        {
            get; set;
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        public MethodInvoker TaskCancelled
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProgressForm()
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
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            formConfirmed = false;
            Cancel = false;
            progressBar.Properties.Maximum = MaxValue;
            progressBar.Properties.Minimum = MinValue;
            progressBar.Position = MinValue;
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (MessageBox.Show("确定取消吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Application.DoEvents();
                TaskCancelled?.Invoke();
                formConfirmed = true;
                Cancel = true;
                this.Close();
            }
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.DoEvents();
            if (!formConfirmed && MessageBox.Show("确定取消吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                Application.DoEvents();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!formConfirmed)
            {
                Application.DoEvents();
                TaskCancelled?.Invoke();
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 增加步长
        /// </summary>
        public void IncreaseStep()
        {
            lock (locker)
            {
                if (progressBar.Position < MaxValue)
                {
                    Application.DoEvents();
                    progressBar.Increment(1);                    
                }
            }            
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void RefreshProgressBar()
        {
            Application.DoEvents();
            progressBar.Refresh();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public void CloseFrom()
        {
            Application.DoEvents();
            formConfirmed = true;
            this.Close();
        }

        #endregion        
    }
}
