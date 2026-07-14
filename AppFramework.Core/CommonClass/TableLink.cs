//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableLink.cs
// 描述： 表之间的关系
// 作者：ChenJie 
// 编写日期：2016/08/26
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 表之间的关系
    /// </summary>
    [Serializable]
    public class TableLink
    {
        #region 定义成员变量

        private string _tableName;
        private string _dataFieldName;
        private TableJoin _tableJoinType;
        private string _coequalTableName;
        private string _alias;
        private string _coequalDataFieldName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="coequalTableName">另外一个表名</param>
        /// <param name="dataFieldName">字段名</param>
        /// <param name="tableJoinType">连接关系</param>
        public TableLink(string coequalTableName, string dataFieldName, TableJoin tableJoinType) :
            this(string.Empty, dataFieldName, tableJoinType, coequalTableName, dataFieldName, string.Empty)
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="coequalTableName">另外一个表名</param>
        /// <param name="dataFieldName">字段名</param>
        /// <param name="tableJoinType">连接关系</param>
        public TableLink(string tableName, string coequalTableName, string dataFieldName, TableJoin tableJoinType) :
            this(tableName, dataFieldName, tableJoinType, coequalTableName, dataFieldName, string.Empty)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dataFieldName">字段名</param>
        /// <param name="tableJoinType">连接关系</param>
        /// <param name="coequalTableName">另外一个表名</param>
        /// <param name="coequalDataFieldName">另外一个字段名</param>
        public TableLink(string tableName, string dataFieldName, TableJoin tableJoinType, string coequalTableName,
            string coequalDataFieldName) : this(tableName, dataFieldName, tableJoinType, coequalTableName, coequalDataFieldName, string.Empty)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dataFieldName">字段名</param>
        /// <param name="tableJoinType">连接关系</param>
        /// <param name="coequalTableName">另外一个表名</param>
        /// <param name="coequalDataFieldName">另外一个字段名</param>
        /// <param name="coequalDataFieldName">另外一个表的别名</param>
        public TableLink(string tableName, string dataFieldName, TableJoin tableJoinType, string coequalTableName,
            string coequalDataFieldName, string alias)
        {
            _tableName = tableName;
            _dataFieldName = dataFieldName;
            _tableJoinType = tableJoinType;
            _coequalTableName = coequalTableName;
            _coequalDataFieldName = coequalDataFieldName;
            _alias = alias;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                if (_tableName == value)
                    return;
                _tableName = value;
            }
        }

        /// <summary>
        /// 连接关系
        /// </summary>
        public TableJoin TableJoinType
        {
            get
            {
                return _tableJoinType;
            }
            set
            {
                if (_tableJoinType == value)
                    return;
                _tableJoinType = value;
            }
        }

        /// <summary>
        /// 字段名称
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
        /// 另外一个表名
        /// </summary>
        public string CoequalTableName
        {
            get
            {
                return _coequalTableName;
            }
            set
            {
                if (_coequalTableName == value)
                    return;
                _coequalTableName = value;
            }
        }

        /// <summary>
        /// 另外一个字段名称
        /// </summary>
        public string CoequalDataFieldName
        {
            get
            {
                return _coequalDataFieldName;
            }
            set
            {
                if (_coequalDataFieldName == value)
                    return;
                _coequalDataFieldName = value;
            }
        }

        /// <summary>
        /// 另外一个表的别名
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

        #endregion
    }
}
