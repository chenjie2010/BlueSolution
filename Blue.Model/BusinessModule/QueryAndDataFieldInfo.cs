//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: QueryAndDataFieldInfo.cs
// 描述: QueryAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>QueryAndDataFieldInfo 类</para>
    /// <para>查询语句与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class QueryAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _queryAndDataFieldId;
        private decimal _dataFieldId;
        private decimal _userQueryId;
        private decimal _tableId;
        private byte _dataFieldProperty;
        private byte _systemDataField;
        private byte _dataFieldType;
        private bool _isOutput;
        private byte _customAggregate;
        private byte _sortingType;
        private string _condition = string.Empty;
        private byte _dataFieldInnerRealtion;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public QueryAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="queryAndDataFieldId">查询字段编号</param>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="userQueryId">查询编号</param>
        ///<param name="tableId">表的编号</param>
        ///<param name="dataFieldProperty">字段属性</param>
        ///<param name="systemDataField">系统字段信息</param>
        ///<param name="dataFieldType">字段类型</param>
        ///<param name="isOutput">是否显示</param>
        ///<param name="customAggregate">分组语句</param>
        ///<param name="sortingType">排序语句</param>
        ///<param name="condition">条件语句</param>
        ///<param name="dataFieldInnerRealtion">与下一字段的关系</param>
        ///<param name="sorting">排序</param>
        public QueryAndDataFieldInfo(decimal queryAndDataFieldId, decimal dataFieldId, decimal userQueryId, decimal tableId, byte dataFieldProperty,
            byte systemDataField, byte dataFieldType, bool isOutput, byte customAggregate, byte sortingType,
            string condition, byte dataFieldInnerRealtion, int sorting)
        {
            _queryAndDataFieldId = queryAndDataFieldId;
            _dataFieldId = dataFieldId;
            _userQueryId = userQueryId;
            _tableId = tableId;
            _dataFieldProperty = dataFieldProperty;
            _systemDataField = systemDataField;
            _dataFieldType = dataFieldType;
            _isOutput = isOutput;
            _customAggregate = customAggregate;
            _sortingType = sortingType;
            _condition = condition;
            _dataFieldInnerRealtion = dataFieldInnerRealtion;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 查询字段编号
        /// </summary>
        public decimal QueryAndDataFieldId
        {
            get
            {
                return _queryAndDataFieldId;
            }
            set
            {
                if (_queryAndDataFieldId == value)
                    return;
                _queryAndDataFieldId = value;
            }
        }

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
        /// 查询编号
        /// </summary>
        public decimal UserQueryId
        {
            get
            {
                return _userQueryId;
            }
            set
            {
                if (_userQueryId == value)
                    return;
                _userQueryId = value;
            }
        }

        /// <summary>
        /// 表的编号
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
        /// 系统字段信息
        /// </summary>
        public byte SystemDataField
        {
            get
            {
                return _systemDataField;
            }
            set
            {
                if (_systemDataField == value)
                    return;
                _systemDataField = value;
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
        /// 是否显示
        /// </summary>
        public bool IsOutput
        {
            get
            {
                return _isOutput;
            }
            set
            {
                if (_isOutput == value)
                    return;
                _isOutput = value;
            }
        }

        /// <summary>
        /// 分组语句
        /// </summary>
        public byte CustomAggregate
        {
            get
            {
                return _customAggregate;
            }
            set
            {
                if (_customAggregate == value)
                    return;
                _customAggregate = value;
            }
        }

        /// <summary>
        /// 排序语句
        /// </summary>
        public byte SortingType
        {
            get
            {
                return _sortingType;
            }
            set
            {
                if (_sortingType == value)
                    return;
                _sortingType = value;
            }
        }

        /// <summary>
        /// 条件语句
        /// </summary>
        [NotNullValidator(MessageTemplate = " 条件语句不能为空")]
        [StringLengthValidator(1, 512, MessageTemplate = "条件语句长度范围在1位～512位！")]
        public string Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                if (_condition == value)
                    return;
                _condition = value;
            }
        }

        /// <summary>
        /// 与下一字段的关系
        /// </summary>
        public byte DataFieldInnerRealtion
        {
            get
            {
                return _dataFieldInnerRealtion;
            }
            set
            {
                if (_dataFieldInnerRealtion == value)
                    return;
                _dataFieldInnerRealtion = value;
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

        #endregion

    }
}