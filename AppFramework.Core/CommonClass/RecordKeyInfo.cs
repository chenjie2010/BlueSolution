//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RecordKeyInfo.cs
// 描述： 记录关键字信息
// 作者：ChenJie 
// 编写日期：2018/02/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用用户信息
    /// </summary>
    [Serializable]
    public class RecordKeyInfo
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RecordKeyInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="depId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="businessId"></param>
        /// <param name="businessForeignId"></param>
        /// <param name="businessAlternativeId"></param>
        /// <param name="recordSorting"></param>
        /// <param name="auditedStatus"></param>
        /// <param name="currentState"></param>
        /// <param name="creationTime"></param>
        /// <param name="modificationTime"></param>
        /// <param name="modifiedByUserName"></param>
        /// <param name="isDeleted"></param>
        public RecordKeyInfo(decimal userId, string userName, decimal depId, decimal userTypeId, decimal businessId, decimal businessForeignId, decimal businessAlternativeId, 
            int recordSorting, byte auditedStatus, byte currentState, DateTime creationTime, DateTime modificationTime, string modifiedByUserName, bool isDeleted)
        {
            UserId = userId;
            UserName = userName;
            DepId = depId;
            UserTypeId = userTypeId;
            BusinessId = businessId;
            BusinessForeignId = businessForeignId;
            BusinessAlternativeId = businessAlternativeId;
            RecordSorting = recordSorting;
            AuditedStatus = auditedStatus;
            CurrentState = currentState;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
            ModifiedByUserName = modifiedByUserName;
            IsDeleted = isDeleted;
        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型编号
        /// </summary>
        public decimal UserTypeId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户单位编号
        /// </summary>
        public decimal DepId
        {
            get;
            set;
        }

        public decimal BusinessId
        {
            get;
            set;
        }

        public decimal BusinessForeignId
        {
            get;
            set;
        }

        public decimal BusinessAlternativeId
        {
            get;
            set;
        }

        public int RecordSorting
        {
            get;
            set;
        }

        public byte AuditedStatus
        {
            get;
            set;
        }

        public byte CurrentState
        {
            get;
            set;
        }

        public DateTime CreationTime
        {
            get;
            set;
        }

        public DateTime ModificationTime
        {
            get;
            set;
        }

        public string ModifiedByUserName
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        #endregion
    }
}
