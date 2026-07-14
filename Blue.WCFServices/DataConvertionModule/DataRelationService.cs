//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataRelationService.cs
// 描述: DataRelation 操作服务类
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
using Blue.Model.DataConvertionModule;
using Blue.BusinessInterface.DataConvertionModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.DataConvertionModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.DataRelation.
    /// </summary>
    public class DataRelationService : CommonNodeServices, IDataRelationContract
    {
        #region 业务实例
        
        private static readonly IDataRelationHandler dataRelationHandler = BusinessLogicContainer.Instance.DataConvertionModuleContainer.Resolve<IDataRelationHandler>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataRelationService() : base(dataRelationHandler)
        {

        }

		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 DataRelation 表中插入一条新记录
		/// </summary>
		/// <param name="dataRelationInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataRelationInfo dataRelationInfo)
		{
            return dataRelationHandler.Insert(dataRelationInfo);
		}
        
        /// <summary>
		/// 获得 DataRelationInfo 对象
		/// </summary>
		///<param name="relationId">关系编号</param>
		/// <returns> DataRelationInfo 对象</returns>
		public DataRelationInfo GetModelInfo(decimal relationId)
		{	
            return dataRelationHandler.GetModelInfo(relationId);           
		}		
		
        /// <summary>
		/// 更新 DataRelationInfo 对象
		/// </summary>
		/// <param name="dataRelationInfo">DataRelationInfo 对象</param>
		public void Update(DataRelationInfo dataRelationInfo)
		{	          
            dataRelationHandler.Update(dataRelationInfo);
        }	
  
        /// <summary>
		/// 删除 DataRelationInfo 对象
		/// </summary>
		///<param name="relationId">关系编号</param>
		/// <returns> DataRelationInfo 对象</returns>
		public void Delete(decimal relationId)
		{	
            dataRelationHandler.Delete(relationId);
        }
        
        /// <summary>
        /// 获得 DataRelationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataRelationInfo 对象列表</returns>
        public IList<DataRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return dataRelationHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 DataRelation 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> DataRelationInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return dataRelationHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 数据转表
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="whereConditons"></param>
        public void Import(decimal relationId, IList<WhereConditon> whereConditons)
        {
            dataRelationHandler.Import(relationId, whereConditons);
        }

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public IList<DataFieldRelationInfo> GetModelInfosByRelationId(decimal relationId)
        {
            return dataRelationHandler.GetModelInfosByRelationId(relationId);
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal relationId, List<KeyValueItem> keyValueItems)
        {
            dataRelationHandler.UpdateDataFieldRelation(relationId, keyValueItems);
        }

        /// <summary>
        /// 获得表的对应关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetTableRelation(decimal relationId)
        {
            return dataRelationHandler.GetTableRelation(relationId);
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
            return dataRelationHandler.GetDataFieldRelation(relationId, sourceTableId, destinationTableId);
        }

        #endregion
    }
}
