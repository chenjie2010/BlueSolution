//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IDataBussinessContract.cs
// 描述： DataBussiness 契约层接口
// 作者：ChenJie 
// 编写日期：2019/05/17
// Copyright 201
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
    /// CustomWorkflowProcess 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowProcessContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface IDataBussinessContract
    {
        /// <summary>
        /// 更新当前状态查询条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        [OperationContract(Name = "UpdateCurrentState")]
        int UpdateCurrentState(decimal tableId, string where, CurrentState currentState);

        /// <summary>
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="where">查询字段条件</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecordByTableId")]
        DataSet GetPageRecordByTableId(decimal tableId, int startPosition, int count, string where, ref int totalCount);

        /// <summary>
        /// 针对行复制或是列替换来查询数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="rowColCopyType">复制类型</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns>获得查询的记录</returns>
        [OperationContract(Name = "GetQueriedData")]
        DataSet GetQueriedData(decimal sourceTableId, decimal destinationTableId, RowColCopyType rowColCopyType, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 批量复制数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>导入的记录数</returns>
        [OperationContract(Name = "ImportDatarRows")]
        int Import(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause);

        /// <summary>
        /// 字段数据替换
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系，key是源字段，value是目的字段</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>被替换的记录数目</returns>
        [OperationContract(Name = "UpdateColumns")]
        int Update(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause);

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableRecordCount")]
        int GetTableRecordCount(decimal tableId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>

        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得系统表分页数据集
        /// </summary>
        /// <param name="systemTableName">系统表</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetAuthorizedData")]
        DataSet GetAuthorizedData(SystemTable systemTableName, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);
    }
}
