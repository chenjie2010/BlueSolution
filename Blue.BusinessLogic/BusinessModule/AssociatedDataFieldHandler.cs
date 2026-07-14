//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：AssociatedDataFieldHandler.cs
// 描述：AssociatedDataField 业务处理类
// 作者：ChenJie 
// 编写日期：2016/10/3
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.AssociatedDataField.
    /// </summary>
    public class AssociatedDataFieldHandler : CommonNodeBusiness, IAssociatedDataFieldHandler
    {
        #region 工厂类实例
        
        private static readonly IAssociatedDataField dalAssociatedDataField = BusinessDataAccessFactory.CreateAssociatedDataField(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AssociatedDataFieldHandler() : base(dalAssociatedDataField)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 associateddatafield 表中插入一条新记录
		/// </summary>
		/// <param name="associatedDataFieldInfo"></param>
		/// <returns></returns>
		public decimal Insert(AssociatedDataFieldInfo associatedDataFieldInfo)
		{
            //自动增加的关键字的值
			decimal associatedDataFieldId = 0;
            
			// 验证输入
			if (associatedDataFieldInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                associatedDataFieldId = dalAssociatedDataField.Insert(associatedDataFieldInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return associatedDataFieldId;
		}
        
        /// <summary>
		/// 获得 AssociatedDataFieldInfo 对象
		/// </summary>
		///<param name="associatedDataFieldId">关联字段编号</param>
		/// <returns> AssociatedDataFieldInfo 对象</returns>
		public AssociatedDataFieldInfo GetModelInfo(decimal associatedDataFieldId)
		{			
			AssociatedDataFieldInfo  associatedDataFieldInfo = null;
            
			// 验证输入
			if(associatedDataFieldId < 0)
            {
				return null;
            }

            try
            {
                associatedDataFieldInfo =  dalAssociatedDataField.GetModelInfo(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return associatedDataFieldInfo;
		}        
        
        /// <summary>
		/// 更新 AssociatedDataFieldInfo 对象
		/// </summary>
		/// <param name="associatedDataFieldInfo">AssociatedDataFieldInfo 对象</param>
		public void Update(AssociatedDataFieldInfo associatedDataFieldInfo)
		{	
            // 验证输入
            if (associatedDataFieldInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalAssociatedDataField.Update(associatedDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 AssociatedDataFieldInfo 对象
		/// </summary>
		///<param name="associatedDataFieldId">关联字段编号</param>
		/// <returns> AssociatedDataFieldInfo 对象</returns>
		public void Delete(decimal associatedDataFieldId)
		{		
            // 验证输入
			if(associatedDataFieldId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalAssociatedDataField.Delete(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 AssociatedDataFieldInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 对象列表</returns>
		public IList<AssociatedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<AssociatedDataFieldInfo>  associatedDataFieldInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                associatedDataFieldInfos = dalAssociatedDataField.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return associatedDataFieldInfos;
		}               
        
        /// <summary>
		/// 获得 AssociatedDataField 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>AssociatedDataFieldInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalAssociatedDataField.GetTotalCount(whereConditons);
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
        /// 获得字段的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal associatedDataFieldId)
        {
            string physicalName = string.Empty;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("关联字段编号不能小于等于0。");
            }

            try
            {
                physicalName = dalAssociatedDataField.GetPhysicalName(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal associatedDataFieldId)
        {
            string logicalName = string.Empty;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("关联字段编号不能小于等于0。");
            }

            try
            {
                logicalName = dalAssociatedDataField.GetLogicalName(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalName;
        }

        /// <summary>
        /// 通过关联字段的类型获得关联表中关联字段个数
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="DataFieldCategory"></param>
        /// <returns></returns>
        public int GetAssociatedDataFieldCount(decimal associationId, AssociatedDataFieldCategory dataFieldCategory)
        {
            int count = 0;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                count = dalAssociatedDataField.GetAssociatedDataFieldCount(associationId, dataFieldCategory);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;

        }

        /// <summary>
        /// 获得字段的属性信息
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public List<BasedDataFieldInfo> GetDataFieldProperties(decimal associationId)
        {
            List<BasedDataFieldInfo> basedDataFieldProperties = null;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                basedDataFieldProperties = dalAssociatedDataField.GetDataFieldProperties(associationId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return basedDataFieldProperties;
        }


        /// <summary>
        /// 获得字段的长度
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的长度</returns>
        public int GetDataLength(decimal associatedDataFieldId)
        {
            int dataLength = 0;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dataLength = dalAssociatedDataField.GetDataLength(associatedDataFieldId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataLength;
        }

        /// <summary>
        /// 关联表的字段名称关系
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDataFieldNameRelation(decimal dataFieldId)
        {
            Dictionary<string, string> dataFieldNameRelation = null;
            // 验证输入
            if (dataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                dataFieldNameRelation = dalAssociatedDataField.GetDataFieldNameRelation(dataFieldId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldNameRelation;
        }

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public BasedDataType GetBasedDataType(decimal associatedDataFieldId)
        {
            BasedDataType basedDataType = BasedDataType.String;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                basedDataType = dalAssociatedDataField.GetBasedDataType(associatedDataFieldId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return basedDataType;
        }

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociationId(decimal associatedDataFieldId)
        {
            decimal associationId = 0;

            // 验证输入
            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                associationId = dalAssociatedDataField.GetAssociationId(associatedDataFieldId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return associationId;
        }

        /// <summary>
        /// 获得字段列表
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public IList<AssociatedDataFieldInfo> GetModelInfos(decimal associationId)
        {
            //创建集合对象
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = null;

            // 验证输入
            if (associationId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                associatedDataFieldInfos = dalAssociatedDataField.GetModelInfos(associationId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return associatedDataFieldInfos;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
