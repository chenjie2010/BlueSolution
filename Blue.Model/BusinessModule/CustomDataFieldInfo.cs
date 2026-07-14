//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomDataFieldInfo.cs
// 描述: CustomDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/14
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomDataFieldInfo 类</para>
    /// <para>自定义字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomDataFieldInfo
    {
        #region 内部成员变量

        private decimal _dataFieldId;
        private decimal _enumId;
        private decimal _parentDataFieldId;
        private decimal _associatedDataFieldId;
        private decimal _tableId;
        private string _logicalName = string.Empty;
        private string _physicalName = string.Empty;
        private string _dataFieldCode = string.Empty;
        private byte _dataFieldProperty;
        private byte _dataFieldType;
        private int _dataFieldLength;
        private byte _basedDataType;
        private string _regexExpression = string.Empty;
        private string _expressionText = string.Empty;
        private long _dataFieldSetting;
        private bool _requiredDataField;
        private bool _autoComplete;
        private bool _indexCreated;
        private bool _helpEnabled;
        private string _helpContent = string.Empty;
        private string _tooltip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="enumId">枚举编号</param>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="logicalName">字段逻辑名称</param>
        ///<param name="physicalName">字段物理名称</param>
        ///<param name="dataFieldCode">字段编码</param>
        ///<param name="dataFieldProperty">字段属性</param>
        ///<param name="dataFieldType">字段类型</param>
        ///<param name="dataFieldLength">字段长度</param>
        ///<param name="basedDataType">基础类型</param>
        ///<param name="regexExpression">正则表达式</param>
        ///<param name="expressionText">表达式文本</param>
        ///<param name="dataFieldSetting">字段设置</param>
        ///<param name="requiredDataField">是否必填</param>
        ///<param name="autoComplete">自动完成</param>
        ///<param name="indexCreated">创建索引</param>
        ///<param name="helpEnabled">启用帮助</param>
        ///<param name="helpContent">帮助内容</param>
        ///<param name="tooltip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomDataFieldInfo(decimal dataFieldId, decimal enumId, decimal parentDataFieldId, decimal associatedDataFieldId, decimal tableId,
            string logicalName, string physicalName, string dataFieldCode, byte dataFieldProperty, byte dataFieldType,
            int dataFieldLength, byte basedDataType, string regexExpression, string expressionText, long dataFieldSetting, bool requiredDataField,
            bool autoComplete, bool indexCreated, bool helpEnabled, string helpContent, string tooltip,
            int sorting, string notes)
        {
            _dataFieldId = dataFieldId;
            _enumId = enumId;
            _parentDataFieldId = parentDataFieldId;
            _associatedDataFieldId = associatedDataFieldId;
            _tableId = tableId;
            _logicalName = logicalName;
            _physicalName = physicalName;
            _dataFieldCode = dataFieldCode;
            _dataFieldProperty = dataFieldProperty;
            _dataFieldType = dataFieldType;
            _dataFieldLength = dataFieldLength;
            _basedDataType = basedDataType;
            _regexExpression = regexExpression;
            _expressionText = expressionText;
            _dataFieldSetting = dataFieldSetting;
            _requiredDataField = requiredDataField;
            _autoComplete = autoComplete;
            _indexCreated = indexCreated;
            _helpEnabled = helpEnabled;
            _helpContent = helpContent;
            _tooltip = tooltip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get
            {
                return _dataFieldId;
            }
            set
            {
                if (_dataFieldId == value)
                    return;
                _dataFieldId = value;
            }
        }

        /// <summary>
        /// 枚举编号
        /// </summary>
        public decimal EnumId
        {
            get
            {
                return _enumId;
            }
            set
            {
                if (_enumId == value)
                    return;
                _enumId = value;
            }
        }

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal ParentDataFieldId
        {
            get
            {
                return _parentDataFieldId;
            }
            set
            {
                if (_parentDataFieldId == value)
                    return;
                _parentDataFieldId = value;
            }
        }

        /// <summary>
        /// 关联字段编号
        /// </summary>
        public decimal AssociatedDataFieldId
        {
            get
            {
                return _associatedDataFieldId;
            }
            set
            {
                if (_associatedDataFieldId == value)
                    return;
                _associatedDataFieldId = value;
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
        /// 字段逻辑名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 字段逻辑名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "字段逻辑名称长度范围在1位～64位！")]
        public string LogicalName
        {
            get
            {
                return _logicalName;
            }
            set
            {
                if (_logicalName == value)
                    return;
                _logicalName = value;
            }
        }

        /// <summary>
        /// 字段物理名称
        /// </summary>
        public string PhysicalName
        {
            get
            {
                return _physicalName;
            }
            set
            {
                if (_physicalName == value)
                    return;
                _physicalName = value;
            }
        }

        /// <summary>
        /// 字段编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 字段编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "字段编码长度范围在1位～32位！")]
        public string DataFieldCode
        {
            get
            {
                return _dataFieldCode;
            }
            set
            {
                if (_dataFieldCode == value)
                    return;
                _dataFieldCode = value;
            }
        }

        /// <summary>
        /// 字段属性
        /// </summary>
        public byte DataFieldProperty
        {
            get
            {
                return _dataFieldProperty;
            }
            set
            {
                if (_dataFieldProperty == value)
                    return;
                _dataFieldProperty = value;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public byte DataFieldType
        {
            get
            {
                return _dataFieldType;
            }
            set
            {
                if (_dataFieldType == value)
                    return;
                _dataFieldType = value;
            }
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int DataFieldLength
        {
            get
            {
                return _dataFieldLength;
            }
            set
            {
                if (_dataFieldLength == value)
                    return;
                _dataFieldLength = value;
            }
        }

        /// <summary>
        /// 基础类型
        /// </summary>
        public byte BasedDataType
        {
            get
            {
                return _basedDataType;
            }
            set
            {
                if (_basedDataType == value)
                    return;
                _basedDataType = value;
            }
        }
        
        /// <summary>
        /// 正则表达式
        /// </summary>
        [StringLengthValidator(0, 1024, MessageTemplate = "正则表达式长度不能超过1024位！")]
        public string RegexExpression
        {
            get
            {
                return _regexExpression;
            }
            set
            {
                if (_regexExpression == value)
                    return;
                _regexExpression = value;
            }
        }

        /// <summary>
        /// 表达式文本
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "表达式文本长度不能超过4000位！")]
        public string ExpressionText
        {
            get
            {
                return _expressionText;
            }
            set
            {
                if (_expressionText == value)
                    return;
                _expressionText = value;
            }
        }

        /// <summary>
        /// 字段设置
        /// </summary>
        public long DataFieldSetting
        {
            get
            {
                return _dataFieldSetting;
            }
            set
            {
                if (_dataFieldSetting == value)
                    return;
                _dataFieldSetting = value;
            }
        }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool RequiredDataField
        {
            get
            {
                return _requiredDataField;
            }
            set
            {
                if (_requiredDataField == value)
                    return;
                _requiredDataField = value;
            }
        }

        /// <summary>
        /// 自动完成
        /// </summary>
        public bool AutoComplete
        {
            get
            {
                return _autoComplete;
            }
            set
            {
                if (_autoComplete == value)
                    return;
                _autoComplete = value;
            }
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        public bool IndexCreated
        {
            get
            {
                return _indexCreated;
            }
            set
            {
                if (_indexCreated == value)
                    return;
                _indexCreated = value;
            }
        }

        /// <summary>
        /// 启用帮助
        /// </summary>
        public bool HelpEnabled
        {
            get
            {
                return _helpEnabled;
            }
            set
            {
                if (_helpEnabled == value)
                    return;
                _helpEnabled = value;
            }
        }

        /// <summary>
        /// 帮助内容
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "帮助内容长度不能超过4000位！")]
        public string HelpContent
        {
            get
            {
                return _helpContent;
            }
            set
            {
                if (_helpContent == value)
                    return;
                _helpContent = value;
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "提示信息长度不能超过256位！")]
        public string Tooltip
        {
            get
            {
                return _tooltip;
            }
            set
            {
                if (_tooltip == value)
                    return;
                _tooltip = value;
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