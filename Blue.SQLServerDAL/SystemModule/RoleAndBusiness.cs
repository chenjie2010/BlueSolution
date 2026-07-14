//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：RoleAndBusiness.cs
// 描述：RoleAndBusiness 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/12/22
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// RoleAndBusiness 表的数据层访问类
    /// </summary>
    public class RoleAndBusiness : CorrelatedTableDataAcess, IRoleAndBusiness
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public RoleAndBusiness() : base("RoleAndBusiness", "BusinessId", "RoleId")
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 RoleAndBusiness 表中插入一条新记录
		/// </summary>
		/// <param name="roleAndBusinessInfo">roleAndBusinessInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(RoleAndBusinessInfo roleAndBusinessInfo)
		{
			//自动增加的关键字的值
			decimal roleAndBusinessId= 0;
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO RoleAndBusiness(BusinessEnabled, ThirdModeEnabled, InitializedDate, ExpiredDate, Notes)");
			sb.Append("VALUES (@BusinessEnabled, @ThirdModeEnabled, @InitializedDate, @ExpiredDate, @Notes);");
			sb.Append("SET @RoleId = SCOPE_IDENTITY()");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddOutParameter(dbCommand, "BusinessId", DbType.Decimal,8);
					db.AddOutParameter(dbCommand, "RoleId", DbType.Decimal,8);
					db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, roleAndBusinessInfo.BusinessEnabled);
                    db.AddInParameter(dbCommand, "ThirdModeEnabled", DbType.Boolean, roleAndBusinessInfo.ThirdModeEnabled);
                    db.AddInParameter(dbCommand, "InitializedDate", DbType.DateTime, roleAndBusinessInfo.InitializedDate);
					db.AddInParameter(dbCommand, "ExpiredDate", DbType.DateTime, roleAndBusinessInfo.ExpiredDate);
					db.AddInParameter(dbCommand, "Notes", DbType.String, roleAndBusinessInfo.Notes);
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
					roleAndBusinessId= DataConvertionHelper.GetDecimal(dbCommand.Parameters["@RoleId"].Value, 0);
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return roleAndBusinessId;
		}

        /// <summary>
		/// 获得 RoleAndBusinessInfo 对象
		/// </summary>
		///<param name="businessId">业务编号</param>
		///<param name="roleId">角色编号</param>
		/// <returns> RoleAndBusinessInfo 对象</returns>
		public RoleAndBusinessInfo GetModelInfo(decimal businessId, decimal roleId)
		{			
			RoleAndBusinessInfo roleAndBusinessInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, businessId, DataFieldCondition.Equal, DataFieldInnerRealtion.None));
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", DbType.Decimal, roleId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<RoleAndBusinessInfo> roleAndBusinessInfos = GetModeInfos(whereConditons, null, true);
            if (roleAndBusinessInfos != null && roleAndBusinessInfos.Count > 0)
            {
                roleAndBusinessInfo = roleAndBusinessInfos[0];
            }          

            return roleAndBusinessInfo;
		}
        
        /// <summary>
		/// 更新 RoleAndBusinessInfo 对象
		/// </summary>
		/// <param name="roleAndBusinessInfo">RoleAndBusinessInfo 对象</param>
		public void Update(RoleAndBusinessInfo roleAndBusinessInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE RoleAndBusiness SET BusinessEnabled = @BusinessEnabled, InitializedDate = @InitializedDate, ExpiredDate = @ExpiredDate, ");
			sb.Append("Notes = @Notes WHERE BusinessId = @BusinessId AND RoleId = @RoleId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, roleAndBusinessInfo.BusinessId);
					db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleAndBusinessInfo.RoleId);
					db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, roleAndBusinessInfo.BusinessEnabled);
					db.AddInParameter(dbCommand, "InitializedDate", DbType.DateTime, roleAndBusinessInfo.InitializedDate);
					db.AddInParameter(dbCommand, "ExpiredDate", DbType.DateTime, roleAndBusinessInfo.ExpiredDate);
					db.AddInParameter(dbCommand, "Notes", DbType.String, roleAndBusinessInfo.Notes);
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
		///  删除 RoleAndBusinessInfo 对象
		/// </summary>
	    ///<param name="businessId">业务编号</param>
		///<param name="roleId">角色编号</param>
		public void Delete(decimal businessId, decimal roleId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM RoleAndBusiness ");
			sb.Append("WHERE BusinessId = @BusinessId AND RoleId = @RoleId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, businessId);
					db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
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
		/// 获得 RoleAndBusinessInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndBusinessInfo 对象列表</returns>
		public IList<RoleAndBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModeInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 RoleAndBusiness 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>RoleAndBusinessInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RoleAndBusiness ", "BusinessId", false, whereConditons);
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
        /// 更新角色的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndBusinessInfos"></param>
        public void Update(decimal roleId, IList<RoleAndBusinessInfo> roleAndBusinessInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT RoleId FROM RoleAndBusiness WHERE RoleId = @RoleId AND BusinessId = @BusinessId) ");
            sb.Append("BEGIN UPDATE RoleAndBusiness SET BusinessEnabled = @BusinessEnabled, ThirdModeEnabled = @ThirdModeEnabled, InitializedDate = @InitializedDate, ExpiredDate = @ExpiredDate, Notes = @Notes ");
            sb.Append("WHERE BusinessId = @BusinessId AND RoleId = @RoleId ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO RoleAndBusiness(RoleId, BusinessId, BusinessEnabled, ThirdModeEnabled, InitializedDate, ExpiredDate, Notes) ");
            sb.Append("VALUES (@RoleId, @BusinessId, @BusinessEnabled, @ThirdModeEnabled, @InitializedDate, @ExpiredDate, @Notes) END");
            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (RoleAndBusinessInfo roleAndBusinessInfo in roleAndBusinessInfos)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, roleAndBusinessInfo.BusinessId);
                            db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                            db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, roleAndBusinessInfo.BusinessEnabled);
                            db.AddInParameter(dbCommand, "ThirdModeEnabled", DbType.Boolean, roleAndBusinessInfo.ThirdModeEnabled);
                            db.AddInParameter(dbCommand, "InitializedDate", DbType.DateTime, DataConvertionHelper.SetDateTime(roleAndBusinessInfo.InitializedDate));
                            db.AddInParameter(dbCommand, "ExpiredDate", DbType.DateTime, DataConvertionHelper.SetDateTime(roleAndBusinessInfo.ExpiredDate));
                            db.AddInParameter(dbCommand, "Notes", DbType.String, roleAndBusinessInfo.Notes);
                            //执行更新操作
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                            {
                                throw new Exception("更新失败！");
                            }
                        }
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
        /// 获得角色对应的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataSet GetBusinessAuthority(decimal roleId, decimal menuId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomBusiness.BusinessId, CustomBusiness.BusinessName, A.BusinessEnabled, A.ThirdModeEnabled, A.InitializedDate, A.ExpiredDate, A.Notes FROM CustomBusiness ");
            sb.Append("LEFT OUTER JOIN (SELECT BusinessId, RoleId, BusinessEnabled, ThirdModeEnabled, InitializedDate, ExpiredDate, Notes FROM RoleAndBusiness WHERE RoleId = @RoleId) A ");
            sb.Append("ON CustomBusiness.BusinessId = A.BusinessId ");
            sb.Append("WHERE MenuId = @MenuId AND (A.RoleId = @RoleId OR A.RoleId IS NULL) ORDER BY CustomBusiness.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {                    
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(roleId));
                    db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, DataConvertionHelper.SetDecimal(menuId));
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 RoleAndBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RoleAndBusinessInfo 对象列表</returns>
        private IList<RoleAndBusinessInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<RoleAndBusinessInfo>  roleAndBusinessInfos = new List<RoleAndBusinessInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM RoleAndBusiness");
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
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal businessId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            bool businessEnabled = DataConvertionHelper.GetBoolean(dataReader[2]);
                            bool thirdModeEnabled = DataConvertionHelper.GetBoolean(dataReader[3]);
                            DateTime initializedDate = DataConvertionHelper.GetDateTime(dataReader[4]);
                            DateTime expiredDate = DataConvertionHelper.GetDateTime(dataReader[5]);
                            string notes = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 RoleAndBusinessInfo 对象加入集合中
                            roleAndBusinessInfos.Add(new RoleAndBusinessInfo(roleId, businessId, businessEnabled, thirdModeEnabled, initializedDate,
                            expiredDate, notes));
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
            
			return roleAndBusinessInfos;
		} 
        
        /// <summary>
		/// 获得 RoleAndBusinessInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>RoleAndBusinessInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RoleAndBusiness");
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
        /// 获得表 RoleAndBusiness 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndBusiness ", "BusinessId", "*", false, false, startPosition, 
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
        /// 获得以表 RoleAndBusiness 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndBusiness ", "BusinessId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RoleAndBusiness 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndBusiness ", "BusinessId", "*", false, false, startPosition, 
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
        /// 获得以表 RoleAndBusiness 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndBusiness ", "BusinessId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 RoleAndBusinessInfo 对象
		/// </summary>
	    /// <param name="businessId">业务编号</param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal businessId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM RoleAndBusiness WHERE BusinessId = @BusinessId";
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
				{
					db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, businessId);
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
        /// 删除满足条件的所有  RoleAndBusinessInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndBusiness");
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
