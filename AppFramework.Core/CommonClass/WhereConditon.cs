//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WhereConditon.cs
// 描述： WhereConditon 条件类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;

namespace AppFramework.Core
{
    /// <summary>
    /// Where 条件类
    /// </summary>
    [Serializable]
    public class WhereConditon
    {
        #region 定义成员变量

        private string _dataTableName;
        private string _dataFieldName;
        private string _dataFieldParameterName;
        private DbType _dataFieldDataType;
        private object _dataFieldValue;
        private DataFieldCondition _condition;
        private DataFieldInnerRealtion _realtionToPreDataField;
        private DataFieldBracket _dataFieldBracketType;
        private int _dataFieldBracketCount;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        /// <param name="dataFieldInnerRealtion">与上一个字段的关系</param>
        public WhereConditon(string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue, DataFieldCondition condition, DataFieldInnerRealtion dataFieldInnerRealtion)
            : this(string.Empty, dataFieldName, dataFieldParameterName, dataFieldDataType, dataFieldValue, condition, dataFieldInnerRealtion, DataFieldBracket.None, 0)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        public WhereConditon(string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue, DataFieldCondition condition)
            : this(string.Empty, dataFieldName, dataFieldParameterName, dataFieldDataType, dataFieldValue, condition, DataFieldInnerRealtion.None, DataFieldBracket.None, 0)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        public WhereConditon(string dataTableName, string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue, DataFieldCondition condition)
            : this(dataTableName, dataFieldName, dataFieldParameterName, dataFieldDataType, dataFieldValue, condition, DataFieldInnerRealtion.None, DataFieldBracket.None, 0)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="realtionToPreDataField">与上一个字段的关系</param>
        /// <param name="dataFieldBracketType">括号的类型</param>
        /// <param name="dataFieldBracketCount">括号的数量</param>
        public WhereConditon(string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue,
            DataFieldCondition condition, DataFieldInnerRealtion realtionToPreDataField, DataFieldBracket dataFieldBracketType, int dataFieldBracketCount)
            : this(string.Empty, dataFieldName, dataFieldParameterName, dataFieldDataType, dataFieldValue,
                condition, realtionToPreDataField, dataFieldBracketType, dataFieldBracketCount)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTableName">数据表名称</param>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="realtionToPreDataField">与上一个字段的关系</param>
        /// <param name="dataFieldBracketType">括号的类型</param>
        /// <param name="dataFieldBracketCount">括号的数量</param>
        public WhereConditon(string dataTableName, string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue,
            DataFieldCondition condition, DataFieldInnerRealtion realtionToPreDataField, DataFieldBracket dataFieldBracketType, int dataFieldBracketCount)
        {
            _dataTableName = dataTableName;
            _dataFieldName = dataFieldName;
            _dataFieldParameterName = dataFieldParameterName;
            _dataFieldDataType = dataFieldDataType;
            _dataFieldValue = dataFieldValue;
            _condition = condition;
            _realtionToPreDataField = realtionToPreDataField;
            _dataFieldBracketType = dataFieldBracketType;
            _dataFieldBracketCount = dataFieldBracketCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTableName">数据表名称</param>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="condition">条件</param>
        /// <param name="dataFieldInnerRealtion">与上一个字段的关系</param>
        public WhereConditon(string dataTableName, string dataFieldName, string dataFieldParameterName, DbType dataFieldDataType, object dataFieldValue, DataFieldCondition condition, DataFieldInnerRealtion dataFieldInnerRealtion)
            : this(dataTableName, dataFieldName, dataFieldParameterName, dataFieldDataType, dataFieldValue, condition, dataFieldInnerRealtion, DataFieldBracket.None, 0)
        {

        }

        #endregion

        #region 属性

        /// <summary>
        /// 表的名称
        /// </summary>
        public string DataTableName
        {
            get
            {
                return _dataTableName;
            }
            set
            {
                if (_dataTableName == value)
                    return;
                _dataTableName = value;
            }
        }

        /// <summary>
        /// 数据字段的名称
        /// </summary>
        public string DataFieldName
        {
            get
            {
                return _dataFieldName;
            }
            set
            {
                if (_dataFieldName == value)
                    return;
                _dataFieldName = value;
            }
        }

        /// <summary>
        /// 字段参数名称
        /// </summary>
        public string DataFieldParameterName
        {
            get
            {
                return _dataFieldParameterName;
            }
            set
            {
                if (_dataFieldParameterName == value)
                    return;
                _dataFieldParameterName = value;
            }
        }

        /// <summary>
        /// 数据字段的类型
        /// </summary>
        public DbType DataFieldDataType
        {
            get
            {
                return _dataFieldDataType;
            }
            set
            {
                if (_dataFieldDataType == value)
                    return;
                _dataFieldDataType = value;
            }
        }

        /// <summary>
        /// 数据字段的值
        /// </summary>
        public object DataFieldValue
        {
            get
            {
                return _dataFieldValue;
            }
            set
            {
                _dataFieldValue = value;
            }
        }

        /// <summary>
        /// 条件
        /// </summary>
        public DataFieldCondition Condition
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
        /// 与上一个字段的关系
        /// </summary>
        public DataFieldInnerRealtion RealtionToPreDataField
        {
            get
            {
                return _realtionToPreDataField;
            }
            set
            {
                if (_realtionToPreDataField == value)
                    return;
                _realtionToPreDataField = value;
            }
        }

        /// <summary>
        /// 括号的类型
        /// </summary>
        public DataFieldBracket DataFieldBracketType
        {
            get
            {
                return _dataFieldBracketType;
            }
            set
            {
                if (_dataFieldBracketType == value)
                    return;
                _dataFieldBracketType = value;
            }
        }

        /// <summary>
        /// 括号的数量
        /// </summary>
        public int DataFieldBracketCount
        {
            get
            {
                return _dataFieldBracketCount;
            }
            set
            {
                if (_dataFieldBracketCount == value)
                    return;
                _dataFieldBracketCount = value;
            }
        }

        #endregion
    }
}
