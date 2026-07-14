//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IDataFieldRelation.cs
// 描述：DataFieldRelation 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/4/8
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
    /// DataFieldRelation 接口
    /// </summary>
    public interface IDataFieldRelationship : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 更新联系字段
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="dataFieldRelationshipInfos"></param>
        void UpdateDataFields(decimal parentDataFieldId, IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos);

        /// <summary>
        /// 获得字段
        /// 节点的父编号为视图编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRelationDataFields(decimal parentDataFieldId);

        /// <summary>
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRelationDataFieldsWithFullName(decimal parentDataFieldId);

        #endregion
    }
}