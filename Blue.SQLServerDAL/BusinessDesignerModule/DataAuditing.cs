//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditing.cs
// 描述: DataAuditing 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.DataAccessLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessModule;
using Blue.SQLServerDAL.UserModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditing 表的数据层访问类
    /// </summary>
    public class DataAuditing : CommonNodeDataAccess, IDataAuditing
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditing() : base("DataAuditing", "DataAuditingId", "GroupId", "DataAuditingName", "DataAuditingCode", false, true, "TableType")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 DataAuditing 表中插入一条新记录
        /// </summary>
        /// <param name="dataAuditingInfo">dataAuditingInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(DataAuditingInfo dataAuditingInfo)
        {
            //自动增加的关键字的值
            decimal dataAuditingId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataAuditing(RoleId, ReportId, UserId, TableId, GroupId, CombinedTableId, ");
            sb.Append("ParentRoleId, ParentDataAuditingId, DataAuditingName, DataAuditingCode, DataAuditingType, DataAuditingProperty, ");
            sb.Append("SystemCondition, SystemDataFieldAuthority, InitReviewStatus, EnableManager, AllocationStatus, FinalReviewStatus, TableType, ");
            sb.Append("AllowAuditing, AllowAllocation, Sorting, Notes)");
            sb.Append("VALUES (@RoleId, @ReportId, @UserId, @TableId, @GroupId, @CombinedTableId, ");
            sb.Append("@ParentRoleId, @ParentDataAuditingId, @DataAuditingName, @DataAuditingCode, @DataAuditingType, @DataAuditingProperty, ");
            sb.Append("@SystemCondition, @SystemDataFieldAuthority, @InitReviewStatus, @EnableManager, @AllocationStatus, @FinalReviewStatus, @TableType, ");
            sb.Append("@AllowAuditing, @AllowAllocation, @Sorting, @Notes);");
            sb.Append("SET @DataAuditingId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            CustomGroup customGroup = new CustomGroup();
            dataAuditingInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "DataAuditing", "Sorting", "GroupId", dataAuditingInfo.GroupId, 0) + 1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "DataAuditingId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.RoleId));
                        db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ReportId));
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.UserId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.TableId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.GroupId));
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.CombinedTableId));
                        db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ParentRoleId));
                        db.AddInParameter(dbCommand, "ParentDataAuditingId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ParentDataAuditingId));
                        db.AddInParameter(dbCommand, "DataAuditingName", DbType.String, dataAuditingInfo.DataAuditingName);
                        db.AddInParameter(dbCommand, "DataAuditingCode", DbType.String, dataAuditingInfo.DataAuditingCode);
                        db.AddInParameter(dbCommand, "DataAuditingType", DbType.Byte, dataAuditingInfo.DataAuditingType);
                        db.AddInParameter(dbCommand, "DataAuditingProperty", DbType.Int64, dataAuditingInfo.DataAuditingProperty);
                        db.AddInParameter(dbCommand, "SystemDataFieldAuthority", DbType.Int64, dataAuditingInfo.SystemDataFieldAuthority);
                        db.AddInParameter(dbCommand, "SystemCondition", DbType.Int64, dataAuditingInfo.SystemCondition);
                        db.AddInParameter(dbCommand, "InitReviewStatus", DbType.Boolean, dataAuditingInfo.InitReviewStatus);
                        db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, dataAuditingInfo.EnableManager);
                        db.AddInParameter(dbCommand, "AllocationStatus", DbType.Boolean, dataAuditingInfo.AllocationStatus);
                        db.AddInParameter(dbCommand, "FinalReviewStatus", DbType.Boolean, dataAuditingInfo.FinalReviewStatus);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, dataAuditingInfo.TableType);
                        db.AddInParameter(dbCommand, "AllowAuditing", DbType.Boolean, dataAuditingInfo.AllowAuditing);
                        db.AddInParameter(dbCommand, "AllowAllocation", DbType.Boolean, dataAuditingInfo.AllowAllocation);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataAuditingInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, dataAuditingInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        dataAuditingId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataAuditingId"].Value, 0);
                    }
                    customGroup.UpdateLeafOfParentNode(dataAuditingInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return dataAuditingId;
        }
        
        /// <summary>
		/// 获得 DataAuditingInfo 对象
		/// </summary>
		///<param name="dataAuditingId"></param>
		/// <returns> DataAuditingInfo 对象</returns>
		public DataAuditingInfo GetModelInfo(decimal dataAuditingId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataAuditingId", "DataAuditingId", DbType.Decimal, dataAuditingId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataAuditingInfo> dataAuditingInfos = GetModelInfos(whereConditons, null, true);
            if (dataAuditingInfos != null && dataAuditingInfos.Count > 0)
            {
                dataAuditingInfo = dataAuditingInfos[0];
            }

            return dataAuditingInfo;
        }

        /// <summary>
        /// 更新 DataAuditingInfo 对象
        /// </summary>
        /// <param name="dataAuditingInfo">DataAuditingInfo 对象</param>
        public void Update(DataAuditingInfo dataAuditingInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE DataAuditing SET ReportId = @ReportId, RoleId = @RoleId, UserId = @UserId, TableId = @TableId, ");
            sb.Append("GroupId = @GroupId, CombinedTableId = @CombinedTableId, ParentRoleId = @ParentRoleId, ParentDataAuditingId = @ParentDataAuditingId, ");
            sb.Append("DataAuditingName = @DataAuditingName, DataAuditingCode = @DataAuditingCode, DataAuditingType = @DataAuditingType, ");
            sb.Append("DataAuditingProperty = @DataAuditingProperty, SystemCondition = @SystemCondition, SystemDataFieldAuthority = @SystemDataFieldAuthority, InitReviewStatus = @InitReviewStatus, ");
            sb.Append("EnableManager =  @EnableManager, AllocationStatus = @AllocationStatus, FinalReviewStatus = @FinalReviewStatus, TableType = @TableType, ");
            sb.Append("AllowAuditing = @AllowAuditing, AllowAllocation = @AllowAllocation, Notes = @Notes ");
            sb.Append("WHERE DataAuditingId = @DataAuditingId");

            bool delete = false;
            IList<decimal> dataAuditingIds = null;
            DataAuditingAndDataField dataAuditingAndDataField = new DataAuditingAndDataField();
            GroupType groupType = (GroupType)dataAuditingInfo.DataAuditingType;
            if (groupType == GroupType.InfoAudited)
            {
                DataAuditingInfo oldDataAuditingInfo = GetModelInfo(dataAuditingInfo.DataAuditingId);
                if (oldDataAuditingInfo.TableType != dataAuditingInfo.TableType)
                {
                    delete = true;
                }
                else
                {
                    FormType formType = (FormType)oldDataAuditingInfo.TableType;
                    switch (formType)
                    {
                        case FormType.CombinedTable:
                            if (oldDataAuditingInfo.CombinedTableId != dataAuditingInfo.CombinedTableId)
                            {
                                delete = true;
                            }
                            break;

                        case FormType.Table:
                            if (oldDataAuditingInfo.TableId != dataAuditingInfo.TableId)
                            {
                                delete = true;
                            }
                            break;
                    }
                }
                if (delete)
                {
                    dataAuditingIds = GetDataAuditingIds(dataAuditingInfo.DataAuditingId);
                }
            }
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {

                    if (delete)
                    {
                        foreach (var dataAuditingId in dataAuditingIds)
                        {
                            dataAuditingAndDataField.Delete(dataAuditingId, db, transaction);
                        }
                    }
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingInfo.DataAuditingId);
                        db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ReportId));
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.RoleId));
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.UserId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.TableId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.GroupId));
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.CombinedTableId));
                        db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ParentRoleId));
                        db.AddInParameter(dbCommand, "ParentDataAuditingId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingInfo.ParentDataAuditingId));
                        db.AddInParameter(dbCommand, "DataAuditingName", DbType.String, dataAuditingInfo.DataAuditingName);
                        db.AddInParameter(dbCommand, "DataAuditingCode", DbType.String, dataAuditingInfo.DataAuditingCode);
                        db.AddInParameter(dbCommand, "DataAuditingType", DbType.Byte, dataAuditingInfo.DataAuditingType);
                        db.AddInParameter(dbCommand, "DataAuditingProperty", DbType.Int64, dataAuditingInfo.DataAuditingProperty);
                        db.AddInParameter(dbCommand, "SystemCondition", DbType.Int64, dataAuditingInfo.SystemCondition);
                        db.AddInParameter(dbCommand, "SystemDataFieldAuthority", DbType.Int64, dataAuditingInfo.SystemDataFieldAuthority);
                        db.AddInParameter(dbCommand, "InitReviewStatus", DbType.Boolean, dataAuditingInfo.InitReviewStatus);
                        db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, dataAuditingInfo.EnableManager);
                        db.AddInParameter(dbCommand, "AllocationStatus", DbType.Boolean, dataAuditingInfo.AllocationStatus);
                        db.AddInParameter(dbCommand, "FinalReviewStatus", DbType.Boolean, dataAuditingInfo.FinalReviewStatus);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, dataAuditingInfo.TableType);
                        db.AddInParameter(dbCommand, "AllowAuditing", DbType.Boolean, dataAuditingInfo.AllowAuditing);
                        db.AddInParameter(dbCommand, "AllowAllocation", DbType.Boolean, dataAuditingInfo.AllowAllocation);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, dataAuditingInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                        transaction.Commit();
                    }
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
        ///  删除 DataAuditingInfo 对象
        /// </summary>
        ///<param name="dataAuditingId"></param>
        public void Delete(decimal dataAuditingId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataAuditing ");
            sb.Append("WHERE DataAuditingId = @DataAuditingId");
            DataAuditingAndDataField dataAuditingAndDataField = new DataAuditingAndDataField();

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingAndDataField.Delete(dataAuditingId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
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
        /// 获得 DataAuditingInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingInfo 对象列表</returns>
        public IList<DataAuditingInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 DataAuditing 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>DataAuditingInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataAuditing ", "DataAuditingId", false, whereConditons);
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
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        public void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir)
        {
            if (upLoadFileInfo.UpLoadFileData != null && upLoadFileInfo.UpLoadFileData.Length > 0)
            {
                StringBuilder sbPath = new StringBuilder();
                sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbPath.Append(@"\");
                }
                sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, subDir);
                if (!Directory.Exists(sbPath.ToString()))
                {
                    Directory.CreateDirectory(sbPath.ToString());
                }
                sbPath.Append(upLoadFileInfo.UpLoadFileName);
                using (FileStream fileStream = new FileStream(sbPath.ToString(), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fileStream.Write(upLoadFileInfo.UpLoadFileData, 0, (int)upLoadFileInfo.UpLoadFileData.Length);
                    fileStream.Close();
                }
            }
        }

        /// <summary>
        /// 通过组合表编号查询个人信息更新的数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        public int GetTotalCountByCombinedTableId(decimal combinedTableId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("CombinedTableId", "CombinedTableId", DbType.Decimal, combinedTableId, DataFieldCondition.Equal));

            return GetTotalCount(whereConditons);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfo(decimal auditingId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditing.* FROM DataAuditing ");
            sb.Append("INNER JOIN DataAuditing A ON DataAuditing.DataAuditingId = A.ParentDataAuditingId WHERE A.DataAuditingId = @DataAuditingId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, auditingId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[7]);
                            decimal parentDataAuditingId = DataConvertionHelper.GetDecimal(dataReader[8]);
                            string dataAuditingName = DataConvertionHelper.GetString(dataReader[9]);
                            string dataAuditingCode = DataConvertionHelper.GetString(dataReader[10]);
                            byte dataAuditingType = DataConvertionHelper.GetByte(dataReader[11]);
                            long dataAuditingProperty = DataConvertionHelper.GetLong(dataReader[12]);
                            long systemCondition = DataConvertionHelper.GetLong(dataReader[13]);
                            long systemDataFieldAuthority = DataConvertionHelper.GetLong(dataReader[14]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[15]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[18]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[19]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            dataAuditingInfo = new DataAuditingInfo(dataAuditingId, roleId, reportId, userId, tableId, groupId,
                            combinedTableId, parentRoleId, parentDataAuditingId, dataAuditingName, dataAuditingCode,
                            dataAuditingType, dataAuditingProperty, systemCondition, systemDataFieldAuthority, tableType,
                            initReviewStatus, enableManager, allocationStatus, finalReviewStatus, allowAuditing,
                            allowAllocation, sorting, notes);
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

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfoByLogId(decimal auditingLogId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditing.*  FROM DataAuditing INNER JOIN DataAuditing A  ON DataAuditing.DataAuditingId = A.ParentDataAuditingId ");
            sb.Append("INNER JOIN DataAuditingLog ON A.DataAuditingId = DataAuditingLog.DataAuditingId WHERE AuditingLogId = @AuditingLogId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[7]);
                            decimal parentDataAuditingId = DataConvertionHelper.GetDecimal(dataReader[8]);
                            string dataAuditingName = DataConvertionHelper.GetString(dataReader[9]);
                            string dataAuditingCode = DataConvertionHelper.GetString(dataReader[10]);
                            byte dataAuditingType = DataConvertionHelper.GetByte(dataReader[11]);
                            long dataAuditingProperty = DataConvertionHelper.GetLong(dataReader[12]);
                            long systemCondition = DataConvertionHelper.GetLong(dataReader[13]);
                            long systemDataFieldAuthority = DataConvertionHelper.GetLong(dataReader[14]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[15]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[18]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[19]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            dataAuditingInfo = new DataAuditingInfo(dataAuditingId, roleId, reportId, userId, tableId, groupId,
                            combinedTableId, parentRoleId, parentDataAuditingId, dataAuditingName, dataAuditingCode,
                            dataAuditingType, dataAuditingProperty, systemCondition, systemDataFieldAuthority, tableType,
                            initReviewStatus, enableManager, allocationStatus, finalReviewStatus, allowAuditing,
                            allowAllocation, sorting, notes);
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

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetModelInfoByLogId(decimal auditingLogId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditing.*  FROM DataAuditing ");
            sb.Append("INNER JOIN DataAuditingLog ON DataAuditing.DataAuditingId = DataAuditingLog.DataAuditingId WHERE AuditingLogId = @AuditingLogId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[7]);
                            decimal parentDataAuditingId = DataConvertionHelper.GetDecimal(dataReader[8]);
                            string dataAuditingName = DataConvertionHelper.GetString(dataReader[9]);
                            string dataAuditingCode = DataConvertionHelper.GetString(dataReader[10]);
                            byte dataAuditingType = DataConvertionHelper.GetByte(dataReader[11]);
                            long dataAuditingProperty = DataConvertionHelper.GetLong(dataReader[12]);
                            long systemCondition = DataConvertionHelper.GetLong(dataReader[13]);
                            long systemDataFieldAuthority = DataConvertionHelper.GetLong(dataReader[14]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[15]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[18]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[19]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            //将创建 DataAuditingInfo 对象加入集合中
                            dataAuditingInfo = new DataAuditingInfo(dataAuditingId, roleId, reportId, userId, tableId, groupId,
                            combinedTableId, parentRoleId, parentDataAuditingId, dataAuditingName, dataAuditingCode,
                            dataAuditingType, dataAuditingProperty, systemCondition, systemDataFieldAuthority, tableType,
                            initReviewStatus, enableManager, allocationStatus, finalReviewStatus, allowAuditing,
                            allowAllocation, sorting, notes);
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

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得个人信息审核依赖的个人信息编号
        /// </summary>
        ///<param name="dataAuditingId">个人信息审核编号</param>
        /// <returns> 个人信息审核依赖的个人信息编号</returns>
        public decimal GetParentDataAuditingId(decimal dataAuditingId)
        {
            decimal parentDataAuditingId = decimal.MinValue;

            try
            {
                string sqlSelect = "SELECT ParentDataAuditingId FROM DataAuditing WHERE DataAuditingId = @DataAuditingId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingId);
                    parentDataAuditingId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentDataAuditingId;
        }

        /// <summary>
        /// 获得关联的信息变更对象编号
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public IList<decimal> GetDataAuditingIds(decimal parentDataAuditingId)
        {
            IList<decimal> dataAuditingIds = new List<decimal>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditing.DataAuditingId FROM DataAuditing ");
            sb.Append("WHERE ParentDataAuditingId = @ParentDataAuditingId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentDataAuditingId", DbType.Decimal, parentDataAuditingId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            dataAuditingIds.Add(dataAuditingId);
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

            return dataAuditingIds;
        }

        /// <summary>
        /// 获得关联的信息变更对象
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataAuditings(decimal parentDataAuditingId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditing.DataAuditingId, DataAuditing.DataAuditingName, DataAuditing.DataAuditingCode, DataAuditing.DataAuditingType FROM DataAuditing ");
            sb.Append("WHERE ParentDataAuditingId = @ParentDataAuditingId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentDataAuditingId", DbType.Decimal, parentDataAuditingId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string dataAuditingName = DataConvertionHelper.GetString(dataReader[1]);
                            string dataAuditingCode = DataConvertionHelper.GetString(dataReader[2]);
                            byte dataAuditingType = DataConvertionHelper.GetByte(dataReader[2]);
                            commonNodes.Add(new CommonNode(dataAuditingId, parentDataAuditingId, dataAuditingName, dataAuditingCode));
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
        /// 获取当前初审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataAuditingId, decimal userId)
        {
            Dictionary<decimal, string> reviewers = new Dictionary<decimal, string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleAndUser.UserId, A.UserName, A.UserActualName FROM DataAuditing INNER JOIN RoleAndUser ");
            sb.Append("ON DataAuditing.RoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN UserAccount A ON A.UserId = RoleAndUser.UserId INNER JOIN UserAccount B ON B.DepId = A.DepId WHERE DataAuditing.DataAuditingId = @DataAuditingId AND B.UserId = @UserId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal reviewerId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            if (!reviewers.ContainsKey(reviewerId))
                            {
                                reviewers.Add(reviewerId, string.Format("{0}[{1}]", userName, userActualName));
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

            return reviewers;
        }

        /// <summary>
        /// 获取当前终审的评审人列表
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataAuditingId)
        {
            Dictionary<decimal, string> reviewers = new Dictionary<decimal, string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleAndUser.UserId, UserAccount.UserName, UserAccount.UserActualName FROM DataAuditing INNER JOIN RoleAndUser ");
            sb.Append("ON DataAuditing.ParentRoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = RoleAndUser.UserId WHERE DataAuditing.DataAuditingId = @DataAuditingId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {

                        while (dataReader.Read())
                        {
                            decimal reviewerId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            if (!reviewers.ContainsKey(reviewerId))
                            {
                                reviewers.Add(reviewerId, string.Format("{0}[{1}]", userName, userActualName));
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

            return reviewers;
        }

        /// <summary>
        /// 完全更新记录(多表联合查询使用)
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">被更新的字段</param>        
        /// <param name="queryBuilder">查询条件</param>
        /// <param name="whereConditons">附加查询条件</param>
        public void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons)
        {
            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            string tableName = customTable.GetTablePhysicalName(tableId);
            RecordKeyInfo recordKeyInfo = GetKeyWords(tableId, recordId);
            QueryClauseConstructor qeryClauseConstructor = new QueryClauseConstructor(queryBuilder, 0, 0);
            string where = DataAccessHandler.GetWhereSentence(whereConditons);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);

            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            int totalCount = 0;
            DataSet ds = null;
            if (relaitonDataFields.Count > 0)
            {               
                ds = DataAccessHandler.GetPageRecord(db, tableName, recordIdName, string.Format("{0}, {1}", recordIdName, userIdName, bussinessIdName),
                    false, false, 0, 0, whereConditons, ref totalCount);
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("UPDATE {0} SET ModificationTime = @ModificationTime", tableName);
                    foreach (CommonDataField commonDataField in commonDataFields)
                    {
                        if (commonDataField.DataFieldDataType == DbType.Object)
                        {
                            /* 附件类型 */
                            UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                            /* 更新文件: 如果文件名不为空，内容为空，则表示文件内容不变 */
                            if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName))
                            {
                                if (upLoadFileInfo.UpLoadFileData != null && upLoadFileInfo.UpLoadFileData.Length > 0)
                                {
                                    string oldUpLoadFileName = upLoadFileInfo.UpLoadFileName;
                                    upLoadFileInfo.UpLoadFileName = GenerateFileName(recordKeyInfo.UserName, commonDataField.DataFieldName, recordKeyInfo.RecordSorting, upLoadFileInfo.UpLoadFileName, false);
                                    SaveUploadFiles(upLoadFileInfo, commonDataField.DataFieldName);
                                    DeleteUploadFiles(oldUpLoadFileName, commonDataField.DataFieldName);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                /* 删除文件：如果文件名为空，则源名称不为空，则表示删除源文件 */
                                if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                                {
                                    int count = 0;
                                    //查询语句
                                    string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", tableName, commonDataField.DataFieldName);
                                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                                    {
                                        //给参数赋值
                                        db.AddInParameter(dbCommand, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadSourceFileName);
                                        count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                                    }
                                    if (count <= 1)
                                    {
                                        DeleteUploadFiles(commonDataField.DataFieldName, upLoadFileInfo.UpLoadSourceFileName);
                                    }
                                }
                            }
                        }
                        sb.AppendFormat(", {0} = @{0}", commonDataField.DataFieldName);
                    }
                    if (qeryClauseConstructor.TableCount > 1)
                    {
                        sb.AppendFormat(" FROM {0} ", qeryClauseConstructor.TableClause);
                    }
                    if (qeryClauseConstructor.Where.Length > 0 || where.Length > 0)
                    {
                        sb.Append(" WHERE ");
                    }
                    if (qeryClauseConstructor.Where.Length > 0)
                    {
                        sb.AppendFormat("({0})", qeryClauseConstructor.Where);
                    }
                    if (where.Length > 0)
                    {
                        if (qeryClauseConstructor.Where.Length > 0)
                        {
                            sb.Append(" AND ");
                        }
                        sb.AppendFormat("({0})", where);
                    }
                    using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
                    {
                        if ((whereConditons != null) && (whereConditons.Count > 0))
                        {
                            DataAccessHandler.AddInParameter(db, cmd, whereConditons);
                        }
                        db.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                        foreach (CommonDataField commonDataField in commonDataFields)
                        {
                            if (commonDataField.DataFieldDataType == DbType.Object)
                            {
                                /* 附件类型 */
                                UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                                DataAccessHandler.AddInParameter(db, cmd, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadFileName);
                            }
                            else
                            {
                                if (commonDataField.DataFieldValue == null)
                                {
                                    commonDataField.DataFieldValue = DBNull.Value;
                                }
                                DataAccessHandler.AddInParameter(db, cmd, commonDataField.DataFieldName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
                            }
                        }
                        //执行插入操作
                        db.ExecuteNonQuery(cmd, transaction);
                    }
                    decimal userId = decimal.MinValue;
                    decimal instanceId = decimal.MinValue;
                    if (relaitonDataFields.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            userId = Convert.ToDecimal(dr[userIdName]);
                            instanceId = DataConvertionHelper.GetDecimal(dr[bussinessIdName]);
                            UpdateRelationDataFields(userId, instanceId, relaitonDataFields, instanceId > 0, db, transaction);
                        }
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
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        public void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields)
        {
            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            RecordKeyInfo recordKeyInfo  = GetKeyWords(tableId, recordId);
            CustomTableInfo customTableInfo = customTable.GetModelInfo(tableId);

            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(recordId, customTableInfo.PhysicalName, commonDataFields, recordKeyInfo, db, transaction);
                    UpdateRelationDataFields(recordKeyInfo.UserId, recordKeyInfo.BusinessId, relaitonDataFields, recordKeyInfo.BusinessId > 0, db, transaction);
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
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordIds">记录编号集合</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        public void Update(decimal tableId, IList<decimal> recordIds, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields)
        {
            foreach (decimal recordId in recordIds)
            {
                Update(tableId, recordId, commonDataFields, relaitonDataFields);
            }
        }

        /// <summary>
        /// 通过表的编号记录编号获得用户编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public RecordKeyInfo GetKeyWords(decimal tableId, decimal recordId)
        {
            RecordKeyInfo recordKeyInfo = null;

            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            //查询语句
            string sqlSelect = string.Format("SELECT UserId, UserName, DepId, UserTypeId, BusinessId, BusinessForeignId, BusinessAlternativeId, RecordSorting, AuditedStatus, CurrentState, CreationTime, ModificationTime, ModifiedByUserName, IsDeleted FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                        string userName = DataConvertionHelper.GetString(dataReader[1]);
                        decimal depId = DataConvertionHelper.GetDecimal(dataReader[2]);
                        decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[3]);
                        decimal businessId = DataConvertionHelper.GetDecimal(dataReader[4]);
                        decimal businessForeignId = DataConvertionHelper.GetDecimal(dataReader[5]);
                        decimal businessAlternativeId = DataConvertionHelper.GetDecimal(dataReader[6]);
                        int recordSorting = DataConvertionHelper.GetInt(dataReader[7]);
                        byte auditedStatus = DataConvertionHelper.GetByte(dataReader[8]);
                        byte currentState = DataConvertionHelper.GetByte(dataReader[9]);
                        DateTime creationTime = DataConvertionHelper.GetDateTime(dataReader[10]);
                        DateTime modificationTime = DataConvertionHelper.GetDateTime(dataReader[11]);
                        string modifiedByUserName = DataConvertionHelper.GetString(dataReader[12]);
                        bool isDeleted = DataConvertionHelper.GetBoolean(dataReader[13]);
                        recordKeyInfo = new RecordKeyInfo(userId, userName, depId, userTypeId, businessId, businessForeignId, businessAlternativeId,
                            recordSorting, auditedStatus, currentState, creationTime, modificationTime, modifiedByUserName, isDeleted);
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            return recordKeyInfo;
        }

        /// <summary>
        /// 通过表的编号记录编号获得用户编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public decimal GetUserIdByRecordId(decimal tableId, decimal recordId)
        {
            decimal userId = 0;

            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            //查询语句
            string sqlSelect = string.Format("SELECT UserId FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return userId;
        }

        /// <summary>
        /// 更新当前表中的字段
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="whereConditons"></param>
        public void Update(RecordEntity recordEntity, IList<WhereConditon> whereConditons)
        {
            CustomTable customTable = new CustomTable();
            CustomTableInfo customTableInfo = customTable.GetModelInfo(recordEntity.TableId);
            DataTableType dataTableType = (DataTableType)customTableInfo.TableType;
            byte dataWarehouseId = customTable.GetDataWarehouseId(recordEntity.TableId);
            SqlDatabase dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customTable.GetDataWarehouseId(recordEntity.TableId)));
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);

            int totalCount = 0;
            DataSet ds = null;
            if (recordEntity.RelaitonDataFields.Count > 0)
            {
                ds = DataAccessHandler.GetPageRecord(dbBlue, customTableInfo.PhysicalName, recordIdName, string.Format("{0}, {1}", recordIdName, userIdName, bussinessIdName),
                    false, false, 0, 0, whereConditons, ref totalCount);
            }

            StringBuilder sb = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET ModificationTime = @ModificationTime", customTableInfo.PhysicalName);
            foreach (CommonDataField commonDataField in recordEntity.CommonDataFields)
            {
                if (commonDataField.DataFieldDataType == DbType.Object)
                {
                    /* 附件类型 */
                    UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                    /* 更新文件: 如果文件名不为空，内容为空，则表示文件内容不变 */
                    if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName))
                    {
                        if (upLoadFileInfo.UpLoadFileData != null && upLoadFileInfo.UpLoadFileData.Length > 0)
                        {
                            SaveUploadFiles(upLoadFileInfo, commonDataField.DataFieldName);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        /* 删除文件：如果文件名为空，则源名称不为空，则表示删除源文件 */
                        if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                        {
                            int count = 0;
                            //查询语句
                            string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", customTableInfo.PhysicalName, commonDataField.DataFieldName);
                            using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sqlSelect))
                            {
                                //给参数赋值
                                dbBlue.AddInParameter(dbCommand, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadSourceFileName);
                                count = DataConvertionHelper.GetInt(dbBlue.ExecuteScalar(dbCommand));
                            }
                            if (count <= 1)
                            {
                                DeleteUploadFiles(commonDataField.DataFieldName, upLoadFileInfo.UpLoadSourceFileName);
                            }
                        }
                    }
                }
                sb.AppendFormat(", {0} = @{0}", commonDataField.DataFieldName);
            }
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.AppendFormat(" WHERE {0}", DataAccessHandler.GetConditionSentence(whereConditons));
            }
            using (DbConnection conn = dbBlue.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                try
                {
                    using (DbCommand cmd = dbBlue.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        dbBlue.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                        foreach (CommonDataField commonDataField in recordEntity.CommonDataFields)
                        {
                            if (commonDataField.DataFieldDataType == DbType.Object)
                            {
                                /* 附件类型 */
                                UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                                DataAccessHandler.AddInParameter(dbBlue, cmd, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadFileName);
                            }
                            else
                            {
                                if (commonDataField.DataFieldValue == null)
                                {
                                    commonDataField.DataFieldValue = DBNull.Value;
                                }
                                DataAccessHandler.AddInParameter(dbBlue, cmd, commonDataField.DataFieldName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
                            }
                        }
                        DataAccessHandler.AddInParameter(dbBlue, cmd, whereConditons);
                        //执行更新操作
                        dbBlue.ExecuteNonQuery(cmd, trans);
                        decimal userId = decimal.MinValue;
                        decimal instanceId = decimal.MinValue;
                        if (recordEntity.RelaitonDataFields.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                userId = Convert.ToDecimal(dr[userIdName]);
                                instanceId = DataConvertionHelper.GetDecimal(dr[bussinessIdName]);
                                UpdateRelationDataFields(userId, instanceId, recordEntity.RelaitonDataFields, instanceId > 0, dbBlue, trans);
                            }
                        }
                        trans.Commit();
                    }
                }
                catch (Exception exception)
                {
                    trans.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public List<RecordItem> Process(decimal userId, IList<RecordEntity> recordEntities)
        {
            UserAccount userAccount = new UserAccount();
            CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(userId);

            return Process(commonUserInfo, recordEntities, false);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        public Dictionary<decimal, List<RecordItem>> Process(CommonUserInfo commonUserInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            return Process(commonUserInfo, dicRecordEntities, false);
        }        

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public List<RecordItem> Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities)
        {
            return Process(commonUserInfo, recordEntities, false);
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="description"></param>
        /// <param name="dataAuditingStepInfo"></param>
        public List<RecordItem> ProcessWithLog(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            List<RecordItem> recordItems = null;

            DataAuditingLog dataAuditingLog = new DataAuditingLog();
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingLog.UpdateAuditingInfo(dataAuditingLogInfo, db, transaction);
                    dataAuditingStepInfo.AuditingLogId = dataAuditingLogInfo.AuditingLogId;
                    dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                    transaction.Commit();
                    foreach (var recordEntitiy in recordEntities)
                    {
                        recordEntitiy.InstanceId = dataAuditingLogInfo.AuditingLogId;
                    }
                    recordItems = Process(commonUserInfo, recordEntities, true);
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return recordItems;
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="dataAuditingStepInfo"></param>
        /// <returns></returns>
        public List<RecordItem> Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            List<RecordItem> recordItems = null;

            DataAuditingLog dataAuditingLog = new DataAuditingLog();
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    decimal auditingLogId = dataAuditingLog.Insert(dataAuditingLogInfo, db, transaction);
                    dataAuditingStepInfo.AuditingLogId = auditingLogId;
                    dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                    transaction.Commit();
                    foreach (var recordEntitiy in recordEntities)
                    {
                        recordEntitiy.InstanceId = auditingLogId;
                    }
                    Process(commonUserInfo, recordEntities, true);
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return recordItems;
        }

        /// <summary>
        /// 更新主从表(启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        public void UpdateCurretStateByInstanceId(decimal tableId, decimal recordId, decimal instanceId)
        {
            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            string physicalName = customTable.GetTablePhysicalName(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

            //选择语句
            CurrentState currentState = CurrentState.History;
            CurrentState newState = CurrentState.Current;
            string sqlSelect = string.Format("SELECT CurrentState FROM {0} WHERE RecordId = @RecordId", physicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                //执行更新操作                   
                currentState = (CurrentState)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand), 0);
            }
            switch (currentState)
            {
                case CurrentState.Current:
                    newState = CurrentState.History;
                    break;

                case CurrentState.History:
                    UpdateRelatedDataFields(tableId, instanceId, recordId);
                    break;
            }

            //生成更新语句
            string sqlUpdate2 = string.Format("UPDATE {0} SET CurrentState = @CurrentState WHERE RecordId = @RecordId", physicalName);
            string sqlUpdate1 = string.Format("UPDATE {0} SET CurrentState = @CurrentState WHERE BusinessId = @BusinessId AND RecordId != @RecordId", physicalName);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate1))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)newState);
                        db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                        //执行更新操作                   
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    if (newState == CurrentState.Current)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate2))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.History);
                            db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, instanceId);
                            db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                            //执行更新操作                   
                            db.ExecuteNonQuery(dbCommand, transaction);
                        }
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
        /// 更新主从表(未启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="userId"></param>
        public void UpdateCurretStateByUserId(decimal tableId, decimal recordId, decimal userId)
        {
            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            string physicalName = customTable.GetTablePhysicalName(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

            //选择语句
            CurrentState currentState = CurrentState.History;
            CurrentState newState = CurrentState.Current;
            string sqlSelect = string.Format("SELECT CurrentState FROM {0} WHERE RecordId = @RecordId", physicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                //执行更新操作                   
                currentState = (CurrentState)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand), 0);
            }
            switch (currentState)
            {
                case CurrentState.Current:
                    newState = CurrentState.History;                    
                    break;

                case CurrentState.History:
                    newState = CurrentState.Current;
                    UpdateRelatedDataFields(tableId, decimal.MinValue, recordId);
                    break;
            }

            //生成更新语句
            string sqlUpdate1 = string.Format("UPDATE {0} SET CurrentState = @CurrentState WHERE RecordId = @RecordId", physicalName);
            string sqlUpdate2 = string.Format("UPDATE {0} SET CurrentState = @CurrentState WHERE UserId = @UserId AND RecordId != @RecordId", physicalName);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate1))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)newState);
                        db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                        //执行更新操作                   
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    if (newState == CurrentState.Current)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate2))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.History);
                            db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                            db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                            //执行更新操作                   
                            db.ExecuteNonQuery(dbCommand, transaction);
                        }
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

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除 CustomCellInfo 对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE DataAuditingAndDataField FROM DataAuditingAndDataField INNER JOIN DataAuditing ");
            sb.Append("ON DataAuditing.DataAuditingId = DataAuditingAndDataField.DataAuditingId ");
            sb.Append("WHERE DataAuditing.TableId = @TableId; ");
            sb.Append("DELETE FROM DataAuditing ");
            sb.Append("WHERE TableId = @TableId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentState"></param>
        /// <param name="physicalName"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void UpdateCurrentStateByUserId(decimal userId, byte currentState, string physicalName, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET CurrentState = @CurrentState ", physicalName);
            sb.Append("WHERE UserId = @UserId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, currentState);

                    //执行更新操作                   
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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
        /// 获得 DataAuditingInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataAuditingInfo 对象列表</returns>
        private IList<DataAuditingInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<DataAuditingInfo>  dataAuditingInfos = new List<DataAuditingInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM DataAuditing");
            
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if((sortingCondtions != null) && (sortingCondtions.Count > 0))
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
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[7]);
                            decimal parentDataAuditingId = DataConvertionHelper.GetDecimal(dataReader[8]);
                            string dataAuditingName = DataConvertionHelper.GetString(dataReader[9]);
                            string dataAuditingCode = DataConvertionHelper.GetString(dataReader[10]);
                            byte dataAuditingType = DataConvertionHelper.GetByte(dataReader[11]);
                            long dataAuditingProperty = DataConvertionHelper.GetLong(dataReader[12]);
                            long systemCondition = DataConvertionHelper.GetLong(dataReader[13]);
                            long systemDataFieldAuthority = DataConvertionHelper.GetLong(dataReader[14]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[15]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[18]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[19]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            //将创建 DataAuditingInfo 对象加入集合中
                            dataAuditingInfos.Add(new DataAuditingInfo(dataAuditingId, roleId, reportId, userId, tableId,
                            groupId, combinedTableId, parentRoleId, parentDataAuditingId, dataAuditingName,
                            dataAuditingCode, dataAuditingType, dataAuditingProperty, systemCondition, systemDataFieldAuthority,
                            tableType, initReviewStatus, enableManager, allocationStatus, finalReviewStatus,
                            allowAuditing, allowAllocation, sorting, notes));
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
            
			return dataAuditingInfos;
		}
        		
		/// <summary>
		/// 获得 DataAuditingInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>DataAuditingInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataAuditing");
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
        /// 获得表 DataAuditing 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int  startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {             
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditing ", "DataAuditingId", "*", false, false, startPosition, 
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
        /// 获得以表 DataAuditing 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditing ", "DataAuditingId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 DataAuditing 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditing ", "DataAuditingId", "*", false, false, startPosition, 
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
        /// 获得以表 DataAuditing 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditing ", "DataAuditingId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  DataAuditingInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataAuditing");
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

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            DataAuditingInfo dataAuditingInfo = GetModelInfo(nodeId);
            if (dataAuditingInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(dataAuditingInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(dataAuditingInfo.DataAuditingName);
            }

            return names;
        }

        #endregion

        #region 自定义私有方法
        
        /// <summary>
        /// 更新联动字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="instanceId"></param>
        /// <param name="recordId"></param>
        private void UpdateRelatedDataFields(decimal tableId, decimal instanceId, decimal recordId)
        {
            try
            {
                Dictionary<decimal, List<CommonDataField>> dicCommonDataField = new Dictionary<decimal, List<CommonDataField>>();
                DataFieldRelationship dataFieldRelationship = new DataFieldRelationship();
                CustomDataField customDataField = new CustomDataField();
                IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
                List<CommonDataFieldInfo> commonDataFieldInfos = new List<CommonDataFieldInfo>();
                foreach (var customDataFieldInfo in customDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        commonDataFieldInfos.Add(new CommonDataFieldInfo(customDataFieldInfo.DataFieldId, customDataFieldInfo.PhysicalName,
                            customDataFieldInfo.LogicalName, string.Empty, dataFieldProperty, customDataFieldInfo.DataFieldType));
                    }
                }
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                string businessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
                string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                CustomTable customTable = new CustomTable();
                DataTable data = customTable.GetTableData(tableId, recordId, commonDataFieldInfos);
                if (data != null && data.Rows.Count > 0)
                {
                    decimal userId = Convert.ToDecimal(data.Rows[0][userIdName]);
                    foreach (var customDataFieldInfo in customDataFieldInfos)
                    {
                        bool relationship = AuthorityHelper.CheckAuthority(customDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
                        if (relationship)
                        {
                            IList<CommonNode> relationCommonNodes = dataFieldRelationship.GetRelationDataFields(customDataFieldInfo.DataFieldId);
                            if (relationCommonNodes.Count > 0)
                            {
                                object dataValue = data.Rows[0][customDataFieldInfo.PhysicalName];
                                foreach (var relationCommonNode in relationCommonNodes)
                                {
                                    List<CommonDataField> commonDataFields = null;
                                    decimal relationTableId = customDataField.GetTableId(relationCommonNode.NodeId);
                                    if (!dicCommonDataField.ContainsKey(relationTableId))
                                    {
                                        commonDataFields = new List<CommonDataField>();
                                        dicCommonDataField.Add(relationTableId, commonDataFields);
                                    }
                                    else
                                    {
                                        commonDataFields = dicCommonDataField[relationTableId];
                                    }
                                    commonDataFields.Add(new CommonDataField(relationCommonNode.NodeCode, dataValue, (DbType)DataFieldHelper.GetDbType((BasedDataType)customDataFieldInfo.BasedDataType)));
                                }
                            }
                        }
                    }
                    byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    using (DbConnection conn = db.CreateConnection())
                    {
                        conn.Open();
                        DbTransaction transaction = conn.BeginTransaction();
                        try
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (KeyValuePair<decimal, List<CommonDataField>> keyValue in dicCommonDataField)
                            {
                                if (keyValue.Value.Count == 0) continue;
                                CustomTableInfo customTableInfo = customTable.GetModelInfo(keyValue.Key);
                                sb.AppendFormat("UPDATE {0} SET ", customTableInfo.PhysicalName);
                                foreach (CommonDataField commonDataField in keyValue.Value)
                                {
                                    sb.AppendFormat("{0} = @{1}, ", commonDataField.DataFieldName, commonDataField.DataFieldParameterName);
                                }
                                sb.Remove(sb.Length - 2, 2);
                                sb.AppendFormat(" WHERE {0} = @{0} ", userIdName);
                                if (instanceId > 0)
                                {
                                    sb.AppendFormat("AND {0} = @{0} ", businessIdName);
                                }
                                if ((DataTableType)customTableInfo.TableType == DataTableType.MasterSlaveTable)
                                {
                                    sb.AppendFormat("AND {0} = @{0} ", currentStateName);
                                }
                                using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
                                {
                                    //给参数赋值
                                    DataAccessHandler.AddInParameter(db, cmd, keyValue.Value);
                                    db.AddInParameter(cmd, userIdName, DbType.Decimal, userId);
                                    if (instanceId > 0)
                                    {
                                        db.AddInParameter(cmd, businessIdName, DbType.Decimal, instanceId);
                                    }
                                    if ((DataTableType)customTableInfo.TableType == DataTableType.MasterSlaveTable)
                                    {
                                        db.AddInParameter(cmd, currentStateName, DbType.Byte, (byte)CurrentState.Current);
                                    }
                                    //执行更新操作                   
                                    db.ExecuteNonQuery(cmd, transaction);
                                }
                                sb.Clear();
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
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }  

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="tableName"></param>
        /// <param name="commonDataFields"></param>
        /// <param name="recordKeyInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(decimal recordId, string tableName, IList<CommonDataField> commonDataFields, RecordKeyInfo recordKeyInfo, SqlDatabase db, DbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET ModificationTime = @ModificationTime", tableName);
            foreach (CommonDataField commonDataField in commonDataFields)
            {
                if (commonDataField.DataFieldDataType == DbType.Object)
                {
                    /* 附件类型 */
                    UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                    /* 更新文件: 如果文件名不为空，内容为空，则表示文件内容不变 */
                    if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName))
                    {
                        upLoadFileInfo.UpLoadFileName = GenerateFileName(recordKeyInfo.UserName, commonDataField.DataFieldName, recordKeyInfo.RecordSorting, upLoadFileInfo.UpLoadFileName, false);
                        SaveUploadFiles(upLoadFileInfo, commonDataField.DataFieldName);
                    }
                    else
                    {
                        /* 删除文件：如果文件名为空，则源名称不为空，则表示删除源文件 */
                        if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                        {
                            int count = 0;
                            //查询语句
                            string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", tableName, commonDataField.DataFieldName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadSourceFileName);
                                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }
                            if (count <= 1)
                            {
                                DeleteUploadFiles(commonDataField.DataFieldName, upLoadFileInfo.UpLoadSourceFileName);
                            }
                        }
                    }
                }
                sb.AppendFormat(", {0} = @{0}", commonDataField.DataFieldName);
            }
            sb.Append(" WHERE RecordId = @RecordId");
            using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(cmd, "RecordId", DbType.Decimal, recordId);
                db.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                foreach (CommonDataField commonDataField in commonDataFields)
                {
                    if (commonDataField.DataFieldDataType == DbType.Object)
                    {
                        /* 附件类型 */
                        UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                        DataAccessHandler.AddInParameter(db, cmd, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadFileName);
                    }
                    else
                    {
                        if (commonDataField.DataFieldValue == null)
                        {
                            commonDataField.DataFieldValue = DBNull.Value;
                        }
                        DataAccessHandler.AddInParameter(db, cmd, commonDataField.DataFieldName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
                    }
                }
                //执行插入操作
                if (db.ExecuteNonQuery(cmd, transaction) < 1)
                {
                    throw new Exception("更新失败！");
                }
            }
        }
        
        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="recordSorting"></param>
        /// <param name="srouceFileName"></param>
        /// <param name="mirror"></param>
        /// <returns></returns>
        private string GenerateFileName(string userName, string dataFieldName, int recordSorting, string srouceFileName, bool mirror)
        {
            if (mirror)
            {
                return string.Format("{0}_{1}_{2:yyMMddHHmmssff}_{3}{4}", userName, dataFieldName, DateTime.Now, recordSorting, Path.GetExtension(srouceFileName));
            }
            else
            {
                return string.Format("{0}_{1}_{2}_{3:yyMMddHHmmssff}{4}", userName, dataFieldName, recordSorting, DateTime.Now, Path.GetExtension(srouceFileName));
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <param name="mirror"></param>
        /// <returns></returns>
        private Dictionary<decimal, List<RecordItem>> Process(CommonUserInfo commonUserInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities, bool mirror)
        {
            /* 表单编号与记录对应关系 */
            Dictionary<decimal, List<RecordItem>> dicRecordItems = new Dictionary<decimal, List<RecordItem>>();

            foreach (KeyValuePair<decimal, List<RecordEntity>> keyValue in dicRecordEntities)
            {
                List<RecordItem> recordItems = Process(commonUserInfo, keyValue.Value, mirror);
                dicRecordItems.Add(keyValue.Key, recordItems);
            }

            return dicRecordItems;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="mirror"></param>
        /// <returns></returns>
        private List<RecordItem> Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, bool mirror)
        {
            List<RecordItem> recordItems = new List<RecordItem>();
            DataFieldRelationship dataFieldRelationship = new DataFieldRelationship();
            CustomTable customTable = new CustomTable();

            foreach (RecordEntity recordEntity in recordEntities)
            {
                if (recordEntity == null || recordEntity.CommonDataFields == null || recordEntity.CommonDataFields.Count == 0) continue;
                CustomTableInfo customTableInfo = customTable.GetModelInfo(recordEntity.TableId);
                DataTableType dataTableType = (DataTableType)customTableInfo.TableType;
                SqlDatabase dbBlue = null;
                if (mirror)
                {
                    dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                }
                else
                {
                    byte dataWarehouseId = customTable.GetDataWarehouseId(recordEntity.TableId);
                    dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                }
                int recordSorting = 0;
                if (recordEntity.RecordId <= 0)
                {
                    recordSorting = DataAccessHandler.GetMaxRecordSorting(dbBlue, customTableInfo.PhysicalName, commonUserInfo.UserId);
                }
                StringBuilder sb = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();
                if (recordEntity.RecordId > 0)
                {
                    sb.AppendFormat("UPDATE {0} SET UserId = @UserId, UserName = @UserName, DepId = @DepId, UserTypeId = @UserTypeId, ", customTableInfo.PhysicalName);
                    sb.Append("ModificationTime = @ModificationTime");
                }
                else
                {
                    sb.AppendFormat("INSERT INTO {0} (UserId, UserName, UserTypeId, DepID, RecordSorting, AuditedStatus, CurrentState, CreationTime, ModificationTime, IsDeleted", customTableInfo.PhysicalName);
                    sbValue.Append("VALUES (@UserId, @UserName, @UserTypeId, @DepID, @RecordSorting, @AuditedStatus, @CurrentState, @CreationTime, @ModificationTime, @IsDeleted");
                    if (recordEntity.BusinessEnabled)
                    {
                        if (DataConvertionHelper.IsNullValue(recordEntity.InstanceId))
                        {
                            throw new ArgumentException("业务编号不能为空。");
                        }
                        sb.Append(", BusinessId");
                        sbValue.Append(", @BusinessId");
                    }
                    if (recordEntity.BusinessEnabled && recordEntity.BusinessAlternativeId > 0)
                    {
                        sb.Append(", BusinessAlternativeId");
                        sbValue.Append(", @BusinessAlternativeId");
                    }
                }
                foreach (CommonDataField commonDataField in recordEntity.CommonDataFields)
                {
                    if (commonDataField.DataFieldDataType == DbType.Object)
                    {
                        /* 附件类型 */
                        UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                        if (upLoadFileInfo == null)
                        {
                            continue;
                        }
                        if (recordEntity.RecordId > 0)
                        {
                            recordSorting = DataAccessHandler.GetCurrentRecordSorting(dbBlue, customTableInfo.PhysicalName, recordEntity.RecordId);
                            /* 更新文件: 如果文件名不为空，内容为空，则表示文件内容不变 */
                            if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName))
                            {
                                upLoadFileInfo.UpLoadFileName = GenerateFileName(commonUserInfo.UserName, commonDataField.DataFieldName, recordSorting, upLoadFileInfo.UpLoadFileName, mirror);
                                SaveUploadFiles(upLoadFileInfo, commonDataField.DataFieldName);
                            }
                            else
                            {
                                /* 删除文件：如果文件名为空，源名称不为空，则表示删除源文件 */
                                if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                                {
                                    int count = 0;
                                    //查询语句
                                    string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", customTableInfo.PhysicalName, commonDataField.DataFieldName);
                                    using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sqlSelect))
                                    {
                                        //给参数赋值
                                        dbBlue.AddInParameter(dbCommand, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadSourceFileName);
                                        count = DataConvertionHelper.GetInt(dbBlue.ExecuteScalar(dbCommand));
                                    }
                                    if (count <= 1)
                                    {
                                        DeleteUploadFiles(commonDataField.DataFieldName, upLoadFileInfo.UpLoadSourceFileName);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName) && string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                            {
                                throw new ArgumentException("文件名为空。");
                            }                            
                            if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadSourceFileName))
                            {
                                /* 仅增加文件名，例如个人信息更新时，临时增加的记录中有附件字段内容保存不变 */
                                upLoadFileInfo.UpLoadFileName = upLoadFileInfo.UpLoadSourceFileName;
                            }
                            else
                            {
                                /* 仅增加文件名和文件内容 */
                                upLoadFileInfo.UpLoadFileName = GenerateFileName(commonUserInfo.UserName, commonDataField.DataFieldName, recordSorting, upLoadFileInfo.UpLoadFileName, mirror);
                                SaveUploadFiles(upLoadFileInfo, commonDataField.DataFieldName);
                            }
                        }
                    }
                    if (recordEntity.RecordId > 0)
                    {
                        sb.AppendFormat(", {0} = @{0}", commonDataField.DataFieldName);
                    }
                    else
                    {
                        sb.AppendFormat(", {0}", commonDataField.DataFieldName);
                        sbValue.AppendFormat(", @{0}", commonDataField.DataFieldName);
                    }
                }
                if (recordEntity.RecordId > 0)
                {
                    sb.Append(" WHERE RecordId = @RecordId");
                }
                else
                {
                    sb.AppendFormat(") {0});SET @RecordId = SCOPE_IDENTITY()", sbValue);
                }
                using (DbConnection conn = dbBlue.CreateConnection())
                {
                    conn.Open();
                    DbTransaction trans = conn.BeginTransaction();
                    try
                    {
                        if (recordEntity.RecordId <= 0 && (dataTableType == DataTableType.MasterSlaveTable))
                        {
                            if (recordEntity.BusinessEnabled)
                            {
                                UpdateCurrentStateByBusinessId(recordEntity.InstanceId, (byte)CurrentState.History, customTableInfo.PhysicalName, dbBlue, trans);
                            }
                            else
                            {
                                UpdateCurrentStateByUserId(commonUserInfo.UserId, (byte)CurrentState.History, customTableInfo.PhysicalName, dbBlue, trans);
                            }
                        }
                        using (DbCommand cmd = dbBlue.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            if (recordEntity.RecordId > 0)
                            {
                                dbBlue.AddInParameter(cmd, "RecordId", DbType.Decimal, recordEntity.RecordId);
                            }
                            else
                            {
                                dbBlue.AddOutParameter(cmd, "RecordId", DbType.Decimal, 10);
                                if (recordEntity.BusinessEnabled)
                                {
                                    dbBlue.AddInParameter(cmd, "BusinessId", DbType.Decimal, recordEntity.InstanceId);
                                }
                                if (recordEntity.BusinessEnabled && recordEntity.BusinessAlternativeId > 0)
                                {
                                    dbBlue.AddInParameter(cmd, "BusinessAlternativeId", DbType.Decimal, recordEntity.BusinessAlternativeId);
                                }
                                dbBlue.AddInParameter(cmd, "RecordSorting", DbType.Int32, recordSorting);
                                dbBlue.AddInParameter(cmd, "AuditedStatus", DbType.Byte, (byte)AuditedStatus.None);
                                dbBlue.AddInParameter(cmd, "CurrentState", DbType.Byte, (byte)recordEntity.CurrentState);
                                dbBlue.AddInParameter(cmd, "IsDeleted", DbType.Boolean, false);
                            }
                            dbBlue.AddInParameter(cmd, "UserId", DbType.Decimal, commonUserInfo.UserId);
                            dbBlue.AddInParameter(cmd, "UserName", DbType.String, commonUserInfo.UserName);
                            dbBlue.AddInParameter(cmd, "UserTypeId", DbType.Decimal, commonUserInfo.UserTypeId);
                            dbBlue.AddInParameter(cmd, "DepId", DbType.Decimal, commonUserInfo.DepId);
                            if (recordEntity.RecordId <= 0)
                            {
                                dbBlue.AddInParameter(cmd, "CreationTime", DbType.DateTime, DateTime.Now);
                            }
                            dbBlue.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                            foreach (CommonDataField commonDataField in recordEntity.CommonDataFields)
                            {
                                if (commonDataField.DataFieldDataType == DbType.Object)
                                {
                                    /* 附件类型 */
                                    UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                                    if (upLoadFileInfo != null)
                                    {
                                        DataAccessHandler.AddInParameter(dbBlue, cmd, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadFileName);
                                    }
                                }
                                else
                                {
                                    if (commonDataField.DataFieldValue == null)
                                    {
                                        commonDataField.DataFieldValue = DBNull.Value;
                                    }
                                    DataAccessHandler.AddInParameter(dbBlue, cmd, commonDataField.DataFieldName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
                                }
                            }
                            //执行插入操作
                            if (dbBlue.ExecuteNonQuery(cmd, trans) < 1)
                            {
                                throw new Exception("插入失败！");
                            }
                            if (recordEntity.RecordId <= 0)
                            {
                                recordEntity.RecordId = DataConvertionHelper.GetDecimal(cmd.Parameters["@RecordId"].Value, 0);
                            }
                        }
                        if (recordEntity.RelaitonDataFields.Count > 0)
                        {
                            UpdateRelationDataFields(commonUserInfo.UserId, recordEntity.InstanceId, recordEntity.RelaitonDataFields, recordEntity.BusinessEnabled, dbBlue, trans);
                        }
                        trans.Commit();
                    }
                    catch (Exception exception)
                    {
                        trans.Rollback();
                        //记录日志, 抛出异常, 不包装异常 
                        ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                    }
                }
                recordItems.Add(new RecordItem(recordEntity.TableId, recordEntity.RecordId, recordEntity.CurrentState));
            }

            return recordItems;
        }
        
        /// <summary>
        /// 删除上传的文件
        /// </summary>
        /// <param name="dataFieldName"></param>
        /// <param name="fileName"></param>
        public void DeleteUploadFiles(string dataFieldName, string fileName)
        {
            try
            {
                StringBuilder sbPath = new StringBuilder();
                sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbPath.Append(@"\");
                }
                sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, dataFieldName);
                sbPath.Append(fileName);
                if (File.Exists(sbPath.ToString()))
                {
                    File.Delete(sbPath.ToString());
                }
            }
            catch { }
        }
        
        /// <summary>
        /// 更新联系字段
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <param name="commonDataFields"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateRelationDataFields(decimal userId, decimal instanceId, IList<CommonDataField> commonDataFields, bool businessEnabled, SqlDatabase db, DbTransaction transaction)
        {
            Dictionary<decimal, IList<CommonDataField>> dicCommonDataFields = new Dictionary<decimal, IList<CommonDataField>>();
            CustomTable customTable = new CustomTable();
            CustomDataField customDataField = new CustomDataField();
            foreach (CommonDataField commonDataField in commonDataFields)
            {
                IList<CommonDataField> dataFields = null;
                decimal tableId = customDataField.GetTableId(commonDataField.DataFieldId);
                if (dicCommonDataFields.ContainsKey(tableId))
                {
                    dataFields = dicCommonDataFields[tableId];
                }
                else
                {
                    dataFields = new List<CommonDataField>();
                    dicCommonDataFields.Add(tableId, dataFields);
                }
                dataFields.Add(commonDataField);
            }
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<decimal, IList<CommonDataField>> keyValue in dicCommonDataFields)
            {
                CustomTableInfo customTableInfo = customTable.GetModelInfo(keyValue.Key);
                sb.Clear();
                sb.AppendFormat("UPDATE {0} SET ", customTableInfo.PhysicalName);
                if (keyValue.Value.Count == 0)
                {
                    continue;
                }
                foreach (CommonDataField commonDataField in keyValue.Value)
                {
                    sb.AppendFormat("{0} = @{1}, ", commonDataField.DataFieldName, commonDataField.DataFieldParameterName);
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" WHERE ");
                DataTableType dataTableType = (DataTableType)customTableInfo.TableType;
                switch (dataTableType)
                {
                    case DataTableType.PrimaryTable:
                    case DataTableType.AssistanTable:
                        sb.Append("UserId = @UserId ");
                        break;

                    case DataTableType.MasterSlaveTable:
                        sb.Append("UserId = @UserId AND CurrentState = @CurrentState ");
                        break;
                };
                if (businessEnabled)
                {
                    sb.Append("AND BusinessId = @BusinessId");
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    if (dataTableType == DataTableType.MasterSlaveTable)
                    {
                        db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.Current);
                    }
                    if (businessEnabled)
                    {
                        db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, instanceId);
                    }
                    foreach (CommonDataField commonDataField in keyValue.Value)
                    {
                        if (commonDataField.DataFieldDataType == DbType.Object)
                        {
                            /* 附件类型 */
                            UpLoadFileInfo upLoadFileInfo = commonDataField.DataFieldValue as UpLoadFileInfo;
                            DataAccessHandler.AddInParameter(db, dbCommand, commonDataField.DataFieldName, DbType.String, upLoadFileInfo.UpLoadFileName);
                        }
                        else
                        {
                            if (commonDataField.DataFieldValue == null)
                            {
                                commonDataField.DataFieldValue = DBNull.Value;
                            }
                            DataAccessHandler.AddInParameter(db, dbCommand, commonDataField.DataFieldName, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);
                        }
                    }
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="currentState"></param>
        /// <param name="physicalName"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateCurrentStateByBusinessId(decimal businessId, byte currentState, string physicalName, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET CurrentState = @CurrentState ", physicalName);
            sb.Append("WHERE BusinessId = @BusinessId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, businessId);
                    db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, currentState);

                    //执行更新操作             
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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

        #endregion
    }
}
