//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleAndPrint.cs
// 描述: RoleAndPrint 数据层访问类
// 作者：ChenJie 
// 编写日期：2019/11/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// RoleAndPrint 表的数据层访问类
    /// </summary>
    public class RoleAndPrint : CorrelatedTableDataAcess, IRoleAndPrint
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndPrint() : base("RoleAndPrint", "RoleId", "PrintId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
		/// 获得 RoleAndPrintInfo 对象
		/// </summary>
		///<param name="roleId">角色编号</param>
		///<param name="printId">数据打印编号</param>
		/// <returns> RoleAndPrintInfo 对象</returns>
		public RoleAndPrintInfo GetModelInfo(decimal roleId, decimal printId)
        {
            RoleAndPrintInfo roleAndPrintInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", DbType.Decimal, roleId, DataFieldCondition.Equal));

            //创建集合对象
            IList<RoleAndPrintInfo> roleAndPrintInfos = GetModelInfos(whereConditons, null, true);
            if (roleAndPrintInfos != null && roleAndPrintInfos.Count > 0)
            {
                roleAndPrintInfo = roleAndPrintInfos[0];
            }

            return roleAndPrintInfo;
        }

        /// <summary>
        ///  删除 RoleAndPrintInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="printId">数据打印编号</param>
        public void Delete(decimal roleId, decimal printId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndPrint ");
            sb.Append("WHERE RoleId = @RoleId AND PrintId = @PrintId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
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
        /// 获得 RoleAndPrintInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndPrintInfo 对象列表</returns>
        public IList<RoleAndPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 RoleAndPrint 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>RoleAndPrintInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RoleAndPrint ", "RoleId", false, whereConditons);
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
        /// 根据打印编号获得角色对象列表
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRolesByPrintId(decimal printId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomRole.RoleId, GroupId, RoleName, RoleCode FROM CustomRole ");
                sb.Append("INNER JOIN RoleAndPrint ON RoleAndPrint.RoleId = CustomRole.RoleId WHERE PrintId = @PrintId");
                
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string roleName = DataConvertionHelper.GetString(dataReader[2]);
                            string roleCode = DataConvertionHelper.GetString(dataReader[3]);
                            commonNodes.Add(new CommonNode(printId, groupId, roleName, roleCode, true));
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
        /// 更新角色的打印范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="printIds"></param>
        public void UpdatePrints(decimal roleId, List<decimal> printIds)
        {
            IList<decimal> oldPrintIds = GetSecondIds(roleId);

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        /* 1. 新的打印对象不存在则插入 */
                        foreach (var printId in printIds)
                        {
                            if (!oldPrintIds.Contains(printId))
                            {
                                Insert(new CorrelatedModel(roleId, printId), db, transaction);
                            }
                        }

                        /* 2. 旧的打印对象不存在则删除 */
                        foreach (var printId in oldPrintIds)
                        {
                            if (!printIds.Contains(printId))
                            {
                                Delete(roleId, printId, db, transaction);
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
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得角色的打印对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintsByRoleId(decimal roleId)
        {
            List<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomPrint.PrintId, GroupId, PrintName, PrintCode, TableType FROM CustomPrint ");
                sb.Append("INNER JOIN RoleAndPrint ON RoleAndPrint.PrintId = CustomPrint.PrintId WHERE RoleId = @RoleId");
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal printId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string printName = DataConvertionHelper.GetString(dataReader[2]);
                            string printCode = DataConvertionHelper.GetString(dataReader[3]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[4]);
                            commonNodes.Add(new CommonNode(printId, groupId, printName, printCode, true, tableType));
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
        /// 获得用户授权的打印分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintCategories(decimal userId)
        {
            List<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT DISTINCT CustomGroup.GroupId, ParentGroupId, GroupName, GroupCode, GroupType, IsLeaf FROM CustomGroup ");
                sb.Append("INNER JOIN CustomPrint ON CustomGroup.GroupId = CustomPrint.GroupId ");
                sb.Append("INNER JOIN RoleAndPrint ON RoleAndPrint.PrintId = CustomPrint.PrintId ");
                sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = RoleAndPrint.RoleId ");
                sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomPrint.PrintVisible = @PrintVisible");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "PrintVisible", DbType.Boolean, true);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentGroupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string groupName = DataConvertionHelper.GetString(dataReader[2]);
                            string groupCode = DataConvertionHelper.GetString(dataReader[3]);
                            byte groupType = DataConvertionHelper.GetByte(dataReader[4]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[5]);
                            commonNodes.Add(new CommonNode(groupId, parentGroupId, groupName, groupCode, isLeaf, groupType));
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
        /// 验证用户是否具有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="printId"></param>
        /// <returns></returns>
        public bool ValidatePrintItem(decimal userId, decimal printId)
        {
            bool result = false;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT COUNT(*) FROM CustomPrint ");
                sb.Append("INNER JOIN RoleAndPrint ON RoleAndPrint.PrintId = CustomPrint.PrintId ");
                sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = RoleAndPrint.RoleId ");
                sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomPrint.PrintId = @PrintId AND PrintVisible = @PrintVisible");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                    db.AddInParameter(dbCommand, "PrintVisible", DbType.Boolean, true);
                    int count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 根据打印分类获得用户授权的打印
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrints(decimal userId, decimal groupId)
        {
            List<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT DISTINCT CustomPrint.PrintId, PrintName, PrintCode, TableType FROM CustomPrint ");
                sb.Append("INNER JOIN RoleAndPrint ON RoleAndPrint.PrintId = CustomPrint.PrintId ");
                sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = RoleAndPrint.RoleId ");
                sb.Append("WHERE RoleAndUser.UserId = @UserId AND GroupId = @GroupId AND CustomPrint.PrintVisible = @PrintVisible");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, groupId);
                    db.AddInParameter(dbCommand, "PrintVisible", DbType.Boolean, true);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal printId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string printName = DataConvertionHelper.GetString(dataReader[1]);
                            string printCode = DataConvertionHelper.GetString(dataReader[2]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[3]);
                            commonNodes.Add(new CommonNode(printId, groupId, printName, printCode, true, tableType));
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

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 RoleAndPrintInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RoleAndPrintInfo 对象列表</returns>
        private IList<RoleAndPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<RoleAndPrintInfo> roleAndPrintInfos = new List<RoleAndPrintInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM RoleAndPrint");

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
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal printId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            //将创建 RoleAndPrintInfo 对象加入集合中
                            roleAndPrintInfos.Add(new RoleAndPrintInfo(roleId, printId));
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

            return roleAndPrintInfos;
        }


        /// <summary>
        /// 获得 RoleAndPrintInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndPrintInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RoleAndPrint");
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
        /// 获得表 RoleAndPrint 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndPrint", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndPrint 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndPrint ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RoleAndPrint 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndPrint ", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndPrint 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndPrint ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 RoleAndPrintInfo 对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal roleId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM RoleAndPrint WHERE RoleId = @RoleId";
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
        /// 删除满足条件的所有  RoleAndPrintInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndPrint");
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
