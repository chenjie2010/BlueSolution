//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserTypeTreeDropdownList .cs
// 描述: 用户类型下拉选择项操作基类 
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
    public class UserTypeTreeDropdownList : SimpleTreeDropdownBase, ITreeDropdownHandler
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
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
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
        public UserTypeTreeDropdownList(ICustomGroupContract customGroupContract, IUserTypeContract userTypeContract)
            : this(customGroupContract, userTypeContract, decimal.MinValue, false)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="rootNodeId"></param>
        public UserTypeTreeDropdownList(ICustomGroupContract customGroupContract, IUserTypeContract userTypeContract, decimal rootNodeId)
            : this(customGroupContract, userTypeContract, rootNodeId, false)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="parentNamesShowed"></param>
        public UserTypeTreeDropdownList(ICustomGroupContract customGroupContract, IUserTypeContract userTypeContract, bool parentNamesShowed)
            : this(customGroupContract, userTypeContract, decimal.MinValue, parentNamesShowed)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customGroupContract"></param>
        /// <param name="userTypeContract"></param>
        /// <param name="rootNodeId"></param>
        /// <param name="parentNamesShowed"></param>
        public UserTypeTreeDropdownList(ICustomGroupContract customGroupContract, IUserTypeContract userTypeContract, decimal rootNodeId, bool parentNamesShowed) 
            : base(customGroupContract, userTypeContract, rootNodeId, (byte)GroupType.UserType, parentNamesShowed)
        {
            CustomGroupContract = customGroupContract;
            UserTypeContract = userTypeContract;
            RootNodeId = rootNodeId;
            ParentNamesShowed = parentNamesShowed;
        }

        #endregion

        #region 抽象方法实现

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        protected override void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* 第一层为用户类型分类节点，第二层为用户类型节点 */
            switch (treeNodeType)
            {
                case TreeNodeType.Category:
                    CommonNodeContract = CustomGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = UserTypeContract;
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
