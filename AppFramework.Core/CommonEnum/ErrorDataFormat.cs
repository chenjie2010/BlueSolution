//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DatabaseType.cs
// 描述： 数据库版本类型枚举
// 作者：ChenJie 
// 编写日期：2016-01-01
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 错误的数据格式
    /// </summary>
    [Description("错误的数据格式")]
    public enum ErrorDataFormat
    {
        /// <summary>
        ///  无错误信息
        /// </summary>
        [Description("无错误信息")]
        None,

        /// <summary>
        ///  该用户名不存在
        /// </summary>
        [Description("该用户名不存在")]
        NotExistedUser,

        /// <summary>
        ///  该用户数据重复存在
        /// </summary>
        [Description("该用户数据重复存在")]
        DuplicateUser,

        /// <summary>
        ///  该单位名称不存在
        /// </summary>
        [Description("该单位名称不存在")]
        NotExistedDepName,

        /// <summary>
        ///  该单位附加一名称不存在
        /// </summary>
        [Description("该单位附加一名称不存在")]
        NotExistedFirstDepCode,

        /// <summary>
        ///  该单位附加二名称不存在
        /// </summary>
        [Description("该单位附加二名称不存在")]
        NotExistedSecondDepCode,

        /// <summary>
        ///  该用户名不属于当前用户管理范围
        /// </summary>
        [Description("该用户名不属于当前用户管理范围")]
        InValidUser,

        /// <summary>
        ///  不能为空
        /// </summary>
        [Description("不能为空")]
        UnNull,

        /// <summary>
        ///  只能为0，1或是 True 或是 False
        /// </summary>
        [Description("只能为0，1或是 True 或是 False")]
        BoolError,

        /// <summary>
        ///  实数错误
        /// </summary>
        [Description("实数错误")]
        DecimalNumberError,

        /// <summary>
        ///  整型错误
        /// </summary>
        [Description("整型错误")]
        IntError,

        /// <summary>
        ///  时间错误
        /// </summary>
        [Description("时间错误")]
        DateTimeError,

        /// <summary>
        ///  数字型格式字符串错误
        /// </summary>
        [Description("数字型格式字符串错误")]
        NumeralStringError,

        /// <summary>
        ///  字母型字符串错误
        /// </summary>
        [Description("字母型字符串错误")]
        CharStringError,

        /// <summary>
        ///  字母数字混合型字符串格式错误
        /// </summary>
        [Description("字母数字混合型字符串格式错误")]
        MixedStringError,

        /// <summary>
        ///  字符串长度错误
        /// </summary>
        [Description("字符串长度错误")]
        StringLengthError,

        /// <summary>
        /// 枚举类型错误
        /// </summary>
        [Description("枚举类型错误")]
        EnumError,

        /// <summary>
        /// 关联不存在
        /// </summary>
        [Description("关联不存在")]
        AssocationError,

        /// <summary>
        ///  数据已存在
        /// </summary>
        [Description("数据已存在")]
        ExistedData,

        /// <summary>
        ///  数据不存在
        /// </summary>
        [Description("数据不存在")]
        NotExistedData,
        
        /// <summary>
        ///  未知错误
        /// </summary>
        [Description("未知错误")]
        UnKownError,
    }
}
