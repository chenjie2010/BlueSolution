//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSheetService.cs
// 描述: CustomSheet 操作服务类
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
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomSheet.
    /// </summary>
    public class CustomSheetService : CommonNodeServices, ICustomSheetContract
    {
        #region 业务实例
        
        private static readonly ICustomSheetHandler customSheetHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<ICustomSheetHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSheetService() : base(customSheetHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 CustomSheet 表中插入一条新记录
		/// </summary>
		/// <param name="customSheetInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomSheetInfo customSheetInfo)
		{
            return customSheetHandler.Insert(customSheetInfo);
		}
        
        /// <summary>
		/// 获得 CustomSheetInfo 对象
		/// </summary>
		///<param name="sheetId">样表编号</param>
		/// <returns> CustomSheetInfo 对象</returns>
		public CustomSheetInfo GetModelInfo(decimal sheetId)
		{	
            return customSheetHandler.GetModelInfo(sheetId);           
		}		
		
        /// <summary>
		/// 更新 CustomSheetInfo 对象
		/// </summary>
		/// <param name="customSheetInfo">CustomSheetInfo 对象</param>
		public void Update(CustomSheetInfo customSheetInfo)
		{	          
            customSheetHandler.Update(customSheetInfo);
        }	
  
        /// <summary>
		/// 删除 CustomSheetInfo 对象
		/// </summary>
		///<param name="sheetId">样表编号</param>
		/// <returns> CustomSheetInfo 对象</returns>
		public void Delete(decimal sheetId)
		{	
            customSheetHandler.Delete(sheetId);
        }
        
        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSheetInfo 对象列表</returns>
        public IList<CustomSheetInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customSheetHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomSheet 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomSheetInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customSheetHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void UpdateSheetSorting(decimal reportId, IList<string> sheetNames)
        {
            customSheetHandler.UpdateSheetSorting(reportId, sheetNames);
        }

        /// <summary>
        /// 获得报表文件
        /// </summary>
        /// <param name="reportId">报表编号</param>
        /// <returns></returns>
        public byte[] DownloadReportFile(decimal reportId)
        {
            return customSheetHandler.DownloadReportFile(reportId);
        }

        /// <summary>
        /// 保存报表文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportType"></param>
        /// <param name="data"></param>
        /// <param name="rowAndCols"></param>
        public void UploadReportFile(decimal reportId, byte[] data, Dictionary<decimal, RowAndCol> rowAndCols)
        {
            customSheetHandler.UploadReportFile(reportId, data, rowAndCols);
        }

        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<CustomSheetInfo> GetModelInfos(decimal reportId)
        {
            return customSheetHandler.GetModelInfos(reportId);
        }

        /// <summary>
        /// 批文编号自动加1
        /// </summary>
        /// <param name="sheetId"></param>
        public void AutoIncreaseApprovalNumber(decimal sheetId)
        {
            customSheetHandler.AutoIncreaseApprovalNumber(sheetId);
        }

        /// <summary>
        /// 更新边距
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="customMargin"></param>
        public void UpdateMargin(decimal sheetId, CustomMargin customMargin)
        {
            customSheetHandler.UpdateMargin(sheetId, customMargin);
        }

        /// <summary>
        /// 获得边距
        /// </summary>
        /// <param name="sheetId"></param>
        public CustomMargin GetMargin(decimal sheetId)
        {
            return customSheetHandler.GetMargin(sheetId);
        }

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public void Update(decimal sheetId, int rowCount, int columnCount)
        {
            customSheetHandler.Update(sheetId, rowCount, columnCount);
        }

        /// <summary>
        /// 获得样表的行列数
        /// </summary>
        /// <param name="SheetId"></param>
        /// <returns></returns>
        public RowAndCol GetRowAndColCountBySheetId(decimal sheetId)
        {
            return customSheetHandler.GetRowAndColCountBySheetId(sheetId);
        }

        /// <summary>
        /// 获得表套的样表行列数
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<RowAndCol> GetRowAndColCount(decimal reportId)
        {
            return customSheetHandler.GetRowAndColCount(reportId);
        }

        /// <summary>
        /// 导入 Excel 格式的文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void Insert(decimal reportId, IList<CustomSheetInfo> sheetNames)
        {
            customSheetHandler.Insert(reportId, sheetNames);
        }

        #endregion
    }
}
