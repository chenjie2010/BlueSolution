//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowInstanceStepInfo.cs
// 描述: WorkflowInstanceStepInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/30
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>WorkflowInstanceStepInfo 类</para>
    /// <para>工作流实例流程</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class WorkflowInstanceStepInfo
    {
        #region 内部成员变量

        private decimal _stepId;
        private decimal _instanceId;
        private decimal _processId;
        private decimal _userId;
        private byte _reviewedStatus;
        private DateTime _timeReviewed;
        private int _processCounter;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public WorkflowInstanceStepInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="stepId">步骤编号</param>
        ///<param name="instanceId">工作流实例编号</param>
        ///<param name="processId">流程编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="reviewedStatus">审核状态</param>
        ///<param name="timeReviewed">处理时间</param>
        ///<param name="processCounter">计数器</param>
        public WorkflowInstanceStepInfo(decimal stepId, decimal instanceId, decimal processId, decimal userId, byte reviewedStatus,
            DateTime timeReviewed, int processCounter)
        {
            _stepId = stepId;
            _instanceId = instanceId;
            _processId = processId;
            _userId = userId;
            _reviewedStatus = reviewedStatus;
            _timeReviewed = timeReviewed;
            _processCounter = processCounter;

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
        /// 审核状态
        /// </summary>
        public byte ReviewedStatus
        {
            get
            {
                return _reviewedStatus;
            }
            set
            {
                if (_reviewedStatus == value)
                    return;
                _reviewedStatus = value;
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
        /// 计数器
        /// </summary>
        public int ProcessCounter
        {
            get
            {
                return _processCounter;
            }
            set
            {
                if (_processCounter == value)
                    return;
                _processCounter = value;
            }
        }

        #endregion

    }
}