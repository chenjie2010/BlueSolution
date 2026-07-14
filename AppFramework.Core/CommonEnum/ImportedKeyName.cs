//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AbortedResult.cs
// 描述： 工作流终止结果
// 作者：ChenJie 
// 编写日期：2019-01-01
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 导入数据时的关键字名称
    /// </summary>
    [Description("导入数据时的关键字名称")]
    public enum ImportedKeyName
    {
        /// <summary>
        /// 以用户名为关键字
        /// </summary>
        [Description("以用户名为关键字")]
        UserName = 0,

        /// <summary>
        /// 以身份证号码为关键字
        /// </summary>
        [Description("以身份证号码为关键字")]
        UserIdetity = 1
    }
}
