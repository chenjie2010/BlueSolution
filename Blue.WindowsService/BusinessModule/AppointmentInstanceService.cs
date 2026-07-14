//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentInstanceService.cs
// 描述: AppointmentInstance 操作服务类
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.AppointmentInstance.
    /// </summary>
    public class AppointmentInstanceService : IAppointmentInstanceContract
    {
        #region 业务实例
        
        private static readonly IAppointmentInstanceHandler appointmentInstanceHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IAppointmentInstanceHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AppointmentInstanceService()
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 AppointmentInstance 表中插入一条新记录
		/// </summary>
		/// <param name="appointmentInstanceInfo"></param>
		/// <returns></returns>
		public decimal Insert(AppointmentInstanceInfo appointmentInstanceInfo)
		{
            return appointmentInstanceHandler.Insert(appointmentInstanceInfo);
		}
        
        /// <summary>
		/// 获得 AppointmentInstanceInfo 对象
		/// </summary>
		///<param name="appInstanceId">实例编号</param>
		/// <returns> AppointmentInstanceInfo 对象</returns>
		public AppointmentInstanceInfo GetModelInfo(decimal appInstanceId)
		{	
            return appointmentInstanceHandler.GetModelInfo(appInstanceId);           
		}		
		
        /// <summary>
		/// 更新 AppointmentInstanceInfo 对象
		/// </summary>
		/// <param name="appointmentInstanceInfo">AppointmentInstanceInfo 对象</param>
		public void Update(AppointmentInstanceInfo appointmentInstanceInfo)
		{	          
            appointmentInstanceHandler.Update(appointmentInstanceInfo);
        }	
  
        /// <summary>
		/// 删除 AppointmentInstanceInfo 对象
		/// </summary>
		///<param name="appInstanceId">实例编号</param>
		/// <returns> AppointmentInstanceInfo 对象</returns>
		public void Delete(decimal appInstanceId)
		{	
            appointmentInstanceHandler.Delete(appInstanceId);
        }
        
        /// <summary>
        /// 获得 AppointmentInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentInstanceInfo 对象列表</returns>
        public IList<AppointmentInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return appointmentInstanceHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 AppointmentInstance 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> AppointmentInstanceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return appointmentInstanceHandler.GetTotalCount(whereConditons);
        }
        
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
