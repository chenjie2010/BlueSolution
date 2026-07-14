//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WebSubMenu.cs
// 描述： Web 子菜单类
// 作者：ChenJie 
// 编写日期：2018/11/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// Web 子菜单类
    /// </summary>
    public class WebSubMenu
    {
        #region 属性

        /// <summary>
        /// 子菜单编号
        /// </summary>
        public decimal SubMenuId
        {
            get;
            set;
        }

        /// <summary>
        ///  子菜单名称
        /// </summary>
        public string SubMenuName
        {
            get;
            set;
        }

        /// <summary>
        ///  子菜单图标名称
        /// </summary>
        public string SubMenuIconName
        {
            get;
            set;
        }

        /// <summary>
        ///  子菜单地址
        /// </summary>
        public string SubMenuURL
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WebSubMenu()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="subMenuId"></param>
        /// <param name="subMenuName"></param>
        /// <param name="subMenuIconName"></param>
        /// <param name="subMenuURL"></param>
        public WebSubMenu(decimal subMenuId, string subMenuName, string subMenuIconName, string subMenuURL)
        {
            SubMenuId = subMenuId;
            SubMenuName = subMenuName;
            SubMenuIconName = subMenuIconName;
            SubMenuURL = subMenuURL;
        }
        
        #endregion

        #region 重载方法
        
        #endregion
    }
}
