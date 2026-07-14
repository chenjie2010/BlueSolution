//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowDirectionService.cs
// 描述：CustomWorkflowDirection 操作服务类
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
    /// 操作服务类，对于的表： dbo.CustomWorkflowDirection.
    /// </summary>
    public class CustomWorkflowDirectionService : ICustomWorkflowDirectionContract
    {
        #region 业务实例
        
        private static readonly ICustomWorkflowDirectionHandler customWorkflowDirectionHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomWorkflowDirectionHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomWorkflowDirectionService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customworkflowdirection 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowDirectionInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowDirectionInfo customWorkflowDirectionInfo)
		{
            return customWorkflowDirectionHandler.Insert(customWorkflowDirectionInfo);
		}
        
        /// <summary>
		/// 获得 CustomWorkflowDirectionInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="parentProcessId">流程编号</param>
		/// <returns> CustomWorkflowDirectionInfo 对象</returns>
		public CustomWorkflowDirectionInfo GetModeInfo(decimal processId, decimal parentProcessId)
		{	
            return customWorkflowDirectionHandler.GetModeInfo(processId, parentProcessId);           
		}		
		
        /// <summary>
		/// 更新 CustomWorkflowDirectionInfo 对象
		/// </summary>
		/// <param name="customWorkflowDirectionInfo">CustomWorkflowDirectionInfo 对象</param>
		public void Update(CustomWorkflowDirectionInfo customWorkflowDirectionInfo)
		{	          
            customWorkflowDirectionHandler.Update(customWorkflowDirectionInfo);
        }	
  
        /// <summary>
		/// 删除 CustomWorkflowDirectionInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="parentProcessId">流程编号</param>
		/// <returns> CustomWorkflowDirectionInfo 对象</returns>
		public void Delete(decimal processId, decimal parentProcessId)
		{	
            customWorkflowDirectionHandler.Delete(processId, parentProcessId);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowDirectionInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowDirectionInfo 对象列表</returns>
		public IList<CustomWorkflowDirectionInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customWorkflowDirectionHandler.GetModeInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowDirection 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowDirectionInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customWorkflowDirectionHandler.GetTotalCount(whereConditons);
        }
        
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
