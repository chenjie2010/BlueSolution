//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserConfigContract.cs
// 描述: UserConfig 契约层接口
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.UserModule;

namespace Blue.WCFContracts.UserModule
{
    /// <summary>
    /// UserConfig 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserConfigContract", Namespace = "http://www.scu.edu.cn/UserModule/")]
    public interface IUserConfigContract : IDependentContracts<UserConfigInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得用户的个人信息设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByUserId")]
        IList<UserConfigInfo> GetModelInfos(decimal userId);

        /// <summary>
        /// 更新用户设置
        /// </summary>
        /// <param name="userConfigInfos"></param>
        [OperationContract(Name = "UpdateUserConfigInfos")]
        void UpdateUserConfigInfos(Dictionary<int, UserConfigInfo> userConfigInfos);

        #endregion
    }
}