//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserConfigHandler.cs
// 描述: UserConfig 业务处理类
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.UserModule;
using Blue.Model.UserModule;
using Blue.BusinessInterface.UserModule;

namespace Blue.BusinessLogic.UserModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserConfig.
    /// </summary>
    public class UserConfigHandler : IUserConfigHandler
    {
        #region 工厂类实例

        private static readonly IUserConfig dalUserConfig = UserDataAccessFactory.CreateUserConfig();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserConfigHandler()
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 userconfig 表中插入一条新记录
        /// </summary>
        /// <param name="userConfigInfo"></param>
        public void Insert(UserConfigInfo userConfigInfo)
        {
            // 验证输入
            if (userConfigInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                dalUserConfig.Insert(userConfigInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 UserConfigInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="userConfigName">用户配置名称</param>
        /// <returns> UserConfigInfo 对象</returns>
        public UserConfigInfo GetModelInfo(decimal userId, int userConfigName)
        {
            UserConfigInfo userConfigInfo = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                userConfigInfo = dalUserConfig.GetModelInfo(userId, userConfigName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userConfigInfo;
        }

        /// <summary>
        /// 更新 UserConfigInfo 对象
        /// </summary>
        /// <param name="userConfigInfo">UserConfigInfo 对象</param>
        public void Update(UserConfigInfo userConfigInfo)
        {
            // 验证输入
            if (userConfigInfo == null)
            {
                throw new ArgumentException("不能更新空对象。");
            }
            try
            {
                dalUserConfig.Update(userConfigInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 UserConfigInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="userConfigName">用户配置名称</param>
        /// <returns> UserConfigInfo 对象</returns>
        public void Delete(decimal userId, int userConfigName)
        {
            // 验证输入
            if (userId < 0)
            {
                throw new ArgumentException("编号错误.");
            }

            try
            {
                dalUserConfig.Delete(userId, userConfigName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 UserConfigInfo  对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserConfigInfo  对象列表</returns>
        public IList<UserConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<UserConfigInfo> userConfigInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                userConfigInfos = dalUserConfig.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userConfigInfos;
        }

        /// <summary>
        /// 获得 CustomSheet 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomSheetInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalUserConfig.GetTotalCount(whereConditons);
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
        /// 获得用户的个人信息设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserConfigInfo> GetModelInfos(decimal userId)
        {
            IList<UserConfigInfo> userConfigInfos = null;

            try
            {
                userConfigInfos = dalUserConfig.GetModelInfos(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userConfigInfos;
        }

        /// <summary>
        /// 更新用户设置
        /// </summary>
        /// <param name="userConfigInfos"></param>
        public void UpdateUserConfigInfos(Dictionary<int, UserConfigInfo> userConfigInfos)
        {
            try
            {
                dalUserConfig.UpdateUserConfigInfos(userConfigInfos);
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
