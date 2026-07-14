//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PrintMultiSelectedItems.cs
// 描述: 打印多选操作
// 作者：ChenJie 
// 编写日期：2019-11-17
// 版权所有 (C) 四川大学 2019
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
    /// 打印多选操作
    /// </summary>
    public class PrintMultiSelectedItems : MultiSelectedItemsBase, IMultiSelectedItemsHandler
    {       
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomPrintContract customPrintContract;

        #endregion

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="customPrintContract"></param>
        /// <param name="nodeType"></param>
        /// <param name="onlySelectedLeaf"></param>
        public PrintMultiSelectedItems(ICustomGroupContract customGroupContract, ICustomPrintContract customPrintContract, byte nodeType, bool onlySelectedLeaf)
            : base(nodeType, onlySelectedLeaf)
        {
            CommonNodeContract = customGroupContract;
            QueriedCommonNodeContract = customPrintContract;
            this.customGroupContract = customGroupContract;
            this.customPrintContract = customPrintContract;
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

            /* (1) 打印分类 (2) 打印  */
            switch (level)
            {
                case 0:
                    treeNodeType = TreeNodeType.Category;
                    break;

                case 1:
                    treeNodeType = TreeNodeType.Leaf;
                    break;

                default:
                    throw new ArgumentException("不支持该打印节点类型。");
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
                    CommonNodeContract = customPrintContract;
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
