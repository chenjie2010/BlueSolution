//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCache.cs
// 描述: 内存方式缓存类
// 作者：ChenJie 
// 编写日期：2016-07-18
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 内存方式缓存类
    /// </summary>
    public sealed class CustomMemoryCache : CustomCacheBase
    {
        #region 私有变量

        /// <summary>
        /// 缓存保留的时间
        /// </summary>
        private readonly int hours;

        #endregion

        #region 属性

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public object this[string index]
        {
            get
            {
                return Read(index);
            }
            set
            {
                Set(index, value);
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数，默认保留2个小时
        /// </summary>
        public CustomMemoryCache()
        {
            this.hours = 2;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hours">缓存保留的时间</param>
        public CustomMemoryCache(int hours)
        {
            this.hours = hours;
        }

        #endregion


        #region 公有方法
        /// <summary>
        /// 添加缓存对象 
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <param name="cacheData">缓存对象</param>
        /// <returns>成功返回 true, 否则返回 false</returns>
        public bool Add(string name, object cacheData)
        {
            CacheItemPolicy userCacheItemPolicy = GetCacheItemPolicy();
            return userCache.Add(name, cacheData, userCacheItemPolicy);
        }

        /// <summary>
        /// 添加缓存对象 
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <param name="cacheData">缓存对象</param>
        public void Set(string name, object cacheData)
        {
            CacheItemPolicy userCacheItemPolicy = GetCacheItemPolicy();
            userCache.Set(name, cacheData, userCacheItemPolicy);
        }

        #endregion

        #region 私有静态方法

        /// <summary>
        /// 获得缓存依赖
        /// </summary>
        /// <returns></returns>
        private CacheItemPolicy GetCacheItemPolicy()
        {
            CacheItemPolicy userCacheItemPolicy = new CacheItemPolicy();
            userCacheItemPolicy.Priority = CacheItemPriority.Default;
            userCacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(hours);
            //policy.RemovedCallback += delegate (CacheEntryRemovedArguments arguments)
            //{
            //    CacheItem cacheItem = arguments.CacheItem;
            //};

            return userCacheItemPolicy;
        }

        #endregion
       
    }
}
