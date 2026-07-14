//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomQueyHandler.cs
// 描述：CustomQuey 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/31
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
    /// 业务层处理类，对于的表： dbo.CustomQuey.
    /// </summary>
    public class CustomQueyHandler : CommonNodeBusiness, ICustomQueyHandler
    {
        #region 工厂类实例

        private static readonly ICustomQuey dalCustomQuey = BusinessDataAccessFactory.CreateCustomQuey();
        private static readonly ICustomQueyAndDataField dalCustomQueyAndDataField = BusinessDataAccessFactory.CreateCustomQueyAndDataField();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomQueyHandler() : base(dalCustomQuey)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customquey 表中插入一条新记录
        /// </summary>
        /// <param name="customQueyInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomQueyInfo customQueyInfo)
        {
            //自动增加的关键字的值
            decimal customQueyId = 0;

            // 验证输入
            if (customQueyInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customQueyId = dalCustomQuey.Insert(customQueyInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customQueyId;
        }

        /// <summary>
        /// 获得 CustomQueyInfo 对象
        /// </summary>
        ///<param name="dataQueriedId">数据查询编号</param>
        /// <returns> CustomQueyInfo 对象</returns>
        public CustomQueyInfo GetModelInfo(decimal dataQueriedId)
        {
            CustomQueyInfo customQueyInfo = null;

            // 验证输入
            if (dataQueriedId < 0)
            {
                return null;
            }

            try
            {
                customQueyInfo = dalCustomQuey.GetModelInfo(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customQueyInfo;
        }

        /// <summary>
        /// 更新 CustomQueyInfo 对象
        /// </summary>
        /// <param name="customQueyInfo">CustomQueyInfo 对象</param>
        public void Update(CustomQueyInfo customQueyInfo)
        {
            // 验证输入
            if (customQueyInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomQuey.Update(customQueyInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomQueyInfo 对象
        /// </summary>
        ///<param name="dataQueriedId">数据查询编号</param>
        /// <returns> CustomQueyInfo 对象</returns>
        public void Delete(decimal dataQueriedId)
        {
            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomQuey.Delete(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomQueyInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomQueyInfo 对象列表</returns>
        public IList<CustomQueyInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomQueyInfo> customQueyInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customQueyInfos = dalCustomQuey.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customQueyInfos;
        }

        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomQuey.GetTotalCount(whereConditons);
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
        /// 获得分页数据集记录数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public int GetAuthorizedRecordCount(QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, byte dataWarehouseId)
        {
            int count = 0;

            try
            {
                count = dalCustomQuey.GetAuthorizedRecordCount(queryBuilder, whereConditons, dataWarehouseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 完全删除记录(多表联合查询使用)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tableIds"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        public void Delete(decimal dataWarehouseId, IList<decimal> tableIds, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons)
        {
            try
            {
                dalCustomQuey.Delete(dataWarehouseId, tableIds, queryBuilder, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomQuey.GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomQuey.GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得 CustomQuey 表中记录的数目
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>CustomQueyInfo 记录的数目</returns>
        public int GetTotalCountByViewId(decimal viewId)
        {
            int count = 0;

            if (viewId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomQuey.GetTotalCountByViewId(viewId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetQueriedData(decimal dataQueriedId, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataTable dataTable = null;

            if (dataQueriedId <= 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                dataTable = dalCustomQuey.GetQueriedData(dataQueriedId, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }
        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetCustomDataFieldInfos(decimal dataQueriedId)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = null;
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                customDataFieldInfos = dalCustomQueyAndDataField.GetCustomDataFieldInfos(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }
        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDigitDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = null;
            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                commonNodes = dalCustomQueyAndDataField.GetDigitDataFields(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetConditionalCustomDataFieldInfos(decimal dataQueriedId)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = null;
            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                customDataFieldInfos = dalCustomQueyAndDataField.GetConditionalCustomDataFieldInfos(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetAssociatedDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                commonNodes = dalCustomQueyAndDataField.GetAssociatedDataFields(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateCustomQueyAndDataFieldSorting(decimal dataQueriedId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            // 验证输入
            if (dataQueriedId <= 0 || dataFieldId <= 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomQueyAndDataField.UpdateSorting(dataQueriedId, dataFieldId, movedDriectionOfNode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得自定义查询中的视图字段（不包含系统字段）
        /// </summary>
        /// <param name="dataQueriedId">查询编号</param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                commonNodes = dalCustomQuey.GetDataFields(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
		/// 向 CustomQueyAndDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customQueyAndDataFieldInfo">customQueyAndDataFieldInfo 对象</param>
		public void InsertCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            // 验证输入
            if (customQueyAndDataFieldInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dalCustomQueyAndDataField.Insert(customQueyAndDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		/// 获得 CustomQueyAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyAndDataFieldInfo 对象</returns>
		public CustomQueyAndDataFieldInfo GetCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId)
        {
            CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo = null;

            // 验证输入
            if (dataQueriedId < 0)
            {
                return null;
            }

            try
            {
                customQueyAndDataFieldInfo = dalCustomQueyAndDataField.GetModelInfo(dataFieldId, dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customQueyAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">CustomQueyAndDataFieldInfo 对象</param>
        public void UpdateCustomQueyAndDataFieldInfo(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
        {
            // 验证输入
            if (customQueyAndDataFieldInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomQueyAndDataField.Update(customQueyAndDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		///  删除 CustomQueyAndDataFieldInfo 对象
		/// </summary>
	    ///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		public void DeleteCustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId)
        {
            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomQueyAndDataField.Delete(dataFieldId, dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereSentences(decimal dataQueriedId, string whereExpression)
        {
            bool result = false;

            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                result = dalCustomQuey.ValidateWhereSentences(dataQueriedId, whereExpression);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
		public DataSet GetCustomQueyAndDataFieldInfos(decimal dataQueriedId)
        {
            DataSet ds = null;

            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                ds = dalCustomQueyAndDataField.GetCustomQueyAndDataFieldInfos(dataQueriedId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得SQL语句
        /// </summary>
        ///<param name="dataQueriedId">查询编号</param>
        /// <returns> SQL语句</returns>
        public string GetConditions(decimal dataQueriedId)
        {
            string conditions = string.Empty;

            // 验证输入
            if (dataQueriedId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                conditions = dalCustomQuey.GetConditions(dataQueriedId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return conditions;
        }

        /// <summary>
        /// 验证SQL语句是否正确
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns></returns>
        public bool ValidateSQL(byte dataWarehouseId, string sql)
        {
            bool result = false;

            try
            {
                result = dalCustomQuey.ValidateSQL(dataWarehouseId, sql);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;

        }

        #endregion

        #region 私有方法

        #endregion
    }
}
