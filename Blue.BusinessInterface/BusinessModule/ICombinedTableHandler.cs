//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICombinedTableHandler.cs
// 描述: CombinedTable 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/15
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
    /// <summary>
    /// CombinedTable 接口
    /// </summary>
    public interface ICombinedTableHandler : ICommonNodeBusiness, IPrincipalBusiness<CombinedTableInfo>
    {
        #region 接口

        /// <summary>
        /// 获得组合表的记录数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
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
        DataSet GetTableData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        int GetRecordCount(decimal userId, decimal combinedTableId, bool businessEnabled, decimal instanceId);

        /// <summary>
        /// 获得组合表的分页数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataTable GetCombinedTableData(decimal combinedTableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId,
            int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得不同类型的表的数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataTableType"></param>
        /// <returns></returns>
        int GetTableCountByTableType(decimal combinedTableId, DataTableType dataTableType);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        Dictionary<decimal, DataRowItem> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId, bool onlyTarget);

        /// <summary>
        /// 获得表的编号列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        IList<decimal> GetTableIds(decimal combinedTableId);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        Dictionary<decimal, DataTable> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId);

        /// <summary>
        /// 获得组合表的数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
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
        Dictionary<decimal, DataTable> GetDataFilledData(decimal combinedTableId, bool businessEnabled,
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId);

        /// <summary>
        /// 更新组合表信息
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <param name="combinedTableRelationInfos"></param>
        void Update(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos);

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <param name="combinedTableRelationInfos">关系表</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos);

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos);

        /// <summary>
        /// 获得组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        List<CommonNode> GetDataFields(decimal combinedTableId);

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        IList<CommonNode> GetTables(decimal combinedTableId);

        #endregion
    }
}