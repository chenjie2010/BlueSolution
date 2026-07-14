//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingAndDataField.cs
// 描述: DataAuditingAndDataField 数据访问层接口
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
    /// DataAuditingAndDataField 接口
    /// </summary>
    public interface IDataAuditingAndDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        List<CommonNode> GetDataFields(decimal dataAuditingId);

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="dataAuditingAndDataFieldInfos"></param>
        void UpdateDataFields(decimal dataAuditingId, IList<DataAuditingAndDataFieldInfo> dataAuditingAndDataFieldInfos);

        #endregion
    }
}