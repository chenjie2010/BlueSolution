//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CurrentDataBackup.cs
// 描述: 保存当前用户全局信息
// 作者：ChenJie 
// 编写日期：2018-05-19
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.BusinessLogic;
using Blue.BusinessLogic.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.DataCommutation
{
    /// <summary>
    /// 保存当前数据自动备份全局信息，属于单件模式（Singleton Pattern）
    /// </summary>
    public class CurrentDataBackup
    {
        #region 常量

        /// <summary>
        /// 最大重试次数
        /// </summary>
        private const int MAX_RETRY_TIMES = 10;

        /// <summary>
        /// 每个时间间隔单位为10分钟
        /// </summary>
        private const int TIME_SPAN_PER_RETRY = 1000 * 60 * 10;

        #endregion


        #region 内部成员变量

        private Timer timerDataBackup;
        private UserMessageHandler userMessageHandler = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        CurrentDataBackup()
        {
            userMessageHandler = new UserMessageHandler();
        }

        #endregion

        #region 嵌套类

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly CurrentDataBackup instance = new CurrentDataBackup();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static CurrentDataBackup Instance
        {
            get
            {
                return Nested.instance;
            }
        }
                
        #endregion

        #region 公有方法

        /// <summary>
        /// 执行数据自动备份
        /// </summary>
        /// <param name="autoBackup"></param>
        /// <param name="backupDateTime"></param>
        /// <param name="backupPeriod"></param>
        /// <param name="range"></param>
        public void ExecuteAutoBackupData(bool autoBackup, DateTime backupDateTime, BackupPeriod backupPeriod, long range)
        {
            if (!autoBackup)
            {
                if (timerDataBackup != null)
                {
                    timerDataBackup.Dispose();
                    timerDataBackup = null;
                }                
                return;
            }
            /* 执行 */
            TimeSpan newPeriod = new TimeSpan(1, 0, 0, 0, 0);
            switch (backupPeriod)
            {
                case BackupPeriod.OneDay:
                    newPeriod = new TimeSpan(1, 0, 0, 0, 0);
                    break;

                case BackupPeriod.TwoDays:
                    newPeriod = new TimeSpan(2, 0, 0, 0, 0);
                    break;

                case BackupPeriod.ThreeDays:
                    newPeriod = new TimeSpan(3, 0, 0, 0, 0);
                    break;

                case BackupPeriod.OneWeek:
                    newPeriod = new TimeSpan(7, 0, 0, 0, 0);
                    break;

                case BackupPeriod.TwoWeeks:
                    newPeriod = new TimeSpan(14, 0, 0, 0, 0);
                    break;

                case BackupPeriod.ThreeWeeks:
                    newPeriod = new TimeSpan(21, 0, 0, 0, 0);
                    break;

                case BackupPeriod.OneMonth:
                    newPeriod = new TimeSpan(30, 0, 0, 0, 0);
                    break;

                case BackupPeriod.FiveWeeks:
                    newPeriod = new TimeSpan(36, 0, 0, 0, 0);
                    break;

                case BackupPeriod.SixWeeks:
                    newPeriod = new TimeSpan(42, 0, 0, 0, 0);
                    break;

                default:
                    throw new ArgumentException("不支持该枚举。");

            }
            while (backupDateTime <= DateTime.Now)
            {
                backupDateTime += newPeriod;
            }
            TimeSpan dueTime = backupDateTime - DateTime.Now;
            DataAutoBackup dataAutoBackup = new DataAutoBackup();
            timerDataBackup = new Timer((o) =>
            {
                bool retry = false;
                int count = 0;
                AppSettingHelper.BackupException = string.Empty;
                do
                {
                    try
                    {
                        bool result = CommonBusinessHelper.TestDatabaseConnection();
                        if (result)
                        {
                            try
                            {
                                dataAutoBackup.Backup(range);
                            }
                            catch (Exception ex)
                            {
                                AppSettingHelper.BackupException = ex.Message;
                                UserMessageInfo userMessageInfo = new UserMessageInfo(decimal.MinValue, 0, "系统消息：数据库备份异常。", ex.Message, Convert.ToByte(MessageType.SystemMessage),
                                    false, false, 1, DateTime.Now, DateTime.Now.AddMonths(12), DateTime.Now);
                                userMessageHandler.InsertUserMessage(userMessageInfo, AttachmentCategory.Message, new List<ExtendedUpLoadFileInfo>(), new List<decimal>());
                            }
                        }
                        else
                        {
                            retry = true;
                            count++;
                            string message = string.Format("数据库连接失败，重试{0}次失败。", count);
                            AppSettingHelper.BackupException = message;
                            Thread.Sleep(TIME_SPAN_PER_RETRY * count);
                        }
                    }
                    catch
                    { }
                } while (retry && count <= MAX_RETRY_TIMES);
                AppSettingHelper.LastestBackupTime = DateTime.Now.ToString("G");
            }, null, dueTime, newPeriod);
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
