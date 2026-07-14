//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：SystemConfigService.cs
// 描述：SystemConfig 操作服务类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.SystemConfig.
    /// </summary>
    public class SystemConfigService : ISystemConfigContract
    {
        #region 业务实例
        
        private readonly ISystemConfigHandler systemConfigHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ISystemConfigHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public SystemConfigService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 systemconfig 表中插入一条新记录
		/// </summary>
		/// <param name="systemConfigInfo"></param>
		public void Insert(SystemConfigInfo systemConfigInfo)
		{
            systemConfigHandler.Insert(systemConfigInfo);
		}

        /// <summary>
        /// 获得 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigKeyName">系统配置名称</param>
        /// <returns> SystemConfigInfo 对象</returns>
        public SystemConfigInfo GetModelInfo(SystemConfigKeyName systemConfigKeyName)
		{	
            return systemConfigHandler.GetModelInfo(systemConfigKeyName);           
		}		
		
        /// <summary>
		/// 更新 SystemConfigInfo 对象
		/// </summary>
		/// <param name="systemConfigInfo">SystemConfigInfo 对象</param>
		public void Update(SystemConfigInfo systemConfigInfo)
		{	          
            systemConfigHandler.Update(systemConfigInfo);
        }	
  
        /// <summary>
		/// 删除 SystemConfigInfo 对象
		/// </summary>
		///<param name="systemConfigName">系统配置名称</param>
		/// <returns> SystemConfigInfo 对象</returns>
		public void Delete(int systemConfigName)
		{	
            systemConfigHandler.Delete(systemConfigName);
        }
        
        /// <summary>
		/// 获得 SystemConfigInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>SystemConfigInfo 对象列表</returns>
		public IList<SystemConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return systemConfigHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 SystemConfig 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>SystemConfigInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return systemConfigHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="systemConfigInfos"></param>
        public void UpdateSystemConfigInfos(Dictionary<int, SystemConfigInfo> systemConfigInfos)
        {
            systemConfigHandler.UpdateSystemConfigInfos(systemConfigInfos);
        }

        /// <summary>
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, SystemConfigInfo> GetSystemConfigInfos()
        {
            return systemConfigHandler.GetSystemConfigInfos();
        }

        /// <summary>
        /// 通过系统关键字查询对应的值
        /// </summary>
        /// <param name="systemConfigKeyName"></param>
        /// <returns></returns>
        public string GetSystemConfigValue(SystemConfigKeyName systemConfigKeyName)
        {
            return systemConfigHandler.GetSystemConfigValue(systemConfigKeyName);
        }

        /// <summary>
        /// 测试用户名密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TestConnection(string userName, string password)
        {
            bool result = false;

            string userNameConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemoteUserName);
            string passwordConfirmed = systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.RemotePassword);
            if (userName.Equals(userNameConfirmed) && password.Equals(passwordConfirmed))
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
