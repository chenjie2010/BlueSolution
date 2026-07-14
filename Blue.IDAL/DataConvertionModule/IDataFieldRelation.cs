//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataFieldRelation.cs
// 描述: DataFieldRelation 数据访问层接口
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
using Blue.Model.DataConvertionModule;

namespace Blue.IDAL.DataConvertionModule
{
    /// <summary>
    /// DataFieldRelation 接口
    /// </summary>
    public interface IDataFieldRelation : IDualDependentTable<DataFieldRelationInfo>
    {
        #region 接口

        /// <summary>
        /// 数据转表
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="whereConditons"></param>
        void Import(decimal relationId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        IList<DataFieldRelationInfo> GetModelInfos(decimal relationId);
        
        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="keyValueItems"></param>
        void UpdateDataFieldRelation(decimal relationId, List<KeyValueItem> keyValueItems);
        
        /// <summary>
        /// 获得表的对应关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        Dictionary<decimal, decimal> GetTableRelation(decimal relationId);

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        Dictionary<decimal, decimal> GetDataFieldRelation(decimal relationId, decimal sourceTableId, decimal destinationTableId);

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        Dictionary<decimal, decimal> GetDataFieldRelationByAttachment(decimal relationId, decimal sourceTableId, decimal destinationTableId);

        #endregion
    }
}