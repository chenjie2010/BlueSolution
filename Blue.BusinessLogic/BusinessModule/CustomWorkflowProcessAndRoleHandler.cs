//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessAndRoleHandler.cs
// 描述：CustomWorkflowProcessAndRole 业务处理类
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
    /// 业务层处理类，对于的表： dbo.CustomWorkflowProcessAndRole.
    /// </summary>
    public class CustomWorkflowProcessAndRoleHandler : ICustomWorkflowProcessAndRoleHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomWorkflowProcessAndRole dalCustomWorkflowProcessAndRole = BusinessDataAccessFactory.CreateCustomWorkflowProcessAndRole(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomWorkflowProcessAndRoleHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customworkflowprocessandrole 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowProcessAndRoleInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowProcessAndRoleInfo customWorkflowProcessAndRoleInfo)
		{
            //自动增加的关键字的值
			decimal customWorkflowProcessAndRoleId = 0;
            
			// 验证输入
			if (customWorkflowProcessAndRoleInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customWorkflowProcessAndRoleId = dalCustomWorkflowProcessAndRole.Insert(customWorkflowProcessAndRoleInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customWorkflowProcessAndRoleId;
		}
        
        /// <summary>
		/// 获得 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="roleId">角色编号</param>
		/// <returns> CustomWorkflowProcessAndRoleInfo 对象</returns>
		public CustomWorkflowProcessAndRoleInfo GetModeInfo(decimal processId, decimal roleId)
		{			
			CustomWorkflowProcessAndRoleInfo  customWorkflowProcessAndRoleInfo = null;
            
			// 验证输入
			if(processId < 0)
            {
				return null;
            }

            try
            {
                customWorkflowProcessAndRoleInfo =  dalCustomWorkflowProcessAndRole.GetModeInfo(processId, roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customWorkflowProcessAndRoleInfo;
		}        
        
        /// <summary>
		/// 更新 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		/// <param name="customWorkflowProcessAndRoleInfo">CustomWorkflowProcessAndRoleInfo 对象</param>
		public void Update(CustomWorkflowProcessAndRoleInfo customWorkflowProcessAndRoleInfo)
		{	
            // 验证输入
            if (customWorkflowProcessAndRoleInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomWorkflowProcessAndRole.Update(customWorkflowProcessAndRoleInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="roleId">角色编号</param>
		/// <returns> CustomWorkflowProcessAndRoleInfo 对象</returns>
		public void Delete(decimal processId, decimal roleId)
		{		
            // 验证输入
			if(processId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomWorkflowProcessAndRole.Delete(processId, roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomWorkflowProcessAndRoleInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessAndRoleInfo 对象列表</returns>
		public IList<CustomWorkflowProcessAndRoleInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomWorkflowProcessAndRoleInfo>  customWorkflowProcessAndRoleInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customWorkflowProcessAndRoleInfos = dalCustomWorkflowProcessAndRole.GetModeInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customWorkflowProcessAndRoleInfos;
		}               
        
        /// <summary>
		/// 获得 CustomWorkflowProcessAndRole 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowProcessAndRoleInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomWorkflowProcessAndRole.GetTotalCount(whereConditons);
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
