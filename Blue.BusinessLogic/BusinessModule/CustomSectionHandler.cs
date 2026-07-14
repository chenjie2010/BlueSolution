//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSectionHandler.cs
// 描述: CustomSection 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/13
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomSection.
    /// </summary>
    public class CustomSectionHandler : CommonNodeBusiness, ICustomSectionHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomSection dalCustomSection = BusinessDataAccessFactory.CreateCustomSection(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSectionHandler() : base(dalCustomSection)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customsection 表中插入一条新记录
		/// </summary>
		/// <param name="customSectionInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomSectionInfo customSectionInfo)
		{
            //自动增加的关键字的值
			decimal customSectionId = 0;
            
			// 验证输入
			if (customSectionInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customSectionId = dalCustomSection.Insert(customSectionInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customSectionId;
		}
        
        /// <summary>
		/// 获得 CustomSectionInfo 对象
		/// </summary>
		///<param name="sectionId">窗体分组编号</param>
		/// <returns> CustomSectionInfo 对象</returns>
		public CustomSectionInfo GetModelInfo(decimal sectionId)
		{			
			CustomSectionInfo  customSectionInfo = null;
            
			// 验证输入
			if(sectionId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customSectionInfo =  dalCustomSection.GetModelInfo(sectionId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customSectionInfo;
		}        
        
        /// <summary>
		/// 更新 CustomSectionInfo 对象
		/// </summary>
		/// <param name="customSectionInfo">CustomSectionInfo 对象</param>
		public void Update(CustomSectionInfo customSectionInfo)
		{	
            // 验证输入
            if (customSectionInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomSection.Update(customSectionInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomSectionInfo 对象
		/// </summary>
		///<param name="sectionId">窗体分组编号</param>
		/// <returns> CustomSectionInfo 对象</returns>
		public void Delete(decimal sectionId)
		{		
            // 验证输入
			if(sectionId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomSection.Delete(sectionId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomSectionInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSectionInfo  对象列表</returns>
		public IList<CustomSectionInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomSectionInfo>  customSectionInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customSectionInfos = dalCustomSection.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customSectionInfos;
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
                count = dalCustomSection.GetTotalCount(whereConditons);
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
        /// 获得所有的窗体
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomSectionInfo> GetModelInfos(decimal dataId)
        {
            //创建集合对象
            IList<CustomSectionInfo> customSectionInfos = null;

            // 验证输入
            if (dataId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                customSectionInfos = dalCustomSection.GetModelInfos(dataId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customSectionInfos;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
