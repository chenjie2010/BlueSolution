//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ClientUserSetting.cs
// 描述： 客户端用户设置
// 作者：ChenJie 
// 编写日期：2018-06-27
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 客户端用户设置
    /// </summary>
    [Description("客户端用户设置")]
    public enum ClientUserSetting
    {
        /// <summary>
        ///  邮件提示
        /// </summary>
        [Description("邮件提示")]
        Mail_Tip = 1,

        /// <summary>
        ///  工作流提示
        /// </summary>
        [Description("工作流提示")]
        Workflow_Tip = 2

    }
}
