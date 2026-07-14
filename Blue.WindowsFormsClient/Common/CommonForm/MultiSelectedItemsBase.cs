//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: MultiSelectedItemsBase .cs
// 描述: MultiSelectedItemsBase 
// 作者：ChenJie 
// 编写日期：2018-01-16
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 多选操作
    /// </summary>
    public abstract class MultiSelectedItemsBase : IMultiSelectedItemsHandler
    {
        #region 受保护变量

        /// <summary>
        /// 节点类型
        /// </summary>
        protected byte nodeType;

        #endregion               

        #region  属性

        /// <summary>
        /// 只选择叶子节点
        /// </summary>
        public bool OnlySelectedLeaf
        {
            get;
            set;
        }

        /// <summary>
        /// 节点操作契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            set;
            get;
        }

        /// <summary>
        /// 查询节点操作契约
        /// </summary>
        public ICommonNodeContract QueriedCommonNodeContract
        {
            set;
            get;
        }

        #endregion

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="onlySelectedLeaf"></param>
        public MultiSelectedItemsBase(byte nodeType, bool onlySelectedLeaf)
        {
            this.nodeType = nodeType;
            OnlySelectedLeaf = onlySelectedLeaf;
        }

        #endregion

        #region  接口方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public IList<CommonNode> InitTree()
        {
            IList<CommonNode> commonNodes = null;

            if (ContainsNodeType(0) && nodeType > 0)
            {
                commonNodes = CommonNodeContract.GetChildNodes(decimal.MinValue, nodeType);
            }
            else
            {
                commonNodes = CommonNodeContract.GetChildNodes(decimal.MinValue);
            }

            return commonNodes;
        }

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            IList<CommonNode> commonNodes = null;

            TreeNodeType treeNodeType = GetNodeType(treeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            CommonNode commonNode = treeNode.Tag as CommonNode;
            if (ContainsNodeType(treeNode.Level + 1) && nodeType > 0)
            {
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId, nodeType);
            }
            else
            {
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
            }

            return commonNodes;
        }

        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IList<CommonNode> Query(string content)
        {
            return QueriedCommonNodeContract.GetChildNodes(string.Format("%{0}%", content));
        }

        /// <summary>
        /// 获得被选择的单位列表
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="departmentIdList"></param>
        public void GetNodeList(TreeNodeCollection tc, IList<CommonNode> commonNodes)
        {
            foreach (TreeNode tn in tc)
            {
                if (tn.Checked)
                {
                    TreeNodeType treeNodeType = GetNodeType(tn.Level);
                    if (treeNodeType == TreeNodeType.Leaf)
                    {
                        commonNodes.Add(tn.Tag as CommonNode);
                    }
                }
                GetNodeList(tn.Nodes, commonNodes);
            }
        }

        /// <summary>
        /// 判断是否是叶子节点
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public bool IsLeafNode(TreeNode tn)
        {
            TreeNodeType treeNodeType = GetNodeType(tn.Level);

            return treeNodeType == TreeNodeType.Leaf;
        }

        #endregion

        #region  抽象方法

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected abstract TreeNodeType GetNodeType(int level);

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected abstract void SetCommonNodeContract(TreeNodeType treeNodeType);

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected abstract bool ContainsNodeType(int level);

        #endregion

        #region  私有方法       



        #endregion
    }
}
