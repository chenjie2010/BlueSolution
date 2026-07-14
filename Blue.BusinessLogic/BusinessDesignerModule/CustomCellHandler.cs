//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCellHandler.cs
// 描述: CustomCell 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomCell.
    /// </summary>
    public class CustomCellHandler : ICustomCellHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomCell dalCustomCell = BusinessDesignerDataAccessFactory.CreateCustomCell();
        private static readonly ICellStyle dalCellStyle = BusinessDesignerDataAccessFactory.CreateCellStyle();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomCellHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customcell 表中插入一条新记录
		/// </summary>
		/// <param name="customCellInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomCellInfo customCellInfo)
		{
            //自动增加的关键字的值
			decimal customCellId = 0;
            
			// 验证输入
			if (customCellInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customCellId = dalCustomCell.Insert(customCellInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customCellId;
		}
        
        /// <summary>
		/// 获得 CustomCellInfo 对象
		/// </summary>
		///<param name="cellId">CellId</param>
		/// <returns> CustomCellInfo 对象</returns>
		public CustomCellInfo GetModelInfo(decimal cellId)
		{			
			CustomCellInfo  customCellInfo = null;
            
			// 验证输入
			if(cellId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customCellInfo =  dalCustomCell.GetModelInfo(cellId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customCellInfo;
		}        
        
        /// <summary>
		/// 更新 CustomCellInfo 对象
		/// </summary>
		/// <param name="customCellInfo">CustomCellInfo 对象</param>
		public void Update(CustomCellInfo customCellInfo)
		{	
            // 验证输入
            if (customCellInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomCell.Update(customCellInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomCellInfo 对象
		/// </summary>
		///<param name="cellId">CellId</param>
		/// <returns> CustomCellInfo 对象</returns>
		public void Delete(decimal cellId)
		{		
            // 验证输入
			if(cellId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomCell.Delete(cellId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomCellInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCellInfo  对象列表</returns>
		public IList<CustomCellInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomCellInfo>  customCellInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customCellInfos = dalCustomCell.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customCellInfos;
		}               
        
        /// <summary>
		/// 获得 CustomSheet 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSheetInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomCell.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
		}

        #endregion

        #region 自定义方法

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId, byte cellType)
        {
            IList<CustomCellInfo> customCellInfos = null;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customCellInfos = dalCustomCell.GetModelInfos(sheetId, cellType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCellInfos;
        }
        
        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId)
        {
            IList<CustomCellInfo> customCellInfos = null;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customCellInfos = dalCustomCell.GetModelInfos(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCellInfos;
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
            CustomCellInfo customCellInfo = null;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customCellInfo = dalCustomCell.GetModelInfo(sheetId, row, col);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCellInfo;
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
            int count = 0;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomCell.Delete(sheetId, row, col);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 更新 CustomCellInfo 对象的条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="conditionText"></param>
        public void Update(decimal cellId, string conditionText)
        {
            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.Update(cellId, conditionText);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.UpdateDataCellRowAndCol(sheetId, start, count, cellRowAndCol);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            bool result = false;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                result = dalCustomCell.IncludeDataCell(sheetId, start, count, cellRowAndCol);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
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
            bool result = false;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                result = dalCustomCell.IncludeDataCell(sheetId, row, col, rowCount, colCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
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
            bool result = false;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                result = dalCustomCell.IncludeExtendDataCell(sheetId, start, count, cellRowAndCol, insertOrDelete);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
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
            bool result = false;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                result = dalCustomCell.VaildateCellCondition(tableId, template, condition, tableLinks);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
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
            DataTable dataTable = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomCell.GetCellRangeData(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            Dictionary<decimal, string> extendedRowTextData = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                extendedRowTextData = dalCustomCell.GetExtendedRowTextData(cellId, dataWarehouseId, condition, templateText);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedRowTextData;
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
            string rowCellText = string.Empty;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                rowCellText = dalCustomCell.GetRowCellText(cellId, dataWarehouseId, condition, templateText, whereConditons,
                    relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return rowCellText;
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
            DataTable dataTable = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomCell.GetRowCellStatiscsDetail(cellId, dataWarehouseId,  whereConditons,
                    relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            string statiscsText = string.Empty;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                statiscsText = dalCustomCell.GetRowCellStatiscsText(cellId, dataWarehouseId, whereConditons, 
                    relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return statiscsText;
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
            DataTable dataTable = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomCell.GetCellDetail(cellId, dataWarehouseId, dataFieldIds, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            DataTable dataTable = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomCell.GetCellDetail(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            TextIntValue cellText = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                cellText = dalCustomCell.GetCellText(cellId, dataWarehouseId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellText;
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
            string cellText = string.Empty;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                cellText = dalCustomCell.GetCellText(cellId, userId, tableId, condition, templateText);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellText;
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
            DataSet ds = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                ds = dalCustomCell.GetExtendRowColData(cellId, userId, tableId, condition);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 清除单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        public void ClearDataFieldCondition(decimal cellId)
        {
            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.ClearDataFieldCondition(cellId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.SaveDataFieldCondition(cellId, tableId, condition, template, inputCellType, number, 
                    dataFieldConditions, dataFieldShows);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.SaveDataFieldCondition(cellStyleIds, tableId, inputCellType, number, dataFieldConditions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomCell.SaveDataFieldCondition(cellId, tableId, inputCellType, number, dataFieldConditions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByCellId(decimal cellId, CellCondition cellCondition)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCellStyle.GetCommonNodes(cellId, cellCondition);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId)
        {
            IList<CellStyleInfo> cellStyleInfos = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                cellStyleInfos = dalCellStyle.GetModelInfos(cellId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellStyleInfos;
        }

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public IList<CellStyleInfo> GetModelInfosByCellId(decimal cellId, CellCondition cellCondition)
        {
            IList<CellStyleInfo> cellStyleInfos = null;

            // 验证输入
            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                cellStyleInfos = dalCellStyle.GetModelInfos(cellId, cellCondition);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellStyleInfos;
        }

        /// <summary>
        /// 获得表套所属的数据仓库编号
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal cellId)
        {
            byte dataWarehouseId = 0;

            if (cellId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataWarehouseId = dalCustomCell.GetDataWarehouseId(cellId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
