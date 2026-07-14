//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：RoleAndDataField.cs
// 描述：RoleAndDataField 数据层访问类
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
    /// RoleAndDataField 表的数据层访问类
    /// </summary>
    public class RoleAndDataField : CorrelatedTableDataAcess, IRoleAndDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndDataField() : base("RoleAndDataField", "DataFieldId", "RoleId", "AuthorityType")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 RoleAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="roleAndDataFieldInfo">roleAndDataFieldInfo 对象</param>
        public void Insert(RoleAndDataFieldInfo roleAndDataFieldInfo)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO RoleAndDataField(DataFieldId, RoleId, DataAuthorityType, AuthorityType)");
            sb.Append("VALUES (@DataFieldId, @RoleId, @DataAuthorityType, @AuthorityType)");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值                    
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, roleAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleAndDataFieldInfo.RoleId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, roleAndDataFieldInfo.DataAuthorityType);
                    db.AddInParameter(dbCommand, "AuthorityType", DbType.Byte, roleAndDataFieldInfo.AuthorityType);
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
		/// 获得 RoleAndDataFieldInfo 对象
		/// </summary>
		///<param name="roleId">角色编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> RoleAndDataFieldInfo 对象</returns>
		public RoleAndDataFieldInfo GetModelInfo(decimal roleId, decimal dataFieldId)
        {
            RoleAndDataFieldInfo roleAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", DbType.Decimal, roleId, DataFieldCondition.Equal, DataFieldInnerRealtion.None));
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<RoleAndDataFieldInfo> roleAndDataFieldInfos = GetModeInfos(whereConditons, null, true);
            if (roleAndDataFieldInfos != null && roleAndDataFieldInfos.Count > 0)
            {
                roleAndDataFieldInfo = roleAndDataFieldInfos[0];
            }

            return roleAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 RoleAndDataFieldInfo 对象
        /// </summary>
        /// <param name="roleAndDataFieldInfo">RoleAndDataFieldInfo 对象</param>
        public void Update(RoleAndDataFieldInfo roleAndDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE RoleAndDataField SET DataAuthorityType = @DataAuthorityType, AuthorityType = @AuthorityType ");
            sb.Append("WHERE RoleId = @RoleId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleAndDataFieldInfo.RoleId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, roleAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, roleAndDataFieldInfo.DataAuthorityType);
                    db.AddInParameter(dbCommand, "AuthorityType", DbType.Byte, roleAndDataFieldInfo.AuthorityType);
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
        ///  删除 RoleAndDataFieldInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal roleId, decimal dataFieldId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndDataField ");
            sb.Append("WHERE RoleId = @RoleId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
        /// 获得 RoleAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndDataFieldInfo 对象列表</returns>
        public IList<RoleAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 RoleAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>RoleAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RoleAndDataField ", "RoleId", false, whereConditons);
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
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public DataSet GetDataFiledAuthority(decimal roleId, decimal tableId, byte dataAuthorityType)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT A.DataFieldId, A.LogicalName, ISNULL(B.AuthorityType, 0) AS AuthorityType FROM ");
            sb.Append("(SELECT CustomDataField.DataFieldId, CustomDataField.LogicalName, CustomDataField.Sorting FROM CustomDataField WHERE CustomDataField.TableId = @TableId) AS A ");
            sb.Append("LEFT OUTER JOIN (SELECT RoleAndDataField.DataFieldId, RoleAndDataField.AuthorityType FROM RoleAndDataField WHERE RoleAndDataField.RoleId = @RoleId AND DataAuthorityType = @DataAuthorityType) AS B ");
            sb.Append("ON A.DataFieldId = B.DataFieldId ORDER BY Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, dataAuthorityType);
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
        /// 获得字段的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionType"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public DataFieldAuthority GetDataFieldAuthority(decimal userId, byte dataAuthorityType, decimal dataFieldId)
        {
            DataFieldAuthority dataFieldAuthority = DataFieldAuthority.InVisible;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                //生成插入语句
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT B.AuthorityType FROM CustomDataField INNER JOIN(SELECT DataFieldId, MAX(AuthorityType) AuthorityType FROM ");
                sb.AppendFormat("(SELECT RoleAndDataField.DataFieldId, CASE WHEN MAX(RoleAndDataField.AuthorityType) = {0} THEN {0} WHEN ", (byte)DataFieldAuthority.ReadOnly);
                sb.AppendFormat("MAX(RoleAndDataField.AuthorityType) = {0} AND((CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime)) ", (byte)DataFieldAuthority.ReadAndWrite);
                sb.AppendFormat("THEN {0} ELSE {1} END AS AuthorityType FROM RoleAndUser ", (byte)DataFieldAuthority.ReadAndWrite, (byte)DataFieldAuthority.ReadOnly);
                sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndUser.RoleId ");
                sb.Append("INNER JOIN RoleAndDataField ON RoleAndUser.RoleId = RoleAndDataField.RoleId ");
                sb.Append("INNER JOIN CustomDataField ON RoleAndDataField.DataFieldId = CustomDataField.DataFieldId ");
                sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomRole.IsLockedOut = @IsLockedOut AND RoleAndDataField.DataAuthorityType = @DataAuthorityType AND RoleAndDataField.AuthorityType > @AuthorityType ");
                sb.Append("AND CustomDataField.DataFieldId = @DataFieldId GROUP BY RoleAndDataField.DataFieldId, CustomRole.InitializedDate, CustomRole.ExpiredDate) A GROUP BY DataFieldId) B ");
                sb.Append("ON CustomDataField.DataFieldId = B.DataFieldId ORDER BY Sorting ");

                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "IsLockedOut", DbType.Boolean, false);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, dataAuthorityType);
                    db.AddInParameter(dbCommand, "AuthorityType", DbType.Byte, (byte)DataFieldAuthority.InVisible);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    dataFieldAuthority = (DataFieldAuthority)DataConvertionHelper.GetConvertedInt(db.ExecuteScalar(dbCommand), 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldAuthority;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 按照表的编号删除
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE RoleAndDataField FROM RoleAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON RoleAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomDataField.TableId = @TableId");

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

        /// <summary>
        /// 更新字段权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndDataFieldInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(decimal roleId, IList<RoleAndDataFieldInfo> roleAndDataFieldInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT RoleId FROM RoleAndDataField WHERE RoleId = @RoleId AND DataFieldId = @DataFieldId AND DataAuthorityType = @DataAuthorityType) ");
            sb.Append("BEGIN UPDATE RoleAndDataField SET AuthorityType = @AuthorityType WHERE RoleId = @RoleId AND DataFieldId = @DataFieldId AND DataAuthorityType = @DataAuthorityType ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO RoleAndDataField(DataFieldId, RoleId, DataAuthorityType, AuthorityType) ");
            sb.Append("VALUES (@DataFieldId, @RoleId, @DataAuthorityType, @AuthorityType) END");

            try
            {
                foreach (RoleAndDataFieldInfo roleAndDataFieldInfo in roleAndDataFieldInfos)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(roleAndDataFieldInfo.DataFieldId));
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(roleAndDataFieldInfo.RoleId));
                        db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, roleAndDataFieldInfo.DataAuthorityType);
                        db.AddInParameter(dbCommand, "AuthorityType", DbType.Byte, DataConvertionHelper.SetByte(roleAndDataFieldInfo.AuthorityType));
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                }
            }
            catch (Exception exception)
            {

                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 RoleAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RoleAndDataFieldInfo 对象列表</returns>
        private IList<RoleAndDataFieldInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<RoleAndDataFieldInfo> roleAndDataFieldInfos = new List<RoleAndDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM RoleAndDataField");
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
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte dataAuthorityType = DataConvertionHelper.GetByte(dataReader[2]);
                            byte authorityType = DataConvertionHelper.GetByte(dataReader[3]);
                            //将创建 RoleAndDataFieldInfo 对象加入集合中
                            roleAndDataFieldInfos.Add(new RoleAndDataFieldInfo(dataFieldId, roleId, dataAuthorityType, authorityType));
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

            return roleAndDataFieldInfos;
        }

        /// <summary>
        /// 获得 RoleAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RoleAndDataField");
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
        /// 获得表 RoleAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndDataField ", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndDataField ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RoleAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndDataField ", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndDataField ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 RoleAndDataFieldInfo 对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal roleId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM RoleAndDataField WHERE RoleId = @RoleId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
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
        /// 删除满足条件的所有  RoleAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndDataField");
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
