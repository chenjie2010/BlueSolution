//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataHandler.cs
// 描述: RemoteData 业务处理类
// 作者：ChenJie 
// 编写日期：2018/10/27
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
using Blue.IDAL.DataConvertionModule;
using Blue.Model.DataConvertionModule;
using Blue.BusinessInterface.DataConvertionModule;

namespace Blue.BusinessLogic.DataConvertionModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.RemoteData.
    /// </summary>
    public class RemoteDataHandler : CommonNodeBusiness, IRemoteDataHandler
    {
        #region 工厂类实例
        
        private static readonly IRemoteData dalRemoteData = DataConvertionFactory.CreateRemoteData();
        private static readonly IRemoteDataAndField remoteDataAndField = DataConvertionFactory.CreateRemoteDataAndField();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RemoteDataHandler() : base(dalRemoteData)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 remotedata 表中插入一条新记录
		/// </summary>
		/// <param name="remoteDataInfo"></param>
		/// <returns></returns>
		public decimal Insert(RemoteDataInfo remoteDataInfo)
		{
            //自动增加的关键字的值
			decimal remoteDataId = 0;
            
			// 验证输入
			if (remoteDataInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                remoteDataId = dalRemoteData.Insert(remoteDataInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return remoteDataId;
		}
        
        /// <summary>
		/// 获得 RemoteDataInfo 对象
		/// </summary>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataInfo 对象</returns>
		public RemoteDataInfo GetModelInfo(decimal remoteDataId)
		{			
			RemoteDataInfo  remoteDataInfo = null;
            
			// 验证输入
			if(remoteDataId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                remoteDataInfo =  dalRemoteData.GetModelInfo(remoteDataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return remoteDataInfo;
		}        
        
        /// <summary>
		/// 更新 RemoteDataInfo 对象
		/// </summary>
		/// <param name="remoteDataInfo">RemoteDataInfo 对象</param>
		public void Update(RemoteDataInfo remoteDataInfo)
		{	
            // 验证输入
            if (remoteDataInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalRemoteData.Update(remoteDataInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 RemoteDataInfo 对象
		/// </summary>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataInfo 对象</returns>
		public void Delete(decimal remoteDataId)
		{		
            // 验证输入
			if(remoteDataId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalRemoteData.Delete(remoteDataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 RemoteDataInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RemoteDataInfo  对象列表</returns>
		public IList<RemoteDataInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<RemoteDataInfo>  remoteDataInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                remoteDataInfos = dalRemoteData.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return remoteDataInfos;
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
                count = dalRemoteData.GetTotalCount(whereConditons);
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
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelation(decimal remoteDataId, decimal destinationTableId)
        {
            Dictionary<decimal, decimal> dataFieldRelation = null;

            try
            {
                dataFieldRelation = remoteDataAndField.GetDataFieldRelation(remoteDataId, destinationTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldRelation;
        }

        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public IList<RemoteDataAndFieldInfo> GetModelInfos(decimal remoteDataId)
        {
            //创建集合对象
            IList<RemoteDataAndFieldInfo> remoteDataAndFieldInfos = null;

            try
            {
                remoteDataAndFieldInfos = remoteDataAndField.GetModelInfos(remoteDataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return remoteDataAndFieldInfos;
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal remoteDataId, List<KeyValueItem> keyValueItems)
        {
            try
            {
                 remoteDataAndField.UpdateDataFieldRelation(remoteDataId, keyValueItems);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获本地的表与字段对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, decimal>> GetTableRelation(decimal remoteDataId)
        {
            Dictionary<decimal, Dictionary<decimal, decimal>> tableRelation = null;

            try
            {
                tableRelation = remoteDataAndField.GetTableRelation(remoteDataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableRelation;
        }

        #endregion

            #region 私有方法

            #endregion
        }
}
