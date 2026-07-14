//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： VerificationCategory.cs
// 描述： 校验类型
// 作者：ChenJie 
// 编写日期：2019-02-01
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 校验类型
    /// </summary>
    [Description("校验类型")]
    public enum VerificationCategory
    {
        /// <summary>
        ///  用户信息
        /// </summary>
        [Description("用户信息")]
        UserInfo = 0,

        /// <summary>
        ///  枚举类型
        /// </summary>
        [Description("枚举类型")]
        Enum = 1,

        /// <summary>
        ///  关联类型
        /// </summary>
        [Description("关联类型")]
        Association = 2,

        /// <summary>
        ///  联动类型
        /// </summary>
        [Description("联动类型")]
        Relation = 3,

        /// <summary>
        ///  数据冗余类型
        /// </summary>
        [Description("数据冗余类型")]
        Redundancy = 4

    }
}
