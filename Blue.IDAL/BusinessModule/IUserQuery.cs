//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserQuery.cs
// 描述: UserQuery 数据访问层接口
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// UserQuery 接口
    /// </summary>
    public interface IUserQuery : ICommonNode, IPrincipalTable<UserQueryInfo>
    {
        #region 接口

        /// <summary>
        /// 向 UserQuery 表中插入一条新记录和查询字段集合
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos);

        #endregion
    }
}