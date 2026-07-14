//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RoleAuthority.cs
// 描述： 角色权限
// 作者：ChenJie 
// 编写日期：2018-09-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RoleAuthority
    {
        #region 定义私有变量

        private Int64 _roleProperty;
        private Int64 _menuAuthority;
        private Int64 _menuSubAuthority;
        private Int64 _systemAuthority;
        private Int64 _systemSubAuthority;        

        #endregion

        #region 属性

        /// <summary>
        /// 角色属性
        /// </summary>
        public Int64 RoleProperty
        {
            get
            {
                return _roleProperty;
            }
            set
            {
                _roleProperty = value;
            }
        }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public Int64 MenuAuthority
        {
            get
            {
                return _menuAuthority;
            }
            set
            {
                _menuAuthority = value;
            }
        }

        /// <summary>
        /// 菜单子权限
        /// </summary>
        public Int64 MenuSubAuthority
        {
            get
            {
                return _menuSubAuthority;
            }
            set
            {
                _menuSubAuthority = value;
            }
        }

        /// <summary>
        /// 键
        /// </summary>
        public Int64 SystemAuthority
        {
            get
            {
                return _systemAuthority;
            }
            set
            {
                _systemAuthority = value;
            }
        }

        /// <summary>
        /// 系统子权限
        /// </summary>
        public Int64 SystemSubAuthority
        {
            get
            {
                return _systemSubAuthority;
            }
            set
            {
                _systemSubAuthority = value;
            }
        }

        #endregion

        #region 构造函数        

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RoleAuthority()
        {
            _roleProperty = 0;
            _menuAuthority = 0;
            _menuSubAuthority = 0;
            _systemAuthority = 0;
            _systemSubAuthority = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleProperty"></param>
        /// <param name="menuAuthority"></param>
        /// <param name="menuSubAuthority"></param>
        /// <param name="systemAuthority"></param>
        /// <param name="systemSubAuthority"></param>
        public RoleAuthority(Int64 roleProperty, Int64 menuAuthority, Int64 menuSubAuthority, Int64 systemAuthority, Int64 systemSubAuthority)
        {
            _roleProperty = roleProperty;
            _menuAuthority = menuAuthority;
            _menuSubAuthority = menuSubAuthority;
            _systemAuthority = systemAuthority;
            _systemSubAuthority = systemSubAuthority;
        }

        #endregion

        #region 重载方法

       
        #endregion
    }
}
