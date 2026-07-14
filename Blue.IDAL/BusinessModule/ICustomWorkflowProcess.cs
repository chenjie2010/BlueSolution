//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomWorkflowProcess.cs
// 描述：CustomWorkflowProcess 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// CustomWorkflowProcess 接口
    /// </summary>
    public interface ICustomWorkflowProcess : ICommonNode, IPrincipalTable<CustomWorkflowProcessInfo>
    {
        #region 接口

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<decimal> GetGlobalNextReviewers(decimal userId, decimal processId, int top);

        /// <summary>
        /// 获得单位间动态节点的审核用户
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="depIds"></param>
        /// <returns></returns>
        Dictionary<decimal, decimal> GetNextReviewers(decimal processId, IList<decimal> depIds);

        /// <summary>
        /// 获取下一步条件节点的联系人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId, Dictionary<decimal, CommonDataField> commonDataFields);

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        IList<decimal> GetNextReviewers(decimal userId, decimal processId, int top);

        /// <summary>
        /// 获得条件范围
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        ConditionType GetConditionType(decimal processId);

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        byte GetProcessCategory(decimal stepId);

        /// <summary>
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        long GetProcessSetting(decimal stepId);

        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal workflowId, byte processCategory);

        /// <summary>
        /// 通过角色查看工作流参与的数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        int GetWorkflowProcessCountByRoleId(decimal roleId);

        /// <summary>
        /// 获得工作流节点编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        decimal GetWorkflowId(decimal processId);

        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="processId"></param>
        void UpdateWorkflowRootNode(decimal processId);

        /// <summary>
        /// 获得工作流根节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        CommonNode GetWorkflowRootNode(decimal workflowId);

        /// <summary>
        ///获取下一步处理联系人
        /// </summary>
        /// <param name="userId"></param>
        ///  /// <param name="processId"></param>
        /// <returns></returns>
        Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId);

        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        string GetWorkflowName(decimal processId);

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        string GetProcessName(decimal processId);

        /// <summary>
        /// 获得工作流程的节点类型
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        byte GetProcessType(decimal processId);

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);
   
        /// <summary>
        /// 获得数据填报编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        decimal GetDatalId(decimal processId);

        #endregion
    }
}