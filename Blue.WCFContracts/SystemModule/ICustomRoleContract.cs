//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomRoleContract.cs
// 描述： CustomRole 契约层接口
// 作者：ChenJie 
// 编写日期：2017/12/22
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// CustomRole 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomRoleContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomRoleContract : ICommonNodeContract, IPrincipalContracts<CustomRoleInfo>
    {
        #region 自定义接口


        /// <summary>
        /// 根据打印编号获得角色对象列表
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRolesByPrintId")]
        IList<CommonNode> GetRolesByPrintId(decimal printId);

        /// <summary>
        /// 根据打印编号获得角色数量
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRoleCountByPrintId")]
        int GetRoleCountByPrintId(decimal printId);

        /// <summary>
        /// 更新角色的打印范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="printIds"></param>
        [OperationContract(Name = "UpdatePrints")]
        void UpdatePrints(decimal roleId, List<decimal> printIds);

        /// <summary>
        /// 获得角色的打印对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPrintsByRoleId")]
        List<CommonNode> GetPrintsByRoleId(decimal roleId);

        /// <summary>
        /// 获得用户授权的打印分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPrintCategories")]
        List<CommonNode> GetPrintCategories(decimal userId);

        /// <summary>
        /// 根据打印分类获得用户授权的打印
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPrints")]
        List<CommonNode> GetPrints(decimal userId, decimal groupId);

        /// <summary>
        /// 获得字段的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionType"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFieldAuthority")]
        DataFieldAuthority GetDataFieldAuthority(decimal userId, byte dataAuthorityType, decimal dataFieldId);

        /// <summary>
        /// 获得下一级子节点
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="nodeId">父节点编号</param>
        /// <param name="databaseNodeType">获得子节点类型</param>
        /// <param name="dataAuthorityType">权限类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetAuthorizedNodesByCondition")]
        IList<CommonNode> GetAuthorizedCommonNodes(decimal userId, decimal nodeId, DatabaseNodeType databaseNodeType, DataAuthorityType dataAuthorityType);
                
        /// <summary>
        /// 校验该用户是否拥有子权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="menuSubAuthority"></param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateMenuSubAuthority")]
        bool ValidateMenuSubAuthority(string userName, MenuSubAuthority menuSubAuthority);

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
        [OperationContract(Name = "InsertByUserIds")]
        void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, long dataFieldPower,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds);

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
        [OperationContract(Name = "InsertByWhereConditons")]
        void Insert(IList<WhereConditon> whereConditons, IList<decimal> roleIds, AuthorityMethod authorityMethod, long dataFieldPower,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds);

        /// <summary>
        /// 获得 CommonNode 对象的列表 
        /// </summary>
        /// <param name="isSystemRole">是否是系统角色</param>
        /// <returns></returns>
        [OperationContract(Name = "GetSystemCommonNodes")]
        IList<CommonNode> GetCommonNodes(bool isSystemRole);

        /// <summary>
        /// 获得含授权字段的仓库编号列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataWarehouseIds")]
        IList<byte> GetDataWarehouseIds(decimal userId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得授权的数据库、分组和表格等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAuthorizedCommonNodes")]
        Dictionary<DatabaseNodeType, IList<CommonNode>> GetAuthorizedCommonNodes(decimal userId, byte dataWarehouseId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRoleAuthority")]
        RoleAuthority GetRoleAuthority(decimal userId);

        /// <summary>
        /// 更新角色的系统权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuAuthority"></param>
        /// <param name="menuSubAuthority"></param>
        /// <param name="systemAuthority"></param>
        /// <param name="systemSubAuthority"></param>
        [OperationContract(Name = "UpdateAuthority")]
        void Update(decimal roleId, Int64 menuAuthority, Int64 menuSubAuthority, Int64 systemAuthority, Int64 systemSubAuthority);

        /// <summary>
        /// 获得表的权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name=""></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetTableAuthority")]
        Int64 GetTableAuthority(decimal userId, decimal tableId, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得授权的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableIds"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAuthorizedExtendedCustomDataFieldInfos")]
        List<ExtendedCustomDataFieldInfo> GetAuthorizedExtendedCustomDataFieldInfos(decimal userId, IList<decimal> tableIds, DataAuthorityType dataAuthorityType);

        /// <summary>
        /// 获得角色属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRoleProperty")]
        Int64 GetRoleProperty(decimal userId);

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByUserId")]
        IList<CustomRoleInfo> GetModelInfos(decimal userId);

        /// <summary>
        /// 获得角色表对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRoleAndTableInfo")]
        RoleAndTableInfo GetRoleAndTableInfo(decimal roleId, decimal tableId, byte dataAuthorityType);

        /// <summary>
        /// 获得角色对应的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetBusinessAuthority")]
        DataSet GetBusinessAuthority(decimal roleId, decimal menuId);

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSystemDataFieldAuthority")]
        Int64 GetSystemDataFieldAuthority(decimal userId, decimal tableId, byte dataAuthorityType);

        /// <summary>
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFiledAuthority")]
        DataSet GetDataFiledAuthority(decimal roleId, decimal tableId, byte dataAuthorityType);

        /// <summary>
        /// 更新角色的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndBusinessInfos"></param>
        [OperationContract(Name = "UpdateBusinessAuthority")]
        void Update(decimal roleId, IList<RoleAndBusinessInfo> roleAndBusinessInfos);

        /// <summary>
        /// 更新表的权限信息和字段权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <param name="tableAuthority"></param>
        /// <param name="systemDataFieldAuthority"></param>
        /// <param name="roleAndDataFieldInfos"></param>
        [OperationContract(Name = "UpdateRoleAuthority")]
        void Update(decimal roleId, decimal tableId, byte dataAuthorityType, Int64 tableAuthority, Int64 systemDataFieldAuthority, IList<RoleAndDataFieldInfo> roleAndDataFieldInfos);

        /// <summary>
        /// 根据角色名称查角色编号
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色编号</returns>
        [OperationContract(Name = "GetRoleIdByRoleName")]
        decimal GetRoleIdByRoleName(string roleName);

        #endregion
    }
}