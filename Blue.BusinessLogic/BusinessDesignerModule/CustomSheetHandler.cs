//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSheetHandler.cs
// 描述: CustomSheet 业务处理类
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
using AppFramework.Reference.BusinessLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomSheet.
    /// </summary>
    public class CustomSheetHandler : CommonNodeBusiness, ICustomSheetHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomSheet dalCustomSheet = BusinessDesignerDataAccessFactory.CreateCustomSheet(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSheetHandler() : base(dalCustomSheet)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customsheet 表中插入一条新记录
		/// </summary>
		/// <param name="customSheetInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomSheetInfo customSheetInfo)
		{
            //自动增加的关键字的值
			decimal customSheetId = 0;
            
			// 验证输入
			if (customSheetInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customSheetId = dalCustomSheet.Insert(customSheetInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customSheetId;
		}
        
        /// <summary>
		/// 获得 CustomSheetInfo 对象
		/// </summary>
		///<param name="sheetId">样表编号</param>
		/// <returns> CustomSheetInfo 对象</returns>
		public CustomSheetInfo GetModelInfo(decimal sheetId)
		{			
			CustomSheetInfo  customSheetInfo = null;
            
			// 验证输入
			if(sheetId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customSheetInfo =  dalCustomSheet.GetModelInfo(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customSheetInfo;
		}        
        
        /// <summary>
		/// 更新 CustomSheetInfo 对象
		/// </summary>
		/// <param name="customSheetInfo">CustomSheetInfo 对象</param>
		public void Update(CustomSheetInfo customSheetInfo)
		{	
            // 验证输入
            if (customSheetInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomSheet.Update(customSheetInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomSheetInfo 对象
		/// </summary>
		///<param name="sheetId">样表编号</param>
		/// <returns> CustomSheetInfo 对象</returns>
		public void Delete(decimal sheetId)
		{		
            // 验证输入
			if(sheetId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomSheet.Delete(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomSheetInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSheetInfo  对象列表</returns>
		public IList<CustomSheetInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomSheetInfo>  customSheetInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customSheetInfos = dalCustomSheet.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customSheetInfos;
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
                count = dalCustomSheet.GetTotalCount(whereConditons);
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
        /// 更新排序
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void UpdateSheetSorting(decimal reportId, IList<string> sheetNames)
        {
            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.UpdateSheetSorting(reportId, sheetNames);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得报表文件
        /// </summary>
        /// <param name="reportId">报表编号</param>
        /// <returns></returns>
        public byte[] DownloadReportFile(decimal reportId)
        {
            byte[] data = null;

            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                data = dalCustomSheet.DownloadReportFile(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
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
            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.UploadReportFile(reportId, data, rowAndCols);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<CustomSheetInfo> GetModelInfos(decimal reportId)
        {
            IList<CustomSheetInfo> customSheetInfos = null;

            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                customSheetInfos = dalCustomSheet.GetModelInfos(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customSheetInfos;
        }

        /// <summary>
        /// 批文编号自动加1
        /// </summary>
        /// <param name="sheetId"></param>
        public void AutoIncreaseApprovalNumber(decimal sheetId)
        {
            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.AutoIncreaseApprovalNumber(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新边距
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="customMargin"></param>
        public void UpdateMargin(decimal sheetId, CustomMargin customMargin)
        {
            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.UpdateMargin(sheetId, customMargin);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得边距
        /// </summary>
        /// <param name="sheetId"></param>
        public CustomMargin GetMargin(decimal sheetId)
        {
            CustomMargin customMargin = null;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            try
            {
                customMargin = dalCustomSheet.GetMargin(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customMargin;
        }

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public void Update(decimal sheetId, int rowCount, int columnCount)
        {
            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.Update(sheetId, rowCount, columnCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得样表的行列数
        /// </summary>
        /// <param name="SheetId"></param>
        /// <returns></returns>
        public RowAndCol GetRowAndColCountBySheetId(decimal sheetId)
        {
            RowAndCol rowAndCol = null;

            // 验证输入
            if (sheetId <= 0)
            {
                throw new ArgumentException("参数异常。样表编号不能小于或等于0");
            }

            try
            {
                rowAndCol = dalCustomSheet.GetRowAndColCountBySheetId(sheetId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return rowAndCol;
        }

        /// <summary>
        /// 获得表套的样表行列数
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<RowAndCol> GetRowAndColCount(decimal reportId)
        {
            IList<RowAndCol> rowAndCols = null;

            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                rowAndCols = dalCustomSheet.GetRowAndColCount(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return rowAndCols;
        }

        /// <summary>
        /// 导入 Excel 格式的文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void Insert(decimal reportId, IList<CustomSheetInfo> sheetNames)
        {
            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("参数异常。报表编号不能小于或等于0");
            }

            try
            {
                dalCustomSheet.Insert(reportId, sheetNames);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
