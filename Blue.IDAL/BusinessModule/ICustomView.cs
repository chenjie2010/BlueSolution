//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomView.cs
// 描述：CustomView 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/13
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// CustomView 接口
    /// </summary>
    public interface ICustomView : ICommonNode, IPrincipalTable<CustomViewInfo>
    {
        #region 接口

        /// <summary>
        /// 获得系统字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        Int64 GetSystemDataFields(decimal viewId);

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
        DataTable GetViewData(decimal viewId, Int64 systemLogicalDataFields, bool userAccount, bool departmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);
               
        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图的主表编号</returns>
        decimal GetTableId(decimal viewId);

        /// <summary>
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图物理名称</returns>
        string GetViewPhysicalName(decimal viewId);

        /// <summary>
        /// 获得视图的主物理表类型
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 主物理表类型</returns>
        DataTableType GetMainTableType(decimal viewId);

        /// <summary>
        /// 获得视图的主物理表名
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 物理表名</returns>
        string GetTablePhysicalName(decimal viewId);

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        byte GetDataWarehouseId(decimal viewId);

        /// <summary>
        /// 向 CustomView 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo">customViewInfo 对象</param>
        /// <param name="customViewAndTableInfos">视图与表的关系</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos);

        /// <summary>
        /// 更新一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <param name="customViewAndTableInfos"></param>
        void Update(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos);

        #endregion
    }
}