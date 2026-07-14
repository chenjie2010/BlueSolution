//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CellStyle.cs
// 描述: CellStyle 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// CellStyle 表的数据层访问类
    /// </summary>
    public class CellStyle : ICellStyle
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CellStyle()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CellStyle 表中插入一条新记录
		/// </summary>
		/// <param name="cellStyleInfo">cellStyleInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CellStyleInfo cellStyleInfo)
		{
			//自动增加的关键字的值
			decimal cellStyleId= 0;
			
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                cellStyleId = Insert(cellStyleInfo, db, null);
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return cellStyleId;
		}

        /// <summary>
		/// 获得 CellStyleInfo 对象
		/// </summary>
		///<param name="styleId">样式编号</param>
		/// <returns> CellStyleInfo 对象</returns>
		public CellStyleInfo GetModelInfo(decimal styleId)
		{			
			CellStyleInfo  cellStyleInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("StyleId", "StyleId", DbType.Decimal, styleId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CellStyleInfo>  cellStyleInfos = GetModelInfos(whereConditons, null, true);
            if (cellStyleInfos != null && cellStyleInfos.Count > 0)
            {
                cellStyleInfo = cellStyleInfos[0];
            }

            return cellStyleInfo;            
		}
        
        /// <summary>
		/// 更新 CellStyleInfo 对象
		/// </summary>
		/// <param name="cellStyleInfo">CellStyleInfo 对象</param>
		public void Update(CellStyleInfo cellStyleInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CellStyle SET DataFieldId = @DataFieldId, CellId = @CellId, StyleType = @StyleType, ");
			sb.Append("StyleProperty = @StyleProperty, SystemDataFieldId = @SystemDataFieldId, Sorting = @Sorting ");
			sb.Append("WHERE StyleId = @StyleId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "StyleId", DbType.Decimal, cellStyleInfo.StyleId);
					db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, cellStyleInfo.DataFieldId);
					db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellStyleInfo.CellId);
					db.AddInParameter(dbCommand, "StyleType", DbType.Byte, cellStyleInfo.StyleType);
					db.AddInParameter(dbCommand, "StyleProperty", DbType.Int64, cellStyleInfo.StyleProperty);
					db.AddInParameter(dbCommand, "SystemDataFieldId", DbType.Byte, cellStyleInfo.SystemDataFieldId);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, cellStyleInfo.Sorting);
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
		///  删除 CellStyleInfo 对象
		/// </summary>
	    ///<param name="styleId">样式编号</param>
		public void Delete(decimal styleId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CellStyle ");
			sb.Append("WHERE StyleId = @StyleId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "StyleId", DbType.Decimal, styleId);
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
		/// 获得 CellStyleInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CellStyleInfo 对象列表</returns>
		public IList<CellStyleInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CellStyle 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CellStyleInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CellStyle ", "StyleId", false, whereConditons);
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
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfos(decimal cellId)
        {
            //创建集合对象
            IList<CellStyleInfo> cellStyleInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("CellId", "CellId", DbType.Decimal, cellId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            cellStyleInfos = GetModelInfos(whereConditons, sortingCondtions, false);

            return cellStyleInfos;
        }

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfos(decimal cellId, CellCondition cellCondition)
        {
            //创建集合对象
            IList<CellStyleInfo> cellStyleInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("CellId", "CellId", DbType.Decimal, cellId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("StyleType", "StyleType", DbType.Byte, (byte)cellCondition, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            cellStyleInfos = GetModelInfos(whereConditons, sortingCondtions, false);

            return cellStyleInfos;
        }        

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal cellId, CellCondition cellCondition)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CellStyle.SystemDataFieldId, CustomDataField.DataFieldId, CustomDataField.TableId, ");
            sb.Append("CustomDataField.LogicalName, CustomDataField.PhysicalName, CustomDataField.DataFieldProperty ");
            sb.Append("FROM CellStyle LEFT JOIN CustomDataField ");
            sb.Append("ON CellStyle.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CellStyle.CellId = @CellId AND StyleType = @StyleType ");
            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellId));
                    db.AddInParameter(dbCommand, "StyleType", DbType.Byte, (byte)cellCondition);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            byte systemDataFieldId = DataConvertionHelper.GetByte(dataReader[0]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[3]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[4]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[5], 0);
                            decimal id = 0;
                            if (systemDataFieldId > 0)
                            {
                                id = systemDataFieldId;
                                dataFieldProperty = (byte)DataFieldProperty.SystemPhysicalDataField;
                                SystemDataField systemDataField = (SystemDataField)systemDataFieldId;
                                logicalName = UserEnumHelper.GetEnumText(systemDataField);
                                physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(systemDataField);
                            }
                            else
                            {
                                id = dataFieldId;
                            }
                            commonNodes.Add(new CommonNode(id, tableId, logicalName, physicalName, true, dataFieldProperty));
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

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得系统字段列表
        /// </summary>
        /// <param name="customCellInfos"></param>
        /// <returns></returns>
        public IList<CommonNode> GetSystemCommonNodes(IList<CustomCellInfo> customCellInfos)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            if (customCellInfos == null || customCellInfos.Count <= 0)
            {
                return commonNodes;
            }

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT CellStyle.SystemDataFieldId FROM CellStyle ");
            sb.Append("WHERE CellStyle.SystemDataFieldId > 0 AND (");
            for (int i = 0; i < customCellInfos.Count; i++)
            {
                sb.AppendFormat("CellStyle.CellId = @CellId_{0} OR ", i);
            }
            sb.Remove(sb.Length - 4, 4);
            sb.Append(")");
            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    int index = 0;
                    foreach (var customCellInfo in customCellInfos)
                    {
                        db.AddInParameter(dbCommand, string.Format("CellId_{0}", index++), DbType.Decimal, customCellInfo.CellId);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            byte systemDataFieldId = DataConvertionHelper.GetByte(dataReader[0]);
                            SystemDataField systemDataField = (SystemDataField)systemDataFieldId;
                            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
                            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(systemDataField);
                            commonNodes.Add(new CommonNode(systemDataFieldId, decimal.MinValue, logicalName, physicalName, true));
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
        /// 获得系统字段列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetSystemCommonNodes(decimal cellId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT CellStyle.SystemDataFieldId FROM CellStyle ");
            sb.Append("WHERE CellStyle.CellId = @CellId AND CellStyle.SystemDataFieldId > 0");
            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            byte systemDataFieldId = DataConvertionHelper.GetByte(dataReader[0]);
                            SystemDataField systemDataField = (SystemDataField)systemDataFieldId;
                            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
                            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(systemDataField);
                            commonNodes.Add(new CommonNode(systemDataFieldId, decimal.MinValue, logicalName, physicalName, true));
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
		///  删除 CellStyle 对象
		/// </summary>
	    ///<param name="cellId">单元格编号</param>
		public void Delete(decimal cellId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CellStyle ");
            sb.Append("WHERE CellId = @CellId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CellStyle 对象
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal cellId, CellCondition cellCondition, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CellStyle ");
            sb.Append("WHERE CellId = @CellId AND StyleType = @StyleType");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                    db.AddInParameter(dbCommand, "StyleType", DbType.Byte, (byte)cellCondition);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="styleIds"></param>
        /// <param name="cellCondition"></param>
        /// <param name="transaction"></param>
        public void Delete(IList<decimal> styleIds, CellCondition cellCondition, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CellStyle WHERE StyleType = @StyleType AND (");
            for (int i = 0; i < styleIds.Count; i++)
            {
                sb.Append(string.Format("StyleId = @StyleId_{0} OR ", i));
            }
            sb.Remove(sb.Length - 4, 4);
            sb.Append(")");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "StyleType", DbType.Byte, (byte)cellCondition);
                    //给参数赋值
                    for (int i = 0; i < styleIds.Count; i++)
                    {
                        db.AddInParameter(dbCommand, string.Format("StyleId_{0}", i), DbType.Decimal, styleIds[i]);
                    }
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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
        /// 向 CellStyle 表中插入一条新记录
        /// </summary>
        /// <param name="cellStyleInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(CellStyleInfo cellStyleInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal cellStyleId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CellStyle(DataFieldId, CellId, StyleType, StyleProperty, SystemDataFieldId, ");
            sb.Append("Sorting)");
            sb.Append("VALUES (@DataFieldId, @CellId, @StyleType, @StyleProperty, @SystemDataFieldId, ");
            sb.Append("@Sorting);");
            sb.Append("SET @StyleId = SCOPE_IDENTITY()");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "StyleId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellStyleInfo.DataFieldId));
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellStyleInfo.CellId);
                    db.AddInParameter(dbCommand, "StyleType", DbType.Byte, cellStyleInfo.StyleType);
                    db.AddInParameter(dbCommand, "StyleProperty", DbType.Int64, cellStyleInfo.StyleProperty);
                    db.AddInParameter(dbCommand, "SystemDataFieldId", DbType.Byte, DataConvertionHelper.SetByte(cellStyleInfo.SystemDataFieldId));
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, cellStyleInfo.Sorting);
                    //执行插入操作
                    int count = 0;
                    if (transaction != null)
                    {
                        count = db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        count = db.ExecuteNonQuery(dbCommand);
                    }
                    cellStyleId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@StyleId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellStyleId;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CellStyleInfo 对象列表</returns>
        private IList<CellStyleInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CellStyleInfo>  cellStyleInfos = new List<CellStyleInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CellStyle");
            
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if((sortingCondtions != null) && (sortingCondtions.Count > 0))
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
							decimal styleId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal cellId = DataConvertionHelper.GetDecimal(dataReader[2]);
							byte styleType = DataConvertionHelper.GetByte(dataReader[3]);
							long styleProperty = DataConvertionHelper.GetLong(dataReader[4]);
							byte systemDataFieldId = DataConvertionHelper.GetByte(dataReader[5]);
							int sorting = DataConvertionHelper.GetInt(dataReader[6]);
							//将创建 CellStyleInfo 对象加入集合中
							cellStyleInfos.Add(new CellStyleInfo(styleId, dataFieldId, cellId, styleType, styleProperty, 
							systemDataFieldId, sorting));							
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
            
			return cellStyleInfos;
		}
        
		
		/// <summary>
		/// 获得 CellStyleInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CellStyleInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CellStyle");
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
        /// 获得表 CellStyle 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int  startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {             
                ds =  DataAccessHandler.GetPageRecord(db, "CellStyle ", "StyleId", "*", false, false, startPosition, 
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
        /// 获得以表 CellStyle 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CellStyle ", "StyleId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CellStyle 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CellStyle ", "StyleId", "*", false, false, startPosition, 
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
        /// 获得以表 CellStyle 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CellStyle ", "StyleId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CellStyleInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CellStyle");
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
        
        #endregion
        
		#endregion		
    }
}
