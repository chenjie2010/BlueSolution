//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IRoleAndBusiness.cs
// 描述：RoleAndBusiness 数据访问层接口
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
    /// RoleAndBusiness 接口
    /// </summary>
    public interface IRoleAndBusiness : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得角色对应的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        DataSet GetBusinessAuthority(decimal roleId, decimal menuId);

        /// <summary>
        /// 更新角色的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndBusinessInfos"></param>
        void Update(decimal roleId, IList<RoleAndBusinessInfo> roleAndBusinessInfos);

        #endregion
    }
}