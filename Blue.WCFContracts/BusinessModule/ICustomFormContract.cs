//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomFormContract.cs
// 描述： CustomForm 契约层接口
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
    /// CustomForm 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomFormContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomFormContract : ICommonNodeContract, IPrincipalContracts<CustomFormInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 通过组合表编号查询数据填报的窗体数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        [OperationContract(Name = "GetTotalCountByCombinedTableId")]
        int GetTotalCountByCombinedTableId(decimal combinedTableId);

        /// <summary>
        /// 向 CustomForm 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithAttachmments")]
        decimal Insert(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新数据表格和附件信息
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        [OperationContract(Name = "UpdateWithAttachmments")]
        void Update(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosBySectionId")]
        IList<CustomFormInfo> GetModelInfos(decimal sectionId);

        #endregion
    }
}