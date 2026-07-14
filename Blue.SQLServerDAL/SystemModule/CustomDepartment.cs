//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDepartment.cs
// 描述：CustomDepartment 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// CustomDepartment 表的数据层访问类
    /// </summary>
    public class CustomDepartment : CommonNodeDataAccess, ICustomDepartment
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomDepartment() : base("CustomDepartment", "DepId", "ParentDepId", "DepName", "DepCode", true, true)
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomDepartment 表中插入一条新记录
		/// </summary>
		/// <param name="customDepartmentInfo">customDepartmentInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomDepartmentInfo customDepartmentInfo)
		{
			//自动增加的关键字的值
			decimal customDepartmentId= 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomDepartment", "Sorting", "ParentDepId", customDepartmentInfo.ParentDepId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO CustomDepartment(ParentDepId, DepName, DepCode, DepValue, FirstCode, SecondCode, ");
			sb.Append("DepartmentProperty, IsLeaf, IsSystemDepartment, IsVisibleForInterface, Sorting, Notes, CreatedTime, UpdatedTime)");
			sb.Append("VALUES (@ParentDepId, @DepName, @DepCode, @DepValue, @FirstCode, @SecondCode, ");
			sb.Append("@DepartmentProperty, @IsLeaf, @IsSystemDepartment, @IsVisibleForInterface, @Sorting, @Notes, @CreatedTime, @UpdatedTime);");
			sb.Append("SET @DepId = SCOPE_IDENTITY()");
            DateTime time = DateTime.Now;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "DepId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "ParentDepId", DbType.Decimal, customDepartmentInfo.ParentDepId);
                        db.AddInParameter(dbCommand, "DepName", DbType.String, customDepartmentInfo.DepName);
                        db.AddInParameter(dbCommand, "DepCode", DbType.String, customDepartmentInfo.DepCode);
                        db.AddInParameter(dbCommand, "DepValue", DbType.String, customDepartmentInfo.DepValue);
                        db.AddInParameter(dbCommand, "FirstCode", DbType.String, customDepartmentInfo.FirstCode);
                        db.AddInParameter(dbCommand, "SecondCode", DbType.String, customDepartmentInfo.SecondCode);
                        db.AddInParameter(dbCommand, "DepartmentProperty", DbType.Byte, customDepartmentInfo.DepartmentProperty);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "IsSystemDepartment", DbType.Boolean, customDepartmentInfo.IsSystemDepartment);
                        db.AddInParameter(dbCommand, "IsVisibleForInterface", DbType.Boolean, customDepartmentInfo.IsVisibleForInterface);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);                        
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customDepartmentInfo.Notes);
                        db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, time);
                        db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, time);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customDepartmentId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DepId"].Value, 0);                        
                    }
                    if (customDepartmentInfo.ParentDepId > 0)
                    {
                        UpdateLeafOfParentNode(customDepartmentInfo.ParentDepId, false, db, transaction);
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
            
			return customDepartmentId;
		}

        /// <summary>
		/// 获得 CustomDepartmentInfo 对象
		/// </summary>
		///<param name="depId">部门编号</param>
		/// <returns> CustomDepartmentInfo 对象</returns>
		public CustomDepartmentInfo GetModelInfo(decimal depId)
		{			
			CustomDepartmentInfo customDepartmentInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("DepId", "DepId", System.Data.DbType.Decimal, depId, DataFieldCondition.Equal));
            
            //创建集合对象
			IList<CustomDepartmentInfo> customDepartmentInfos = GetModelInfos(whereConditons, null, true);
            if (customDepartmentInfos != null && customDepartmentInfos.Count > 0)
            {
                customDepartmentInfo = customDepartmentInfos[0];
            }          

            return customDepartmentInfo;
		}
        
        /// <summary>
		/// 更新 CustomDepartmentInfo 对象
		/// </summary>
		/// <param name="customDepartmentInfo">CustomDepartmentInfo 对象</param>
		public void Update(CustomDepartmentInfo customDepartmentInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomDepartment SET DepName = @DepName, DepCode = @DepCode, DepValue = @DepValue, ");
            sb.Append("FirstCode = @FirstCode, SecondCode = @SecondCode, DepartmentProperty = @DepartmentProperty, ");
            sb.Append("IsSystemDepartment = @IsSystemDepartment, IsVisibleForInterface = @IsVisibleForInterface, Notes = @Notes, UpdatedTime = @UpdatedTime ");
			sb.Append("WHERE DepId = @DepId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "DepId", DbType.Decimal, customDepartmentInfo.DepId);
					db.AddInParameter(dbCommand, "DepName", DbType.String, customDepartmentInfo.DepName);
					db.AddInParameter(dbCommand, "DepCode", DbType.String, customDepartmentInfo.DepCode);
                    db.AddInParameter(dbCommand, "DepValue", DbType.String, customDepartmentInfo.DepValue);
                    db.AddInParameter(dbCommand, "FirstCode", DbType.String, customDepartmentInfo.FirstCode);
					db.AddInParameter(dbCommand, "SecondCode", DbType.String, customDepartmentInfo.SecondCode);
					db.AddInParameter(dbCommand, "DepartmentProperty", DbType.Byte, customDepartmentInfo.DepartmentProperty);
                    db.AddInParameter(dbCommand, "IsSystemDepartment", DbType.Boolean, customDepartmentInfo.IsSystemDepartment);
                    db.AddInParameter(dbCommand, "IsVisibleForInterface", DbType.Boolean, customDepartmentInfo.IsVisibleForInterface);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDepartmentInfo.Notes);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
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
        ///  删除 CustomDepartmentInfo 对象
        /// </summary>
        ///<param name="depId">部门编号</param>
        public void Delete(decimal depId)
        {
            //生成删除语句
            string sqlDelete = "DELETE FROM CustomDepartment WHERE DepId = @DepId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            bool updateLeaf = true;
            decimal parentDepId = GetParentNodeId(depId);
            int count = GetTotalCountOfChildNode(parentDepId);
            if (count > 1)
            {
                updateLeaf = false;
            }
            DepartmentScope departmentScope = new DepartmentScope();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    departmentScope.DeleteBySecondForeignKey(depId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                    {
                        db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }                    
                    if (updateLeaf)
                    {
                        UpdateLeafOfParentNode(parentDepId, true, db, transaction);
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
		/// 获得 CustomDepartmentInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 对象列表</returns>
		public IList<CustomDepartmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModelInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 CustomDepartment 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDepartmentInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomDepartment ", "DepId", false, whereConditons);
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
        /// 获得单位数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetDepartmentCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemDepartment", "IsSystemDepartment", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsVisibleForInterface", "IsVisibleForInterface", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("UpdatedTime", CustomSorting.Descending));
                count = DataAccessHandler.GetRecordCount(db, "CustomDepartment", whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得单位分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetDepartmentData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemDepartment", "IsSystemDepartment", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsVisibleForInterface", "IsVisibleForInterface", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                string dataFileNames = "DepId, DepName, DepCode, FirstCode, SecondCode, CreatedTime, UpdatedTime";
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("UpdatedTime", CustomSorting.Descending));
                ds = DataAccessHandler.GetPageRecord(db, "CustomDepartment", dataFileNames, false, null, pos,
                    pageSize, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 获得所有的单位信息
        /// </summary>
        /// <returns></returns>
        public IList<CustomDepartmentInfo> GetCustomDepartmentInfos()
        {
            IList<CustomDepartmentInfo> customDepartmentInfos = null;

            customDepartmentInfos =  GetModelInfos(null, null);

            return customDepartmentInfos;
        }

        /// <summary>
        /// 获得单位文本值
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetDepartmentText(decimal depId)
        {
            string departmentText = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT (DepValue + '~' + FirstCode + '~' + SecondCode) AS DepartmentText FROM CustomDepartment WHERE DepId = @DepId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    departmentText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return departmentText;
        }

        /// <summary>
        /// 获得系统接口标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public bool GetIsVisibleForInterface(decimal depId)
        {
            bool isVisibleForInterface = false;

            string sqlSelect = "SELECT IsVisibleForInterface FROM CustomDepartment WHERE DepId = @DepId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    isVisibleForInterface = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isVisibleForInterface;
        }

        /// <summary>
        /// 获得系统标记位
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public bool GetIsSystemDepartment(decimal depId)
        {
            bool isSystemDepartment = false;

            string sqlSelect = "SELECT IsSystemDepartment FROM CustomDepartment WHERE DepId = @DepId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    isSystemDepartment = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isSystemDepartment;
        }

        /// <summary>
        /// 获得单位编号
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public decimal GetDepIdByName(string depName)
        {
            decimal depId = decimal.MinValue;

            string sqlSelect = "SELECT DepId FROM CustomDepartment WHERE DepName = @DepName";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepName", DbType.String, depName);
                    depId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return depId;
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            IList<string> columnCaptions = new List<string>();

            string[] columnNames = new string[] { "DepName", "DepCode", "DepValue", "FirstCode", "SecondCode", "DepartmentProperty"};
            foreach (string columnName in columnNames)
            {
                columnCaptions.Add(ColumnCaptionHelper.GetColumnCaption(columnName));
            }

            return columnCaptions;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentDepId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentDepId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string depCode = GetDepCode(parentDepId);
                string sqlSelect = "SELECT DepName, DepCode, DepValue, FirstCode, SecondCode, DepartmentProperty FROM CustomDepartment WHERE DepCode LIKE @DepCode";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepCode", DbType.String, string.Format("{0}_%", depCode));
                    ds = db.ExecuteDataSet(dbCommand);
                }

                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomDepartment 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomDepartment ", "DepId", "DepName, DepCode, DepValue, FirstCode, SecondCode, DepartmentProperty",
                    false, false, startPosition, count, null, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得单位编码
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>
        public string GetDepCode(decimal depId)
        {
            string depCode = string.Empty;

            string sqlSelect = "SELECT DepCode FROM CustomDepartment WHERE DepId = @DepId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    depCode = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return depCode;
        }

        /// <summary>
        /// 获得单位名称与编号集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndIds()
        {
            Dictionary<string, decimal> departmentNameAndIds = new Dictionary<string, decimal>();

            //查询语句
            string sqlSelect = "SELECT DepName, DepId FROM CustomDepartment";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string depName = DataConvertionHelper.GetString(dataReader[0]);
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[1]);
                            if (!departmentNameAndIds.ContainsKey(depName))
                            {
                                departmentNameAndIds.Add(depName, depID);
                            }
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

            return departmentNameAndIds;
        }

        /// <summary>
        /// 获得用户单位编号和用户单位名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetDepIdAndNames()
        {
            Dictionary<decimal, string> depIdAndNames = new Dictionary<decimal, string>();

            //查询语句
            string sqlSelect = "SELECT DepID, DepName FROM CustomDepartment";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string depName = DataConvertionHelper.GetString(dataReader[1]);
                            //将创建 UserAndRoleInfo 对象加入集合中
                            depIdAndNames.Add(depID, depName);
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

            return depIdAndNames;
        }

        /// <summary>
        /// 获得单位对象
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(string depName)
        {
            CommonNode commonNode = null;

            string sqlSelect = "SELECT DepId, ParentDepId, DepCode, IsLeaf FROM CustomDepartment WHERE DepName = @DepName";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepName", DbType.String, depName);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal depId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentDepId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string depCode = DataConvertionHelper.GetString(dataReader[2]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[3]);
                            commonNode = new CommonNode(depId, parentDepId, depName, depCode, isLeaf);
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
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodesWithRoot(string depName)
        {
            return GetTreeviewCommonNodes(depName, 0);
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName)
        {
            return GetTreeviewCommonNodes(depName, AppSettingHelper.EnumCodeLength);
        }

        /// <summary>
        /// 通过根节点单位信息
        /// </summary>
        public CustomDepartmentInfo GetRootDepartmentInfo()
        {
            CustomDepartmentInfo customDepartmentInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ParentDepId", "ParentDepId", System.Data.DbType.Decimal, DBNull.Value, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomDepartmentInfo> customDepartmentInfos = GetModelInfos(whereConditons, null, true);
            if (customDepartmentInfos != null && customDepartmentInfos.Count > 0)
            {
                customDepartmentInfo = customDepartmentInfos[0];
            }

            return customDepartmentInfo;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法       

        /// <summary>
		/// 获得 CustomDepartmentInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomDepartmentInfo 对象列表</returns>
		private IList<CustomDepartmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomDepartmentInfo> customDepartmentInfos = new List<CustomDepartmentInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomDepartment");
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
                            decimal depId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentDepId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string depName = DataConvertionHelper.GetString(dataReader[2]);
                            string depCode = DataConvertionHelper.GetString(dataReader[3]);
                            string depValue = DataConvertionHelper.GetString(dataReader[4]);
                            string firstCode = DataConvertionHelper.GetString(dataReader[5]);
                            string secondCode = DataConvertionHelper.GetString(dataReader[6]);
                            byte departmentProperty = DataConvertionHelper.GetByte(dataReader[7]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[8]);
                            bool isSystemDepartment = DataConvertionHelper.GetBoolean(dataReader[9]);
                            bool isVisibleForInterface = DataConvertionHelper.GetBoolean(dataReader[10]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[11]);
                            string notes = DataConvertionHelper.GetString(dataReader[12]);
                            DateTime createdTime = DataConvertionHelper.GetDateTime(dataReader[13]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[14]);
                            //将创建 CustomDepartmentInfo 对象加入集合中
                            customDepartmentInfos.Add(new CustomDepartmentInfo(depId, parentDepId, depName, depCode, depValue,
                            firstCode, secondCode, departmentProperty, isLeaf, isSystemDepartment, isVisibleForInterface, 
                            sorting, notes, createdTime, updatedTime));
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

            return customDepartmentInfos;
        }

        /// <summary>
        /// 获得 CustomDepartmentInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDepartmentInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomDepartment");
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
        /// 获得表 CustomDepartment 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDepartment ", "DepId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomDepartment 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDepartment ", "DepId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomDepartment 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDepartment ", "DepId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomDepartment 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDepartment ", "DepId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomDepartmentInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomDepartment");
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
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="depName"></param>
        /// <param name="minCodeLength"></param>
        /// <returns></returns>
        private CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(string depName, int minCodeLength)
        {
            CommonItemList<decimal, CommonNode> commonItemList = new CommonItemList<decimal, CommonNode>(decimal.MinusOne, string.Empty);

            CommonNode commonNode = GetCommonNode(depName);
            if (commonNode != null)
            {
                commonItemList.Text = commonNode.NodeName;
                commonItemList.Value = commonNode.NodeId;
                if (commonNode.NodeCode.Length > AppSettingHelper.EnumCodeLength)
                {
                    string code = commonNode.NodeCode;
                    do
                    {
                        code = code.Substring(0, code.Length - AppSettingHelper.EnumCodeLength);
                        IList<CommonNode> commonNodes = GetChildNodesByParentNodeCode(code, AppSettingHelper.EnumCodeLength);
                        foreach (CommonNode node in commonNodes)
                        {
                            commonItemList.CommonList.Add(node);
                        }
                    } while (code.Length > minCodeLength);
                }
            }

            return commonItemList;
        }

        #endregion

        #endregion
    }
}
