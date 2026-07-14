//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentInstanceHandler.cs
// 描述: AppointmentInstance 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/24
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.AppointmentInstance.
    /// </summary>
    public class AppointmentInstanceHandler : IAppointmentInstanceHandler
    {
        #region 工厂类实例
        
        private static readonly IAppointmentInstance dalAppointmentInstance = BusinessDataAccessFactory.CreateAppointmentInstance(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AppointmentInstanceHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 appointmentinstance 表中插入一条新记录
		/// </summary>
		/// <param name="appointmentInstanceInfo"></param>
		/// <returns></returns>
		public decimal Insert(AppointmentInstanceInfo appointmentInstanceInfo)
		{
            //自动增加的关键字的值
			decimal appointmentInstanceId = 0;
            
			// 验证输入
			if (appointmentInstanceInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                appointmentInstanceId = dalAppointmentInstance.Insert(appointmentInstanceInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return appointmentInstanceId;
		}
        
        /// <summary>
		/// 获得 AppointmentInstanceInfo 对象
		/// </summary>
		///<param name="appInstanceId">实例编号</param>
		/// <returns> AppointmentInstanceInfo 对象</returns>
		public AppointmentInstanceInfo GetModelInfo(decimal appInstanceId)
		{			
			AppointmentInstanceInfo  appointmentInstanceInfo = null;
            
			// 验证输入
			if(appInstanceId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                appointmentInstanceInfo =  dalAppointmentInstance.GetModelInfo(appInstanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return appointmentInstanceInfo;
		}        
        
        /// <summary>
		/// 更新 AppointmentInstanceInfo 对象
		/// </summary>
		/// <param name="appointmentInstanceInfo">AppointmentInstanceInfo 对象</param>
		public void Update(AppointmentInstanceInfo appointmentInstanceInfo)
		{	
            // 验证输入
            if (appointmentInstanceInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalAppointmentInstance.Update(appointmentInstanceInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 AppointmentInstanceInfo 对象
		/// </summary>
		///<param name="appInstanceId">实例编号</param>
		/// <returns> AppointmentInstanceInfo 对象</returns>
		public void Delete(decimal appInstanceId)
		{		
            // 验证输入
			if(appInstanceId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalAppointmentInstance.Delete(appInstanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 AppointmentInstanceInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentInstanceInfo  对象列表</returns>
		public IList<AppointmentInstanceInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<AppointmentInstanceInfo>  appointmentInstanceInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                appointmentInstanceInfos = dalAppointmentInstance.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return appointmentInstanceInfos;
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
                count = dalAppointmentInstance.GetTotalCount(whereConditons);
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
        
        #endregion
		
		#region 私有方法
		
		#endregion
    }
}
