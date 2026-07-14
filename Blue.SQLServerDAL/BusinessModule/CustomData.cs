//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomData.cs
// 描述：CustomData 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/11/27
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomData 表的数据层访问类
    /// </summary>
    public class CustomData : CommonNodeDataAccess, ICustomData
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomData() : base("CustomData", "DataId", "GroupId", "DataName", "DataCode",true, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomDataInfo customDataInfo)
        {
            //自动增加的关键字的值
            decimal customDataId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                customDataId = Insert(customDataInfo, null, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataId;
        }

        /// <summary>
		/// 获得 CustomDataInfo 对象
		/// </summary>
		///<param name="dataId">数据填报编号</param>
		/// <returns> CustomDataInfo 对象</returns>
		public CustomDataInfo GetModelInfo(decimal dataId)
        {
            CustomDataInfo customDataInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataId", "DataId", DbType.Decimal, dataId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomDataInfo> customDataInfos = GetModelInfos(whereConditons, null, true);
            if (customDataInfos != null && customDataInfos.Count > 0)
            {
                customDataInfo = customDataInfos[0];
            }

            return customDataInfo;
        }

        /// <summary>
        /// 更新 CustomDataInfo 对象
        /// </summary>
        /// <param name="customDataInfo">CustomDataInfo 对象</param>
        public void Update(CustomDataInfo customDataInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomData SET TableId = @TableId, ReportId = @ReportId, RoleId = @RoleId, ");
            sb.Append("GroupId = @GroupId, ParentReportId = @ParentReportId, ParentRoleId = @ParentRoleId, ");
            sb.Append("ViewId = @ViewId, DataName = @DataName, DataCode = @DataCode, ");
            sb.Append("DataFilledType = @DataFilledType, DataShowMode = @DataShowMode, DataInputMode = @DataInputMode, ");
            sb.Append("ClientFormHeight = @ClientFormHeight, WebFormHeight = @WebFormHeight, IsInitReview = @IsInitReview, ");
            sb.Append("EnableManager = @EnableManager, IsFinalReview = @IsFinalReview, DataFilledProperty = @DataFilledProperty, ");
            sb.Append("EnableGuidance = @EnableGuidance, Guidance = @Guidance, ConditionEnabled = @ConditionEnabled, ");
            sb.Append("ConditionContent = @ConditionContent, ConditionType = @ConditionType, Notes = @Notes ");
            sb.Append("WHERE DataId = @DataId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customDataInfo.DataId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.TableId));
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ReportId));
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.RoleId));
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customDataInfo.GroupId);
                    db.AddInParameter(dbCommand, "ParentReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentReportId));
                    db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentRoleId));
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ViewId));
                    db.AddInParameter(dbCommand, "DataName", DbType.String, customDataInfo.DataName);
                    db.AddInParameter(dbCommand, "DataCode", DbType.String, customDataInfo.DataCode);
                    db.AddInParameter(dbCommand, "DataFilledType", DbType.Byte, customDataInfo.DataFilledType);
                    db.AddInParameter(dbCommand, "DataShowMode", DbType.Byte, customDataInfo.DataShowMode);
                    db.AddInParameter(dbCommand, "DataInputMode", DbType.Byte, customDataInfo.DataInputMode);
                    db.AddInParameter(dbCommand, "ClientFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.ClientFormHeight));
                    db.AddInParameter(dbCommand, "WebFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.WebFormHeight));
                    db.AddInParameter(dbCommand, "IsInitReview", DbType.Boolean, customDataInfo.IsInitReview);
                    db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, customDataInfo.EnableManager);
                    db.AddInParameter(dbCommand, "IsFinalReview", DbType.Boolean, customDataInfo.IsFinalReview);
                    db.AddInParameter(dbCommand, "DataFilledProperty", DbType.Byte, customDataInfo.DataFilledProperty);
                    db.AddInParameter(dbCommand, "EnableGuidance", DbType.Boolean, customDataInfo.EnableGuidance);
                    db.AddInParameter(dbCommand, "Guidance", DbType.String, customDataInfo.Guidance);
                    db.AddInParameter(dbCommand, "ConditionEnabled", DbType.Boolean, customDataInfo.ConditionEnabled);
                    db.AddInParameter(dbCommand, "ConditionContent", DbType.String, customDataInfo.ConditionContent);
                    db.AddInParameter(dbCommand, "ConditionType", DbType.Byte, customDataInfo.ConditionType);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDataInfo.Notes);
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
        ///  删除 CustomDataInfo 对象
        /// </summary>
        ///<param name="dataId">数据填报编号</param>
        public void Delete(decimal dataId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomData ");
            sb.Append("WHERE DataId = @DataId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        CustomSection customSection = new CustomSection();
                        customSection.Delete(dataId, db, transaction);
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, "DataId", DbType.Decimal, dataId);
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
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomDataInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataInfo 对象列表</returns>
        public IList<CustomDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomData 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDataInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomData ", "DataId", false, whereConditons);
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
        /// 获得填报属性
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public byte GetDataFilledProperty(decimal dataId)
        {
            byte dataFilledProperty = 0;

            try
            {
                string sqlSelect = "SELECT DataFilledProperty FROM CustomData WHERE DataId = @DataId";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, dataId);
                    dataFilledProperty = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFilledProperty;
        }

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataId, decimal userId)
        {
            Dictionary<decimal, string> reviewers = new Dictionary<decimal, string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleAndUser.UserId, A.UserName, A.UserActualName FROM CustomData INNER JOIN RoleAndUser ");
            sb.Append("ON CustomData.RoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN UserAccount A ON A.UserId = RoleAndUser.UserId INNER JOIN UserAccount B ON B.DepId = A.DepId WHERE CustomData.DataId = @DataId AND B.UserId = @UserId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, dataId);
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
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataId)
        {
            Dictionary<decimal, string> reviewers = new Dictionary<decimal, string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleAndUser.UserId, UserAccount.UserName, UserAccount.UserActualName FROM CustomData INNER JOIN RoleAndUser ");
            sb.Append("ON CustomData.ParentRoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = RoleAndUser.UserId WHERE CustomData.DataId = @DataId");
        
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, dataId);
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
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomDataInfo customDataInfo = GetModelInfo(nodeId);
            if (customDataInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customDataInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customDataInfo.DataName);
            }

            return names;
        }

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal customDataId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customDataId = Insert(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customDataId;
        }

        /// <summary>
        /// 更新数据填报和附件信息
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos"></param>
        public void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos, db, transaction);
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

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 根据当前实例编号，获得数据填报业务初审状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        public bool GetInitReview(decimal instanceId)
        {
            bool isInitReview = false;

            try
            {
                string sqlSelect = "SELECT IsInitReview FROM CustomData INNER JOIN BusinessInstance ON CustomData.DataId=BusinessInstance.DataId WHERE InstanceId = @InstanceId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    isInitReview = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isInitReview;
        }

        /// <summary>
        /// 根据当前实例编号，获得数据填报业务终审状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        public bool GetFinalReview(decimal instanceId)
        {
            bool isFinalReview = false;

            try
            {
                string sqlSelect = "SELECT IsFinalReview FROM CustomData INNER JOIN BusinessInstance ON CustomData.DataId=BusinessInstance.DataId WHERE InstanceId = @InstanceId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    isFinalReview = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isFinalReview;
        }
        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomDataInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomDataInfo 对象列表</returns>
        private IList<CustomDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomDataInfo> customDataInfos = new List<CustomDataInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomData");
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
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal parentReportId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[7]);
                            string dataName = DataConvertionHelper.GetString(dataReader[8]);
                            string dataCode = DataConvertionHelper.GetString(dataReader[9]);
                            byte dataFilledType = DataConvertionHelper.GetByte(dataReader[10]);
                            byte dataShowMode = DataConvertionHelper.GetByte(dataReader[11]);
                            byte dataInputMode = DataConvertionHelper.GetByte(dataReader[12]);
                            int clientFormHeight = DataConvertionHelper.GetInt(dataReader[13]);
                            int webFormHeight = DataConvertionHelper.GetInt(dataReader[14]);
                            bool isInitReview = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool isFinalReview = DataConvertionHelper.GetBoolean(dataReader[17]);
                            byte dataFilledProperty = DataConvertionHelper.GetByte(dataReader[18]);
                            bool enableGuidance = DataConvertionHelper.GetBoolean(dataReader[19]);
                            string guidance = DataConvertionHelper.GetString(dataReader[20]);
                            bool conditionEnabled = DataConvertionHelper.GetBoolean(dataReader[21]);
                            string conditionContent = DataConvertionHelper.GetString(dataReader[22]);
                            byte conditionType = DataConvertionHelper.GetByte(dataReader[23]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[24]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[25]);
                            string notes = DataConvertionHelper.GetString(dataReader[26]);
                            //将创建 CustomDataInfo 对象加入集合中
                            customDataInfos.Add(new CustomDataInfo(dataId, tableId, reportId, roleId, groupId,
                            parentReportId, parentRoleId, viewId, dataName, dataCode,
                            dataFilledType, dataShowMode, dataInputMode, clientFormHeight, webFormHeight,
                            isInitReview, enableManager, isFinalReview, dataFilledProperty, enableGuidance,
                            guidance, conditionEnabled, conditionContent, conditionType, isLeaf,
                            sorting, notes));
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

            return customDataInfos;
        }

        /// <summary>
        /// 获得 CustomDataInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomData");
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
        /// 获得表 CustomData 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomData ", "DataId", "*", false, false, startPosition,
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
        /// 获得以表 CustomData 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomData ", "DataId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomData 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomData ", "DataId", "*", false, false, startPosition,
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
        /// 获得以表 CustomData 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomData ", "DataId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomDataInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomData");
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
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>自动增加的关键字的值</returns>
        private decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customDataId = 0;

            customDataInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomData", "Sorting", "GroupId", customDataInfo.GroupId, 0) + 1;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomData(TableId, ReportId, RoleId, GroupId, ParentReportId, ParentRoleId, ViewId, DataName, DataCode, DataFilledType, ");
            sb.Append("DataShowMode, DataInputMode, ClientFormHeight, WebFormHeight, IsInitReview, EnableManager, IsFinalReview, DataFilledProperty, EnableGuidance, Guidance, ConditionEnabled, ConditionContent, ");
            sb.Append("ConditionType, IsLeaf, Sorting, Notes)");
            sb.Append("VALUES (@TableId, @ReportId, @RoleId, @GroupId, @ParentReportId, @ParentRoleId, @ViewId, @DataName, @DataCode, @DataFilledType, ");
            sb.Append("@DataShowMode, @DataInputMode, @ClientFormHeight, @WebFormHeight, @IsInitReview, @EnableManager, @IsFinalReview, @DataFilledProperty, @EnableGuidance, @Guidance, @ConditionEnabled, @ConditionContent, ");
            sb.Append("@ConditionType, @IsLeaf, @Sorting, @Notes);");
            sb.Append("SET @DataId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "DataId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.TableId));
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ReportId));
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.RoleId));
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customDataInfo.GroupId);
                    db.AddInParameter(dbCommand, "ParentReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentReportId));
                    db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentRoleId));
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ViewId));                    
                    db.AddInParameter(dbCommand, "DataName", DbType.String, customDataInfo.DataName);
                    db.AddInParameter(dbCommand, "DataCode", DbType.String, customDataInfo.DataCode);
                    db.AddInParameter(dbCommand, "DataFilledType", DbType.Byte, customDataInfo.DataFilledType);
                    db.AddInParameter(dbCommand, "DataShowMode", DbType.Byte, customDataInfo.DataShowMode);
                    db.AddInParameter(dbCommand, "DataInputMode", DbType.Byte, customDataInfo.DataInputMode);
                    db.AddInParameter(dbCommand, "ClientFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.ClientFormHeight));
                    db.AddInParameter(dbCommand, "WebFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.WebFormHeight));
                    db.AddInParameter(dbCommand, "IsInitReview", DbType.Boolean, customDataInfo.IsInitReview);
                    db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, customDataInfo.EnableManager);
                    db.AddInParameter(dbCommand, "IsFinalReview", DbType.Boolean, customDataInfo.IsFinalReview);
                    db.AddInParameter(dbCommand, "DataFilledProperty", DbType.Byte, customDataInfo.DataFilledProperty);
                    db.AddInParameter(dbCommand, "EnableGuidance", DbType.Boolean, customDataInfo.EnableGuidance);
                    db.AddInParameter(dbCommand, "Guidance", DbType.String, customDataInfo.Guidance);
                    db.AddInParameter(dbCommand, "ConditionEnabled", DbType.Boolean, customDataInfo.ConditionEnabled);
                    db.AddInParameter(dbCommand, "ConditionContent", DbType.String, customDataInfo.ConditionContent);
                    db.AddInParameter(dbCommand, "ConditionType", DbType.Byte, customDataInfo.ConditionType);
                    db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customDataInfo.Sorting);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDataInfo.Notes);
                    /* 1. 执行插入记录操作 */
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    customDataId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataId"].Value, 0);
                }
                CustomGroup customGroup = new CustomGroup();
                customGroup.UpdateLeafOfParentNode(customDataInfo.GroupId, false, db, transaction);

                /* 2. 插入附件 */
                if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                {
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Insert(customDataId, (byte)AttachmentCategory.DataFilled, upLoadFileInfos, db, transaction);
                }

                /* 3. 插入条件附件 */
                if (conditionalUpLoadFileInfos != null && conditionalUpLoadFileInfos.Count > 0)
                {
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Insert(customDataId, (byte)AttachmentCategory.DataFilledCondition, conditionalUpLoadFileInfos, db, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataId;
        }
        
        /// <summary>
        /// 更新 CustomDataInfo 对象
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos, 
            SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomData SET TableId = @TableId, ReportId = @ReportId, RoleId = @RoleId, ");
            sb.Append("GroupId = @GroupId, ParentReportId = @ParentReportId, ParentRoleId = @ParentRoleId, ");
            sb.Append("ViewId = @ViewId, DataName = @DataName, DataCode = @DataCode, ");
            sb.Append("DataFilledType = @DataFilledType, DataShowMode = @DataShowMode, DataInputMode = @DataInputMode, ");
            sb.Append("ClientFormHeight = @ClientFormHeight, WebFormHeight = @WebFormHeight, IsInitReview = @IsInitReview, ");
            sb.Append("EnableManager = @EnableManager, IsFinalReview = @IsFinalReview, DataFilledProperty = @DataFilledProperty, ");
            sb.Append("EnableGuidance = @EnableGuidance, Guidance = @Guidance, ConditionEnabled = @ConditionEnabled, ");
            sb.Append("ConditionContent = @ConditionContent, ConditionType = @ConditionType, Notes = @Notes ");
            sb.Append("WHERE DataId = @DataId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customDataInfo.DataId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.TableId));
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ReportId));
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.RoleId));
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customDataInfo.GroupId);
                    db.AddInParameter(dbCommand, "ParentReportId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentReportId));
                    db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ParentRoleId));
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataInfo.ViewId));
                    db.AddInParameter(dbCommand, "DataName", DbType.String, customDataInfo.DataName);
                    db.AddInParameter(dbCommand, "DataCode", DbType.String, customDataInfo.DataCode);
                    db.AddInParameter(dbCommand, "DataFilledType", DbType.Byte, customDataInfo.DataFilledType);
                    db.AddInParameter(dbCommand, "DataShowMode", DbType.Byte, customDataInfo.DataShowMode);
                    db.AddInParameter(dbCommand, "DataInputMode", DbType.Byte, customDataInfo.DataInputMode);
                    db.AddInParameter(dbCommand, "ClientFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.ClientFormHeight));
                    db.AddInParameter(dbCommand, "WebFormHeight", DbType.Int32, DataConvertionHelper.SetInt(customDataInfo.WebFormHeight));
                    db.AddInParameter(dbCommand, "IsInitReview", DbType.Boolean, customDataInfo.IsInitReview);
                    db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, customDataInfo.EnableManager);
                    db.AddInParameter(dbCommand, "IsFinalReview", DbType.Boolean, customDataInfo.IsFinalReview);
                    db.AddInParameter(dbCommand, "DataFilledProperty", DbType.Byte, customDataInfo.DataFilledProperty);
                    db.AddInParameter(dbCommand, "EnableGuidance", DbType.Boolean, customDataInfo.EnableGuidance);
                    db.AddInParameter(dbCommand, "Guidance", DbType.String, customDataInfo.Guidance);
                    db.AddInParameter(dbCommand, "ConditionEnabled", DbType.Boolean, customDataInfo.ConditionEnabled);
                    db.AddInParameter(dbCommand, "ConditionContent", DbType.String, customDataInfo.ConditionContent);
                    db.AddInParameter(dbCommand, "ConditionType", DbType.Byte, customDataInfo.ConditionType);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDataInfo.Notes);
                    //执行更新操作
                    int count = 0;
                    if (transaction != null)
                    {
                        count = db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        count = db.ExecuteNonQuery(dbCommand);
                    }
                    if (count != 1)
                    {
                        throw new Exception("更新失败！");
                    }                    
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(customDataInfo.DataId, (byte)AttachmentCategory.DataFilled, upLoadFileInfos, db, transaction);
                    messageAttachment.Update(customDataInfo.DataId, (byte)AttachmentCategory.DataFilledCondition, conditionalUpLoadFileInfos, db, transaction);
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
