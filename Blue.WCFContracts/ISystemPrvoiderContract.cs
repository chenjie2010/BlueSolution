//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDuplexServiceCallBackContract.cs
// 描述: 系统服务回调契约
// 作者：ChenJie 
// 编写日期：2016-08-16
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AppFramework.Core;

namespace Blue.WCFContracts
{
    /// <summary>
    /// UserAccount 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserAccISystemPrvoiderContractountContract", Namespace = "http://www.scu.edu.cn/")]
    public interface ISystemPrvoiderContract
    {
        #region 自定义接口

        /// <summary>
        /// 获得服务器端时间
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetServerTime")]
        DateTime GetServerTime();

        /// <summary>
        /// 最后备份时间
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetLastestBackupTime")]
        string GetLastestBackupTime();

        /// <summary>
        /// 备份时的异常信息
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetBackupException")]
        string GetBackupException();

        /// <summary>
        /// 清除备份时的异常信息
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "ClearBackupException")]
        void ClearBackupException();

        /// <summary>
        /// 设置自动备份
        /// </summary>
        /// <param name="autoBackup"></param>
        /// <param name="backupDateTime"></param>
        /// <param name="backupPeriod"></param>
        /// <param name="range"></param>
        [OperationContract(Name = "ExecuteAutoBackupData")]
        void ExecuteAutoBackupData(bool autoBackup, DateTime backupDateTime, BackupPeriod backupPeriod, long range);

        #endregion
    }
}
