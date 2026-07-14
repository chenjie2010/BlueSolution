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
    public abstract class TreeDropdownBase : ITreeDropdownHandler
    {
        #region 属性

        /// <summary>
        /// 根节点编号
        /// </summary>
        public decimal RootNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 分组节点类型
        /// </summary>
        public byte NodeType
        {
            get;
            set;
        }

        /// <summary>
        /// 通用节点契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询节点操作契约
        /// </summary>
        public ICommonNodeContract QueriedCommonNodeContract
        {
            set;
            get;
        }

        /// <summary>
        /// 显示父辈节点名称
        /// </summary>
        public bool ParentNamesShowed
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonNodeContract"></param>
        /// <param name="queriedCommonNodeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="parentNamesShowed"></param>
        public TreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, bool parentNamesShowed)
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
        public TreeDropdownBase(ICommonNodeContract commonNodeContract, ICommonNodeContract queriedCommonNodeContract, decimal rootNodeId, byte nodeType, bool parentNamesShowed)
        {
            CommonNodeContract = commonNodeContract;
            QueriedCommonNodeContract = queriedCommonNodeContract;
            RootNodeId = rootNodeId;
            NodeType = nodeType;
            ParentNamesShowed = parentNamesShowed;
        }

        #endregion

        #region 接口方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public virtual IList<CommonNode> InitTree()
        {
            if (ContainsNodeType(0) && NodeType > 0)
            {
                return CommonNodeContract.GetChildNodes(RootNodeId, NodeType);
            }
            else
            {
                return CommonNodeContract.GetChildNodes(RootNodeId);
            }
        }       
        
        /// <summary>
        /// 查询获得的节点
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public virtual IList<CommonNode> Query(string content)
        {
            return QueriedCommonNodeContract.GetChildNodes(string.Format("%{0}%", content));
        }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public abstract IList<CommonNode> AfterExpand(TreeNode treeNode);

        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public abstract string GetToolTipText(TreeNode treeNode);

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected abstract bool ContainsNodeType(int level);

        #endregion

    }
}
