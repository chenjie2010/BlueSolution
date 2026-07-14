//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AccessResult.cs
// 描述： 单点登录访问结果
// 作者：ChenJie 
// 编写日期：2019/05/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 单点登录访问结果
    /// </summary>
    public class AccessResult
    {
        #region 属性

        public bool Success
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccessResult()
        {
            Success = false;
            ErrorMessage = string.Empty;
            UserName = string.Empty;
        }

        #endregion
    }
}
