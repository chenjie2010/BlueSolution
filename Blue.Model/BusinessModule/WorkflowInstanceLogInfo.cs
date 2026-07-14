//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowInstanceLogInfo.cs
// 描述: WorkflowInstanceLogInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/29
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>WorkflowInstanceLogInfo 类</para>
    /// <para>工作流实例日志</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class WorkflowInstanceLogInfo
    {
        #region 内部成员变量

        private decimal _logId;
        private decimal _processId;
        private decimal _instanceId;
        private decimal _userId;
        private decimal _parentUserId;
        private byte _reviewedAction;
        private string _comment = string.Empty;
        private DateTime _timeReviewed;
        private bool _isDraft;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public WorkflowInstanceLogInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="logId">日志编号</param>
        ///<param name="processId">流程编号</param>
        ///<param name="instanceId">工作流实例编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="parentUserId">用户编号</param>
        ///<param name="reviewedAction">审核动作</param>
        ///<param name="comment">附加信息</param>
        ///<param name="timeReviewed">处理时间</param>
        ///<param name="isDraft">是否是草稿</param>
        public WorkflowInstanceLogInfo(decimal logId, decimal processId, decimal instanceId, decimal userId, decimal parentUserId,
            byte reviewedAction, string comment, DateTime timeReviewed, bool isDraft)
        {
            _logId = logId;
            _processId = processId;
            _instanceId = instanceId;
            _userId = userId;
            _parentUserId = parentUserId;
            _reviewedAction = reviewedAction;
            _comment = comment;
            _timeReviewed = timeReviewed;
            _isDraft = isDraft;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 日志编号
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
        /// 流程编号
        /// </summary>
        public decimal ProcessId
        {
            get
            {
                return _processId;
            }
            set
            {
                if (_processId == value)
                    return;
                _processId = value;
            }
        }

        /// <summary>
        /// 工作流实例编号
        /// </summary>
        public decimal InstanceId
        {
            get
            {
                return _instanceId;
            }
            set
            {
                if (_instanceId == value)
                    return;
                _instanceId = value;
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
        /// 审核动作
        /// </summary>
        public byte ReviewedAction
        {
            get
            {
                return _reviewedAction;
            }
            set
            {
                if (_reviewedAction == value)
                    return;
                _reviewedAction = value;
            }
        }

        /// <summary>
        /// 附加信息
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "附加信息长度不能超过512位！")]
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

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime TimeReviewed
        {
            get
            {
                return _timeReviewed;
            }
            set
            {
                if (_timeReviewed == value)
                    return;
                _timeReviewed = value;
            }
        }

        /// <summary>
        /// 是否是草稿
        /// </summary>
        public bool IsDraft
        {
            get
            {
                return _isDraft;
            }
            set
            {
                if (_isDraft == value)
                    return;
                _isDraft = value;
            }
        }

        #endregion

    }
}