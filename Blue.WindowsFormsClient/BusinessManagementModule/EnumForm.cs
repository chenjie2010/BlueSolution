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
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class EnumForm : TreeLayerForm
    {
        #region  私有常量

        /* 枚举最大层级 */
        private const int MAX_ENUM_LEVEL = 9;

        #endregion
         
        #region 契约接口

        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDataFieldContract customDataFieldContract;

        #endregion

        #region  私有变量

        private readonly EnumModule enumModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumForm()
        {
            InitializeComponent();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            CommonNodeContract = customEnumContract;
            enumModule = new EnumModule() { CustomEnumContract = customEnumContract, CustomDataFieldContract = customDataFieldContract };
            TreeNodeShow = enumModule;

            SettingCaption = "刷新(&R)";
            SettingEnabled = false;
            SettingTip = "刷新该节点下所有子节点的编码";
            NullValuePrompt = "请输入枚举名称或者枚举编码";

            /* 初始化属性 */
            MaxLevel = MAX_ENUM_LEVEL;  /*  允许最大层次 */
            Tip = string.Format("提示信息：(1)枚举的树形结构不能超过 {0} 层节点", MaxLevel + 1);
            AddControls(enumModule);
        }

        /// <summary>
        /// 枚举窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnAfterSelectOfTreeView(object sender, TreeViewEventArgs e)
        {
            int count = 0;
            CommonNode commonNode = e.Node.Tag as CommonNode;
            if (commonNode != null && commonNode.NodeId > 0)
            {
                count = customDataFieldContract.GetDataFieldCountByEnumId(commonNode.NodeId);                
            }
            enumModule.SetTextOfDataFieldList(count);
        }

        /// <summary>
        /// 刷新节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
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
                customEnumContract.RefreshCode(commonNode.NodeId);
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
        /// 枚举数据交换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnExchangeClick(object sender, TreeNodeItemClickEventArgs e)
        {            
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            EnumExchanged enumExchanged = new EnumExchanged("枚举数据", commonNode.NodeId, commonNode.NodeCode, "EnumCode ASC", customEnumContract);
            DataExchangeModeForm frmDataExchangeMode = new DataExchangeModeForm()
            {                
                Tip = string.Format("当前选择节点：{0}。", commonNode.NodeName),
                DataExportedInterface = enumExchanged,
                RefreshForm = () =>
                {
                    InitFirstLevelNodes();
                }
            };
            frmDataExchangeMode.ShowDialog();
        }

        /// <summary>
        /// 节点选择前设置按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentQueriedState)
            {
                SettingEnabled = false;
            }
            else
            {
                if (e.Node != null && e.Node.Level > 0)
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
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            /* 检查是否存在子枚举 */
            int count = customEnumContract.GetTotalCountOfChildNode(commonNode.NodeId);
            if (count > 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("删除失败，{0}节点下有{1}个枚举。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            /* 检查是否有属于该节点的字段, 不允许删除有字段类型属于该枚举的节点 */
            count = customDataFieldContract.GetDataFieldCountByEnumId(commonNode.NodeId);
            if (count > 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("删除失败，共有{0}个字段的枚举类型属于[{0}]。",
                    count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            customEnumContract.Delete(commonNode.NodeId);
            DeleteNode();
            Cursor = Cursors.Default;
            MessageBox.Show(string.Format("节点[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            bool result = false;
            string verifyResult = string.Empty;
            if (!enumModule.CheckInputs(out verifyResult))
            {
                e.Cancel = true;
                Cursor = Cursors.Default;
                MessageBox.Show(verifyResult, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CustomEnumInfo customEnumInfo = enumModule.GetModelInfo();
            switch (e.EditState)
            {
                case EditState.Add:
                    if (commonNode.NodeId > 0)
                    {
                        customEnumInfo.ParentEnumId = commonNode.NodeId;
                    }
                    else
                    {
                        customEnumInfo.ParentEnumId = decimal.MinValue;
                    }
                    customEnumInfo.IsLeaf = true;
                    result = ValidationHelper.Validate<CustomEnumInfo>(customEnumInfo, out verifyResult);
                    if (result)
                    {
                        if (CommonNodeContract.IsExistIdenticalName(commonNode.NodeId, customEnumInfo.EnumName))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //if (CommonNodeContract.IsExistIdenticalCode(customEnumInfo.EnumValue))
                        //{
                        //    e.Cancel = true;
                        //    Cursor = Cursors.Default;
                        //    MessageBox.Show(string.Format("该枚举的枚举值[{0}]已存在", customEnumInfo.EnumValue), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}
                        decimal enumId = customEnumContract.Insert(customEnumInfo);
                        TreeNode tn = new TreeNode { Text = customEnumInfo.EnumName, Tag = new CommonNode(enumId, customEnumInfo.ParentEnumId, customEnumInfo.EnumName, customEnumInfo.EnumCode, true) };
                        AddNode(tn);
                        enumModule.ClearTextOfDataFieldList();
                    }
                    break;

                case EditState.Edit:
                    CustomEnumInfo oldCustomEnumInfo = customEnumContract.GetModelInfo(commonNode.NodeId);
                    customEnumInfo.EnumId = commonNode.NodeId;
                    customEnumInfo.ParentEnumId = commonNode.ParentNodeId;
                    result = ValidationHelper.Validate<CustomEnumInfo>(customEnumInfo, out verifyResult);
                    if (result)
                    {
                        if (!customEnumInfo.EnumName.Equals(oldCustomEnumInfo.EnumName) && customEnumContract.IsExistIdenticalName(customEnumInfo.ParentEnumId, customEnumInfo.EnumName))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", customEnumInfo.EnumName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!customEnumInfo.EnumValue.Equals(oldCustomEnumInfo.EnumValue) && customEnumContract.IsExistIdenticalCode(customEnumInfo.EnumValue))
                        {
                            e.Cancel = true;
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("该枚举的枚举值[{0}]已存在", customEnumInfo.EnumValue), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customEnumContract.Update(customEnumInfo);
                        if (!customEnumInfo.EnumName.Equals(oldCustomEnumInfo.EnumName))
                        {
                            ModifyNode(customEnumInfo.EnumName);
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


        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "自定义枚举", string.Empty, false));
            InitTreeNodes(commonNodes);
            enumModule.ClearTextOfDataFieldList();
        }

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
