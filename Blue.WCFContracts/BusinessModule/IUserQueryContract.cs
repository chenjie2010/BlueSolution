//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserQueryContract.cs
// 描述: UserQuery 契约层接口
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// UserQuery 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserQueryContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface IUserQueryContract : ICommonNodeContract, IPrincipalContracts<UserQueryInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 向 UserQuery 表中插入一条新记录和查询字段集合
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
        /// <returns>自动增加的关键字的值</returns>
        [OperationContract(Name = "InsertUserQueryInfo")]
        decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos);

        /// <summary>
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetQueryAndDataFieldInfos")]
        IList<QueryAndDataFieldInfo> GetQueryAndDataFieldInfos(decimal userQueryId);

        #endregion
    }
}