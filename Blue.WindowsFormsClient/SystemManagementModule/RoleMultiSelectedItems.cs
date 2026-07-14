//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleMultiSelectedItems.cs
// 描述: RoleMultiSelectedItems
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
    /// 角色多选操作
    /// </summary>
    public class RoleMultiSelectedItems : MultiSelectedItemsBase, IMultiSelectedItemsHandler
    {       
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomRoleContract customRoleContract;

        #endregion
                
        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="customRoleContract"></param>
        /// <param name="nodeType"></param>
        /// <param name="onlySelectedLeaf"></param>
        public RoleMultiSelectedItems(ICustomGroupContract customGroupContract, ICustomRoleContract customRoleContract, byte nodeType, bool onlySelectedLeaf)
            : base(nodeType, onlySelectedLeaf)
        {
            CommonNodeContract = customGroupContract;
            QueriedCommonNodeContract = customRoleContract;
            this.customGroupContract = customGroupContract;
            this.customRoleContract = customRoleContract;
            this.nodeType = nodeType;
            OnlySelectedLeaf = onlySelectedLeaf;
        }

        #endregion        

        #region  受保护方法

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override TreeNodeType GetNodeType(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            /* (1) 角色分类 (2) 角色  */
            switch (level)
            {
                case 0:
                    treeNodeType = TreeNodeType.Category;
                    break;

                case 1:
                    treeNodeType = TreeNodeType.Leaf;
                    break;

                default:
                    throw new ArgumentException("不支持该角色节点类型。");
            }

            return treeNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected override void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* (1) 角色分类 (2) 角色  */
            switch (treeNodeType)
            {
                case TreeNodeType.Category:
                    CommonNodeContract = customGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = customRoleContract;
                    break;
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

            TreeNodeType treeNodeType = GetNodeType(level);

            if (treeNodeType == TreeNodeType.Leaf)
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}
