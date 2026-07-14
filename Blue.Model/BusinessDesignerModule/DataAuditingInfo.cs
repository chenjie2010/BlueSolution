//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingInfo.cs
// 描述: DataAuditingInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/22
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
    /// <summary>
    /// <para>DataAuditingInfo 类</para>
    /// <para>数据审核</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class DataAuditingInfo
    {
        #region 内部成员变量

        private decimal _dataAuditingId;
        private decimal _roleId;
        private decimal _reportId;
        private decimal _userId;
        private decimal _tableId;
        private decimal _groupId;
        private decimal _combinedTableId;
        private decimal _parentRoleId;
        private decimal _parentDataAuditingId;
        private string _dataAuditingName = string.Empty;
        private string _dataAuditingCode = string.Empty;
        private byte _dataAuditingType;
        private long _dataAuditingProperty;
        private long _systemCondition;
        private long _systemDataFieldAuthority;
        private byte _tableType;
        private bool _initReviewStatus;
        private bool _enableManager;
        private bool _allocationStatus;
        private bool _finalReviewStatus;
        private bool _allowAuditing;
        private bool _allowAllocation;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditingInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataAuditingId">数据审核编号</param>
        ///<param name="roleId">角色编号</param>
        ///<param name="reportId">报表编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="combinedTableId"></param>
        ///<param name="parentRoleId">角色编号</param>
        ///<param name="parentDataAuditingId">数据审核编号</param>
        ///<param name="dataAuditingName">数据审核名称</param>
        ///<param name="dataAuditingCode">数据审核编码</param>
        ///<param name="dataAuditingType">数据审核类型</param>
        ///<param name="dataAuditingProperty">数据审核属性</param>
        ///<param name="systemCondition">审核系统条件</param>
        ///<param name="systemDataFieldAuthority">系统字段权限</param>
        ///<param name="tableType">表格类型</param>
        ///<param name="initReviewStatus">启用初审</param>
        ///<param name="enableManager">启用管理范围</param>
        ///<param name="allocationStatus">启用分配</param>
        ///<param name="finalReviewStatus">启用终审</param>
        ///<param name="allowAuditing">分配角色允许审核</param>
        ///<param name="allowAllocation">审核角色允许再分配</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public DataAuditingInfo(decimal dataAuditingId, decimal roleId, decimal reportId, decimal userId, decimal tableId,
            decimal groupId, decimal combinedTableId, decimal parentRoleId, decimal parentDataAuditingId, string name,
            string dataAuditingCode, byte dataAuditingType, long dataAuditingProperty, long systemCondition, long systemDataFieldAuthority,
            byte tableType, bool initReviewStatus, bool enableManager, bool allocationStatus, bool finalReviewStatus,
            bool allowAuditing, bool allowAllocation, int sorting, string notes)
        {
            _dataAuditingId = dataAuditingId;
            _roleId = roleId;
            _reportId = reportId;
            _userId = userId;
            _tableId = tableId;
            _groupId = groupId;
            _combinedTableId = combinedTableId;
            _parentRoleId = parentRoleId;
            _parentDataAuditingId = parentDataAuditingId;
            _dataAuditingName = name;
            _dataAuditingCode = dataAuditingCode;
            _dataAuditingType = dataAuditingType;
            _dataAuditingProperty = dataAuditingProperty;
            _systemCondition = systemCondition;
            _systemDataFieldAuthority = systemDataFieldAuthority;
            _tableType = tableType;
            _initReviewStatus = initReviewStatus;
            _enableManager = enableManager;
            _allocationStatus = allocationStatus;
            _finalReviewStatus = finalReviewStatus;
            _allowAuditing = allowAuditing;
            _allowAllocation = allowAllocation;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 数据审核编号
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
        /// 角色编号
        /// </summary>
        public decimal RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (_roleId == value)
                    return;
                _roleId = value;
            }
        }

        /// <summary>
        /// 报表编号
        /// </summary>
        public decimal ReportId
        {
            get
            {
                return _reportId;
            }
            set
            {
                if (_reportId == value)
                    return;
                _reportId = value;
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
        /// 表编号
        /// </summary>
        public decimal TableId
        {
            get
            {
                return _tableId;
            }
            set
            {
                if (_tableId == value)
                    return;
                _tableId = value;
            }
        }

        /// <summary>
        /// 分组编号
        /// </summary>
        public decimal GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (_groupId == value)
                    return;
                _groupId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal CombinedTableId
        {
            get
            {
                return _combinedTableId;
            }
            set
            {
                if (_combinedTableId == value)
                    return;
                _combinedTableId = value;
            }
        }

        /// <summary>
        /// 角色编号
        /// </summary>
        public decimal ParentRoleId
        {
            get
            {
                return _parentRoleId;
            }
            set
            {
                if (_parentRoleId == value)
                    return;
                _parentRoleId = value;
            }
        }

        /// <summary>
        /// 数据审核编号
        /// </summary>
        public decimal ParentDataAuditingId
        {
            get
            {
                return _parentDataAuditingId;
            }
            set
            {
                if (_parentDataAuditingId == value)
                    return;
                _parentDataAuditingId = value;
            }
        }

        /// <summary>
        /// 数据审核名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据审核名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "数据审核名称长度范围在1位～64位！")]
        public string DataAuditingName
        {
            get
            {
                return _dataAuditingName;
            }
            set
            {
                if (_dataAuditingName == value)
                    return;
                _dataAuditingName = value;
            }
        }

        /// <summary>
        /// 数据审核编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据审核编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "数据审核编码长度范围在1位～32位！")]
        public string DataAuditingCode
        {
            get
            {
                return _dataAuditingCode;
            }
            set
            {
                if (_dataAuditingCode == value)
                    return;
                _dataAuditingCode = value;
            }
        }

        /// <summary>
        /// 数据审核类型
        /// </summary>
        public byte DataAuditingType
        {
            get
            {
                return _dataAuditingType;
            }
            set
            {
                if (_dataAuditingType == value)
                    return;
                _dataAuditingType = value;
            }
        }

        /// <summary>
        /// 数据审核属性
        /// </summary>
        public long DataAuditingProperty
        {
            get
            {
                return _dataAuditingProperty;
            }
            set
            {
                if (_dataAuditingProperty == value)
                    return;
                _dataAuditingProperty = value;
            }
        }

        /// <summary>
        /// 审核系统条件
        /// </summary>
        public long SystemCondition
        {
            get
            {
                return _systemCondition;
            }
            set
            {
                if (_systemCondition == value)
                    return;
                _systemCondition = value;
            }
        }

        /// <summary>
        /// 系统字段权限
        /// </summary>
        public long SystemDataFieldAuthority
        {
            get
            {
                return _systemDataFieldAuthority;
            }
            set
            {
                if (_systemDataFieldAuthority == value)
                    return;
                _systemDataFieldAuthority = value;
            }
        }

        /// <summary>
        /// 表格类型
        /// </summary>
        public byte TableType
        {
            get
            {
                return _tableType;
            }
            set
            {
                if (_tableType == value)
                    return;
                _tableType = value;
            }
        }

        /// <summary>
        /// 启用初审
        /// </summary>
        public bool InitReviewStatus
        {
            get
            {
                return _initReviewStatus;
            }
            set
            {
                if (_initReviewStatus == value)
                    return;
                _initReviewStatus = value;
            }
        }

        /// <summary>
        /// 启用管理范围
        /// </summary>
        public bool EnableManager
        {
            get
            {
                return _enableManager;
            }
            set
            {
                if (_enableManager == value)
                    return;
                _enableManager = value;
            }
        }

        /// <summary>
        /// 启用分配
        /// </summary>
        public bool AllocationStatus
        {
            get
            {
                return _allocationStatus;
            }
            set
            {
                if (_allocationStatus == value)
                    return;
                _allocationStatus = value;
            }
        }

        /// <summary>
        /// 启用终审
        /// </summary>
        public bool FinalReviewStatus
        {
            get
            {
                return _finalReviewStatus;
            }
            set
            {
                if (_finalReviewStatus == value)
                    return;
                _finalReviewStatus = value;
            }
        }

        /// <summary>
        /// 分配角色允许审核
        /// </summary>
        public bool AllowAuditing
        {
            get
            {
                return _allowAuditing;
            }
            set
            {
                if (_allowAuditing == value)
                    return;
                _allowAuditing = value;
            }
        }

        /// <summary>
        /// 审核角色允许再分配
        /// </summary>
        public bool AllowAllocation
        {
            get
            {
                return _allowAllocation;
            }
            set
            {
                if (_allowAllocation == value)
                    return;
                _allowAllocation = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sorting
        {
            get
            {
                return _sorting;
            }
            set
            {
                if (_sorting == value)
                    return;
                _sorting = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 255, MessageTemplate = "备注长度不能超过255位！")]
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (_notes == value)
                    return;
                _notes = value;
            }
        }

        #endregion

    }
}