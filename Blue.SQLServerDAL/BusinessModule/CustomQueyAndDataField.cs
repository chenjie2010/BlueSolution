//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomQueyAndDataField.cs
// 描述：CustomQueyAndDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/31
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomQueyAndDataField 表的数据层访问类
    /// </summary>
    public class CustomQueyAndDataField : CorrelatedTableDataAcess, ICustomQueyAndDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomQueyAndDataField() : base("CustomViewAndDataField", "DataFieldId", "DataQueriedId", "DataFieldMode")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomQueyAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">customQueyAndDataFieldInfo 对象</param>
        public void Insert(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customQueyAndDataFieldInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomQueyAndDataField", "Sorting", "DataQueriedId", customQueyAndDataFieldInfo.DataQueriedId, 0) + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomQueyAndDataField(DataFieldId, DataQueriedId, DataFieldMode, DataFieldFormat, QueryAllowed, Conditions, Sorting)");
            sb.Append("VALUES (@DataFieldId, @DataQueriedId, @DataFieldMode, @DataFieldFormat, @QueryAllowed, @Conditions, @Sorting)");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customQueyAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, customQueyAndDataFieldInfo.DataQueriedId);
                    db.AddInParameter(dbCommand, "DataFieldMode", DbType.Byte, customQueyAndDataFieldInfo.DataFieldMode);
                    db.AddInParameter(dbCommand, "DataFieldFormat", DbType.String, customQueyAndDataFieldInfo.DataFieldFormat);
                    db.AddInParameter(dbCommand, "QueryAllowed", DbType.Boolean, customQueyAndDataFieldInfo.QueryAllowed);
                    db.AddInParameter(dbCommand, "Conditions", DbType.String, customQueyAndDataFieldInfo.Conditions);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customQueyAndDataFieldInfo.Sorting);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		/// 获得 CustomQueyAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyAndDataFieldInfo 对象</returns>
		public CustomQueyAndDataFieldInfo GetModelInfo(decimal dataFieldId, decimal dataQueriedId)
        {
            CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.None));
            whereConditons.Add(new WhereConditon("DataQueriedId", "DataQueriedId", DbType.Decimal, dataQueriedId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<CustomQueyAndDataFieldInfo> customQueyAndDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customQueyAndDataFieldInfos != null && customQueyAndDataFieldInfos.Count > 0)
            {
                customQueyAndDataFieldInfo = customQueyAndDataFieldInfos[0];
            }

            return customQueyAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">CustomQueyAndDataFieldInfo 对象</param>
        public void Update(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomQueyAndDataField SET DataFieldMode = @DataFieldMode, DataFieldFormat = @DataFieldFormat, QueryAllowed = @QueryAllowed, Conditions = @Conditions ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND DataQueriedId = @DataQueriedId ");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customQueyAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, customQueyAndDataFieldInfo.DataQueriedId);
                    db.AddInParameter(dbCommand, "DataFieldMode", DbType.Byte, customQueyAndDataFieldInfo.DataFieldMode);
                    db.AddInParameter(dbCommand, "DataFieldFormat", DbType.String, customQueyAndDataFieldInfo.DataFieldFormat);
                    db.AddInParameter(dbCommand, "QueryAllowed", DbType.Boolean, customQueyAndDataFieldInfo.QueryAllowed);
                    db.AddInParameter(dbCommand, "Conditions", DbType.String, customQueyAndDataFieldInfo.Conditions);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataQueriedId">数据查询编号</param>
        public void Delete(decimal dataFieldId, decimal dataQueriedId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomQueyAndDataField ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND DataQueriedId = @DataQueriedId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, dataQueriedId);
                    //执行删除操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("删除失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomQueyAndDataFieldInfo> GetModelInfos(decimal dataQueriedId)
        {

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("DataQueriedId", "DataQueriedId", System.Data.DbType.Decimal, dataQueriedId, DataFieldCondition.Equal));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomQueyAndDataFieldInfo 对象列表</returns>
        public IList<CustomQueyAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomQueyAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomQueyAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomQueyAndDataField ", "DataFieldId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetCustomDataFieldInfos(decimal dataQueriedId)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = new List<CustomDataFieldInfo>();

            //查询语句           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.* FROM CustomQueyAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE DataQueriedId = @DataQueriedId ORDER BY CustomQueyAndDataField.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[5]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[6]);
                            string dataFieldCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[8]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[9]);
                            int dataFieldLength = DataConvertionHelper.GetInt(dataReader[10]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[11]);
                            string regexExpression = DataConvertionHelper.GetString(dataReader[12]);
                            string expressionText = DataConvertionHelper.GetString(dataReader[13]);
                            long dataFieldSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool requiredDataField = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool autoComplete = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool indexCreated = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool helpEnabled = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[19]);
                            string tooltip = DataConvertionHelper.GetString(dataReader[20]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[21]);
                            string notes = DataConvertionHelper.GetString(dataReader[22]);
                            //将创建 CustomDataFieldInfo 对象加入集合中
                            customDataFieldInfos.Add(new CustomDataFieldInfo(dataFieldId, associatedDataFieldId, tableId, parentDataFieldId, enumId,
                            logicalName, physicalName, dataFieldCode, dataFieldProperty, dataFieldType,
                            dataFieldLength, basedDataType, regexExpression, expressionText, dataFieldSetting, requiredDataField,
                            autoComplete, indexCreated, helpEnabled, helpContent, tooltip,
                            sorting, notes));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }


        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetConditionalCustomDataFieldInfos(decimal dataQueriedId)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = new List<CustomDataFieldInfo>();

            //查询语句           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.* FROM CustomQueyAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE DataQueriedId = @DataQueriedId AND QueryAllowed = @QueryAllowed ORDER BY CustomQueyAndDataField.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    db.AddInParameter(dbCommand, "QueryAllowed", DbType.Boolean, true);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[5]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[6]);
                            string dataFieldCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[8]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[9]);
                            int dataFieldLength = DataConvertionHelper.GetInt(dataReader[10]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[11]);
                            string regexExpression = DataConvertionHelper.GetString(dataReader[12]);
                            string expressionText = DataConvertionHelper.GetString(dataReader[13]);
                            long dataFieldSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool requiredDataField = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool autoComplete = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool indexCreated = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool helpEnabled = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[19]);
                            string tooltip = DataConvertionHelper.GetString(dataReader[20]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[21]);
                            string notes = DataConvertionHelper.GetString(dataReader[22]);
                            //将创建 CustomDataFieldInfo 对象加入集合中
                            customDataFieldInfos.Add(new CustomDataFieldInfo(dataFieldId, associatedDataFieldId, tableId, parentDataFieldId, enumId,
                            logicalName, physicalName, dataFieldCode, dataFieldProperty, dataFieldType,
                            dataFieldLength, basedDataType, regexExpression, expressionText, dataFieldSetting, requiredDataField,
                            autoComplete, indexCreated, helpEnabled, helpContent, tooltip,
                            sorting, notes));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDigitDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.DataFieldId, LogicalName, PhysicalName, DataFieldMode FROM CustomQueyAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE DataQueriedId = @DataQueriedId ");
            IList<WhereConditon> whereConditons = DataFieldHelper.GetWhereConditons(DataFieldFilter.DigtalTypeInPhysicalField);
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" AND ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.Append("ORDER BY CustomQueyAndDataField.Sorting");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            byte dataFieldMode = DataConvertionHelper.GetByte(dataReader[3]);
                            commonNodes.Add(new CommonNode(dataFieldId, dataQueriedId, logicalName, physicalName, dataFieldMode));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetAssociatedDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.DataFieldId, LogicalName, PhysicalName, DataFieldMode FROM CustomQueyAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE DataQueriedId = @DataQueriedId ORDER BY CustomQueyAndDataField.DataFieldMode, CustomQueyAndDataField.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            byte dataFieldMode = DataConvertionHelper.GetByte(dataReader[3]);
                            commonNodes.Add(new CommonNode(dataFieldId, dataQueriedId, logicalName, physicalName, dataFieldMode));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal dataQueriedId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            string sqlFirstSelect = string.Empty;
            string sqlSecondSelect = string.Empty;
            string sqlFirstUpdate = string.Empty;
            string sqlSecondUpdate = string.Empty;
            string dataFieldIdName = "DataFieldId", dataQueriedIdName = "DataQueriedId", tableName = "CustomQueyAndDataField";
            decimal otherDataFieldId = 0;
            byte dataFieldMode = GetDataFieldMode(dataQueriedId, dataFieldId);
            int sorting = 0;
            int otherSorting = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            /* 当前节点的排序值 */
            sqlFirstSelect = string.Format("SELECT Sorting FROM {0} WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, dataQueriedIdName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
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
                            sbTop.AppendFormat("UPDATE {0} SET Sorting = Sorting + 1 WHERE {1} = @{1} AND Sorting < @Sorting", tableName, dataQueriedIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbTop.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = 1 WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, dataQueriedIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
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
                            sb.AppendFormat("SELECT TOP 1 {0}, Sorting FROM {1} WHERE {2} = @{2} AND DataFieldMode = @DataFieldMode AND ", dataFieldIdName, tableName, dataQueriedIdName);
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
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
                                db.AddInParameter(dbCommand, "DataFieldMode", DbType.Byte, dataFieldMode);
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
                                sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE  {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, dataQueriedIdName);
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
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
                                    db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
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
                            sbBottom.AppendFormat("SELECT MAX(Sorting) FROM {0} WHERE {1} = @{1}", tableName, dataQueriedIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
                                otherSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }

                            sbBottom.Clear();
                            sbBottom.AppendFormat("UPDATE {0} SET Sorting = Sorting - 1 WHERE {1} = @{1} AND Sorting > @Sorting", tableName, dataQueriedIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE {1} = @{1} AND {2} = @{2}", tableName, dataFieldIdName, dataQueriedIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataQueriedIdName, DbType.Decimal, dataQueriedId);
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

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public DataSet GetCustomQueyAndDataFieldInfos(decimal dataQueriedId)
        {
            DataSet ds = null;
            //查询语句           
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomQueyAndDataField.DataFieldId, CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName AS TableLogicalName,");
            sb.Append("CustomDataField.LogicalName AS DataFieldLogicalName, CustomQueyAndDataField.DataFieldMode, CustomQueyAndDataField.DataFieldFormat, ");
            sb.Append("CustomQueyAndDataField.QueryAllowed, CustomQueyAndDataField.Conditions FROM CustomQueyAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE DataQueriedId = @DataQueriedId ORDER BY CustomQueyAndDataField.DataFieldMode, CustomQueyAndDataField.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CustomQueyAndDataField FROM CustomQueyAndDataField INNER JOIN CustomDataField ");
            sb.Append("ON CustomQueyAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomDataField.TableId = @TableId ");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        ///// <summary>
        ///// 更新查询中的字段
        ///// </summary>
        ///// <param name="dataQueriedId"></param>
        ///// <param name="commonNodes"></param>
        ///// <param name="db"></param>
        ///// <param name="transaction"></param>
        //public void UpdateDataFileds(decimal dataQueriedId, IList<CustomQueyAndDataFieldInfo> customQueyAndDataFieldInfos, SqlDatabase db, DbTransaction transaction)
        //{
        //    IList<CustomQueyAndDataFieldInfo> oldCustomQueyAndDataFieldInfos = GetModelInfos(dataQueriedId);

        //    /* 1. 不存在则插入，存在则更新 */
        //    foreach (var customQueyAndDataFieldInfo in customQueyAndDataFieldInfos)
        //    {
        //        bool find = false;
        //        foreach (var oldCustomQueyAndDataFieldInfo in oldCustomQueyAndDataFieldInfos)
        //        {
        //            if (customQueyAndDataFieldInfo.DataQueriedId == oldCustomQueyAndDataFieldInfo.DataQueriedId
        //                && customQueyAndDataFieldInfo.DataFieldId == oldCustomQueyAndDataFieldInfo.DataFieldId
        //                && customQueyAndDataFieldInfo.DataFieldMode == oldCustomQueyAndDataFieldInfo.DataFieldMode)
        //            {
        //                find = true;
        //                if (customQueyAndDataFieldInfo.Sorting != oldCustomQueyAndDataFieldInfo.Sorting)
        //                {
        //                    UpdateSorting(customQueyAndDataFieldInfo, db, transaction);
        //                }
        //                break;
        //            }
        //        }
        //        if (!find)
        //        {
        //            Insert(customQueyAndDataFieldInfo, db, transaction);
        //        }
        //    }

        //    /* 2. 存在则忽略，不存在则删除*/
        //    foreach (var oldCustomQueyAndDataFieldInfo in oldCustomQueyAndDataFieldInfos)
        //    {
        //        bool find = false;
        //        foreach (var customQueyAndDataFieldInfo in customQueyAndDataFieldInfos)
        //        {
        //            if (customQueyAndDataFieldInfo.DataQueriedId == oldCustomQueyAndDataFieldInfo.DataQueriedId
        //                && customQueyAndDataFieldInfo.DataFieldId == oldCustomQueyAndDataFieldInfo.DataFieldId
        //                && customQueyAndDataFieldInfo.DataFieldMode == oldCustomQueyAndDataFieldInfo.DataFieldMode)
        //            {
        //                find = true;
        //                break;
        //            }
        //        }
        //        if (!find)
        //        {
        //            Delete(oldCustomQueyAndDataFieldInfo, db, transaction);
        //        }
        //    }

        //}

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomQueyAndDataFieldInfo 对象列表</returns>
        private IList<CustomQueyAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomQueyAndDataFieldInfo> customQueyAndDataFieldInfos = new List<CustomQueyAndDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomQueyAndDataField");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if ((sortingCondtions != null) && (sortingCondtions.Count > 0))
            {
                sb.Append(" ORDER BY ");
                sb.Append(DataAccessHandler.GetSortingSentence(sortingCondtions));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataQueriedId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte dataFieldMode = DataConvertionHelper.GetByte(dataReader[2]);
                            string dataFieldFormat = DataConvertionHelper.GetString(dataReader[3]);
                            bool queryAllowed = DataConvertionHelper.GetBoolean(dataReader[4]);
                            string conditions = DataConvertionHelper.GetString(dataReader[5]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[6]);
                            //将创建 CustomQueyAndDataFieldInfo 对象加入集合中
                            customQueyAndDataFieldInfos.Add(new CustomQueyAndDataFieldInfo(dataFieldId, dataQueriedId, dataFieldMode, dataFieldFormat, queryAllowed,
                            conditions, sorting));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customQueyAndDataFieldInfos;
        }

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomQueyAndDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomQueyAndDataField");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomQueyAndDataField 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomQueyAndDataField ", "DataFieldId", "*", false, false, startPosition,
                    count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomQueyAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQueyAndDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomQueyAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomQueyAndDataField ", "DataFieldId", "*", false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomQueyAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQueyAndDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 删除满足条件的的 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal dataFieldId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM CustomQueyAndDataField WHERE DataFieldId = @DataFieldId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    //执行删除操作
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 删除满足条件的所有  CustomQueyAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomQueyAndDataField");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            else
            {
                throw new ArgumentNullException("批量删除的条件不许未空，即不允许删除该表中所有的数据.");
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 获得字段模式
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        private byte GetDataFieldMode(decimal dataQueriedId, decimal dataFieldId)
        {
            byte dataFieldMode = 0;

            try
            {
                string sqlSelect = "SELECT DataFieldMode FROM CustomQueyAndDataField WHERE DataFieldId = @DataFieldId AND DataQueriedId = @DataQueriedId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    dataFieldMode = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldMode;
        }

        #endregion

        #endregion
    }
}
