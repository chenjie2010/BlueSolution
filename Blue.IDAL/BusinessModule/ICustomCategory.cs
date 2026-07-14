//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomCategory.cs
// 描述：CustomCategory 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/9/17
// Copyright 2016
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
    /// CustomCategory 接口
    /// </summary>
    public interface ICustomCategory: ICommonNode, IPrincipalTable<CustomCategoryInfo>
    {
        #region 接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseIds"></param>
        /// <returns></returns>
        DataSet GetPageRecord(IList<decimal> databaseIds);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal databaseId);

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        decimal GetDatabaseId(decimal categoryId);

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        byte GetDataWarehouseId(decimal categoryId);

        #endregion
    }
}