//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IWorkflowProcessAndDataField.cs
// 描述：WorkflowProcessAndDataField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/12/7
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
    /// WorkflowProcessAndDataField 接口
    /// </summary>
    public interface IWorkflowProcessAndDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        void UpdateSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 向 WorkflowProcessAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo">workflowProcessAndDataFieldInfo 对象</param>
        void Insert(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
		/// 获得 WorkflowProcessAndDataFieldInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
		WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId);

        /// <summary>
        /// 更新 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo">WorkflowProcessAndDataFieldInfo 对象</param>
        void Update(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

        /// <summary>
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        void Delete(decimal processId, decimal dataFieldId);

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal processId);

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId);

        #endregion
    }
}