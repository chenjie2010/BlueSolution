//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomDatabaseContract.cs
// 描述： CustomDatabase 契约层接口
// 作者：ChenJie 
// 编写日期：2016/9/11
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
    /// CustomDatabase 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomDatabaseContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface ICustomDatabaseContract : ICommonNodeContract, IPrincipalContracts<CustomDatabaseInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(byte dataWarehouseId);

        #endregion
    }
}