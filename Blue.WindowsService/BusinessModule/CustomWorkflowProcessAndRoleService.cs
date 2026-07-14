//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessAndRoleService.cs
// 描述：CustomWorkflowProcessAndRole 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomWorkflowProcessAndRole.
    /// </summary>
    public class CustomWorkflowProcessAndRoleService : ICustomWorkflowProcessAndRoleContract
    {
        #region 业务实例
        
        private static readonly ICustomWorkflowProcessAndRoleHandler customWorkflowProcessAndRoleHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomWorkflowProcessAndRoleHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomWorkflowProcessAndRoleService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customworkflowprocessandrole 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowProcessAndRoleInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowProcessAndRoleInfo customWorkflowProcessAndRoleInfo)
		{
            return customWorkflowProcessAndRoleHandler.Insert(customWorkflowProcessAndRoleInfo);
		}
        
        /// <summary>
		/// 获得 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="roleId">角色编号</param>
		/// <returns> CustomWorkflowProcessAndRoleInfo 对象</returns>
		public CustomWorkflowProcessAndRoleInfo GetModeInfo(decimal processId, decimal roleId)
		{	
            return customWorkflowProcessAndRoleHandler.GetModeInfo(processId, roleId);           
		}		
		
        /// <summary>
		/// 更新 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		/// <param name="customWorkflowProcessAndRoleInfo">CustomWorkflowProcessAndRoleInfo 对象</param>
		public void Update(CustomWorkflowProcessAndRoleInfo customWorkflowProcessAndRoleInfo)
		{	          
            customWorkflowProcessAndRoleHandler.Update(customWorkflowProcessAndRoleInfo);
        }	
  
        /// <summary>
		/// 删除 CustomWorkflowProcessAndRoleInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="roleId">角色编号</param>
		/// <returns> CustomWorkflowProcessAndRoleInfo 对象</returns>
		public void Delete(decimal processId, decimal roleId)
		{	
            customWorkflowProcessAndRoleHandler.Delete(processId, roleId);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowProcessAndRoleInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessAndRoleInfo 对象列表</returns>
		public IList<CustomWorkflowProcessAndRoleInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customWorkflowProcessAndRoleHandler.GetModeInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowProcessAndRole 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowProcessAndRoleInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customWorkflowProcessAndRoleHandler.GetTotalCount(whereConditons);
        }
        
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
