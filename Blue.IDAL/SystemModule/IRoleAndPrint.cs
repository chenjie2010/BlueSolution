//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IRoleAndPrint.cs
// 描述: RoleAndPrint 数据访问层接口
// 作者：ChenJie 
// 编写日期：2019/11/17
// Copyright 2019
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
    /// RoleAndPrint 接口
    /// </summary>
    public interface IRoleAndPrint : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 根据打印编号获得角色对象列表
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRolesByPrintId(decimal printId);

        /// <summary>
        /// 更新角色的打印范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="printIds"></param>
        void UpdatePrints(decimal roleId, List<decimal> printIds);

        /// <summary>
        /// 获得角色的打印对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<CommonNode> GetPrintsByRoleId(decimal roleId);

        /// <summary>
        /// 获得用户授权的打印分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<CommonNode> GetPrintCategories(decimal userId);

        /// <summary>
        /// 验证用户是否具有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="printId"></param>
        /// <returns></returns>
        bool ValidatePrintItem(decimal userId, decimal printId);

        /// <summary>
        /// 根据打印分类获得用户授权的打印
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        List<CommonNode> GetPrints(decimal userId, decimal groupId);

        #endregion
    }
}