//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：AssociatedDataFieldInfo.cs
// 描述：AssociatedDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/11/11
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>AssociatedDataFieldInfo 类</para>
    /// <para>自定义关联字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class AssociatedDataFieldInfo
    {
        #region 内部成员变量

        private decimal _associatedDataFieldId;
        private decimal _associationId;
        private string _logicalName = string.Empty;
        private string _physicalName = string.Empty;
        private string _dataFieldCode = string.Empty;
        private byte _basedDataType;
        private int _dataLength;
        private byte _dataFieldCategory;
        private bool _isHierarchal;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public AssociatedDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        ///<param name="associationId">关联编号</param>
        ///<param name="logicalName">关联字段逻辑名称</param>
        ///<param name="physicalName">关联字段物理名称</param>
        ///<param name="dataFieldCode">关联字段编码</param>
        ///<param name="basedDataType">关联字段类型</param>
        ///<param name="dataLength">关联字段长度</param>
        ///<param name="dataFieldCategory">关联字段类别</param>
        ///<param name="isHierarchal">级联字段</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public AssociatedDataFieldInfo(decimal associatedDataFieldId, decimal associationId, string logicalName, string physicalName, string dataFieldCode,
            byte basedDataType, int dataLength, byte dataFieldCategory, bool isHierarchal, int sorting,
            string notes)
        {
            _associatedDataFieldId = associatedDataFieldId;
            _associationId = associationId;
            _logicalName = logicalName;
            _physicalName = physicalName;
            _dataFieldCode = dataFieldCode;
            _basedDataType = basedDataType;
            _dataLength = dataLength;
            _dataFieldCategory = dataFieldCategory;
            _isHierarchal = isHierarchal;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 关联编号
        /// </summary>
        public decimal AssociationId
        {
            get
            {
                return _associationId;
            }
            set
            {
                if (_associationId == value)
                    return;
                _associationId = value;
            }
        }

        /// <summary>
        /// 关联字段逻辑名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关联字段逻辑名称不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "关联字段逻辑名称长度范围在1位～32位！")]
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
        /// 关联字段物理名称
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
        /// 关联字段编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关联字段编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "关联字段编码长度范围在1位～32位！")]
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
        /// 关联字段类型
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
        /// 关联字段长度
        /// </summary>
        public int DataLength
        {
            get
            {
                return _dataLength;
            }
            set
            {
                if (_dataLength == value)
                    return;
                _dataLength = value;
            }
        }

        /// <summary>
        /// 关联字段类别
        /// </summary>
        public byte DataFieldCategory
        {
            get
            {
                return _dataFieldCategory;
            }
            set
            {
                if (_dataFieldCategory == value)
                    return;
                _dataFieldCategory = value;
            }
        }

        /// <summary>
        /// 级联字段
        /// </summary>
        public bool IsHierarchal
        {
            get
            {
                return _isHierarchal;
            }
            set
            {
                if (_isHierarchal == value)
                    return;
                _isHierarchal = value;
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