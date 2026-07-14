//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomExpression.cs
// 描述：CustomExpression 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
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
    /// CustomExpression 接口
    /// </summary>
    public interface ICustomExpression 
    {
        #region 接口

        /// <summary>
        /// 获得表达式相关的字段节点列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal parentDataFieldId);

        /// <summary>
        /// 向 CustomExpression 表中插入一条新记录
        /// </summary>
        /// <param name="customExpressionInfo">customExpressionInfo 对象</param>
        void Insert(CustomExpressionInfo customExpressionInfo);

        /// <summary>
		/// 获得 CustomExpressionInfo 对象
		/// </summary>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="sorting">排序</param>
		/// <returns> CustomExpressionInfo 对象</returns>
		CustomExpressionInfo GetModelInfo(decimal parentDataFieldId, int sorting);

        /// <summary>
        ///  删除 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        void Delete(decimal parentDataFieldId, int sorting);

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        IList<CustomExpressionInfo> GetModelInfos(decimal parentDataFieldId);

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        int GetTotalCount(decimal parentDataFieldId);

        #endregion
    }
}