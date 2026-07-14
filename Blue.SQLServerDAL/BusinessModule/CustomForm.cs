//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomForm.cs
// 描述：CustomForm 数据层访问类
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomForm 表的数据层访问类
    /// </summary>
    public class CustomForm : CommonNodeDataAccess, ICustomForm
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomForm() : base("CustomForm", "FormId", "SectionId", "FormName", "FormCode", false, true, "FormType")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomForm 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomFormInfo customFormInfo)
        {
            //自动增加的关键字的值
            decimal customFormId = 0;

            try
            {
                customFormId = Insert(customFormInfo, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormId;
        }

        /// <summary>
		/// 获得 CustomFormInfo 对象
		/// </summary>
		///<param name="formId">业务编号</param>
		/// <returns> CustomFormInfo 对象</returns>
		public CustomFormInfo GetModelInfo(decimal formId)
        {
            CustomFormInfo customFormInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("FormId", "FormId", DbType.Decimal, formId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomFormInfo> customFormInfos = GetModelInfos(whereConditons, null, true);
            if (customFormInfos != null && customFormInfos.Count > 0)
            {
                customFormInfo = customFormInfos[0];
            }

            return customFormInfo;
        }

        /// <summary>
        /// 更新 CustomFormInfo 对象
        /// </summary>
        /// <param name="customFormInfo">CustomFormInfo 对象</param>
        public void Update(CustomFormInfo customFormInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                Update(customFormInfo, null, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomFormInfo 对象
        /// </summary>
        ///<param name="formId">业务编号</param>
        public void Delete(decimal formId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomForm ");
            sb.Append("WHERE FormId = @FormId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "FormId", DbType.Decimal, formId);
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
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<CustomFormInfo> GetModelInfos(decimal sectionId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();

            whereConditons.Add(new WhereConditon("SectionId", "SectionId", System.Data.DbType.Decimal,
                sectionId, DataFieldCondition.Equal));

            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomFormInfo 对象列表</returns>
        public IList<CustomFormInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomForm 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomFormInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomForm ", "FormId", false, whereConditons);
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
        /// 通过组合表编号查询数据填报的窗体数量
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
        /// 向 CustomForm 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
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
                    customDataId = Insert(customFormInfo, upLoadFileInfos, db, transaction);
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
        /// 更新数据表格和附件信息
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(customFormInfo, upLoadFileInfos, db, transaction);
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
        /// 删除 CustomFormInfo 对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal sectionId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomForm ");
            sb.Append("WHERE SectionId = @SectionId");
           
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, sectionId);
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
        /// 删除 CustomFormInfo 对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomForm ");
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomFormInfo 对象列表</returns>
        private IList<CustomFormInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomFormInfo> customFormInfos = new List<CustomFormInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomForm");
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
                            decimal formId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal sectionId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string formName = DataConvertionHelper.GetString(dataReader[4]);
                            string formCode = DataConvertionHelper.GetString(dataReader[5]);
                            byte formType = DataConvertionHelper.GetByte(dataReader[6]);
                            byte systemFormType = DataConvertionHelper.GetByte(dataReader[7]);
                            bool businessEnabled = DataConvertionHelper.GetBoolean(dataReader[8]);
                            long formProperty = DataConvertionHelper.GetLong(dataReader[9]);
                            byte showMode = DataConvertionHelper.GetByte(dataReader[10]);
                            long dataFieldSetting = DataConvertionHelper.GetLong(dataReader[11]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[12]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[13]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[14]);
                            string notes = DataConvertionHelper.GetString(dataReader[15]);
                            //将创建 CustomFormInfo 对象加入集合中
                            customFormInfos.Add(new CustomFormInfo(formId, sectionId, combinedTableId, tableId, formName,
                            formCode, formType, systemFormType, businessEnabled, formProperty,
                            showMode, dataFieldSetting, enableHelp, helpContent, sorting,
                            notes));
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

            return customFormInfos;
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomFormInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomForm");
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
        /// 获得表 CustomForm 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomForm ", "FormId", "*", false, false, startPosition,
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
        /// 获得以表 CustomForm 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomForm ", "FormId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomForm 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomForm ", "FormId", "*", false, false, startPosition,
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
        /// 获得以表 CustomForm 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomForm ", "FormId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomFormInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomForm");
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
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>自动增加的关键字的值</returns>
        private decimal Insert(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customFormId = 0;

            customFormInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomForm", "Sorting", "SectionId", customFormInfo.SectionId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomForm(SectionId, CombinedTableId, TableId, FormName, FormCode, ");
            sb.Append("FormType, SystemFormType, BusinessEnabled, FormProperty, ShowMode, DataFieldSetting, ");
            sb.Append("EnableHelp, HelpContent, Sorting, Notes)");
            sb.Append("VALUES (@SectionId, @CombinedTableId, @TableId, @FormName, @FormCode, ");
            sb.Append("@FormType, @SystemFormType, @BusinessEnabled, @FormProperty, @ShowMode, @DataFieldSetting, ");
            sb.Append("@EnableHelp, @HelpContent, @Sorting, @Notes);");
            sb.Append("SET @FormId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "FormId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, customFormInfo.SectionId);
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customFormInfo.CombinedTableId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customFormInfo.TableId));
                    db.AddInParameter(dbCommand, "FormName", DbType.String, customFormInfo.FormName);
                    db.AddInParameter(dbCommand, "FormCode", DbType.String, customFormInfo.FormCode);
                    db.AddInParameter(dbCommand, "FormType", DbType.Byte, customFormInfo.FormType);
                    db.AddInParameter(dbCommand, "SystemFormType", DbType.Byte, customFormInfo.SystemFormType);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, customFormInfo.BusinessEnabled);
                    db.AddInParameter(dbCommand, "FormProperty", DbType.Int64, customFormInfo.FormProperty);
                    db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customFormInfo.ShowMode);
                    db.AddInParameter(dbCommand, "DataFieldSetting", DbType.Int64, customFormInfo.DataFieldSetting);
                    db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customFormInfo.EnableHelp);
                    db.AddInParameter(dbCommand, "HelpContent", DbType.String, customFormInfo.HelpContent);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customFormInfo.Sorting);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customFormInfo.Notes);
                    /* 1. 执行插入记录操作 */
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
                    customFormId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@FormId"].Value, 0);
                }
                CustomSection customSection = new CustomSection();
                customSection.UpdateLeafOfParentNode(customFormInfo.SectionId, false, db, transaction);

                /* 2. 插入附件 */
                if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                {
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Insert(customFormId, (byte)AttachmentCategory.DataForm, upLoadFileInfos, db, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customFormId;
        }

        /// <summary>
        /// 更新 customFormInfo 对象
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomForm SET SectionId = @SectionId, CombinedTableId = @CombinedTableId, TableId = @TableId, FormName = @FormName, ");
            sb.Append("FormCode = @FormCode, FormType = @FormType, SystemFormType =  @SystemFormType, BusinessEnabled = @BusinessEnabled, FormProperty = @FormProperty, ");
            sb.Append("ShowMode = @ShowMode, DataFieldSetting = @DataFieldSetting, EnableHelp = @EnableHelp, HelpContent = @HelpContent, Notes = @Notes ");
            sb.Append("WHERE FormId = @FormId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "FormId", DbType.Decimal, customFormInfo.FormId);
                    db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, customFormInfo.SectionId);
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customFormInfo.CombinedTableId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customFormInfo.TableId));
                    db.AddInParameter(dbCommand, "FormName", DbType.String, customFormInfo.FormName);
                    db.AddInParameter(dbCommand, "FormCode", DbType.String, customFormInfo.FormCode);
                    db.AddInParameter(dbCommand, "FormType", DbType.Byte, customFormInfo.FormType);
                    db.AddInParameter(dbCommand, "SystemFormType", DbType.Byte, customFormInfo.SystemFormType);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, customFormInfo.BusinessEnabled);
                    db.AddInParameter(dbCommand, "FormProperty", DbType.Int64, customFormInfo.FormProperty);
                    db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customFormInfo.ShowMode);
                    db.AddInParameter(dbCommand, "DataFieldSetting", DbType.Int64, customFormInfo.DataFieldSetting);
                    db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customFormInfo.EnableHelp);
                    db.AddInParameter(dbCommand, "HelpContent", DbType.String, customFormInfo.HelpContent);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customFormInfo.Notes);
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
                    messageAttachment.Update(customFormInfo.FormId, (byte)AttachmentCategory.DataForm, upLoadFileInfos, db, transaction);
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
