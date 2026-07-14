//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataRelationContract.cs
// 描述: DataRelation 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
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
    /// DataRelation 契约接口
    /// </summary>
    [ServiceContract(Name = "IDataRelationContract", Namespace = "http://www.scu.edu.cn/DataConvertionModule/")]
    public interface IDataRelationContract : ICommonNodeContract, IPrincipalContracts<DataRelationInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 数据转表
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="whereConditons"></param>
        [OperationContract(Name = "Import")]
        void Import(decimal relationId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByRelationId")]
        IList<DataFieldRelationInfo> GetModelInfosByRelationId(decimal relationId);

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="keyValueItems"></param>
        [OperationContract(Name = "UpdateDataFieldRelation")]
        void UpdateDataFieldRelation(decimal relationId, List<KeyValueItem> keyValueItems);

        /// <summary>
        /// 获得表的对应关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableRelation")]
        Dictionary<decimal, decimal> GetTableRelation(decimal relationId);

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFieldRelation")]
        Dictionary<decimal, decimal> GetDataFieldRelation(decimal relationId, decimal sourceTableId, decimal destinationTableId);
        
        #endregion
    }
}