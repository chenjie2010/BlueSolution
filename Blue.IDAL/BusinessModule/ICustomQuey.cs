//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomQuey.cs
// 描述：CustomQuey 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/31
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// CustomQuey 接口
    /// </summary>
    public interface ICustomQuey : ICommonNode, IPrincipalTable<CustomQueyInfo>
    {
        #region 接口

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count);

        /// <summary>
        /// 获得分页数据集记录数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        int GetAuthorizedRecordCount(QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, byte dataWarehouseId);
        
        /// <summary>
        /// 完全删除记录(多表联合查询使用)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tableIds"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        void Delete(decimal dataWarehouseId, IList<decimal> tableIds, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        int GetTotalCountByViewId(decimal viewId);

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetQueriedData(decimal dataQueriedId, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得自定义查询中的视图字段（不包含系统字段）
        /// </summary>
        /// <param name="dataQueriedId">查询编号</param>
        /// <returns></returns>
        IList<CommonNode> GetDataFields(decimal dataQueriedId);

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool ValidateWhereSentences(decimal dataQueriedId, string whereExpression);     

        /// <summary>
        /// 获得SQL语句
        /// </summary>
        ///<param name="dataQueriedId">查询编号</param>
        /// <returns> SQL语句</returns>
        string GetConditions(decimal dataQueriedId);

        /// <summary>
        /// 验证SQL语句是否正确
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns></returns>
        bool ValidateSQL(byte dataWarehouseId, string sql);

        #endregion
    }
}