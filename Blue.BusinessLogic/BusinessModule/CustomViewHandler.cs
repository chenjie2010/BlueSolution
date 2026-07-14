//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewHandler.cs
// 描述：CustomView 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/13
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
    /// 业务层处理类，对于的表： dbo.CustomView.
    /// </summary>
    public class CustomViewHandler : CommonNodeBusiness, ICustomViewHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomView dalCustomView = BusinessDataAccessFactory.CreateCustomView();
        private static readonly ICustomTable dalCustomTable = BusinessDataAccessFactory.CreateCustomTable();
        private static readonly ICustomViewAndTable dalCustomViewAndTable = BusinessDataAccessFactory.CreateCustomViewAndTable();
        private static readonly ICustomViewAndDataField dalCustomViewAndDataField = BusinessDataAccessFactory.CreateCustomViewAndDataField();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewHandler() : base(dalCustomView)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customview 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomViewInfo customViewInfo)
        {
            //自动增加的关键字的值
            decimal customViewId = 0;

            // 验证输入
            if (customViewInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customViewId = dalCustomView.Insert(customViewInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customViewId;
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> CustomViewInfo 对象</returns>
        public CustomViewInfo GetModelInfo(decimal viewId)
        {
            CustomViewInfo customViewInfo = null;

            // 验证输入
            if (viewId < 0)
            {
                return null;
            }

            try
            {
                customViewInfo = dalCustomView.GetModelInfo(viewId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customViewInfo;
        }

        /// <summary>
        /// 更新 CustomViewInfo 对象
        /// </summary>
        /// <param name="customViewInfo">CustomViewInfo 对象</param>
        public void Update(CustomViewInfo customViewInfo)
        {
            // 验证输入
            if (customViewInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomView.Update(customViewInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> CustomViewInfo 对象</returns>
        public void Delete(decimal viewId)
        {
            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomView.Delete(viewId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewInfo 对象列表</returns>
        public IList<CustomViewInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomViewInfo> customViewInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customViewInfos = dalCustomView.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customViewInfos;
        }

        /// <summary>
        /// 获得 CustomView 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomViewInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomView.GetTotalCount(whereConditons);
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
        /// 获得系统字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public Int64 GetSystemDataFields(decimal viewId)
        {
            Int64 systemDataFields = 0;

            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                systemDataFields = dalCustomView.GetSystemDataFields(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemDataFields;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="userAccount"></param>
        /// <param name="departmentProperty"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetViewData(decimal viewId, Int64 systemLogicalDataFields, bool userAccount, bool departmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataTable dataTables = null;

            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                dataTables = dalCustomView.GetViewData(viewId, systemLogicalDataFields, userAccount, departmentProperty, dataFieldNameRelations,
             startPosition, count, whereConditons, sortingCondtions, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTables;
        }

      

        /// <summary>
        /// 根据视图的信息获得表的信息
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTablesByViewId(decimal viewId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                commonNodes = dalCustomViewAndTable.GetTablesByViewId(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图的主表编号</returns>
        public decimal GetTableId(decimal viewId)
        {
            decimal tableId = decimal.MinValue;

            // 验证输入
            if (viewId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                tableId = dalCustomView.GetTableId(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableId;
        }

        /// <summary>
        /// 获得视图的主物理表类型
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 主物理表类型</returns>
        public DataTableType GetMainTableType(decimal viewId)
        {
            DataTableType tableType = 0;

            // 验证输入
            if (viewId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                tableType = dalCustomView.GetMainTableType(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableType;
        }

        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图物理名称</returns>
        public string GetViewPhysicalName(decimal viewId)
        {
            string physicalName = string.Empty;

            // 验证输入
            if (viewId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                physicalName = dalCustomView.GetViewPhysicalName(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        }

        /// <summary>
        /// 获得视图的主物理表名
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal viewId)
        {
            string physicalName = string.Empty;

            // 验证输入
            if (viewId <= 0)
            {
                throw new ArgumentException("视图编号不能小于或是等于0。");
            }

            try
            {
                physicalName = dalCustomView.GetTablePhysicalName(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        }        

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal viewId)
        {
            byte dataWarehouseId = 0;

            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                dataWarehouseId = dalCustomView.GetDataWarehouseId(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        /// <summary>
        /// 获得视图与表对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CustomViewAndTableInfo> GetAssociatedTables(decimal viewId)
        {
            IList<CustomViewAndTableInfo> customViewAndTableInfos = null;

            // 验证输入
            if (viewId < 0)
            {
                throw new ArgumentException("编号错误。");
            }
            try
            {
                customViewAndTableInfos = dalCustomViewAndTable.GetModelInfos(viewId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customViewAndTableInfos;
        }

        /// <summary>
        /// 获得字段
        /// 节点的父编号为视图编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetAssociatedDataFields(decimal viewId)
        {
            IList<CommonNode> commonNodes = null;

            Dictionary<decimal, string> dicTableNames = new Dictionary<decimal, string>();
            commonNodes = dalCustomViewAndDataField.GetDataFieldsByViewId(viewId);

            foreach (var obj in commonNodes)
            {
                string logicalName = string.Empty;
                if (!dicTableNames.ContainsKey(obj.ParentNodeId))
                {
                    logicalName = dalCustomTable.GetTableLogicalName(obj.ParentNodeId);
                    dicTableNames.Add(obj.ParentNodeId, logicalName);
                }
                else
                {
                    logicalName = dicTableNames[obj.ParentNodeId];
                }

                obj.NodeName = string.Format("[{0}][{1}]", logicalName, obj.NodeName);
            }
            
            return commonNodes;
        }

        /// <summary>
        /// 向 CustomView 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo">customViewInfo 对象</param>
        /// <param name="customViewAndTableInfos">视图与表的关系</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            //自动增加的关键字的值
            decimal customViewId = 0;

            // 验证输入
            if (customViewInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customViewId = dalCustomView.Insert(customViewInfo, customViewAndTableInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customViewId;
        }

        /// <summary>
        /// 更新一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <param name="customViewAndTableInfos"></param>
        public void Update(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            // 验证输入
            if (customViewInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomView.Update(customViewInfo, customViewAndTableInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
        /// 更新视图编号与字段编号的关系
        /// </summary>
        /// <param name="viewId"></param>
        public void UpdateDataFields(decimal viewId, IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos)
        {

            // 验证输入
            if (viewId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }
            try
            {
                dalCustomViewAndDataField.UpdateDataFields(viewId, customViewAndDataFieldInfos);
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
