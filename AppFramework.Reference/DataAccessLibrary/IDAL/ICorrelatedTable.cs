//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICorrelatedTable.cs
// 描述： ICorrelatedTable 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Core;

namespace AppFramework.Reference.DataAccessLibrary
{
    /// <summary>
    /// ICorrelatedTable 接口，联系表类型：它的主键由其它两个表的主键共同构成
    /// </summary>
    public interface ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 向联系表中插入一条新记录
        /// </summary>
        /// <param name="correlatedModel">correlatedModel 对象</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
        void Insert(CorrelatedModel correlatedModel, SqlDatabase db, DbTransaction transaction);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="correlatedModel">correlatedModel 对象</param>        
        void Update(CorrelatedModel correlatedModel);

        /// <summary>
        /// 获得模型
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <param name="secondForeignKey"></param>
        /// <returns></returns>
        CorrelatedModel GetCorrelatedModel(decimal firstForeignKey, decimal secondForeignKey);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="firstKeyValues"></param>
        /// <param name="secondKeyValue"></param>
        /// <param name="rangeValue"></param>
        void Update(IList<decimal> firstKeyValues, decimal secondKeyValue, byte rangeValue);

        /// <summary>
		///  删除 CorrelatedModel 对象
		/// </summary>
	    ///<param name="firstForeignKey">外键一的值</param>
		///<param name="secondForeignKey">外键二的值</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
		void Delete(decimal firstForeignKey, decimal secondForeignKey, SqlDatabase db, DbTransaction transaction);

        /// <summary>
		///  删除 CorrelatedModel 对象
		/// </summary>
	    ///<param name="firstForeignKey">外键一的值</param>
		///<param name="secondForeignKey">外键二的值</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
		void Delete(IList<decimal> firstForeignKeys, decimal secondForeignKey, SqlDatabase db, DbTransaction transaction);

        /// <summary>
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        ///<param name="secondForeignKeys">外键二的值集合</param>
        /// <param name="db">数据库对象</param>
        /// /// <param name="transaction">事务</param>
        void Delete(decimal firstForeignKey, IList<decimal> secondForeignKeys, SqlDatabase db, DbTransaction transaction);

        /// <summary>
        ///  删除 CorrelatedModel 对象
        /// </summary>
        ///<param name="firstForeignKey">外键一的值</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        int Delete(decimal firstForeignKey, SqlDatabase db, DbTransaction transaction);

        /// <summary>
		///  删除 CorrelatedModel 对象
		/// </summary>
	    ///<param name="secondForeignKey">用户编号</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
		int DeleteBySecondForeignKey(decimal secondForeignKey, SqlDatabase db, DbTransaction transaction);

        /// <summary>
        /// 根据用户编号获得用户类型
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        IList<decimal> GetSecondIds(decimal firstForeignKey);

        /// <summary>
        /// 获得表中记录的数目
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        int GetTotalCountByFirstForeignKey(decimal firstForeignKey);

        /// <summary>
        /// 获得表中记录的数目
        /// </summary>
        /// <param name="secondForeignKey"></param>
        /// <returns></returns>
        int GetTotalCountBySecondForeignKey(decimal secondForeignKey);

        #endregion
    }
}
