//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTableHandler.cs
// 描述: CombinedTable 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/15
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CombinedTable.
    /// </summary>
    public class CombinedTableHandler : CommonNodeBusiness, ICombinedTableHandler
    {
        #region 工厂类实例
        
        private static readonly ICombinedTable dalCombinedTable = BusinessDataAccessFactory.CreateCombinedTable();
        private static readonly ICombinedDataField dalCombinedDataField = BusinessDataAccessFactory.CreateCombinedDataField();
        private static readonly ICombinedTableRelation dalCombinedTableRelation = BusinessDataAccessFactory.CreateCombinedTableRelation();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CombinedTableHandler() : base(dalCombinedTable)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 combinedtable 表中插入一条新记录
		/// </summary>
		/// <param name="combinedTableInfo"></param>
		/// <returns></returns>
		public decimal Insert(CombinedTableInfo combinedTableInfo)
		{
            //自动增加的关键字的值
			decimal combinedTableId = 0;
            
			// 验证输入
			if (combinedTableInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                combinedTableId = dalCombinedTable.Insert(combinedTableInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return combinedTableId;
		}
        
        /// <summary>
		/// 获得 CombinedTableInfo 对象
		/// </summary>
		///<param name="combinedTableId"></param>
		/// <returns> CombinedTableInfo 对象</returns>
		public CombinedTableInfo GetModelInfo(decimal combinedTableId)
		{			
			CombinedTableInfo  combinedTableInfo = null;
            
			// 验证输入
			if(combinedTableId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                combinedTableInfo =  dalCombinedTable.GetModelInfo(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return combinedTableInfo;
		}        
        
        /// <summary>
		/// 更新 CombinedTableInfo 对象
		/// </summary>
		/// <param name="combinedTableInfo">CombinedTableInfo 对象</param>
		public void Update(CombinedTableInfo combinedTableInfo)
		{	
            // 验证输入
            if (combinedTableInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCombinedTable.Update(combinedTableInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CombinedTableInfo 对象
		/// </summary>
		///<param name="combinedTableId"></param>
		/// <returns> CombinedTableInfo 对象</returns>
		public void Delete(decimal combinedTableId)
		{		
            // 验证输入
			if(combinedTableId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCombinedTable.Delete(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CombinedTableInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CombinedTableInfo  对象列表</returns>
		public IList<CombinedTableInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CombinedTableInfo>  combinedTableInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                combinedTableInfos = dalCombinedTable.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return combinedTableInfos;
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
                count = dalCombinedTable.GetTotalCount(whereConditons);
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
        /// 获得组合表的记录数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal combinedTableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalCombinedTable.GetRecordCount(combinedTableId, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得组合表的记录
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetTableData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            try
            {
                ds = dalCombinedTable.GetTableData(combinedTableId, dataFieldNameRelations, startPosition, count, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得组合表的分页数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetCombinedTableData(decimal combinedTableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId,
            int startPosition, int count, ref int totalCount)
        {
            DataTable dataTable = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCombinedTable.GetCombinedTableData(combinedTableId, systemLogicalDataFields, dataFieldNameRelations, userId, startPosition, count, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }

        /// <summary>
        /// 获得不同类型的表的数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataTableType"></param>
        /// <returns></returns>
        public int GetTableCountByTableType(decimal combinedTableId, DataTableType dataTableType)
        {
            int count = 0;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表编号不能小于或是等于0。");
            }

            try
            {
                count = dalCombinedTableRelation.GetTableCountByTableType(combinedTableId, dataTableType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataRowItem> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId, bool onlyTarget)
        {
            Dictionary<decimal, DataRowItem> dataRowItems = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表编号不能小于或是等于0。");
            }

            try
            {
                dataRowItems = dalCombinedTable.GetMirrorRowData(combinedTableId, dataFieldNameRelations, instanceId, onlyTarget);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataRowItems;
        }

        /// <summary>
        /// 获得表的编号列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<decimal> GetTableIds(decimal combinedTableId)
        {
            IList<decimal> tableIds = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                tableIds = dalCombinedTableRelation.GetSecondIds(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableIds;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId)
        {
            Dictionary<decimal, DataTable> dataRowValues = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                dataRowValues = dalCombinedTable.GetMirrorRowData(combinedTableId, dataFieldNameRelations, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataRowValues;
        }

        /// <summary>
        /// 获得组合表的数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataTable GetCombinedTableData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            DataTable dataTable = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCombinedTable.GetCombinedTableData(combinedTableId, businessEnabled, dataFieldNameRelations, userId, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }

        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal userId, decimal combinedTableId, bool businessEnabled, decimal instanceId)
        {
            int count = 0;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表编号不能小于或是等于0。");
            }

            try
            {
                count = dalCombinedTable.GetRecordCount(userId, combinedTableId, businessEnabled, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetDataFilledData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            Dictionary<decimal, DataTable> dataRowValues = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                dataRowValues = dalCombinedTable.GetDataFilledData(combinedTableId, businessEnabled, dataFieldNameRelations, userId, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataRowValues;
        }

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <param name="combinedTableRelationInfos">关系表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            //自动增加的关键字的值
            decimal combinedTableId = 0;

            // 验证输入
            if (combinedTableInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                combinedTableId = dalCombinedTable.Insert(combinedTableInfo, combinedTableRelationInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return combinedTableId;
        }

        /// <summary>
        /// 更新组合表信息
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <param name="combinedTableRelationInfos"></param>
        public void Update(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            // 验证输入
            if (combinedTableInfo == null)
            {
                throw new ArgumentException("不能删除空对象.");
            }

            try
            {
                dalCombinedTable.Update(combinedTableInfo, combinedTableRelationInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public List<CommonNode> GetDataFields(decimal combinedTableId)
        {
            List<CommonNode> commonNodes = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCombinedDataField.GetDataFields(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
                        
            return commonNodes;
        }

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTables(decimal combinedTableId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCombinedTableRelation.GetTables(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;            
        }

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        public void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos)
        {
            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCombinedDataField.UpdateDataFields(combinedTableId, combinedDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

        }

        #endregion

        #region 私有方法

        #endregion
    }
}
