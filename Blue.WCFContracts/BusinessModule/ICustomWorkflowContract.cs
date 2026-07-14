//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomWorkflowContract.cs
// 描述： CustomWorkflow 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
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
    /// CustomWorkflow 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomWorkflowContract : ICommonNodeContract, IPrincipalContracts<CustomWorkflowInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 工作流节点作为父节点的数目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetParentNodeCount")]
        int GetParentNodeCount(decimal workflowId);

        /// <summary>
        /// 根据工作流实例编号获得工作流对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomWorkflowInfo")]
        CustomWorkflowInfo GetCustomWorkflowInfo(decimal instanceId);

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithUpLoadFileInfos")]
        decimal Insert(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        [OperationContract(Name = "UpdateWithUpLoadFileInfos")]
        void Update(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        #endregion
    }
}