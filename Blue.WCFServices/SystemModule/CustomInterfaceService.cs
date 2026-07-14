//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterfaceService.cs
// 描述: CustomInterface 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/28
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
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomInterface.
    /// </summary>
    public class CustomInterfaceService : CommonNodeServices, ICustomInterfaceContract
    {
        #region 业务实例
        
        private static readonly ICustomInterfaceHandler customInterfaceHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ICustomInterfaceHandler>();

        #endregion
        
		#region 构造函数

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomInterfaceService() : base(customInterfaceHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 CustomInterface 表中插入一条新记录
		/// </summary>
		/// <param name="customInterfaceInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomInterfaceInfo customInterfaceInfo)
		{
            return customInterfaceHandler.Insert(customInterfaceInfo);
		}
        
        /// <summary>
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceId">接口编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public CustomInterfaceInfo GetModelInfo(decimal interfaceId)
		{	
            return customInterfaceHandler.GetModelInfo(interfaceId);           
		}		
		
        /// <summary>
		/// 更新 CustomInterfaceInfo 对象
		/// </summary>
		/// <param name="customInterfaceInfo">CustomInterfaceInfo 对象</param>
		public void Update(CustomInterfaceInfo customInterfaceInfo)
		{	          
            customInterfaceHandler.Update(customInterfaceInfo);
        }	
  
        /// <summary>
		/// 删除 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceId">接口编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public void Delete(decimal interfaceId)
		{	
            customInterfaceHandler.Delete(interfaceId);
        }
        
        /// <summary>
        /// 获得 CustomInterfaceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomInterfaceInfo 对象列表</returns>
        public IList<CustomInterfaceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customInterfaceHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomInterface 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomInterfaceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customInterfaceHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 标识符是否已经存在
        /// </summary>
        /// <param name="interfaceIdentifier"></param>
        /// <returns></returns>
        public bool IsExistedIdentifier(string interfaceIdentifier)
        {
            return customInterfaceHandler.IsExistedIdentifier(interfaceIdentifier);
        }

        /// <summary>
        /// 更新条件
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        public void UpdateConditions(decimal interfaceId, IList<decimal> userTypeIds, IList<decimal> departmentIds)
        {
            customInterfaceHandler.UpdateConditions(interfaceId, userTypeIds, departmentIds);
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetUserTypes(decimal interfaceId)
        {
            return customInterfaceHandler.GetUserTypes(interfaceId);
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDepartments(decimal interfaceId)
        {
            return customInterfaceHandler.GetDepartments(interfaceId);
        }

        #endregion
    }
}
