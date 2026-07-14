//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LoggingHelper.cs
// 描述: 操作日志记录类
// 作者：ChenJie 
// 编写日期：2016-06-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using AppFramework.Core;


namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 操作日志记录类
    /// </summary>
    public sealed class LoggingHelper
    {
        #region 常量

        /// <summary>
        /// 配置源
        /// </summary>
        private const string CONFIG_SOURCE_NAME = "LogConfigSource";

        /// <summary>
        /// XML
        /// </summary>
        private const string CATEGORY_XML_NAME = "Category";

        /// <summary>
        /// System
        /// </summary>
        private const string CATEGORY_SYS_NAME = "General";


        #endregion

        #region 私有变量

        private static readonly LogWriterFactory logWriterFactory;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        static LoggingHelper()
        {
            try
            {
                string configPath = ConfigFileOperation.GetConfigPath(CONFIG_SOURCE_NAME);
                if (!string.IsNullOrWhiteSpace(configPath))
                {
                    using (FileConfigurationSource configurationSource = new FileConfigurationSource(configPath))
                    {
                        logWriterFactory = new LogWriterFactory(configurationSource);                     
                    }
                }
                else
                {
                    throw new Exception("日志处理配置文件路径为空.");
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 写入 XML 文件

        /// <summary>
        /// 将日志写入到 XML 文件中
        /// </summary>
        /// <param name="message">内容</param>
        public static void WriteLog(string message)
        {
            WriteLog(0, 0, "默认标题", DateTime.Now, string.Empty, message, "默认分类");
        }

        /// <summary>
        /// 将日志写入到 XML 文件中
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        public static void WriteLog(string title, string message)
        {
            WriteLog(0, (int)Priority.Normal, title, DateTime.Now, string.Empty, message, "默认分类");
        }

        /// <summary>
        /// 将日志写入到 XML 文件中
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">内容</param>
        /// <param name="priority">级别</param>
        public static void WriteLog(string title, string message, Priority priority)
        {
            WriteLog(0, (int)priority, title, DateTime.Now, string.Empty, message, "默认分类");
        }


        /// <summary>
        /// 将日志写入到 XML 文件中
        /// </summary>
        /// <param name="eventId">事件号</param>
        /// <param name="priority">优先权</param>
        /// <param name="title">标题</param>
        /// <param name="timeStamp">时间</param>
        /// <param name="machineName">机器名称</param>
        /// <param name="message">写入信息</param>
        /// <param name="category">日志记录类别</param>
        public static void WriteLog(int eventId, int priority, string title, DateTime timeStamp, string machineName,
            string message, string category)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.EventId = eventId;
            logEntry.Priority = priority;
            logEntry.Title = title;
            logEntry.TimeStamp = timeStamp;
            logEntry.MachineName = machineName;
            logEntry.Message = message;
            logEntry.Categories.Add(category);        

            LogWriter logWriter = logWriterFactory.Create();
            logWriter.Write(logEntry, CATEGORY_XML_NAME);
        }

        /// <summary>
        /// 将日志写入到 XML 文件中
        /// </summary>
        /// <param name="eventId">事件号</param>
        /// <param name="priority">优先权</param>
        /// <param name="title">标题</param>
        /// <param name="timeStamp">时间</param>
        /// <param name="machineName">机器名称</param>
        /// <param name="message">写入信息</param>
        /// <param name="category">日志记录类别</param>
        /// <param name="extendedProperties">扩展属性</param>
        public static void WriteLog(int eventId, int priority, string title, DateTime timeStamp, string machineName,
            string message, string category, Dictionary<string, object> extendedProperties)
        {
            LogEntry logEntry = new LogEntry();
            logEntry.EventId = eventId;
            logEntry.Priority = priority;
            logEntry.Title = title;
            logEntry.TimeStamp = timeStamp;
            logEntry.MachineName = machineName;
            logEntry.Message = message;
            logEntry.ExtendedProperties = extendedProperties;
            logEntry.Categories.Add(category);

            LogWriter logWriter = logWriterFactory.Create();
            logWriter.Write(logEntry, CATEGORY_XML_NAME);
        }

        #endregion


    }
}
