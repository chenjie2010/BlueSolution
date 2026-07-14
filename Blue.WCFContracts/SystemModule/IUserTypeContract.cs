//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IUserTypeContract.cs
// 描述： UserType 契约层接口
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
    /// UserType 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserTypeContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface IUserTypeContract : ICommonNodeContract, IPrincipalContracts<UserTypeInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 根据系统条件获得用户类型
        /// </summary>
        /// <param name="isSystemUserType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesBySystemUserType")]
        IList<CommonNode> GetCommonNodes(bool isSystemUserType);

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetNameAndUserTypeIds")]
        Dictionary<string, decimal> GetNameAndUserTypeIds();

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetUserTypeIdAndNames")]
        Dictionary<decimal, string> GetUserTypeIdAndNames();

        /// <summary>
        /// 通过用户编号获得管理用户类型节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByUserId")]
        IList<CommonNode> GetCommonNodes(decimal userId);

        #endregion
    }
}