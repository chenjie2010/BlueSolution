//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserConfigHandler.cs
// 描述: UserConfig 业务处理类
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.UserModule;

namespace Blue.BusinessInterface.UserModule
{
    /// <summary>
    /// UserConfig 接口
    /// </summary>
    public interface IUserConfigHandler : IDependentBusiness<UserConfigInfo>
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