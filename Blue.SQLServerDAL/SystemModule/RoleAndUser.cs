//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：RoleAndUser.cs
// 描述：RoleAndUser 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/28
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
using Blue.SQLServerDAL.UserModule;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// RoleAndUser 表的数据层访问类
    /// </summary>
    public class RoleAndUser : CorrelatedTableDataAcess, IRoleAndUser
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public RoleAndUser() : base("RoleAndUser", "UserId", "RoleId")
        {
		}

        #endregion

        #region 实现默认接口

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        public void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Insert(userIds, authorityMethod, dataFieldAuthority, authoritiedRoleIds, ownDepartment, departmentIds, userTypeIds, db, transaction);
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
        /// 用户授权
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="roleIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        public void Insert(IList<WhereConditon> whereConditons, IList<decimal> roleIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            IList<WhereConditon> conditons = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserAccount.UserId FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId = UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepId = UserAccount.DepId ");
            if (roleIds.Count > 0)
            {
                sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.UserId = UserAccount.UserId ");
                conditons = new List<WhereConditon>();
                foreach (WhereConditon whereConditon in whereConditons)
                {
                    conditons.Add(whereConditon);
                }
                for (int i = 0; i < roleIds.Count; i++)
                {
                    if (roleIds.Count > 1)
                    {
                        if (i == 0)
                        {
                            conditons.Add(new WhereConditon("RoleAndUser", "RoleId", string.Format("RoleId_{0}", i), System.Data.DbType.Decimal, roleIds[i],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                        }
                        else if (i == roleIds.Count - 1)
                        {
                            conditons.Add(new WhereConditon("RoleAndUser", "RoleId", string.Format("RoleId_{0}", i), System.Data.DbType.Decimal, roleIds[i],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                        }
                        else
                        {
                            conditons.Add(new WhereConditon("RoleAndUser", "RoleId", string.Format("RoleId_{0}", i), System.Data.DbType.Decimal, roleIds[i],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 1));
                        }
                    }
                    else
                    {
                        conditons.Add(new WhereConditon("RoleAndUser", "RoleId", "RoleId", System.Data.DbType.Decimal, roleIds[0],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                }
            }
            else
            {
                conditons = whereConditons;
            }
            string condition = DataAccessHandler.GetConditionSentence(conditons);
            if (condition.Length > 0)
            {
                sb.AppendFormat("WHERE {0}", condition);
            }
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            IList<decimal> userIds = new List<decimal>();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                if ((conditons != null) && (conditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, conditons);
                }
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        userIds.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Insert(userIds, authorityMethod, dataFieldAuthority, authoritiedRoleIds, ownDepartment, departmentIds, userTypeIds, db, transaction);
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

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="transaction"></param>
        private void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority, IList<decimal> authoritiedRoleIds,
            bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds, SqlDatabase db, DbTransaction transaction)
        {
            UserAccount userAccount = new UserAccount();
            DepartmentScope departmentScope = new DepartmentScope();
            UserTypeScope userTypeScope = new UserTypeScope();
            if (authorityMethod == AuthorityMethod.Update)
            {
                foreach (decimal userId in userIds)
                {
                    departmentScope.Delete(userId, db, transaction);
                    userTypeScope.Delete(userId, db, transaction);
                    Delete(userId, db, transaction);
                }
                foreach (decimal userId in userIds)
                {
                    foreach (decimal authoritiedRoleId in authoritiedRoleIds)
                    {
                        Insert(new CorrelatedModel(userId, authoritiedRoleId), db, transaction);
                    }
                    if (ownDepartment)
                    {
                        decimal depId = userAccount.GetDepIdByUserId(userId, db, transaction);
                        departmentScope.Insert(new CorrelatedModel(userId, depId), db, transaction);
                    }
                    else
                    {
                        foreach (decimal departmentId in departmentIds)
                        {
                            departmentScope.Insert(new CorrelatedModel(userId, departmentId), db, transaction);
                        }
                    }
                    foreach (decimal userTypeId in userTypeIds)
                    {
                        userTypeScope.Insert(new CorrelatedModel(userId, userTypeId), db, transaction);
                    }
                    userAccount.UpdateDataFieldAuthority(userId, dataFieldAuthority, db, transaction);
                }
            }
            else
            {
                foreach (decimal userId in userIds)
                {
                    Int64 fieldAuthority = userAccount.GetDataFieldAuthority(userId, db, transaction);
                    userAccount.UpdateDataFieldAuthority(userId, fieldAuthority | dataFieldAuthority, db, transaction);

                    IList<decimal> roleIds = GetRoleIds(userId);
                    foreach (decimal authoritiedRoleId in authoritiedRoleIds)
                    {
                        if (!roleIds.Contains(authoritiedRoleId))
                        {
                            Insert(new CorrelatedModel(userId, authoritiedRoleId), db,transaction);
                        }
                    }

                    IList<decimal> relatedDepartmentIds = departmentScope.GetRelatedDepartmentIds(userId, db, transaction);
                    if (ownDepartment)
                    {
                        decimal depId = userAccount.GetDepIdByUserId(userId, db, transaction);
                        if (!relatedDepartmentIds.Contains(depId))
                        {
                            departmentScope.Insert(new CorrelatedModel(userId, depId), db, transaction);
                        }
                    }
                    else
                    {
                        foreach (decimal departmentId in departmentIds)
                        {
                            if (!relatedDepartmentIds.Contains(departmentId))
                            {
                                departmentScope.Insert(new CorrelatedModel(userId, departmentId),db, transaction);
                            }
                        }
                    }
                    IList<decimal> relatedUserTypeIds = userTypeScope.GetRelatedUserTypeIds(userId, db, transaction);
                    foreach (decimal userTypeId in userTypeIds)
                    {
                        if (!relatedUserTypeIds.Contains(userTypeId))
                        {
                            userTypeScope.Insert(new CorrelatedModel(userId, userTypeId), db, transaction);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获得角色编号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<decimal> GetRoleIds(decimal userId)
        {
            //创建集合对象
            IList<decimal> roleIds = new List<decimal>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleId FROM RoleAndUser");
            sb.Append(" WHERE UserId = @UserId ");
            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            roleIds.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
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

            return roleIds;
        }


        /// <summary>
        ///  删除 RoleAndUserInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="transaction">事务</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal userId, DbTransaction transaction)
        {
            int count = 0;

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndUser ");
            sb.Append("WHERE UserId = @UserId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
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

        #endregion

        #endregion
    }
}
