//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableSetting.cs
// 描述： 表的设置
// 作者：ChenJie 
// 编写日期：2018-01-19
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表的设置
    /// </summary>
    [Description("表的设置")]
    public enum TableSetting
    {
        /// <summary>
        /// 删除用户时数据保留
        /// </summary>
        [Description("删除用户时数据保留")]
        DataReserved = 1, 

        /// <summary>
        /// 启用日志：所有记录修改记录到业务数据库中
        /// </summary>
        [Description("启用日志")]
        Log = 2
    }
}
