//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CorrelatedTableDataAcess.cs
// 描述：关联表数据库访问类
// 作者：ChenJie 
// 编写日期：2016/08/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.DataAccessLibrary
{
    /// <summary>
    /// 关联表数据库访问类
    /// </summary>
    public abstract class CorrelatedTableDataAcess : ICorrelatedTable
    { 

        #region 私有变量

        private readonly string tableName;
        private readonly string firstForeignKeyName;
        private readonly string secondForeignKeyName;
        private readonly string rangeName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="firstForeignKeyName"></param>
        /// <param name="secondForeignKeyName"></param>
        public CorrelatedTableDataAcess(string tableName, string firstForeignKeyName, string secondForeignKeyName)
            : this (tableName, firstForeignKeyName, secondForeignKeyName, string.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="firstForeignKeyName"></param>
        /// <param name="secondForeignKeyName"></param>
        /// <param name="rangeName"></param>
        public CorrelatedTableDataAcess(string tableName, string firstForeignKeyName, string secondForeignKeyName, string rangeName)
        {
            this.tableName = tableName;
            this.firstForeignKeyName = firstForeignKeyName;
            this.secondForeignKeyName = secondForeignKeyName;
            this.rangeName = rangeName;
        }

        #endregion        

        #region 实现默认接口

        /// <summary>
        /// 向联系表中插入一条新记录
        /// </summary>
        /// <param name="correlatedModel">correlatedModel 对象</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
        public void Insert(CorrelatedModel correlatedModel, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO {0}({1}, {2}", tableName, firstForeignKeyName, secondForeignKeyName);
            if (!string.IsNullOrWhiteSpace(rangeName))
            {
                sb.AppendFormat(", {3}", rangeName);
            }
            sb.AppendFormat(") VALUES(@{0}, @{1}", firstForeignKeyName, secondForeignKeyName);
            if (!string.IsNullOrWhiteSpace(rangeName))
            {
                sb.AppendFormat(", {0});", rangeName);
            }
            else
            {
                sb.AppendFormat(");", rangeName);
            }
                      
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, correlatedModel.ForeignKey);
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, correlatedModel.OtherForeignKey);
                    if (!string.IsNullOrWhiteSpace(rangeName))
                    {
                        db.AddInParameter(dbCommand, rangeName, DbType.Byte, correlatedModel.RangeValue);
                    }
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        /// 更新记录
        /// </summary>
        /// <param name="correlatedModel">correlatedModel 对象</param>        
        public void Update(CorrelatedModel correlatedModel)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET  {1} = @{1} ", tableName, rangeName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, rangeName, DbType.Byte, correlatedModel.RangeValue);
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, correlatedModel.ForeignKey);
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, correlatedModel.OtherForeignKey);
                    //执行删除操作
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
        /// 更新记录
        /// </summary>
        /// <param name="firstKeyValues"></param>
        /// <param name="secondKeyValue"></param>
        /// <param name="rangeValue"></param>
        public void Update(IList<decimal> firstKeyValues, decimal secondKeyValue, byte rangeValue)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET  {1} = @{1} ", tableName, rangeName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (decimal firstKeyValue in firstKeyValues)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, rangeName, DbType.Byte, rangeValue);
                            db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstKeyValue);
                            db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondKeyValue);
                            //执行删除操作
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
        /// 获得模型
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <param name="secondForeignKey"></param>
        /// <returns></returns>
        public CorrelatedModel GetCorrelatedModel(decimal firstForeignKey, decimal secondForeignKey)
        {
            CorrelatedModel correlatedModel = null;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0} FROM {1} ", rangeName, tableName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                    
                    byte rangeValue = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand), 0);
                    if (rangeValue > 0)
                    {
                        correlatedModel = new CorrelatedModel(firstForeignKey, secondForeignKey, rangeValue);
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return correlatedModel;
        }

        /// <summary>
		///  删除 CorrelatedModel 对象
		/// </summary>
	    ///<param name="firstForeignKey">外键一的值</param>
		///<param name="secondForeignKey">外键二的值</param>
        /// <param name="db">系统数据库对象</param>
        /// <param name="transaction">事务</param>
		public void Delete(decimal firstForeignKey, decimal secondForeignKey, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);        
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                    //执行删除操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        ///<param name="secondForeignKeys">外键二的值集合</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
        public void Delete(decimal firstForeignKey,  IList<decimal> secondForeignKeys, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);
            try
            {
                foreach (decimal secondForeignKey in secondForeignKeys)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                        db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                        //执行删除操作
                        db.ExecuteNonQuery(dbCommand, transaction);
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
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        ///<param name="secondForeignKey">外键二的值</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
        public void Delete(IList<decimal> firstForeignKeys, decimal secondForeignKey, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0} AND {1} = @{1}", firstForeignKeyName, secondForeignKeyName);
            try
            {
                foreach (decimal firstForeignKey in firstForeignKeys)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                        db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
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

        /// <summary>
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
        public void Delete(IList<decimal> firstForeignKeys, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0}", firstForeignKeyName);
            try
            {
                foreach (decimal firstForeignKey in firstForeignKeys)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                        //执行删除操作
                        db.ExecuteNonQuery(dbCommand, transaction);
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
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        /// <param name="db">系统数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public int Delete(decimal firstForeignKey, SqlDatabase db, DbTransaction transaction)
        {
            int count = 0;

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0}", firstForeignKeyName);          
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                    //执行删除操作
                    count = db.ExecuteNonQuery(dbCommand, transaction);
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
		///  删除 CorrelatedModel 对象
		/// </summary>
	    ///<param name="secondForeignKey">第二个外键编号</param>
        /// <param name="db">系统数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
		public int DeleteBySecondForeignKey(decimal secondForeignKey, SqlDatabase db, DbTransaction transaction)
        {
            int count = 0;

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE FROM {0} ", tableName);
            sb.AppendFormat("WHERE {0} = @{0}", secondForeignKeyName);         
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                    //执行删除操作
                    count = db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 获得表中记录的数目
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        public int GetTotalCountByFirstForeignKey(decimal firstForeignKey)
        {
            int count = 0;

            try
            {
                string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", tableName, firstForeignKeyName);

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
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
        /// 获得表中记录的数目
        /// </summary>
        /// <param name="secondForeignKey"></param>
        /// <returns></returns>
        public int GetTotalCountBySecondForeignKey(decimal secondForeignKey)
        {
            int count = 0;

            try
            {
                string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", tableName, secondForeignKeyName);

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, secondForeignKeyName, DbType.Decimal, secondForeignKey);
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
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
        /// 根据外键编号获得另外一个外键编号列表
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        public IList<decimal> GetSecondIds(decimal firstForeignKey)
        {
            IList<decimal> secondIds = new List<decimal>();

            try
            {
                string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", secondForeignKeyName, tableName, firstForeignKeyName);

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, firstForeignKeyName, DbType.Decimal, firstForeignKey);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            secondIds.Add(Convert.ToDecimal(dataReader[0]));
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

            return secondIds;
        }

        #endregion
    }
}
