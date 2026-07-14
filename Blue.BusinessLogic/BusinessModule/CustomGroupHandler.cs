//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomGroupHandler.cs
// 描述：CustomGroup 业务处理类
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
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomGroup.
    /// </summary>
    public class CustomGroupHandler : CommonNodeBusiness, ICustomGroupHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomGroup dalCustomGroup = BusinessDataAccessFactory.CreateCustomGroup(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomGroupHandler() : base(dalCustomGroup)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customgroup 表中插入一条新记录
		/// </summary>
		/// <param name="customGroupInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomGroupInfo customGroupInfo)
		{
            //自动增加的关键字的值
			decimal customGroupId = 0;
            
			// 验证输入
			if (customGroupInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customGroupId = dalCustomGroup.Insert(customGroupInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customGroupId;
		}
        
        /// <summary>
		/// 获得 CustomGroupInfo 对象
		/// </summary>
		///<param name="groupId">分组编号</param>
		/// <returns> CustomGroupInfo 对象</returns>
		public CustomGroupInfo GetModelInfo(decimal groupId)
		{			
			CustomGroupInfo  customGroupInfo = null;
            
			// 验证输入
			if(groupId < 0)
            {
				return null;
            }

            try
            {
                customGroupInfo =  dalCustomGroup.GetModelInfo(groupId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customGroupInfo;
		}        
        
        /// <summary>
		/// 更新 CustomGroupInfo 对象
		/// </summary>
		/// <param name="customGroupInfo">CustomGroupInfo 对象</param>
		public void Update(CustomGroupInfo customGroupInfo)
		{	
            // 验证输入
            if (customGroupInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomGroup.Update(customGroupInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomGroupInfo 对象
		/// </summary>
		///<param name="groupId">分组编号</param>
		/// <returns> CustomGroupInfo 对象</returns>
		public void Delete(decimal groupId)
		{		
            // 验证输入
			if(groupId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomGroup.Delete(groupId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomGroupInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomGroupInfo 对象列表</returns>
		public IList<CustomGroupInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomGroupInfo>  customGroupInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customGroupInfos = dalCustomGroup.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customGroupInfos;
		}               
        
        /// <summary>
		/// 获得 CustomGroup 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomGroupInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomGroup.GetTotalCount(whereConditons);
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
        /// 获得数据集(获得节点自身数据)
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal groupId)
        {
            DataSet ds = null;

            // 验证输入
            if (groupId <= 0)
            {
                throw new ArgumentException("分组编号不能小于等于0。");
            }

            try
            {
                ds = dalCustomGroup.GetPageRecord(groupId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentGroupId"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentGroupId, GroupType groupType)
        {
            DataSet ds = null;

            // 验证输入
            if (parentGroupId <= 0)
            {
                throw new ArgumentException("分组编号不能小于等于0。");
            }

            try
            {
                ds = dalCustomGroup.GetPageRecord(parentGroupId, groupType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, byte groupType, ref int totalCount)
        {
            DataSet ds = null;
            
            try
            {
                ds = dalCustomGroup.GetPageRecord(startPosition, count, groupType, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
