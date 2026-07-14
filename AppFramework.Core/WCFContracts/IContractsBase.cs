//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICommonContracts.cs
// 描述: ICommonContracts 契约层节点接口
// 作者：ChenJie 
// 编写日期：2011/2/24
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core.CommonClass;

namespace AppFramework.Core.WCFContracts
{
    /// <summary>
    /// 所有契约接口的父接口
    /// </summary>
    [ServiceContract(Name = "IContractsBase", Namespace = "http://www.scu.edu.cn/WCFContracts/")]
    public interface IContractsBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 更新 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        [OperationContract(Name = "Update")]
        void Update(T modeInfo);

        /// <summary>
        /// 获得对象的列表
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>对象列表</returns>
        [OperationContract(Name = "GetModeInfos")]
        IList<T> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 获得  表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        [OperationContract(Name = "GetTotalCount")]
        int GetTotalCount(IList<WhereConditon> whereConditons);

        #endregion
    }
}
