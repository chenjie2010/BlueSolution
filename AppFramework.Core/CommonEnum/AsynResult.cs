//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AsynResult.cs
// 描述： 异步调用的返回值的枚举
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{

    /// <summary>
    /// 异步调用的返回值的枚举
    /// </summary>
    public enum AsynResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 2
    }
}
