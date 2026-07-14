//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：RoleAndTable.cs
// 描述：RoleAndTable 数据层访问类
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// RoleAndTable 表的数据层访问类
    /// </summary>
    public class RoleAndTable : CorrelatedTableDataAcess, IRoleAndTable
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndTable() : base("RoleAndTable", "TableId", "RoleId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 RoleAndTable 表中插入一条新记录
        /// </summary>
        /// <param name="roleAndTableInfo">roleAndTableInfo 对象</param>
        public void Insert(RoleAndTableInfo roleAndTableInfo)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO RoleAndTable(TableId, RoleId, DataAuthorityType, TableAuthority, SystemDataFieldAuthority)");
            sb.Append("VALUES (@TableId, @RoleId, @DataAuthorityType, @TableAuthority, @SystemDataFieldAuthority)");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值                    
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, roleAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleAndTableInfo.RoleId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, roleAndTableInfo.DataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, roleAndTableInfo.TableAuthority);
                    db.AddInParameter(dbCommand, "SystemDataFieldAuthority", DbType.Int64, roleAndTableInfo.SystemDataFieldAuthority);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
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
		/// 获得 RoleAndTableInfo 对象
		/// </summary>
		///<param name="roleId">角色编号</param>
		///<param name="tableId">表编号</param>
        ///<param name="tableId">表编号</param>
		/// <returns> RoleAndTableInfo 对象</returns>
		public RoleAndTableInfo GetModelInfo(decimal roleId, decimal tableId, byte dataAuthorityType)
        {
            RoleAndTableInfo roleAndTableInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", DbType.Decimal, roleId, DataFieldCondition.Equal, DataFieldInnerRealtion.None));
            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("DataAuthorityType", "DataAuthorityType", DbType.Byte, dataAuthorityType, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<RoleAndTableInfo> roleAndTableInfos = GetModeInfos(whereConditons, null, true);
            if (roleAndTableInfos != null && roleAndTableInfos.Count > 0)
            {
                roleAndTableInfo = roleAndTableInfos[0];
            }

            return roleAndTableInfo;
        }

        /// <summary>
        /// 更新 RoleAndTableInfo 对象
        /// </summary>
        /// <param name="roleAndTableInfo">RoleAndTableInfo 对象</param>
        public void Update(RoleAndTableInfo roleAndTableInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE RoleAndTable SET DataAuthorityType =  @DataAuthorityType, TableAuthority = @TableAuthority, SystemDataFieldAuthority = @SystemDataFieldAuthority ");
            sb.Append("WHERE RoleId = @RoleId AND TableId = @TableId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleAndTableInfo.RoleId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, roleAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, roleAndTableInfo.DataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, roleAndTableInfo.TableAuthority);
                    db.AddInParameter(dbCommand, "SystemDataFieldAuthority", DbType.Int64, roleAndTableInfo.SystemDataFieldAuthority);
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
        ///  删除 RoleAndTableInfo 对象
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="tableId">表编号</param>
        public void Delete(decimal roleId, decimal tableId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndTable ");
            sb.Append("WHERE RoleId = @RoleId AND TableId = @TableId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    //执行删除操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("删除失败！");
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
        /// 获得 RoleAndTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndTableInfo 对象列表</returns>
        public IList<RoleAndTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 RoleAndTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>RoleAndTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RoleAndTable ", "RoleId", false, whereConditons);
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
        /// 获得角色对应的字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public Int64 GetSystemDataFieldAuthority(decimal userId, decimal tableId, byte dataAuthorityType)
        {
            Int64 systemDataFieldAuthority = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SystemDataFieldAuthority FROM UserAccount INNER JOIN RoleAndUser ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN RoleAndTable ON CustomRole.RoleId = RoleAndTable.RoleId ");
            sb.Append("WHERE UserAccount.UserId =  @UserId AND RoleAndTable.TableId = @TableId AND RoleAndTable.DataAuthorityType = @DataAuthorityType");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            systemDataFieldAuthority |= DataConvertionHelper.GetLong(dataReader[0]);
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

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TableAuthority FROM UserAccount INNER JOIN RoleAndUser ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN RoleAndTable ON CustomRole.RoleId = RoleAndTable.RoleId ");
            sb.Append("WHERE UserAccount.UserId =  @UserId AND RoleAndTable.TableId = @TableId AND RoleAndTable.DataAuthorityType = @DataAuthorityType");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            tableAuthority |= DataConvertionHelper.GetLong(dataReader[0]);
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

            return tableAuthority;
        }

        /// <summary>
        /// 获得含授权表的仓库编号列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public IList<byte> GetDataWarehouseIds(decimal userId, DataAuthorityType dataAuthorityType)
        {
            IList<byte> dataWarehouseIds = new List<byte>();

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT CustomDatabase.DataWarehouseId FROM RoleAndTable ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = RoleAndTable.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndTable.RoleId ");
            sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndTable.DataAuthorityType = @DataAuthorityType AND RoleAndTable.TableAuthority & @TableAuthority > 0 ");
            sb.Append("AND CustomRole.IsLockedOut = 0 AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND(CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
            sb.Append("ORDER BY CustomDatabase.DataWarehouseId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, AuthorityHelper.GetShiftedValue(1L, (byte)GridViewAuthority.View)); 
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            dataWarehouseIds.Add(DataConvertionHelper.GetByte(dataReader[0]));
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

            return dataWarehouseIds;
        }

        /// <summary>
        /// 获得授权的数据库、分组和表等信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <returns></returns>
        public Dictionary<DatabaseNodeType, IList<CommonNode>> GetAuthorizedCommonNodes(decimal userId, byte dataWarehouseId, DataAuthorityType dataAuthorityType)
        {
            Dictionary<DatabaseNodeType, IList<CommonNode>> dicCommonNodes = new Dictionary<DatabaseNodeType, IList<CommonNode>>();

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT DISTINCT CustomDatabase.DatabaseId, CustomDatabase.DatabaseName, CustomCategory.CategoryId, CustomCategory.CategoryName, ");
            //sb.Append("CustomTable.TableId, CustomTable.LogicalName, CustomTable.PhysicalName, CustomTable.TableType, CustomDatabase.Sorting, CustomCategory.Sorting, CustomTable.Sorting FROM RoleAndDataField ");
            //sb.Append("INNER JOIN CustomDataField ON CustomDataField.DataFieldId = RoleAndDataField.DataFieldId ");
            //sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
            //sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            //sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            //sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndDataField.RoleId ");
            //sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
            //sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomDatabase.DataWarehouseId = @DataWarehouseId ");
            //sb.Append("AND RoleAndDataField.DataAuthorityType = @DataAuthorityType AND RoleAndDataField.AuthorityType > 0 AND CustomRole.IsLockedOut = 0 ");
            //sb.Append("AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) ");
            //sb.Append("AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
            //sb.Append("ORDER BY CustomDatabase.Sorting, CustomCategory.Sorting, CustomTable.Sorting");
            sb.Append("SELECT DISTINCT CustomDatabase.DatabaseId, CustomDatabase.DatabaseName, CustomCategory.CategoryId, CustomCategory.CategoryName, ");
            sb.Append("CustomTable.TableId, CustomTable.LogicalName, CustomTable.PhysicalName, CustomTable.TableType, CustomDatabase.Sorting, CustomCategory.Sorting, CustomTable.Sorting FROM RoleAndTable ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = RoleAndTable.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndTable.RoleId ");
            sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomDatabase.DataWarehouseId = @DataWarehouseId AND RoleAndTable.DataAuthorityType = @DataAuthorityType AND RoleAndTable.TableAuthority & @TableAuthority > 0 ");
            sb.Append("AND CustomRole.IsLockedOut = 0 AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
            sb.Append("ORDER BY CustomDatabase.Sorting, CustomCategory.Sorting, CustomTable.Sorting");
            try
            {
                IList<decimal> databaseIds = new List<decimal>();
                IList<decimal> categoryIds = new List<decimal>();
                IList<CommonNode> databaseCommonNodes = new List<CommonNode>();
                IList<CommonNode> categoryCommonNodes = new List<CommonNode>();
                IList<CommonNode> tableCommonNodes = new List<CommonNode>();
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, dataWarehouseId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, AuthorityHelper.GetShiftedValue(1L, (byte)GridViewAuthority.View));
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal databaseId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string databaseName = DataConvertionHelper.GetString(dataReader[1]);
                            decimal categoryId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[5]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[6]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[7]);
                            if (!databaseIds.Contains(databaseId))
                            {
                                databaseIds.Add(databaseId);
                                databaseCommonNodes.Add(new CommonNode(databaseId, dataWarehouseId, databaseName, false));
                            }
                            if (!categoryIds.Contains(categoryId))
                            {
                                categoryIds.Add(categoryId);
                                categoryCommonNodes.Add(new CommonNode(categoryId, databaseId, categoryName, false));
                            }
                            tableCommonNodes.Add(new CommonNode(tableId, categoryId, logicalName, physicalName, true, tableType));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                dicCommonNodes.Add(DatabaseNodeType.Category, categoryCommonNodes);
                dicCommonNodes.Add(DatabaseNodeType.Database, databaseCommonNodes);
                dicCommonNodes.Add(DatabaseNodeType.Table, tableCommonNodes);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dicCommonNodes;
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
            List<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.*, AuthorityType FROM CustomDataField ");
            sb.Append("INNER JOIN (SELECT RoleAndDataField.DataFieldId, MAX(RoleAndDataField.AuthorityType) AuthorityType FROM RoleAndDataField ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndDataField.RoleId ");
            sb.Append("INNER JOIN RoleAndUser ON CustomRole.RoleId = RoleAndUser.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndDataField.DataAuthorityType = @DataAuthorityType AND RoleAndDataField.AuthorityType > 0 AND CustomRole.IsLockedOut = 0 ");
            sb.Append("AND((CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) ");
            sb.Append("AND(CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime)) ");
            sb.Append("GROUP BY RoleAndDataField.DataFieldId) A ON CustomDataField.DataFieldId = A.DataFieldId ");
            if (tableIds != null && tableIds.Count > 0)
            {
                sb.Append("WHERE ");
                for (int idx = 0; idx < tableIds.Count; idx++)
                {
                    if (idx == 0)
                    {
                        sb.AppendFormat("TableId = @TableId_{0} ", idx);
                    }
                    else
                    {
                        sb.AppendFormat("OR TableId = @TableId_{0} ", idx);
                    }
                }
            }
            sb.Append(" ORDER BY CustomDataField.TableId, CustomDataField.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (tableIds == null || tableIds.Count == 0)
                    {
                        throw new ArgumentNullException("表格编号集合数量不能为空。");
                    }
                    for (int idx = 0; idx < tableIds.Count; idx++)
                    {
                        db.AddInParameter(dbCommand, string.Format("TableId_{0}", idx), DbType.String, tableIds[idx]);
                    }
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, (byte)dataAuthorityType);
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[5]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[6]);
                            string dataFieldCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[8]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[9]);
                            int dataFieldLength = DataConvertionHelper.GetInt(dataReader[10]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[11]);
                            string regexExpression = DataConvertionHelper.GetString(dataReader[12]);
                            string expressionText = DataConvertionHelper.GetString(dataReader[13]);
                            long dataFieldSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool requiredDataField = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool autoComplete = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool indexCreated = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool helpEnabled = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[19]);
                            string tooltip = DataConvertionHelper.GetString(dataReader[20]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[21]);
                            string notes = DataConvertionHelper.GetString(dataReader[22]);
                            byte authorityType = DataConvertionHelper.GetConvertedByte(dataReader[23]);
                            //将创建 ExtendedCustomDataFieldInfo 对象加入集合中
                            extendedCustomDataFieldInfos.Add(new ExtendedCustomDataFieldInfo(dataFieldId, enumId, parentDataFieldId, associatedDataFieldId, tableId,
                            logicalName, physicalName, dataFieldCode, dataFieldProperty, dataFieldType,
                            dataFieldLength, basedDataType, regexExpression, expressionText, dataFieldSetting, requiredDataField,
                            autoComplete, indexCreated, helpEnabled, helpContent, tooltip,
                            sorting, notes, physicalName, authorityType));
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


            return extendedCustomDataFieldInfos;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 更新表的权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="tableId"></param>
        /// <param name="dataAuthorityType"></param>
        /// <param name="tableAuthority"></param>
        /// <param name="systemDataFieldAuthority"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(decimal roleId, decimal tableId, byte dataAuthorityType, Int64 tableAuthority, Int64 systemDataFieldAuthority, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT RoleId FROM RoleAndTable WHERE TableId = @TableId AND RoleId = @RoleId AND DataAuthorityType = @DataAuthorityType) ");
            sb.Append("BEGIN UPDATE RoleAndTable SET TableAuthority = @TableAuthority, SystemDataFieldAuthority = @SystemDataFieldAuthority WHERE RoleId = @RoleId AND TableId = @TableId AND DataAuthorityType = @DataAuthorityType ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO RoleAndTable(TableId, RoleId, DataAuthorityType, TableAuthority, SystemDataFieldAuthority) ");
            sb.Append("VALUES (@TableId, @RoleId, @DataAuthorityType, @TableAuthority, @SystemDataFieldAuthority) END");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    db.AddInParameter(dbCommand, "DataAuthorityType", DbType.Byte, dataAuthorityType);
                    db.AddInParameter(dbCommand, "TableAuthority", DbType.Int64, tableAuthority);
                    db.AddInParameter(dbCommand, "SystemDataFieldAuthority", DbType.Int64, systemDataFieldAuthority);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 RoleAndTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RoleAndTableInfo 对象列表</returns>
        private IList<RoleAndTableInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<RoleAndTableInfo> roleAndTableInfos = new List<RoleAndTableInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM RoleAndTable");
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
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte dataAuthorityType = DataConvertionHelper.GetByte(dataReader[2]);
                            long tableAuthority = DataConvertionHelper.GetLong(dataReader[3]);
                            long systemDataFieldAuthority = DataConvertionHelper.GetLong(dataReader[4]);
                            //将创建 RoleAndTableInfo 对象加入集合中
                            roleAndTableInfos.Add(new RoleAndTableInfo(tableId, roleId, dataAuthorityType, tableAuthority, systemDataFieldAuthority));
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

            return roleAndTableInfos;
        }

        /// <summary>
        /// 获得 RoleAndTableInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RoleAndTableInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RoleAndTable");
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
        /// 获得表 RoleAndTable 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndTable ", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndTable 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndTable ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RoleAndTable 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "RoleAndTable ", "RoleId", "*", false, false, startPosition,
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
        /// 获得以表 RoleAndTable 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RoleAndTable ", "RoleId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 RoleAndTableInfo 对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal roleId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM RoleAndTable WHERE RoleId = @RoleId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    //执行删除操作
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

        /// <summary>
        /// 删除满足条件的所有  RoleAndTableInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RoleAndTable");
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
