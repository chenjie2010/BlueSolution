//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： Priority.cs
// 描述： 数据库日志优先级
// 作者：ChenJie 
// 编写日期：2016-07-02
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据库日志优先级
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// 最低级别
        /// </summary>
        Lowest,

        /// <summary>
        /// 低级别
        /// </summary>
        Low,

        /// <summary>
        /// 正常级别
        /// </summary>
        Normal,

        /// <summary>
        /// 高级别
        /// </summary>
        High,

        /// <summary>
        /// 最高级别
        /// </summary>
        Highest
    }
}
