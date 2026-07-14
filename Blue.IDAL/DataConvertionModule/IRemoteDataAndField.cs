//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IRemoteDataAndField.cs
// 描述: RemoteDataAndField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/10/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.DataConvertionModule;

namespace Blue.IDAL.DataConvertionModule
{
    /// <summary>
    /// RemoteDataAndField 接口
    /// </summary>
    public interface IRemoteDataAndField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        Dictionary<decimal, decimal> GetDataFieldRelation(decimal remoteDataId, decimal destinationTableId);

        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        IList<RemoteDataAndFieldInfo> GetModelInfos(decimal remoteDataId);

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="keyValueItems"></param>
        void UpdateDataFieldRelation(decimal remoteDataId, List<KeyValueItem> keyValueItems);

        /// <summary>
        /// 获本地的表与字段对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        Dictionary<decimal, Dictionary<decimal, decimal>> GetTableRelation(decimal remoteDataId);

        #endregion
    }
}