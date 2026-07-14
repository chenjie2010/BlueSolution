//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TreeDropdownBase .cs
// 描述: 通用的下拉选择项操作基类 
// 作者：ChenJie 
// 编写日期：2018-01-17
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
    /// 通用的下拉选择项操作基类
    /// </summary>
    public abstract class SimpleTreeDropdownBase : TreeDropdownBase, ITreeDropdownHandler
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="queriedCommonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="parentNamesShowed"></param>
        public SimpleTreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, bool parentNamesShowed)
            : this(commonNodeContract, queriedCommonNodeContract,  rootNodeId, 0, parentNamesShowed)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="queriedCommonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="nodeType"></param>
        /// <param name="parentNamesShowed"></param>
        public SimpleTreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, byte nodeType, bool parentNamesShowed)
            : base (commonNodeContract, queriedCommonNodeContract,  rootNodeId, nodeType, parentNamesShowed)
        {
                        
        }

        #endregion

        #region 抽象方法实现

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            IList<CommonNode> commonNodes = null;

            TreeNodeType treeNodeType = GetNodeType(treeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            CommonNode commonNode = treeNode.Tag as CommonNode;
            /* 按照节点类型条件获得子节点*/
            if (ContainsNodeType(treeNode.Level+1) && NodeType >= 0)
            {
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId, NodeType);
            }
            else
            {
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override string GetToolTipText(TreeNode treeNode)
        {
            TreeNodeType treeNodeType = GetNodeType(treeNode.Level);
            SetCommonNodeContract(treeNodeType);
            CommonNode commonNode = treeNode.Tag as CommonNode;
            IList<string> names = CommonNodeContract.GetHierarchicalNamesOfNode(commonNode.NodeId);
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }

            return sb.ToString();
        }

        #endregion

        #region 受保护方法

        /// <summary>
        /// 获得树形结构节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected TreeNodeType GetNodeType(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            switch (level)
            {
                case 0:
                    treeNodeType = TreeNodeType.Category;
                    break;

                case 1:
                    treeNodeType = TreeNodeType.Leaf;
                    break;
            }

            return treeNodeType;
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected abstract void SetCommonNodeContract(TreeNodeType treeNodeType);

        #endregion
    }
}
