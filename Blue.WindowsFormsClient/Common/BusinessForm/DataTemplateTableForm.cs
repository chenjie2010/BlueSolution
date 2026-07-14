using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataTemplateTableForm : Form
    {
        #region 定义成员变量

        #endregion

        #region 属性

        /// <summary>
        /// 控件
        /// </summary>
        public XtraScrollableControl Panel
        {
            get
            {
                return xscPanel;
            }
        }

        /// <summary>
        /// 提示框
        /// </summary>
        public MemoEdit MemoEditToolTip
        {
            get
            {
                return  meToolTip;
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        public SumbittedHandlerDelegate SumbittedHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 当前窗体只读状态
        /// </summary>
        public bool FormReadOnly
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTemplateTableForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体加载与方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTemplateTableForm_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = !FormReadOnly;
        }
       
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!FormReadOnly)
                {
                    Cursor = Cursors.WaitCursor;
                    SumbittedHandler?.Invoke();
                    Cursor = Cursors.Default;
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
