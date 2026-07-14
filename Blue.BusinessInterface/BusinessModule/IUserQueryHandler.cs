//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserQueryHandler.cs
// 描述: UserQuery 业务处理类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
    /// <summary>
    /// UserQuery 接口
    /// </summary>
    public interface IUserQueryHandler : ICommonNodeBusiness, IPrincipalBusiness<UserQueryInfo>
    {
        #region 接口

        /// <summary>
        /// 向 UserQuery 表中插入一条新记录和查询字段集合
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos);

        /// <summary>
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        IList<QueryAndDataFieldInfo> GetQueryAndDataFieldInfos(decimal userQueryId);

        #endregion
    }
}