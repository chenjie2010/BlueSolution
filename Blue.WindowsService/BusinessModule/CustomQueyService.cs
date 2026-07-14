//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomQueyService.cs
// 描述：CustomQuey 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/31
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomQuey.
    /// </summary>
    public class CustomQueyService : CommonNodeServices, ICustomQueyContract
    {
        #region 业务实例

        private static readonly ICustomQueyHandler customQueyHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomQueyHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomQueyService() : base(customQueyHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customquey 表中插入一条新记录
        /// </summary>
        /// <param name="customQueyInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomQueyInfo customQueyInfo)
        {
            return customQueyHandler.Insert(customQueyInfo);
        }

        /// <summary>
        /// 获得 CustomQueyInfo 对象
        /// </summary>
        ///<param name="dataQueriedId">数据查询编号</param>
        /// <returns> CustomQueyInfo 对象</returns>
        public CustomQueyInfo GetModelInfo(decimal dataQueriedId)
        {
            return customQueyHandler.GetModelInfo(dataQueriedId);
        }

        /// <summary>
        /// 更新 CustomQueyInfo 对象
        /// </summary>
        /// <param name="customQueyInfo">CustomQueyInfo 对象</param>
        public void Update(CustomQueyInfo customQueyInfo)
        {
            customQueyHandler.Update(customQueyInfo);
        }

        /// <summary>
        /// 删除 CustomQueyInfo 对象
        /// </summary>
        ///<param name="dataQueriedId">数据查询编号</param>
        /// <returns> CustomQueyInfo 对象</returns>
        public void Delete(decimal dataQueriedId)
        {
            customQueyHandler.Delete(dataQueriedId);
        }

        /// <summary>
        /// 获得 CustomQueyInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomQueyInfo 对象列表</returns>
        public IList<CustomQueyInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customQueyHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customQueyHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得分页数据集记录数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public int GetAuthorizedRecordCount(QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, byte dataWarehouseId)
        {
            return customQueyHandler.GetAuthorizedRecordCount(queryBuilder, whereConditons, dataWarehouseId);            
        }

        /// <summary>
        /// 完全删除记录(多表联合查询使用)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tableIds"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        public void Delete(decimal dataWarehouseId, IList<decimal> tableIds, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons)
        {
            customQueyHandler.Delete(dataWarehouseId, tableIds, queryBuilder, whereConditons);
        }

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count)
        {
            return customQueyHandler.GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count);
        }

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
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, ref int totalCount)
        {
            return customQueyHandler.GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count, ref totalCount);
        }

        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        public int GetTotalCountByViewId(decimal viewId)
        {
            return customQueyHandler.GetTotalCountByViewId(viewId);
        }

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
        public DataTable GetQueriedData(decimal dataQueriedId, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return customQueyHandler.GetQueriedData(dataQueriedId, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetCustomDataFieldInfos(decimal dataQueriedId)
        {
            return customQueyHandler.GetCustomDataFieldInfos(dataQueriedId);
        }

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDigitDataFields(decimal dataQueriedId)
        {
            return customQueyHandler.GetDigitDataFields(dataQueriedId);
        }

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetConditionalCustomDataFieldInfos(decimal dataQueriedId)
        {
            return customQueyHandler.GetConditionalCustomDataFieldInfos(dataQueriedId);
        }

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetAssociatedDataFields(decimal dataQueriedId)
        {
            return customQueyHandler.GetAssociatedDataFields(dataQueriedId);
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateCustomQueyAndDataFieldSorting(decimal dataQueriedId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            customQueyHandler.UpdateCustomQueyAndDataFieldSorting(dataQueriedId, dataFieldId, movedDriectionOfNode);
        }

        /// <summary>
        /// 获得自定义查询中的视图字段（不包含系统字段）
        /// </summary>
        /// <param name="dataQueriedId">查询编号</param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal dataQueriedId)
        {
            return customQueyHandler.GetDataFields(dataQueriedId);
        }

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public DataSet GetCustomQueyAndDataFieldInfos(decimal dataQueriedId)
        {
            return customQueyHandler.GetCustomQueyAndDataFieldInfos(dataQueriedId);
        }

        /// <summary>
        /// 向 CustomQueyAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">customQueyAndDataFieldInfo 对象</param>
        public void InsertCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            customQueyHandler.InsertCustomQueyAndDataFieldInfo(customQueyAndDataFieldInfo);
        }

        /// <summary>
		/// 获得 CustomQueyAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyAndDataFieldInfo 对象</returns>
		public CustomQueyAndDataFieldInfo GetCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId)
        {
            return customQueyHandler.GetCustomQueyAndDataFieldInfo(dataFieldId, dataQueriedId);
        }

        /// <summary>
        /// 更新 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">CustomQueyAndDataFieldInfo 对象</param>
        public void UpdateCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            customQueyHandler.UpdateCustomQueyAndDataFieldInfo(customQueyAndDataFieldInfo);
        }

        /// <summary>
		///  删除 CustomQueyAndDataFieldInfo 对象
		/// </summary>
	    ///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		public void DeleteCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId)
        {
            customQueyHandler.DeleteCustomQueyAndDataFieldInfo(dataFieldId, dataQueriedId);
        }

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereSentences(decimal dataQueriedId, string whereExpression)
        {
            return customQueyHandler.ValidateWhereSentences(dataQueriedId, whereExpression);
        }
               
        /// <summary>
        /// 获得SQL语句
        /// </summary>
        ///<param name="dataQueriedId">查询编号</param>
        /// <returns> SQL语句</returns>
        public string GetConditions(decimal dataQueriedId)
        {
            return customQueyHandler.GetConditions(dataQueriedId);
        }
        
        /// <summary>
        /// 验证SQL语句是否正确
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns></returns>
        public bool ValidateSQL(byte dataWarehouseId, string sql)
        {
            return customQueyHandler.ValidateSQL(dataWarehouseId, sql);
        }

        #endregion
    }
}
