//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BackupDataRange.cs
// 描述： 备份数据范围
// 作者：ChenJie 
// 编写日期：2019/05/18
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 备份数据范围
    /// </summary>
    [Description("备份数据范围")]
    public enum BackupDataRange
    {
        /// <summary>
        /// 系统数据
        /// </summary>
        [Description("系统数据")]
        System = 0,

        /// <summary>
        /// 用户照片
        /// </summary>
        [Description("用户照片")]
        UserPhoto = 1,

        /// <summary>
        /// 报表模板文件
        /// </summary>
        [Description("报表模板文件")]
        ReportTemplate = 2,

        /// <summary>
        /// 数据仓库一
        /// </summary>
        [Description("数据仓库一")]
        First = 11,

        /// <summary>
        /// 数据仓库二
        /// </summary>
        [Description("数据仓库二")]
        Second = 15,

        /// <summary>
        /// 数据仓库三
        /// </summary>
        [Description("数据仓库三")]
        Third = 20,

        /// <summary>
        /// 数据仓库四
        /// </summary>
        [Description("数据仓库四")]
        Fourth = 25,

        /// <summary>
        /// 数据仓库五
        /// </summary>
        [Description("数据仓库五")]
        Fifth = 30

    }
}
