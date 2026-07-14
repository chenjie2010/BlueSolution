//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomWorkflowMap.cs
// 描述：CustomWorkflowMap 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/4/23
// Copyright 2018
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
    /// CustomWorkflowMap 接口
    /// </summary>
    public interface ICustomWorkflowMap : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得 CustomWorkflowMap 表中记录的数目
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        int GetTotalCountByProcessId(decimal processId);

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入多条记录
        /// </summary>
        /// <param name="customWorkflowMapInfos">customWorkflowMapInfos 对象列表</param>
        void Insert(IList<CustomWorkflowMapInfo> customWorkflowMapInfos);

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
        CustomWorkflowMapInfo GetModelInfo(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        void UpdateSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        void Insert(CustomWorkflowMapInfo customWorkflowMapInfo);

        /// <summary>
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        void Delete(decimal parentProcessId, decimal processId);

        /// <summary>
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
       DataSet GetProcessMap(decimal workflowId);

        /// <summary>
        /// 更新 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <param name="processId"></param>
        /// <param name="customWorkflowMapInfo"></param>
        /// <param name="customWorkflowMapInfo">CustomWorkflowMapInfo 对象</param>
        void Update(decimal parentProcessId, decimal processId, CustomWorkflowMapInfo customWorkflowMapInfo);

        #endregion
    }
}