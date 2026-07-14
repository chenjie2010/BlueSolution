//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSnapshotHandler.cs
// 描述: CustomSnapshot 业务处理类
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
    /// 业务层处理类，对于的表： dbo.CustomSnapshot.
    /// </summary>
    public class CustomSnapshotHandler : ICustomSnapshotHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomSnapshot dalCustomSnapshot = BusinessDesignerDataAccessFactory.CreateCustomSnapshot(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSnapshotHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customsnapshot 表中插入一条新记录
		/// </summary>
		/// <param name="customSnapshotInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomSnapshotInfo customSnapshotInfo)
		{
            //自动增加的关键字的值
			decimal customSnapshotId = 0;
            
			// 验证输入
			if (customSnapshotInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customSnapshotId = dalCustomSnapshot.Insert(customSnapshotInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customSnapshotId;
		}
        
        /// <summary>
		/// 获得 CustomSnapshotInfo 对象
		/// </summary>
		///<param name="snapshotId">快照编号</param>
		/// <returns> CustomSnapshotInfo 对象</returns>
		public CustomSnapshotInfo GetModelInfo(decimal snapshotId)
		{			
			CustomSnapshotInfo  customSnapshotInfo = null;
            
			// 验证输入
			if(snapshotId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customSnapshotInfo =  dalCustomSnapshot.GetModelInfo(snapshotId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customSnapshotInfo;
		}        
        
        /// <summary>
		/// 更新 CustomSnapshotInfo 对象
		/// </summary>
		/// <param name="customSnapshotInfo">CustomSnapshotInfo 对象</param>
		public void Update(CustomSnapshotInfo customSnapshotInfo)
		{	
            // 验证输入
            if (customSnapshotInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomSnapshot.Update(customSnapshotInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomSnapshotInfo 对象
		/// </summary>
		///<param name="snapshotId">快照编号</param>
		/// <returns> CustomSnapshotInfo 对象</returns>
		public void Delete(decimal snapshotId)
		{		
            // 验证输入
			if(snapshotId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomSnapshot.Delete(snapshotId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomSnapshotInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSnapshotInfo  对象列表</returns>
		public IList<CustomSnapshotInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomSnapshotInfo>  customSnapshotInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customSnapshotInfos = dalCustomSnapshot.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customSnapshotInfos;
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
                count = dalCustomSnapshot.GetTotalCount(whereConditons);
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
        /// 获得统计类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId)
        {
            IList<CommonItem<decimal>> commonItems = null;

            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonItems = dalCustomSnapshot.GetCommonItems(reportId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonItems;
        }

        /// <summary>
        /// 获得基础类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId, decimal userId)
        {
            IList<CommonItem<decimal>> commonItems = null;

            // 验证输入
            if (reportId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonItems = dalCustomSnapshot.GetCommonItems(reportId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public byte[] DownloadSnapshot(decimal snapshotId)
        {
            byte[] data = null;

            // 验证输入
            if (snapshotId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                data = dalCustomSnapshot.DownloadSnapshot(snapshotId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        ///  插入记录与报表文件
        /// </summary>
        /// <param name="customSnapshotInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public decimal Insert(CustomSnapshotInfo customSnapshotInfo, byte[] data)
        {
            decimal customCoverSnapshotId = 0;

            // 验证输入
            if (customSnapshotInfo == null)
            {
                throw new ArgumentException("插入对象不能为空。");
            }

            try
            {
                customCoverSnapshotId = dalCustomSnapshot.Insert(customSnapshotInfo, data);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCoverSnapshotId;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
