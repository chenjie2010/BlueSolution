//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserTypeHandler.cs
// 描述：UserType 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserType.
    /// </summary>
    public class UserTypeHandler : CommonNodeBusiness, IUserTypeHandler
    {
        #region 工厂类实例
        
        private static readonly IUserType dalUserType = SystemDataAccessFactory.CreateUserType();
        private static readonly IUserTypeScope dalUserTypeScope = SystemDataAccessFactory.CreateUserTypeScope();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserTypeHandler() : base(dalUserType)
        {
        }
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 usertype 表中插入一条新记录
		/// </summary>
		/// <param name="userTypeInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserTypeInfo userTypeInfo)
		{
            //自动增加的关键字的值
			decimal userTypeId = 0;
            
			// 验证输入
			if (userTypeInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                userTypeId = dalUserType.Insert(userTypeInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return userTypeId;
		}
        
        /// <summary>
		/// 获得 UserTypeInfo 对象
		/// </summary>
		///<param name="userTypeId">用户类型编号</param>
		/// <returns> UserTypeInfo 对象</returns>
		public UserTypeInfo GetModelInfo(decimal userTypeId)
		{			
			UserTypeInfo  userTypeInfo = null;
            
			// 验证输入
			if(userTypeId < 0)
            {
				return null;
            }

            try
            {
                userTypeInfo =  dalUserType.GetModelInfo(userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return userTypeInfo;
		}        
        
        /// <summary>
		/// 更新 UserTypeInfo 对象
		/// </summary>
		/// <param name="userTypeInfo">UserTypeInfo 对象</param>
		public void Update(UserTypeInfo userTypeInfo)
		{	
            // 验证输入
            if (userTypeInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalUserType.Update(userTypeInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 UserTypeInfo 对象
		/// </summary>
		///<param name="userTypeId">用户类型编号</param>
		/// <returns> UserTypeInfo 对象</returns>
		public void Delete(decimal userTypeId)
		{		
            // 验证输入
			if(userTypeId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalUserType.Delete(userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 UserTypeInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserTypeInfo 对象列表</returns>
		public IList<UserTypeInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<UserTypeInfo>  userTypeInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                userTypeInfos = dalUserType.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return userTypeInfos;
		}               
        
        /// <summary>
		/// 获得 UserType 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>UserTypeInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalUserType.GetTotalCount(whereConditons);
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

        /// <summary>
        /// 根据系统条件获得用户类型
        /// </summary>
        /// <param name="isSystemUserType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemUserType)
        {
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalUserType.GetCommonNodes(isSystemUserType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得用户类型数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetUserTypeCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            try
            {
                count = dalUserType.GetUserTypeCount(fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得用户类型分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetUserTypeData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataTable dt = null;

            try
            {
                dt = dalUserType.GetUserTypeData(pos, pageSize, fromUpdatedTime, toUpdatedTime);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);

            }

            return dt;
        }


        /// <summary>
        /// 获得接口可见标记位
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public bool GetIsVisibleForInterface(decimal userTypeId)
        {
            bool isVisibleForInterface = false;

            if (userTypeId <= 0)
            {
                throw new ArgumentException("用户类型编号不能小于或是等于0。");
            }

            try
            {
                isVisibleForInterface = dalUserType.GetIsVisibleForInterface(userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isVisibleForInterface;
        }

        /// <summary>
        /// 获得系统标记位
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public bool GetIsSystemUserType(decimal userTypeId)
        {
            bool isSystemUserType = false;

            if (userTypeId <= 0)
            {
                throw new ArgumentException("用户类型编号不能小于或是等于0。");
            }

            try
            {
                isSystemUserType = dalUserType.GetIsSystemUserType(userTypeId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isSystemUserType;
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndUserTypeIds()
        {
            Dictionary<string, decimal> nameAndUserTypeIds = null;

            try
            {
                nameAndUserTypeIds = dalUserType.GetNameAndUserTypeIds();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nameAndUserTypeIds;
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetUserTypeIdAndNames()
        {
            Dictionary<decimal, string> userTypeIdAndNames = null;

            try
            {
                userTypeIdAndNames = dalUserType.GetUserTypeIdAndNames();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userTypeIdAndNames;
        }
        
        /// <summary>
        /// 通过用户编号获得管理用户类型节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal userId)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = null;

            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalUserTypeScope.GetCommonNodes(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;

        }

        #endregion

        #region 私有方法

        #endregion
    }
}
