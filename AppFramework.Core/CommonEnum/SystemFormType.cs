//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemFormType.cs
// 描述： 系统表格类型
// 作者：ChenJie 
// 编写日期：2018-09-02
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统表格类型
    /// </summary>
    public enum SystemFormType
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 用户基本信息
        /// </summary>
        [Description("用户基本信息")]
        CommonUserInfo = 1
    }
}
