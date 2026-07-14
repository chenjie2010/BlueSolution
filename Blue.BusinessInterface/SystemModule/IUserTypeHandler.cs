//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserTypeHandler.cs
// 描述：UserType 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.SystemModule;

namespace Blue.BusinessInterface.SystemModule
{
/// <summary>
    /// UserType 接口
    /// </summary>
    public interface IUserTypeHandler: ICommonNodeBusiness, IPrincipalBusiness<UserTypeInfo>
    {
        #region 接口

        /// <summary>
        /// 根据系统条件获得用户类型
        /// </summary>
        /// <param name="isSystemUserType"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(bool isSystemUserType);

        /// <summary>
        /// 获得用户类型数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        int GetUserTypeCount(DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 获得用户类型分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        DataTable GetUserTypeData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime);

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        Dictionary<string, decimal> GetNameAndUserTypeIds();

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        Dictionary<decimal, string> GetUserTypeIdAndNames();

        /// <summary>
        /// 通过用户编号获得管理用户类型节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal userId);

        #endregion
    }
}
