//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomQueyAndDataField.cs
// 描述：CustomQueyAndDataField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/31
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
    /// CustomQueyAndDataField 接口
    /// </summary>
    public interface ICustomQueyAndDataField : ICorrelatedTable
    {
        #region 接口  

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        IList<CustomDataFieldInfo> GetCustomDataFieldInfos(decimal dataQueriedId);

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        IList<CommonNode> GetDigitDataFields(decimal dataQueriedId);

        /// <summary>
        /// 获取查询条件字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        IList<CustomDataFieldInfo> GetConditionalCustomDataFieldInfos(decimal dataQueriedId);

        /// <summary>
        /// 获得查询中已关联的字段
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        IList<CommonNode> GetAssociatedDataFields(decimal dataQueriedId);

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        void UpdateSorting(decimal dataQueriedId, decimal dataFieldId, MovedDriection movedDriectionOfNode);

        /// <summary>
        /// 向 CustomQueyAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">customQueyAndDataFieldInfo 对象</param>
        void Insert(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo);

        /// <summary>
		/// 获得 CustomQueyAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyAndDataFieldInfo 对象</returns>
		CustomQueyAndDataFieldInfo GetModelInfo(decimal dataFieldId, decimal dataQueriedId);

        /// <summary>
        /// 更新 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customQueyAndDataFieldInfo">CustomQueyAndDataFieldInfo 对象</param>
        void Update(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo);

        /// <summary>
        ///  删除 CustomQueyAndDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataQueriedId">数据查询编号</param>
        void Delete(decimal dataFieldId, decimal dataQueriedId);

        /// <summary>
        /// 获得 CustomQueyAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
		DataSet GetCustomQueyAndDataFieldInfos(decimal dataQueriedId);

        #endregion
    }
}