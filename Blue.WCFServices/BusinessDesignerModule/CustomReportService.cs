//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomReportService.cs
// 描述：CustomReport 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/9
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
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomReport.
    /// </summary>
    public class CustomReportService : CommonNodeServices, ICustomReportContract
    {
        #region 业务实例

        private static readonly ICustomReportHandler customReportHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<ICustomReportHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomReportService() : base(customReportHandler)
        {

		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customreport 表中插入一条新记录
		/// </summary>
		/// <param name="customReportInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomReportInfo customReportInfo)
		{
            return customReportHandler.Insert(customReportInfo);
		}
        
        /// <summary>
		/// 获得 CustomReportInfo 对象
		/// </summary>
		///<param name="reportId">报表编号编号</param>
		/// <returns> CustomReportInfo 对象</returns>
		public CustomReportInfo GetModelInfo(decimal reportId)
		{	
            return customReportHandler.GetModelInfo(reportId);           
		}		
		
        /// <summary>
		/// 更新 CustomReportInfo 对象
		/// </summary>
		/// <param name="customReportInfo">CustomReportInfo 对象</param>
		public void Update(CustomReportInfo customReportInfo)
		{	          
            customReportHandler.Update(customReportInfo);
        }	
  
        /// <summary>
		/// 删除 CustomReportInfo 对象
		/// </summary>
		///<param name="reportId">报表编号编号</param>
		/// <returns> CustomReportInfo 对象</returns>
		public void Delete(decimal reportId)
		{	
            customReportHandler.Delete(reportId);
        }
        
        /// <summary>
		/// 获得 CustomReportInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomReportInfo 对象列表</returns>
		public IList<CustomReportInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customReportHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomReport 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomReportInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customReportHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 根据报表类型条件获得报表节点
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="reportCategory"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal groupId, ReportCategory reportCategory)
        {
            return customReportHandler.GetCommonNodes(groupId, reportCategory);
        }

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        public void SheetSaveAs(decimal sheetId, decimal reportId, byte[] data)
        {
            customReportHandler.SheetSaveAs(sheetId, reportId, data);
        }

        /// <summary>
        /// 表套另存
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="groupId"></param>
        public void SaveAs(decimal reportId, decimal groupId)
        {
            customReportHandler.SaveAs(reportId, groupId);
        }

        /// <summary>
        /// 获得表套的所属数据仓库
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal reportId)
        {
           return customReportHandler.GetDataWarehouseId(reportId);
        }
        
        #endregion
    }
}
