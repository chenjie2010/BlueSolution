//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataRelationHandler.cs
// 描述: DataRelation 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.DataConvertionModule;

namespace Blue.BusinessInterface.DataConvertionModule
{
    /// <summary>
    /// DataRelation 接口
    /// </summary>
    public interface IDataRelationHandler : ICommonNodeBusiness, IPrincipalBusiness<DataRelationInfo>
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
        IList<DataFieldRelationInfo> GetModelInfosByRelationId(decimal relationId);

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

        #endregion
    }
}