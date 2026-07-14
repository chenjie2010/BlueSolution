//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ExceptionHelper.cs
// 描述: 异常处理装饰类
// 作者：ChenJie 
// 编写日期：2016-06-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 异常处理装饰类
    /// </summary>
    public sealed class ExceptionHelper
    {
        #region 常量

        private const string CONFIG_SOURCE_NAME = "ExceptionConfigSource";

        #endregion

        #region 私有变量
        
        private static readonly ExceptionManager exManager;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        static ExceptionHelper()
        {
            try
            {
                string configPath = ConfigFileOperation.GetConfigPath(CONFIG_SOURCE_NAME);
                if (!string.IsNullOrWhiteSpace(configPath))
                {
                    using (FileConfigurationSource configurationSource = new FileConfigurationSource(configPath))
                    {                       
                        LogWriterFactory logWriterFactory = new LogWriterFactory(configurationSource);
                        Logger.SetLogWriter(logWriterFactory.Create());
                        ExceptionPolicyFactory exceptionFactory = new ExceptionPolicyFactory(configurationSource);
                        exManager = exceptionFactory.CreateManager();
                    }
                }
                else
                {
                    throw new Exception("异常处理配置文件路径为空.");
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.WriteLog(exception.Message);
            }
        }

        #endregion

        #region 记录日志处理异常的方式

        /// <summary>
        /// 记录日志, 不抛出异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NoExceptionPolicyWithLog(Exception ex)
        {
            HandleCommonException(ex, "NoExceptionPolicyWithLog");            
        }

        /// <summary>
        /// 记录日志, 抛出异常(不包装异常)
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NotifyRethrowNoWrapPolicyWithLog(Exception ex)
        {
            HandleWrapException(ex, "NotifyRethrowNoWrapPolicyWithLog");            
        }

        #endregion


        #region 不记录日志处理异常的方式

        /// <summary>
        /// 不记录日志, 不抛出异常 (用在 WCF 层)
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NoExceptionPolicy(Exception ex)
        {
            HandleCommonException(ex, "NoExceptionPolicy");
        }

        /// <summary>
        /// 不记录日志, 抛出异常, 且不包装异常 (用在 AppFramework 层)
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NotifyRethrowNoWrapPolicy(Exception ex)
        {
            HandleCommonException(ex, "NotifyRethrowNoWrapPolicy");
        }

        /// <summary>
        /// 不记录日志, 抛出异常, 且包装异常（用在 DAL 层）
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void NotifyRethrowWrapPolicy(Exception ex)
        {
            HandleWrapException(ex, "ThrowNewExceptionPolicy");
        }

        /// <summary>
        /// 不记录日志, 抛出异常, 且替换异常 （用在 UI 层）
        /// </summary>
        /// <param name="ex"></param>
        public static void ThrowNewExceptionPolicy(SecurityException ex)
        {
            HandleReplaceException(ex, "ThrowNewExceptionPolicy");
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 常规异常处理
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="policy">异常策略名称</param>
        private static void HandleCommonException(Exception ex, string policy)
        {            
            bool rethrow = exManager.HandleException(ex, policy);
            if (rethrow)
            {
                throw ex;
            }
         }

        /// <summary>
        /// 包装异常处理
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="policy">异常策略名称</param>
        private static void HandleWrapException(Exception ex, string policy)
        {
            WrapException exceptionWrap = new WrapException(ex.Message, ex);
            bool rethrow = exManager.HandleException(ex, policy);
            if (rethrow)
            {
                throw exceptionWrap;
            }
        }

        /// <summary>
        /// 常规异常处理
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="policy">异常策略名称</param>
        private static void HandleReplaceException(Exception ex, string policy)
        {
            ReplaceException replaceException = new ReplaceException(ex.Message, ex);
            bool rethrow = exManager.HandleException(ex, policy);
            if (rethrow)
            {
                throw replaceException;
            }
        }

        #endregion
    }
}
