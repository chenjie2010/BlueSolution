//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomViewAndTable.cs
// 描述：CustomViewAndTable 数据访问层接口
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
    /// CustomViewAndTable 接口
    /// </summary>
    public interface ICustomViewAndTable : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 根据视图的信息获得表的信息
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetTablesByViewId(decimal viewId);

        /// <summary>
        /// 获得视图与表对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CustomViewAndTableInfo> GetModelInfos(decimal viewId);

        #endregion
    }
}