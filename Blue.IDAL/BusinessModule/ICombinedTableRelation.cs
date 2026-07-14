//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICombinedTableRelation.cs
// 描述: CombinedTableRelation 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/8/15
// Copyright 2018
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
    /// CombinedTableRelation 接口
    /// </summary>
    public interface ICombinedTableRelation : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得不同类型的表的数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataTableType"></param>
        /// <returns></returns>
        int GetTableCountByTableType(decimal combinedTableId, DataTableType dataTableType);

        /// <summary>
        /// 获得组合表列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        IList<CombinedTableRelationInfo> GetModelInfos(decimal combinedTableId);

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        IList<CommonNode> GetTables(decimal combinedTableId);

        #endregion
    }
}