//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessHandler.cs
// 描述：CustomWorkflowProcess 业务处理类
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
    /// CustomWorkflowProcess 接口
    /// </summary>
    public interface ICustomWorkflowProcessHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomWorkflowProcessInfo>
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
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        long GetProcessSetting(decimal stepId);

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        byte GetProcessCategory(decimal stepId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        void UpdateWorkflowProcessAndDataFieldSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 插入工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        void InsertWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
        /// 更新工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        void UpdateWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
        WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId);

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        DataSet GetPageRecordByProcessId(decimal processId);

        /// <summary>
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        void Delete(decimal processId, decimal dataFieldId);

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
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        void InsertCustomWorkflowMapInfo(CustomWorkflowMapInfo customWorkflowMapInfo);

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        void InsertCustomWorkflowMapInfos(IList<CustomWorkflowMapInfo> customWorkflowMapInfos);

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
        /// 根据工作流编号获取节点关系
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        IList<KeyValueItem> GetKeyValueItems(decimal workflowId);

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowMapInfo 对象</returns>
        CustomWorkflowMapInfo GetCustomWorkflowMapInfo(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        void UpdateMapSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode);

        /// <summary>
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        void DeleteCustomWorkflowMapInfo(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 更新 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <param name="processId"></param>
        /// <param name="customWorkflowMapInfo"></param>
        /// <param name="customWorkflowMapInfo">CustomWorkflowMapInfo 对象</param>
        void Update(decimal parentProcessId, decimal processId, CustomWorkflowMapInfo customWorkflowMapInfo);

        /// <summary>
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        DataSet GetProcessMap(decimal workflowId);

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId);

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
