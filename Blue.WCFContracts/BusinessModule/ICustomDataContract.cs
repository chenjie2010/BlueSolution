//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomDataContract.cs
// 描述： CustomData 契约层接口
// 作者：ChenJie 
// 编写日期：2017/11/27
// Copyright 2017
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
    /// CustomData 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomDataContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomDataContract : ICommonNodeContract, IPrincipalContracts<CustomDataInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得填报属性
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFilledProperty")]
        byte GetDataFilledProperty(decimal dataId);

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetFinalReviewers")]
        Dictionary<decimal, string> GetFinalReviewers(decimal dataId);

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetInitReviewers")]
        Dictionary<decimal, string> GetInitReviewers(decimal dataId, decimal userId);

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithAttachmments")]
        decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos);


        /// <summary>
        /// 更新数据填报和附件信息
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        [OperationContract(Name = "UpdateWithAttachmments")]
        void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos);

        #endregion
    }
}