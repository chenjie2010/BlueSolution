//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomWorkflowInstanceInfo.cs
// 描述: CustomWorkflowInstanceInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomWorkflowInstanceInfo 类</para>
    /// <para>工作流实例</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomWorkflowInstanceInfo
    {
        #region 内部成员变量

        private decimal _instanceId;
        private decimal _userId;
        private decimal _workflowId;
        private decimal _parentUserId;
        private string _instanceName = string.Empty;
        private byte _instanceStatus;
        private DateTime _timeCreated;
        private DateTime _timeModified;
        private DateTime _timeSumbitted;
        private bool _isArchived;
        private string _archivedUserName = string.Empty;
        private string _archivedName = string.Empty;
        private DateTime _timeArchived;
        private long _pageSign = 0;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowInstanceInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="instanceId">工作流实例编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="workflowId">工作流编号</param>
        ///<param name="parentUserId">用户编号</param>
        ///<param name="instanceName">工作流实例名称</param>
        ///<param name="instanceStatus">工作流实例状态</param>
        ///<param name="timeCreated">创建时间</param>
        ///<param name="timeModified">修改时间</param>
        ///<param name="timeSumbitted">提交时间</param>
        ///<param name="isArchived">是否归档</param>
        ///<param name="archivedUserName">归档人用户名</param>
        ///<param name="archivedName">归档人姓名</param>
        ///<param name="timeArchived">归档时间</param>
        ///<param name="pageSign">页面标记</param>
        public CustomWorkflowInstanceInfo(decimal instanceId, decimal userId, decimal workflowId, decimal parentUserId, string instanceName,
            byte instanceStatus, DateTime timeCreated, DateTime timeModified, DateTime timeSumbitted, bool isArchived,
            string archivedUserName, string archivedName, DateTime timeArchived, long pageSign)
        {
            _instanceId = instanceId;
            _userId = userId;
            _workflowId = workflowId;
            _parentUserId = parentUserId;
            _instanceName = instanceName;
            _instanceStatus = instanceStatus;
            _timeCreated = timeCreated;
            _timeModified = timeModified;
            _timeSumbitted = timeSumbitted;
            _isArchived = isArchived;
            _archivedUserName = archivedUserName;
            _archivedName = archivedName;
            _timeArchived = timeArchived;
            _pageSign = pageSign;

        }

        #endregion

        #region 字段属性

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
        /// 工作流编号
        /// </summary>
        public decimal WorkflowId
        {
            get
            {
                return _workflowId;
            }
            set
            {
                if (_workflowId == value)
                    return;
                _workflowId = value;
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
        /// 工作流实例名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 工作流实例名称不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "工作流实例名称长度范围在1位～128位！")]
        public string InstanceName
        {
            get
            {
                return _instanceName;
            }
            set
            {
                if (_instanceName == value)
                    return;
                _instanceName = value;
            }
        }

        /// <summary>
        /// 工作流实例状态
        /// </summary>
        public byte InstanceStatus
        {
            get
            {
                return _instanceStatus;
            }
            set
            {
                if (_instanceStatus == value)
                    return;
                _instanceStatus = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime TimeCreated
        {
            get
            {
                return _timeCreated;
            }
            set
            {
                if (_timeCreated == value)
                    return;
                _timeCreated = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime TimeModified
        {
            get
            {
                return _timeModified;
            }
            set
            {
                if (_timeModified == value)
                    return;
                _timeModified = value;
            }
        }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime TimeSumbitted
        {
            get
            {
                return _timeSumbitted;
            }
            set
            {
                if (_timeSumbitted == value)
                    return;
                _timeSumbitted = value;
            }
        }

        /// <summary>
        /// 是否归档
        /// </summary>
        public bool IsArchived
        {
            get
            {
                return _isArchived;
            }
            set
            {
                if (_isArchived == value)
                    return;
                _isArchived = value;
            }
        }

        /// <summary>
        /// 归档人用户名
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "归档人用户名长度不能超过32位！")]
        public string ArchivedUserName
        {
            get
            {
                return _archivedUserName;
            }
            set
            {
                if (_archivedUserName == value)
                    return;
                _archivedUserName = value;
            }
        }

        /// <summary>
        /// 归档人姓名
        /// </summary>
        [StringLengthValidator(0, 128, MessageTemplate = "归档人姓名长度不能超过128位！")]
        public string ArchivedName
        {
            get
            {
                return _archivedName;
            }
            set
            {
                if (_archivedName == value)
                    return;
                _archivedName = value;
            }
        }

        /// <summary>
        /// 归档时间
        /// </summary>
        public DateTime TimeArchived
        {
            get
            {
                return _timeArchived;
            }
            set
            {
                if (_timeArchived == value)
                    return;
                _timeArchived = value;
            }
        }

        /// <summary>
        /// 页面标记
        /// </summary>
        public long PageSign
        {
            get
            {
                return _pageSign;
            }
            set
            {
                if (_pageSign == value)
                    return;
                _pageSign = value;
            }
        }

        #endregion

    }
}