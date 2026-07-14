//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewService.cs
// 描述：CustomView 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/13
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomView.
    /// </summary>
    public class CustomViewService : CommonNodeServices, ICustomViewContract
    {
        #region 业务实例

        private static readonly ICustomViewHandler customViewHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomViewHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewService() : base(customViewHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customview 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomViewInfo customViewInfo)
        {
            return customViewHandler.Insert(customViewInfo);
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> CustomViewInfo 对象</returns>
        public CustomViewInfo GetModelInfo(decimal viewId)
        {
            return customViewHandler.GetModelInfo(viewId);
        }

        /// <summary>
        /// 更新 CustomViewInfo 对象
        /// </summary>
        /// <param name="customViewInfo">CustomViewInfo 对象</param>
        public void Update(CustomViewInfo customViewInfo)
        {
            customViewHandler.Update(customViewInfo);
        }

        /// <summary>
        /// 删除 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> CustomViewInfo 对象</returns>
        public void Delete(decimal viewId)
        {
            customViewHandler.Delete(viewId);
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewInfo 对象列表</returns>
        public IList<CustomViewInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customViewHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomView 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomViewInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customViewHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得系统字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public Int64 GetSystemDataFields(decimal viewId)
        {
            return customViewHandler.GetSystemDataFields(viewId);
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
            return customViewHandler.GetViewData(viewId, systemLogicalDataFields, userAccount, departmentProperty, dataFieldNameRelations,
            startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }
        
        /// <summary>
        /// 根据视图的信息获得表的信息
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTablesByViewId(decimal viewId)
        {
            return customViewHandler.GetTablesByViewId(viewId);
        }

        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图的主表编号</returns>
        public decimal GetTableId(decimal viewId)
        {
            return customViewHandler.GetTableId(viewId);
        }

        /// <summary>
        /// 获得视图的主物理表类型
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 主物理表类型</returns>
        public DataTableType GetMainTableType(decimal viewId)
        {
            return customViewHandler.GetMainTableType(viewId);
        }

        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图物理名称</returns>
        public string GetViewPhysicalName(decimal viewId)
        {
            return customViewHandler.GetViewPhysicalName(viewId);
        }

        /// <summary>
        /// 获得视图的主物理表名
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal viewId)
        {
            return customViewHandler.GetTablePhysicalName(viewId);
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal viewId)
        {
            return customViewHandler.GetDataWarehouseId(viewId);
        }

        /// <summary>
        /// 获得视图与表对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CustomViewAndTableInfo> GetAssociatedTables(decimal viewId)
        {
            return customViewHandler.GetAssociatedTables(viewId);
        }
     
        /// <summary>
        /// 更新视图编号与字段编号的关系
        /// </summary>
        /// <param name="viewId"></param>
        public void UpdateDataFields(decimal viewId, IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos)
        {
            customViewHandler.UpdateDataFields(viewId, customViewAndDataFieldInfos);
        }

        /// <summary>
        /// 获得字段
        /// 节点的父编号为数据表编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetAssociatedDataFields(decimal viewId)
        {
            return customViewHandler.GetAssociatedDataFields(viewId);
        }

        /// <summary>
        /// 向 CustomView 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo">customViewInfo 对象</param>
        /// <param name="customViewAndTableInfos">视图与表的关系</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            return customViewHandler.Insert(customViewInfo, customViewAndTableInfos);
        }

        /// <summary>
        /// 更新一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <param name="customViewAndTableInfos"></param>
        public void Update(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            customViewHandler.Update(customViewInfo, customViewAndTableInfos);
        }

        #endregion
    }
}
