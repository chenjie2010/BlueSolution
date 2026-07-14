//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： LogonSteps.cs
// 描述： 登录步骤枚举
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 登录步骤枚举
    /// </summary>
    [Description("登录步骤枚举")]
    public enum LoginedStep
    {
        /// <summary>
        /// 测试网络连接
        /// </summary>
        [Description("测试网络连接")]
        TestConnection = 0,

        /// <summary>
        /// 检查版本一致性
        /// </summary>
        [Description("检查版本一致性")]
        CheckVersionConsistency = 1,

        /// <summary>
        /// 检查计算机时间
        /// </summary>
        [Description("检查计算机时间")]
        CheckSystemTime = 2,

        /// <summary>
        /// 验证用户身份
        /// </summary>
        [Description("验证用户身份")]
        Validator = 3,

        /// <summary>
        /// 加载应用程序
        /// </summary>
        [Description("加载应用程序")]
        Load = 4
    }
}
