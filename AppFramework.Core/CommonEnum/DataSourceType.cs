//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataSourceType.cs
// 描述： 数据源类型
// 作者：ChenJie 
// 编写日期：2018-10-26
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据源类型
    /// </summary>
    public enum DataSourceType
    {
        /// <summary>
        ///  本地数据源
        /// </summary>
        [Description("本地数据源")]
        Local = 0,

        /// <summary>
        ///  远程数据源
        /// </summary>
        [Description("远程数据源")]
        Remote = 1

    }
}
