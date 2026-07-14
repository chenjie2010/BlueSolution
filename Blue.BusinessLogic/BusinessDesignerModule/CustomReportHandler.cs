//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomReportHandler.cs
// 描述：CustomReport 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomReport.
    /// </summary>
    public class CustomReportHandler : CommonNodeBusiness, ICustomReportHandler
    {
        #region 工厂类实例

        private static readonly ICustomReport dalCustomReport = BusinessDesignerDataAccessFactory.CreateCustomReport(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomReportHandler() : base(dalCustomReport)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customreport 表中插入一条新记录
		/// </summary>
		/// <param name="customReportInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomReportInfo customReportInfo)
		{
            //自动增加的关键字的值
			decimal customReportId = 0;
            
			// 验证输入
			if (customReportInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customReportId = dalCustomReport.Insert(customReportInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customReportId;
		}
        
        /// <summary>
		/// 获得 CustomReportInfo 对象
		/// </summary>
		///<param name="reportId">报表编号编号</param>
		/// <returns> CustomReportInfo 对象</returns>
		public CustomReportInfo GetModelInfo(decimal reportId)
		{			
			CustomReportInfo  customReportInfo = null;
            
			// 验证输入
			if(reportId < 0)
            {
				return null;
            }

            try
            {
                customReportInfo =  dalCustomReport.GetModelInfo(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customReportInfo;
		}        
        
        /// <summary>
		/// 更新 CustomReportInfo 对象
		/// </summary>
		/// <param name="customReportInfo">CustomReportInfo 对象</param>
		public void Update(CustomReportInfo customReportInfo)
		{	
            // 验证输入
            if (customReportInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomReport.Update(customReportInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomReportInfo 对象
		/// </summary>
		///<param name="reportId">报表编号编号</param>
		/// <returns> CustomReportInfo 对象</returns>
		public void Delete(decimal reportId)
		{		
            // 验证输入
			if(reportId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomReport.Delete(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomReportInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomReportInfo 对象列表</returns>
		public IList<CustomReportInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomReportInfo>  customReportInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customReportInfos = dalCustomReport.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customReportInfos;
		}               
        
        /// <summary>
		/// 获得 CustomReport 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomReportInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomReport.GetTotalCount(whereConditons);
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
        /// 根据报表类型条件获得报表节点
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="reportCategory"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal groupId, ReportCategory reportCategory)
        {
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalCustomReport.GetCommonNodes(groupId, reportCategory);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得报表名称
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public string GetReportName(decimal reportId)
        {
            string reportName = string.Empty;

            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                reportName = dalCustomReport.GetReportName(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reportName;
        }

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        public void SheetSaveAs(decimal sheetId, decimal reportId, byte[] data)
        {
            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dalCustomReport.SheetSaveAs(sheetId, reportId, data);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 表套另存
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="groupId"></param>
        public void SaveAs(decimal reportId, decimal groupId)
        {
            // 验证输入
            if (groupId <= 0)
            {
                throw new ArgumentException("参数异常。分组编号不能小于或等于0");
            }

            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dalCustomReport.SaveAs(reportId, groupId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得表套的所属数据仓库
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal reportId)
        {
            byte dataWarehouseId = 0;

            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dataWarehouseId = dalCustomReport.GetDataWarehouseId(reportId);
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
