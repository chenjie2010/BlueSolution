//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IAssociatedDataFieldContract.cs
// 描述： AssociatedDataField 契约层接口
// 作者：ChenJie 
// 编写日期：2016/10/3
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// AssociatedDataField 契约接口
    /// </summary>
    [ServiceContract(Name = "IAssociatedDataFieldContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface IAssociatedDataFieldContract : ICommonNodeContract, IPrincipalContracts<AssociatedDataFieldInfo>
    {
        #region 自定义接口        

        /// <summary>
        /// 获得字段的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        [OperationContract(Name = "GetPhysicalName")]
        string GetPhysicalName(decimal associatedDataFieldId);

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        [OperationContract(Name = "GetLogicalName")]
        string GetLogicalName(decimal associatedDataFieldId);

        /// <summary>
        /// 通过关联字段的类型获得关联表中关联字段个数
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="DataFieldCategory"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAssociatedDataFieldCount")]
        int GetAssociatedDataFieldCount(decimal associationId, AssociatedDataFieldCategory dataFieldCategory);

        /// <summary>
        /// 获得字段的属性信息
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFieldProperties")]
        List<BasedDataFieldInfo> GetDataFieldProperties(decimal associationId);

        /// <summary>
        /// 获得字段的长度
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的长度</returns>
        [OperationContract(Name = "GetDataLength")]
        int GetDataLength(decimal associatedDataFieldId);

        /// <summary>
        /// 关联表的字段名称关系
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFieldNameRelation")]
        Dictionary<string, string> GetDataFieldNameRelation(decimal dataFieldId);

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetBasedDataType")]
        BasedDataType GetBasedDataType(decimal associatedDataFieldId);

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAssociationId")]
        decimal GetAssociationId(decimal associatedDataFieldId);

        /// <summary>
        /// 获得字段列表
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModeInfosByAssociationId")]
        IList<AssociatedDataFieldInfo> GetModelInfos(decimal associationId);

        #endregion
    }
}