//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserConfigService.cs
// 描述: UserConfig 操作服务类
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.UserModule;
using Blue.BusinessInterface.UserModule;
using Blue.WCFContracts.UserModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.UserModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserConfig.
    /// </summary>
    public class UserConfigService : IUserConfigContract
    {
        #region 业务实例

        private readonly IUserConfigHandler userConfig = BusinessLogicContainer.Instance.UserModuleContainer.Resolve<IUserConfigHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserConfigService()
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 userconfig 表中插入一条新记录
        /// </summary>
        /// <param name="userConfigInfo"></param>
        public void Insert(UserConfigInfo userConfigInfo)
        {
            userConfig.Insert(userConfigInfo);
        }

        /// <summary>
        /// 获得 UserConfigInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="userConfigName">用户配置名称</param>
        /// <returns> UserConfigInfo 对象</returns>
        public UserConfigInfo GetModelInfo(decimal userId, int userConfigName)
        {
            return userConfig.GetModelInfo(userId, userConfigName);
        }

        /// <summary>
        /// 更新 UserConfigInfo 对象
        /// </summary>
        /// <param name="userConfigInfo">UserConfigInfo 对象</param>
        public void Update(UserConfigInfo userConfigInfo)
        {
            userConfig.Update(userConfigInfo);
        }

        /// <summary>
        /// 删除 UserConfigInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="userConfigName">用户配置名称</param>
        /// <returns> UserConfigInfo 对象</returns>
        public void Delete(decimal userId, int userConfigName)
        {
            userConfig.Delete(userId, userConfigName);
        }

        /// <summary>
        /// 获得 UserConfigInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserConfigInfo 对象列表</returns>
        public IList<UserConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userConfig.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserConfig 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> UserConfigInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userConfig.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得用户的个人信息设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserConfigInfo> GetModelInfos(decimal userId)
        {
            return userConfig.GetModelInfos(userId);
        }

        /// <summary>
        /// 更新用户设置
        /// </summary>
        /// <param name="userConfigInfos"></param>
        public void UpdateUserConfigInfos(Dictionary<int, UserConfigInfo> userConfigInfos)
        {
            userConfig.UpdateUserConfigInfos(userConfigInfos);
        }

        #endregion
    }
}
