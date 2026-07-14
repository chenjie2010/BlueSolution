//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomRole.cs
// 描述：CustomRole 数据访问层接口
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
    /// CustomRole 接口
    /// </summary>
    public interface ICustomRole : ICommonNode, IPrincipalTable<CustomRoleInfo>
    {
        #region 接口

        /// <summary>
        /// 获得下一级子节点
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="nodeId">父节点编号</param>
        /// <param name="databaseNodeType">获得子节点类型</param>
        /// <param name="dataAuthorityType">权限类型</param>
        /// <returns></returns>
        IList<CommonNode> GetAuthorizedCommonNodes(decimal userId, decimal nodeId, DatabaseNodeType databaseNodeType, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 校验该用户是否拥有子权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="menuSubAuthority"></param>
        /// <returns></returns>
        bool ValidateMenuSubAuthority(string userName, MenuSubAuthority menuSubAuthority);

        /// <summary>
        /// 获得 CommonNode 对象的列表 
        /// </summary>
        /// <param name="isSystemRole">是否是系统角色</param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(bool isSystemRole);

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        RoleAuthority GetRoleAuthority(decimal userId);

        /// <summary>
        /// 更新角色的系统权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuAuthority"></param>
        /// <param name="menuSubAuthority"></param>
        /// <param name="systemAuthority"></param>
        /// <param name="systemSubAuthority"></param>
        void Update(decimal roleId, Int64 menuAuthority, Int64 menuSubAuthority, Int64 systemAuthority, Int64 systemSubAuthority);

        /// <summary>
        /// 获得角色属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Int64 GetRoleProperty(decimal userId);

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CustomRoleInfo> GetModelInfos(decimal userId);

        /// <summary>
        /// 更新表的权限信息和字段权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <param name="tableAuthority"></param>
        /// <param name="systemDataFieldAuthority"></param>
        /// <param name="roleAndDataFieldInfos"></param>
        void Update(decimal roleId, decimal tableId, byte dataAuthorityType, Int64 tableAuthority, Int64 systemDataFieldAuthority, IList<RoleAndDataFieldInfo> roleAndDataFieldInfos);

        /// <summary>
        /// 根据角色名称查角色编号
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色编号</returns>
        decimal GetRoleIdByRoleName(string roleName);

        #endregion
    }
}