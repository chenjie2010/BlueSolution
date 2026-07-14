//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：SystemConfigHandler.cs
// 描述：SystemConfig 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.SystemConfig.
    /// </summary>
    public class SystemConfigHandler : ISystemConfigHandler
    {
        #region 工厂类实例

        private static readonly ISystemConfig dalSystemConfig = SystemDataAccessFactory.CreateSystemConfig();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public SystemConfigHandler()
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 systemconfig 表中插入一条新记录
        /// </summary>
        /// <param name="systemConfigInfo"></param>
        public void Insert(SystemConfigInfo systemConfigInfo)
        {
            // 验证输入
            if (systemConfigInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dalSystemConfig.Insert(systemConfigInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

        }

        /// <summary>
        /// 获得 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigName">系统配置名称</param>
        /// <returns> SystemConfigInfo 对象</returns>
        public SystemConfigInfo GetModelInfo(SystemConfigKeyName systemConfigName)
        {
            SystemConfigInfo systemConfigInfo = null;

            // 验证输入
            if (systemConfigName < 0)
            {
                return null;
            }

            try
            {
                systemConfigInfo = dalSystemConfig.GetModelInfo(systemConfigName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemConfigInfo;
        }

        /// <summary>
        /// 更新 SystemConfigInfo 对象
        /// </summary>
        /// <param name="systemConfigInfo">SystemConfigInfo 对象</param>
        public void Update(SystemConfigInfo systemConfigInfo)
        {
            // 验证输入
            if (systemConfigInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalSystemConfig.Update(systemConfigInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigName">系统配置名称</param>
        /// <returns> SystemConfigInfo 对象</returns>
        public void Delete(int systemConfigName)
        {
            // 验证输入
            if (systemConfigName < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalSystemConfig.Delete(systemConfigName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 SystemConfigInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>SystemConfigInfo 对象列表</returns>
        public IList<SystemConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<SystemConfigInfo> systemConfigInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                systemConfigInfos = dalSystemConfig.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemConfigInfos;
        }

        /// <summary>
        /// 获得 SystemConfig 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>SystemConfigInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalSystemConfig.GetTotalCount(whereConditons);
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
        /// 更新系统设置
        /// </summary>
        /// <param name="systemConfigInfos"></param>
        public void UpdateSystemConfigInfos(Dictionary<int, SystemConfigInfo> systemConfigInfos)
        {
            try
            {
                dalSystemConfig.UpdateSystemConfigInfos(systemConfigInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, SystemConfigInfo> GetSystemConfigInfos()
        {
            Dictionary<int, SystemConfigInfo> systemConfigInfos = null;

            try
            {
                systemConfigInfos = dalSystemConfig.GetSystemConfigInfos();

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemConfigInfos;
        }

        /// <summary>
        /// 通过系统关键字查询对应的值
        /// </summary>
        /// <param name="systemConfigKeyName"></param>
        /// <returns></returns>
        public string GetSystemConfigValue(SystemConfigKeyName systemConfigKeyName)
        {
            return dalSystemConfig.GetSystemConfigValue(systemConfigKeyName);
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
