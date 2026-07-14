//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserLogService.cs
// 描述：UserLog 操作服务类
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserLog.
    /// </summary>
    public class UserLogService : IUserLogContract
    {
        #region 业务实例
        
        private static readonly IUserLogHandler userLogHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<IUserLogHandler>();
        
        #endregion
        
		#region 构造函数

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserLogService()
		{
              
		}

		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 userlog 表中插入一条新记录
		/// </summary>
		/// <param name="userLogInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserLogInfo userLogInfo)
		{
            return userLogHandler.Insert(userLogInfo);
		}
        
        /// <summary>
		/// 获得 UserLogInfo 对象
		/// </summary>
		///<param name="logId"></param>
		/// <returns> UserLogInfo 对象</returns>
		public UserLogInfo GetModelInfo(decimal logId)
		{	
            return userLogHandler.GetModelInfo(logId);           
		}		
		
        /// <summary>
		/// 更新 UserLogInfo 对象
		/// </summary>
		/// <param name="userLogInfo">UserLogInfo 对象</param>
		public void Update(UserLogInfo userLogInfo)
		{	          
            userLogHandler.Update(userLogInfo);
        }	
  
        /// <summary>
		/// 删除 UserLogInfo 对象
		/// </summary>
		///<param name="logId"></param>
		/// <returns> UserLogInfo 对象</returns>
		public void Delete(decimal logId)
		{	
            userLogHandler.Delete(logId);
        }
        
        /// <summary>
		/// 获得 UserLogInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserLogInfo 对象列表</returns>
		public IList<UserLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return userLogHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 UserLog 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>UserLogInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return userLogHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 按编号批量删除日志
        /// </summary>
        /// <param name="logIds"></param>
        public void Delete(IList<decimal> logIds)
        {
            userLogHandler.Delete(logIds);
        }

        /// <summary>
        /// 按条件删除日志
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int Delete(IList<WhereConditon> whereConditons)
        {
            return userLogHandler.Delete(whereConditons);
        }

        /// <summary>
        /// 获得以表 UserLog 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return userLogHandler.GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount);
        }

        #endregion
    }
}
