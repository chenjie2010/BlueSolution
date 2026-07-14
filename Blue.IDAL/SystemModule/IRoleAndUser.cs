//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IRoleAndUser.cs
// 描述：RoleAndUser 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// RoleAndUser 接口
    /// </summary>
    public interface IRoleAndUser : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds);

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="roleIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        void Insert(IList<WhereConditon> whereConditons, IList<decimal> roleIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds);

        #endregion
    }
}