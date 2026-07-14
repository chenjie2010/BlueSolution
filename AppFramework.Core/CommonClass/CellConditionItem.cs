//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellConditionItem.cs
// 描述： 单元格条件项
// 作者：ChenJie 
// 编写日期：2018/10/07
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格条件项
    /// </summary>
    public class CellConditionItem
    {
        #region 定义私有变量

        private string _tableName;
        private IList<string> _tableNames;
        private string _dataFileNames;
        private string _fromClause;
        private string _whereClause;

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
                _tableName = value;
            }
        }

        /// <summary>
        /// 表名列表
        /// </summary>
        public IList<string> TableNames
        {
            get
            {
                return _tableNames;
            }
            set
            {
                _tableNames = value;
            }
        }
        

        /// <summary>
        /// 字段
        /// </summary>
        public string DataFileNames
        {
            get
            {
                return _dataFileNames;
            }
            set
            {
                _dataFileNames = value;
            }
        }        

        /// <summary>
        /// From 句子
        /// </summary>
        public string FromClause
        {
            get
            {
                return _fromClause;
            }
            set
            {
                _fromClause = value;
            }
        }

        /// <summary>
        /// Where 句子
        /// </summary>
        public string WhereClause
        {
            get
            {
                return _whereClause;
            }
            set
            {
                _whereClause = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CellConditionItem()
        {
            _tableName = string.Empty;
            _tableNames = new List<string>();
            _dataFileNames = string.Empty;
            _fromClause = string.Empty;
            _whereClause = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableNames"></param>
        /// <param name="dataFileNames"></param>
        /// <param name="fromClause"></param>
        /// <param name="whereClause"></param>
        public CellConditionItem(string tableName, IList<string> tableNames, string dataFileNames, string fromClause, string whereClause)
        {
            _tableName = tableName;
            _tableNames = tableNames;
            _dataFileNames = dataFileNames;
            _fromClause = fromClause;
            _whereClause = whereClause;
        }

        #endregion
    }
    }
