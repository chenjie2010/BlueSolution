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
using AppFramework.WinFormsLibrary;

namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    public partial class PersonDataForm : Form
    {
        #region 属性

        /// <summary>
        /// 控件
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
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
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public MemoEdit MemoEditToolTip
        {
            get
            {
                return meToolTip;
            }
        }

        /// <summary>
        /// 申请事由
        /// </summary>
        public string Comment
        {
            set
            {
                meComment.Text = value;
            }
            get
            {
                return meComment.Text;
            }
        }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// 提交
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public DataSumbittedHandlerDelegate SumbittedHandler
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public PersonDataForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体方法

        /// <summary>
        /// /窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonDataForm_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = !ReadOnly;
            btnCancel.Enabled = !ReadOnly;
            cmbReviewer.ReadOnly = ReadOnly;
            meComment.ReadOnly = ReadOnly;
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
                Cursor = Cursors.WaitCursor;
                if (cmbReviewer.Properties.Items.Count == 0)
                {
                    MessageBox.Show("未设置审核人，请与管理员联系。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonNode commonNode = cmbReviewer.EditValue as CommonNode;
                SumbittedHandler?.Invoke(commonNode.NodeId, meComment.Text.Trim());
                Cursor = Cursors.Default;
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

        #region 公有方法

        /// <summary>
        /// 加载评审人列表
        /// </summary>
        /// <param name="reviewers"></param>
        public void LoadReviewers(Dictionary<decimal, string> reviewers)
        {
            foreach (KeyValuePair<decimal, string> keyValue in reviewers)
            {
                cmbReviewer.Properties.Items.Add(new CommonNode(keyValue.Key, keyValue.Value));
            }
            if (reviewers.Count > 0)
            {
                cmbReviewer.SelectedIndex = 0;
            }
        }

        #endregion
    }
}
