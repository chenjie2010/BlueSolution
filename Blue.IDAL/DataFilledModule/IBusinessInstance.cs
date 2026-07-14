//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IBusinessInstance.cs
// 描述：BusinessInstance 数据访问层接口
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
    /// BusinessInstance 接口
    /// </summary>
    public interface IBusinessInstance : IPrincipalTable<BusinessInstanceInfo>
    {
        #region 接口

        /// <summary>
        /// 终止业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        bool AbortBussinessInstance(decimal userId, decimal instanceId);

        /// <summary>
        /// 撤回提交的业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        bool WithDrawBussinessInstance(decimal userId, decimal instanceId);

        /// <summary>
        /// 根据当前实例编号，获得当前实例状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        InstanceState GetInstanceState(decimal instanceId);

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetBusinessInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetBusinessInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得填报实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        IList<BusinessInstanceInfo> GetModelInfos(decimal userId, decimal dataId);

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        int GetBusinessInstanceCount(decimal userId, DataSumbittedState dataSumbittedState);

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        int GetBusinessInstanceCount(decimal userId, decimal dataId, DataSumbittedState dataSumbittedState);

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        InstanceSet Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        InstanceItem Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        byte[] GetFileData(string dataFieldName, string fileName);

        #endregion
    }
}