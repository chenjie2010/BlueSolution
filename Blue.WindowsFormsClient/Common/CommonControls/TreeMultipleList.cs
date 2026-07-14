//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TreeMultipleList.cs
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
    public class TreeMultipleList : ComoboxTreeview
    {
        #region 私有变量

        /// <summary>
        /// 是否是查询状态
        /// </summary>
        private bool isQueriedState = false;

        /// <summary>
        /// 初始化节点
        /// </summary>
        private IList<CommonNode> commonNodes = null;

        #endregion

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

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeMultipleList()
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
        private void TreeMultipleList_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeMultipleList_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    IList<CommonNode> commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
                    TreeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeMultipleList_OnSearch(object sender, StringEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Content))
            {
                isQueriedState = true;
                IList<string> nodeCodes = new List<string>();
                if (commonNodes != null && commonNodes.Count > 0)
                {
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        nodeCodes.Add(string.Format("{0}%", commonNode.NodeCode));
                    }
                }
                IList<CommonNode> nodes = CommonNodeContract.GetChildNodes(string.Format("%{0}%", e.Content), nodeCodes);
                TreeViewHandler.InitFirstLevelNodes(nodes);
            }
            else
            {
                isQueriedState = false;
                InitalizeTreeView(commonNodes);
            }
        }      

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeMultipleList_OnNodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            /* 主要处理查询后的节点的父节点关系 */
            if (isQueriedState && commonNode != null && commonNode.NodeId > 0 && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (e.Node.Level == 0))
            {
                if (string.IsNullOrWhiteSpace(e.Node.ToolTipText))
                {
                    IList<string> names = CommonNodeContract.GetHierarchicalNamesOfNode(commonNode.NodeId);
                    StringBuilder sb = new StringBuilder();
                    foreach (string name in names)
                    {
                        sb.AppendFormat("[{0}]", name);
                    }
                    e.Node.ToolTipText = sb.ToString();
                }
            }
        }
        
        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化节点
        /// </summary>
        public void InitalizeTreeView(IList<CommonNode> nodes)
        {
            commonNodes = nodes;
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
            // TreeMultipleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TreeMultipleList";
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.TreeMultipleList_AfterTreeNodeExpand);
            this.OnSearch += new System.EventHandler<AppFramework.WinFormsControls.StringEventArgs>(this.TreeMultipleList_OnSearch);
            this.OnNodeMouseHover += new System.EventHandler<System.Windows.Forms.TreeNodeMouseHoverEventArgs>(this.TreeMultipleList_OnNodeMouseHover);
            this.Load += new System.EventHandler(this.TreeMultipleList_Load);
            this.ResumeLayout(false);

        }       
    }
}
