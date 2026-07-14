//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IRemoteDataContract.cs
// 描述: RemoteData 契约层接口
// 作者：ChenJie 
// 编写日期：2018/10/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.DataConvertionModule;

namespace Blue.WCFContracts.DataConvertionModule
{
    /// <summary>
    /// RemoteData 契约接口
    /// </summary>
    [ServiceContract(Name = "IRemoteDataContract", Namespace = "http://www.scu.edu.cn/DataConvertionModule/")]
    public interface IRemoteDataContract : ICommonNodeContract, IPrincipalContracts<RemoteDataInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFieldRelation")]
        Dictionary<decimal, decimal> GetDataFieldRelation(decimal remoteDataId, decimal destinationTableId);

        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByRemoteDataId")]
        IList<RemoteDataAndFieldInfo> GetModelInfos(decimal remoteDataId);

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="keyValueItems"></param>
        [OperationContract(Name = "UpdateDataFieldRelation")]
        void UpdateDataFieldRelation(decimal remoteDataId, List<KeyValueItem> keyValueItems);

        /// <summary>
        /// 获本地的表与字段对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableRelation")]
        Dictionary<decimal, Dictionary<decimal, decimal>> GetTableRelation(decimal remoteDataId);

        #endregion
    }
}