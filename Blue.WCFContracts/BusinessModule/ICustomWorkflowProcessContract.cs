//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomWorkflowProcessContract.cs
// 描述： CustomWorkflowProcess 契约层接口
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
    /// CustomWorkflowProcess 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowProcessContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomWorkflowProcessContract : ICommonNodeContract, IPrincipalContracts<CustomWorkflowProcessInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetGlobalNextReviewers")]
        IList<decimal> GetGlobalNextReviewers(decimal userId, decimal processId, int top);

        /// <summary>
        /// 获得单位间动态节点的审核用户
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="depIds"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetNextReviewersByDepIds")]
        Dictionary<decimal, decimal> GetNextReviewers(decimal processId, IList<decimal> depIds);

        /// <summary>
        /// 获取下一步条件节点的联系人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetNextReviewersByUserId")]
        Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId, Dictionary<decimal, CommonDataField> commonDataFields);
        
        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetNextReviewersByProcessId")]
        IList<decimal> GetNextReviewers(decimal userId, decimal processId, int top);

        /// <summary>
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetProcessSetting")]
        long GetProcessSetting(decimal stepId);

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetProcessCategory")]
        byte GetProcessCategory(decimal stepId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        [OperationContract(Name = "UpdateWorkflowProcessAndDataFieldSorting")]
        void UpdateWorkflowProcessAndDataFieldSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode);
        
        /// <summary>
        /// 插入工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        [OperationContract(Name = "InsertWorkflowProcessAndDataFieldInfo")]
        void InsertWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
        /// 更新工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        [OperationContract(Name = "UpdateWorkflowProcessAndDataFieldInfo")]
        void UpdateWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
        [OperationContract(Name = "GetWorkflowProcessAndDataFieldInfo")]
        WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId);

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordByProcessId")]
        DataSet GetPageRecordByProcessId(decimal processId);

        /// <summary>
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        [OperationContract(Name = "DeleteWorkflowProcessAndDataFieldInfo")]
        void Delete(decimal processId, decimal dataFieldId);

        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByWorkflowId")]
        IList<CommonNode> GetCommonNodes(decimal workflowId, byte processCategory);

        /// <summary>
        /// 通过角色查看工作流参与的数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetWorkflowProcessCountByRoleId")]
        int GetWorkflowProcessCountByRoleId(decimal roleId);

        /// <summary>
        /// 获得工作流节点编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetWorkflowId")]
        decimal GetWorkflowId(decimal processId);

        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="processId"></param>
        [OperationContract(Name = "UpdateWorkflowRootNode")]
        void UpdateWorkflowRootNode(decimal processId);

        /// <summary>
        /// 获得工作流根节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetWorkflowRootNode")]
        CommonNode GetWorkflowRootNode(decimal workflowId);

        /// <summary>
        ///获取下一步处理联系人
        /// </summary>
        /// <param name="userId"></param>
        ///  /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetNextReviewers")]
        Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId);

        /// <summary>
        /// 根据工作流编号获取节点关系
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        /// 
        [OperationContract(Name = "GetKeyValueItems")]
        IList<KeyValueItem> GetKeyValueItems(decimal workflowId);

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowMapInfo 对象</returns>
        [OperationContract(Name = "GetCustomWorkflowMapInfo")]
        CustomWorkflowMapInfo GetCustomWorkflowMapInfo(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入记录
        /// </summary>
        /// <param name="customWorkflowMapInfos">customWorkflowMapInfo 对象</param>
        [OperationContract(Name = "InsertCustomWorkflowMap")]
        void InsertCustomWorkflowMapInfos(IList<CustomWorkflowMapInfo> customWorkflowMapInfos);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        [OperationContract(Name = "UpdateMapSorting")]
        void UpdateMapSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode);

        /// <summary>
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        [OperationContract(Name = "DeleteCustomWorkflowMap")]
        void DeleteCustomWorkflowMapInfo(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 更新 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <param name="processId"></param>
        /// <param name="customWorkflowMapInfo"></param>
        /// <param name="customWorkflowMapInfo">CustomWorkflowMapInfo 对象</param>
        [OperationContract(Name = "UpdateCustomWorkflowMap")]
        void Update(decimal parentProcessId, decimal processId, CustomWorkflowMapInfo customWorkflowMapInfo);

        /// <summary>
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetProcessMap")]
        DataSet GetProcessMap(decimal workflowId);

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByProcessId")]
        IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId);

        /// <summary>
        /// 获得工作流程的节点类型
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetProcessType")]
        byte GetProcessType(decimal processId);

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertWithUpLoadFileInfos")]
        decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        [OperationContract(Name = "UpdateWithUpLoadFileInfos")]
        void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 获得数据填报编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDatalId")]
        decimal GetDatalId(decimal processId);

        #endregion
    }
}