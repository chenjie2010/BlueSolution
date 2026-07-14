//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ISystemConfig.cs
// 描述：SystemConfig 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// SystemConfig 接口
    /// </summary>
    public interface ISystemConfig : ITableBase<SystemConfigInfo>
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
        /// 插入 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        void Insert(SystemConfigInfo modeInfo);

        /// <summary>
        /// 获得 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigKeyName">T 类对象的编号</param>
        /// <returns>  SystemConfigInfo 对象</returns>
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