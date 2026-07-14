using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class DepartmentForm : TreeLayerForm
    {
        #region  私有常量

        /* 单位最大层级 */
        private const int MAX_DEP_LEVEL = 7;

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ISystemConfigContract systemConfigContract;

        #endregion

       #region  私有变量

        private readonly DepartmentModule departmentModule;

        #endregion

       #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            CommonNodeContract = customDepartmentContract;
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            departmentModule = new DepartmentModule() { CustomDepartmentContract = customDepartmentContract };
            TreeNodeShow = departmentModule;
            
            SettingCaption = "刷新(&R)";
            SettingEnabled = false;
            SettingTip = "刷新该单位下所有下级单位的编码";
            NullValuePrompt = "请输入单位名称或者单位编码";

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = MAX_DEP_LEVEL;  /*  允许最大层次 */
            Tip = string.Format("提示信息：(1)用户必须属于一个单位 (2)单位树形结构不能超过 {0} 层节点", MaxLevel + 1);
            string labelInfo = systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.DepartmentLabelInfo);
            string[] labelInfos = labelInfo.Split('|');
            if (labelInfos.Length == 3)
            {
                departmentModule.LayerName = labelInfos[0];
                departmentModule.LayerCode = labelInfos[1];
                departmentModule.DepartmentProperty = labelInfos[2];
            }
            IList<EnumItem> enumItems = SystemConfigHelper.GetDepartmentPorperty();
            departmentModule.AddDepartmentPorperty(enumItems);
            AddControls(departmentModule);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            /* 检查是否存在子单位 */
            int count = customDepartmentContract.GetTotalCountOfChildNode(commonNode.NodeId);
            if (count > 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("{0}节点下有{1}个单位，请删除这些单位。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* 检查是否有用户属于该节点定义的单位, 不允许删除有用户属于单位的节点 */
            count = userAccountContract.GetUserCountByDepId(commonNode.NodeId);
            if (count > 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("共有{0}个用户属于[{0}]单位，请先删除这些用户。",
                    count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            customDepartmentContract.Delete(commonNode.NodeId);
            DeleteNode();
            Cursor = Cursors.Default;
            MessageBox.Show(string.Format("节点[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            bool result = false;
            string verifyResult = string.Empty;
            CustomDepartmentInfo customDepartmentInfo = departmentModule.GetModelInfo();
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = ValidationHelper.Validate(customDepartmentInfo, out verifyResult);
                    if (result)
                    {
                        if (CommonNodeContract.IsExistIdenticalName(commonNode.NodeId, customDepartmentInfo.DepName))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (CommonNodeContract.IsExistIdenticalCode(customDepartmentInfo.DepCode))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("该单位的单位编码[{0}]已存在", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customDepartmentInfo.ParentDepId = commonNode.NodeId;
                        customDepartmentInfo.IsLeaf = true;

                        decimal depId = customDepartmentContract.Insert(customDepartmentInfo);
                        TreeNode tn = new TreeNode { Text = customDepartmentInfo.DepName, Tag = new CommonNode(depId, customDepartmentInfo.ParentDepId, customDepartmentInfo.DepName, customDepartmentInfo.DepCode, true) };
                        AddNode(tn);
                    }
                    break;

                case EditState.Edit:
                    CustomDepartmentInfo oldDepartmentInfo = customDepartmentContract.GetModelInfo(commonNode.NodeId);
                    customDepartmentInfo.DepId = commonNode.NodeId;
                    customDepartmentInfo.ParentDepId = oldDepartmentInfo.ParentDepId;
                    result = ValidationHelper.Validate<CustomDepartmentInfo>(customDepartmentInfo, out verifyResult);
                    if (result)
                    {
                        if (!customDepartmentInfo.DepName.Equals(oldDepartmentInfo.DepName) && customDepartmentContract.IsExistIdenticalName(oldDepartmentInfo.ParentDepId, customDepartmentInfo.DepName))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", customDepartmentInfo.DepName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!customDepartmentInfo.DepCode.Equals(oldDepartmentInfo.DepCode) && customDepartmentContract.IsExistIdenticalCode(customDepartmentInfo.DepCode))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("该单位的单位编码[{0}]已存在", customDepartmentInfo.DepCode), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customDepartmentContract.Update(customDepartmentInfo);
                        if (!customDepartmentInfo.DepName.Equals(oldDepartmentInfo.DepName))
                        {
                            ModifyNode(customDepartmentInfo.DepName);
                        }
                    }
                    break;
            }
            e.Cancel = !result;
            if (!result)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Cursor = Cursors.Default;
        }
        
        /// <summary>
        /// 节点选择前设置按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentQueriedState)
            {
                SettingEnabled = false;
            }
            else
            {
                if (e.Node != null)
                {
                    SettingEnabled = true;
                }
                else
                {
                    SettingEnabled = false;
                }
            }
        }

        /// <summary>
        /// 刷新单位编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            if (MessageBox.Show(string.Format("确定刷新所选择节点[{0}]的所有子节点编码吗？", e.TreeNode.Text), "提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                ShowProgressPanel();
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                customDepartmentContract.RefreshCode(commonNode.NodeId);
                HideProgressPanel();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                HideProgressPanel();
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 单位导入导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentForm_OnExchangeClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            DepartmentExchanged departmentExchanged = new DepartmentExchanged("单位数据", commonNode.NodeId, commonNode.NodeCode, "DepCode ASC", customDepartmentContract);
            DataExchangeModeForm frmDataExchangeMode = new DataExchangeModeForm()
            {                
                Tip = string.Format("当前选择节点：{0}。", commonNode.NodeName),
                DataExportedInterface = departmentExchanged,
                RefreshForm = () =>
                {
                    InitFirstLevelNodes();
                }
            };
            frmDataExchangeMode.ShowDialog();
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override bool ContainsNodeType(int level)
        {
            return false;
        }

        #endregion        
    }
}
