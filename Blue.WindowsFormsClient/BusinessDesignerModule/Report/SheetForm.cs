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
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class SheetForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomReportContract customReportContract;
        private readonly ICustomSheetContract customSheetContract;

        #endregion

        #region  私有变量

        private ReportModule reportModule;
        private SheetModule sheetModule;

        #endregion

        #region  属性

        /// <summary>
        /// 编号
        /// </summary>
        public decimal ReportId
        {
            set;
            get;
        }

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportCategory ReportCategory
        {
            get;
            set;
        }

        /// <summary>
        /// 增加样表
        /// </summary>
        public AddSheetDelegate AddSheet
        {
            set;
            get;
        }

        /// <summary>
        /// 编辑样表
        /// </summary>
        public EditSheetDelegate EditSheet
        {
            set;
            get;
        }

        /// <summary>
        /// 删除样表
        /// </summary>
        public DeleteSheetDelegate DeleteSheet
        {
            set;
            get;
        }

        /// <summary>
        /// 移动样表
        /// </summary>
        public MoveSheetDelegate MoveSheet
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SheetForm()
        {
            InitializeComponent();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 2;  /*  允许最大层次 */
            Tip = "创建样表成功后请进行样表设计。";
            NullValuePrompt = "请输入样表名称查询";

            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;
            IList<UserControl> userControls = new List<UserControl>();
            reportModule = new ReportModule() { CustomReportContract = customReportContract, ReportCategory = ReportCategory };
            userControls.Add(reportModule);

            sheetModule = new SheetModule() { CustomSheetContract = customSheetContract };
            userControls.Add(sheetModule);
            AddControls(userControls);
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_Load(object sender, EventArgs e)
        {
                                  
        }
        
        private void SheetForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        private void SheetForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(combinedNodeType);
            SetParametersOnPanel(combinedNodeType);
        }

        private void SheetForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customSheetContract;
        }

        /// <summary>
        /// 删除样表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            customSheetContract.Delete(commonNode.NodeId);
            DeleteNode();
            DeleteSheet(e.TreeNode.Index);
            Cursor = Cursors.Default;
            MessageBox.Show(string.Format("节点[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level);
            result = sheetModule.ValidateModelInfo(out verifyResult);
            if (result)
            {
                CustomSheetInfo customSheetInfo = sheetModule.GetModelInfo();
                Cursor = Cursors.WaitCursor;
                switch (e.EditState)
                {
                    case EditState.Add:
                        int rowCount = 50;
                        int columnCount = 30;
                        customSheetInfo.ReportId = ReportId;
                        customSheetInfo.SheetRowCount = rowCount;
                        customSheetInfo.SheetColCount = columnCount;
                        if (customSheetContract.IsExistIdenticalName(commonNode.NodeId, customSheetInfo.SheetName))
                        {
                            verifyResult = string.Format("同一报表下样表名称不允许重复, 样表名称[{0}]已存在。", customSheetInfo.SheetName);
                            result = false;
                        }
                        else
                        {
                            nodeId = customSheetContract.Insert(customSheetInfo);
                            name = customSheetInfo.SheetName;
                            value = customSheetInfo.SheetCode;
                            AddSheet(customSheetInfo.SheetName, rowCount, columnCount);
                        }
                        break;

                    case EditState.Edit:
                        CustomSheetInfo oldCustomSheetInfo = customSheetContract.GetModelInfo(commonNode.NodeId);
                        customSheetInfo.ReportId = oldCustomSheetInfo.ReportId;

                        if (!customSheetInfo.SheetName.Equals(oldCustomSheetInfo.SheetName)
                            && customReportContract.IsExistIdenticalName(commonNode.NodeId, oldCustomSheetInfo.SheetName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一报表下样表名称不允许重复, 样表名称[{0}]已存在。", customSheetInfo.SheetName);
                        }
                        customSheetContract.Update(customSheetInfo);
                        name = customSheetInfo.SheetName;                        
                        EditSheet(e.TreeNode.Index, customSheetInfo.SheetName);
                        break;
                }
            }
            Cursor = Cursors.Default;
            if (result)
            {
                if (e.EditState == EditState.Add)
                {
                    TreeNode tn = new TreeNode
                    {
                        Text = name,
                        Tag = new CommonNode(nodeId, commonNode.NodeId, name, value, true)
                    };
                    AddNode(tn);
                }
                else if (e.EditState == EditState.Edit)
                {
                    if (!commonNode.NodeName.Equals(name))
                    {
                        ModifyNode(name);
                    }
                }                
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    reportModule.Visible = false;
                    sheetModule.Visible = true;
                    TreeNodeShow = sheetModule;
                }
                else
                {
                    reportModule.Visible = true;
                    sheetModule.Visible = false;
                    TreeNodeShow = reportModule;
                }
            }
            else
            {
                SetCommonNodeContract(combinedNodeType);
                SetParametersOnPanel(combinedNodeType);
            }
            if (combinedNodeType == CombinedNodeType.Leaf)
            {
                SettingEnabled = true;
            }
            else
            {
                SettingEnabled = false;
            }
        }

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SheetForm_OnMovedNodeClick(object sender, AppFramework.WinFormsControls.MovedNodeEventArgs e)
        {
            MoveSheet(e.MovedTreeNode.Index, e.PreviousNodeIndex, e.MovedDriection);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetCommonNodeContract(CombinedNodeType combinedNodeType)
        {
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                    CommonNodeContract = customReportContract;
                    break;

                case CombinedNodeType.Leaf:
                    CommonNodeContract = customSheetContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetParametersOnPanel(CombinedNodeType combinedNodeType)
        {
            /* (1) 报表 (2) 样表 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                    TreeNodeShow = reportModule;
                    sheetModule.Visible = false;
                    reportModule.Visible = true;
                    break;

                case CombinedNodeType.Leaf:
                    TreeNodeShow = sheetModule;
                    reportModule.Visible = false;
                    sheetModule.Visible = true;
                    break;
            }
        }


        /// <summary>
        /// 获得报表节点报表业务类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType combinedNodeType = CombinedNodeType.Root;

            /* (1) 报表 (2) 样表 */
            switch (level)
            {
                case 0:
                    combinedNodeType = CombinedNodeType.Root;
                    break;

                case 1:
                    combinedNodeType = CombinedNodeType.Leaf;
                    break;
            }

            return combinedNodeType;
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            CommonNode commonNode = customReportContract.GetCommonNode(ReportId);
            commonNode.ParentNodeId = decimal.MinValue;
            commonNode.IsLeaf = false;
            commonNodes.Add(commonNode);
            InitTreeNodes(commonNodes);
        }

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override bool ContainsNodeType(int level)
        {
            bool result = true;

            CombinedNodeType combinedNodeType = GetNodeType(level);

            if (combinedNodeType == CombinedNodeType.Leaf)
            {
                return result = false;
            }

            return result;
        }

        #endregion
    }
}
