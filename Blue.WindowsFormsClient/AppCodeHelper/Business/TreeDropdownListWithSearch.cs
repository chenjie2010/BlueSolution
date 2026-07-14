//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LogHelper.cs
// 描述: 单位下拉框类
// 作者：ChenJie 
// 编写日期：2016-08-23
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary.Utility;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 单位下拉框类
    /// </summary>
    public class TreeDropdownListWithSearch : ComoboxTreeviewWithSearch
    {
        #region 属性

        /// <summary>
        /// 通用节点契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        #endregion


        #region 私有变量

        private readonly TreeViewHandler<TreeNode> treeViewHandler;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeDropdownListWithSearch()
        {
            InitializeComponent();
            this.ShowNodeToolTips = true;
            treeViewHandler = new TreeViewHandler<TreeNode>(TreeView);
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownListWithSearch_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {                
                InitalizeTreeView();
            }
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownListWithSearch_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    IList<CommonNode> commonNodes = CommonNodeContract.GetCommonNodes(commonNode.NodeId);
                    treeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void TreeDropdownListWithSearch_OnSearch(object sender, StringEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Content))
            {
                IList<CommonNode> commonNodes = CommonNodeContract.GetCommonNodes(string.Format("%{0}%", e.Content));
                treeViewHandler.InitFirstLevelNodes(commonNodes);
                if (TreeView.Nodes.Count <= 5)
                {
                    foreach (TreeNode tn in TreeView.Nodes)
                    {
                        CommonNode commonNode = tn.Tag as CommonNode;
                        SetToolTipText(commonNode.NodeId, tn);
                    }
                }
            }
            else
            {
                InitalizeTreeView();
            }
        }

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownListWithSearch_OnNodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            /* 主要处理查询后的节点的父节点关系 */
            if (commonNode != null && commonNode.NodeId > 0 && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (e.Node.Level == 0))
            {
                if (string.IsNullOrWhiteSpace(e.Node.ToolTipText))
                {
                    SetToolTipText(commonNode.NodeId, e.Node);
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置提示
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="treeNode"></param>
        private void SetToolTipText(decimal nodeId, TreeNode treeNode)
        {
            IList<string> names = CommonNodeContract.GetParentNamesOfNode(nodeId);
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }
            treeNode.ToolTipText = sb.ToString();
        }

        /// <summary>
        /// 初始化节点
        /// </summary>
        public void InitalizeTreeView()
        {
            IList<CommonNode> commonNodes = CommonNodeContract.GetCommonNodes(decimal.MinValue);
            treeViewHandler.InitFirstLevelNodes(commonNodes);
            if (TreeView.Nodes.Count == 1)
            {
                TreeView.Nodes[0].Expand();
            }
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeDropdownListWithSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TreeDropdownListWithSearch";
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.TreeDropdownListWithSearch_AfterTreeNodeExpand);
            this.OnNodeMouseHover += new System.EventHandler<System.Windows.Forms.TreeNodeMouseHoverEventArgs>(this.TreeDropdownListWithSearch_OnNodeMouseHover);
            this.OnSearch += new System.EventHandler<AppFramework.WinFormsControls.StringEventArgs>(this.TreeDropdownListWithSearch_OnSearch);
            this.Load += new System.EventHandler(this.TreeDropdownListWithSearch_Load);
            this.ResumeLayout(false);

        }
    }
}
