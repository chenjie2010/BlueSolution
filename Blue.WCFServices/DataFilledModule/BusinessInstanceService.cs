//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessInstanceService.cs
// 描述：BusinessInstance 操作服务类
// 作者：ChenJie 
// 编写日期：2018/2/17
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.Model.DataFilledModule;
using Blue.BusinessInterface.DataFilledModule;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WCFServices.DataFilledModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.BusinessInstance.
    /// </summary>
    public class BusinessInstanceService : IBusinessInstanceContract
    {
        #region 业务实例

        private static readonly IBusinessInstanceHandler businessInstanceHandler = BusinessLogicContainer.Instance.DataFilledModuleContainer.Resolve<IBusinessInstanceHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BusinessInstanceService()
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 businessinstance 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <returns></returns>
        public decimal Insert(BusinessInstanceInfo businessInstanceInfo)
        {
            return businessInstanceHandler.Insert(businessInstanceInfo);
        }

        /// <summary>
        /// 获得 BusinessInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        /// <returns> BusinessInstanceInfo 对象</returns>
        public BusinessInstanceInfo GetModelInfo(decimal instanceId)
        {
            return businessInstanceHandler.GetModelInfo(instanceId);
        }

        /// <summary>
        /// 更新 BusinessInstanceInfo 对象
        /// </summary>
        /// <param name="businessInstanceInfo">BusinessInstanceInfo 对象</param>
        public void Update(BusinessInstanceInfo businessInstanceInfo)
        {
            businessInstanceHandler.Update(businessInstanceInfo);
        }

        /// <summary>
        /// 删除 BusinessInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        /// <returns> BusinessInstanceInfo 对象</returns>
        public void Delete(decimal instanceId)
        {
            businessInstanceHandler.Delete(instanceId);
        }

        /// <summary>
        /// 获得 BusinessInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 对象列表</returns>
        public IList<BusinessInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return businessInstanceHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 BusinessInstance 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return businessInstanceHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口        

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            return businessInstanceHandler.GetPageRecord(instanceId);
        }

        /// <summary>
        /// 终止业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool AbortBussinessInstance(decimal userId, decimal instanceId)
        {
            return businessInstanceHandler.AbortBussinessInstance(userId, instanceId);
        }

        /// <summary>
        /// 撤回提交的业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool WithDrawBussinessInstance(decimal userId, decimal instanceId)
        {
            return businessInstanceHandler.WithDrawBussinessInstance(userId, instanceId);
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人编号</returns>
        public decimal GetLastestReviewerId(decimal instanceId)
        {
            return businessInstanceHandler.GetLastestReviewerId(instanceId);
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            return businessInstanceHandler.GetLastestComment(instanceId);
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核操作
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核操作</returns>
        public ReviewedAction GetLastestReviewedAction(decimal instanceId)
        {
            return businessInstanceHandler.GetLastestReviewedAction(instanceId);
        }

        /// <summary>
        /// 根据当前实例编号，获得当前实例状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        public InstanceState GetInstanceState(decimal instanceId)
        {
            return businessInstanceHandler.GetInstanceState(instanceId);
        }

        /// <summary>
        /// 向 businessinstancestep 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceStepInfo"></param>
        public void Insert(BusinessInstanceStepInfo businessInstanceStepInfo)
        {
            businessInstanceHandler.Insert(businessInstanceStepInfo);
        }

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetBusinessInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return businessInstanceHandler.GetBusinessInstanceAudited(startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetBusinessInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return businessInstanceHandler.GetBusinessInstanceUnaudited(startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 获得填报实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<BusinessInstanceInfo> GetModelInfos(decimal userId, decimal dataId)
        {
            return businessInstanceHandler.GetModelInfos(userId, dataId);
        }

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
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return businessInstanceHandler.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        public int GetBusinessInstanceCount(decimal userId, DataSumbittedState dataSumbittedState)
        {
            return businessInstanceHandler.GetBusinessInstanceCount(userId, dataSumbittedState);
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        public int GetBusinessInstanceCount(decimal userId, decimal dataId, DataSumbittedState dataSumbittedState)
        {
            return businessInstanceHandler.GetBusinessInstanceCount(userId, dataId, dataSumbittedState);
        }

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceSet Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            return businessInstanceHandler.Process(businessInstanceInfo, businessInstanceStepInfo, dicRecordEntities);
        }

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceItem Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, IList<RecordEntity> recordEntities)
        {
            return businessInstanceHandler.Process(businessInstanceInfo, businessInstanceStepInfo, recordEntities);
        }

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            return businessInstanceHandler.GetFileData(dataFieldName, fileName);
        }

        #endregion
    }
}
