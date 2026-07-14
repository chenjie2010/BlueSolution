//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomQueyContract.cs
// 描述： CustomQuey 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/31
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomQuey 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomQueyContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomQueyContract : ICommonNodeContract, IPrincipalContracts<CustomQueyInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得分页数据集记录数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        [ServiceKnownType(typeof(QueryFieldCollection))]
        [ServiceKnownType(typeof(QueryField))]
        [OperationContract(Name = "GetAuthorizedRecordCount")]
        int GetAuthorizedRecordCount(QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, byte dataWarehouseId);

        /// <summary>
        /// 完全删除记录(多表联合查询使用)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tableIds"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        [ServiceKnownType(typeof(QueryFieldCollection))]
        [ServiceKnownType(typeof(QueryField))]
        [OperationContract(Name = "DeleteByConditions")]
        void Delete(decimal dataWarehouseId, IList<decimal> tableIds, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [ServiceKnownType(typeof(QueryFieldCollection))]
        [ServiceKnownType(typeof(QueryField))]
        [OperationContract(Name = "GetAuthorizedDataWithoutCount")]
        DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count);

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
        [ServiceKnownType(typeof(QueryFieldCollection))]
        [ServiceKnownType(typeof(QueryField))]
        [OperationContract(Name = "GetAuthorizedData")]
        DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, ref int totalCount);
        
        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        [OperationContract(Name = "GetTotalCountByViewId")]
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
        [OperationContract(Name = "GetQueriedData")]
        DataTable GetQueriedData(decimal dataQueriedId, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomDataFieldInfos")]
        IList<CustomDataFieldInfo> GetCustomDataFieldInfos(decimal dataQueriedId);

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDigitDataFields")]
        IList<CommonNode> GetDigitDataFields(decimal dataQueriedId);

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetConditionalCustomDataFieldInfos")]
       IList<CustomDataFieldInfo> GetConditionalCustomDataFieldInfos(decimal dataQueriedId);
       
        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAssociatedDataFields")]
        IList<CommonNode> GetAssociatedDataFields(decimal dataQueriedId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        [OperationContract(Name = "UpdateCustomQueyAndDataFieldSorting")]
        void UpdateCustomQueyAndDataFieldSorting(decimal dataQueriedId, decimal dataFieldId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 获得自定义查询中的视图字段（不包含系统字段）
        /// </summary>
        /// <param name="dataQueriedId">查询编号</param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFields")]
        IList<CommonNode> GetDataFields(decimal dataQueriedId);

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomQueyAndDataFieldInfos")]
        DataSet GetCustomQueyAndDataFieldInfos(decimal dataQueriedId);

        /// <summary>
		/// 向 CustomQueyAndDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customQueyAndDataFieldInfo">customQueyAndDataFieldInfo 对象</param>
        [OperationContract(Name = "InsertCustomQueyAndDataFieldInfo")]
        void InsertCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo);

        /// <summary>
		/// 获得 CustomQueyAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyAndDataFieldInfo 对象</returns>
        [OperationContract(Name = "GetCustomQueyAndDataFieldInfo")]
        CustomQueyAndDataFieldInfo GetCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId);

        /// <summary>
        /// 更新 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">CustomQueyAndDataFieldInfo 对象</param>
        [OperationContract(Name = "UpdateCustomQueyAndDataFieldInfo")]
        void UpdateCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo);

        /// <summary>
        ///  删除 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataQueriedId">数据查询编号</param>
        [OperationContract(Name = "DeleteCustomQueyAndDataFieldInfo")]
        void DeleteCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId);

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateWhereSentences")]
        bool ValidateWhereSentences(decimal dataQueriedId, string whereExpression);
              
        /// <summary>
        /// 获得SQL语句
        /// </summary>
        ///<param name="dataQueriedId">查询编号</param>
        /// <returns> SQL语句</returns>
        [OperationContract(Name = "GetConditions")]
        string GetConditions(decimal dataQueriedId);
        
        /// <summary>
        /// 验证SQL语句是否正确
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateSQL")]
        bool ValidateSQL(byte dataWarehouseId, string sql);

        #endregion
    }
}