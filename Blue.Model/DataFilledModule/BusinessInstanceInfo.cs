//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessInstanceInfo.cs
// 描述: BusinessInstanceInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.DataFilledModule
{
    /// <summary>
    /// <para>BusinessInstanceInfo 类</para>
    /// <para>填报业务实例</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class BusinessInstanceInfo
    {
        #region 内部成员变量

        private decimal _instanceId;
        private decimal _userId;
        private decimal _parentUserId;
        private decimal _dataId;
        private string _instanceName = string.Empty;
        private byte _instanceState;
        private DateTime _timeCreated;
        private DateTime _timeModified;
        private DateTime _timeSumbitted;
        private decimal _reviewerId;
        private bool _isDraft;
        private string _tmpComments;
        private DateTime _lastTimeHandled;
        private bool _audittedStatus;
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
        public BusinessInstanceInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="parentUserId">用户编号</param>
        ///<param name="dataId">数据填报编号</param>
        ///<param name="instanceName">实例名称</param>
        ///<param name="instanceState">实例状态</param>
        ///<param name="timeCreated">创建时间</param>
        ///<param name="timeModified">修改时间</param>
        ///<param name="timeSumbitted">提交时间</param>
        ///<param name="reviewerId">审核人</param>
        ///<param name="isDraft">是否草稿</param>
        ///<param name="tmpComments">草稿意见</param>
        ///<param name="lastTimeHandled">最新处理时间</param>
        ///<param name="audittedStatus">审核状态</param>
        ///<param name="isArchived">是否归档</param>
        ///<param name="archivedUserName">归档人用户名</param>
        ///<param name="archivedName">归档人姓名</param>
        ///<param name="timeArchived">归档时间</param>
        ///<param name="pageSign">页面标记</param>
        public BusinessInstanceInfo(decimal instanceId, decimal userId, decimal parentUserId, decimal dataId, string instanceName,
            byte instanceState, DateTime timeCreated, DateTime timeModified, DateTime timeSumbitted, decimal reviewerId, bool isDraft,
            string tmpComments, DateTime lastTimeHandled, bool audittedStatus, bool isArchived, string archivedUserName, string archivedName,
            DateTime timeArchived, long pageSign)
        {
            _instanceId = instanceId;
            _userId = userId;
            _parentUserId = parentUserId;
            _dataId = dataId;
            _instanceName = instanceName;
            _instanceState = instanceState;
            _timeCreated = timeCreated;
            _timeModified = timeModified;
            _timeSumbitted = timeSumbitted;
            _reviewerId = reviewerId;
            _isDraft = isDraft;
            _tmpComments = tmpComments;
            _lastTimeHandled = lastTimeHandled;
            _audittedStatus = audittedStatus;
            _isArchived = isArchived;
            _archivedUserName = archivedUserName;
            _archivedName = archivedName;
            _timeArchived = timeArchived;
            _pageSign = pageSign;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 实例编号
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
        /// 数据填报编号
        /// </summary>
        public decimal DataId
        {
            get
            {
                return _dataId;
            }
            set
            {
                if (_dataId == value)
                    return;
                _dataId = value;
            }
        }

        /// <summary>
        /// 实例名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 实例名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "实例名称长度范围在1位～64位！")]
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
        /// 实例状态
        /// </summary>
        public byte InstanceState
        {
            get
            {
                return _instanceState;
            }
            set
            {
                if (_instanceState == value)
                    return;
                _instanceState = value;
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
        /// 审核人
        /// </summary>
        public decimal ReviewerId
        {
            get
            {
                return _reviewerId;
            }
            set
            {
                if (_reviewerId == value)
                    return;
                _reviewerId = value;
            }
        }

        /// <summary>
        /// 是否草稿
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

        /// <summary>
        /// 草稿意见
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "实例名称长度范围在0位～512位！")]
        public string TmpComments
        {
            get
            {
                return _tmpComments;
            }
            set
            {
                if (_tmpComments == value)
                    return;
                _tmpComments = value;
            }
        }

        ///<param name="isDraft"></param>
        ///<param name=""></param>

        /// <summary>
        /// 最新处理时间
        /// </summary>
        public DateTime LastTimeHandled
        {
            get
            {
                return _lastTimeHandled;
            }
            set
            {
                if (_lastTimeHandled == value)
                    return;
                _lastTimeHandled = value;
            }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public bool AudittedStatus
        {
            get
            {
                return _audittedStatus;
            }
            set
            {
                if (_audittedStatus == value)
                    return;
                _audittedStatus = value;
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