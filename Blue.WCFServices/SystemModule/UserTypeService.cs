//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserTypeService.cs
// 描述：UserType 操作服务类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserType.
    /// </summary>
    public class UserTypeService : CommonNodeServices, IUserTypeContract
    {
        #region 业务实例

        private static readonly IUserTypeHandler userTypeHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<IUserTypeHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserTypeService() : base(userTypeHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 usertype 表中插入一条新记录
        /// </summary>
        /// <param name="userTypeInfo"></param>
        /// <returns></returns>
        public decimal Insert(UserTypeInfo userTypeInfo)
        {
            return userTypeHandler.Insert(userTypeInfo);
        }

        /// <summary>
        /// 获得 UserTypeInfo 对象
        /// </summary>
        ///<param name="userTypeId">用户类型编号</param>
        /// <returns> UserTypeInfo 对象</returns>
        public UserTypeInfo GetModelInfo(decimal userTypeId)
        {
            return userTypeHandler.GetModelInfo(userTypeId);
        }

        /// <summary>
        /// 更新 UserTypeInfo 对象
        /// </summary>
        /// <param name="userTypeInfo">UserTypeInfo 对象</param>
        public void Update(UserTypeInfo userTypeInfo)
        {
            userTypeHandler.Update(userTypeInfo);
        }

        /// <summary>
        /// 删除 UserTypeInfo 对象
        /// </summary>
        ///<param name="userTypeId">用户类型编号</param>
        /// <returns> UserTypeInfo 对象</returns>
        public void Delete(decimal userTypeId)
        {
            userTypeHandler.Delete(userTypeId);
        }

        /// <summary>
        /// 获得 UserTypeInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserTypeInfo 对象列表</returns>
        public IList<UserTypeInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userTypeHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserType 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserTypeInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userTypeHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 根据系统条件获得用户类型
        /// </summary>
        /// <param name="isSystemUserType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemUserType)
        {
            return userTypeHandler.GetCommonNodes(isSystemUserType);
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndUserTypeIds()
        {
            return userTypeHandler.GetNameAndUserTypeIds();
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetUserTypeIdAndNames()
        {
            return userTypeHandler.GetUserTypeIdAndNames();
        }

        /// <summary>
        /// 通过用户编号获得管理用户类型节点列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal userId)
        {
            return userTypeHandler.GetCommonNodes(userId);
        }

        #endregion
    }
}
