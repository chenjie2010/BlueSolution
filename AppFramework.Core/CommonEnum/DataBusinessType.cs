//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataBusinessType.cs
// 描述： 数据业务类型
// 作者：ChenJie 
// 编写日期：2018/12/14
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据业务类型
    /// </summary>
    [Description("数据业务类型")]
    public enum DataBusinessType
    {
        /// <summary>
        /// 个人信息填报
        /// </summary>
        [Description("个人信息填报")]
        PersonalInfoFilled = 0,

        /// <summary>
        /// 个人信息审核
        /// </summary>
        [Description("个人信息审核")]
        PersonalInfoAudited = 1
    }
}
