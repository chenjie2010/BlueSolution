//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserQueryService.cs
// 描述: UserQuery 操作服务类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.Unity;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserQuery.
    /// </summary>
    public class UserQueryService : CommonNodeServices, IUserQueryContract
    {
        #region 业务实例
        
        private static readonly IUserQueryHandler userQueryHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IUserQueryHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserQueryService() : base(userQueryHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 UserQuery 表中插入一条新记录
		/// </summary>
		/// <param name="userQueryInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserQueryInfo userQueryInfo)
		{
            return userQueryHandler.Insert(userQueryInfo);
		}
        
        /// <summary>
		/// 获得 UserQueryInfo 对象
		/// </summary>
		///<param name="userQueryId">查询编号</param>
		/// <returns> UserQueryInfo 对象</returns>
		public UserQueryInfo GetModelInfo(decimal userQueryId)
		{	
            return userQueryHandler.GetModelInfo(userQueryId);           
		}		
		
        /// <summary>
		/// 更新 UserQueryInfo 对象
		/// </summary>
		/// <param name="userQueryInfo">UserQueryInfo 对象</param>
		public void Update(UserQueryInfo userQueryInfo)
		{	          
            userQueryHandler.Update(userQueryInfo);
        }	
  
        /// <summary>
		/// 删除 UserQueryInfo 对象
		/// </summary>
		///<param name="userQueryId">查询编号</param>
		/// <returns> UserQueryInfo 对象</returns>
		public void Delete(decimal userQueryId)
		{	
            userQueryHandler.Delete(userQueryId);
        }
        
        /// <summary>
        /// 获得 UserQueryInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserQueryInfo 对象列表</returns>
        public IList<UserQueryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userQueryHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserQuery 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> UserQueryInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userQueryHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 向 UserQuery 表中插入一条新记录和查询字段集合
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos)
        {
            return userQueryHandler.Insert(userQueryInfo, queryAndDataFieldInfos);
        }

        /// <summary>
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        public IList<QueryAndDataFieldInfo> GetQueryAndDataFieldInfos(decimal userQueryId)
        {
            return userQueryHandler.GetQueryAndDataFieldInfos(userQueryId);
        }

        #endregion
    }
}
