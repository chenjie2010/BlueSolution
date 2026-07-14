//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessInstanceHandler.cs
// 描述：BusinessInstance 业务处理类
// 作者：ChenJie 
// 编写日期：2018/2/17
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.DataFilledModule;
using Blue.Model.DataFilledModule;
using Blue.BusinessInterface.DataFilledModule;

namespace Blue.BusinessLogic.DataFilledModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.BusinessInstance.
    /// </summary>
    public class BusinessInstanceHandler : IBusinessInstanceHandler
    {
        #region 工厂类实例

        private static readonly IBusinessInstance dalBusinessInstance = DataFilledModuleDataAccessFactory.CreateBusinessInstance();
        private static readonly IBusinessInstanceStep dalBusinessInstanceStep = DataFilledModuleDataAccessFactory.CreateBusinessInstanceStep();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BusinessInstanceHandler()
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 businessinstance 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <returns></returns>
        public decimal Insert(BusinessInstanceInfo businessInstanceInfo)
        {
            //自动增加的关键字的值
            decimal businessInstanceId = 0;

            // 验证输入
            if (businessInstanceInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                businessInstanceId = dalBusinessInstance.Insert(businessInstanceInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceId;
        }

        /// <summary>
        /// 获得 BusinessInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        /// <returns> BusinessInstanceInfo 对象</returns>
        public BusinessInstanceInfo GetModelInfo(decimal instanceId)
        {
            BusinessInstanceInfo businessInstanceInfo = null;

            // 验证输入
            if (instanceId < 0)
            {
                return null;
            }

            try
            {
                businessInstanceInfo = dalBusinessInstance.GetModelInfo(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceInfo;
        }

        /// <summary>
        /// 更新 BusinessInstanceInfo 对象
        /// </summary>
        /// <param name="businessInstanceInfo">BusinessInstanceInfo 对象</param>
        public void Update(BusinessInstanceInfo businessInstanceInfo)
        {
            // 验证输入
            if (businessInstanceInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalBusinessInstance.Update(businessInstanceInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 BusinessInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        /// <returns> BusinessInstanceInfo 对象</returns>
        public void Delete(decimal instanceId)
        {
            // 验证输入
            if (instanceId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalBusinessInstance.Delete(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 BusinessInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 对象列表</returns>
        public IList<BusinessInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<BusinessInstanceInfo> businessInstanceInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                businessInstanceInfos = dalBusinessInstance.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceInfos;
        }

        /// <summary>
        /// 获得 BusinessInstance 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalBusinessInstance.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet ds = null;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }
            try
            {
                ds = dalBusinessInstanceStep.GetPageRecord(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 终止业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool AbortBussinessInstance(decimal userId, decimal instanceId)
        {
            bool success = false;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }
            try
            {
                success = dalBusinessInstance.AbortBussinessInstance(userId, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return success;
        }

        /// <summary>
        /// 撤回提交的业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool WithDrawBussinessInstance(decimal userId, decimal instanceId)
        {
            bool success = false;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }
            try
            {
                success = dalBusinessInstance.WithDrawBussinessInstance(userId,instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return success;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人编号</returns>
        public decimal GetLastestReviewerId(decimal instanceId)
        {
            decimal userId = decimal.MinValue;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }

            try
            {
                userId = dalBusinessInstanceStep.GetLastestReviewerId(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核操作
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核操作</returns>
        public ReviewedAction GetLastestReviewedAction(decimal instanceId)
        {
            ReviewedAction reviewedAction = ReviewedAction.None;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }

            try
            {
                reviewedAction = dalBusinessInstanceStep.GetLastestReviewedAction(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewedAction;

        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            string comment = string.Empty;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }

            try
            {
                comment = dalBusinessInstanceStep.GetLastestComment(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return comment;
        }

        /// <summary>
        /// 根据当前实例编号，获得当前实例状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        public InstanceState GetInstanceState(decimal instanceId)
        {
            InstanceState instanceState = 0;
            
            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数不能小于或等于0");
            }

            try
            {
                instanceState = dalBusinessInstance.GetInstanceState(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceState;
        }

        /// <summary>
        /// 向 businessinstancestep 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceStepInfo"></param>
        public void Insert(BusinessInstanceStepInfo businessInstanceStepInfo)
        {

            // 验证输入
            if (businessInstanceStepInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dalBusinessInstanceStep.Insert(businessInstanceStepInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
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
            DataSet ds = null;

            try
            {
                ds = dalBusinessInstance.GetBusinessInstanceUnaudited(startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            DataSet ds = null;

            try
            {
                ds = dalBusinessInstance.GetBusinessInstanceAudited(startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }


        /// <summary>
        /// 获得填报实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<BusinessInstanceInfo> GetModelInfos(decimal userId, decimal dataId)
        {
            IList<BusinessInstanceInfo> businessInstanceInfos = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号或不能小于或是等于0。");
            }


            try
            {
                businessInstanceInfos = dalBusinessInstance.GetModelInfos(userId, dataId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceInfos;
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
            DataSet ds = null;

            try
            {
                ds = dalBusinessInstance.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        public int GetBusinessInstanceCount(decimal userId, DataSumbittedState dataSumbittedState)
        {
            int count = 0;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }

            try
            {
                count = dalBusinessInstance.GetBusinessInstanceCount(userId, dataSumbittedState);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return count;
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
            int count = 0;

            // 验证输入
            if (userId <= 0 || dataId <= 0)
            {
                throw new ArgumentException("用户编号或是业务编号不能小于或是等于0。");
            }

            try
            {
                count = dalBusinessInstance.GetBusinessInstanceCount(userId, dataId, dataSumbittedState);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
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
            InstanceSet instanceSet = null;

            // 验证输入
            if (businessInstanceInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                if (instanceState == InstanceState.None)
                {
                    businessInstanceInfo.TimeModified = DateTime.Now;
                    businessInstanceInfo.TimeSumbitted = DateTime.MinValue;
                }
                instanceSet = dalBusinessInstance.Process(businessInstanceInfo, businessInstanceStepInfo, dicRecordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceSet;
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
            InstanceItem instanceItem = null;

            // 验证输入
            if (businessInstanceInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                if (instanceState == InstanceState.None)
                {
                    businessInstanceInfo.TimeModified = DateTime.Now;
                    businessInstanceInfo.TimeSumbitted = DateTime.MinValue;
                }
                instanceItem = dalBusinessInstance.Process(businessInstanceInfo, businessInstanceStepInfo, recordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceItem;
        }

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            byte[] data = null;

            // 验证输入
            if (string.IsNullOrWhiteSpace(dataFieldName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("字段名称或者文件名不能为空。");
            }

            try
            {
                data = dalBusinessInstance.GetFileData(dataFieldName, fileName);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }


            return data;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
