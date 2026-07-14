//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingStepInfo.cs
// 描述: DataAuditingStepInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/21
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
    /// <summary>
    /// <para>DataAuditingStepInfo 类</para>
    /// <para>记录审核</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class DataAuditingStepInfo
    {
        #region 内部成员变量

        private decimal _stepId;
        private decimal _userId;
        private decimal _auditingLogId;
        private decimal _parentUserId;
        private byte _auditingAction;
        private DateTime _auditingTime;
        private string _comment = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditingStepInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="stepId">步骤编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="auditingLogId">审核日志编号</param>
        ///<param name="parentUserId">用户编号</param>
        ///<param name="auditingAction">操作动作</param>
        ///<param name="auditingTime">操作时间</param>
        ///<param name="comment">提交意见</param>
        public DataAuditingStepInfo(decimal stepId, decimal userId, decimal auditingLogId, decimal parentUserId, byte auditingAction,
            DateTime auditingTime, string comment)
        {
            _stepId = stepId;
            _userId = userId;
            _auditingLogId = auditingLogId;
            _parentUserId = parentUserId;
            _auditingAction = auditingAction;
            _auditingTime = auditingTime;
            _comment = comment;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 步骤编号
        /// </summary>
        public decimal StepId
        {
            get
            {
                return _stepId;
            }
            set
            {
                if (_stepId == value)
                    return;
                _stepId = value;
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
        /// 操作动作
        /// </summary>
        public byte AuditingAction
        {
            get
            {
                return _auditingAction;
            }
            set
            {
                if (_auditingAction == value)
                    return;
                _auditingAction = value;
            }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime AuditingTime
        {
            get
            {
                return _auditingTime;
            }
            set
            {
                if (_auditingTime == value)
                    return;
                _auditingTime = value;
            }
        }

        /// <summary>
        /// 提交意见
        /// </summary>
        [NotNullValidator(MessageTemplate = " 提交意见不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "提交意见长度范围在1位～256位！")]
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (_comment == value)
                    return;
                _comment = value;
            }
        }

        #endregion

    }
}