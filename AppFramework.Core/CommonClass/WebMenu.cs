//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WebMenu.cs
// 描述： Web 菜单类
// 作者：ChenJie 
// 编写日期：2018/11/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AppFramework.Core
{
    /// <summary>
    /// Web 菜单类
    /// </summary>
    public class WebMenu
    {

        #region 属性

        /// <summary>
        /// 菜单编号
        /// </summary>
        public byte MenuId
        {
            get;
            set;
        }

        /// <summary>
        ///  菜单名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }

        /// <summary>
        ///  菜单图标名称
        /// </summary>
        public string MenuIconName
        {
            get;
            set;
        }

        public List<WebSubMenu> WebSubMenus
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public WebMenu()
        {
            WebSubMenus = new List<WebSubMenu>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="menuName"></param>
        /// <param name="menuIconName"></param>
        /// <param name="webSubMenus"></param>
        public WebMenu(byte menuId, string menuName, string menuIconName, List<WebSubMenu> webSubMenus)
        {
            MenuId = menuId;
            MenuName = menuName;
            MenuIconName = menuIconName;
            WebSubMenus = webSubMenus;
        }
        
        #endregion
        
    }
}
