//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowHandler.cs
// 描述：CustomWorkflow 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomWorkflow.
    /// </summary>
    public class CustomWorkflowHandler : CommonNodeBusiness, ICustomWorkflowHandler
    {
        #region 工厂类实例

        private static readonly ICustomWorkflow dalCustomWorkflow = BusinessDataAccessFactory.CreateCustomWorkflow();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowHandler() : base(dalCustomWorkflow)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customworkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo)
        {
            //自动增加的关键字的值
            decimal customWorkflowId = 0;

            // 验证输入
            if (customWorkflowInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customWorkflowId = dalCustomWorkflow.Insert(customWorkflowInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowId;
        }

        /// <summary>
        /// 获得 CustomWorkflowInfo 对象
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        /// <returns> CustomWorkflowInfo 对象</returns>
        public CustomWorkflowInfo GetModelInfo(decimal workflowId)
        {
            CustomWorkflowInfo customWorkflowInfo = null;

            // 验证输入
            if (workflowId < 0)
            {
                return null;
            }

            try
            {
                customWorkflowInfo = dalCustomWorkflow.GetModelInfo(workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo)
        {
            // 验证输入
            if (customWorkflowInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflow.Update(customWorkflowInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomWorkflowInfo 对象
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        /// <returns> CustomWorkflowInfo 对象</returns>
        public void Delete(decimal workflowId)
        {
            // 验证输入
            if (workflowId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomWorkflow.Delete(workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomWorkflowInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 对象列表</returns>
        public IList<CustomWorkflowInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomWorkflowInfo> customWorkflowInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customWorkflowInfos = dalCustomWorkflow.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflow 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomWorkflow.GetTotalCount(whereConditons);
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
        /// 工作流节点作为父节点的数目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public int GetParentNodeCount(decimal workflowId)
        {
            int count = 0;

            // 验证输入
            if (workflowId <= 0)
            {
                throw new ArgumentException("工作流编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomWorkflow.GetParentNodeCount(workflowId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 根据工作流实例编号获得工作流对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public CustomWorkflowInfo GetCustomWorkflowInfo(decimal instanceId)
        {
            CustomWorkflowInfo customWorkflowInfo = null;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                customWorkflowInfo = dalCustomWorkflow.GetCustomWorkflowInfo(instanceId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowInfo;
        }

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal customWorkflowId = 0;

            // 验证输入
            if (customWorkflowInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customWorkflowId = dalCustomWorkflow.Insert(customWorkflowInfo, upLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowId;
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            // 验证输入
            if (customWorkflowInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflow.Update(customWorkflowInfo, upLoadFileInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
