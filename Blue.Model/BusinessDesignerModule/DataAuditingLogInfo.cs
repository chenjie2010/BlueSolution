//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingLogInfo.cs
// 描述: DataAuditingLogInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/12/17
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
    /// <summary>
    /// <para>DataAuditingLogInfo 类</para>
    /// <para>数据审核日志</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class DataAuditingLogInfo
    {
        #region 内部成员变量

        private decimal _auditingLogId;
        private decimal _dataAuditingId;
        private decimal _userId;
        private decimal _parentUserId;
        private string _auditingLogName = string.Empty;
        private byte _auditingLogType;
        private byte _auditingStatus;
        private DateTime _auditingLogTime;
        private string _logDescription = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditingLogInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="auditingLogId">审核日志编号</param>
        ///<param name="dataAuditingId"></param>
        ///<param name="userId">用户编号</param>
        ///<param name="parentUserId">用户编号</param>
        ///<param name="auditingLogName">审核日志名称</param>
        ///<param name="auditingLogType">审核日志类型</param>
        ///<param name="auditingStatus">审核日志状态</param>
        ///<param name="auditingLogTime">审核日志时间</param>
        ///<param name="logDescription">审核日志描述</param>
        public DataAuditingLogInfo(decimal auditingLogId, decimal dataAuditingId, decimal userId, decimal parentUserId, string auditingLogName,
            byte auditingLogType, byte auditingStatus, DateTime auditingLogTime, string logDescription)
        {
            _auditingLogId = auditingLogId;
            _dataAuditingId = dataAuditingId;
            _userId = userId;
            _parentUserId = parentUserId;
            _auditingLogName = auditingLogName;
            _auditingLogType = auditingLogType;
            _auditingStatus = auditingStatus;
            _auditingLogTime = auditingLogTime;
            _logDescription = logDescription;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 审核日志编号
        /// </summary>
        public decimal AuditingLogId
        {
            get
            {
                return _auditingLogId;
            }
            set
            {
                if (_auditingLogId == value)
                    return;
                _auditingLogId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal DataAuditingId
        {
            get
            {
                return _dataAuditingId;
            }
            set
            {
                if (_dataAuditingId == value)
                    return;
                _dataAuditingId = value;
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId == value)
                    return;
                _userId = value;
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal ParentUserId
        {
            get
            {
                return _parentUserId;
            }
            set
            {
                if (_parentUserId == value)
                    return;
                _parentUserId = value;
            }
        }

        /// <summary>
        /// 审核日志名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 审核日志名称不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "审核日志名称长度范围在1位～128位！")]
        public string AuditingLogName
        {
            get
            {
                return _auditingLogName;
            }
            set
            {
                if (_auditingLogName == value)
                    return;
                _auditingLogName = value;
            }
        }

        /// <summary>
        /// 审核日志类型
        /// </summary>
        public byte AuditingLogType
        {
            get
            {
                return _auditingLogType;
            }
            set
            {
                if (_auditingLogType == value)
                    return;
                _auditingLogType = value;
            }
        }

        /// <summary>
        /// 审核日志状态
        /// </summary>
        public byte AuditingStatus
        {
            get
            {
                return _auditingStatus;
            }
            set
            {
                if (_auditingStatus == value)
                    return;
                _auditingStatus = value;
            }
        }

        /// <summary>
        /// 审核日志时间
        /// </summary>
        public DateTime AuditingLogTime
        {
            get
            {
                return _auditingLogTime;
            }
            set
            {
                if (_auditingLogTime == value)
                    return;
                _auditingLogTime = value;
            }
        }

        /// <summary>
        /// 审核日志描述
        /// </summary>
        [NotNullValidator(MessageTemplate = " 审核日志描述不能为空")]
        [StringLengthValidator(1, 255, MessageTemplate = "审核日志描述长度范围在1位～255位！")]
        public string LogDescription
        {
            get
            {
                return _logDescription;
            }
            set
            {
                if (_logDescription == value)
                    return;
                _logDescription = value;
            }
        }

        #endregion

    }
}