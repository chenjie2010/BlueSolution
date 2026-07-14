//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IRoleAndDataField.cs
// 描述：RoleAndDataField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/12/22
// Copyright 2017
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
    /// RoleAndDataField 接口
    /// </summary>
    public interface IRoleAndDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得字段的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionType"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        DataFieldAuthority GetDataFieldAuthority(decimal userId, byte dataAuthorityType, decimal dataFieldId);

        /// <summary>
        /// 向 RoleAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="roleAndDataFieldInfo">roleAndDataFieldInfo 对象</param>
        void Insert(RoleAndDataFieldInfo roleAndDataFieldInfo);

        /// <summary>
		/// 获得 RoleAndDataFieldInfo 对象
		/// </summary>
		///<param name="roleId">角色编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> RoleAndDataFieldInfo 对象</returns>
		RoleAndDataFieldInfo GetModelInfo(decimal roleId, decimal dataFieldId);

        /// <summary>
        /// 更新 RoleAndDataFieldInfo 对象
        /// </summary>
        /// <param name="roleAndDataFieldInfo">RoleAndDataFieldInfo 对象</param>
        void Update(RoleAndDataFieldInfo roleAndDataFieldInfo);

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        DataSet GetDataFiledAuthority(decimal roleId, decimal tableId, byte dataAuthorityType);

        #endregion
    }
}