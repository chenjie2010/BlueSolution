//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LogHelper.cs
// 描述: 数据库日志记录模块
// 作者：ChenJie 
// 编写日期：2016-08-23
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFramework.Core;
using Blue.WCFContracts.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        #region 契约接口

        private static readonly IUserLogContract userLogContract;
        private static readonly bool writeLog = false;
        private static readonly int systemLogLevel = 1;

        #endregion
        
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static LogHelper()
        {
            userLogContract = SystemChannelFactory.CreateUserLogContract();
            ISystemConfigContract systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            string enableLog =  systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.EnableLog);

            if (!string.IsNullOrWhiteSpace(enableLog))
            {
                writeLog = Convert.ToBoolean(enableLog);
            }
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logTitle"></param>
        /// <param name="logAction"></param>
        /// <param name="decsription"></param>
        public static void WriteLog(LogTitle logTitle, LogAction logAction, string decsription)
        {
            if (!writeLog || systemLogLevel > (int)LogLevel.Info)
            {
                return;
            }
            UserLogInfo userLogInfo = new UserLogInfo(0, CurrentUser.Instance.UserId,
                (byte)PlatformState.WinForms, decsription, 0, (byte)logAction, (byte)LogLevel.Info, DateTime.Now);
            userLogContract.Insert(userLogInfo);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logTitle"></param>
        /// <param name="logAction"></param>
        /// <param name="logLevel"></param>
        /// <param name="decsription"></param>
        public static void WriteLog(LogTitle logTitle, LogAction logAction, LogLevel logLevel, string decsription)
        {
            if (!writeLog || systemLogLevel > (int)logLevel)
            {
                return;
            }
            UserLogInfo userLogInfo = new UserLogInfo(0, CurrentUser.Instance.UserId,
                (byte)PlatformState.WinForms, decsription, 0, (byte)logAction, (byte)logLevel, DateTime.Now);
            userLogContract.Insert(userLogInfo);
        }

        #endregion
    }
}
