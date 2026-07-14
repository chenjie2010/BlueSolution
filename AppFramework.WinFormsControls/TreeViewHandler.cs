using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    public class TreeViewHandler<T> where T : TreeNode, new()
    {
        #region  受保护变量

        protected readonly TreeView trvTreeView;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="trvTreeView"></param>
        public TreeViewHandler(TreeView trvTreeView)
        {
            this.trvTreeView = trvTreeView;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载第一层节点
        /// </summary>
        /// <param name="commonNodes">子节点对象列表</param>
        public void InitFirstLevelNodes(IList<CommonNode> commonNodes)
        {
            trvTreeView.Nodes.Clear();
            if ((commonNodes == null) || (commonNodes.Count == 0))
            {
                return;
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                trvTreeView.Nodes.Add(tn);
                if (!commonNode.IsLeaf)
                {
                    T childNode = new T { Text = string.Empty, Tag = new CommonNode() };
                    tn.Nodes.Add(childNode);
                }
            }
            if (trvTreeView.Nodes.Count == 1)
            {
                trvTreeView.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// 加载给定节点下的所有子节点 (延迟加载的方式)
        /// </summary>
        /// <param name="tnParent">父节点对象</param>
        /// <param name="commonNodes">节点列表</param>
        /// <param name="show">是否显示子节点</param>
        public void LoadPartialNodes(T tnParent, IList<CommonNode> commonNodes)
        {
            tnParent.Nodes.Clear();
            if ((tnParent == null) || (commonNodes == null) || (commonNodes.Count == 0))
            {
                return;
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                tnParent.Nodes.Add(tn);
                if (!commonNode.IsLeaf)
                {
                    T childNode = new T { Text = string.Empty, Tag = new CommonNode() };
                    tn.Nodes.Add(childNode);
                }
            }
        }

        /// <summary>
        /// 加载部分节点
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadMatchedNodes(IList<CommonNode> commonNodes)
        {
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return;
            }
            if (trvTreeView.Nodes.Count > 0)
            {
                trvTreeView.Nodes.Clear();
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                if (DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    trvTreeView.Nodes.Add(tn);
                    if (!commonNode.IsLeaf)
                    {
                        LoadMatchedNodes(tn, commonNodes);
                        if (tn.Nodes.Count == 0)
                        {
                            T childNode = new T { Text = string.Empty, Tag = new CommonNode() };
                            tn.Nodes.Add(childNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载部分节点
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <param name="commonNodes"></param>
        public void LoadMatchedNodes(decimal parentNodeId, IList<CommonNode> commonNodes)
        {
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return;
            }
            if (trvTreeView.Nodes.Count > 0)
            {
                trvTreeView.Nodes.Clear();
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                if (parentNodeId == commonNode.ParentNodeId)
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    trvTreeView.Nodes.Add(tn);
                    if (!commonNode.IsLeaf)
                    {
                        LoadMatchedNodes(tn, commonNodes);
                        if (tn.Nodes.Count == 0)
                        {
                            T childNode = new T { Text = string.Empty, Tag = new CommonNode() };
                            tn.Nodes.Add(childNode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载部分节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="commonNodes"></param>
        public void LoadMatchedNodes(T tnParent, IList<CommonNode> commonNodes)
        {
            if ((tnParent == null) || (commonNodes == null) || (commonNodes.Count == 0))
            {
                return;
            }
            CommonNode commonNodeTag = tnParent.Tag as CommonNode;
            foreach (CommonNode commonNode in commonNodes)
            {
                if (commonNodeTag.NodeId == commonNode.ParentNodeId)
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    tnParent.Nodes.Add(tn);
                    if (!commonNode.IsLeaf)
                    {
                        LoadMatchedNodes(tn, commonNodes);
                        if (tn.Nodes.Count == 0)
                        {
                            T childNode = new T { Text = string.Empty, Tag = new CommonNode() };
                            tn.Nodes.Add(childNode);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 完全加载树形结构
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <param name="commonNodes"></param>
        public void InitFullTreeNodes(decimal parentNodeId, IList<CommonNode> commonNodes)
        {
            if (trvTreeView.Nodes.Count > 0)
            {
                trvTreeView.Nodes.Clear();
            }
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return;
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                if (parentNodeId == commonNode.ParentNodeId)
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    trvTreeView.Nodes.Add(tn);
                    /* 非叶子节点，则继续加载子节点 */
                    if (!commonNode.IsLeaf)
                    {
                        LoadAllNodes(tn, commonNodes);
                    }
                }
            }
        }

        /// <summary>
        /// 完全加载树形结构
        /// </summary>
        /// <param name="commonNodes">子节点对象列表</param>
        public void InitFullTreeNodes(IList<CommonNode> commonNodes)
        {
            if (trvTreeView.Nodes.Count > 0)
            {
                trvTreeView.Nodes.Clear();
            }
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return;
            }
            foreach (CommonNode commonNode in commonNodes)
            {
                if (DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    trvTreeView.Nodes.Add(tn);
                    /* 非叶子节点，则继续加载子节点 */
                    if (!commonNode.IsLeaf)
                    {
                        LoadAllNodes(tn, commonNodes);
                    }
                }
            }
        }

        /// <summary>
        /// 加载给定节点下所有的子节点
        /// </summary>
        /// <param name="tnParent">父节点对象</param>
        /// <param name="commonNodes">节点列表</param>
        public void LoadAllNodes(T tnParent, IList<CommonNode> commonNodes)
        {
            if ((tnParent == null) || (commonNodes == null) || (commonNodes.Count == 0))
            {
                return;
            }
            if (tnParent.Nodes.Count > 0)
            {
                tnParent.Nodes.Clear();
            }
            CommonNode commonNodeTag = tnParent.Tag as CommonNode;
            foreach (CommonNode commonNode in commonNodes)
            {
                if (commonNode.ParentNodeId == commonNodeTag.NodeId)
                {
                    T tn = new T { Text = commonNode.NodeName, Tag = commonNode };
                    tnParent.Nodes.Add(tn);

                    /* 非叶子节点，则继续加载子节点 */
                    if (!commonNode.IsLeaf)
                    {
                        LoadAllNodes(tn, commonNodes);
                    }
                }
            }
        }

        /// <summary>
        /// 给节点赋值
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="authority"></param>
        public void SetCheckValues(T tnParent, long authority)
        {
            TreeNodeCollection tc = null;
            if (tnParent != null)
            {
                tc = tnParent.Nodes;
                
            }
            else
            {
                tc = trvTreeView.Nodes;
            }
            foreach (T tnChild in tc)
            {
                CommonNode commonNode = tnChild.Tag as CommonNode;
                tnChild.Checked = AuthorityHelper.CheckAuthority(authority, Convert.ToByte(commonNode.NodeId));
                SetCheckValues(tnChild, authority);
            }
        }

        /// <summary>
        /// 清除节点
        /// </summary>
        public void ClearNodes()
        {
            trvTreeView.Nodes.Clear();
        }
        #endregion
    }
}
