//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CacheLoader.cs
// 描述: 缓存预加载类
// 作者：ChenJie 
// 编写日期：2016/08/26
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using Blue.SQLServerDAL;

namespace Blue.BusinessLogic
{
    /// <summary>
    /// 缓存预加载类
    /// </summary>
    public sealed class CacheLoader
    {
        #region 缓存预加载方法

        /// <summary>
        /// 加载数据库列字段的说明
        /// </summary>
        public static void LoadColumnCaption()
        {
            ColumnCaptionHelper.LoadColumnCaption();
        }

        #endregion
    }
}
