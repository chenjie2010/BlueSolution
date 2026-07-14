using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppFramework.Core;

namespace AppFramework.Reference.DataFieldLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryClauseConstructor
    {
        #region 定义私有变量

        private QueryBuilder queryBuilder;
        private int startPosition;
        private int count;

        #endregion

        #region 定义成员变量

        private string _select;
        private Dictionary<string, string> _dataFieldNameRelations;
        private string _primaryTableName;
        private string _tableClause;
        private string _where;
        private string _groupBy;
        private string _orderBy;
        private int _tableCount;

        #endregion

        #region 定义属性

        /// <summary>
        /// 选择语句
        /// </summary>
        public string Select
        {
            get
            {
                return _select;
            }
        }

        /// <summary>
        /// 字段的物理名称与逻辑名称的对应关系
        /// </summary>
        public Dictionary<string, string> DataFieldNameRelations
        {
            get
            {
                return _dataFieldNameRelations;
            }
        }

        /// <summary>
        /// 主表名称
        /// </summary>
        public string PrimaryTableName
        {
            get
            {
                return _primaryTableName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string TableClause
        {
            get
            {
                return _tableClause;
            }
        }        

        /// <summary>
        /// where 条件
        /// </summary>
        public string Where
        {
            get
            {
                return _where;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GroupBy
        {
            get
            {
                return _groupBy;
            }
        }

        /// <summary>
        /// 表的个数
        /// </summary>
        public int TableCount
        {
            get
            {
                return _tableCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OrderBy
        {
            get
            {
                return _orderBy;
            } 
        }

        #endregion

        #region 定义构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        public QueryClauseConstructor(QueryBuilder queryBuilder, int startPosition, int count)
        {
            this.queryBuilder = queryBuilder;
            this.startPosition = startPosition;
            this.count = count;
            _dataFieldNameRelations = new Dictionary<string, string>();
            _select = BuildSelectClause();            
            foreach (DictionaryEntry d in queryBuilder.CurrentTableNames)
            {
                _primaryTableName = d.Key.ToString();
                break;
            }
            _tableCount = queryBuilder.CurrentTableNames.Count;            
            _tableClause = BuildFromClause();
            _where = BuildWhereClause();
            _groupBy = BuildGroupByClause();
            _orderBy = GetOrderBy();
        }

        #endregion
       
        #region 公有方法

        /// <summary>
        /// 创建部分 SQL 语句
        /// </summary>
        /// <returns></returns>
        public string BuildPartialSqlSelectStatement(string whereClause)
        {
            //检查条件的合法性
            if (queryBuilder.QueryFields.Count == 0 || queryBuilder.CurrentTableNames.Count == 0)
            {
                return string.Empty;
            }

            //生成 SQL 语句
            StringBuilder sb = new StringBuilder();

            // select
            sb.Append("SELECT ");
            if (queryBuilder.Distinct)
            {
                sb.Append("DISTINCT ");
            }
            sb.Append(_select);

            // from
            sb.AppendFormat("\r\nFROM {0}", _tableClause);

            // where
            if ((queryBuilder.ValidWhere && _where.Length > 0) || whereClause.Length > 0)
            {
                sb.Append(" WHERE ");
            }
            if ((queryBuilder.ValidWhere && _where.Length > 0))
            {
                sb.Append(_where);
            }
            if (whereClause.Length > 0)
            {
                if ((queryBuilder.ValidWhere && _where.Length > 0))
                {
                    sb.Append(" AND ");
                }
                sb.Append(whereClause);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 创建分页的 SQL 语句
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public string BuildSqlSelectStatement(string whereClause)
        {
            string sql = string.Empty;

            //检查条件的合法性
            if (queryBuilder.QueryFields.Count == 0 || queryBuilder.CurrentTableNames.Count == 0)
            {
                return string.Empty;
            }

            bool sort = false;
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if ((CustomSorting)qf.Sorting != CustomSorting.None)
                {
                    sort = true;
                    break;
                }                
            }
            
            if (sort)
            {
                /* 排序 */
                sql = GetSortedSqlStatement(_primaryTableName, _select, _tableClause, _where, whereClause, _groupBy, 
                    _orderBy, startPosition, count);
            }
            else
            {
                /* 无排序 */
                sql = GetUnSortedSqlStatement(_primaryTableName, _select, _tableClause, _where, whereClause, _groupBy, startPosition, count);
            }

            return sql;
        }

        /// <summary>
        /// 创建重命名后的排序字段语句 
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderBy()
        {
            StringBuilder sb = new StringBuilder();
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if ((CustomSorting)qf.Sorting != CustomSorting.None)
                {
                    // add separator
                    if (sb.Length > 0)
                        sb.Append(",\r\n\t");
                    sb.Append(qf.Name);

                    // descending
                    if ((CustomSorting)qf.Sorting == CustomSorting.Descending)
                        sb.Append(" DESC");
                }
            }

            return sb.ToString();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 1. 单表、无排序, 2. 双表、无排序
        /// </summary>
        /// <param name="primaryTableName"></param>
        /// <param name="select"></param>
        /// <param name="tableClause"></param>
        /// <param name="where"></param>
        /// <param name="whereClause"></param>
        /// <param name="groupBy"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetUnSortedSqlStatement(string primaryTableName, string select, string tableClause, string where, string whereClause,  
            string groupBy, int startPosition, int count)
        {
            //生成 SQL 语句
            StringBuilder sb = new StringBuilder();

            // select
            sb.Append("SELECT ");
            if (queryBuilder.Distinct)
            {
                sb.Append("DISTINCT ");
            }
            else
            {
                if (!queryBuilder.GroupBy && count > 0)
                {
                    sb.AppendFormat("TOP {0} ", count);
                }
            }
            sb.Append(select);

            // from
            sb.AppendFormat(" FROM {0}", tableClause);

            if (startPosition == 0)
            {
                if ((queryBuilder.ValidWhere && where.Length > 0) || whereClause.Length > 0)
                {
                    sb.Append(" WHERE ");
                }
                if ((queryBuilder.ValidWhere && where.Length > 0))
                {
                    sb.Append(where);
                }
                if (whereClause.Length > 0)
                {
                    if ((queryBuilder.ValidWhere && where.Length > 0))
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(whereClause);
                }
                // group by
                if (queryBuilder.GroupBy && groupBy.Length > 0)
                {
                    sb.AppendFormat(" GROUP BY {0}", groupBy);
                }
                if (!queryBuilder.GroupBy && !queryBuilder.Distinct)
                {
                    sb.AppendFormat(" ORDER BY {0}.RecordId ", primaryTableName);
                }
            }
            else
            {
                sb.Append(" WHERE ");                
                if (queryBuilder.ValidWhere && where.Length > 0)
                {
                    sb.AppendFormat("({0}) AND ", where);
                }
                if (whereClause.Length > 0)
                {
                    sb.AppendFormat("({0}) AND ", whereClause);
                }
                sb.AppendFormat("RecordId > (SELECT MAX(RecordId) FROM (SELECT ", primaryTableName);
                if (queryBuilder.Distinct)
                {
                    sb.Append("DISTINCT ");
                }
                sb.AppendFormat("TOP {0} {1}.RecordId FROM {2}", startPosition, primaryTableName, tableClause);
                if ((queryBuilder.ValidWhere && where.Length > 0) || whereClause.Length > 0)
                {
                    sb.Append(" WHERE ");
                }
                if ((queryBuilder.ValidWhere && where.Length > 0))
                {
                    sb.Append(where);
                }
                if (whereClause.Length > 0)
                {
                    if ((queryBuilder.ValidWhere && where.Length > 0))
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(whereClause);
                }
                sb.AppendFormat(" ORDER BY {0}.RecordId) AS T) ORDER BY {0}.RecordId ", primaryTableName);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 1. 单表、有排序, 2. 双表、有排序
        /// </summary>
        /// <param name="primaryTableName"></param>
        /// <param name="select"></param>
        /// <param name="tableClause"></param>
        /// <param name="where"></param>
        /// <param name="whereClause"></param>
        /// <param name="groupBy"></param>
        /// <param name="orderBy"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetSortedSqlStatement(string primaryTableName, string select, string tableClause, string where, string whereClause, 
            string groupBy, string orderBy, int startPosition, int count)
        {
            StringBuilder sb = new StringBuilder();

            //构造查询语句
            // select
            sb.Append("SELECT ");
            if (queryBuilder.Distinct)
            {
                sb.Append("DISTINCT ");
            }
            else
            {
                if (!queryBuilder.GroupBy && count > 0)
                {
                    sb.AppendFormat("TOP {0} ", count);
                }
            }
            sb.Append(select);

            // from
            sb.AppendFormat(" FROM {0}", tableClause);

            if (startPosition == 0)
            {
                if ((queryBuilder.ValidWhere && where.Length > 0) || whereClause.Length > 0)
                {
                    sb.Append(" WHERE ");
                }
                if ((queryBuilder.ValidWhere && where.Length > 0))
                {
                    sb.Append(where);
                }
                if (whereClause.Length > 0)
                {
                    if ((queryBuilder.ValidWhere && where.Length > 0))
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(whereClause);
                }
                // group by
                if (queryBuilder.GroupBy && groupBy.Length > 0)
                {
                    sb.AppendFormat(" GROUP BY {0}", groupBy);
                }
                // order by    
                if (orderBy.Length > 0)
                {
                    sb.AppendFormat(" ORDER BY {0}, {1}.RecordId", orderBy, primaryTableName);
                }
            }
            else
            {
                sb.AppendFormat(" WHERE ");
                if (queryBuilder.ValidWhere && where.Length > 0)
                {
                    sb.AppendFormat("({0}) AND ", where);
                }
                if (whereClause.Length > 0)
                {
                    sb.AppendFormat("({0}) AND ", whereClause);
                }
                sb.AppendFormat(" {0}.RecordId NOT IN (SELECT TOP {1} {0}.RecordId FROM {2} ", primaryTableName, startPosition, tableClause);
                if ((queryBuilder.ValidWhere && where.Length > 0) || whereClause.Length > 0)
                {
                    sb.Append(" WHERE ");
                }
                if ((queryBuilder.ValidWhere && where.Length > 0))
                {
                    sb.Append(where);
                }
                if (whereClause.Length > 0)
                {
                    if ((queryBuilder.ValidWhere && where.Length > 0))
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(whereClause);
                }
                sb.AppendFormat(" ORDER BY {0}, {1}.RecordId", orderBy, primaryTableName);
                // group by
                if (queryBuilder.GroupBy && groupBy.Length > 0)
                {
                    sb.AppendFormat(" GROUP BY {0}", groupBy);
                }
                sb.AppendFormat(") ORDER BY {0}, {1}.RecordId", orderBy, primaryTableName);
                // group by
                if (queryBuilder.GroupBy && groupBy.Length > 0)
                {
                    sb.AppendFormat(" GROUP BY {0}", groupBy);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 创建 Select 语句
        /// </summary>
        /// <returns></returns>
        private string BuildSelectClause()
        {
            StringBuilder sb = new StringBuilder();
            if (!queryBuilder.GroupBy && !queryBuilder.Distinct)
            {
                foreach (DictionaryEntry d in queryBuilder.CurrentTableNames)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.AppendFormat("{0}.RecordId AS {0}_RecordId", d.Key);
                    _dataFieldNameRelations.Add(string.Format("{0}_RecordId", d.Key), string.Format("{0}_RecordId", d.Key));
                }
            }
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if (qf.Output)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    // 增加字段表达式 ("table.column" or "colexpr")
                    string item = qf.ToString(queryBuilder.GroupBy);
                    sb.Append(item);

                    // 增加别名
                    if (!_dataFieldNameRelations.ContainsKey(qf.Name))
                    {
                        _dataFieldNameRelations.Add(qf.Name, qf.Alias);
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 创建 From 语句
        /// </summary>
        /// <returns></returns>
        private string BuildFromClause()
        {
            // 创建 From 语句
            StringBuilder sb = new StringBuilder();
            //第一个 DictionaryEntry
            DictionaryEntry entry;
            //建立表之间的连接查询
            foreach (DictionaryEntry d in queryBuilder.CurrentTableNames)
            {
                if (sb.Length == 0)
                {
                    entry = d;
                    sb.AppendFormat("{0} ", d.Key);
                }
                else
                {
                    if (d.Value.ToString().Equals("Cross Join"))
                    {
                        sb.AppendFormat("{0} {1} ", d.Value, d.Key);
                    }
                    else
                    {
                        sb.AppendFormat("{0} {1} ON {2}.UserId = {3}.UserId ", d.Value, d.Key, entry.Key, d.Key);
                    }
                }
            }
            List<string> tableNames = new List<string>();
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if ((DataFieldProperty)qf.DataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                {
                    SystemDataField systemDataField = (SystemDataField)Convert.ToByte(qf.DataFieldId);
                    string tableName = DataFieldHelper.GetSystemTablePhysicalName(systemDataField);
                    if(!string.IsNullOrWhiteSpace(tableName) && !tableNames.Contains(tableName))
                    {
                        string keyName = string.Empty;
                        tableNames.Add(tableName);
                        switch(systemDataField)
                        {
                            case SystemDataField.UserActualName:
                                keyName = "UserId";
                                break;

                            case SystemDataField.UserTypeName:
                            case SystemDataField.UserTypeCode:
                                keyName = "UserTypeId";
                                break;

                            case SystemDataField.DepName:
                            case SystemDataField.DepCode:
                            case SystemDataField.DepValue:
                            case SystemDataField.DepProperty:
                            case SystemDataField.DepFstAdditionalCode:
                            case SystemDataField.DepScdAdditionalCode:
                                keyName = "DepId";
                                break;

                            default:
                                throw new ArgumentException("不支持该类型");
                        }
                        sb.AppendFormat("Inner Join {0} ON {0}.{2} = {1}.{2} ", tableName, _primaryTableName, keyName);
                    }                    
                }
            }
            //完成
            return sb.ToString();
        }

        /// <summary>
        /// 创建 Where 语句
        /// </summary>
        /// <returns></returns>
        private string BuildWhereClause()
        {
            StringBuilder sb = new StringBuilder();
            DataFieldInnerRealtion queryRealtion = DataFieldInnerRealtion.None;
            foreach (QueryField qf in queryBuilder.QueryFields)
            {                
                if (qf.Criteria.Length > 0)
                {
                    switch (queryRealtion)
                    {
                        case DataFieldInnerRealtion.And:
                            sb.Append(" \r\nAND ");
                            break;
                        case DataFieldInnerRealtion.Or:
                            sb.Append(" \r\nOR ");
                            break;
                        default:
                            break;
                    }
                    sb.Append("(");
                    sb.Append(qf.Criteria);
                    sb.Append(")");
                    queryRealtion = (DataFieldInnerRealtion)qf.QueryDataFieldRealtion;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 创建 GroupBy 语句
        /// </summary>
        /// <returns></returns>
        private string BuildGroupByClause()
        {
            // no group by? done.
            if (!queryBuilder.GroupBy)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if ((Aggregate)qf.CustomAggregate == Aggregate.GroupBy && qf.Output)
                {
                    // add separator
                    if (sb.Length > 0)
                        sb.Append(",\r\n\t");

                    // add field expression ("table.column" or "colexpr")                    
                    sb.Append(qf.ColumnName);
                }                
            }

            //// extension
            //switch (queryBuilder.CurrentGroupByExtension)
            //{
            //    case GroupByExtension.All:
            //        return "ALL " + sb.ToString();
            //    case GroupByExtension.Cube:
            //        sb.Append(" WITH CUBE");
            //        break;
            //    case GroupByExtension.Rollup:
            //        sb.Append(" WITH ROLLUP");
            //        break;
            //}

            // done
            return sb.ToString();
        }

        /// <summary>
        /// 创建 OrderBy 语句 
        /// </summary>
        /// <returns></returns>
        private string GetOrderBy()
        {
            StringBuilder sb = new StringBuilder();
            foreach (QueryField qf in queryBuilder.QueryFields)
            {
                if ((CustomSorting)qf.Sorting != CustomSorting.None)
                {
                    // add separator
                    if (sb.Length > 0)
                        sb.Append(",\r\n\t");

                    // add ORDER BY expression ("table.column" or "colexpr")
                    sb.Append(qf.ColumnName);

                    // descending
                    if ((CustomSorting)qf.Sorting == CustomSorting.Descending)
                        sb.Append(" DESC");
                }
            }



            return sb.ToString();
        }

        #endregion
    }
}
