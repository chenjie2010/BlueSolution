//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: QueryBuilder.cs
// 描述: QueryBuilder 查询语句类
// 作者：ChenJie 
// 编写日期：2011-08-05
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询语句类
    /// </summary>
    [Serializable]
    public class QueryBuilder
    {
        #region 定义成员变量

        private QueryFieldCollection _queryFields;	//查询字段集合
        private OrderedDictionary _currentTableNames;		// 表集合和关系
        private bool _groupBy;		// 是否增加分组
        private bool _distinct;		// 是否消除重复记录
        private bool _validWhere; /* WHERE 条件是否有效 */

        #endregion

        #region 定义构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryBuilder()
        {
            _currentTableNames = new OrderedDictionary();
            _queryFields = new QueryFieldCollection();
            _distinct = false;
            _groupBy = false;
            _validWhere = true;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 表集合
        /// </summary>
        public OrderedDictionary CurrentTableNames
        {
            get
            {
                if (_currentTableNames == null)
                {
                    _currentTableNames = new OrderedDictionary();
                }
                return _currentTableNames;
            }
            set
            {
                if (_currentTableNames == value)
                    return;
                _currentTableNames = value;
            }
        }

        /// <summary>
        /// 查询字段集合
        /// </summary>
        public QueryFieldCollection QueryFields
        {
            get { return _queryFields; }
        }

        /// <summary>
        /// 分组
        /// </summary>
        public bool GroupBy
        {
            get { return _groupBy; }
            set
            {
                if (_groupBy != value)
                {
                    _groupBy = value;
                }
            }
        }

        /// <summary>
        /// 是否消除重复记录
        /// </summary>
        public bool Distinct
        {
            get { return _distinct; }
            set
            {
                _distinct = value;
            }
        }

        /// <summary>
        /// 是否忽略 WHERE 条件
        /// </summary>
        public bool ValidWhere
        {
            get
            {
                return _validWhere;
            }
            set
            {
                if (_validWhere == value)
                    return;
                _validWhere = value;
            }
        }

        #endregion
    }
}
