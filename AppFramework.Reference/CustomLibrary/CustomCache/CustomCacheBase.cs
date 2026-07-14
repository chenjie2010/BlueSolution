//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCache.cs
// 描述: 缓存类
// 作者：ChenJie 
// 编写日期：2016-07-18
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public class CustomCacheBase
    {
        #region 只读变量

        protected readonly ObjectCache userCache;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomCacheBase()
        {
            userCache = MemoryCache.Default;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 读取缓存对象
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns>缓存对象</returns>
        public object Read(string name)
        {
            return userCache.Get(name);
        }

        /// <summary>
        /// 通过缓存名称判断缓存对象是否存在
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns>存在返回 true, 否则返回 false</returns>
        public bool Contains(string name)
        {
            return userCache.Contains(name);
        }

        /// <summary>
        /// 缓存对象
        /// </summary>
        /// <param name="name">缓存名称</param>
        public void Remove(string name)
        {
            userCache.Remove(name);
        }

        ///// <summary>
        ///// 清除所有缓存
        ///// </summary>
        //public static void Clear()
        //{
        //    List<string> cacheKeys = userCache.Select(kvp => kvp.Key).ToList();
        //    foreach (string cacheKey in cacheKeys)
        //    {
        //        userCache.Remove(cacheKey);
        //    }
        //}

        #endregion
    }
}
