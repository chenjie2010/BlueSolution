//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterfaceHandler.cs
// 描述: CustomInterface 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomInterface.
    /// </summary>
    public class CustomInterfaceHandler : CommonNodeBusiness, ICustomInterfaceHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomInterface dalCustomInterface = SystemDataAccessFactory.CreateCustomInterface();
        private static readonly ICustomInterfaceAndDep dalCustomInterfaceAndDep = SystemDataAccessFactory.CreateCustomInterfaceAndDep();
        private static readonly ICustomInterfaceAndUserType dalCustomInterfaceAndUserType = SystemDataAccessFactory.CreateCustomInterfaceAndUserType();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomInterfaceHandler() : base(dalCustomInterface)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 custominterface 表中插入一条新记录
		/// </summary>
		/// <param name="customInterfaceInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomInterfaceInfo customInterfaceInfo)
		{
            //自动增加的关键字的值
			decimal customInterfaceId = 0;
            
			// 验证输入
			if (customInterfaceInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customInterfaceId = dalCustomInterface.Insert(customInterfaceInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customInterfaceId;
		}
        
        /// <summary>
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceId">接口编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public CustomInterfaceInfo GetModelInfo(decimal interfaceId)
		{			
			CustomInterfaceInfo  customInterfaceInfo = null;
            
			// 验证输入
			if(interfaceId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customInterfaceInfo =  dalCustomInterface.GetModelInfo(interfaceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customInterfaceInfo;
		}        
        
        /// <summary>
		/// 更新 CustomInterfaceInfo 对象
		/// </summary>
		/// <param name="customInterfaceInfo">CustomInterfaceInfo 对象</param>
		public void Update(CustomInterfaceInfo customInterfaceInfo)
		{	
            // 验证输入
            if (customInterfaceInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomInterface.Update(customInterfaceInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceId">接口编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public void Delete(decimal interfaceId)
		{		
            // 验证输入
			if(interfaceId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomInterface.Delete(interfaceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomInterfaceInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomInterfaceInfo  对象列表</returns>
		public IList<CustomInterfaceInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomInterfaceInfo>  customInterfaceInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customInterfaceInfos = dalCustomInterface.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customInterfaceInfos;
		}               
        
        /// <summary>
		/// 获得 CustomSheet 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSheetInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomInterface.GetTotalCount(whereConditons);
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
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceIdentifier">标识符编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public CustomInterfaceInfo GetModelInfo(string interfaceIdentifier)
        {
            CustomInterfaceInfo customInterfaceInfo = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(interfaceIdentifier))
            {
                throw new ArgumentException("标识符不能为空。");
            }

            try
            {
                customInterfaceInfo = dalCustomInterface.GetModelInfo(interfaceIdentifier);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customInterfaceInfo;
        }

        /// <summary>
        /// 标识符是否已经存在
        /// </summary>
        /// <param name="interfaceIdentifier"></param>
        /// <returns></returns>
        public bool IsExistedIdentifier(string interfaceIdentifier)
        {
            bool result = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(interfaceIdentifier))
            {
                throw new ArgumentException("标识符不能为空。");
            }

            try
            {
                result = dalCustomInterface.IsExistedIdentifier(interfaceIdentifier);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 更新条件
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        public void UpdateConditions(decimal interfaceId, IList<decimal> userTypeIds, IList<decimal> departmentIds)
        {
            // 验证输入
            if (interfaceId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomInterface.UpdateConditions(interfaceId, userTypeIds, departmentIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetUserTypes(decimal interfaceId)
        {
            IList<decimal> userTypeIds = dalCustomInterfaceAndUserType.GetSecondIds(interfaceId);
            IUserTypeHandler userTypeHandler = new UserTypeHandler();
            IList<CommonNode> userTypes = userTypeHandler.GetCommonNodes(userTypeIds);

            return userTypes;
        }

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDepartments(decimal interfaceId)
        {
            IList<decimal> departmentIds = dalCustomInterfaceAndDep.GetSecondIds(interfaceId);
            ICustomDepartmentHandler customDepartmentHandler = new CustomDepartmentHandler();
            IList<CommonNode> departments = customDepartmentHandler.GetCommonNodes(departmentIds);

            return departments;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
