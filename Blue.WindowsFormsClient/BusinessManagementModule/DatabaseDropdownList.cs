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
    /// 组合表的下拉选择项操作类
    /// </summary>
    public class DatabaseDropdownList : TreeDropdownBase, ITreeDropdownHandler
    {
        #region 契约属性
        
        /// <summary>
        /// 数据库契约
        /// </summary>
        public ICustomDatabaseContract CustomDatabaseContract
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseDropdownList(ICustomDatabaseContract customDatabaseContract)
            : base(customDatabaseContract, customDatabaseContract, decimal.MinValue, false)
        {
            CustomDatabaseContract = customDatabaseContract;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 初始化获得节点
        /// </summary>
        /// <returns></returns>
        public override IList<CommonNode> InitTree()
        {
            IList<CommonNode> commonNodes = UserEnumHelper.GetCommonNodes(typeof(DataWarehouse));

            foreach(var commonNode in commonNodes)
            {
                commonNode.IsLeaf = false;
            }

            return commonNodes;
        }

        /// <summary>
        /// 扩展后获得的节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override IList<CommonNode> AfterExpand(TreeNode treeNode)
        {
            IList<CommonNode> commonNodes = null;
            
            CommonNode commonNode = treeNode.Tag as CommonNode;
            /* 查询节点包括类型 */
            if (treeNode.Level == 0)
            {
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
                foreach (var node in commonNodes)
                {
                    node.IsLeaf = true;
                }
            }

            return commonNodes;
        }

        #endregion

        #region 抽象方法实现

        /// <summary>
        /// 获取提示
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public override string GetToolTipText(TreeNode treeNode)
        {
            return string.Empty;
        }


        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override bool ContainsNodeType(int level)
        {
            return false;
        }

        #endregion
    }
}
