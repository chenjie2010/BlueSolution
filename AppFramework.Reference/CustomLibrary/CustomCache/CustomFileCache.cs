//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: FileCache.cs
// 描述: 文件方式缓存类
// 作者：ChenJie 
// 编写日期：2016-07-18
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Caching;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 文件方式缓存类
    /// </summary>
    public sealed class CustomFileCache : CustomCacheBase
    {
        #region 私有变量

        /// <summary>
        /// 依赖文件路径
        /// </summary>
        private readonly string filePath;

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
        /// 构造函数
        /// </summary>
        /// <param name="filePath">依赖文件路径</param>
        public CustomFileCache(string filePath)
        {
            this.filePath = filePath;
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
            CacheItemPolicy userCacheItemPolicy = GetCacheItemPolicy(filePath);
            return userCache.Add(name, cacheData, userCacheItemPolicy);
        }

        /// <summary>
        /// 添加缓存对象 
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <param name="cacheData">缓存对象</param>
        /// <param name="filePath">依赖文件路径</param>
        public void Set(string name, object cacheData)
        {
            CacheItemPolicy userCacheItemPolicy = GetCacheItemPolicy(filePath);
            userCache.Set(name, cacheData, userCacheItemPolicy);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得缓存依赖策略
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private CacheItemPolicy GetCacheItemPolicy(string filePath)
        {
            CacheItemPolicy userCacheItemPolicy = new CacheItemPolicy();

            IList<string> filePaths = new List<string>() { filePath };
            userCacheItemPolicy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

            return userCacheItemPolicy;
        }
        
        #endregion
    }
}
