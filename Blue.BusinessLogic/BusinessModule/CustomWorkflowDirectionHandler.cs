//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowDirectionHandler.cs
// 描述：CustomWorkflowDirection 业务处理类
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
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomWorkflowDirection.
    /// </summary>
    public class CustomWorkflowDirectionHandler : ICustomWorkflowDirectionHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomWorkflowDirection dalCustomWorkflowDirection = BusinessDataAccessFactory.CreateCustomWorkflowDirection(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomWorkflowDirectionHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customworkflowdirection 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowDirectionInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowDirectionInfo customWorkflowDirectionInfo)
		{
            //自动增加的关键字的值
			decimal customWorkflowDirectionId = 0;
            
			// 验证输入
			if (customWorkflowDirectionInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customWorkflowDirectionId = dalCustomWorkflowDirection.Insert(customWorkflowDirectionInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customWorkflowDirectionId;
		}
        
        /// <summary>
		/// 获得 CustomWorkflowDirectionInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="parentProcessId">流程编号</param>
		/// <returns> CustomWorkflowDirectionInfo 对象</returns>
		public CustomWorkflowDirectionInfo GetModeInfo(decimal processId, decimal parentProcessId)
		{			
			CustomWorkflowDirectionInfo  customWorkflowDirectionInfo = null;
            
			// 验证输入
			if(processId < 0)
            {
				return null;
            }

            try
            {
                customWorkflowDirectionInfo =  dalCustomWorkflowDirection.GetModeInfo(processId, parentProcessId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customWorkflowDirectionInfo;
		}        
        
        /// <summary>
		/// 更新 CustomWorkflowDirectionInfo 对象
		/// </summary>
		/// <param name="customWorkflowDirectionInfo">CustomWorkflowDirectionInfo 对象</param>
		public void Update(CustomWorkflowDirectionInfo customWorkflowDirectionInfo)
		{	
            // 验证输入
            if (customWorkflowDirectionInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomWorkflowDirection.Update(customWorkflowDirectionInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomWorkflowDirectionInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="parentProcessId">流程编号</param>
		/// <returns> CustomWorkflowDirectionInfo 对象</returns>
		public void Delete(decimal processId, decimal parentProcessId)
		{		
            // 验证输入
			if(processId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomWorkflowDirection.Delete(processId, parentProcessId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomWorkflowDirectionInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowDirectionInfo 对象列表</returns>
		public IList<CustomWorkflowDirectionInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomWorkflowDirectionInfo>  customWorkflowDirectionInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customWorkflowDirectionInfos = dalCustomWorkflowDirection.GetModeInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customWorkflowDirectionInfos;
		}               
        
        /// <summary>
		/// 获得 CustomWorkflowDirection 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowDirectionInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomWorkflowDirection.GetTotalCount(whereConditons);
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
