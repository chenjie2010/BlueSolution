//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IUserTypeScope.cs
// 描述：UserTypeScope 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// UserTypeScope 接口
    /// </summary>
    public interface IUserTypeScope: ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 通过用户编号获得管理单位节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal userId);
        
        #endregion
    }
}