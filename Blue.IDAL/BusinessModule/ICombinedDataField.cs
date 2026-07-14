//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICombinedDataField.cs
// 描述: CombinedDataField 数据访问层接口
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
    /// CombinedDataField 接口
    /// </summary>
    public interface ICombinedDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos);

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        List<CommonNode> GetDataFields(decimal combinedTableId);

        #endregion
    }
}