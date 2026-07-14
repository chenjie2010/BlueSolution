//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemTableHelper.cs
// 描述: 系统表帮助类
// 作者：ChenJie 
// 编写日期：2019/05/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace AppFramework.Reference.DataFieldLibrary
{
    /// <summary>
    /// 系统表帮助类
    /// </summary>
    public sealed class SystemTableHelper
    {
        /// <summary>
        /// 根据类型获得其名称
        /// </summary>
        /// <param name="systemTable"></param>
        /// <returns></returns>
        public static string GetSystemTablePhysicalName(SystemTable systemTable)
        {
            string systemTablePhysicalName = string.Empty;

            switch (systemTable)
            {
                case SystemTable.UserType:
                    systemTablePhysicalName = "UserType";
                    break;

                case SystemTable.Department:
                    systemTablePhysicalName = "CustomDepartment";
                    break;

                case SystemTable.User:
                    systemTablePhysicalName = "UserAccount";
                    break;

                case SystemTable.EnumType:
                    systemTablePhysicalName = "CustomEnum";
                    break;

                case SystemTable.Association:
                    systemTablePhysicalName = "CustomAssociation";
                    break;
            }

            return systemTablePhysicalName;
        }

        /// <summary>
        /// 根据类型获得其关键字名称
        /// </summary>
        /// <param name="systemTable"></param>
        /// <returns></returns>
        public static string GetSystemTableKeyName(SystemTable systemTable)
        {
            string systemTableKeyName = string.Empty;

            switch (systemTable)
            {
                case SystemTable.UserType:
                    systemTableKeyName = "UserTypeId";
                    break;

                case SystemTable.Department:
                    systemTableKeyName = "DepID";
                    break;

                case SystemTable.User:
                    systemTableKeyName = "UserId";
                    break;

                case SystemTable.EnumType:
                    systemTableKeyName = "EnumId";
                    break;

                case SystemTable.Association:
                    systemTableKeyName = "AssociationId";
                    break;
            }

            return systemTableKeyName;
        }
    }
}
