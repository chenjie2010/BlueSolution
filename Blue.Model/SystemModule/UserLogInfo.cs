//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserLogInfo.cs
// 描述: UserLogInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/7/5
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>UserLogInfo 类</para>
    /// <para>用户日志</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class UserLogInfo
    {
        #region 内部成员变量

        private decimal _logId;
        private decimal _userId;
        private byte _logClass;
        private string _businessName = string.Empty;
        private int _logEnumName;
        private byte _logAction;
        private byte _logLevel;
        private DateTime _logDate;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserLogInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="logId"></param>
        ///<param name="userId">用户编号</param>
        ///<param name="logClass">平台名称</param>
        ///<param name="businessName">业务名称</param>
        ///<param name="logEnumName">操作名称</param>
        ///<param name="logAction"></param>
        ///<param name="logLevel"></param>
        ///<param name="logDate">记录时间</param>
        public UserLogInfo(decimal logId, decimal userId, byte logClass, string businessName, int logEnumName,
            byte logAction, byte logLevel, DateTime logDate)
        {
            _logId = logId;
            _userId = userId;
            _logClass = logClass;
            _businessName = businessName;
            _logEnumName = logEnumName;
            _logAction = logAction;
            _logLevel = logLevel;
            _logDate = logDate;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 
        /// </summary>
        public decimal LogId
        {
            get
            {
                return _logId;
            }
            set
            {
                if (_logId == value)
                    return;
                _logId = value;
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
        /// 平台名称
        /// </summary>
        public byte LogClass
        {
            get
            {
                return _logClass;
            }
            set
            {
                if (_logClass == value)
                    return;
                _logClass = value;
            }
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 业务名称不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "业务名称长度范围在1位～128位！")]
        public string BusinessName
        {
            get
            {
                return _businessName;
            }
            set
            {
                if (_businessName == value)
                    return;
                _businessName = value;
            }
        }

        /// <summary>
        /// 操作名称
        /// </summary>
        public int LogEnumName
        {
            get
            {
                return _logEnumName;
            }
            set
            {
                if (_logEnumName == value)
                    return;
                _logEnumName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte LogAction
        {
            get
            {
                return _logAction;
            }
            set
            {
                if (_logAction == value)
                    return;
                _logAction = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte LogLevel
        {
            get
            {
                return _logLevel;
            }
            set
            {
                if (_logLevel == value)
                    return;
                _logLevel = value;
            }
        }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogDate
        {
            get
            {
                return _logDate;
            }
            set
            {
                if (_logDate == value)
                    return;
                _logDate = value;
            }
        }

        #endregion

    }
}