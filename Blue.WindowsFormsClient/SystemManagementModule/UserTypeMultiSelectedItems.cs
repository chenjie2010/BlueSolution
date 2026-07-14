//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserTypeMultiSelectedItems.cs
// 描述: UserTypeMultiSelectedItems
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
    /// 用户类型多选操作
    /// </summary>
    public class UserTypeMultiSelectedItems : MultiSelectedItemsBase, IMultiSelectedItemsHandler
    {

        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;

        #endregion       

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="nodeType"></param>
        /// <param name="onlySelectedLeaf"></param>
        public UserTypeMultiSelectedItems(ICustomGroupContract customGroupContract, IUserTypeContract userTypeContract, byte nodeType, bool onlySelectedLeaf) 
            : base(nodeType, onlySelectedLeaf)
        {
            CommonNodeContract = customGroupContract;
            QueriedCommonNodeContract = userTypeContract;
            this.customGroupContract = customGroupContract;
            this.userTypeContract = userTypeContract;
        }

        #endregion
      

        #region  受保护方法

        /// <summary>
        /// 获得用户类型节点的类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override TreeNodeType GetNodeType(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            /* (1) 用户类型分类节点 (2) 用户类型  */
            switch (level)
            {
                case 0:
                    treeNodeType = TreeNodeType.Category;
                    break;

                case 1:
                    treeNodeType = TreeNodeType.Leaf;
                    break;

                default:
                    throw new ArgumentException("不支持该用户类型节点。");
            }

            return treeNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected override void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* (1) 用户类型分类节点 (2) 用户类型  */
            switch (treeNodeType)
            {
                case TreeNodeType.Category:
                    CommonNodeContract = customGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = userTypeContract;
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
                return result = false;
            }

            return result;
        }


        #endregion
    }
}
