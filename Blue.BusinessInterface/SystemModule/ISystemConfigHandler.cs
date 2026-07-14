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
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.SystemModule;

namespace Blue.BusinessInterface.SystemModule
{
/// <summary>
    /// SystemConfig 接口
    /// </summary>
    public interface ISystemConfigHandler : IBusinessBase<SystemConfigInfo>
    {
        #region 接口

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="systemConfigInfos"></param>
        void UpdateSystemConfigInfos(Dictionary<int, SystemConfigInfo> systemConfigInfos);

        /// <summary>
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        Dictionary<int, SystemConfigInfo> GetSystemConfigInfos();

        /// <summary>
        /// 插入SystemConfigInfo 对象
        /// </summary>
        /// <param name="modeInfo">SystemConfigInfo 对象</param>
        void Insert(SystemConfigInfo modeInfo);

        /// <summary>
        /// 获得 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigKeyName">T 类对象的编号</param>
        /// <returns> SystemConfigInfo 对象</returns>
        SystemConfigInfo GetModelInfo(SystemConfigKeyName systemConfigKeyName);

        /// <summary>
        /// 根据编号删除 T 类的对象
        /// </summary>
        /// <param name="modeId">T 类对象的编号</param>
        void Delete(int modeId);

        /// <summary>
        /// 通过系统关键字查询对应的值
        /// </summary>
        /// <param name="systemConfigKeyName"></param>
        /// <returns></returns>
        string GetSystemConfigValue(SystemConfigKeyName systemConfigKeyName);

        #endregion
    }
}
