//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleHelper.cs
// 描述: 角色帮助类
// 作者：ChenJie 
// 编写日期：2018/01/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.Model.BusinessModule;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 角色帮助类
    /// </summary>
    public sealed class RoleHelper
    {
        #region 公有静态方法

        /// <summary>
        /// 是否包含角色自定义权限
        /// </summary>
        /// <param name="rolePropertyValue"></param>
        /// <param name="roleProperty"></param>
        /// <returns></returns>
        public static bool ContainsRoleAuthority(Int64 rolePropertyValue, RoleProperty roleProperty)
        {
            byte pos = Convert.ToByte(roleProperty);

            bool result = false;
            if (pos > 0)
            {
                result = ((rolePropertyValue >> pos) & 1L) == 1L;
            }
            else
            {
                result = (rolePropertyValue & 1L) == 1L;
            }

            return result;            
        }

        #endregion
    }
}
