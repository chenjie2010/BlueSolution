//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessHandler.cs
// 描述：CustomWorkflowProcess 业务处理类
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
    /// 业务层处理类，对于的表： dbo.CustomWorkflowProcess.
    /// </summary>
    public class CustomWorkflowProcessHandler : CommonNodeBusiness, ICustomWorkflowProcessHandler
    {
        #region 工厂类实例

        private static readonly ICustomWorkflowProcess dalCustomWorkflowProcess = BusinessDataAccessFactory.CreateCustomWorkflowProcess();
        private static readonly IWorkflowProcessAndDataField dalWorkflowProcessAndDataField = BusinessDataAccessFactory.CreateWorkflowProcessAndDataField();
        private static readonly ICustomWorkflowMap dalCustomWorkflowMap = BusinessDataAccessFactory.CreateCustomWorkflowMap();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowProcessHandler() : base(dalCustomWorkflowProcess)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customworkflowprocess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {
            //自动增加的关键字的值
            decimal customWorkflowProcessId = 0;

            // 验证输入
            if (customWorkflowProcessInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customWorkflowProcessId = dalCustomWorkflowProcess.Insert(customWorkflowProcessInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowProcessId;
        }

        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowProcessInfo 对象</returns>
        public CustomWorkflowProcessInfo GetModelInfo(decimal processId)
        {
            CustomWorkflowProcessInfo customWorkflowProcessInfo = null;

            // 验证输入
            if (processId < 0)
            {
                return null;
            }

            try
            {
                customWorkflowProcessInfo = dalCustomWorkflowProcess.GetModelInfo(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowProcessInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {
            // 验证输入
            if (customWorkflowProcessInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflowProcess.Update(customWorkflowProcessInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomWorkflowProcessInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowProcessInfo 对象</returns>
        public void Delete(decimal processId)
        {
            // 验证输入
            if (processId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomWorkflowProcess.Delete(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 对象列表</returns>
        public IList<CustomWorkflowProcessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customWorkflowProcessInfos = dalCustomWorkflowProcess.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowProcessInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflowProcess 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomWorkflowProcess.GetTotalCount(whereConditons);
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
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<decimal> GetGlobalNextReviewers(decimal userId, decimal processId, int top)
        {
            IList<decimal> nextReviewers = null;

            // 验证输入
            if (userId <= 0 || processId <= 0)
            {
                throw new ArgumentException("用户编号或者流程节点编号不能小于或是等于0。");
            }

            try
            {
                nextReviewers = dalCustomWorkflowProcess.GetGlobalNextReviewers(userId, processId, top);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nextReviewers;
        }

        /// <summary>
        /// 获得单位间动态节点的审核用户
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="depIds"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetNextReviewers(decimal processId, IList<decimal> depIds)
        {
            Dictionary<decimal, decimal> nextReviewers = null;

            // 验证输入
            if (processId <= 0)
            {
                throw new ArgumentException("流程节点编号不能小于或是等于0。");
            }

            try
            {
                nextReviewers = dalCustomWorkflowProcess.GetNextReviewers(processId, depIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nextReviewers;
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
            IList<decimal> nextReviewers = null;

            // 验证输入
            if (userId <= 0 || processId <= 0)
            {
                throw new ArgumentException("用户编号或者流程节点编号不能小于或是等于0。");
            }

            try
            {
                nextReviewers = dalCustomWorkflowProcess.GetNextReviewers(userId, processId, top);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nextReviewers;
        }

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public byte GetProcessCategory(decimal stepId)
        {
            byte processCategory = 0;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                processCategory = dalCustomWorkflowProcess.GetProcessCategory(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processCategory;
        }


        /// <summary>
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public long GetProcessSetting(decimal stepId)
        {
            long processSetting = 0;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                processSetting = dalCustomWorkflowProcess.GetProcessSetting(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processSetting;
        }


        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateWorkflowProcessAndDataFieldSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            // 验证输入
            if (processId <= 0 || dataFieldId <= 0)
            {
                throw new ArgumentException("工作流编号不能小于或是等于0。");
            }

            try
            {
                dalWorkflowProcessAndDataField.UpdateSorting(processId, dataFieldId, movedDriectionOfNode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal workflowId, byte processCategory)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (workflowId <= 0)
            {
                throw new ArgumentException("工作流编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCustomWorkflowProcess.GetCommonNodes(workflowId, processCategory);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 通过角色查看工作流参与的数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int GetWorkflowProcessCountByRoleId(decimal roleId)
        {
            int count = 0;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomWorkflowProcess.GetWorkflowProcessCountByRoleId(roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得工作流节点编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetWorkflowId(decimal processId)
        {
            decimal getWorkflowId = 0;

            // 验证输入
            if (processId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                getWorkflowId = dalCustomWorkflowProcess.GetWorkflowId(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return getWorkflowId;
        }

        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="processId"></param>
        public void UpdateWorkflowRootNode(decimal processId)
        {
            // 验证输入
            if (processId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomWorkflowProcess.UpdateWorkflowRootNode(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得工作流根节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public CommonNode GetWorkflowRootNode(decimal workflowId)
        {
            CommonNode commonNode = null;

            try
            {
                commonNode = dalCustomWorkflowProcess.GetWorkflowRootNode(workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
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
            Dictionary<decimal, Dictionary<decimal, string>> nextReviewers = null;

            try
            {
                nextReviewers = dalCustomWorkflowProcess.GetNextReviewers(userId, processId, commonDataFields);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            return nextReviewers;
        }

        /// <summary>
        ///获取下一步处理联系人
        /// </summary>
        /// <param name="userId"></param>
        ///  /// <param name="processId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId)
        {
            Dictionary<decimal, Dictionary<decimal, string>> nextReviewers = null;

            try
            {
                nextReviewers = dalCustomWorkflowProcess.GetNextReviewers(userId, processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nextReviewers;
        }

        /// <summary>
        /// 根据工作流编号获取节点关系
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IList<KeyValueItem> GetKeyValueItems(decimal workflowId)
        {
            IList<KeyValueItem> keyValueItems = null;

            try
            {
                keyValueItems = dalCustomWorkflowMap.GetKeyValueItems(workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return keyValueItems;
        }

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        /// <returns> CustomWorkflowMapInfo 对象</returns>
        public CustomWorkflowMapInfo GetCustomWorkflowMapInfo(decimal parentProcessId, decimal processId)
        {
            CustomWorkflowMapInfo customWorkflowMapInfo = null;

            try
            {
                customWorkflowMapInfo = dalCustomWorkflowMap.GetModelInfo(parentProcessId, processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowMapInfo;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateMapSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode)
        {
            // 验证输入
            if (parentProcessId <= 0 || processId <= 0)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflowMap.UpdateSorting(parentProcessId, processId, movedDriectionOfNode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        public void InsertCustomWorkflowMapInfos(IList<CustomWorkflowMapInfo> customWorkflowMapInfos)
        {
            // 验证输入
            if (customWorkflowMapInfos == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }
            try
            {
                dalCustomWorkflowMap.Insert(customWorkflowMapInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }               

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        public void InsertCustomWorkflowMapInfo(CustomWorkflowMapInfo customWorkflowMapInfo)
        {
            // 验证输入
            if (customWorkflowMapInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }
            try
            {
                dalCustomWorkflowMap.Insert(customWorkflowMapInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        public void DeleteCustomWorkflowMapInfo(decimal parentProcessId, decimal processId)
        {
            // 验证输入
            if (parentProcessId  <= 0 || processId <= 0)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            { 
                dalCustomWorkflowMap.Delete(parentProcessId, processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (customWorkflowMapInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflowMap.Update(parentProcessId, processId, customWorkflowMapInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public DataSet GetProcessMap(decimal workflowId)
        {
            DataSet ds = null;
            try
            {
                ds = dalCustomWorkflowMap.GetProcessMap(workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 插入工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        public void InsertWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            try
            {
                 dalWorkflowProcessAndDataField.Insert(workflowProcessAndDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新工作流程与字段关系对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        public void UpdateWorkflowProcessAndDataFieldInfo(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            try
            {
                dalWorkflowProcessAndDataField.Update(workflowProcessAndDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
        public WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId)
        {
            WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo = null;

            try
            {
                workflowProcessAndDataFieldInfo = dalWorkflowProcessAndDataField.GetModelInfo(processId, dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowProcessAndDataFieldInfo;
        }

        /// <summary>
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal processId, decimal dataFieldId)
        {
            try
            {
                dalWorkflowProcessAndDataField.Delete(processId, dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetPageRecordByProcessId(decimal processId)
        {
            DataSet ds = null;

            try
            {
                ds = dalWorkflowProcessAndDataField.GetPageRecord(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId)
        {
            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = null;

            try
            {
                workflowProcessAndDataFieldInfos = dalWorkflowProcessAndDataField.GetModelInfos(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowProcessAndDataFieldInfos;
        }

        /// <summary>
        /// 获得工作流程的节点类型
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public byte GetProcessType(decimal processId)
        {
            byte processType = 0;

            try
            {
                processType = dalCustomWorkflowProcess.GetProcessType(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processType;
        }

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal customWorkflowProcessId = 0;

            // 验证输入
            if (customWorkflowProcessInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customWorkflowProcessId = dalCustomWorkflowProcess.Insert(customWorkflowProcessInfo, upLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowProcessId;
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            // 验证输入
            if (customWorkflowProcessInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomWorkflowProcess.Update(customWorkflowProcessInfo, upLoadFileInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
          
        /// <summary>
        /// 获得数据填报编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetDatalId(decimal processId)
        {
            decimal dataId = decimal.MinValue;

            try
            {
                dataId = dalCustomWorkflowProcess.GetDatalId(processId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataId;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
