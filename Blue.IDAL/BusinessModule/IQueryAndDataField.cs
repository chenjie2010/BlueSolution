//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IQueryAndDataField.cs
// 描述: QueryAndDataField 数据访问层接口
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
    /// QueryAndDataField 接口
    /// </summary>
    public interface IQueryAndDataField : IPrincipalTable<QueryAndDataFieldInfo>
    {
        #region 接口

        /// <summary>
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        IList<QueryAndDataFieldInfo> GetModelInfos(decimal userQueryId);

        #endregion
    }
}