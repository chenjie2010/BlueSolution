//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IBusinessBase.cs
// 描述： 所有业务接口的父接口
// 作者：ChenJie 
// 编写日期：2016/07/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;

namespace AppFramework.Reference.BusinessLibrary
{
    /// <summary>
    /// 所有业务接口的父接口
    /// </summary>
    public interface IBusinessBase<T> where T : class
    {
        #region 接口

        /// <summary>
        /// 更新 T 类的对象
        /// </summary>
        /// <param name="modeInfo">T 类的对象</param>
        void Update(T modeInfo);

        /// <summary>
        /// 获得对象的列表
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>对象列表</returns>
        IList<T> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 获得  表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        int GetTotalCount(IList<WhereConditon> whereConditons);

        #endregion
    }
}
