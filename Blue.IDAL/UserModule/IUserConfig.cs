//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserConfig.cs
// 描述: UserConfig 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.UserModule;

namespace Blue.IDAL.UserModule
{
    /// <summary>
    /// UserConfig 接口
    /// </summary>
    public interface IUserConfig: IDependentTable<UserConfigInfo>
    {
        #region 接口

        /// <summary>
        /// 获得用户的个人信息设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<UserConfigInfo> GetModelInfos(decimal userId);

        /// <summary>
        /// 更新用户设置
        /// </summary>
        /// <param name="userConfigInfos"></param>
        void UpdateUserConfigInfos(Dictionary<int, UserConfigInfo> userConfigInfos);

        #endregion
    }
}