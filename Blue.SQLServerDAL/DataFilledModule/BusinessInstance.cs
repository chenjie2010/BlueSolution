//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessInstance.cs
// 描述：BusinessInstance 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/2/17
// Copyright 2018
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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.DataFilledModule;
using Blue.Model.DataFilledModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessDesignerModule;
using Blue.SQLServerDAL.BusinessModule;
using Blue.SQLServerDAL.UserModule;

namespace Blue.SQLServerDAL.DataFilledModule
{
    /// <summary>
    /// BusinessInstance 表的数据层访问类
    /// </summary>
    public class BusinessInstance : IBusinessInstance
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BusinessInstance()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 BusinessInstance 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceInfo">businessInstanceInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(BusinessInstanceInfo businessInstanceInfo)
        {
            //自动增加的关键字的值
            decimal businessInstanceId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                businessInstanceId = Insert(businessInstanceInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceId;
        }

        /// <summary>
		/// 获得 BusinessInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">实例编号</param>
		/// <returns> BusinessInstanceInfo 对象</returns>
		public BusinessInstanceInfo GetModelInfo(decimal instanceId)
        {
            BusinessInstanceInfo businessInstanceInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("InstanceId", "InstanceId", DbType.Decimal, instanceId, DataFieldCondition.Equal));

            //创建集合对象
            IList<BusinessInstanceInfo> businessInstanceInfos = GetModeInfos(whereConditons, null, true);
            if (businessInstanceInfos != null && businessInstanceInfos.Count > 0)
            {
                businessInstanceInfo = businessInstanceInfos[0];
            }

            return businessInstanceInfo;
        }

