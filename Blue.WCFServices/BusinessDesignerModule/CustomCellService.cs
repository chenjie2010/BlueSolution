//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCellService.cs
// 描述: CustomCell 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomCell.
    /// </summary>
    public class CustomCellService : ICustomCellContract
    {
        #region 业务实例
        
        private static readonly ICustomCellHandler customCellHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<ICustomCellHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomCellService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 CustomCell 表中插入一条新记录
		/// </summary>
		/// <param name="customCellInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomCellInfo customCellInfo)
		{
            return customCellHandler.Insert(customCellInfo);
		}
        
        /// <summary>
		/// 获得 CustomCellInfo 对象
		/// </summary>
		///<param name="cellId">CellId</param>
		/// <returns> CustomCellInfo 对象</returns>
		public CustomCellInfo GetModelInfo(decimal cellId)
		{	
            return customCellHandler.GetModelInfo(cellId);           
		}		
		
        /// <summary>
		/// 更新 CustomCellInfo 对象
		/// </summary>
		/// <param name="customCellInfo">CustomCellInfo 对象</param>
		public void Update(CustomCellInfo customCellInfo)
		{	          
            customCellHandler.Update(customCellInfo);
        }	
  
        /// <summary>
		/// 删除 CustomCellInfo 对象
		/// </summary>
		///<param name="cellId">CellId</param>
		/// <returns> CustomCellInfo 对象</returns>
		public void Delete(decimal cellId)
		{	
            customCellHandler.Delete(cellId);
        }
        
        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCellInfo 对象列表</returns>
        public IList<CustomCellInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customCellHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomCell 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomCellInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customCellHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId, byte cellType)
        {
            return customCellHandler.GetModelInfos(sheetId, cellType);
        }

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId)
        {
            return customCellHandler.GetModelInfos(sheetId);
        }

        /// <summary>
        /// 获得 CustomCellStyleInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public CustomCellInfo GetModelInfo(decimal sheetId, int row, int col)
        {
            return customCellHandler.GetModelInfo(sheetId, row, col);
        }

        /// <summary>
        /// 删除 CustomCellStyleInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int Delete(decimal sheetId, int row, int col)
        {
            return customCellHandler.Delete(sheetId, row, col);
        }

        /// <summary>
        /// 更新 CustomCellInfo 对象的条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="conditionText"></param>
        public void Update(decimal cellId, string conditionText)
        {
            customCellHandler.Update(cellId, conditionText);
        }

        /// <summary>
        /// 更新数据单元格的行与列
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        public void UpdateDataCellRowAndCol(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol)
        {
            customCellHandler.UpdateDataCellRowAndCol(sheetId, start, count, cellRowAndCol);
        }

        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <returns></returns>
        public bool IncludeDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol)
        {
            return customCellHandler.IncludeDataCell(sheetId, start, count, cellRowAndCol);
        }

        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <returns></returns>
        public bool IncludeDataCell(decimal sheetId, int row, int col, int rowCount, int colCount)
        {
            return customCellHandler.IncludeDataCell(sheetId, row, col, rowCount, colCount);
        }

        /// <summary>
        /// 是否包含行列扩展的数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <param name="insertOrDelete"></param>
        /// <returns></returns>
        public bool IncludeExtendDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol, bool insertOrDelete)
        {
            return customCellHandler.IncludeExtendDataCell(sheetId, start, count, cellRowAndCol, insertOrDelete);
        }

        /// <summary>
        /// 验证条件是否正确
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="template"></param>
        /// <param name="condition"></param>
        /// <param name="tableLinks"></param>
        /// <returns></returns>
        public bool VaildateCellCondition(decimal tableId, string template, string condition, List<TableLink> tableLinks)
        {
            return customCellHandler.VaildateCellCondition(tableId, template, condition, tableLinks);
        }

        /// <summary>
        /// 获得单元格范围的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellRangeData(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetCellRangeData(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得行扩展数据
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetExtendedRowTextData(decimal cellId, byte dataWarehouseId, string condition, string templateText)
        {
            return customCellHandler.GetExtendedRowTextData(cellId, dataWarehouseId, condition, templateText);
        }

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
        public string GetRowCellText(decimal cellId, byte dataWarehouseId, string condition, string templateText, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetRowCellText(cellId, dataWarehouseId, condition, templateText, whereConditons, 
                relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得统计数据的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetRowCellStatiscsDetail(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetRowCellStatiscsDetail(cellId, dataWarehouseId, whereConditons, 
                relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得行单元格的统计值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public string GetRowCellStatiscsText(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons, 
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetRowCellStatiscsText(cellId, dataWarehouseId, whereConditons, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得单元格的统计值的详情（含自定义字段）
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<decimal> dataFieldIds, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetCellDetail(cellId, dataWarehouseId, dataFieldIds, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得单元格的统计值的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetCellDetail(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public TextIntValue GetCellText(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            return customCellHandler.GetCellText(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        public string GetCellText(decimal cellId, decimal userId, decimal tableId, string condition, string templateText)
        {
            return customCellHandler.GetCellText(cellId, userId, tableId, condition, templateText);
        }

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
        public DataSet GetExtendRowColData(decimal cellId, decimal userId, decimal tableId, string condition)
        {
            return customCellHandler.GetExtendRowColData(cellId, userId, tableId, condition);
        }

        /// <summary>
        /// 清除单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        public void ClearDataFieldCondition(decimal cellId)
        {
            customCellHandler.ClearDataFieldCondition(cellId);
        }

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
        public void SaveDataFieldCondition(decimal cellId, decimal tableId, string condition, string template,
            byte inputCellType, int number, IList<CommonNode> dataFieldConditions, IList<CommonNode> dataFieldShows)
        {
            customCellHandler.SaveDataFieldCondition(cellId, tableId, condition, template, inputCellType, 
                number, dataFieldConditions, dataFieldShows);
        }

        /// <summary>
        /// 批量保存单元格的字段条件
        /// </summary>
        /// <param name="cellStyleIds"></param>
        /// <param name="tableId"></param>
        /// <param name="reportCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        public void SaveDataFieldCondition(IList<decimal> cellStyleIds, decimal tableId, byte inputCellType, int number, IList<CommonNode> dataFieldConditions)
        {
            customCellHandler.SaveDataFieldCondition(cellStyleIds, tableId, inputCellType, number, dataFieldConditions);
        }

        /// <summary>
        /// 保存单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="tableId"></param>
        /// <param name="inputCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        public void SaveDataFieldCondition(decimal cellId, decimal tableId, byte inputCellType, int number, IList<CommonNode> dataFieldConditions)
        {
            customCellHandler.SaveDataFieldCondition(cellId, tableId, inputCellType, number, dataFieldConditions);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByCellId(decimal cellId, CellCondition cellCondition)
        {
            return customCellHandler.GetCommonNodesByCellId(cellId, cellCondition);
        }

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId)
        {
            return customCellHandler.GetModelInfosByCellId(cellId);
        }

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId, CellCondition cellCondition)
        {
            return customCellHandler.GetModelInfosByCellId(cellId, cellCondition);
        }

        /// <summary>
        /// 获得表套所属的数据仓库编号
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal cellId)
        {
            return customCellHandler.GetDataWarehouseId(cellId);
        }

        #endregion
    }
}
