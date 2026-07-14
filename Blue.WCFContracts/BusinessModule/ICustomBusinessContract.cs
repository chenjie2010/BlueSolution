//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomBusinessContract.cs
// 描述： CustomBusiness 契约层接口
// 作者：ChenJie 
// 编写日期：2017/12/20
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
    /// CustomBusiness 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomBusinessContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomBusinessContract : ICommonNodeContract, IPrincipalContracts<CustomBusinessInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 根据条件查询业务数量
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTotalCountByConditionId")]
        int GetTotalCount(decimal conditionId, BusinessMenu businessMenu);

        /// <summary>
        /// 通过数据填报编号获取业务名称
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecord")]
        string GetBusinessNameByInstanceId(decimal dataId);

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetBusiness")]
        IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId);

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns
        [OperationContract(Name = "DownLoadIcons")]
        byte[] DownLoadIcons(string fileName);

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithAttachmments")]
        decimal Insert(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData);


        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        [OperationContract(Name = "UpdateWithAttachmments")]
        void Update(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData);

        #endregion
    }
}