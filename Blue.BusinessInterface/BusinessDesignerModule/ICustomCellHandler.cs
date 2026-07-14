//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomCellHandler.cs
// 描述: CustomCell 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.BusinessInterface.BusinessDesignerModule
{
    /// <summary>
    /// CustomCell 接口
    /// </summary>
    public interface ICustomCellHandler : IPrincipalBusiness<CustomCellInfo>
    {
        #region 接口

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        IList<CustomCellInfo> GetModelInfos(decimal sheetId, byte cellType);

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        IList<CustomCellInfo> GetModelInfos(decimal sheetId);

        /// <summary>
        /// 获得 CustomCellStyleInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        CustomCellInfo GetModelInfo(decimal sheetId, int row, int col);

        /// <summary>
        /// 删除 CustomCellStyleInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        int Delete(decimal sheetId, int row, int col);

        /// <summary>
        /// 更新 CustomCellInfo 对象的条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="conditionText"></param>
        void Update(decimal cellId, string conditionText);

        /// <summary>
        /// 更新数据单元格的行与列
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        void UpdateDataCellRowAndCol(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol);

        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <returns></returns>
        bool IncludeDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol);


        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <returns></returns>
        bool IncludeDataCell(decimal sheetId, int row, int col, int rowCount, int colCount);

        /// <summary>
        /// 是否包含行列扩展的数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <param name="insertOrDelete"></param>
        /// <returns></returns>
        bool IncludeExtendDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol, bool insertOrDelete);
        
        /// <summary>
        /// 验证条件是否正确
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="template"></param>
        /// <param name="condition"></param>
        /// <param name="tableLinks"></param>
        /// <returns></returns>
        bool VaildateCellCondition(decimal tableId, string template, string condition, List<TableLink> tableLinks);

        /// <summary>
        /// 获得行单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        string GetRowCellText(decimal cellId, byte dataWarehouseId, string condition, string templateText, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得单元格范围的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        DataTable GetCellRangeData(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得行扩展数据
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetExtendedRowTextData(decimal cellId, byte dataWarehouseId, string condition, string templateText);

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        TextIntValue GetCellText(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得统计数据的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        DataTable GetRowCellStatiscsDetail(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得行单元格的统计值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        string GetRowCellStatiscsText(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons,
                           IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得单元格的统计值的详情（含自定义字段）
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<decimal> dataFieldIds, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得单元格的统计值的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes);

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        string GetCellText(decimal cellId, decimal userId, decimal tableId, string condition, string templateText);

        /// <summary>
        /// 获得扩展行列数据
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        DataSet GetExtendRowColData(decimal cellId, decimal userId, decimal tableId, string condition);

        /// <summary>
        /// 清除单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        void ClearDataFieldCondition(decimal cellId);

        /// <summary>
        /// 保存单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="template"></param>
        /// <param name="inputCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        /// <param name="dataFieldShows"></param>
        void SaveDataFieldCondition(decimal cellId, decimal tableId, string condition, string template,
            byte inputCellType, int number, IList<CommonNode> dataFieldConditions, IList<CommonNode> dataFieldShows);

        /// <summary>
        /// 批量保存单元格的字段条件
        /// </summary>
        /// <param name="cellStyleIds"></param>
        /// <param name="tableId"></param>
        /// <param name="reportCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        void SaveDataFieldCondition(IList<decimal> cellStyleIds, decimal tableId, byte inputCellType, int number, IList<CommonNode> dataFieldConditions);

        /// <summary>
        /// 保存单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="tableId"></param>
        /// <param name="inputCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        void SaveDataFieldCondition(decimal cellId, decimal tableId, byte inputCellType, int number, IList<CommonNode> dataFieldConditions);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodesByCellId(decimal cellId, CellCondition cellCondition);

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId);

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId, CellCondition cellCondition);

        /// <summary>
        /// 获得表套所属的数据仓库编号
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        byte GetDataWarehouseId(decimal cellId);

        #endregion
    }
}