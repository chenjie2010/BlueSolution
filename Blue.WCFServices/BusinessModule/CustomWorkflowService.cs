//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowService.cs
// 描述：CustomWorkflow 操作服务类
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
    /// 操作服务类，对于的表： dbo.CustomWorkflow.
    /// </summary>
    public class CustomWorkflowService : CommonNodeServices, ICustomWorkflowContract
    {
        #region 业务实例

        private static readonly ICustomWorkflowHandler customWorkflowHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomWorkflowHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowService() : base(customWorkflowHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customworkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo)
        {
            return customWorkflowHandler.Insert(customWorkflowInfo);
        }

        /// <summary>
        /// 获得 CustomWorkflowInfo 对象
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        /// <returns> CustomWorkflowInfo 对象</returns>
        public CustomWorkflowInfo GetModelInfo(decimal workflowId)
        {
            return customWorkflowHandler.GetModelInfo(workflowId);
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo)
        {
            customWorkflowHandler.Update(customWorkflowInfo);
        }

        /// <summary>
        /// 删除 CustomWorkflowInfo 对象
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        /// <returns> CustomWorkflowInfo 对象</returns>
        public void Delete(decimal workflowId)
        {
            customWorkflowHandler.Delete(workflowId);
        }

        /// <summary>
        /// 获得 CustomWorkflowInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 对象列表</returns>
        public IList<CustomWorkflowInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customWorkflowHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomWorkflow 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customWorkflowHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 工作流节点作为父节点的数目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public int GetParentNodeCount(decimal workflowId)
        {
            return customWorkflowHandler.GetParentNodeCount(workflowId);
        }

        /// <summary>
        /// 根据工作流实例编号获得工作流对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public CustomWorkflowInfo GetCustomWorkflowInfo(decimal instanceId)
        {
            return customWorkflowHandler.GetCustomWorkflowInfo(instanceId);
        }

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            return customWorkflowHandler.Insert(customWorkflowInfo, upLoadFileInfos);
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            customWorkflowHandler.Update(customWorkflowInfo, upLoadFileInfos);
        }

        #endregion
    }
}
