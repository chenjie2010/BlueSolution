//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomDataInfo.cs
// 描述: CustomDataInfo 实体类
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
    /// <para>CustomDataInfo 类</para>
    /// <para>数据填报</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomDataInfo
    {
        #region 内部成员变量

        private decimal _dataId;
        private decimal _tableId;
        private decimal _reportId;
        private decimal _roleId;
        private decimal _groupId;
        private decimal _parentReportId;
        private decimal _parentRoleId;
        private decimal _viewId;
        private string _dataName = string.Empty;
        private string _dataCode = string.Empty;
        private byte _dataFilledType;
        private byte _dataShowMode;
        private byte _dataInputMode;
        private int _clientFormHeight;
        private int _webFormHeight;
        private bool _isInitReview;
        private bool _enableManager;
        private bool _isFinalReview;
        private byte _dataFilledProperty;
        private bool _enableGuidance;
        private string _guidance = string.Empty;
        private bool _conditionEnabled;
        private string _conditionContent = string.Empty;
        private byte _conditionType;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDataInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataId">数据填报编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="reportId"></param>
        ///<param name="roleId">角色编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="parentReportId">报表编号</param>
        ///<param name="parentRoleId">角色编号</param>
        ///<param name="viewId">视图编号</param>
        ///<param name="dataName">数据填报名称</param>
        ///<param name="dataCode">数据填报编码</param>
        ///<param name="dataFilledType">填报类型</param>
        ///<param name="dataShowMode">展现模式</param>
        ///<param name="dataInputMode">录入模式</param>
        ///<param name="clientFormHeight">客户端窗体高度</param>
        ///<param name="webFormHeight">Web端窗体高度</param>
        ///<param name="isInitReview">是否初审</param>
        ///<param name="enableManager">启用管理范围</param>
        ///<param name="isFinalReview">是否终审</param>
        ///<param name="dataFilledProperty">填报属性</param>
        ///<param name="enableGuidance">启用阅读指南</param>
        ///<param name="guidance">阅读指南内容</param>
        ///<param name="conditionEnabled">启用条件</param>
        ///<param name="conditionContent">条件内容</param>
        ///<param name="conditionType">条件类型</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomDataInfo(decimal dataId, decimal tableId, decimal reportId, decimal roleId, decimal groupId,
            decimal parentReportId, decimal parentRoleId, decimal viewId, string dataName, string dataCode,
            byte dataFilledType, byte dataShowMode, byte dataInputMode, int clientFormHeight, int webFormHeight,
            bool isInitReview, bool enableManager, bool isFinalReview, byte dataFilledProperty, bool enableGuidance,
            string guidance, bool conditionEnabled, string conditionContent, byte conditionType, bool isLeaf,
            int sorting, string notes)
        {
            _dataId = dataId;
            _tableId = tableId;
            _reportId = reportId;
            _roleId = roleId;
            _groupId = groupId;
            _parentReportId = parentReportId;
            _parentRoleId = parentRoleId;
            _viewId = viewId;
            _dataName = dataName;
            _dataCode = dataCode;
            _dataFilledType = dataFilledType;
            _dataShowMode = dataShowMode;
            _dataInputMode = dataInputMode;
            _clientFormHeight = clientFormHeight;
            _webFormHeight = webFormHeight;
            _isInitReview = isInitReview;
            _enableManager = enableManager;
            _isFinalReview = isFinalReview;
            _dataFilledProperty = dataFilledProperty;
            _enableGuidance = enableGuidance;
            _guidance = guidance;
            _conditionEnabled = conditionEnabled;
            _conditionContent = conditionContent;
            _conditionType = conditionType;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 
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
        /// 报表编号
        /// </summary>
        public decimal ParentReportId
        {
            get
            {
                return _parentReportId;
            }
            set
            {
                if (_parentReportId == value)
                    return;
                _parentReportId = value;
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
        /// 视图编号
        /// </summary>
        public decimal ViewId
        {
            get
            {
                return _viewId;
            }
            set
            {
                if (_viewId == value)
                    return;
                _viewId = value;
            }
        }

        /// <summary>
        /// 数据填报名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据填报名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "数据填报名称长度范围在1位～256位！")]
        public string DataName
        {
            get
            {
                return _dataName;
            }
            set
            {
                if (_dataName == value)
                    return;
                _dataName = value;
            }
        }

        /// <summary>
        /// 数据填报编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据填报编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "数据填报编码长度范围在1位～32位！")]
        public string DataCode
        {
            get
            {
                return _dataCode;
            }
            set
            {
                if (_dataCode == value)
                    return;
                _dataCode = value;
            }
        }

        /// <summary>
        /// 填报类型
        /// </summary>
        public byte DataFilledType
        {
            get
            {
                return _dataFilledType;
            }
            set
            {
                if (_dataFilledType == value)
                    return;
                _dataFilledType = value;
            }
        }

        /// <summary>
        /// 展现模式
        /// </summary>
        public byte DataShowMode
        {
            get
            {
                return _dataShowMode;
            }
            set
            {
                if (_dataShowMode == value)
                    return;
                _dataShowMode = value;
            }
        }

        /// <summary>
        /// 录入模式
        /// </summary>
        public byte DataInputMode
        {
            get
            {
                return _dataInputMode;
            }
            set
            {
                if (_dataInputMode == value)
                    return;
                _dataInputMode = value;
            }
        }

        /// <summary>
        /// 客户端窗体高度
        /// </summary>
        public int ClientFormHeight
        {
            get
            {
                return _clientFormHeight;
            }
            set
            {
                if (_clientFormHeight == value)
                    return;
                _clientFormHeight = value;
            }
        }

        /// <summary>
        /// Web端窗体高度
        /// </summary>
        public int WebFormHeight
        {
            get
            {
                return _webFormHeight;
            }
            set
            {
                if (_webFormHeight == value)
                    return;
                _webFormHeight = value;
            }
        }

        /// <summary>
        /// 是否初审
        /// </summary>
        public bool IsInitReview
        {
            get
            {
                return _isInitReview;
            }
            set
            {
                if (_isInitReview == value)
                    return;
                _isInitReview = value;
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
        /// 是否终审
        /// </summary>
        public bool IsFinalReview
        {
            get
            {
                return _isFinalReview;
            }
            set
            {
                if (_isFinalReview == value)
                    return;
                _isFinalReview = value;
            }
        }

        /// <summary>
        /// 填报属性
        /// </summary>
        public byte DataFilledProperty
        {
            get
            {
                return _dataFilledProperty;
            }
            set
            {
                if (_dataFilledProperty == value)
                    return;
                _dataFilledProperty = value;
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
        /// 启用条件
        /// </summary>
        public bool ConditionEnabled
        {
            get
            {
                return _conditionEnabled;
            }
            set
            {
                if (_conditionEnabled == value)
                    return;
                _conditionEnabled = value;
            }
        }

        /// <summary>
        /// 条件内容
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "条件内容长度不能超过4000位！")]
        public string ConditionContent
        {
            get
            {
                return _conditionContent;
            }
            set
            {
                if (_conditionContent == value)
                    return;
                _conditionContent = value;
            }
        }

        /// <summary>
        /// 条件类型
        /// </summary>
        public byte ConditionType
        {
            get
            {
                return _conditionType;
            }
            set
            {
                if (_conditionType == value)
                    return;
                _conditionType = value;
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