//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowHandler.cs
// 描述：CustomWorkflow 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
/// <summary>
    /// CustomWorkflow 接口
    /// </summary>
    public interface ICustomWorkflowHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomWorkflowInfo>
    {
        #region 接口

        /// <summary>
        /// 工作流节点作为父节点的数目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        int GetParentNodeCount(decimal workflowId);

        /// <summary>
        /// 根据工作流实例编号获得工作流对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        CustomWorkflowInfo GetCustomWorkflowInfo(decimal instanceId);

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        void Update(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        #endregion
    }
}
