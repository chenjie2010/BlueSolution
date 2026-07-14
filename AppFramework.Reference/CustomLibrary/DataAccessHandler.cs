//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAccessHandler.cs
// 描述: 数据库访问层的帮助类
// 作者：ChenJie 
// 编写日期：2016-07-17
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 数据库访问层的帮助类
    /// </summary>
    public class DataAccessHandler
    {
        #region 获取表某个字段的值

        /// <summary>
        /// 获取表某个字段的最大值
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表的名称</param>
        /// <param name="dataFieldName">字段的名称</param>      
        /// <param name="condition">条件字段名称</param>
        /// <param name="conditionValue">条件字段的值</param>  
        /// <param name="defaultValue">默认值</param>  
        /// <returns>值</returns>
        public static int GetValueOfDataField(SqlDatabase db, string tableName, string dataFieldName, string condition, decimal conditionValue, int defaultValue)
        {
            int value = defaultValue;
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(dataFieldName))
            {
                bool isNull = DataConvertionHelper.IsNullValue(conditionValue);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT {0} FROM {1} ", dataFieldName, tableName);
                if (!string.IsNullOrEmpty(condition))
                {
                    sb.Append(" WHERE ");
                    sb.Append(condition);
                    if (isNull)
                    {
                        sb.Append(" IS NULL");
                    }
                    else
                    {
                        sb.Append(" = @");
                        sb.Append(condition);
                    }
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (!isNull)
                        {
                            db.AddInParameter(dbCommand, condition, DbType.Decimal, conditionValue);
                        }
                    }
                    object obj = db.ExecuteScalar(dbCommand);
                    if (obj != null && obj != DBNull.Value)
                    {
                        value = (int)obj;
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// 获取表某个字段的最大值
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表的名称</param>
        /// <param name="dataFieldName">字段的名称</param>      
        /// <param name="condition">条件字段名称</param>
        /// <param name="conditionValue">条件字段的值</param>  
        /// <param name="defaultValue">默认值</param>  
        /// <returns>最大值</returns>
        public static int GetMaxValueOfDataField(SqlDatabase db, string tableName, string dataFieldName, string condition, decimal conditionValue, int defaultValue)
        {
            int maxValue = defaultValue;
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(dataFieldName))
            {
                bool isNull = DataConvertionHelper.IsNullValue(conditionValue);
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT MAX(");
                sb.Append(dataFieldName);
                sb.Append(") FROM ");
                sb.Append(tableName);
                if (!string.IsNullOrEmpty(condition))
                {
                    sb.Append(" WHERE ");
                    sb.Append(condition);
                    if (isNull)
                    {
                        sb.Append(" IS NULL");
                    }
                    else
                    {
                        sb.Append(" = @");
                        sb.Append(condition);
                    }
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        if (!isNull)
                        {
                            db.AddInParameter(dbCommand, condition, DbType.Decimal, conditionValue);
                        }
                    }
                    object obj = db.ExecuteScalar(dbCommand);
                    if (obj != null && obj != DBNull.Value)
                    {
                        maxValue = (int)obj;
                    }
                }
            }

            return maxValue;
        }

        /// <summary>
        /// 获取表某个字段的最大值
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表的名称</param>
        /// <param name="dataFieldName">字段的名称</param>      
        /// <param name="condition">条件字段名称</param>
        /// <param name="conditionValue">条件字段一的值</param>  
        /// <param name="otherCondition">条件字段二的名称</param>  
        /// <param name="conditionValue">条件字段二的值</param>  
        /// <param name="defaultValue">默认值</param>  
        /// <returns>最大值</returns>
        public static int GetMaxValueOfDataField(SqlDatabase db, string tableName, string dataFieldName, string condition, decimal conditionValue, string otherCondition, byte otherValue, int defaultValue)
        {
            int maxValue = defaultValue;
            if (!string.IsNullOrEmpty(tableName) && !string.IsNullOrEmpty(dataFieldName))
            {
                bool isNull = DataConvertionHelper.IsNullValue(conditionValue);
                bool testNull = DataConvertionHelper.IsNullValue(otherValue);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT MAX({0}) FROM {1}", dataFieldName, tableName);
                if (!string.IsNullOrEmpty(condition) || !string.IsNullOrEmpty(otherCondition))
                {
                    sb.Append(" WHERE ");
                }
                if (!string.IsNullOrEmpty(condition))
                {
                    if (isNull)
                    {
                        sb.AppendFormat(" {0} IS NULL", condition);
                    }
                    else
                    {
                        sb.AppendFormat(" {0} =  @{0}", condition);
                    }
                }
                if (!string.IsNullOrEmpty(otherCondition))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        sb.Append(" AND ");
                    }
                    if (testNull)
                    {
                        sb.AppendFormat(" {0} IS NULL", otherCondition);
                    }
                    else
                    {
                        sb.AppendFormat(" {0} =  @{0}", otherCondition);
                    }
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (!string.IsNullOrEmpty(condition) && !isNull)
                    {
                        db.AddInParameter(dbCommand, condition, DbType.Decimal, conditionValue);
                    }
                    if (!string.IsNullOrEmpty(otherCondition) && !testNull)
                    {
                        db.AddInParameter(dbCommand, otherCondition, DbType.Byte, otherValue);
                    }
                    object obj = db.ExecuteScalar(dbCommand);
                    if (obj != null && obj != DBNull.Value)
                    {
                        maxValue = (int)obj;
                    }
                }
            }

            return maxValue;
        }

        #endregion

        #region 获得单表的记录数目

        /// <summary>
        /// 获得单表的记录数目
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称</param>
        /// <param name="dataFieldName">字段的名称</param>  
        /// <param name="distinct">是否清除相同的记录</param>
        /// <param name="where">查询条件</param>
        /// <param name="whereConditons">查询字段条件的集合</param>        
        /// <returns>记录数</returns>
        public static int GetRecordCount(SqlDatabase db, string tableName, string dataFieldName, bool distinct,  string where, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            sb.Append("SELECT COUNT(");
            if (distinct)
            {
                sb.Append("DISTINCT(");
                sb.Append(dataFieldName);
                sb.Append(")");
            }
            else
            {
                sb.Append("1");
            }
            sb.Append(") FROM ");
            sb.Append(tableName);
            if (!string.IsNullOrEmpty(conditionSentence))
            {
                sb.AppendFormat(" WHERE {0}", conditionSentence);
                if (!string.IsNullOrEmpty(where))
                {
                    sb.AppendFormat(" AND ({0})", where);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(where))
                {
                    sb.AppendFormat(" WHERE {0}", where);
                }
            }

            //获得系统数据库对象
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }

            return count;
        }

        /// <summary>
        /// 获得单表的记录数目
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称</param>
        /// <param name="dataFieldName">字段的名称</param>  
        /// <param name="distinct">是否清除相同的记录</param>
        /// <param name="whereConditons">查询字段条件的集合</param>        
        /// <returns>记录数</returns>
        public static int GetRecordCount(SqlDatabase db, string tableName, string dataFieldName, bool distinct, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            sb.Append("SELECT COUNT(");
            if (distinct)
            {
                sb.Append("DISTINCT(");
                sb.Append(dataFieldName);
                sb.Append(")");
            }
            else
            {
                sb.Append("1");
            }
            sb.Append(") FROM ");
            sb.Append(tableName);
            if (!string.IsNullOrEmpty(conditionSentence))
            {
                sb.Append(" WHERE ");
                sb.Append(conditionSentence);
            }

            //获得系统数据库对象
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }

            return count;
        }

        /// <summary>
        /// 获得单表的记录数目
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称</param>
        /// <param name="tableLinks">表链接集合</param>
        /// <param name="whereConditons">查询字段条件的集合</param>        
        /// <returns>记录数</returns>
        public static int GetRecordCount(SqlDatabase db, string tableName, IList<TableLink> tableLinks, IList<WhereConditon> whereConditons)
        {

            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }

            return GetRecordCount(db, tableNames, string.Empty, false, whereConditons);
        }

        /// <summary>
        /// 获得单表的记录数目
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称</param>
        /// <param name="whereConditons">查询字段条件的集合</param>        
        /// <returns>记录数</returns>
        public static int GetRecordCount(SqlDatabase db, string tableName, IList<WhereConditon> whereConditons)
        {
            return GetRecordCount(db, tableName, string.Empty, false, whereConditons);
        }

        #endregion

        #region 获得单表的记录数目

        /// <summary>
        /// 获得该用户在记录中的排序值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static int GetNextRecordSorting(SqlDatabase db, string tablePhysicalName, decimal recordId)
        {
            int recordSorting = 0;

            string sqlSelect = string.Format("SELECT RecordSorting FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                recordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 1) + 1;
            }

            return recordSorting;
        }

        /// <summary>
        /// 获得该用户在记录中的排序值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static int GetCurrentRecordSorting(SqlDatabase db, string tablePhysicalName, decimal recordId)
        {
            int recordSorting = 0;

            string sqlSelect = string.Format("SELECT RecordSorting FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                recordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 1);
            }

            return recordSorting;
        }

        /// <summary>
        /// 获得该用户在记录中最大的排序值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetMaxRecordSorting(SqlDatabase db, string tablePhysicalName, decimal userId)
        {
            int recordSorting = 0;

            string sqlSelect = string.Format("SELECT MAX(RecordSorting) FROM {0} WHERE UserId = @UserId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                recordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0) + 1;
            }

            return recordSorting;
        }

        #endregion

        #region 分页排序算法方案一

        //----------------分页排序算法-------------------
        //方案一：
        //SELECT TOP 页大小 * 
        //FROM TestTable 
        //WHERE (ID > 
        //(SELECT MAX(id) 
        //FROM (SELECT TOP 页大小*页数 id 
        //FROM 表 
        //ORDER BY id) AS T)) 
        //ORDER BY ID 
        //-----------------------------------------------
        /// <summary>
        /// 获得单个表的分页数据集
        /// 必须要求主键，且此主键的类型必须是数字类型，以主键为排序字段
        /// 不能多字段排序
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称或是视图名称</param>
        /// <param name="identityName">数字型关键字名称</param>
        /// <param name="dataFileNames">查询的字段,以逗号分隔</param>
        /// <param name="distinct">是否清除查询出来的相同记录</param>
        /// <param name="identityNameOrder">关键字排序</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns>数据集</returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string identityName, string dataFileNames, bool distinct, bool identityNameOrder,
            int startPosition, int count, IList<WhereConditon> whereConditons, ref int recordCount)
        {
            return GetPageRecord(db, tableName, identityName, dataFileNames, distinct, identityNameOrder, null, startPosition,
                    count, whereConditons, ref recordCount);
        }

        /// <summary>
        /// 获得多个表的分页数据集
        /// 第一个表必须要求主键，且此主键的类型必须是数字类型，以主键为排序字段
        /// 与多个表连接查询后结果中，第一个表的主键仍是唯一的
        /// 不能多字段排序
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称或是视图名称</param>
        /// <param name="identityName">数字型关键字名称</param>
        /// <param name="dataFileNames">查询的字段,以逗号分隔</param>
        /// <param name="distinct">是否清除查询出来的相同记录</param>
        /// <param name="identityNameOrder">关键字排序</param>
        /// <param name="tableLinks">表之间的关系</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns>数据集</returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string identityName, string dataFileNames, bool distinct, bool identityNameOrder,
            IList<TableLink> tableLinks, int startPosition, int count, IList<WhereConditon> whereConditons, ref int recordCount)
        {
            DataSet ds = null;
            if (string.IsNullOrEmpty(identityName))
            {
                throw new ArgumentException("关键字名称不能为空.");
            }
            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }
            string newIdentityName = string.Format("{0}.{1}", tableName, identityName);
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(identityName))
            {
                throw new ArgumentException("关键字名称不能为空.");
            }
            string conditionSentence = GetConditionSentence(whereConditons);
            if (distinct)
            {
                recordCount = GetRecordCount(db, tableNames, newIdentityName, distinct, whereConditons);
            }
            else
            {
                recordCount = GetRecordCount(db, tableNames, identityName, distinct, whereConditons);
            }

            //构造查询语句
            if (startPosition == 0)
            {
                sb.Append("SELECT ");
                if (distinct)
                {
                    sb.Append("DISTINCT ");
                }
                if (count > 0)
                {
                    sb.Append("TOP ");
                    sb.Append(count);
                }
                if (!string.IsNullOrEmpty(dataFileNames))
                {
                    sb.Append(" ");
                    sb.Append(dataFileNames);
                }
                else
                {
                    sb.Append(" *");
                }
                sb.Append(" FROM ");
                sb.Append(tableNames);
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(" WHERE ");
                    sb.Append(conditionSentence);
                }
                sb.Append(" ORDER BY ");
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC ");
                }
                else
                {
                    sb.Append(" DESC ");
                }
            }
            else
            {
                sb.Append("SELECT ");
                if (distinct)
                {
                    sb.Append("DISTINCT ");
                }
                sb.Append("TOP ");
                sb.Append(count);
                if (!string.IsNullOrEmpty(dataFileNames))
                {
                    sb.Append(" ");
                    sb.Append(dataFileNames);
                }
                else
                {
                    sb.Append(" *");
                }
                sb.Append(" FROM ");
                sb.Append(tableNames);
                sb.Append(" WHERE ");
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append("(");
                    sb.Append(conditionSentence);
                    sb.Append(") AND ");

                }
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" > (SELECT MAX(");
                }
                else
                {
                    sb.Append(" < (SELECT MIN(");
                }
                sb.Append(identityName);
                sb.Append(") FROM (SELECT ");
                if (distinct)
                {
                    sb.Append("DISTINCT ");
                }
                sb.Append("TOP ");
                sb.Append(startPosition);
                sb.Append(" ");
                sb.Append(newIdentityName);
                sb.Append(" FROM ");
                sb.Append(tableNames);
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(" WHERE ");
                    sb.Append(conditionSentence);
                }
                sb.Append(" ORDER BY ");
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC");
                }
                else
                {
                    sb.Append(" DESC");
                }
                sb.Append(") AS T) ORDER BY ");
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC");
                }
                else
                {
                    sb.Append(" DESC");
                }
            }

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        #endregion

        #region 分页排序算法方案二

        //----------------分页排序算法-------------------
        //方案二：
        //SELECT TOP 页大小 * 
        //FROM TestTable 
        //WHERE (ID NOT IN 
        //(SELECT TOP 页大小*页数 id 
        //FROM 表 
        //ORDER BY id)) 
        //ORDER BY ID 
        //-----------------------------------------------
        /// <summary>
        /// 获得单个表的分页数据集
        /// 必须要求主键，主键的类型可以任意
        /// 可以多字段排序，多个字段都可以有自己单独的排序方式
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称或是视图名称</param>
        /// <param name="identityName">数字型关键字名称</param>
        /// <param name="dataFileNames">查询的字段,以逗号分隔</param>
        /// <param name="distinct">是否清除查询出来的相同记录</param>
        /// <param name="identityNameOrder">关键字排序</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string identityName, string dataFileNames, bool distinct, bool identityNameOrder,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int recordCount)
        {
            if (string.IsNullOrEmpty(identityName))
            {
                throw new ArgumentException("关键字名称不能为空.");
            }

            return GetPageRecord(db, tableName, identityName, dataFileNames, distinct, identityNameOrder, null, startPosition,
                    count, whereConditons, sortingCondtions, ref recordCount);
        }

        /// <summary>
        /// 获得多个表的分页数据集
        /// 必须要求主键，主键的类型可以任意
        /// 可以多字段排序，多个字段都可以有自己单独的排序方式
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称或是视图名称</param>
        /// <param name="identityName">数字型关键字名称</param>
        /// <param name="dataFileNames">查询的字段,以逗号分隔</param>
        /// <param name="distinct">是否清除查询出来的相同记录</param>
        /// <param name="identityNameOrder">关键字排序</param>
        /// <param name="tableLinks">表之间的关系</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string identityName, string dataFileNames, bool distinct, bool identityNameOrder,
            IList<TableLink> tableLinks, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int recordCount)
        {
            DataSet ds = null;
            if (string.IsNullOrEmpty(identityName))
            {
                throw new ArgumentException("关键字名称不能为空.");
            }

            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }
            string newIdentityName = string.Format("{0}.{1}", tableName, identityName);
            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            string sortingSentence = GetSortingSentence(sortingCondtions);

            if (string.IsNullOrEmpty(identityName))
            {
                throw new ArgumentException("关键字名称不能为空.");
            }
            recordCount = GetRecordCount(db, tableNames, identityName, distinct, whereConditons);

            //构造查询语句
            if (startPosition == 0)
            {
                sb.Append("SELECT ");
                if (distinct)
                {
                    sb.Append("DISTINCT ");
                }
                if (count > 0)
                {
                    sb.Append("TOP ");
                    sb.Append(count);
                }
                if (!string.IsNullOrEmpty(dataFileNames))
                {
                    sb.Append(" ");
                    sb.Append(dataFileNames);
                }
                else
                {
                    sb.Append(" *");
                }
                sb.Append(" FROM ");
                sb.Append(tableNames);
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(" WHERE ");
                    sb.Append(conditionSentence);
                }
                sb.Append(" ORDER BY ");
                if (!string.IsNullOrEmpty(sortingSentence))
                {
                    sb.Append(sortingSentence);
                    sb.Append(", ");
                }
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC ");
                }
                else
                {
                    sb.Append(" DESC ");
                }
            }
            else
            {
                sb.Append("SELECT ");
                if (distinct)
                {
                    sb.Append("DISTINCT ");
                }
                sb.Append("TOP ");
                sb.Append(count);
                if (!string.IsNullOrEmpty(dataFileNames))
                {
                    sb.Append(" ");
                    sb.Append(dataFileNames);
                }
                else
                {
                    sb.Append(" *");
                }
                sb.Append(" FROM ");
                sb.Append(tableNames);
                sb.Append(" WHERE ");
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(conditionSentence);
                    sb.Append(" AND ");
                }
                sb.Append(newIdentityName);
                sb.Append(" NOT IN (SELECT TOP ");
                sb.Append(startPosition);
                sb.Append(" ");
                sb.Append(newIdentityName);
                sb.Append(" FROM ");
                sb.Append(tableNames);
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(" WHERE ");
                    sb.Append(conditionSentence);
                }
                sb.Append(" ORDER BY ");
                if (!string.IsNullOrEmpty(sortingSentence))
                {
                    sb.Append(sortingSentence);
                    sb.Append(", ");
                }
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC ");
                }
                else
                {
                    sb.Append(" DESC ");
                }
                sb.Append(") ORDER BY ");
                if (!string.IsNullOrEmpty(sortingSentence))
                {
                    sb.Append(sortingSentence);
                    sb.Append(", ");
                }
                sb.Append(newIdentityName);
                if (identityNameOrder)
                {
                    sb.Append(" ASC ");
                }
                else
                {
                    sb.Append(" DESC ");
                }
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        #endregion

        #region 分页排序算法方案三

        /// <summary>
        /// 支持SQL SERVER 2012 版本以上的分页查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFileNames"></param>
        /// <param name="distinct"></param>
        /// <param name="tableLinks"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortedDataFileNames"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string dataFileNames, bool distinct, IList<TableLink> tableLinks, int startPosition, int count,
            IList<WhereConditon> whereConditons, string sortedDataFileNames, ref int recordCount)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }

            ///获取记录组数
            recordCount = GetRecordCount(db, tableNames, whereConditons);

            //构造查询语句 
            sb.Append("SELECT ");
            if (distinct)
            {
                sb.Append("DISTINCT ");
            }
            if (!string.IsNullOrEmpty(dataFileNames))
            {
                sb.Append(" ");
                sb.Append(dataFileNames);
            }
            else
            {
                sb.Append(" *");
            }
            sb.Append(" FROM ");
            sb.Append(tableNames);
            if (!string.IsNullOrEmpty(conditionSentence))
            {
                sb.Append(" WHERE ");
                sb.Append(conditionSentence);
            }
            sb.Append(" ORDER BY ");
            if (!string.IsNullOrEmpty(sortedDataFileNames))
            {
                sb.Append(sortedDataFileNames);
            }
            else
            {
                throw new ArgumentNullException("排序字段不能为空。");
            }
            sb.AppendFormat(" OFFSET {0} ROW  FETCH NEXT {1} ROW ONLY", startPosition, count);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        /// <summary>
        /// 支持SQL SERVER 2012 版本以上的分页查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFileNames"></param>
        /// <param name="distinct"></param>
        /// <param name="tableLinks"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="whereConditon"></param>
        /// <param name="sortedDataFileNames"></param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string dataFileNames, bool distinct, IList<TableLink> tableLinks, int startPosition, int count,
            IList<WhereConditon> whereConditons, string whereCondtion, string sortedDataFileNames)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }
            
            //构造查询语句 
            sb.Append("SELECT ");
            if (distinct)
            {
                sb.Append("DISTINCT ");
            }
            if (!string.IsNullOrEmpty(dataFileNames))
            {
                sb.Append(" ");
                sb.Append(dataFileNames);
            }
            else
            {
                sb.Append(" *");
            }
            sb.Append(" FROM ");
            sb.Append(tableNames);
            if (!string.IsNullOrEmpty(conditionSentence) || !string.IsNullOrWhiteSpace(whereCondtion))
            {
                sb.Append(" WHERE ");
                if (!string.IsNullOrEmpty(conditionSentence))
                {
                    sb.Append(conditionSentence);
                }
                if (!string.IsNullOrWhiteSpace(whereCondtion))
                {
                    if (!string.IsNullOrEmpty(conditionSentence))
                    {
                        sb.Append(" AND ");
                    }
                    sb.Append(whereCondtion);
                }
            }
            sb.Append(" ORDER BY ");
            if (!string.IsNullOrEmpty(sortedDataFileNames))
            {
                sb.Append(sortedDataFileNames);
            }
            else
            {
                throw new ArgumentNullException("排序字段不能为空。");
            }
            if (count > 0)
            {
                sb.AppendFormat(" OFFSET {0} ROW  FETCH NEXT {1} ROW ONLY", startPosition, count);
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        /// <summary>
        /// SELECT ShopName from Shop ORDER BY ShopName OFFSET 100000 ROW  FETCH NEXT 50 ROW ONLY
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFileNames"></param>
        /// <param name="distinct"></param>
        /// <param name="tableLinks"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string dataFileNames, bool distinct, IList<TableLink> tableLinks, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int recordCount)
        {
            string sortingSentence = GetSortingSentence(sortingCondtions);

            return GetPageRecord(db, tableName, dataFileNames, distinct, tableLinks, startPosition, count, whereConditons, sortingSentence, ref recordCount);
        }

        #endregion

        #region 单表分页和多表分页排序算法

        /// <summary>
        /// 支持SQL SERVER 2012 版本以上的分页查询
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFileNames"></param>
        /// <param name="distinct"></param>
        /// <param name="tableLinks"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static DataSet GetPageRecord(SqlDatabase db, string tableName, string dataFileNames, bool distinct, IList<TableLink> tableLinks, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            string sortingSentence = GetSortingSentence(sortingCondtions);
            string tableNames = string.Empty;
            if (tableLinks != null && tableLinks.Count > 0)
            {
                tableNames = GetTableNames(tableName, tableLinks);
            }
            else
            {
                tableNames = tableName;
            }

            //构造查询语句 
            sb.Append("SELECT ");
            if (distinct)
            {
                sb.Append("DISTINCT ");
            }
            if (!string.IsNullOrEmpty(dataFileNames))
            {
                sb.Append(" ");
                sb.Append(dataFileNames);
            }
            else
            {
                sb.Append(" *");
            }
            sb.Append(" FROM ");
            sb.Append(tableNames);
            if (!string.IsNullOrEmpty(conditionSentence))
            {
                sb.Append(" WHERE ");
                sb.Append(conditionSentence);
            }
            sb.Append(" ORDER BY ");
            if (!string.IsNullOrEmpty(sortingSentence))
            {
                sb.Append(sortingSentence);
            }
            else
            {
                throw new ArgumentNullException("排序字段不能为空。");
            }
            sb.AppendFormat(" OFFSET {0} ROW  FETCH NEXT {1} ROW ONLY", startPosition, count);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        #endregion

        #region 获得排序语句

        /// <summary>
        /// 获得排序语句
        /// </summary>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public static string GetSortingSentence(IList<SortingCondtion> sortingCondtions)
        {
            if (sortingCondtions == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (SortingCondtion sortingCondtion in sortingCondtions)
            {
                if (string.IsNullOrEmpty(sortingCondtion.DataFieldName))
                {
                    throw new ArgumentException("排序字段不能为空.");
                }
                if (!string.IsNullOrEmpty(sortingCondtion.DataTableName))
                {
                    sb.Append(sortingCondtion.DataTableName);
                    sb.Append(".");
                }
                sb.Append(sortingCondtion.DataFieldName);
                switch (sortingCondtion.CustomSorting)
                {
                    case CustomSorting.None:
                        sb.Append(",");
                        break;

                    case CustomSorting.Ascending:
                        sb.Append(" ASC,");
                        break;

                    case CustomSorting.Descending:
                        sb.Append(" DESC,");
                        break;
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        #endregion

        #region 获得 WHERE 条件

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(IList<CommonNode> commonNodes, string tableName, string dataFieldName)
        {  
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return null;
            }

            IList<WhereConditon> whereConditons = new List<WhereConditon>(commonNodes.Count);

            for (int i = 0; i < commonNodes.Count; i++)
            {
                if (i == 0)
                {
                    if (commonNodes.Count == 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNodes[i].NodeId,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNodes[i].NodeId,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    }
                }
                else if (i == commonNodes.Count - 1)
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNodes[i].NodeId,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                else
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNodes[i].NodeId,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
            }

            return whereConditons;
        }

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(IList<string> nodeCodes, string tableName, string dataFieldName)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(nodeCodes.Count);


            for (int i = 0; i < nodeCodes.Count; i++)
            {
                if (i == 0)
                {
                    if (nodeCodes.Count == 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.String, nodeCodes[i],
                            DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.String, nodeCodes[i],
                            DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    }
                }
                else if (i == nodeCodes.Count - 1)
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.String, nodeCodes[i],
                        DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                else
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.String, nodeCodes[i],
                        DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
            }

            return whereConditons;
        }

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(IList<decimal> nodeIds, string dataFieldName)
        {
            return GetWhereConditons(nodeIds, string.Empty, dataFieldName);
        }

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(IList<decimal> nodeIds, string tableName, string dataFieldName)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(nodeIds.Count);


            for (int i = 0; i < nodeIds.Count; i++)
            {
                if (i == 0)
                {
                    if (nodeIds.Count == 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, nodeIds[i],
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, nodeIds[i],
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    }
                }
                else if (i == nodeIds.Count - 1)
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, nodeIds[i],
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                else
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, nodeIds[i],
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
            }

            return whereConditons;
        }

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static List<WhereConditon> GetWhereConditons(IList<byte> nodeIds, string dataFieldName)
        {
            return GetWhereConditons(nodeIds, string.Empty, dataFieldName);
        }

        /// <summary>
        /// 获得WHERE 条件
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static List<WhereConditon> GetWhereConditons(IList<byte> nodeIds, string tableName, string dataFieldName)
        {
            List<WhereConditon> whereConditons = new List<WhereConditon>(nodeIds.Count);


            for (int i = 0; i < nodeIds.Count; i++)
            {
                if (i == 0)
                {
                    if (nodeIds.Count == 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Byte, nodeIds[i],
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Byte, nodeIds[i],
                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    }
                }
                else if (i == nodeIds.Count - 1)
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Byte, nodeIds[i],
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                else
                {
                    whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Byte, nodeIds[i],
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
            }

            return whereConditons;
        }

        #endregion

        #region 获得 WHERE 条件语句

        /// <summary>
        /// 获得 WHERE 条件语句(带值)
        /// </summary>
        /// <param name="whereConditons">查询的字段名称集合</param>
        /// <returns> WHERE 条件语句</returns>
        public static string GetWhereSentence(IList<WhereConditon> whereConditons)
        {
            if (whereConditons == null || whereConditons.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            bool fstCondition = true;
            foreach (WhereConditon whereConditon in whereConditons)
            {
                if (fstCondition)
                {
                    fstCondition = false;
                    if (whereConditon.DataFieldBracketType == DataFieldBracket.LeftBracket)
                    {
                        int count = whereConditon.DataFieldBracketCount;
                        do
                        {
                            sb.Append("(");
                            count--;
                        } while (count > 0);

                    }
                }
                else
                {
                    switch (whereConditon.RealtionToPreDataField)
                    {
                        case DataFieldInnerRealtion.And:
                            sb.Append(" AND ");

                            break;
                        case DataFieldInnerRealtion.Or:
                            sb.Append(" OR ");
                            break;

                        default:
                            break;
                    }
                    if (whereConditon.DataFieldBracketType == DataFieldBracket.LeftBracket)
                    {
                        int count = whereConditon.DataFieldBracketCount;
                        do
                        {
                            sb.Append("(");
                            count--;
                        } while (count > 0);

                    }
                }
                if (!string.IsNullOrEmpty(whereConditon.DataTableName))
                {
                    sb.Append(whereConditon.DataTableName);
                    sb.Append(".");
                }
                sb.Append(whereConditon.DataFieldName);
                switch (whereConditon.Condition)
                {
                    case DataFieldCondition.More:
                        sb.Append(" > ");
                        break;

                    case DataFieldCondition.MoreOrEqual:
                        sb.Append(" >= ");
                        break;

                    case DataFieldCondition.Less:
                        sb.Append(" < ");
                        break;

                    case DataFieldCondition.LessOrEqual:
                        sb.Append(" <= ");
                        break;

                    case DataFieldCondition.Equal:
                        /* 对值进行预处理 */
                        if ((whereConditon.DataFieldValue != null) && (whereConditon.DataFieldValue != DBNull.Value))
                        {
                            switch (whereConditon.DataFieldDataType)
                            {
                                case DbType.Decimal:
                                    whereConditon.DataFieldValue = DataConvertionHelper.SetDecimal((decimal)whereConditon.DataFieldValue);
                                    break;

                                case DbType.Int32:
                                    whereConditon.DataFieldValue = DataConvertionHelper.SetInt((int)whereConditon.DataFieldValue);
                                    break;

                                case DbType.DateTime:
                                    whereConditon.DataFieldValue = DataConvertionHelper.SetDateTime((DateTime)whereConditon.DataFieldValue);
                                    break;

                                default:
                                    break;
                            }
                        }
                        if ((whereConditon.DataFieldValue == null) || (whereConditon.DataFieldValue == DBNull.Value))
                        {
                            sb.Append(" IS NULL");
                        }
                        else
                        {
                            sb.Append(" = ");
                        }
                        break;

                    case DataFieldCondition.Like:
                        sb.Append(" LIKE ");
                        break;

                    case DataFieldCondition.Not:
                        sb.Append(" != ");
                        break;

                    default:
                        break;
                }
                if ((whereConditon.DataFieldValue != null) && (whereConditon.DataFieldValue != DBNull.Value))
                {
                    switch (whereConditon.DataFieldDataType)
                    {
                        case DbType.String:
                        case DbType.DateTime:
                            sb.AppendFormat("'{0}'", whereConditon.DataFieldValue);
                            break;

                        default:
                            sb.Append(whereConditon.DataFieldValue);
                            break;
                    }
                }
                if (whereConditon.DataFieldBracketType == DataFieldBracket.RightBracket)
                {
                    int count = whereConditon.DataFieldBracketCount;
                    do
                    {
                        sb.Append(")");
                        count--;
                    } while (count > 0);

                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得 WHERE 条件语句(带参数)
        /// </summary>
        /// <param name="whereConditons">查询的字段名称集合</param>
        /// <returns> WHERE 条件语句</returns>
        public static string GetConditionSentence(IList<WhereConditon> whereConditons)
        {
            if (whereConditons == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            bool fstCondition = true;
            foreach (WhereConditon whereConditon in whereConditons)
            {
                /* 对值进行预处理 */
                if ((whereConditon.DataFieldValue != null) && (whereConditon.DataFieldValue != DBNull.Value))
                {
                    switch (whereConditon.DataFieldDataType)
                    {
                        case DbType.Decimal:
                            whereConditon.DataFieldValue = DataConvertionHelper.SetDecimal((decimal)whereConditon.DataFieldValue);
                            break;

                        case DbType.Int32:
                            whereConditon.DataFieldValue = DataConvertionHelper.SetInt((int)whereConditon.DataFieldValue);
                            break;

                        case DbType.DateTime:
                            whereConditon.DataFieldValue = DataConvertionHelper.SetDateTime((DateTime)whereConditon.DataFieldValue);
                            break;

                        default:
                            break;
                    }
                }
                if (fstCondition)
                {
                    fstCondition = false;
                    if (whereConditon.DataFieldBracketType == DataFieldBracket.LeftBracket)
                    {
                        int count = whereConditon.DataFieldBracketCount;
                        do
                        {
                            sb.Append("(");
                            count--;
                        } while (count > 0);

                    }
                }
                else
                {
                    switch (whereConditon.RealtionToPreDataField)
                    {
                        case DataFieldInnerRealtion.And:
                            sb.Append(" AND ");

                            break;
                        case DataFieldInnerRealtion.Or:
                            sb.Append(" OR ");
                            break;

                        default:
                            break;
                    }
                    if (whereConditon.DataFieldBracketType == DataFieldBracket.LeftBracket)
                    {
                        int count = whereConditon.DataFieldBracketCount;
                        do
                        {
                            sb.Append("(");
                            count--;
                        } while (count > 0);

                    }
                }
                if (!string.IsNullOrEmpty(whereConditon.DataTableName))
                {
                    sb.Append(whereConditon.DataTableName);
                    sb.Append(".");
                }
                if (!string.IsNullOrWhiteSpace(whereConditon.DataFieldName))
                {
                    sb.Append(whereConditon.DataFieldName);
                }
                else
                {
                    sb.Append(whereConditon.DataFieldValue);
                    return sb.ToString();
                }
                switch (whereConditon.Condition)
                {
                    case DataFieldCondition.More:
                        sb.AppendFormat(" > @{0}", whereConditon.DataFieldParameterName);
                        break;

                    case DataFieldCondition.MoreOrEqual:
                        sb.AppendFormat(" >= @{0}", whereConditon.DataFieldParameterName);
                        break;

                    case DataFieldCondition.Less:
                        sb.AppendFormat(" < @{0}", whereConditon.DataFieldParameterName);
                        break;

                    case DataFieldCondition.LessOrEqual:
                        sb.AppendFormat(" <= @{0}", whereConditon.DataFieldParameterName);
                        break;

                    case DataFieldCondition.Equal:
                        if ((whereConditon.DataFieldValue == null) || (whereConditon.DataFieldValue == DBNull.Value))
                        {

                            sb.Append(" IS NULL");
                        }
                        else
                        {
                            sb.AppendFormat(" = @{0}", whereConditon.DataFieldParameterName);
                        }
                        break;

                    case DataFieldCondition.Like:
                        sb.AppendFormat(" LIKE @{0}", whereConditon.DataFieldParameterName);
                        break;

                    case DataFieldCondition.Not:
                        if ((whereConditon.DataFieldValue == null) || (whereConditon.DataFieldValue == DBNull.Value))
                        {
                            sb.Append(" IS NOT NULL");
                        }
                        else
                        {
                            sb.AppendFormat(" != @{0}", whereConditon.DataFieldParameterName);
                        }
                        break;
                }
                if (whereConditon.DataFieldBracketType == DataFieldBracket.RightBracket)
                {
                    int count = whereConditon.DataFieldBracketCount;
                    do
                    {
                        sb.Append(")");
                        count--;
                    } while (count > 0);

                }
            }

            return sb.ToString();
        }

        #endregion

        #region 通过查询条件给参数赋值

        /// <summary>
        /// 通过查询条件给参数赋值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="whereConditons"></param>
        public static void AddInParameter(SqlDatabase db, DbCommand dbCommand, IList<CommonDataField> commonDataFields)
        {
            if (commonDataFields == null)
            {
                return;
            }

            foreach (CommonDataField commonDataField in commonDataFields)
            {
                if (commonDataField.DataFieldValue == null)
                {
                    commonDataField.DataFieldValue = DBNull.Value;
                }
                AddInParameter(db, dbCommand, commonDataField.DataFieldParameterName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
            }
        }

        /// <summary>
        /// 通过查询条件给参数赋值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="queryDataFields"></param>
        public static void AddInParameter(SqlDatabase db, DbCommand dbCommand, IList<WhereConditon> whereConditons)
        {
            if (whereConditons == null)
            {
                return;
            }

            foreach (WhereConditon whereConditon in whereConditons)
            {
                if ((whereConditon.DataFieldValue == null) || (whereConditon.DataFieldValue == DBNull.Value)
                    || string.IsNullOrWhiteSpace(whereConditon.DataFieldName))
                {
                    continue;
                }
                AddInParameter(db, dbCommand, whereConditon.DataFieldParameterName, whereConditon.DataFieldDataType, whereConditon.DataFieldValue);
            }
        }

        #endregion

        #region 通过查询条件获得所有记录

        /// <summary>
        /// 通过查询条件获得所有记录
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="tableName">表名称或是视图名称</param>
        /// <param name="identityName">数字型关键字名称</param>
        /// <param name="dataFileNames">查询的字段,以逗号分隔</param>
        /// <param name="distinct">是否清除查询出来的相同记录</param> 
        /// <param name="identityNameOrder">关键字排序</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <returns></returns>
        public static DataSet GetAll(SqlDatabase db, string tableName, string identityName, string dataFileNames, bool distinct, bool identityNameOrder, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            string conditionSentence = GetConditionSentence(whereConditons);
            string sortingSentence = GetSortingSentence(sortingCondtions);

            //构造查询语句
            sb.Append("SELECT ");
            if (distinct)
            {
                sb.Append("DISTINCT ");
            }
            if (!string.IsNullOrEmpty(dataFileNames))
            {
                sb.Append(" ");
                sb.Append(dataFileNames);
            }
            else
            {
                sb.Append(" *");
            }
            sb.Append(" FROM ");
            sb.Append(tableName);
            if (!string.IsNullOrEmpty(conditionSentence))
            {
                sb.Append(" WHERE ");
                sb.Append(conditionSentence);
            }
            sb.Append(" ORDER BY ");
            if (!string.IsNullOrEmpty(sortingSentence))
            {
                sb.Append(sortingSentence);
                sb.Append(", ");
            }
            sb.Append(identityName);
            if (identityNameOrder)
            {
                sb.Append(" ASC ");
            }
            else
            {
                sb.Append(" DESC ");
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }
        #endregion

        #region 获得 sql 语句


        /// <summary>
        /// 获得 sql 语句
        /// </summary>
        /// <param name="dataFileNames"></param>
        /// <param name="tableName"></param>
        /// <param name="tableLinks"></param>
        /// <param name="whereConditons"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public static string GetSqlSentence(string dataFileNames, string tableName, IList<TableLink> tableLinks, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(dataFileNames);
            string tableNames = DataAccessHandler.GetTableNames(tableName, tableLinks);
            sb.Append(" FROM ");
            sb.Append(tableNames);
            sb.Append(" WHERE ");
            string conditionSentence = DataAccessHandler.GetConditionSentence(whereConditons);
            sb.Append(conditionSentence);
            string sortingSentence = GetSortingSentence(sortingCondtions);
            if (!string.IsNullOrEmpty(sortingSentence))
            {
                sb.Append(" ORDER BY ");
                sb.Append(sortingSentence);
            }
            return sb.ToString();
        }

        #endregion

        #region 获得表与表之间链接关系的字符串

        /// <summary>
        /// 获得表与表之间链接关系的字符串
        /// </summary>
        /// <param name="tableName">第一个主要的表的名称</param>
        /// <param name="tableLinks">表之间的关系</param>
        /// <returns></returns>
        public static string GetTableNames(string tableName, IList<TableLink> tableLinks)
        {
            if (tableLinks.Count == 0)
            {
                return tableName;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(tableName);
            foreach (TableLink tableLink in tableLinks)
            {
                switch (tableLink.TableJoinType)
                {
                    case TableJoin.InnerJoin:
                        sb.Append(" INNER JOIN ");
                        break;

                    case TableJoin.LeftOuterJoin:
                        sb.Append(" LEFT OUTER JOIN ");
                        break;

                    case TableJoin.RightOuterJoin:
                        sb.Append(" RIGHT OUTER JOIN ");
                        break;

                    case TableJoin.FullOuterJoin:
                        sb.Append(" FULL OUTER JOIN ");
                        break;
                }
                sb.Append(tableLink.CoequalTableName);
                if (!string.IsNullOrWhiteSpace(tableLink.Alias))
                {
                    sb.AppendFormat(" AS {0}", tableLink.Alias);
                }
                if (string.IsNullOrWhiteSpace(tableLink.TableName))
                {
                    tableLink.TableName = tableName;
                }
                if (!string.IsNullOrWhiteSpace(tableLink.Alias))
                {
                    sb.AppendFormat(" ON {0}.{1} = {2}.{3}", tableLink.TableName, tableLink.DataFieldName, tableLink.Alias, tableLink.CoequalDataFieldName);
                }
                else
                {
                    sb.AppendFormat(" ON {0}.{1} = {2}.{3}", tableLink.TableName, tableLink.DataFieldName, tableLink.CoequalTableName, tableLink.CoequalDataFieldName);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="dataFieldParameterName"></param>
        /// <param name="dataFieldType"></param>
        /// <param name="dataFieldValue"></param>
        public static void AddInParameter(SqlDatabase db, DbCommand dbCommand, string dataFieldParameterName, DbType dataFieldType, object dataFieldValue)
        {
            switch (dataFieldType)
            {
                case DbType.Boolean:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Boolean, dataFieldValue);
                    break;
                case DbType.String:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.String, dataFieldValue);
                    break;

                case DbType.Decimal:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Decimal, dataFieldValue);
                    break;

                case DbType.Int32:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Int32, dataFieldValue);
                    break;

                case DbType.Int64:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Int64, dataFieldValue);
                    break;


                case DbType.DateTime:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.DateTime, dataFieldValue);
                    break;

                case DbType.Byte:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Byte, dataFieldValue);
                    break;

                case DbType.Guid:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Guid, dataFieldValue);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbCommand"></param>
        /// <param name="dataFieldParameterName"></param>
        /// <param name="dataFieldType"></param>
        /// <param name="dataFieldValue"></param>
        public static void AddInParameterWithConstraint(SqlDatabase db, DbCommand dbCommand, string dataFieldParameterName, DbType dataFieldType, object dataFieldValue)
        {
            switch (dataFieldType)
            {
                case DbType.Boolean:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Boolean, DataConvertionHelper.GetBoolean(dataFieldValue));
                    break;

                case DbType.String:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.String, dataFieldValue);
                    break;

                case DbType.Decimal:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Decimal, DataConvertionHelper.SetDecimal(DataConvertionHelper.GetDecimal(dataFieldValue)));
                    break;

                case DbType.Int32:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Int32, DataConvertionHelper.SetInt(DataConvertionHelper.GetInt(dataFieldValue)));
                    break;

                case DbType.Int64:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Int64, DataConvertionHelper.SetLong(DataConvertionHelper.GetLong(dataFieldValue)));
                    break;


                case DbType.DateTime:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.DateTime, DataConvertionHelper.SetDateTime(DataConvertionHelper.GetDateTime(dataFieldValue)));
                    break;

                case DbType.Byte:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Byte, DataConvertionHelper.SetByte(DataConvertionHelper.GetByte(dataFieldValue)));
                    break;

                case DbType.Guid:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.Guid, dataFieldValue);
                    break;

                case DbType.Object:
                    db.AddInParameter(dbCommand, dataFieldParameterName, DbType.String, Convert.ToString(dataFieldValue));
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region 判断表和字段是否存在

        /// <summary>
        /// 判断字段在表中是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dataTableName"></param>
        /// <param name="dataFieldName"></param>
        /// <returns></returns>
        public static bool IsExistPhyscialDataField(SqlDatabase db, string dataTableName, string dataFieldName)
        {
            bool exist = false;

            string sqlSelect = "SELECT COUNT(Name) FROM syscolumns WHERE id =object_id(@DataTableName) AND Name = @DataFieldName";
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                db.AddInParameter(dbCommand, "DataTableName", DbType.String, dataTableName);
                db.AddInParameter(dbCommand, "DataFieldName", DbType.String, dataFieldName);
                if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                {
                    exist = true;
                }
            }

            return exist;
        }

        /// <summary>
        /// 判断数据库的表名称已经存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dataTableName"></param>
        /// <returns></returns>
        public static bool IsExistPhyscialDataTable(SqlDatabase db, string dataTableName)
        {
            bool exist = false;

            string sqlSelect = "SELECT COUNT(Name) FROM sysobjects WHERE xtype='u' and status>=0 AND Name = @DataTableName";
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                db.AddInParameter(dbCommand, "DataTableName", DbType.String, dataTableName);
                if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                {
                    exist = true;
                }
            }

            return exist;
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        public static void DeletePhysicalTable(SqlDatabase db, string tablePhysicalName)
        {
            //生成删除语句
            string sqlDelete = string.Format("if exists (select name from sysobjects where name = '{0}') DROP TABLE {0}", tablePhysicalName);

            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
            {
                db.ExecuteNonQuery(dbCommand);
            }           
        }

        /// <summary>
        /// 删除字段
        /// </summary>
        /// <param name="db"></param>
        /// <param name="physcialDataTableName"></param>
        /// <param name="dataFieldPhysicalName"></param>
        public static void DeleteDataField(SqlDatabase db, string physcialDataTableName, string dataFieldPhysicalName)
        {
            //删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("IF EXISTS (SELECT * FROM syscolumns WHERE id = object_id('{0}') and NAME = '{1}') ", physcialDataTableName, dataFieldPhysicalName);
            sb.AppendFormat("BEGIN ALTER TABLE {0} DROP COLUMN {1} END", physcialDataTableName, dataFieldPhysicalName);

            /* 删除业务字段 */
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.ExecuteNonQuery(dbCommand);
            }
        }


        #endregion

        #region 判断表和字段是否存在

        /// <summary>
        /// 检测是否含有危险字符（防止Sql注入）
        /// </summary>
        /// <param name="contents">预检测的内容</param>
        /// <returns>返回True或false</returns>
        public static bool HasDangerousContents(string contents)
        {
            bool bReturnValue = false;
            if (contents.Length > 0)
            {
                string sLowerStr = contents.ToLower();
                //RegularExpressions
                string regularExpression = @"(insert\s)| (delete\s)|(update\s[\s\S].*\sset)|(create\s)|(\stable)|(<[iframe|/iframe|script|/script])|(\sexec)|(\sdeclare)|(\struncate)|(\smaster)|(\sbackup)|(\smid)|(\scount)";
                //Match
                bool bIsMatch = false;
                System.Text.RegularExpressions.Regex sRx = new System.Text.RegularExpressions.Regex(regularExpression);
                bIsMatch = sRx.IsMatch(sLowerStr, 0);
                if (bIsMatch)
                {
                    bReturnValue = true;
                }
            }

            return bReturnValue;
        }

        #endregion

        #region  更新排序字段

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="db"></param>
        /// <param name="keyFieldId"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="keyFieldIdName"></param>
        /// <param name="dataFieldIdName"></param>
        /// <param name="tableName"></param>
        /// <param name="movedDriectionOfNode"></param>
        public static void UpdateSorting(SqlDatabase db, decimal keyFieldId, decimal dataFieldId, string keyFieldIdName, string dataFieldIdName, 
            string tableName, MovedDriection movedDriectionOfNode)
        {         
            string sqlFirstSelect = string.Empty;
            string sqlSecondSelect = string.Empty;
            string sqlFirstUpdate = string.Empty;
            string sqlSecondUpdate = string.Empty;
            decimal otherDataFieldId = 0;
            int sorting = 0;
            int otherSorting = 0;

            /* 当前节点的排序值 */
            sqlFirstSelect = string.Format("SELECT Sorting FROM {0} WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, keyFieldIdName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, dataFieldId);
                sorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
            }

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    switch (movedDriectionOfNode)
                    {
                        case MovedDriection.Top:
                            StringBuilder sbTop = new StringBuilder();
                            sbTop.AppendFormat("UPDATE {0} SET Sorting = Sorting + 1 WHERE {1} = @{1} AND Sorting < @Sorting", tableName, keyFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbTop.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = 1 WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, keyFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, dataFieldId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Previous:
                        case MovedDriection.Next:
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("SELECT TOP 1 {0}, Sorting FROM {1} WHERE {2} = @{2} AND ", dataFieldIdName, tableName, keyFieldIdName);
                            if (movedDriectionOfNode == MovedDriection.Previous)
                            {
                                sb.Append("Sorting < @Sorting ORDER BY Sorting DESC ");
                            }
                            else
                            {
                                sb.Append("Sorting > @Sorting ORDER BY Sorting ASC ");
                            }
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                                {
                                    if (dataReader.Read())
                                    {
                                        otherDataFieldId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand), 0);
                                        otherSorting = DataConvertionHelper.GetInt(dataReader[1]);
                                    }
                                    if (dataReader != null)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            if (otherDataFieldId > 0)
                            {
                                sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE  {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, keyFieldIdName);
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, dataFieldId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, otherSorting);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, otherDataFieldId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                            }
                            break;

                        case MovedDriection.Bottom:
                            StringBuilder sbBottom = new StringBuilder();
                            sbBottom.AppendFormat("SELECT MAX(Sorting) FROM {0} WHERE {1} = @{1}", tableName, keyFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                otherSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }

                            sbBottom.Clear();
                            sbBottom.AppendFormat("UPDATE {0} SET Sorting = Sorting - 1 WHERE {1} = @{1} AND Sorting > @Sorting", tableName, keyFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, keyFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, keyFieldIdName, DbType.Decimal, keyFieldId);
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, dataFieldId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, otherSorting);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        #endregion

        #region 私有静态方法



        #endregion
    }
}
