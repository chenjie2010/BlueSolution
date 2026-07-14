//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AdditionalRecordType.cs
// 描述： 记录的附加数据类型
// 作者：ChenJie 
// 编写日期：2018-09-01
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 记录的附加数据类型
    /// </summary>
    public enum AdditionalRecordType
    {
        /// <summary>
        /// 业务触发器
        /// </summary>
        [Description("业务触发器")]
        Trigger = 0,

        /// <summary>
        /// 动态单位节点数据
        /// </summary>
        [Description("动态单位节点数据")]
        DnamicalDep = 1
    }
}
