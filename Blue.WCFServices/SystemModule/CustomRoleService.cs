//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomRoleService.cs
// 描述：CustomRole 操作服务类
// 作者：ChenJie 
// 编写日期：2017/12/22
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomRole.
    /// </summary>
    public class CustomRoleService : CommonNodeServices, ICustomRoleContract
    {
        #region 业务实例

        private static readonly ICustomRoleHandler customRoleHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<ICustomRoleHandler>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomRoleService() : base(customRoleHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customrole 表中插入一条新记录
        /// </summary>
        /// <param name="customRoleInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomRoleInfo customRoleInfo)
        {
            return customRoleHandler.Insert(customRoleInfo);
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        /// <returns> CustomRoleInfo 对象</returns>
        public CustomRoleInfo GetModelInfo(decimal roleId)
        {
            return customRoleHandler.GetModelInfo(roleId);
        }

        /// <summary>
        /// 更新 CustomRoleInfo 对象
        /// </summary>
        /// <param name="customRoleInfo">CustomRoleInfo 对象</param>
        public void Update(CustomRoleInfo customRoleInfo)
        {
            customRoleHandler.Update(customRoleInfo);
        }

        /// <summary>
        /// 删除 CustomRoleInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        /// <returns> CustomRoleInfo 对象</returns>
        public void Delete(decimal roleId)
        {
            customRoleHandler.Delete(roleId);
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomRoleInfo 对象列表</returns>
        public IList<CustomRoleInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customRoleHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomRole 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomRoleInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customRoleHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 根据打印编号获得角色对象列表
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRolesByPrintId(decimal printId)
        {
            return customRoleHandler.GetRolesByPrintId(printId);
        }

        /// <summary>
        /// 根据打印编号获得角色数量
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public int GetRoleCountByPrintId(decimal printId)
        {
            return customRoleHandler.GetRoleCountByPrintId(printId);
        }
        
        /// <summary>
        /// 更新角色的打印范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="printIds"></param>
        public void UpdatePrints(decimal roleId, List<decimal> printIds)
        {
            customRoleHandler.UpdatePrints(roleId, printIds);
        }

        /// <summary>
        /// 获得角色的打印对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintsByRoleId(decimal roleId)
        {
            return customRoleHandler.GetPrintsByRoleId(roleId);
        }

        /// <summary>
        /// 获得用户授权的打印分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintCategories(decimal userId)
        {
            return customRoleHandler.GetPrintCategories(userId);
        }

        /// <summary>
        /// 根据打印分类获得用户授权的打印
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrints(decimal userId, decimal groupId)
        {
            return customRoleHandler.GetPrints(userId, groupId);
        }

        /// <summary>
        /// 获得字段的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionType"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public DataFieldAuthority GetDataFieldAuthority(decimal userId, byte dataAuthorityType, decimal dataFieldId)
        {
            return customRoleHandler.GetDataFieldAuthority(userId, dataAuthorityType, dataFieldId);
        }

        /// <summary>
        /// 获得下一级子节点
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="nodeId">父节点编号</param>
        /// <param name="databaseNodeType">获得子节点类型</param>
        /// <param name="dataAuthorityType">权限类型</param>
        /// <returns></returns>
        public IList<CommonNode> GetAuthorizedCommonNodes(decimal userId, decimal nodeId, DatabaseNodeType databaseNodeType, DataAuthorityType dataAuthorityType)
        {
            return customRoleHandler.GetAuthorizedCommonNodes(userId, nodeId, databaseNodeType, dataAuthorityType);
        }

        /// <summary>
        /// 校验该用户是否拥有子权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="menuSubAuthority"></param>
        /// <returns></returns>
        public bool ValidateMenuSubAuthority(string userName, MenuSubAuthority menuSubAuthority)
        {
            return customRoleHandler.ValidateMenuSubAuthority(userName, menuSubAuthority);
        }

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldPower"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        public void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, long dataFieldPower,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            customRoleHandler.Insert(userIds, authorityMethod, dataFieldPower, authoritiedRoleIds, ownDepartment, departmentIds, userTypeIds);
        }

        /// <summary>
        /// 用户授权
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="roleIds"></param>
        /// <param name="authorityMethod"></param>
        /// <param name="dataFieldPower"></param>
        /// <param name="authoritiedRoleIds"></param>
        /// <param name="ownDepartment"></param>
        /// <param name="departmentIds"></param>
        /// <param name="userTypeIds"></param>
        public void Insert(IList<WhereConditon> whereConditons, IList<decimal> roleIds, AuthorityMethod authorityMethod, long dataFieldPower,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            customRoleHandler.Insert(whereConditons, roleIds, authorityMethod, dataFieldPower,
                authoritiedRoleIds, ownDepartment, departmentIds, userTypeIds);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表 
        /// </summary>
        /// <param name="isSystemRole">是否是系统角色</param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemRole)
        {
            return customRoleHandler.GetCommonNodes(isSystemRole);
        }

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public Int64 GetSystemDataFieldAuthority(decimal userId, decimal tableId, byte dataAuthorityType)
        {
            return customRoleHandler.GetSystemDataFieldAuthority(userId, tableId, dataAuthorityType);
        }

        /// <summary>
        /// 获得含授权字段的仓库编号列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public IList<byte> GetDataWarehouseIds(decimal userId, DataAuthorityType dataAuthorityType)
        {
            return customRoleHandler.GetDataWarehouseIds(userId, dataAuthorityType);
        }

        /// <summary>
        /// 获得授权的数据库、分组和表格等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public Dictionary<DatabaseNodeType, IList<CommonNode>> GetAuthorizedCommonNodes(decimal userId, byte dataWarehouseId, DataAuthorityType dataAuthorityType)
        {
            return customRoleHandler.GetAuthorizedCommonNodes(userId, dataWarehouseId, dataAuthorityType);
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RoleAuthority GetRoleAuthority(decimal userId)
        {
            return customRoleHandler.GetRoleAuthority(userId);
        }

        /// <summary>
        /// 更新角色的系统权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuAuthority"></param>
        /// <param name="menuSubAuthority"></param>
        /// <param name="systemAuthority"></param>
        /// <param name="systemSubAuthority"></param>
        public void Update(decimal roleId, Int64 menuAuthority, Int64 menuSubAuthority, Int64 systemAuthority, Int64 systemSubAuthority)
        {
            customRoleHandler.Update(roleId, menuAuthority, menuSubAuthority, systemAuthority, systemSubAuthority);
        }

        /// <summary>
        /// 获得表的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name=""></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public Int64 GetTableAuthority(decimal userId, decimal tableId, DataAuthorityType dataAuthorityType)
        {
            return customRoleHandler.GetTableAuthority(userId, tableId, dataAuthorityType);
        }

        /// <summary>
        /// 获得授权的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableIds"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public List<ExtendedCustomDataFieldInfo> GetAuthorizedExtendedCustomDataFieldInfos(decimal userId, IList<decimal> tableIds, DataAuthorityType dataAuthorityType)
        {
            return customRoleHandler.GetAuthorizedExtendedCustomDataFieldInfos(userId, tableIds, dataAuthorityType);
        }

        /// <summary>
        /// 获得角色属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetRoleProperty(decimal userId)
        {
            return customRoleHandler.GetRoleProperty(userId);
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CustomRoleInfo> GetModelInfos(decimal userId)
        {
            return customRoleHandler.GetModelInfos(userId);
        }

        /// <summary>
        /// 获得角色表对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public RoleAndTableInfo GetRoleAndTableInfo(decimal roleId, decimal tableId, byte dataAuthorityType)
        {
            return customRoleHandler.GetRoleAndTableInfo(roleId, tableId, dataAuthorityType);
        }

        /// <summary>
        /// 获得角色对应的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataSet GetBusinessAuthority(decimal roleId, decimal menuId)
        {
            return customRoleHandler.GetBusinessAuthority(roleId, menuId);
        }

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public DataSet GetDataFiledAuthority(decimal roleId, decimal tableId, byte dataAuthorityType)
        {
            return customRoleHandler.GetDataFiledAuthority(roleId, tableId, dataAuthorityType);
        }

        /// <summary>
        /// 更新角色的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndBusinessInfos"></param>
        public void Update(decimal roleId, IList<RoleAndBusinessInfo> roleAndBusinessInfos)
        {
            customRoleHandler.Update(roleId, roleAndBusinessInfos);
        }

        /// <summary>
        /// 更新表的权限信息和字段权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <param name="tableAuthority"></param>
        /// <param name="systemDataFieldAuthority"></param>
        /// <param name="roleAndDataFieldInfos"></param>
        public void Update(decimal roleId, decimal tableId, byte dataAuthorityType, Int64 tableAuthority, Int64 systemDataFieldAuthority, IList<RoleAndDataFieldInfo> roleAndDataFieldInfos)
        {
            customRoleHandler.Update(roleId, tableId, dataAuthorityType, tableAuthority, systemDataFieldAuthority, roleAndDataFieldInfos);
        }

        /// <summary>
        /// 根据角色名称查角色编号
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色编号</returns>
        public decimal GetRoleIdByRoleName(string roleName)
        {
            return customRoleHandler.GetRoleIdByRoleName(roleName);
        }

        #endregion
    }
}
