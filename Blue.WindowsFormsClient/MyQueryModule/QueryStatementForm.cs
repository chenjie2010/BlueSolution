using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QueryStatementForm : Form
    {
        
        #region 契约接口

        private readonly IUserQueryContract userQueryContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有变量


        #endregion

        #region 属性

        //// <summary>
        /// 选择节点
        /// </summary>
        public NodeSelectedDelegate NodeSelected
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryStatementForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userQueryContract = BusinessChannelFactory.CreateUserQueryContract();
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSentenceForm_Load(object sender, EventArgs e)
        {
            userQueryDropdownList.CustomGroupContract = customGroupContract;
            userQueryDropdownList.UserQueryContract = userQueryContract;
            userQueryDropdownList.InitCommonNodeContract(customGroupContract);
            userQueryDropdownList.LoadData();
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userQueryDropdownList_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                UserQueryInfo userQueryInfo = userQueryContract.GetModelInfo(commonNode.NodeId);
                if (userQueryInfo != null)
                {
                    rtxtNotes.Text = userQueryInfo.Notes;
                }
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (userQueryDropdownList.SelectedNode == null)
                {
                    MessageBox.Show("请先选择查询语句的名称！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (NodeSelected != null)
                {
                    NodeSelected(userQueryDropdownList.SelectedNode);
                }
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

     
    }
}
