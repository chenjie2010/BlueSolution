//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomInterface.cs
// 描述: CustomInterface 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// CustomInterface 接口
    /// </summary>
    public interface ICustomInterface : ICommonNode, IPrincipalTable<CustomInterfaceInfo>
    {
        #region 接口

        /// <summary>
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceIdentifier">标识符编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		CustomInterfaceInfo GetModelInfo(string interfaceIdentifier);

        /// <summary>
        /// 标识符是否已经存在
        /// </summary>
        /// <param name="interfaceIdentifier"></param>
        /// <returns></returns>
        bool IsExistedIdentifier(string interfaceIdentifier);

        /// <summary>
        /// 更新条件
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        void UpdateConditions(decimal interfaceId, IList<decimal> userTypeIds, IList<decimal> departmentIds);

        #endregion
    }
}