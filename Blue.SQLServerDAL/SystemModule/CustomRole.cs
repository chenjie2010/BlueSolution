//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomRole.cs
// 描述：CustomRole 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/12/22
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// CustomRole 表的数据层访问类
    /// </summary>
    public class CustomRole : CommonNodeDataAccess, ICustomRole
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomRole() : base("CustomRole", "RoleId", "GroupId", "RoleName", "RoleCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomRole 表中插入一条新记录
        /// </summary>
        /// <param name="customRoleInfo">customRoleInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomRoleInfo customRoleInfo)
        {
            //自动增加的关键字的值
            decimal customRoleId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customRoleInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomRole", "Sorting", "GroupId", customRoleInfo.GroupId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomRole(GroupId, RoleName, RoleCode, InitializedDate, ExpiredDate, ");
            sb.Append("RoleProperty, IsSystemRole, MenuAuthority, MenuSubAuthority, SystemAuthority, SystemSubAuthority, IsLockedOut, Sorting, Notes)");
            sb.Append("VALUES (@GroupId, @RoleName, @RoleCode, @InitializedDate, @ExpiredDate, ");
            sb.Append("@RoleProperty, @IsSystemRole, @MenuAuthority, @MenuSubAuthority, @SystemAuthority, @SystemSubAuthority, @IsLockedOut, @Sorting, @Notes);");
            sb.Append("SET @RoleId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "RoleId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customRoleInfo.GroupId);
                        db.AddInParameter(dbCommand, "RoleName", DbType.String, customRoleInfo.RoleName);
                        db.AddInParameter(dbCommand, "RoleCode", DbType.String, customRoleInfo.RoleCode);
                        db.AddInParameter(dbCommand, "InitializedDate", DbType.DateTime, DataConvertionHelper.SetDateTime(customRoleInfo.InitializedDate));
                        db.AddInParameter(dbCommand, "ExpiredDate", DbType.DateTime, DataConvertionHelper.SetDateTime(customRoleInfo.ExpiredDate));
                        db.AddInParameter(dbCommand, "MenuAuthority", DbType.Int64, customRoleInfo.MenuAuthority);
                        db.AddInParameter(dbCommand, "MenuSubAuthority", DbType.Int64, customRoleInfo.MenuSubAuthority);
                        db.AddInParameter(dbCommand, "RoleProperty", DbType.Int64, customRoleInfo.RoleProperty);
                        db.AddInParameter(dbCommand, "IsSystemRole", DbType.Boolean, customRoleInfo.IsSystemRole);
                        db.AddInParameter(dbCommand, "SystemAuthority", DbType.Int64, 0);
                        db.AddInParameter(dbCommand, "SystemSubAuthority", DbType.Int64, 0);
                        db.AddInParameter(dbCommand, "IsLockedOut", DbType.Boolean, customRoleInfo.IsLockedOut);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customRoleInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customRoleInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customRoleId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@RoleId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customRoleInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
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

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", DbType.Decimal, roleId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomRoleInfo> customRoleInfos = GetModeInfos(whereConditons, null, true);
            if (customRoleInfos != null && customRoleInfos.Count > 0)
            {
                customRoleInfo = customRoleInfos[0];
            }

            return customRoleInfo;
        }

        /// <summary>
        /// 更新 CustomRoleInfo 对象
        /// </summary>
        /// <param name="customRoleInfo">CustomRoleInfo 对象</param>
        public void Update(CustomRoleInfo customRoleInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomRole SET GroupId = @GroupId, RoleName = @RoleName, RoleCode = @RoleCode, ");
            sb.Append("InitializedDate = @InitializedDate, ExpiredDate = @ExpiredDate, RoleProperty = @RoleProperty, IsSystemRole = @IsSystemRole, IsLockedOut = @IsLockedOut, Notes = @Notes ");
            sb.Append("WHERE RoleId = @RoleId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, customRoleInfo.RoleId);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customRoleInfo.GroupId);
                    db.AddInParameter(dbCommand, "RoleName", DbType.String, customRoleInfo.RoleName);
                    db.AddInParameter(dbCommand, "RoleCode", DbType.String, customRoleInfo.RoleCode);
                    db.AddInParameter(dbCommand, "InitializedDate", DbType.DateTime, DataConvertionHelper.SetDateTime(customRoleInfo.InitializedDate));
                    db.AddInParameter(dbCommand, "ExpiredDate", DbType.DateTime, DataConvertionHelper.SetDateTime(customRoleInfo.ExpiredDate));
                    db.AddInParameter(dbCommand, "RoleProperty", DbType.Int64, customRoleInfo.RoleProperty);
                    db.AddInParameter(dbCommand, "IsSystemRole", DbType.Boolean, customRoleInfo.IsSystemRole);
                    db.AddInParameter(dbCommand, "IsLockedOut", DbType.Boolean, customRoleInfo.IsLockedOut);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customRoleInfo.Notes);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomRoleInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        public void Delete(decimal roleId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomRole ");
            sb.Append("WHERE RoleId = @RoleId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            bool updateLeaf = true;
            decimal groupId = GetParentNodeId(roleId);
            int count = GetTotalCountOfChildNode(groupId);
            if (count > 1)
            {
                updateLeaf = false;
            }
            RoleAndBusiness roleAndBusiness = new RoleAndBusiness();
            RoleAndUser roleAndUser = new RoleAndUser();
            RoleAndDataField roleAndDataField = new RoleAndDataField();
            RoleAndTable roleAndTable = new RoleAndTable();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {                    
                    roleAndBusiness.DeleteBySecondForeignKey(roleId, db, transaction);
                    roleAndUser.DeleteBySecondForeignKey(roleId, db, transaction);
                    roleAndDataField.DeleteBySecondForeignKey(roleId, db, transaction);
                    roleAndTable.DeleteBySecondForeignKey(roleId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }
                    if (updateLeaf)
                    {
                        CustomGroup customGroup = new CustomGroup();
                        customGroup.UpdateLeafOfParentNode(groupId, true, db, transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
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
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomRole 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomRoleInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomRole ", "RoleId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

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

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            switch (databaseNodeType)
            {
                case DatabaseNodeType.Database:
                    sb.Append("SELECT DISTINCT CustomDatabase.DatabaseId, CustomDatabase.DatabaseName, CustomDatabase.Sorting FROM RoleAndTable ");
                    sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = RoleAndTable.TableId ");
                    sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
                    sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
                    sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndTable.RoleId ");
                    sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
                    sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndTable.DataAuthorityType = @DataAuthorityType AND RoleAndTable.TableAuthority & @TableAuthority > 0 ");
                    sb.Append("AND CustomRole.IsLockedOut = 0 AND CustomDatabase.DataWarehouseId = @DataWarehouseId AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
                    sb.Append("ORDER BY CustomDatabase.Sorting");
                    break;

                case DatabaseNodeType.Category:
                    sb.Append("SELECT DISTINCT CustomCategory.CategoryId, CustomCategory.CategoryName, CustomCategory.Sorting FROM RoleAndTable ");
                    sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = RoleAndTable.TableId ");
                    sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
                    sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
                    sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndTable.RoleId ");
                    sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
                    sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndTable.DataAuthorityType = @DataAuthorityType AND RoleAndTable.TableAuthority & @TableAuthority > 0 ");
                    sb.Append("AND CustomCategory.DatabaseId = @DatabaseId AND CustomRole.IsLockedOut = 0 AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
                    sb.Append("ORDER BY CustomCategory.Sorting");
                    break;

                case DatabaseNodeType.Table:
                    sb.Append("SELECT DISTINCT CustomTable.TableId, CustomTable.LogicalName, CustomTable.Sorting FROM RoleAndTable ");
                    sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = RoleAndTable.TableId ");
                    sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
                    sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
                    sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndTable.RoleId ");
                    sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
                    sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndTable.DataAuthorityType = @DataAuthorityType AND RoleAndTable.TableAuthority & @TableAuthority > 0 ");
                    sb.Append("AND CustomRole.IsLockedOut = 0 AND CustomTable.CategoryId = @CategoryId AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
                    sb.Append("ORDER BY CustomTable.Sorting");
                    break;

                case DatabaseNodeType.DataField:
                    RoleAndTable roleAndTable = new RoleAndTable();
                    Int64 systemDataFieldAuthority = roleAndTable.GetSystemDataFieldAuthority(userId, nodeId, (byte)DataAuthorityType.Query);
                    IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
                    foreach (EnumItem enumItem in enumItems)
                    {
                        bool result = ((systemDataFieldAuthority >> enumItem.Value) & 1L) == 1L;
                        if (result)
                        {
                            commonNodes.Add(new CommonNode(enumItem.Value, enumItem.Text, (byte)DataFieldProperty.SystemPhysicalDataField));
                        }
                    }
                    sb.Append("SELECT DISTINCT CustomDataField.DataFieldId, CustomDataField.LogicalName, CustomDataField.DataFieldProperty, CustomDataField.Sorting FROM RoleAndDataField ");
                    sb.Append("INNER JOIN CustomDataField ON CustomDataField.DataFieldId = RoleAndDataField.DataFieldId ");
                    sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndDataField.RoleId ");
                    sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
                    sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomDataField.TableId = @TableId ");
                    sb.Append("AND RoleAndDataField.DataAuthorityType = @DataAuthorityType AND RoleAndDataField.AuthorityType > 0 AND CustomRole.IsLockedOut = 0 ");
                    sb.Append("AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) ");
                    sb.Append("AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
                    sb.Append("ORDER BY CustomDataField.Sorting");
                    break;
            }

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    switch (databaseNodeType)
                    {
                        case DatabaseNodeType.Database:
                            db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, nodeId);
                            break;

                        case DatabaseNodeType.Category:
                            db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, nodeId);
                            break;

                        case DatabaseNodeType.Table:
                            db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, nodeId);
                            break;

                        case DatabaseNodeType.DataField:
                            db.AddInParameter(dbCommand, "TableId", DbType.Decimal, nodeId);
                            break;
                    }
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, AuthorityHelper.GetShiftedValue(1L, (byte)GridViewAuthority.View));
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal key = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string name = DataConvertionHelper.GetString(dataReader[1]);
                            if (databaseNodeType == DatabaseNodeType.DataField)
                            {
                                byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[2]);
                                commonNodes.Add(new CommonNode(key, name, dataFieldProperty));
                            }
                            else
                            {
                                commonNodes.Add(new CommonNode(key, name));
                            }
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
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

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT MenuSubAuthority FROM RoleAndUser ");
            sb.Append("INNER JOIN CustomRole ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("WHERE UserName = @UserName");
            try
            {
                Int64 authority = 0;
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            Int64 userWinAuthority = DataConvertionHelper.GetLong(dataReader[0]);
                            authority = authority | userWinAuthority;                            
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                    if (authority > 0)
                    {
                        result = AuthorityHelper.CheckAuthority(authority, (byte)menuSubAuthority);
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表 
        /// </summary>
        /// <param name="isSystemRole">是否是系统角色</param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemRole)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(1);
            whereConditons.Add(new WhereConditon("IsSystemRole", "IsSystemRole", DbType.Boolean, isSystemRole,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            return GetCommonNodesByWhereConditon(whereConditons);
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

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomRole SET MenuAuthority = @MenuAuthority, MenuSubAuthority = @MenuSubAuthority, SystemAuthority = @SystemAuthority, SystemSubAuthority = @SystemSubAuthority ");
            sb.Append("WHERE RoleId = @RoleId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "MenuAuthority", DbType.Int64, menuAuthority);
                    db.AddInParameter(dbCommand, "MenuSubAuthority", DbType.Int64, menuSubAuthority);
                    db.AddInParameter(dbCommand, "SystemAuthority", DbType.Int64, systemAuthority);
                    db.AddInParameter(dbCommand, "SystemSubAuthority", DbType.Int64, systemSubAuthority);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public RoleAuthority GetRoleAuthority(decimal userId)
        {
            RoleAuthority roleAuthority = null;

            Int64 roleProperty = 0;
            Int64 menuAuthority = 0;
            Int64 menuSubAuthority = 0;
            Int64 systemAuthority = 0;
            Int64 systemSubAuthority = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleProperty, MenuAuthority, MenuSubAuthority, SystemAuthority, SystemSubAuthority FROM CustomRole INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND (InitializedDate IS NULL OR InitializedDate <= @CurrentTime) AND (ExpiredDate IS NULL OR ExpiredDate >= @CurrentTime) ");
            sb.Append("ORDER BY CustomRole.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            Int64 currentRoleProperty = DataConvertionHelper.GetLong(dataReader[0]);
                            Int64 currentMenuAuthority = DataConvertionHelper.GetLong(dataReader[1]);
                            Int64 currentMenuSubAuthority = DataConvertionHelper.GetLong(dataReader[2]);
                            Int64 currentSystemAuthority = DataConvertionHelper.GetLong(dataReader[3]);
                            Int64 currentSystemSubAuthority = DataConvertionHelper.GetLong(dataReader[4]);
                            roleProperty = roleProperty | currentRoleProperty;
                            menuAuthority = menuAuthority | currentMenuAuthority;
                            menuSubAuthority = menuSubAuthority | currentMenuSubAuthority;
                            systemAuthority = systemAuthority | currentSystemAuthority;
                            systemSubAuthority = systemSubAuthority | currentSystemSubAuthority;
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                        roleAuthority = new RoleAuthority(roleProperty, menuAuthority, menuSubAuthority, systemAuthority, systemSubAuthority);
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roleAuthority;
        }

        /// <summary>
        /// 获得角色属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetRoleProperty(decimal userId)
        {
            Int64 roleProperty = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleProperty FROM CustomRole INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND (InitializedDate IS NULL OR InitializedDate >= @CurrentTime) AND (ExpiredDate IS NULL OR ExpiredDate<= @CurrentTime) ");
            sb.Append("ORDER BY CustomRole.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            Int64 currentRoleProperty = DataConvertionHelper.GetLong(dataReader[0]);
                            roleProperty = roleProperty | currentRoleProperty;
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
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
            IList<CustomRoleInfo> customRoleInfos = new List<CustomRoleInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomRole.RoleId, GroupId, RoleName, RoleCode, InitializedDate, ExpiredDate, RoleProperty, IsSystemRole, IsLockedOut, Sorting, Notes FROM CustomRole ");
            sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId WHERE RoleAndUser.UserId = @UserId ORDER BY CustomRole.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string roleName = DataConvertionHelper.GetString(dataReader[2]);
                            string roleCode = DataConvertionHelper.GetString(dataReader[3]);
                            DateTime initializedDate = DataConvertionHelper.GetDateTime(dataReader[4]);
                            DateTime expiredDate = DataConvertionHelper.GetDateTime(dataReader[5]);
                            long roleProperty = DataConvertionHelper.GetLong(dataReader[6]);
                            bool isSystemRole = DataConvertionHelper.GetBoolean(dataReader[7]);
                            long menuAuthority = DataConvertionHelper.GetLong(dataReader[8]);
                            long menuSubAuthority = DataConvertionHelper.GetLong(dataReader[9]);
                            long systemAuthority = DataConvertionHelper.GetLong(dataReader[10]);
                            long systemSubAuthority = DataConvertionHelper.GetLong(dataReader[11]);
                            bool isLockedOut = DataConvertionHelper.GetBoolean(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            string notes = DataConvertionHelper.GetString(dataReader[14]);
                            //将创建 CustomRoleInfo 对象加入集合中
                            customRoleInfos.Add(new CustomRoleInfo(roleId, groupId, roleName, roleCode, initializedDate,
                            expiredDate, roleProperty, isSystemRole, menuAuthority, menuSubAuthority,
                            systemAuthority, systemSubAuthority, isLockedOut, sorting, notes));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleInfos;
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
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    RoleAndTable roleAndTable = new RoleAndTable();
                    roleAndTable.Update(roleId, tableId, dataAuthorityType, tableAuthority, systemDataFieldAuthority, db, transaction);
                    RoleAndDataField roleAndDataField = new RoleAndDataField();
                    roleAndDataField.Update(roleId, roleAndDataFieldInfos, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 根据角色名称查角色编号
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色编号</returns>
        public decimal GetRoleIdByRoleName(string roleName)
        {
            decimal roleId = 0;

            //选择语句
            string sqlSelect = "SELECT RoleId FROM CustomRole WHERE RoleName = @RoleName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RoleName", DbType.String, roleName.Trim());
                roleId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return roleId;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomRoleInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomRoleInfo 对象列表</returns>
        private IList<CustomRoleInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomRoleInfo> customRoleInfos = new List<CustomRoleInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomRole");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if ((sortingCondtions != null) && (sortingCondtions.Count > 0))
            {
                sb.Append(" ORDER BY ");
                sb.Append(DataAccessHandler.GetSortingSentence(sortingCondtions));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string roleName = DataConvertionHelper.GetString(dataReader[2]);
                            string roleCode = DataConvertionHelper.GetString(dataReader[3]);
                            DateTime initializedDate = DataConvertionHelper.GetDateTime(dataReader[4]);
                            DateTime expiredDate = DataConvertionHelper.GetDateTime(dataReader[5]);
                            long roleProperty = DataConvertionHelper.GetLong(dataReader[6]);
                            bool isSystemRole = DataConvertionHelper.GetBoolean(dataReader[7]);
                            long menuAuthority = DataConvertionHelper.GetLong(dataReader[8]);
                            long menuSubAuthority = DataConvertionHelper.GetLong(dataReader[9]);
                            long systemAuthority = DataConvertionHelper.GetLong(dataReader[10]);
                            long systemSubAuthority = DataConvertionHelper.GetLong(dataReader[11]);
                            bool isLockedOut = DataConvertionHelper.GetBoolean(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            string notes = DataConvertionHelper.GetString(dataReader[14]);
                            //将创建 CustomRoleInfo 对象加入集合中
                            customRoleInfos.Add(new CustomRoleInfo(roleId, groupId, roleName, roleCode, initializedDate,
                            expiredDate, roleProperty, isSystemRole, menuAuthority, menuSubAuthority,
                            systemAuthority, systemSubAuthority, isLockedOut, sorting, notes));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customRoleInfos;
        }

        /// <summary>
        /// 获得 CustomRoleInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomRoleInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomRole");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomRole 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomRole ", "RoleId", "*", false, false, startPosition,
                    count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomRole 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomRole ", "RoleId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomRole 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomRole ", "RoleId", "*", false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomRole 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomRole ", "RoleId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }



        /// <summary>
        /// 删除满足条件的所有  CustomRoleInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomRole");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            else
            {
                throw new ArgumentNullException("批量删除的条件不许未空，即不允许删除该表中所有的数据.");
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义私有方法

        #endregion

        #endregion
    }
}
