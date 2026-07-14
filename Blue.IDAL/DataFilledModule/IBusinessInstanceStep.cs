//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IBusinessInstanceStep.cs
// 描述：BusinessInstanceStep 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/2/17
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.DataFilledModule;

namespace Blue.IDAL.DataFilledModule
{
    /// <summary>
    /// BusinessInstanceStep 接口
    /// </summary>
    public interface IBusinessInstanceStep
    {
        #region 接口

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal instanceId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人编号</returns>
        decimal GetLastestReviewerId(decimal instanceId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        string GetLastestComment(decimal instanceId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核操作
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核操作</returns>
        ReviewedAction GetLastestReviewedAction(decimal instanceId);

        /// <summary>
        /// 向 BusinessInstanceStep 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceStepInfo">businessInstanceStepInfo 对象</param>        
        void Insert(BusinessInstanceStepInfo businessInstanceStepInfo);

        #endregion
    }
}