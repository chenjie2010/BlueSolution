//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataRelationHandler.cs
// 描述: DataRelation 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
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
    /// 业务层处理类，对于的表： dbo.DataRelation.
    /// </summary>
    public class DataRelationHandler : CommonNodeBusiness, IDataRelationHandler
    {
        #region 工厂类实例
        
        private static readonly IDataRelation dalDataRelation = DataConvertionFactory.CreateDataRelation();
        private static readonly IDataFieldRelation dalDataFieldRelation = DataConvertionFactory.CreateDataFieldRelation();
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataRelationHandler() : base(dalDataRelation)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 datarelation 表中插入一条新记录
		/// </summary>
		/// <param name="dataRelationInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataRelationInfo dataRelationInfo)
		{
            //自动增加的关键字的值
			decimal dataRelationId = 0;
            
			// 验证输入
			if (dataRelationInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                dataRelationId = dalDataRelation.Insert(dataRelationInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataRelationId;
		}
        
        /// <summary>
		/// 获得 DataRelationInfo 对象
		/// </summary>
		///<param name="relationId">关系编号</param>
		/// <returns> DataRelationInfo 对象</returns>
		public DataRelationInfo GetModelInfo(decimal relationId)
		{			
			DataRelationInfo  dataRelationInfo = null;
            
			// 验证输入
			if(relationId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataRelationInfo =  dalDataRelation.GetModelInfo(relationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return dataRelationInfo;
		}        
        
        /// <summary>
		/// 更新 DataRelationInfo 对象
		/// </summary>
		/// <param name="dataRelationInfo">DataRelationInfo 对象</param>
		public void Update(DataRelationInfo dataRelationInfo)
		{	
            // 验证输入
            if (dataRelationInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalDataRelation.Update(dataRelationInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 DataRelationInfo 对象
		/// </summary>
		///<param name="relationId">关系编号</param>
		/// <returns> DataRelationInfo 对象</returns>
		public void Delete(decimal relationId)
		{		
            // 验证输入
			if(relationId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalDataRelation.Delete(relationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 DataRelationInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataRelationInfo  对象列表</returns>
		public IList<DataRelationInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<DataRelationInfo>  dataRelationInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                dataRelationInfos = dalDataRelation.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return dataRelationInfos;
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
                count = dalDataRelation.GetTotalCount(whereConditons);
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
        /// 数据转表
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="whereConditons"></param>
        public void Import(decimal relationId, IList<WhereConditon> whereConditons)
        {
            // 验证输入
            if (relationId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                 dalDataFieldRelation.Import(relationId, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
        }

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public IList<DataFieldRelationInfo> GetModelInfosByRelationId(decimal relationId)
        {
            IList<DataFieldRelationInfo> dataFieldRelationInfos = null;

            // 验证输入
            if (relationId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataFieldRelationInfos = dalDataFieldRelation.GetModelInfos(relationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldRelationInfos;
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal relationId, List<KeyValueItem> keyValueItems)
        {
            // 验证输入
            if (relationId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalDataFieldRelation.UpdateDataFieldRelation(relationId, keyValueItems);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得表的对应关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetTableRelation(decimal relationId)
        {
            Dictionary<decimal, decimal> tableRelation = null;

            // 验证输入
            if (relationId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                tableRelation = dalDataFieldRelation.GetTableRelation(relationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableRelation;
        }

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelation(decimal relationId, decimal sourceTableId, decimal destinationTableId)
        {
            Dictionary<decimal, decimal> dataFieldRelation = null;

            // 验证输入
            if (relationId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataFieldRelation = dalDataFieldRelation.GetDataFieldRelation(relationId, sourceTableId, destinationTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldRelation;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
