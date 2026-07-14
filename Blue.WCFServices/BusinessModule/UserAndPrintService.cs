//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAndPrintService.cs
// 描述: UserAndPrint 操作服务类
// 作者：ChenJie 
// 编写日期：2022/11/13
// Copyright 2022
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserAndPrint.
    /// </summary>
    public class UserAndPrintService : IUserAndPrintContract
    {
        #region 业务实例
        
        private static readonly IUserAndPrintHandler userAndPrintHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IUserAndPrintHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserAndPrintService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 UserAndPrint 表中插入一条新记录
		/// </summary>
		/// <param name="userAndPrintInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserAndPrintInfo userAndPrintInfo)
		{
            return userAndPrintHandler.Insert(userAndPrintInfo);
		}
        
        /// <summary>
		/// 获得 UserAndPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndPrintInfo 对象</returns>
		public UserAndPrintInfo GetModelInfo(decimal printId, decimal userId)
		{	
            return userAndPrintHandler.GetModelInfo(printId, userId);           
		}		
		
        /// <summary>
		/// 更新 UserAndPrintInfo 对象
		/// </summary>
		/// <param name="userAndPrintInfo">UserAndPrintInfo 对象</param>
		public void Update(UserAndPrintInfo userAndPrintInfo)
		{	          
            userAndPrintHandler.Update(userAndPrintInfo);
        }	
  
        /// <summary>
		/// 删除 UserAndPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndPrintInfo 对象</returns>
		public void Delete(decimal printId, decimal userId)
		{	
            userAndPrintHandler.Delete(printId, userId);
        }
        
        /// <summary>
        /// 获得 UserAndPrintInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAndPrintInfo 对象列表</returns>
        public IList<UserAndPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userAndPrintHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserAndPrint 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> UserAndPrintInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userAndPrintHandler.GetTotalCount(whereConditons);
        }
        
        #endregion
		
		#region 实现自定义接口
		
		#endregion
    }
}
