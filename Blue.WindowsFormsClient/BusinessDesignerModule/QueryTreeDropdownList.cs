//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserTypeTreeDropdownList .cs
// 描述: QueryTreeDropdownList 
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
    /// 查询的下拉选择项操作类
    /// </summary>
    public class QueryTreeDropdownList : CombinedTreeDropdownBase, ITreeDropdownHandler
    {
        #region 契约属性

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询契约
        /// </summary>
        public ICustomQueyContract CustomQueyContract
        {
            get; set;
        }

        /// <summary>
        /// 表的类型
        /// </summary>
        public TableFilter TableFilter
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        public QueryTreeDropdownList(ICustomGroupContract customGroupContract, ICustomQueyContract customQueyContract, TableFilter tableFilter)
            : this(customGroupContract, customQueyContract, decimal.MinValue, false, tableFilter)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="rootNodeId"></param>
        public QueryTreeDropdownList(ICustomGroupContract customGroupContract, ICustomQueyContract customQueyContract, decimal rootNodeId, TableFilter tableFilter)
            : this(customGroupContract, customQueyContract, rootNodeId, false, tableFilter)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="parentNamesShowed"></param>
        public QueryTreeDropdownList(ICustomGroupContract customGroupContract, ICustomQueyContract customQueyContract, bool parentNamesShowed, TableFilter tableFilter)
            : this(customGroupContract, customQueyContract, decimal.MinValue, parentNamesShowed, tableFilter)
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="parentNamesShowed"></param>
        public QueryTreeDropdownList(ICustomGroupContract customGroupContract, ICustomQueyContract customQueyContract, decimal rootNodeId, bool parentNamesShowed, TableFilter tableFilter) 
            : base(customGroupContract, customQueyContract, rootNodeId, (byte)GroupType.Query, parentNamesShowed)
        {
            CustomGroupContract = customGroupContract;
            CustomQueyContract = customQueyContract;
            RootNodeId = rootNodeId;
            ParentNamesShowed = parentNamesShowed;
            TableFilter = tableFilter;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            IList<CommonNode> commonNodes = null;

            CombinedNodeType treeNodeType = GetNodeType(treeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            CommonNode commonNode = treeNode.Tag as CommonNode;
            /* 查询节点包括类型 */
            if (treeNodeType == CombinedNodeType.Leaf)
            {
                //if (TableFilter == TableFilter.System)
                //{
                //    commonNodes = CustomQueyContract.GetChildNodes(commonNode.NodeId, (byte)FormType.Custom);
                //}
                //else
                //{
                //    commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
                //}
            }
            else
            {
                if (ContainsNodeType(treeNode.Level+1) && NodeType > 0)
                {
                    commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId, NodeType);                    
                }
                else
                {
                    commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
                }
            }

            return commonNodes;
        }

        #endregion

        #region 抽象方法实现

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected override void SetCommonNodeContract(CombinedNodeType combinedNodeType)
        {
            /* 第一层为用户类型分类节点，第二层为用户类型节点 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    CommonNodeContract = CustomGroupContract;
                    break;

                case CombinedNodeType.Leaf:
                    CommonNodeContract = CustomQueyContract;
                    break;

                default:
                    throw new ArgumentException("不支持该用户类型节点。");
            }
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

            if (combinedNodeType == CombinedNodeType.Leaf && TableFilter == TableFilter.All)
            {
                return result = false;
            }

            return result;
        }

        #endregion
    }
}
