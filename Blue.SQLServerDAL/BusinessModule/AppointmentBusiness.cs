//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentBusiness.cs
// 描述: AppointmentBusiness 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/24
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// AppointmentBusiness 表的数据层访问类
    /// </summary>
    public class AppointmentBusiness : CommonNodeDataAccess, IAppointmentBusiness
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AppointmentBusiness() : base("AppointmentBusiness", "AppointmentId", "GroupId", "AppointmentName", "AppointmentCode", false, true)
        {
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 AppointmentBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="appointmentBusinessInfo">appointmentBusinessInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            //自动增加的关键字的值
            decimal appointmentBusinessId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO AppointmentBusiness(WorkflowId, GroupId, DataId, TableId, ParentWorkflowId, ParentDataId, ");
            sb.Append("AppointmentName, AppointmentCode, AssociatedBussinessType, AppointmentType, PeriodType, ");
            sb.Append("PeriodTime, AppointmentBussinesType, AppointmentEnabled, Sorting, Notes)");
            sb.Append("VALUES (@WorkflowId, @GroupId, @DataId, @TableId, @ParentWorkflowId, @ParentDataId, ");
            sb.Append("@AppointmentName, @AppointmentCode, @AssociatedBussinessType, @AppointmentType, @PeriodType, ");
            sb.Append("@PeriodTime, @AppointmentBussinesType, @AppointmentEnabled, @Sorting, @Notes);");
            sb.Append("SET @AppointmentId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            appointmentBusinessInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "AppointmentBusiness", "Sorting", "GroupId", appointmentBusinessInfo.GroupId, 0) + 1;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "AppointmentId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.WorkflowId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, appointmentBusinessInfo.GroupId);
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.DataId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.TableId));
                        db.AddInParameter(dbCommand, "ParentWorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.ParentWorkflowId));
                        db.AddInParameter(dbCommand, "ParentDataId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.ParentDataId));
                        db.AddInParameter(dbCommand, "AppointmentName", DbType.String, appointmentBusinessInfo.AppointmentName);
                        db.AddInParameter(dbCommand, "AppointmentCode", DbType.String, appointmentBusinessInfo.AppointmentCode);
                        db.AddInParameter(dbCommand, "AssociatedBussinessType", DbType.Byte, appointmentBusinessInfo.AssociatedBussinessType);
                        db.AddInParameter(dbCommand, "AppointmentType", DbType.Byte, appointmentBusinessInfo.AppointmentType);
                        db.AddInParameter(dbCommand, "PeriodType", DbType.Byte, appointmentBusinessInfo.PeriodType);
                        db.AddInParameter(dbCommand, "PeriodTime", DbType.Int32, appointmentBusinessInfo.PeriodTime);
                        db.AddInParameter(dbCommand, "AppointmentBussinesType", DbType.Byte, appointmentBusinessInfo.AppointmentBussinesType);
                        db.AddInParameter(dbCommand, "AppointmentEnabled", DbType.Boolean, appointmentBusinessInfo.AppointmentEnabled);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, appointmentBusinessInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, appointmentBusinessInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        appointmentBusinessId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AppointmentId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(appointmentBusinessInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return appointmentBusinessId;
        }

        /// <summary>
		/// 获得 AppointmentBusinessInfo 对象
		/// </summary>
		///<param name="appointmentId">预约编号</param>
		/// <returns> AppointmentBusinessInfo 对象</returns>
		public AppointmentBusinessInfo GetModelInfo(decimal appointmentId)
        {
            AppointmentBusinessInfo appointmentBusinessInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AppointmentId", "AppointmentId", DbType.Decimal, appointmentId, DataFieldCondition.Equal));

            //创建集合对象
            IList<AppointmentBusinessInfo> appointmentBusinessInfos = GetModelInfos(whereConditons, null, true);
            if (appointmentBusinessInfos != null && appointmentBusinessInfos.Count > 0)
            {
                appointmentBusinessInfo = appointmentBusinessInfos[0];
            }

            return appointmentBusinessInfo;
        }

        /// <summary>
        /// 更新 AppointmentBusinessInfo 对象
        /// </summary>
        /// <param name="appointmentBusinessInfo">AppointmentBusinessInfo 对象</param>
        public void Update(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE AppointmentBusiness SET WorkflowId = @WorkflowId, GroupId = @GroupId, DataId = @DataId, TableId = @TableId, ");
            sb.Append("ParentWorkflowId = @ParentWorkflowId, ParentDataId = @ParentDataId, AppointmentName = @AppointmentName, ");
            sb.Append("AppointmentCode = @AppointmentCode, AssociatedBussinessType = @AssociatedBussinessType, AppointmentType = @AppointmentType, ");
            sb.Append("PeriodType = @PeriodType, PeriodTime = @PeriodTime, AppointmentBussinesType = @AppointmentBussinesType, ");
            sb.Append("AppointmentEnabled = @AppointmentEnabled, Notes = @Notes ");
            sb.Append("WHERE AppointmentId = @AppointmentId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentBusinessInfo.AppointmentId);
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.WorkflowId));
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.GroupId));
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.DataId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.TableId));
                    db.AddInParameter(dbCommand, "ParentWorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.ParentWorkflowId));
                    db.AddInParameter(dbCommand, "ParentDataId", DbType.Decimal, DataConvertionHelper.SetDecimal(appointmentBusinessInfo.ParentDataId));
                    db.AddInParameter(dbCommand, "AppointmentName", DbType.String, appointmentBusinessInfo.AppointmentName);
                    db.AddInParameter(dbCommand, "AppointmentCode", DbType.String, appointmentBusinessInfo.AppointmentCode);
                    db.AddInParameter(dbCommand, "AssociatedBussinessType", DbType.Byte, appointmentBusinessInfo.AssociatedBussinessType);
                    db.AddInParameter(dbCommand, "AppointmentType", DbType.Byte, appointmentBusinessInfo.AppointmentType);
                    db.AddInParameter(dbCommand, "PeriodType", DbType.Byte, appointmentBusinessInfo.PeriodType);
                    db.AddInParameter(dbCommand, "PeriodTime", DbType.Int32, appointmentBusinessInfo.PeriodTime);
                    db.AddInParameter(dbCommand, "AppointmentBussinesType", DbType.Byte, appointmentBusinessInfo.AppointmentBussinesType);
                    db.AddInParameter(dbCommand, "AppointmentEnabled", DbType.Boolean, appointmentBusinessInfo.AppointmentEnabled);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, appointmentBusinessInfo.Notes);
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
        ///  删除 AppointmentBusinessInfo 对象
        /// </summary>
        ///<param name="appointmentId">预约编号</param>
        public void Delete(decimal appointmentId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentBusiness ");
            sb.Append("WHERE AppointmentId = @AppointmentId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentId);
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
        /// 获得 AppointmentBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentBusinessInfo 对象列表</returns>
        public IList<AppointmentBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 AppointmentBusiness 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>AppointmentBusinessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "AppointmentBusiness ", "AppointmentId", false, whereConditons);
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
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            AppointmentBusinessInfo appointmentBusinessInfo = GetModelInfo(nodeId);
            if (appointmentBusinessInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(appointmentBusinessInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(appointmentBusinessInfo.AppointmentName);
            }

            return names;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 AppointmentBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>AppointmentBusinessInfo 对象列表</returns>
        private IList<AppointmentBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<AppointmentBusinessInfo> appointmentBusinessInfos = new List<AppointmentBusinessInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM AppointmentBusiness");

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
                            decimal appointmentId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal parentWorkflowId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal parentDataId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            string appointmentName = DataConvertionHelper.GetString(dataReader[7]);
                            string appointmentCode = DataConvertionHelper.GetString(dataReader[8]);
                            byte associatedBussinessType = DataConvertionHelper.GetByte(dataReader[9]);
                            byte appointmentType = DataConvertionHelper.GetByte(dataReader[10]);
                            byte periodType = DataConvertionHelper.GetByte(dataReader[11]);
                            int periodTime = DataConvertionHelper.GetInt(dataReader[12]);
                            byte appointmentBussinesType = DataConvertionHelper.GetByte(dataReader[13]);
                            bool appointmentEnabled = DataConvertionHelper.GetBoolean(dataReader[14]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[15]);
                            string notes = DataConvertionHelper.GetString(dataReader[16]);
                            //将创建 AppointmentBusinessInfo 对象加入集合中
                            appointmentBusinessInfos.Add(new AppointmentBusinessInfo(appointmentId, workflowId, groupId, dataId, tableId,
                            parentWorkflowId, parentDataId, appointmentName, appointmentCode, associatedBussinessType,
                            appointmentType, periodType, periodTime, appointmentBussinesType, appointmentEnabled,
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

            return appointmentBusinessInfos;
        }


        /// <summary>
        /// 获得 AppointmentBusinessInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentBusinessInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AppointmentBusiness");
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
        /// 获得表 AppointmentBusiness 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "AppointmentBusiness ", "AppointmentId", "*", false, false, startPosition,
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
        /// 获得以表 AppointmentBusiness 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentBusiness ", "AppointmentId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 AppointmentBusiness 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "AppointmentBusiness ", "AppointmentId", "*", false, false, startPosition,
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
        /// 获得以表 AppointmentBusiness 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentBusiness ", "AppointmentId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  AppointmentBusinessInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentBusiness");
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
