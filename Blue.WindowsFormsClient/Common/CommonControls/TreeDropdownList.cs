//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TreeDropdownList.cs
// 描述: 树形结构类
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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Utility;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 树形结构类
    /// </summary>
    public class TreeDropdownList : ComoboxTreeview
    {
        #region 私有变量

        /// <summary>
        /// 是否是查询状态
        /// </summary>
        private bool isQueriedState = false;

        #endregion

        #region 属性

        /// <summary>
        /// 通用的下拉选择项操作对象
        /// </summary>
        public ITreeDropdownHandler TreeDropdownHandler
        {
            get;
            set;
        }               

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeDropdownList()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownList_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownList_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    IList<CommonNode> commonNodes = TreeDropdownHandler.AfterExpand(e.Node);
                    TreeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownList_OnSearch(object sender, StringEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Content))
            {
                isQueriedState = true;
                IList<CommonNode> commonNodes = TreeDropdownHandler.Query(e.Content);
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
                if (TreeView.Nodes.Count <= 5)
                {
                    foreach (TreeNode tn in TreeView.Nodes)
                    {
                        tn.ToolTipText = TreeDropdownHandler.GetToolTipText(tn);
                    }
                }                
            }
            else
            {
                isQueriedState = false;
                InitalizeTreeView();
            }
        }        

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownList_OnNodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            /* 主要处理查询后的节点的父节点关系 */
            if (isQueriedState && commonNode != null && commonNode.NodeId > 0 && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (e.Node.Level == 0))
            {
                if (string.IsNullOrWhiteSpace(e.Node.ToolTipText))
                {
                    e.Node.ToolTipText = TreeDropdownHandler.GetToolTipText(e.Node);
                }
            }
        }

        /// <summary>
        /// 节点选择之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeDropdownList_BeforeTreeNodeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (TreeDropdownHandler!= null && TreeDropdownHandler.ParentNamesShowed && e.Node != null)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                IList<CommonNode> commonNodes = new List<CommonNode>();
                TreeNode currentTreeNode = e.Node;                
                while (currentTreeNode != null)
                {
                    CommonNode node = currentTreeNode.Tag as CommonNode;
                    commonNodes.Add(node);
                    currentTreeNode = currentTreeNode.Parent;
                }
                commonNode.NodeName = UserControlHelper.GetFormattedName(commonNodes);
                e.Node.Tag = commonNode;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化节点
        /// </summary>
        public void InitalizeTreeView()
        {
            IList<CommonNode> commonNodes = TreeDropdownHandler.InitTree();
            TreeViewHandler.InitFirstLevelNodes(commonNodes);
        }

        /// <summary>
        /// 加载全部节点
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <param name="commonNodes"></param>
        public void LoadMatchedNodes(decimal parentNodeId, IList<CommonNode> commonNodes)
        {
            TreeViewHandler.LoadMatchedNodes(parentNodeId, commonNodes);
        }

        /// <summary>
        /// 加载部分节点
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadMatchedNodes(IList<CommonNode> commonNodes)
        {
            TreeViewHandler.LoadMatchedNodes(commonNodes);
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TreeDropdownList";
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.TreeDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.TreeDropdownList_BeforeTreeNodeSelect);
            this.OnSearch += new System.EventHandler<AppFramework.WinFormsControls.StringEventArgs>(this.TreeDropdownList_OnSearch);
            this.OnNodeMouseHover += new System.EventHandler<System.Windows.Forms.TreeNodeMouseHoverEventArgs>(this.TreeDropdownList_OnNodeMouseHover);
            this.Load += new System.EventHandler(this.TreeDropdownList_Load);
            this.ResumeLayout(false);

        }       
    }
}
