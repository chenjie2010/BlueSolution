//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICombinedTableContract.cs
// 描述: CombinedTable 契约层接口
// 作者：ChenJie 
// 编写日期：2018/8/15
// Copyright 2018
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
    /// CombinedTable 契约接口
    /// </summary>
    [ServiceContract(Name = "ICombinedTableContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICombinedTableContract : ICommonNodeContract, IPrincipalContracts<CombinedTableInfo>
    {
        #region 自定义接口


        /// <summary>
        /// 获得组合表的记录数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRecordCountByTableId")]
        int GetRecordCount(decimal combinedTableId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得组合表的记录
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableDataByTableId")]
        DataSet GetTableData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetMirrorRowDataByConditions")]
        Dictionary<decimal, DataRowItem> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId, bool onlyTarget);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetMirrorRowData")]
        Dictionary<decimal, DataTable> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId);

        /// <summary>
        /// 获得表的编号列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableIds")]
        IList<decimal> GetTableIds(decimal combinedTableId);

        /// <summary>
        /// 获得组合表的数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCombinedTableData")]
        DataTable GetCombinedTableData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFilledData")]
        Dictionary<decimal, DataTable> GetDataFilledData(decimal combinedTableId, bool businessEnabled,
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId);
        
        /// <summary>
        /// 更新组合表信息
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <param name="combinedTableRelationInfos"></param>
        [OperationContract(Name = "UpdateCombinedTableInfo")]
        void Update(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos);

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <param name="combinedTableRelationInfos">关系表</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertCombinedTableInfo")]
        decimal Insert(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos);

        /// <summary>
        /// 获得组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFields")]
        List<CommonNode> GetDataFields(decimal combinedTableId);

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTables")]
        IList<CommonNode> GetTables(decimal combinedTableId);

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        [OperationContract(Name = "UpdateDataFields")]
        void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos);

        #endregion
    }
}