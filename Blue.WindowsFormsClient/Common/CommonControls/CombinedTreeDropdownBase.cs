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
    public abstract class CombinedTreeDropdownBase : TreeDropdownBase, ITreeDropdownHandler
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="queriedCommonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="parentNamesShowed"></param>
        public CombinedTreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, bool parentNamesShowed)
            : this(commonNodeContract, queriedCommonNodeContract, rootNodeId, 0, parentNamesShowed)
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
        public CombinedTreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, byte nodeType, bool parentNamesShowed)
            : base(commonNodeContract, queriedCommonNodeContract, rootNodeId, nodeType, parentNamesShowed)
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
            CombinedNodeType treeNodeType = GetNodeType(treeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            CommonNode commonNode = treeNode.Tag as CommonNode;
            /* 对于分组节点，如果有存在节点类型，按照节点类型条件获得子节点；叶子节点默认为不存在类型*/
            if (treeNodeType == CombinedNodeType.Leaf || NodeType <= 0)
            {
                return CommonNodeContract.GetChildNodes(commonNode.NodeId);
            }
            else
            {
                return CommonNodeContract.GetChildNodes(commonNode.NodeId, NodeType);
            }
        }

        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override string GetToolTipText(TreeNode treeNode)
        {
            CombinedNodeType treeNodeType = GetNodeType(treeNode.Level);
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
        protected CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType combinedNodeType = CombinedNodeType.Root;

            switch (level)
            {
                case 0:
                    combinedNodeType = CombinedNodeType.ParentCategory;
                    break;

                case 1:
                    combinedNodeType = CombinedNodeType.ChildCategory;
                    break;

                case 2:
                    combinedNodeType = CombinedNodeType.Leaf;
                    break;
            }

            return combinedNodeType;
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected abstract void SetCommonNodeContract(CombinedNodeType combinedNodeType);

        #endregion
    }
}
