//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICellStyle.cs
// 描述: CellStyle 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.IDAL.BusinessDesignerModule
{
    /// <summary>
    /// CellStyle 接口
    /// </summary>
    public interface ICellStyle : IPrincipalTable<CellStyleInfo>
    {
        #region 接口

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        IList<CellStyleInfo> GetModelInfos(decimal cellId);

        /// <summary>
        /// 获得 CellStyleInfo 对象的列表
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        IList<CellStyleInfo> GetModelInfos(decimal cellId, CellCondition cellCondition);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="cellCondition"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal cellId, CellCondition cellCondition);

        #endregion
    }
}