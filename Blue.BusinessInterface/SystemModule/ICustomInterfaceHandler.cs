//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomInterfaceHandler.cs
// 描述: CustomInterface 业务处理类
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
using Blue.Model.SystemModule;

namespace Blue.BusinessInterface.SystemModule
{
    /// <summary>
    /// CustomInterface 接口
    /// </summary>
    public interface ICustomInterfaceHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomInterfaceInfo>
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

        /// <summary>
        /// 获得管理的用户类型
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        IList<CommonNode> GetUserTypes(decimal interfaceId);

        /// <summary>
        /// 获得管理的单位
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <returns></returns>
        IList<CommonNode> GetDepartments(decimal interfaceId);

        #endregion
    }
}