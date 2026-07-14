//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemPrvoiderService.cs
// 描述: 提供软件相关信息
// 作者：ChenJie 
// 编写日期：2016-08-16
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.ServiceModel;
using System.Collections.Generic;
using Blue.WCFContracts;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DataCommutation;

namespace Blue.WCFServices
{
    /// <summary>
    /// 提供软件相关信息，需要验证用户名和密码
    /// </summary>
    public class SystemPrvoiderService: ISystemPrvoiderContract
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemPrvoiderService()
        {

        }

        #endregion

        #region 实现契约接口

        /// <summary>
        /// 获得服务器当前时间
        /// </summary>
        public DateTime GetServerTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 最后备份时间
        /// </summary>
        /// <returns></returns>
        public string GetLastestBackupTime()
        {
            return AppSettingHelper.LastestBackupTime;
        }

        /// <summary>
        /// 备份时的异常信息
        /// </summary>
        /// <returns></returns>
        public string GetBackupException()
        {
            return AppSettingHelper.BackupException;
        }

        /// <summary>
        /// 清除备份时的异常信息
        /// </summary>
        /// <returns></returns>
        public void ClearBackupException()
        {
            AppSettingHelper.BackupException = string.Empty;
        }


        /// <summary>
        /// 设置自动备份
        /// </summary>
        /// <param name="autoBackup"></param>
        /// <param name="backupDateTime"></param>
        /// <param name="backupPeriod"></param>
        /// <param name="range"></param>
        public void ExecuteAutoBackupData(bool autoBackup, DateTime backupDateTime, BackupPeriod backupPeriod, long range)
        {
            CurrentDataBackup.Instance.ExecuteAutoBackupData(autoBackup, backupDateTime, backupPeriod, range);
        }

        #endregion
    }
}
