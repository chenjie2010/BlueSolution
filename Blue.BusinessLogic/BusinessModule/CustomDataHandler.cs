//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataHandler.cs
// 描述：CustomData 业务处理类
// 作者：ChenJie 
// 编写日期：2017/11/27
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomData.
    /// </summary>
    public class CustomDataHandler : CommonNodeBusiness, ICustomDataHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomData dalCustomData = BusinessDataAccessFactory.CreateCustomData(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomDataHandler() : base(dalCustomData)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customdata 表中插入一条新记录
		/// </summary>
		/// <param name="customDataInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomDataInfo customDataInfo)
		{
            //自动增加的关键字的值
			decimal customDataId = 0;
            
			// 验证输入
			if (customDataInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customDataId = dalCustomData.Insert(customDataInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customDataId;
		}
        
        /// <summary>
		/// 获得 CustomDataInfo 对象
		/// </summary>
		///<param name="dataId">数据填报编号</param>
		/// <returns> CustomDataInfo 对象</returns>
		public CustomDataInfo GetModelInfo(decimal dataId)
		{			
			CustomDataInfo  customDataInfo = null;
            
			// 验证输入
			if(dataId < 0)
            {
				return null;
            }

            try
            {
                customDataInfo =  dalCustomData.GetModelInfo(dataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customDataInfo;
		}        
        
        /// <summary>
		/// 更新 CustomDataInfo 对象
		/// </summary>
		/// <param name="customDataInfo">CustomDataInfo 对象</param>
		public void Update(CustomDataInfo customDataInfo)
		{	
            // 验证输入
            if (customDataInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomData.Update(customDataInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomDataInfo 对象
		/// </summary>
		///<param name="dataId">数据填报编号</param>
		/// <returns> CustomDataInfo 对象</returns>
		public void Delete(decimal dataId)
		{		
            // 验证输入
			if(dataId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomData.Delete(dataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomDataInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataInfo 对象列表</returns>
		public IList<CustomDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomDataInfo>  customDataInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customDataInfos = dalCustomData.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customDataInfos;
		}               
        
        /// <summary>
		/// 获得 CustomData 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDataInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomData.GetTotalCount(whereConditons);
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
        /// 获得填报属性
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public byte GetDataFilledProperty(decimal dataId)
        {
            byte dataFilledProperty = 0;

            // 验证输入
            if (dataId <= 0)
            {
                throw new ArgumentException("数据填报不能小于等于0.");
            }

            try
            {
                dataFilledProperty = dalCustomData.GetDataFilledProperty(dataId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFilledProperty;
        }

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataId)
        {
            Dictionary<decimal, string> reviewers = null;

            // 验证输入
            if (dataId <= 0)
            {
                throw new ArgumentException("数据填报不能小于等于0.");
            }

            try
            {
                reviewers = dalCustomData.GetFinalReviewers(dataId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewers;
        }

      /// <summary>
      /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
      /// </summary>
      /// <param name="dataId"></param>
      /// <param name="userId"></param>
      /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataId, decimal userId)
        {
            Dictionary<decimal, string> reviewers = null;

            // 验证输入
            if (dataId <= 0 || userId <= 0)
            {
                throw new ArgumentException("数据填报或者用户编号不能小于等于0.");
            }

            try
            {
                reviewers = dalCustomData.GetInitReviewers(dataId, userId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewers;
        }

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal dataId = 0;

            // 验证输入
            if (customDataInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dataId = dalCustomData.Insert(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataId;
        }

        /// <summary>
        /// 更新数据填报和附件信息
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        public void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            // 验证输入
            if (customDataInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }

            try
            {
                dalCustomData.Update(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);

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
