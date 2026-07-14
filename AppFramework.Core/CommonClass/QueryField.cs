//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: QueryField.cs
// 描述: QueryField 查询类
// 作者：ChenJie 
// 编写日期：2011-8-5
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询类
    /// </summary>
    [Serializable]
    public class QueryField : ICloneable, IDataErrorInfo
    {
        #region 定义成员变量
        private decimal _dataFieldId;  //字段编号
        private byte _dataFieldType;  //字段类型
        private string _name;	      //字段名称(惟一的,系统自动生成的)，逻辑字段还是物理字段都有一个自动的名称；对于系统字段则为“物理表名_系统字段名称”
        private string _columnName;	      //物理列名(有重复，真实的物理名称)，其中逻辑字段的物理名称则是其相关的物理字段的物理名称组成
        private string _alias;		  //逻辑名称
        private decimal _dataTableId; //表格编号
        private string _dataTableName;  // 表名称
        private string _dataTableAlias;  // 表的别称
        private bool _output;	      // 是否显示在 SELECT 语句中
        private byte _customAggregate;  // 分组语句
        private byte _sorting;		      // 排序语句
        private string _criteria;	  // WHERE 语句
        private byte _queryDataFieldRealtion = (byte)DataFieldInnerRealtion.And;//与下一字段的关系
        private byte _authorityType;
        private DataFieldProperty _dataFieldProperty;

        #endregion

        #region 定义构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="dataTableId">表的编号</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataTableAlias">表的别称</param>
        /// <param name="name">默认的字段名称(惟一的)</param>  
        /// <param name="columnName">列名</param>        
        /// <param name="alias">列的别名</param>
        /// <param name="authorityType">权限类型</param>
        /// <param name="dataFieldProperty">字段属性</param>
        public QueryField(decimal dataFieldId, byte dataFieldType, decimal dataTableId, string dataTableName, string dataTableAlias,
            string name, string columnName, string alias, byte authorityType, DataFieldProperty dataFieldProperty)
        {
            _dataFieldId = dataFieldId;
            _dataFieldType = dataFieldType;
            _dataTableId = dataTableId;
            _dataTableName = dataTableName;
            _dataTableAlias = dataTableAlias;
            _name = name;
            _columnName = columnName;
            _alias = alias;
            _output = true;
            _criteria = string.Empty;
            _authorityType = authorityType;
            _dataFieldProperty = dataFieldProperty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="dataTableId">表的编号</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataTableAlias">表的别称</param>
        /// <param name="name">默认的字段名称(惟一的)</param>  
        /// <param name="columnName">列名</param>        
        /// <param name="alias">列的别名</param>
        /// <param name="authorityType">权限类型</param>
        /// <param name="dataFieldProperty">字段属性</param>
        /// <param name="criteria">WHERE条件</param>
        /// <param name="queryDataFieldRealtion">与下一字段关系</param>
        public QueryField(decimal dataFieldId, byte dataFieldType, decimal dataTableId, string dataTableName, string dataTableAlias,
            string name, string columnName, string alias, byte authorityType, DataFieldProperty dataFieldProperty, string criteria, byte queryDataFieldRealtion)
            : this(dataFieldId, dataFieldType, dataTableId, dataTableName, dataTableAlias,
                name, columnName, alias, authorityType, dataFieldProperty)
        {
            _criteria = criteria;
            _queryDataFieldRealtion = queryDataFieldRealtion;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="dataTableId">表的编号</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataTableAlias">表的别称</param>
        /// <param name="name">默认的字段名称(惟一的)</param>  
        /// <param name="columnName">列名</param>        
        /// <param name="alias">列的别名</param>
        /// <param name="authorityType">权限类型</param>
        /// <param name="dataFieldProperty">字段属性</param>
        /// <param name="criteria">WHERE条件</param>
        /// <param name="queryDataFieldRealtion">与下一字段关系</param>
        /// <param name="sorting">排序</param>
        /// <param name="customAggregate">分组信息枚举</param>
        public QueryField(decimal dataFieldId, byte dataFieldType, decimal dataTableId, string dataTableName, string dataTableAlias,
            string name, string columnName, string alias, byte authorityType, DataFieldProperty dataFieldProperty, string criteria,
            byte queryDataFieldRealtion, byte sorting, byte customAggregate)
            : this(dataFieldId, dataFieldType, dataTableId, dataTableName, dataTableAlias,
                name, columnName, alias, authorityType, dataFieldProperty)
        {
            _sorting = sorting;
            _criteria = criteria;
            _queryDataFieldRealtion = queryDataFieldRealtion;
            _customAggregate = customAggregate;
        }

        #endregion

        #region 属性

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
        /// 默认的字段名称(惟一的)
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get
            {
                return _columnName;
            }
            set
            {
                if (_columnName == value)
                    return;
                _columnName = value;
            }
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get
            {
                return _alias;
            }
            set
            {
                if (_alias == value)
                    return;
                _alias = value;
            }
        }  
        
        /// <summary>
        /// 表的编号
        /// </summary>
        public decimal DataTableId
        {
            get
            {
                return _dataTableId;
            }
            set
            {
                if (_dataTableId == value)
                    return;
                _dataTableId = value;
            }
        }

        /// <summary>
        /// 表名
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
        /// 表的别名
        /// </summary>
        public string DataTableAlias
        {
            get
            {
                return _dataTableAlias;
            }
            set
            {
                if (_dataTableAlias == value)
                    return;
                _dataTableAlias = value;
            }
        }

        /// <summary>
        /// 是否显示在 SELECT 语句中
        /// </summary>
        public bool Output
        {
            get
            {
                return _output;
            }
            set
            {
                if (_output == value)
                    return;
                _output = value;
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
        public byte Sorting
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
        /// WHERE 语句
        /// </summary>
        public string Criteria
        {
            get
            {
                if (_criteria == null)
                {
                    _criteria = string.Empty;
                }
                return _criteria;
            }
            set
            {
                if (_criteria == value)
                    return;
                _criteria = value;
            }
        }

        /// <summary>
        /// 与本字段的关系
        /// </summary>
        public byte QueryDataFieldRealtion
        {
            get
            {
                return _queryDataFieldRealtion;
            }
            set
            {
                if (_queryDataFieldRealtion == value)
                    return;
                _queryDataFieldRealtion = value;
            }
        }

        /// <summary>
        /// 字段权限
        /// </summary>
        public byte AuthorityType
        {
            get
            {
                return _authorityType;
            }
            set
            {
                if (_authorityType == value)
                    return;
                _authorityType = value;
            }            
        }

        /// <summary>
        /// 字段属性
        /// </summary>
        public DataFieldProperty DataFieldProperty
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
        
        #endregion

        #region 实现接口方法
        #region IConeable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region IDataErrorInfo
        string IDataErrorInfo.this[string columnName]
        {
            get { return string.Empty; }
        }

        string IDataErrorInfo.Error
        {
            get { return null; } //错误信息; }
        }
        #endregion
        #endregion

        #region 重载方法
        public string ToString(bool groupBy)
        {
            string str = ToString();

            if (groupBy == false)
            {
                return ToString();
            }

            string fmt = string.Empty;
            Aggregate aggregate = (Aggregate)CustomAggregate;
            switch (aggregate)
            {
                case Aggregate.Sum:
                    fmt = "SUM({0}) AS {1}";
                    break;
                case Aggregate.Avg:
                    fmt = "AVG({0}) AS {1}";
                    break;
                case Aggregate.Min:
                    fmt = "MIN({0}) AS {1}";
                    break;
                case Aggregate.Max:
                    fmt = "MAX({0}) AS {1}";
                    break;
                case Aggregate.Count:
                    fmt = "COUNT({0}) AS {1}";
                    break;
                //case Aggregate.SumDistinct:
                //    fmt = "SUM(DISTINCT {0}) AS {1}";
                //    break;
                //case Aggregate.AvgDistinct:
                //    fmt = "AVG(DISTINCT {0}) AS {1}";
                //    break;
                //case Aggregate.MinDistinct:
                //    fmt = "MIN(DISTINCT {0}) AS {1}";
                //    break;
                //case Aggregate.MaxDistinct:
                //    fmt = "MAX(DISTINCT {0}) AS {1}";
                //    break;
                //case Aggregate.CountDistinct:
                //    fmt = "COUNT(DISTINCT {0}) AS {1}";
                //    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(fmt))
            {
                switch (_dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        str = string.Format(fmt, _name, _name);
                        break;

                    case DataFieldProperty.SystemPhysicalDataField:
                    case DataFieldProperty.LogicalDataField:
                        str = string.Format(fmt, _columnName, _name);
                        break;                        
                }
            }
            return str;

        }

        public override string ToString()
        {
            string result = string.Empty;

            switch (_dataFieldProperty)
            {
                case DataFieldProperty.PhysicalDataField:
                    result = _name;
                    break;

                case DataFieldProperty.LogicalDataField:
                case DataFieldProperty.SystemPhysicalDataField:
                    result = string.Format("{0} AS {1}", _columnName, _name);
                    break;

                //case DataFieldProperty.SystemPhysicalDataField:
                //    result = string.Format("{0}.{1} AS {2}", _dataTableName, _columnName, _name);
                //    break;
            }

            return result;
        }

        #endregion
    }
}
