//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomWorkflowInfo.cs
// 描述: CustomWorkflowInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/23
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomWorkflowInfo 类</para>
    /// <para>工作流</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomWorkflowInfo
    {
        #region 内部成员变量

        private decimal _workflowId;
        private decimal _userId;
        private decimal _groupId;
        private decimal _dataId;
        private decimal _roleId;
        private decimal _parentRoleId;
        private string _workflowName = string.Empty;
        private string _workflowCode = string.Empty;
        private byte _workflowType;
        private byte _structCategory;
        private long _instanceNameRule;
        private string _instanceNameFormat = string.Empty;
        private bool _initReviewStatus;
        private bool _enableManager;
        private bool _allocationStatus;
        private bool _allowAuditing;
        private bool _finalReviewStatus;
        private bool _allowAllocation;
        private bool _enableGuidance;
        private string _guidance = string.Empty;
        private bool _workflowEnabled;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="dataId">数据填报编号</param>
        ///<param name="roleId">角色编号</param>
        ///<param name="parentRoleId">角色编号</param>
        ///<param name="workflowName">工作流名称</param>
        ///<param name="workflowCode">工作流编码</param>
        ///<param name="workflowType">工作流类型</param>
        ///<param name="structCategory">工作流结构</param>
        ///<param name="instanceNameRule">工作流实例命名字段</param>
        ///<param name="instanceNameFormat">工作流实例命名规则</param>
        ///<param name="initReviewStatus">启用初审</param>
        ///<param name="enableManager">启用管理范围</param>
        ///<param name="allocationStatus">启用分配</param>
        ///<param name="allowAuditing">分配角色允许审核</param>
        ///<param name="finalReviewStatus">启用终审</param>
        ///<param name="allowAllocation">审核角色允许再分配</param>
        ///<param name="enableGuidance">启用阅读指南</param>
        ///<param name="guidance">阅读指南内容</param>
        ///<param name="workflowEnabled">启用工作流</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomWorkflowInfo(decimal workflowId, decimal userId, decimal groupId, decimal dataId, decimal roleId,
            decimal parentRoleId, string workflowName, string workflowCode, byte workflowType, byte structCategory,
            long instanceNameRule, string instanceNameFormat, bool initReviewStatus, bool enableManager, bool allocationStatus,
            bool allowAuditing, bool finalReviewStatus, bool allowAllocation, bool enableGuidance, string guidance,
            bool workflowEnabled, bool isLeaf, int sorting, string notes)
        {
            _workflowId = workflowId;
            _userId = userId;
            _groupId = groupId;
            _dataId = dataId;
            _roleId = roleId;
            _parentRoleId = parentRoleId;
            _workflowName = workflowName;
            _workflowCode = workflowCode;
            _workflowType = workflowType;
            _structCategory = structCategory;
            _instanceNameRule = instanceNameRule;
            _instanceNameFormat = instanceNameFormat;
            _initReviewStatus = initReviewStatus;
            _enableManager = enableManager;
            _allocationStatus = allocationStatus;
            _allowAuditing = allowAuditing;
            _finalReviewStatus = finalReviewStatus;
            _allowAllocation = allowAllocation;
            _enableGuidance = enableGuidance;
            _guidance = guidance;
            _workflowEnabled = workflowEnabled;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 工作流名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 工作流名称不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "工作流名称长度范围在1位～128位！")]
        public string WorkflowName
        {
            get
            {
                return _workflowName;
            }
            set
            {
                if (_workflowName == value)
                    return;
                _workflowName = value;
            }
        }

        /// <summary>
        /// 工作流编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 工作流编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "工作流编码长度范围在1位～32位！")]
        public string WorkflowCode
        {
            get
            {
                return _workflowCode;
            }
            set
            {
                if (_workflowCode == value)
                    return;
                _workflowCode = value;
            }
        }

        /// <summary>
        /// 工作流类型
        /// </summary>
        public byte WorkflowType
        {
            get
            {
                return _workflowType;
            }
            set
            {
                if (_workflowType == value)
                    return;
                _workflowType = value;
            }
        }

        /// <summary>
        /// 工作流结构
        /// </summary>
        public byte StructCategory
        {
            get
            {
                return _structCategory;
            }
            set
            {
                if (_structCategory == value)
                    return;
                _structCategory = value;
            }
        }

        /// <summary>
        /// 工作流实例命名字段
        /// </summary>
        public long InstanceNameRule
        {
            get
            {
                return _instanceNameRule;
            }
            set
            {
                if (_instanceNameRule == value)
                    return;
                _instanceNameRule = value;
            }
        }

        /// <summary>
        /// 工作流实例命名规则
        /// </summary>
        [StringLengthValidator(0, 128, MessageTemplate = "工作流实例命名规则长度不能超过128位！")]
        public string InstanceNameFormat
        {
            get
            {
                return _instanceNameFormat;
            }
            set
            {
                if (_instanceNameFormat == value)
                    return;
                _instanceNameFormat = value;
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
        /// 启用阅读指南
        /// </summary>
        public bool EnableGuidance
        {
            get
            {
                return _enableGuidance;
            }
            set
            {
                if (_enableGuidance == value)
                    return;
                _enableGuidance = value;
            }
        }

        /// <summary>
        /// 阅读指南内容
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "阅读指南内容长度不能超过4000位！")]
        public string Guidance
        {
            get
            {
                return _guidance;
            }
            set
            {
                if (_guidance == value)
                    return;
                _guidance = value;
            }
        }

        /// <summary>
        /// 启用工作流
        /// </summary>
        public bool WorkflowEnabled
        {
            get
            {
                return _workflowEnabled;
            }
            set
            {
                if (_workflowEnabled == value)
                    return;
                _workflowEnabled = value;
            }
        }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            set
            {
                if (_isLeaf == value)
                    return;
                _isLeaf = value;
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
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]
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