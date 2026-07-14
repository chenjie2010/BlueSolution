//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IRoleAndTable.cs
// 描述：RoleAndTable 数据访问层接口
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
using Blue.Model.BusinessModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// RoleAndTable 接口
    /// </summary>
    public interface IRoleAndTable : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        Int64 GetSystemDataFieldAuthority(decimal userId, decimal tableId, byte dataAuthorityType);

        /// <summary>
        /// 获得含授权字段的仓库编号列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        IList<byte> GetDataWarehouseIds(decimal userId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得授权的数据库、分组和表格等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        Dictionary<DatabaseNodeType, IList<CommonNode>> GetAuthorizedCommonNodes(decimal userId, byte dataWarehouseId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 向 RoleAndTable 表中插入一条新记录
        /// </summary>
        /// <param name="roleAndTableInfo">roleAndTableInfo 对象</param>
        void Insert(RoleAndTableInfo roleAndTableInfo);

        /// <summary>
        /// 更新 RoleAndTableInfo 对象
        /// </summary>
        /// <param name="roleAndTableInfo">RoleAndTableInfo 对象</param>
        void Update(RoleAndTableInfo roleAndTableInfo);

        /// <summary>
        /// 获得表的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name=""></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        Int64 GetTableAuthority(decimal userId, decimal tableId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得 RoleAndTableInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="dataAuthorityType">数据类型</param>
        /// <returns> RoleAndTableInfo 对象</returns>
        RoleAndTableInfo GetModelInfo(decimal roleId, decimal tableId, byte dataAuthorityType);

        /// <summary>
        /// 获得授权的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableIds"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        List<ExtendedCustomDataFieldInfo> GetAuthorizedExtendedCustomDataFieldInfos(decimal userId, IList<decimal> tableIds, DataAuthorityType dataAuthorityType);

        #endregion
    }
}