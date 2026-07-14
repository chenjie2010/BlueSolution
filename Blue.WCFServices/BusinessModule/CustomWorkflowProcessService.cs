//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessService.cs
// 描述：CustomWorkflowProcess 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomWorkflowProcess.
    /// </summary>
    public class CustomWorkflowProcessService : CommonNodeServices, ICustomWorkflowProcessContract
    {
        #region 业务实例

        private static readonly ICustomWorkflowProcessHandler customWorkflowProcessHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomWorkflowProcessHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowProcessService() : base(customWorkflowProcessHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customworkflowprocess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {
            return customWorkflowProcessHandler.Insert(customWorkflowProcessInfo);
        }

        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowProcessInfo 对象</returns>
        public CustomWorkflowProcessInfo GetModelInfo(decimal processId)
        {
            return customWorkflowProcessHandler.GetModelInfo(processId);
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {
            customWorkflowProcessHandler.Update(customWorkflowProcessInfo);
        }

        /// <summary>
        /// 删除 CustomWorkflowProcessInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowProcessInfo 对象</returns>
        public void Delete(decimal processId)
        {
            customWorkflowProcessHandler.Delete(processId);
        }

        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 对象列表</returns>
        public IList<CustomWorkflowProcessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customWorkflowProcessHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomWorkflowProcess 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customWorkflowProcessHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<decimal> GetGlobalNextReviewers(decimal userId, decimal processId, int top)
        {
            return customWorkflowProcessHandler.GetGlobalNextReviewers(userId,processId, top);
        }

        /// <summary>
        /// 获得单位间动态节点的审核用户
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="depIds"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetNextReviewers(decimal processId, IList<decimal> depIds)
        {
            return customWorkflowProcessHandler.GetNextReviewers(processId, depIds);
        }

        /// <summary>
        /// 获取下一步条件节点的联系人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId, Dictionary<decimal, CommonDataField> commonDataFields)
        {
            return customWorkflowProcessHandler.GetNextReviewers(userId, processId, commonDataFields);
        }

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<decimal> GetNextReviewers(decimal userId, decimal processId, int top)
        {
            return customWorkflowProcessHandler.GetNextReviewers(userId, processId, top);
        }

        /// <summary>
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public long GetProcessSetting(decimal stepId)
        {
            return customWorkflowProcessHandler.GetProcessSetting(stepId);
        }

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public byte GetProcessCategory(decimal stepId)
        {
            return customWorkflowProcessHandler.GetProcessCategory(stepId);
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateWorkflowProcessAndDataFieldSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            customWorkflowProcessHandler.UpdateWorkflowProcessAndDataFieldSorting(processId, dataFieldId, movedDriectionOfNode);
        }

        /// <summary>
        /// 插入工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        public void InsertWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            customWorkflowProcessHandler.InsertWorkflowProcessAndDataFieldInfo(workflowProcessAndDataFieldInfo);
        }

        /// <summary>
        /// 更新工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        public void UpdateWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            customWorkflowProcessHandler.UpdateWorkflowProcessAndDataFieldInfo(workflowProcessAndDataFieldInfo);
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
        public WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId)
        {
            return customWorkflowProcessHandler.GetModelInfo(processId, dataFieldId);
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetPageRecordByProcessId(decimal processId)
        {
            return customWorkflowProcessHandler.GetPageRecordByProcessId(processId);
        }

        /// <summary>
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal processId, decimal dataFieldId)
        {
            customWorkflowProcessHandler.Delete(processId, dataFieldId);
        }

        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal workflowId, byte processCategory)
        {
            return customWorkflowProcessHandler.GetCommonNodes(workflowId, processCategory);
        }

        /// <summary>
        /// 通过角色查看工作流参与的数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int GetWorkflowProcessCountByRoleId(decimal roleId)
        {
            return customWorkflowProcessHandler.GetWorkflowProcessCountByRoleId(roleId);
        }

        /// <summary>
        /// 获得工作流节点编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetWorkflowId(decimal processId)
        {
            return customWorkflowProcessHandler.GetWorkflowId(processId);
        }

        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="processId"></param>
        public void UpdateWorkflowRootNode(decimal processId)
        {
            customWorkflowProcessHandler.UpdateWorkflowRootNode(processId);
        }

        /// <summary>
        /// 获得工作流根节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public CommonNode GetWorkflowRootNode(decimal workflowId)
        {
            return customWorkflowProcessHandler.GetWorkflowRootNode(workflowId);
        }

        /// <summary>
        ///获取下一步处理联系人
        /// </summary>
        /// <param name="userId"></param>
        ///  /// <param name="processId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId)
        {
            return customWorkflowProcessHandler.GetNextReviewers(userId, processId);
        }

        /// <summary>
        /// 根据工作流编号获取节点关系
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IList<KeyValueItem> GetKeyValueItems(decimal workflowId)
        {
            return customWorkflowProcessHandler.GetKeyValueItems(workflowId);
        }

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowMapInfo 对象</returns>
        public CustomWorkflowMapInfo GetCustomWorkflowMapInfo(decimal parentProcessId, decimal processId)
        {
            return customWorkflowProcessHandler.GetCustomWorkflowMapInfo(parentProcessId, processId);
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateMapSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode)
        {
            customWorkflowProcessHandler.UpdateMapSorting(parentProcessId, processId, movedDriectionOfNode);
        }

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入记录
        /// </summary>
        /// <param name="customWorkflowMapInfos">customWorkflowMapInfo 对象</param>
        public void InsertCustomWorkflowMapInfos(IList<CustomWorkflowMapInfo> customWorkflowMapInfos)
        {
            customWorkflowProcessHandler.InsertCustomWorkflowMapInfos(customWorkflowMapInfos);
        }

        /// <summary>
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        public void DeleteCustomWorkflowMapInfo(decimal parentProcessId, decimal processId)
        {
            customWorkflowProcessHandler.DeleteCustomWorkflowMapInfo(parentProcessId, processId);
        }

        /// <summary>
        /// 更新 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <param name="processId"></param>
        /// <param name="customWorkflowMapInfo"></param>
        /// <param name="customWorkflowMapInfo">CustomWorkflowMapInfo 对象</param>
        public void Update(decimal parentProcessId, decimal processId, CustomWorkflowMapInfo customWorkflowMapInfo)
        {
            customWorkflowProcessHandler.Update(parentProcessId, processId, customWorkflowMapInfo);
        }

        /// <summary>
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public DataSet GetProcessMap(decimal workflowId)
        {
            return customWorkflowProcessHandler.GetProcessMap(workflowId);
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId)
        {
            return customWorkflowProcessHandler.GetModelInfos(processId);
        }

        /// <summary>
        /// 获得工作流程的节点类型
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public byte GetProcessType(decimal processId)
        {
            return customWorkflowProcessHandler.GetProcessType(processId);
        }

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            return customWorkflowProcessHandler.Insert(customWorkflowProcessInfo, upLoadFileInfos);
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            customWorkflowProcessHandler.Update(customWorkflowProcessInfo, upLoadFileInfos);
        }

        /// <summary>
        /// 获得数据填报编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetDatalId(decimal processId)
        {
            return customWorkflowProcessHandler.GetDatalId(processId);
        }

        #endregion
    }
}