        /// <summary>
        /// 更新 BusinessInstanceInfo 对象
        /// </summary>
        /// <param name="businessInstanceInfo">BusinessInstanceInfo 对象</param>
        public void Update(BusinessInstanceInfo businessInstanceInfo)
        {

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                Update(businessInstanceInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 BusinessInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        public void Delete(decimal instanceId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM BusinessInstance ");
            sb.Append("WHERE InstanceId = @InstanceId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    BusinessInstanceStep businessInstanceStep = new BusinessInstanceStep();
                    businessInstanceStep.Delete(instanceId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
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
        /// 获得 BusinessInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 对象列表</returns>
        public IList<BusinessInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 BusinessInstance 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "BusinessInstance", "InstanceId", false, whereConditons);
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
        /// 终止业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool AbortBussinessInstance(decimal userId, decimal instanceId)
        {
            bool success = true;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            BusinessInstanceInfo businessInstanceInfo = GetModelInfo(instanceId);
            InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
            InstanceState newInstanceState = InstanceState.None;
            switch (instanceState)
            {
                case InstanceState.InitReview:
                    newInstanceState = InstanceState.InitReviewAborted;
                    break;

                case InstanceState.FinalReview:
                    newInstanceState = InstanceState.FinalReviewAborted;
                    break;

                default:
                    success = false;
                    break;
            }
            if (success && (userId == businessInstanceInfo.ReviewerId))
            {
                int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "BusinessInstanceStep", "Sorting", "InstanceId", instanceId, 0);
                BusinessInstanceStepInfo businessInstanceStepInfo = new BusinessInstanceStepInfo()
                {
                    InstanceId = instanceId,
                    UserId = userId,
                    ReviewedAction = (byte)ReviewedAction.Abort,
                    ActionVisible = true,
                    Sorting = sorting + 1
                };

                BusinessInstanceStep businessInstanceStep = new BusinessInstanceStep();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {

                        Update(instanceId, newInstanceState, userId, db, transaction);
                        businessInstanceStep.Insert(businessInstanceStepInfo, db, transaction);
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
            return success;
        }

        /// <summary>
        /// 撤回提交的业务实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool WithDrawBussinessInstance(decimal userId, decimal instanceId)
        {
            bool success = false;

            BusinessInstanceStep businessInstanceStep = new BusinessInstanceStep();
            decimal lastestReviewerId = businessInstanceStep.GetLastestReviewerId(instanceId);
            if (userId == lastestReviewerId)
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                BusinessInstanceInfo instanceInfo = GetModelInfo(instanceId);
                InstanceState currentInstanceState = (InstanceState)instanceInfo.InstanceState;
                InstanceState previousInstanceState = InstanceState.None;
                CustomData customData = new CustomData();
                switch (currentInstanceState)
                {
                    case InstanceState.InitReview:
                        previousInstanceState = InstanceState.None;
                        lastestReviewerId = instanceInfo.UserId;
                        break;

                    case InstanceState.FinalReview:                        
                        bool initReview = customData.GetInitReview(instanceId);
                        if (initReview)
                        {
                            previousInstanceState = InstanceState.InitReview;
                        }
                        else
                        {
                            previousInstanceState = InstanceState.None;
                            lastestReviewerId = instanceInfo.UserId;
                        }
                        break;

                    case InstanceState.InitReviewAborted:
                        previousInstanceState = InstanceState.InitReview;
                        break;

                    case InstanceState.FinalReviewAborted:
                        previousInstanceState = InstanceState.FinalReview;
                        break;

                    case InstanceState.Completed:
                        CustomData data = new CustomData();
                        bool isFinalReview = data.GetFinalReview(instanceId);
                        if (isFinalReview)
                        {
                            previousInstanceState = InstanceState.FinalReview;
                        }
                        else
                        {
                            bool isInitReview = data.GetInitReview(instanceId);
                            if (isInitReview)
                            {
                                previousInstanceState = InstanceState.InitReview;
                            }
                            else
                            {
                                previousInstanceState = InstanceState.None;
                                lastestReviewerId = instanceInfo.UserId;
                            }
                        }
                        break;

                    case InstanceState.None:
                        bool initReviewed = customData.GetInitReview(instanceId);
                        if (initReviewed)
                        {
                            previousInstanceState = InstanceState.InitReview;
                        }
                        else
                        {
                            previousInstanceState = InstanceState.FinalReview;
                            lastestReviewerId = instanceInfo.UserId;
                        }
                        break;
                        
                }
                int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "BusinessInstanceStep", "Sorting", "InstanceId", instanceId, 0);
                BusinessInstanceStepInfo businessInstanceStepInfo = new BusinessInstanceStepInfo()
                {
                    InstanceId = instanceId,
                    UserId = userId,
                    ReviewedAction = (byte)ReviewedAction.WithDraw,
                    ActionVisible = false,
                    Sorting = sorting + 1
                };

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        Update(instanceId, previousInstanceState, lastestReviewerId, db, transaction);
                        businessInstanceStep.UpdateActionVisible(instanceId, sorting, false, db, transaction);
                        businessInstanceStep.Insert(businessInstanceStepInfo, db, transaction);
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        //记录日志, 抛出异常, 不包装异常 
                        ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                    }
                }
                success = true;
            }

            return success;
        }

        /// <summary>
        /// 根据当前实例编号，获得当前实例状态
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 当前实例状态</returns>
        public InstanceState GetInstanceState(decimal instanceId)
        {
            InstanceState instanceState = 0;

            try
            {
                string sqlSelect = "SELECT InstanceState FROM BusinessInstance WHERE InstanceId = @InstanceId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    instanceState = (InstanceState)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceState;
        }

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetBusinessInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAccount", "UserId", TableJoin.InnerJoin));

                ds = DataAccessHandler.GetPageRecord(db, "BusinessInstance", "InstanceId", "InstanceId, BusinessInstance.UserId, BusinessInstance.ParentUserId, BusinessInstance.DataId, InstanceName, InstanceState, UserName, UserActualName, TimeSumbitted", false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetBusinessInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("BusinessInstanceStep", "InstanceId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("BusinessInstance", "ParentUserId", TableJoin.InnerJoin, "UserAccount", "UserId"));
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("BusinessInstanceStep", "TimeReviewed", CustomSorting.Descending));
                ds = DataAccessHandler.GetPageRecord(db, "BusinessInstance", "InstanceId", "BusinessInstance.InstanceId, InstanceName, CommentReviewed, ReviewedAction, TimeReviewed", false, false, tableLinks, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得填报实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<BusinessInstanceInfo> GetModelInfos(decimal userId, decimal dataId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("IsArchived", "IsArchived", DbType.Boolean, false, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataId", "DataId", DbType.Decimal, dataId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("TimeModified", CustomSorting.Descending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得表 BusinessInstance 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string dataFiledNames = "InstanceId, DataId, UserId, ParentUserId, InstanceName, InstanceState, CAST(AudittedStatus AS int) AS AudittedStatus, TimeSumbitted, LastTimeHandled";
                ds = DataAccessHandler.GetPageRecord(db, "BusinessInstance", "InstanceId", dataFiledNames, false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        public int GetBusinessInstanceCount(decimal userId, DataSumbittedState dataSumbittedState)
        {
            int count = 0;


            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            switch (dataSumbittedState)
            {
                case DataSumbittedState.Drfat:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState", DbType.Byte, InstanceState.None, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;

                case DataSumbittedState.Review:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_0", DbType.Byte, InstanceState.InitReview, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_1", DbType.Byte, InstanceState.FinalReview, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataSumbittedState.Completed:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_0", DbType.Byte, InstanceState.FinalReviewAborted, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_1", DbType.Byte, InstanceState.InitReviewAborted, DataFieldCondition.Equal, DataFieldInnerRealtion.Or));
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_2", DbType.Byte, InstanceState.Completed, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;
            }

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(InstanceId) FROM BusinessInstance WHERE ");
            sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                if ((whereConditons != null) && (whereConditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
            }

            return count;
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <param name="dataSumbittedState"></param>
        /// <returns></returns>
        public int GetBusinessInstanceCount(decimal userId, decimal dataId, DataSumbittedState dataSumbittedState)
        {
            int count = 0;


            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("DataId", "DataId", DbType.Decimal, dataId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("ParentUserId", "ParentUserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            switch (dataSumbittedState)
            {
                case DataSumbittedState.Drfat:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState", DbType.Byte, InstanceState.None, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;

                case DataSumbittedState.Review:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_0", DbType.Byte, InstanceState.InitReview, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_1", DbType.Byte, InstanceState.FinalReview, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataSumbittedState.Completed:
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState", DbType.Byte, InstanceState.Completed, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;
            }

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(InstanceId) FROM BusinessInstance WHERE ");
            sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                if ((whereConditons != null) && (whereConditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
            }

            return count;
        }

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceItem Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, IList<RecordEntity> recordEntities)
        {
            InstanceItem instanceItem = new InstanceItem(); ;
            
            try
            {
                UserAccount userAccount = new UserAccount();
                CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(businessInstanceInfo.ParentUserId);
                if (commonUserInfo == null)
                {
                    throw new ArgumentException("用户不存在。");
                }
                bool instanceIdUpdated = businessInstanceInfo.InstanceId <= 0;
                instanceItem.InstanceId = Process(businessInstanceInfo, businessInstanceStepInfo);
                DataAuditing dataAuditing = new DataAuditing();
                if (instanceIdUpdated)
                {
                    foreach (var recordEntity in recordEntities)
                    {
                        recordEntity.InstanceId = instanceItem.InstanceId;
                    }
                }                
                instanceItem.RecordItems = dataAuditing.Process(commonUserInfo, recordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceItem;
        }

        /// <summary>
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceSet Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            InstanceSet instanceSet = new InstanceSet();

            try
            {
                UserAccount userAccount = new UserAccount();
                CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(businessInstanceInfo.ParentUserId);
                if (commonUserInfo == null)
                {
                    throw new ArgumentException("用户不存在。");
                }
                instanceSet.InstanceId = Process(businessInstanceInfo, businessInstanceStepInfo);
                if (businessInstanceInfo.InstanceId <= 0)
                {
                    foreach (var keyValue in dicRecordEntities)
                    {
                        foreach (var recordEntity in keyValue.Value)
                        {
                            recordEntity.InstanceId = instanceSet.InstanceId;
                        }
                    }
                }
                DataAuditing dataAuditing = new DataAuditing();
                instanceSet.RecordItems = dataAuditing.Process(commonUserInfo, dicRecordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }           

            return instanceSet;
        }        

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            byte[] data = null;

            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
            if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
            {
                sbPath.Append(@"\");
            }
            sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, dataFieldName);
            if (!Directory.Exists(sbPath.ToString()))
            {
                Directory.CreateDirectory(sbPath.ToString());
            }
            sbPath.Append(fileName);
            if (System.IO.File.Exists(sbPath.ToString()))
            {
                using (FileStream fs = new FileStream(sbPath.ToString(), FileMode.Open, FileAccess.Read))
                {
                    BinaryReader r = new BinaryReader(fs);
                    data = r.ReadBytes((int)fs.Length);
                }
            }

            return data;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 BusinessInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>BusinessInstanceInfo 对象列表</returns>
        private IList<BusinessInstanceInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<BusinessInstanceInfo> businessInstanceInfos = new List<BusinessInstanceInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM BusinessInstance");
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
                            decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal parentUserId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string instanceName = DataConvertionHelper.GetString(dataReader[4]);
                            byte instanceState = DataConvertionHelper.GetByte(dataReader[5]);
                            DateTime timeCreated = DataConvertionHelper.GetDateTime(dataReader[6]);
                            DateTime timeModified = DataConvertionHelper.GetDateTime(dataReader[7]);
                            DateTime timeSumbitted = DataConvertionHelper.GetDateTime(dataReader[8]);
                            decimal reviewerId = DataConvertionHelper.GetDecimal(dataReader[9]);
                            bool isDraft = DataConvertionHelper.GetBoolean(dataReader[10]);
                            string tmpComments = DataConvertionHelper.GetString(dataReader[11]);
                            DateTime lastTimeHandled = DataConvertionHelper.GetDateTime(dataReader[12]);
                            bool audittedStatus = DataConvertionHelper.GetBoolean(dataReader[13]);
                            bool isArchived = DataConvertionHelper.GetBoolean(dataReader[14]);
                            string archivedUserName = DataConvertionHelper.GetString(dataReader[15]);
                            string archivedName = DataConvertionHelper.GetString(dataReader[16]);
                            DateTime timeArchived = DataConvertionHelper.GetDateTime(dataReader[17]);
                            long pageSign = DataConvertionHelper.GetLong(dataReader[16]);
                            //将创建 BusinessInstanceInfo 对象加入集合中
                            businessInstanceInfos.Add(new BusinessInstanceInfo(instanceId, userId, parentUserId, dataId, instanceName,
                            instanceState, timeCreated, timeModified, timeSumbitted, reviewerId, isDraft, tmpComments,
                            lastTimeHandled, audittedStatus, isArchived, archivedUserName, archivedName,
                            timeArchived, pageSign));
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

            return businessInstanceInfos;
        }

        /// <summary>
        /// 获得 BusinessInstanceInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BusinessInstance");
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
        /// 获得以表 BusinessInstance 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "BusinessInstance ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得以表 BusinessInstance 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "BusinessInstance ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  BusinessInstanceInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM BusinessInstance");
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
        /// 处理提交的数据
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        /// <param name="businessInstanceStepInfo"></param>
        /// <returns></returns>
        private decimal Process(BusinessInstanceInfo businessInstanceInfo, BusinessInstanceStepInfo businessInstanceStepInfo)
        {
            UserAccount userAccount = new UserAccount();
            CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(businessInstanceInfo.ParentUserId);
            if (commonUserInfo == null)
            {
                throw new ArgumentException("用户不存在。");
            }
            CustomTable customTable = new CustomTable();
            DataFieldRelationship dataFieldRelationship = new DataFieldRelationship();
            BusinessInstanceStep businessInstanceStep = new BusinessInstanceStep();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            if (businessInstanceStepInfo != null)
            {
                businessInstanceStepInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "BusinessInstanceStep", "Sorting", "InstanceId", businessInstanceStepInfo.InstanceId, 0) + 1;
            }

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (businessInstanceInfo.InstanceId > 0)
                    {
                        Update(businessInstanceInfo, db, transaction);
                    }
                    else
                    {
                        businessInstanceInfo.InstanceId = Insert(businessInstanceInfo, db, transaction);
                    }
                    if (businessInstanceStepInfo != null)
                    {
                        businessInstanceStepInfo.InstanceId = businessInstanceInfo.InstanceId;
                        businessInstanceStep.Insert(businessInstanceStepInfo, db, transaction);
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

            return businessInstanceInfo.InstanceId;
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
        /// 更新当前状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentState"></param>
        /// <param name="physicalName"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateCurrentStateByUserId(decimal userId, byte currentState, string physicalName, SqlDatabase db, DbTransaction transaction)
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

        /// <summary>
        /// 更新当前状态
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

        /// <summary>
        /// 更新实例状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="instanceState"></param>
        /// <param name="reviewerId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(decimal instanceId, InstanceState instanceState, decimal reviewerId, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE BusinessInstance SET InstanceState = @InstanceState, ReviewerId = @ReviewerId, LastTimeHandled = @LastTimeHandled ");
            sb.Append("WHERE InstanceId = @InstanceId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "InstanceState", DbType.Byte, instanceState);
                    db.AddInParameter(dbCommand, "ReviewerId", DbType.Decimal, reviewerId);
                    db.AddInParameter(dbCommand, "LastTimeHandled", DbType.DateTime, DateTime.Now);

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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得该用户在记录中最大的排序值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int GetRecordSorting(SqlDatabase db, string tablePhysicalName, decimal userId)
        {
            int recordSorting = 0;

            string sqlSelect = string.Format("SELECT MAX(RecordSorting) FROM {0} WHERE UserId = @UserId", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                recordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0) + 1;
            }

            return recordSorting;
        }

        /// <summary>
        /// 向 BusinessInstance 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceInfo">businessInstanceInfo 对象</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        private decimal Insert(BusinessInstanceInfo businessInstanceInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal businessInstanceId = 0;

            InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO BusinessInstance(UserId, ParentUserId, DataId, InstanceName, InstanceState, ");
            sb.Append("TimeCreated, TimeModified, TimeSumbitted, ReviewerId, IsDraft, TmpComments, LastTimeHandled, ");
            sb.Append("AudittedStatus, IsArchived, PageSign)");
            sb.Append("VALUES (@UserId, @ParentUserId, @DataId, @InstanceName, @InstanceState, ");
            sb.Append("@TimeCreated, @TimeModified, @TimeSumbitted, @ReviewerId, @IsDraft, @TmpComments, @LastTimeHandled, ");
            sb.Append("@AudittedStatus, @IsArchived, @PageSign);");
            sb.Append("SET @InstanceId = SCOPE_IDENTITY()");


            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "InstanceId", DbType.Decimal, 10);                    
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, businessInstanceInfo.UserId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, businessInstanceInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, businessInstanceInfo.DataId);
                    db.AddInParameter(dbCommand, "InstanceName", DbType.String, businessInstanceInfo.InstanceName);
                    db.AddInParameter(dbCommand, "InstanceState", DbType.Byte, businessInstanceInfo.InstanceState);
                    db.AddInParameter(dbCommand, "TimeCreated", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "TimeSumbitted", DbType.DateTime, DataConvertionHelper.SetDateTime(businessInstanceInfo.TimeSumbitted));
                    db.AddInParameter(dbCommand, "ReviewerId", DbType.Decimal, DataConvertionHelper.SetDecimal(businessInstanceInfo.ReviewerId));
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, businessInstanceInfo.IsDraft);
                    db.AddInParameter(dbCommand, "TmpComments", DbType.String, businessInstanceInfo.TmpComments);
                    db.AddInParameter(dbCommand, "LastTimeHandled", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "AudittedStatus", DbType.Boolean, businessInstanceInfo.AudittedStatus);
                    db.AddInParameter(dbCommand, "IsArchived", DbType.Boolean, false);
                    db.AddInParameter(dbCommand, "PageSign", DbType.Int64, businessInstanceInfo.PageSign);

                    //执行插入操作
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
                        throw new Exception("插入失败！");
                    }
                    businessInstanceId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@InstanceId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return businessInstanceId;
        }

        /// <summary>
		/// 更新 BusinessInstanceInfo 对象
		/// </summary>
		/// <param name="businessInstanceInfo">BusinessInstanceInfo 对象</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
		public void Update(BusinessInstanceInfo businessInstanceInfo, SqlDatabase db, DbTransaction transaction)
        {
            InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE BusinessInstance SET DataId = @DataId, InstanceState = @InstanceState, ");
            sb.Append("TimeModified = @TimeModified, TimeSumbitted = @TimeSumbitted, ReviewerId = @ReviewerId, IsDraft = @IsDraft, ");
            sb.Append("TmpComments = @TmpComments, LastTimeHandled = @LastTimeHandled, AudittedStatus =@AudittedStatus ");
            if (businessInstanceInfo.PageSign > 0)
            {
                sb.Append(", PageSign = PageSign | @PageSign ");
            }
            sb.Append("WHERE InstanceId = @InstanceId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, businessInstanceInfo.InstanceId);
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, businessInstanceInfo.DataId);
                    db.AddInParameter(dbCommand, "InstanceState", DbType.Byte, businessInstanceInfo.InstanceState);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "TimeSumbitted", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "ReviewerId", DbType.Decimal, DataConvertionHelper.SetDecimal(businessInstanceInfo.ReviewerId));
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, businessInstanceInfo.IsDraft);
                    db.AddInParameter(dbCommand, "TmpComments", DbType.String, businessInstanceInfo.TmpComments);
                    db.AddInParameter(dbCommand, "LastTimeHandled", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "AudittedStatus", DbType.Boolean, businessInstanceInfo.AudittedStatus);
                    if (businessInstanceInfo.PageSign > 0)
                    {
                        db.AddInParameter(dbCommand, "PageSign", DbType.Int64, businessInstanceInfo.PageSign);
                    }
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        private void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir)
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
                if (System.IO.File.Exists(sbPath.ToString()))
                {
                    System.IO.File.Delete(sbPath.ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="recordSorting"></param>
        /// <param name="srouceFileName"></param>
        /// <returns></returns>
        private string GenerateFileName(string userName, string dataFieldName, int recordSorting, string srouceFileName)
        {
            return string.Format("{0}_{1}_{2}{3}", userName, dataFieldName, recordSorting, Path.GetExtension(srouceFileName));
        }


        #endregion

        #endregion
    }
}
