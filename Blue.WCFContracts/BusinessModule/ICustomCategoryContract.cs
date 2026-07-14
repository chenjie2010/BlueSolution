//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomCategoryContract.cs
// 描述： CustomCategory 契约层接口
// 作者：ChenJie 
// 编写日期：2016/9/17
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomCategory 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomCategoryContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomCategoryContract : ICommonNodeContract, IPrincipalContracts<CustomCategoryInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(decimal databaseId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseIds"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecords")]
        DataSet GetPageRecord(IList<decimal> databaseIds);

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDatabaseId")]
        decimal GetDatabaseId(decimal categoryId);

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataWarehouseId")]
        byte GetDataWarehouseId(decimal categoryId);

        #endregion
    }
}