//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomModuleFactory.cs
// 描述: 数据访问层的工厂类的父类
// 作者：ChenJie 
// 编写日期：2010-07-08
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Web.Caching;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.Utility;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 数据访问层工厂类的父类
    /// </summary>
    public class DynamicObjCreater
    {
        #region 常量

        /// <summary>
        /// 缓存域的名称
        /// </summary>
        private const String CACHE_NAME = "DynamicObjCreater";
       

        #endregion

        #region 只读变量
       
        private static readonly object instance;
        private static readonly CustomFileCache fileCache;
        #endregion

        #region 静态构造函数

        static DynamicObjCreater()
        {
            /* Web Froms 和 Win Forms 程序集缓存的方式不一样
             * Web Froms 采用系统自带的缓存机制：System.Web.HttpContext.Current.Cache
             * Win Forms 采用自定义的缓存机制
            */
            if (Platform.PlatformState != PlatformState.WebForms)
            {
                fileCache = new CustomFileCache(AppSettingHelper.AppSettingFullFileName);
            }
            instance = new object();
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 为数据层模块创建对象并缓存
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyPath, string className)
        {
            return CreateInstance(assemblyPath, string.Empty, className);
        }

        /// <summary>
        /// 为数据层模块创建对象并缓存
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="nameSpace"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyPath, string nameSpace, string className)
        {
            object obj = null;

            try
            {
                lock (instance)
                {
                    //Web Froms 和 Win Forms 的程序集缓存的方式不一样 
                    if (Platform.PlatformState == PlatformState.WebForms)
                    {
                        if (System.Web.HttpContext.Current != null)
                        {
                            obj = System.Web.HttpContext.Current.Cache[className];
                        }
                    }
                    else
                    {
                        obj = fileCache[className];
                    }
                    if (obj == null)
                    {
                        string fullClassName = AssemblyLocator.GetAssemblyPath(assemblyPath, nameSpace, className);
                        obj = Assembly.Load(assemblyPath).CreateInstance(fullClassName);
                        //Type type = Type.GetType(fullClassName);
                        //使用与指定参数匹配程度最高的构造函数来创建指定类型的实例。
                        //Activator.CreateInstance(type, new string[]{"a", "n"});
                        if (Platform.PlatformState == PlatformState.WebForms)
                        {
                            if (System.Web.HttpContext.Current != null)
                            {
                                CacheDependency cacheDependency = new CacheDependency(AppSettingHelper.AppSettingFullFileName);
                                System.Web.HttpContext.Current.Cache.Insert(className, obj, cacheDependency);
                            }
                        }
                        else
                        {
                            fileCache[className] = obj;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicyWithLog(exception);
            }

            return obj;
        }

        #endregion
    }
}
