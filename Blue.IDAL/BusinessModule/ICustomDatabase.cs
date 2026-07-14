//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomDatabase.cs
// 描述：CustomDatabase 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/9/11
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
    /// CustomDatabase 接口
    /// </summary>
    public interface ICustomDatabase : ICommonNode, IPrincipalTable<CustomDatabaseInfo>
    {
        #region 接口

        /// <summary>
        /// 获得数据库的逻辑名称
        /// </summary>
        ///<param name="databaseId">数据库编号</param>
        /// <returns> 数据库的逻辑名称</returns>
        string GetDatabaseName(decimal databaseId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(byte dataWarehouseId);

        #endregion
    }
}