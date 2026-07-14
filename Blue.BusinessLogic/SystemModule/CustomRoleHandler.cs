//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomRoleHandler.cs
// 描述：CustomRole 业务处理类
// 作者：ChenJie 
// 编写日期：2017/12/22
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomRole.
    /// </summary>
    public class CustomRoleHandler : CommonNodeBusiness, ICustomRoleHandler
    {
        #region 工厂类实例

        private static readonly ICustomRole dalCustomRole = SystemDataAccessFactory.CreateCustomRole();
        private static readonly IRoleAndDataField dalRoleAndDataField = SystemDataAccessFactory.CreateRoleAndDataField();
        private static readonly IRoleAndTable dalRoleAndTable = SystemDataAccessFactory.CreateRoleAndTable();
        private static readonly IRoleAndBusiness dalRoleAndBusiness = SystemDataAccessFactory.CreateRoleAndBusiness();
        private static readonly IRoleAndUser dalRoleAndUser = SystemDataAccessFactory.CreateRoleAndUser();
        private static readonly IRoleAndPrint dalRoleAndPrint = SystemDataAccessFactory.CreateRoleAndPrint();
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomRoleHandler() : base(dalCustomRole)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customrole 表中插入一条新记录
        /// </summary>
        /// <param name="customRoleInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomRoleInfo customRoleInfo)
        {
            //自动增加的关键字的值
            decimal customRoleId = 0;

            // 验证输入
            if (customRoleInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customRoleId = dalCustomRole.Insert(customRoleInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleId;
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        /// <returns> CustomRoleInfo 对象</returns>
        public CustomRoleInfo GetModelInfo(decimal roleId)
        {
            CustomRoleInfo customRoleInfo = null;

            // 验证输入
            if (roleId < 0)
            {
                return null;
            }

            try
            {
                customRoleInfo = dalCustomRole.GetModelInfo(roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleInfo;
        }

        /// <summary>
        /// 更新 CustomRoleInfo 对象
        /// </summary>
        /// <param name="customRoleInfo">CustomRoleInfo 对象</param>
        public void Update(CustomRoleInfo customRoleInfo)
        {
            // 验证输入
            if (customRoleInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomRole.Update(customRoleInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomRoleInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        /// <returns> CustomRoleInfo 对象</returns>
        public void Delete(decimal roleId)
        {
            // 验证输入
            if (roleId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomRole.Delete(roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomRoleInfo 对象列表</returns>
        public IList<CustomRoleInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomRoleInfo> customRoleInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customRoleInfos = dalCustomRole.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleInfos;
        }

        /// <summary>
        /// 获得 CustomRole 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomRoleInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomRole.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 根据打印编号获得角色对象列表
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRolesByPrintId(decimal printId)
        {
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalRoleAndPrint.GetRolesByPrintId(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据打印编号获得角色数量
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public int GetRoleCountByPrintId(decimal printId)
        {
            int count = 0;

            try
            {
                count = dalRoleAndPrint.GetTotalCountBySecondForeignKey(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return count;
        }

        /// <summary>
        /// 更新角色的打印范围
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="printIds"></param>
        public void UpdatePrints(decimal roleId, List<decimal> printIds)
        {
            try
            {
                dalRoleAndPrint.UpdatePrints(roleId, printIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得角色的打印对象
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintsByRoleId(decimal roleId)
        {
            List<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalRoleAndPrint.GetPrintsByRoleId(roleId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得用户授权的打印分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrintCategories(decimal userId)
        {
            List<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalRoleAndPrint.GetPrintCategories(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 验证用户是否具有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="printId"></param>
        /// <returns></returns>
        public bool ValidatePrintItem(decimal userId, decimal printId)
        {
            bool result = false;

            try
            {
                result = dalRoleAndPrint.ValidatePrintItem(userId, printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 根据打印分类获得用户授权的打印
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<CommonNode> GetPrints(decimal userId, decimal groupId)
        {
            List<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalRoleAndPrint.GetPrints(userId, groupId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
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
            DataFieldAuthority dataFieldAuthority = DataFieldAuthority.InVisible;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                dataFieldAuthority = dalRoleAndDataField.GetDataFieldAuthority(userId, dataAuthorityType, dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldAuthority;
        }

        /// <summary>
        /// 该业务授权角色数量
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public int GetBusinessCount(decimal businessId)
        {
            int count = 0;

            try
            {
                count = dalRoleAndBusiness.GetTotalCountByFirstForeignKey(businessId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            return count;
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
            IList<CommonNode> commonNodes = new List<CommonNode>();

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                commonNodes = dalCustomRole.GetAuthorizedCommonNodes(userId, nodeId, databaseNodeType, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 校验该用户是否拥有子权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="menuSubAuthority"></param>
        /// <returns></returns>
        public bool ValidateMenuSubAuthority(string userName, MenuSubAuthority menuSubAuthority)
        {
            bool result = false;

            // 验证输入
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("用户名不能为空。");
            }

            try
            {
                result = dalCustomRole.ValidateMenuSubAuthority(userName, menuSubAuthority);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

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
        public void Insert(IList<decimal> userIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            try
            {
                dalRoleAndUser.Insert(userIds, authorityMethod, dataFieldAuthority, authoritiedRoleIds, 
                    ownDepartment, departmentIds, userTypeIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

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
        public void Insert(IList<WhereConditon> whereConditons, IList<decimal> roleIds, AuthorityMethod authorityMethod, Int64 dataFieldAuthority,
            IList<decimal> authoritiedRoleIds, bool ownDepartment, IList<decimal> departmentIds, IList<decimal> userTypeIds)
        {
            try
            {
                dalRoleAndUser.Insert(whereConditons, roleIds, authorityMethod, dataFieldAuthority, authoritiedRoleIds,
                    ownDepartment, departmentIds, userTypeIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表 
        /// </summary>
        /// <param name="isSystemRole">是否是系统角色</param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemRole)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = null;

            try
            {
                commonNodes = dalCustomRole.GetCommonNodes(isSystemRole);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得含授权字段的仓库编号列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public IList<byte> GetDataWarehouseIds(decimal userId, DataAuthorityType dataAuthorityType)
        {
            IList<byte> dataWarehouseIds = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                dataWarehouseIds = dalRoleAndTable.GetDataWarehouseIds(userId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseIds;
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
            Dictionary<DatabaseNodeType, IList<CommonNode>> dicCommonNodes = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                dicCommonNodes = dalRoleAndTable.GetAuthorizedCommonNodes(userId, dataWarehouseId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dicCommonNodes;
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RoleAuthority GetRoleAuthority(decimal userId)
        {
            RoleAuthority roleAuthority = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                roleAuthority = dalCustomRole.GetRoleAuthority(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roleAuthority;
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
            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                dalCustomRole.Update(roleId, menuAuthority, menuSubAuthority, systemAuthority, systemSubAuthority);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            Int64 systemDataFieldAuthority = 0;

            // 验证输入
            if (userId <= 0 || tableId <= 0)
            {
                throw new ArgumentException("用户编号或者表的编号不能小于等于0。");
            }

            try
            {
                systemDataFieldAuthority = dalRoleAndTable.GetSystemDataFieldAuthority(userId, tableId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemDataFieldAuthority;
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
            Int64 tableAuthority = 0;

            // 验证输入
            if (userId <= 0 || tableId <= 0)
            {
                throw new ArgumentException("用户编号或者表的编号不能小于等于0。");
            }

            try
            {
                tableAuthority = dalRoleAndTable.GetTableAuthority(userId, tableId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableAuthority;
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
            List<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = null;

            try
            {
                extendedCustomDataFieldInfos = dalRoleAndTable.GetAuthorizedExtendedCustomDataFieldInfos(userId, tableIds, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return extendedCustomDataFieldInfos;
        }

        /// <summary>
        /// 获得角色属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetRoleProperty(decimal userId)
        {
            Int64 roleProperty = 0;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                roleProperty = dalCustomRole.GetRoleProperty(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roleProperty;
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CustomRoleInfo> GetModelInfos(decimal userId)
        {
            //创建集合对象
            IList<CustomRoleInfo> customRoleInfos = null;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于等于0。");
            }

            try
            {
                customRoleInfos = dalCustomRole.GetModelInfos(userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleInfos;
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
            RoleAndTableInfo roleAndTableInfo = null;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                roleAndTableInfo = dalRoleAndTable.GetModelInfo(roleId, tableId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roleAndTableInfo;
        }

        /// <summary>
        /// 获得角色对应的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataSet GetBusinessAuthority(decimal roleId, decimal menuId)
        {
            DataSet ds = null;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                ds = dalRoleAndBusiness.GetBusinessAuthority(roleId, menuId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            DataSet ds = null;

            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                ds = dalRoleAndDataField.GetDataFiledAuthority(roleId, tableId, dataAuthorityType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 更新角色的业务权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleAndBusinessInfos"></param>
        public void Update(decimal roleId, IList<RoleAndBusinessInfo> roleAndBusinessInfos)
        {
            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                dalRoleAndBusiness.Update(roleId, roleAndBusinessInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (roleId <= 0)
            {
                throw new ArgumentException("角色编号不能小于等于0。");
            }

            try
            {
                dalCustomRole.Update(roleId, tableId, dataAuthorityType, tableAuthority, systemDataFieldAuthority, roleAndDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据角色名称查角色编号
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色编号</returns>
        public decimal GetRoleIdByRoleName(string roleName)
        {
            decimal roleId = decimal.MinValue;

            try
            {
                roleId = dalCustomRole.GetRoleIdByRoleName(roleName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roleId;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
