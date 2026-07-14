//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserTypeScope.cs
// 描述：UserTypeScope 数据层访问类
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
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// UserTypeScope 表的数据层访问类
    /// </summary>
    public class UserTypeScope : CorrelatedTableDataAcess, IUserTypeScope
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserTypeScope() : base ("UserTypeScope", "UserId", "UserTypeId")
		{
		}

        #endregion

        #region 实现默认接口

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 通过用户编号获得管理单位节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal userId)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserType.UserTypeId, GroupId, UserTypeName, UserTypeCode FROM UserTypeScope ");
            sb.Append("INNER JOIN UserType ON UserTypeScope.UserTypeId = UserType.UserTypeId WHERE UserTypeScope.UserId = @UserId ");
            sb.Append(" ORDER BY GroupId ASC, Sorting ASC");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal nodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentNodeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string name = DataConvertionHelper.GetString(dataReader[2]);
                            string code = DataConvertionHelper.GetString(dataReader[3]);
                            commonNodes.Add(new CommonNode(nodeId, parentNodeId, name, code));
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
        /// 管理的用户类型
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IList<decimal> GetRelatedUserTypeIds(decimal userId, SqlDatabase db, DbTransaction transaction)
        {
            //创建集合对象
            IList<decimal> relatedUserTypeIds = new List<decimal>();
            //查询语句
            string sqlSelect = "SELECT UserTypeId FROM UserTypeScope WHERE UserId = @UserId ";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    if (transaction != null)
                    {
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
                        {
                            while (dataReader.Read())
                            {
                                decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                                relatedUserTypeIds.Add(userTypeId);
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }
                    else
                    {
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            while (dataReader.Read())
                            {
                                decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                                relatedUserTypeIds.Add(userTypeId);
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return relatedUserTypeIds;
        }

        /// <summary>
        ///  删除 UserRelatedDepartmentInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="db">数据库对象</param>
        ///<param name="transaction">事务</param>
        /// <returns>返回删除的记录数目数目</returns>
        public int DeleteByUserId(decimal userId, SqlDatabase db, DbTransaction transaction)
        {
            int count = 0;

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserTypeScope ");
            sb.Append("WHERE UserId = @UserId");

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

        #region 私有方法

        #region 默认私有方法

        #endregion

        #region 自定义私有方法

        #endregion

        #endregion
    }
}
