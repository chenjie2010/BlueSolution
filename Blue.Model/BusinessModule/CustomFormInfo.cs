//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomFormInfo.cs
// 描述: CustomFormInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/9/2
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomFormInfo 类</para>
    /// <para>数据窗体</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomFormInfo
    {
        #region 内部成员变量

        private decimal _formId;
        private decimal _sectionId;
        private decimal _combinedTableId;
        private decimal _tableId;
        private string _formName = string.Empty;
        private string _formCode = string.Empty;
        private byte _formType;
        private byte _systemFormType;
        private bool _businessEnabled;
        private long _formProperty;
        private byte _showMode;
        private long _dataFieldSetting;
        private bool _enableHelp;
        private string _helpContent = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomFormInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="formId">窗体编号</param>
        ///<param name="sectionId">窗体分组编号</param>
        ///<param name="combinedTableId"></param>
        ///<param name="tableId">表编号</param>
        ///<param name="formName">窗体名称</param>
        ///<param name="formCode">窗体编码</param>
        ///<param name="formType">业务表类型</param>
        ///<param name="systemFormType"></param>
        ///<param name="businessEnabled">启用业务表</param>
        ///<param name="formProperty">窗体属性</param>
        ///<param name="showMode">展现模式</param>
        ///<param name="dataFieldSetting">系统字段</param>
        ///<param name="enableHelp">启用帮助</param>
        ///<param name="helpContent">帮助内容</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomFormInfo(decimal formId, decimal sectionId, decimal combinedTableId, decimal tableId, string formName,
            string formCode, byte formType, byte systemFormType, bool businessEnabled, long formProperty,
            byte showMode, long dataFieldSetting, bool enableHelp, string helpContent, int sorting,
            string notes)
        {
            _formId = formId;
            _sectionId = sectionId;
            _combinedTableId = combinedTableId;
            _tableId = tableId;
            _formName = formName;
            _formCode = formCode;
            _formType = formType;
            _systemFormType = systemFormType;
            _businessEnabled = businessEnabled;
            _formProperty = formProperty;
            _showMode = showMode;
            _dataFieldSetting = dataFieldSetting;
            _enableHelp = enableHelp;
            _helpContent = helpContent;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 窗体编号
        /// </summary>
        public decimal FormId
        {
            get
            {
                return _formId;
            }
            set
            {
                if (_formId == value)
                    return;
                _formId = value;
            }
        }

        /// <summary>
        /// 窗体分组编号
        /// </summary>
        public decimal SectionId
        {
            get
            {
                return _sectionId;
            }
            set
            {
                if (_sectionId == value)
                    return;
                _sectionId = value;
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
        /// 窗体名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 窗体名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "窗体名称长度范围在1位～256位！")]
        public string FormName
        {
            get
            {
                return _formName;
            }
            set
            {
                if (_formName == value)
                    return;
                _formName = value;
            }
        }

        /// <summary>
        /// 窗体编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 窗体编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "窗体编码长度范围在1位～32位！")]
        public string FormCode
        {
            get
            {
                return _formCode;
            }
            set
            {
                if (_formCode == value)
                    return;
                _formCode = value;
            }
        }

        /// <summary>
        /// 业务表类型
        /// </summary>
        public byte FormType
        {
            get
            {
                return _formType;
            }
            set
            {
                if (_formType == value)
                    return;
                _formType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte SystemFormType
        {
            get
            {
                return _systemFormType;
            }
            set
            {
                if (_systemFormType == value)
                    return;
                _systemFormType = value;
            }
        }

        /// <summary>
        /// 启用业务表
        /// </summary>
        public bool BusinessEnabled
        {
            get
            {
                return _businessEnabled;
            }
            set
            {
                if (_businessEnabled == value)
                    return;
                _businessEnabled = value;
            }
        }

        /// <summary>
        /// 窗体属性
        /// </summary>
        public long FormProperty
        {
            get
            {
                return _formProperty;
            }
            set
            {
                if (_formProperty == value)
                    return;
                _formProperty = value;
            }
        }

        /// <summary>
        /// 展现模式
        /// </summary>
        public byte ShowMode
        {
            get
            {
                return _showMode;
            }
            set
            {
                if (_showMode == value)
                    return;
                _showMode = value;
            }
        }

        /// <summary>
        /// 系统字段
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
        /// 启用帮助
        /// </summary>
        public bool EnableHelp
        {
            get
            {
                return _enableHelp;
            }
            set
            {
                if (_enableHelp == value)
                    return;
                _enableHelp = value;
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