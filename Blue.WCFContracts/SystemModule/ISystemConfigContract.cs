//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ISystemConfigContract.cs
// 描述： SystemConfig 契约层接口
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// SystemConfig 契约接口
    /// </summary>
    [ServiceContract(Name = "ISystemConfigContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface ISystemConfigContract : IContractsBase<SystemConfigInfo>
    {
        #region 自定义接口
        
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        [OperationContract(Name = "TestConnectionByUserName")]
        bool TestConnection(string userName, string password);

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="systemConfigInfos"></param>
        [OperationContract(Name = "UpdateSystemConfigInfos")]
        void UpdateSystemConfigInfos(Dictionary<int, SystemConfigInfo> systemConfigInfos);

        /// <summary>
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetSystemConfigInfos")]
        Dictionary<int, SystemConfigInfo> GetSystemConfigInfos();

        /// <summary>
        /// 插入 SystemConfigInfo 对象
        /// </summary>
        /// <param name="modeInfo"> SystemConfigInfo 对象</param>
        [OperationContract(Name = "Insert")]
        void Insert(SystemConfigInfo modeInfo);

        /// <summary>
        /// 获得 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigKeyName">SystemConfigInfo 对象的编号</param>
        /// <returns> T 类的对象</returns>
        [OperationContract(Name = "GetModelInfo")]
        SystemConfigInfo GetModelInfo(SystemConfigKeyName systemConfigKeyName);

        /// <summary>
        /// 根据编号删除 SystemConfigInfo 对象
        /// </summary>
        /// <param name="modeId">T 类对象的编号</param>
        [OperationContract(Name = "Delete")]
        void Delete(int modeId);

        /// <summary>
        /// 通过系统关键字查询对应的值
        /// </summary>
        /// <param name="systemConfigKeyName"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSystemConfigValue")]
        string GetSystemConfigValue(SystemConfigKeyName systemConfigKeyName);

        #endregion
    }
}