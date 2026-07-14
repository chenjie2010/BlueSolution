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
    /// 通用下拉选择项操作类
    /// </summary>
    public class CommonTreeDropdownList : CombinedTreeDropdownBase, ITreeDropdownHandler
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
        /// 业务契约
        /// </summary>
        public ICommonNodeContract BusinessNodeContract
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="combinedTableContract"></param>
        public CommonTreeDropdownList(ICustomGroupContract customGroupContract, ICommonNodeContract commonNodeContract)
            : this(customGroupContract, commonNodeContract, decimal.MinValue)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="combinedTableContract"></param>
        public CommonTreeDropdownList(ICustomGroupContract customGroupContract, ICommonNodeContract commonNodeContract, byte nodeType)
            : this(customGroupContract, commonNodeContract, decimal.MinValue, nodeType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="combinedTableContract"></param>
        /// <param name="rootNodeId"></param>
        public CommonTreeDropdownList(ICustomGroupContract customGroupContract, ICommonNodeContract commonNodeContract, decimal rootNodeId)
            : this(customGroupContract, commonNodeContract, rootNodeId, Byte.MinValue)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="combinedTableContract"></param>
        /// <param name="rootNodeId"></param>
        public CommonTreeDropdownList(ICustomGroupContract customGroupContract, ICommonNodeContract commonNodeContract, decimal rootNodeId, byte nodeType)
            : base(customGroupContract, commonNodeContract, rootNodeId, nodeType, false)
        {
            RootNodeId = rootNodeId;
            CustomGroupContract = customGroupContract;
            BusinessNodeContract = commonNodeContract;
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
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
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
            /* 第一层为分类节点，第二层为业务类型节点 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    CommonNodeContract = CustomGroupContract;
                    break;

                case CombinedNodeType.Leaf:
                    CommonNodeContract = BusinessNodeContract;
                    break;

                default:
                    throw new ArgumentException("不支持该类型节点。");
            }
        }

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override bool ContainsNodeType(int level)
        {
            bool result = false;

            CombinedNodeType combinedNodeType = GetNodeType(level);

            if (combinedNodeType == CombinedNodeType.ParentCategory)
            {
                return result = true;
            }

            return result;
        }

        #endregion
    }
}
