//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomExpressionContract.cs
// 描述： CustomExpression 契约层接口
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomExpression 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomExpressionContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface ICustomExpressionContract
    {
        #region 自定义接口

        /// <summary>
        /// 获得表达式相关的字段节点列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodes")]
        IList<CommonNode> GetCommonNodes(decimal parentDataFieldId);
        
        /// <summary>
        /// 向 CustomExpression 表中插入一条新记录
        /// </summary>
        /// <param name="customExpressionInfo">customExpressionInfo 对象</param>
        [OperationContract(Name = "Insert")]
        void Insert(CustomExpressionInfo customExpressionInfo);

        /// <summary>
		/// 获得 CustomExpressionInfo 对象
		/// </summary>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="sorting">排序</param>
		/// <returns> CustomExpressionInfo 对象</returns>
        [OperationContract(Name = "GetModelInfo")]
        CustomExpressionInfo GetModelInfo(decimal parentDataFieldId, int sorting);

        /// <summary>
        ///  删除 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        [OperationContract(Name = "Delete")]
        void Delete(decimal parentDataFieldId, int sorting);

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfos")]
        IList<CustomExpressionInfo> GetModelInfos(decimal parentDataFieldId);

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        [OperationContract(Name = "GetTotalCount")]
        int GetTotalCount(decimal parentDataFieldId);

        #endregion
    }
}