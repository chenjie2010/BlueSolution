//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomViewAndDataField.cs
// 描述：CustomViewAndDataField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/13
// Copyright 2017
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
    /// CustomViewAndDataField 接口
    /// </summary>
    public interface ICustomViewAndDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得视图与字段对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CustomViewAndDataFieldInfo> GetModelInfos(decimal viewId);

        /// <summary>
        /// 根据视图的信息获得表的信息
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetDataFieldsByViewId(decimal viewId);

        /// <summary>
        /// 更新视图编号与字段编号的关系
        /// </summary>
        /// <param name="viewId"></param>
        void UpdateDataFields(decimal viewId, IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos);

        #endregion
    }
}