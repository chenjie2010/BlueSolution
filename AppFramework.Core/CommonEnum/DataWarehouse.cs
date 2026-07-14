//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataWarehouse.cs
// 描述： 数据仓库枚举类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据仓库列表
    /// </summary>
    [Description("数据仓库列表|001|002|003|004|005")]
    public enum DataWarehouse
    {
        /// <summary>
        /// 数据仓库一
        /// </summary>
        [Description("数据仓库一")]
        FirstWarehouse = 1,

        /// <summary>
        /// 数据仓库二
        /// </summary>
        [Description("数据仓库二")]
        SecondWarehouse = 2,

        /// <summary>
        /// 数据仓库三
        /// </summary>
        [Description("数据仓库三")]
        ThirdWarehouse = 3,

        /// <summary>
        /// 数据仓库四
        /// </summary>
        [Description("数据仓库四")]
        FourthWarehouse = 4,

        /// <summary>
        /// 数据仓库五
        /// </summary>
        [Description("数据仓库五")]
        FifthWarehouse = 5
    }
}
