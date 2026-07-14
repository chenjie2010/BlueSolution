//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomBusiness.cs
// 描述：CustomBusiness 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/12/20
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;
using Blue.SQLServerDAL.SystemModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomBusiness 表的数据层访问类
    /// </summary>
    public class CustomBusiness : CommonNodeDataAccess, ICustomBusiness
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomBusiness() : base("CustomBusiness", "BusinessId", "MenuId", "BusinessName", "BusinessCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo)
        {
            //自动增加的关键字的值
            decimal customBusinessId = 0;

            try
            {
                customBusinessId = Insert(customBusinessInfo, null, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customBusinessId;
        }

        /// <summary>
		/// 获得 CustomBusinessInfo 对象
		/// </summary>
		///<param name="businessId">业务编号</param>
		/// <returns> CustomBusinessInfo 对象</returns>
		public CustomBusinessInfo GetModelInfo(decimal businessId)
        {
            CustomBusinessInfo customBusinessInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, businessId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomBusinessInfo> customBusinessInfos = GetModeInfos(whereConditons, null, true);
            if (customBusinessInfos != null && customBusinessInfos.Count > 0)
            {
                customBusinessInfo = customBusinessInfos[0];
            }

            return customBusinessInfo;
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        public void Update(CustomBusinessInfo customBusinessInfo)
        {

        }

        /// <summary>
        ///  删除 CustomBusinessInfo 对象
        /// </summary>
        ///<param name="businessId">业务编号</param>
        public void Delete(decimal businessId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomBusiness ");
            sb.Append("WHERE BusinessId = @BusinessId");
            RoleAndBusiness roleAndBusiness = new RoleAndBusiness();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    roleAndBusiness.Delete(businessId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, businessId);
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
        /// 获得 CustomBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomBusinessInfo 对象列表</returns>
        public IList<CustomBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomBusiness 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomBusinessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomBusiness ", "BusinessId", false, whereConditons);
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
        /// 根据条件查询业务数量
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
		public int GetTotalCount(decimal conditionId, BusinessMenu businessMenu)
        {            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            switch(businessMenu)
            {
                case BusinessMenu.Report:
                    whereConditons.Add(new WhereConditon("ReportId", "ReportId", DbType.Decimal, conditionId, DataFieldCondition.Equal));
                    break;

                    //case BusinessMenu.DataFilled:
                    //    break;

                    //case BusinessMenu.Query:
                    //    whereConditons.Add(new WhereConditon("DataQueriedId", "DataQueriedId", DbType.Decimal, conditionId, DataFieldCondition.Equal));
                    //    break;

            }            

            return GetTotalCount(whereConditons);
        }

        /// <summary>
        /// 通过数据填报实例编号获取业务名称
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public string GetBusinessNameByInstanceId(decimal instanceId)
        {
            string businessName = string.Empty;

            try
            {
                string sqlSelect = "SELECT BusinessName FROM CustomBusiness INNER JOIN  BusinessInstance ON BusinessInstance.DataId = CustomBusiness.DataId WHERE InstanceId = @InstanceId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    businessName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessName;
        }

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId, decimal menuId)
        {
            IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos = new List<ExtendedCustomBusinessInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomBusiness.BusinessId, CustomBusiness.WorkflowId, CustomBusiness.ReportId, CustomBusiness.DataAuditingId, CustomBusiness.DataId, CustomBusiness.DataQueriedId, ");
            sb.Append("CustomBusiness.BusinessName, CustomBusiness.BusinessCode, CustomBusiness.BusinessMenu, CustomBusiness.CustomBusinessName, CustomBusiness.BusinessIntro, CustomBusiness.EnableHelp, ");
            sb.Append("CustomBusiness.HelpContent, CustomBusiness.IconType, CustomBusiness.BusinessIcon, CustomBusiness.IconName, CustomBusiness.IconPath, CustomBusiness.BusinessURL, CustomBusiness.Sorting, ");
            sb.Append("CustomBusiness.Notes, RoleAndBusiness.ThirdModeEnabled, RoleAndBusiness.InitializedDate, RoleAndBusiness.ExpiredDate FROM RoleAndBusiness ");
            sb.Append("INNER JOIN CustomBusiness ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndBusiness.RoleId ");
            sb.Append("INNER JOIN CustomMenu ON CustomMenu.MenuId = CustomBusiness.MenuId ");
            sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomBusiness.MenuId = @MenuId AND RoleAndBusiness.BusinessEnabled = @BusinessEnabled ");
            sb.Append("ORDER BY CustomBusiness.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, menuId);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, true);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal businessId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal dataQueriedId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string businessName = DataConvertionHelper.GetString(dataReader[6]);
                            string businessCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte businessMenu = DataConvertionHelper.GetByte(dataReader[8]);
                            byte customBusinessName = DataConvertionHelper.GetByte(dataReader[9]);
                            string businessIntro = DataConvertionHelper.GetString(dataReader[10]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[11]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[12]);
                            byte iconType = DataConvertionHelper.GetByte(dataReader[13]);
                            byte businessIcon = DataConvertionHelper.GetByte(dataReader[14]);
                            string iconName = DataConvertionHelper.GetString(dataReader[15]);
                            string iconPath = DataConvertionHelper.GetString(dataReader[16]);
                            string businessURL = DataConvertionHelper.GetString(dataReader[17]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[18]);
                            string notes = DataConvertionHelper.GetString(dataReader[19]);
                            bool thirdModeEnabled = DataConvertionHelper.GetBoolean(dataReader[20]);
                            DateTime initializedDate = DataConvertionHelper.GetDateTime(dataReader[21]);
                            DateTime expiredDate = DataConvertionHelper.GetDateTime(dataReader[22]);
                            bool businessEnabled = ((DataConvertionHelper.IsNullValue(initializedDate) || initializedDate <= DateTime.Now)
                                && (DataConvertionHelper.IsNullValue(expiredDate) || expiredDate >= DateTime.Now));
                            //将创建 CustomBusinessInfo 对象加入集合中
                            extendedCustomBusinessInfos.Add(new ExtendedCustomBusinessInfo(businessId, menuId, workflowId, reportId, dataAuditingId, dataId,
                            dataQueriedId, businessName, businessCode, businessMenu, customBusinessName, businessIntro, enableHelp, helpContent,
                            iconType, businessIcon, iconName, iconPath, businessURL, sorting, notes, businessEnabled, thirdModeEnabled,
                            initializedDate, expiredDate));
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

            return extendedCustomBusinessInfos;
        }

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId)
        {
            IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos = new List<ExtendedCustomBusinessInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomBusiness.BusinessId, CustomBusiness.MenuId, CustomBusiness.WorkflowId, CustomBusiness.ReportId, CustomBusiness.DataAuditingId, CustomBusiness.DataId, CustomBusiness.DataQueriedId, ");
            sb.Append("CustomBusiness.BusinessName, CustomBusiness.BusinessCode, CustomBusiness.BusinessMenu, CustomBusiness.CustomBusinessName, CustomBusiness.BusinessIntro, CustomBusiness.EnableHelp, ");
            sb.Append("CustomBusiness.HelpContent, CustomBusiness.IconType, CustomBusiness.BusinessIcon, CustomBusiness.IconName, CustomBusiness.IconPath, CustomBusiness.BusinessURL, CustomBusiness.Sorting, "); 
            sb.Append("CustomBusiness.Notes, RoleAndBusiness.ThirdModeEnabled, RoleAndBusiness.InitializedDate, RoleAndBusiness.ExpiredDate FROM RoleAndBusiness ");
            sb.Append("INNER JOIN CustomBusiness ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndBusiness.RoleId ");
            sb.Append("INNER JOIN CustomMenu ON CustomMenu.MenuId = CustomBusiness.MenuId ");
            sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndBusiness.BusinessEnabled = @BusinessEnabled ");
            sb.Append("ORDER BY CustomMenu.Sorting, CustomBusiness.Sorting");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, true);         
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal businessId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal menuId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal dataQueriedId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            string businessName = DataConvertionHelper.GetString(dataReader[7]);
                            string businessCode = DataConvertionHelper.GetString(dataReader[8]);
                            byte businessMenu = DataConvertionHelper.GetByte(dataReader[9]);
                            byte customBusinessName = DataConvertionHelper.GetByte(dataReader[10]);
                            string businessIntro = DataConvertionHelper.GetString(dataReader[11]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[12]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[13]);
                            byte iconType = DataConvertionHelper.GetByte(dataReader[14]);
                            byte businessIcon = DataConvertionHelper.GetByte(dataReader[15]);
                            string iconName = DataConvertionHelper.GetString(dataReader[16]);
                            string iconPath = DataConvertionHelper.GetString(dataReader[17]);
                            string businessURL = DataConvertionHelper.GetString(dataReader[18]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[19]);
                            string notes = DataConvertionHelper.GetString(dataReader[20]);
                            bool thirdModeEnabled = DataConvertionHelper.GetBoolean(dataReader[21]);
                            DateTime initializedDate = DataConvertionHelper.GetDateTime(dataReader[22]);
                            DateTime expiredDate = DataConvertionHelper.GetDateTime(dataReader[23]);
                            bool businessEnabled = ((DataConvertionHelper.IsNullValue(initializedDate) || initializedDate <= DateTime.Now)
                                && (DataConvertionHelper.IsNullValue(expiredDate) || expiredDate >= DateTime.Now));
                            //将创建 CustomBusinessInfo 对象加入集合中
                            extendedCustomBusinessInfos.Add(new ExtendedCustomBusinessInfo(businessId, menuId, workflowId, reportId, dataAuditingId, dataId,
                            dataQueriedId, businessName, businessCode, businessMenu, customBusinessName, businessIntro, enableHelp, helpContent,
                            iconType, businessIcon, iconName, iconPath, businessURL, sorting, notes, businessEnabled, thirdModeEnabled, 
                            initializedDate, expiredDate));
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

            return extendedCustomBusinessInfos;
        }

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            //自动增加的关键字的值
            decimal customBusinessId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customBusinessInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomBusiness", "Sorting", "MenuId", customBusinessInfo.MenuId, 0) + 1;
            customBusinessInfo.IconName = GetFormmattedIconName(customBusinessInfo.MenuId < 0 ? 0 : customBusinessInfo.MenuId, customBusinessInfo.Sorting, customBusinessInfo.IconName);

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomBusiness(MenuId, WorkflowId, ReportId, DataAuditingId, DataId, DataQueriedId, ");
            sb.Append("BusinessName, BusinessCode, BusinessMenu, CustomBusinessName, BusinessIntro, ");
            sb.Append("EnableHelp, HelpContent, IconType, BusinessIcon, IconName, IconPath, BusinessURL, Sorting, Notes)");
            sb.Append("VALUES (@MenuId, @WorkflowId, @ReportId, @DataAuditingId, @DataId, @DataQueriedId, ");
            sb.Append("@BusinessName, @BusinessCode, @BusinessMenu, @CustomBusinessName, @BusinessIntro, ");
            sb.Append("@EnableHelp, @HelpContent, @IconType, @BusinessIcon, @IconName, @IconPath, @BusinessURL, @Sorting, @Notes);");
            sb.Append("SET @BusinessId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "BusinessId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, customBusinessInfo.MenuId);
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.WorkflowId));
                        db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.ReportId));
                        db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataAuditingId));
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataId));
                        db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataQueriedId));
                        db.AddInParameter(dbCommand, "BusinessName", DbType.String, customBusinessInfo.BusinessName);
                        db.AddInParameter(dbCommand, "BusinessCode", DbType.String, customBusinessInfo.BusinessCode);
                        db.AddInParameter(dbCommand, "BusinessMenu", DbType.Byte, customBusinessInfo.BusinessMenu);
                        db.AddInParameter(dbCommand, "CustomBusinessName", DbType.Byte, customBusinessInfo.CustomBusinessName);
                        db.AddInParameter(dbCommand, "BusinessIntro", DbType.String, customBusinessInfo.BusinessIntro);
                        db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customBusinessInfo.EnableHelp);
                        db.AddInParameter(dbCommand, "HelpContent", DbType.String, customBusinessInfo.HelpContent);
                        db.AddInParameter(dbCommand, "IconType", DbType.Byte, customBusinessInfo.IconType);
                        db.AddInParameter(dbCommand, "BusinessIcon", DbType.Byte, customBusinessInfo.BusinessIcon);
                        db.AddInParameter(dbCommand, "IconName", DbType.String, customBusinessInfo.IconName);
                        db.AddInParameter(dbCommand, "IconPath", DbType.String, customBusinessInfo.IconPath);
                        db.AddInParameter(dbCommand, "BusinessURL", DbType.String, customBusinessInfo.BusinessURL);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customBusinessInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customBusinessInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customBusinessId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@BusinessId"].Value, 0);                        
                    }
                    CustomMenu customMenu = new CustomMenu();
                    customMenu.UpdateLeafOfParentNode(customBusinessInfo.MenuId, false, db, transaction);

                    /* 插入附件 */
                    if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                    {
                        PriavteAttachment messageAttachment = new PriavteAttachment();
                        messageAttachment.Insert(customBusinessId, (byte)AttachmentCategory.MenuBusiness, upLoadFileInfos, db, transaction);
                    }
                    if (imageData != null)
                    {
                        FileSavedHelper.SaveIcons(customBusinessInfo.IconName, imageData);
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

            return customBusinessId;
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomBusiness SET MenuId = @MenuId, WorkflowId = @WorkflowId, ReportId = @ReportId, ");
            sb.Append("DataAuditingId = @DataAuditingId, DataId = @DataId, DataQueriedId = @DataQueriedId, BusinessName = @BusinessName, ");
            sb.Append("BusinessCode = @BusinessCode, BusinessMenu = @BusinessMenu, CustomBusinessName = @CustomBusinessName, ");
            sb.Append("BusinessIntro = @BusinessIntro, EnableHelp = @EnableHelp, HelpContent = @HelpContent, ");
            sb.Append("IconType = @IconType, BusinessIcon = @BusinessIcon, IconName = @IconName, IconPath = @IconPath, ");
            sb.Append("BusinessURL = @BusinessURL, Notes = @Notes ");
            sb.Append("WHERE BusinessId = @BusinessId");
            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customBusinessInfo.Sorting = DataAccessHandler.GetValueOfDataField(db, "CustomBusiness", "Sorting", "BusinessId", customBusinessInfo.BusinessId, 0);
            customBusinessInfo.IconName = GetFormmattedIconName(customBusinessInfo.MenuId < 0 ? 0 : customBusinessInfo.MenuId, customBusinessInfo.Sorting, customBusinessInfo.IconName);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, customBusinessInfo.BusinessId);
                        db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, customBusinessInfo.MenuId);
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.WorkflowId));
                        db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.ReportId));
                        db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataAuditingId));
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataId));
                        db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(customBusinessInfo.DataQueriedId));
                        db.AddInParameter(dbCommand, "BusinessName", DbType.String, customBusinessInfo.BusinessName);
                        db.AddInParameter(dbCommand, "BusinessCode", DbType.String, customBusinessInfo.BusinessCode);
                        db.AddInParameter(dbCommand, "BusinessMenu", DbType.Byte, customBusinessInfo.BusinessMenu);
                        db.AddInParameter(dbCommand, "CustomBusinessName", DbType.Byte, customBusinessInfo.CustomBusinessName);
                        db.AddInParameter(dbCommand, "BusinessIntro", DbType.String, customBusinessInfo.BusinessIntro);
                        db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customBusinessInfo.EnableHelp);
                        db.AddInParameter(dbCommand, "HelpContent", DbType.String, customBusinessInfo.HelpContent);
                        db.AddInParameter(dbCommand, "IconType", DbType.Byte, customBusinessInfo.IconType);
                        db.AddInParameter(dbCommand, "BusinessIcon", DbType.Byte, customBusinessInfo.BusinessIcon);                       
                        db.AddInParameter(dbCommand, "IconName", DbType.String, customBusinessInfo.IconName);
                        db.AddInParameter(dbCommand, "IconPath", DbType.String, customBusinessInfo.IconPath);
                        db.AddInParameter(dbCommand, "BusinessURL", DbType.String, customBusinessInfo.BusinessURL);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customBusinessInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(customBusinessInfo.MenuId, (byte)AttachmentCategory.MenuBusiness, upLoadFileInfos, db, transaction);
                    if (imageData != null)
                    {
                        FileSavedHelper.SaveIcons(customBusinessInfo.IconName, imageData);
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
        /// 获得菜单的图标名称
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> 图标名称</returns>
        public string GetIconName(decimal businessId)
        {
            string iconName = string.Empty;

            try
            {
                string sqlSelect = "SELECT IconName FROM CustomBusiness WHERE BusinessId = @BusinessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, DataConvertionHelper.SetDecimal(businessId));
                    iconName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return iconName;
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
            sb.Append("DELETE RoleAndBusiness FROM RoleAndBusiness INNER JOIN CustomBusiness ");
            sb.Append("ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN DataAuditing  ON DataAuditing.DataAuditingId = CustomBusiness.DataAuditingId ");
            sb.Append("WHERE TableId = @TableId;");
            sb.Append("DELETE CustomBusiness FROM CustomBusiness INNER JOIN DataAuditing ");
            sb.Append("ON DataAuditing.DataAuditingId = CustomBusiness.DataAuditingId ");
            sb.Append("WHERE DataAuditing.TableId = @TableId ");

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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomBusinessInfo 对象列表</returns>
        private IList<CustomBusinessInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomBusinessInfo>  customBusinessInfos = new List<CustomBusinessInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM CustomBusiness");
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
                            decimal businessId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal menuId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal dataQueriedId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            string businessName = DataConvertionHelper.GetString(dataReader[7]);
                            string businessCode = DataConvertionHelper.GetString(dataReader[8]);
                            byte businessMenu = DataConvertionHelper.GetByte(dataReader[9]);
                            byte customBusinessName = DataConvertionHelper.GetByte(dataReader[10]);
                            string businessIntro = DataConvertionHelper.GetString(dataReader[11]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[12]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[13]);
                            byte iconType = DataConvertionHelper.GetByte(dataReader[14]);
                            byte businessIcon = DataConvertionHelper.GetByte(dataReader[15]);
                            string iconName = DataConvertionHelper.GetString(dataReader[16]);
                            string iconPath = DataConvertionHelper.GetString(dataReader[17]);
                            string businessURL = DataConvertionHelper.GetString(dataReader[18]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[19]);
                            string notes = DataConvertionHelper.GetString(dataReader[20]);
                            //将创建 CustomBusinessInfo 对象加入集合中
                            customBusinessInfos.Add(new CustomBusinessInfo(businessId, menuId, workflowId, reportId, dataAuditingId,
                            dataId, dataQueriedId, businessName, businessCode, businessMenu,
                            customBusinessName, businessIntro, enableHelp, helpContent, iconType,
                            businessIcon, iconName, iconPath, businessURL, sorting, notes));
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
            
			return customBusinessInfos;
		} 
        
        /// <summary>
		/// 获得 CustomBusinessInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomBusinessInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomBusiness");
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
        /// 获得表 CustomBusiness 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomBusiness ", "BusinessId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomBusiness 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomBusiness ", "BusinessId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomBusiness 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomBusiness ", "BusinessId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomBusiness 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomBusiness ", "BusinessId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomBusinessInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomBusiness");
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

        /// <summary>
        /// 获得格式化后的图片名称
        /// </summary>
        /// <param name="parentMenuId"></param>
        /// <param name="sorting"></param>
        /// <param name="originalIconName"></param>
        /// <returns></returns>
        private string GetFormmattedIconName(decimal parentMenuId, int sorting, string originalIconName)
        {
            /* Path.GetExtension返回的后缀名带 '.' */
            return string.Format("Business_{0}_{1}{2}", parentMenuId, sorting, Path.GetExtension(originalIconName));
        }

        #endregion

        #endregion
    }
}
