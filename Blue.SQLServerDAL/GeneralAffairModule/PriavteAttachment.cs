//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PriavteAttachment.cs
// 描述：PriavteAttachment 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/9/12
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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.SQLServerDAL.GeneralAffairModule
{
    /// <summary>
    /// PriavteAttachment 表的数据层访问类
    /// </summary>
    public class PriavteAttachment : IPriavteAttachment
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public PriavteAttachment()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 PriavteAttachment 表中插入一条新记录
		/// </summary>
		/// <param name="priavteAttachmentInfo">priavteAttachmentInfo 对象</param>
		public void Insert(PriavteAttachmentInfo priavteAttachmentInfo)
		{			
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                Insert(priavteAttachmentInfo, db, null);
            }
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }            
		}

        /// <summary>
        /// 获得 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
		public PriavteAttachmentInfo GetModelInfo(decimal attachmentId, byte attachmentCategory, int sorting)
		{			
			PriavteAttachmentInfo priavteAttachmentInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("AttachmentId", "AttachmentId", System.Data.DbType.Decimal, attachmentId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("AttachmentCategory", "AttachmentCategory", System.Data.DbType.Byte, attachmentCategory, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("Sorting", "Sorting", System.Data.DbType.Int32, sorting, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<PriavteAttachmentInfo> priavteAttachmentInfos = GetModelInfos(whereConditons, null, true);
            if (priavteAttachmentInfos != null && priavteAttachmentInfos.Count > 0)
            {
                priavteAttachmentInfo = priavteAttachmentInfos[0];
            }          

            return priavteAttachmentInfo;
		}

        /// <summary>
        /// 删除 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>        
        public void Delete(decimal attachmentId, byte attachmentCategory, int sorting)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                Delete(attachmentId, attachmentCategory, sorting, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 PriavteAttachmentInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PriavteAttachmentInfo 对象列表</returns>
        public IList<PriavteAttachmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModelInfos(whereConditons, sortingCondtions, false);
		}

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 获得邮件的附件列表
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <returns></returns>
        public IList<PriavteAttachmentInfo> GetModelInfos(decimal attachmentId, byte attachmentCategory)
        {
            IList<PriavteAttachmentInfo> priavteAttachmentInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("AttachmentId", "AttachmentId", System.Data.DbType.Decimal, attachmentId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("AttachmentCategory", "AttachmentCategory", System.Data.DbType.Byte, attachmentCategory,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>(1);
            sortingCondtions.Add(new SortingCondtion("AttachmentType", CustomSorting.Ascending));
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            priavteAttachmentInfos = GetModelInfos(whereConditons, sortingCondtions);

            return priavteAttachmentInfos;
        }

        /// <summary>
        /// 获得附件的路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public string GetAttachmentPath(decimal attachmentId, byte attachmentCategory, int sorting)
        {
            StringBuilder sb = new StringBuilder();
            PriavteAttachmentInfo priavteAttachmentInfo = GetModelInfo(attachmentId, attachmentCategory, sorting);
            if (priavteAttachmentInfo != null)
            {
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedMessageFiles();
                sb.Append(relativeSubDirOfSavedMessageFiles);
                sb.Append(priavteAttachmentInfo.AttachmentPath);
                sb.Append(priavteAttachmentInfo.AttachmentName);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获得附件的数据
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public byte[] GetAttachmentData(decimal attachmentId, byte attachmentCategory, int sorting)
        {
            byte[] data = null;
            PriavteAttachmentInfo priavteAttachmentInfo = GetModelInfo(attachmentId, attachmentCategory, sorting);
            if (priavteAttachmentInfo != null)
            {
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedMessageFiles();
                StringBuilder sb = new StringBuilder();
                sb.Append(relativeSubDirOfSavedMessageFiles);
                sb.Append(priavteAttachmentInfo.AttachmentPath);
                sb.Append(priavteAttachmentInfo.AttachmentName);
                //存储附件是否存在             
                if (File.Exists(sb.ToString()))
                {
                    using (FileStream fs = new FileStream(sb.ToString(), FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader r = new BinaryReader(fs);
                        data = r.ReadBytes((int)fs.Length);
                    }
                }
            }

            return data;
        }
        
        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 向 PriavteAttachment 表中插入一条新记录
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(decimal attachmentId, byte attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedMessageFiles();
            string path = string.Format(@"{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
            StringBuilder sb = new StringBuilder();
            sb.Append(relativeSubDirOfSavedMessageFiles);
            sb.Append(path);
            if (!Directory.Exists(sb.ToString()))
            {
                Directory.CreateDirectory(sb.ToString());
            }
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "PriavteAttachment", "Sorting", "AttachmentId", attachmentId, "AttachmentCategory", attachmentCategory, 0) + 1;
            if(upLoadFileInfos != null)
            {
                foreach (ExtendedUpLoadFileInfo upLoadFileInfo in upLoadFileInfos)
                {
                    string attachmentName = string.Format("{0}_{1}{2}", attachmentId, sorting, Path.GetExtension(upLoadFileInfo.UpLoadSourceFileName));
                    string newFilePath = string.Format("{0}{1}", sb.ToString(), attachmentName);
                    if (upLoadFileInfo.UpLoadFileData != null && upLoadFileInfo.UpLoadFileData.Length > 0)
                    {
                        PriavteAttachmentInfo priavteAttachmentInfo = new PriavteAttachmentInfo(attachmentId, attachmentName, attachmentCategory, upLoadFileInfo.UpLoadSourceFileName, path,
                            (byte)upLoadFileInfo.AttachmentType, upLoadFileInfo.UpLoadFileData.Length, sorting);
                        Insert(priavteAttachmentInfo, db, transaction);
                        using (FileStream fileStream = new FileStream(newFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            fileStream.Write(upLoadFileInfo.UpLoadFileData, 0, upLoadFileInfo.UpLoadFileData.Length);
                            fileStream.Close();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(upLoadFileInfo.FilePath))
                        {
                            upLoadFileInfo.FilePath = string.Format("{0}{1}", relativeSubDirOfSavedMessageFiles, upLoadFileInfo.FilePath);
                            try
                            {
                                File.Copy(upLoadFileInfo.FilePath, newFilePath);
                            }
                            catch { }
                            FileInfo fileInfo = new FileInfo(upLoadFileInfo.FilePath);
                            PriavteAttachmentInfo priavteAttachmentInfo = new PriavteAttachmentInfo(attachmentId, attachmentName, attachmentCategory, upLoadFileInfo.UpLoadSourceFileName, path,
                            (byte)upLoadFileInfo.AttachmentType, Convert.ToInt32(fileInfo.Length), sorting);
                            Insert(priavteAttachmentInfo, db, transaction);
                        }
                    }
                    sorting++;
                }                
            }
        }

        /// <summary>
        /// 删除 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal attachmentId, byte attachmentCategory, int sorting, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PriavteAttachment ");
            sb.Append("WHERE AttachmentId = @AttachmentId AND AttachmentCategory = @AttachmentCategory AND Sorting =  @Sorting");
          
            try
            {
                PriavteAttachmentInfo priavteAttachmentInfo = GetModelInfo(attachmentId, attachmentCategory, sorting);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AttachmentId", DbType.Decimal, attachmentId);
                    db.AddInParameter(dbCommand, "AttachmentCategory", DbType.Byte, attachmentCategory);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                    //执行删除操作
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
                        throw new Exception("删除失败！");
                    }
                    string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedMessageFiles();
                    string fullPath = Path.Combine(relativeSubDirOfSavedMessageFiles, priavteAttachmentInfo.AttachmentPath, priavteAttachmentInfo.AttachmentName);                  
                    try
                    {
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }
                    catch { }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByMailId(decimal attachmentId, byte attachmentCategory, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PriavteAttachment ");
            sb.Append("WHERE AttachmentId = @AttachmentId AND AttachmentCategory = @AttachmentCategory");
           
            try
            {
                IList<PriavteAttachmentInfo> priavteAttachmentInfos = GetModelInfos(attachmentId, attachmentCategory);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AttachmentId", DbType.Decimal, attachmentId);
                    db.AddInParameter(dbCommand, "AttachmentCategory", DbType.Byte, attachmentCategory);
                    //执行删除操作
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
                    }
                }
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedMessageFiles();
                StringBuilder sbFilePath = new StringBuilder();
                foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                {
                    if (sbFilePath.Length > 0)
                    {
                        sbFilePath.Clear();
                    }
                    sbFilePath.Append(relativeSubDirOfSavedMessageFiles);
                    sbFilePath.Append(priavteAttachmentInfo.AttachmentPath);
                    sbFilePath.Append(priavteAttachmentInfo.AttachmentName);
                    try
                    {
                        if (File.Exists(sbFilePath.ToString()))
                        {
                            File.Delete(sbFilePath.ToString());
                        }
                    }
                    catch { }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除某一业务下所有的附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal attachmentId, byte attachmentCategory)
        {
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                DeleteByMailId(attachmentId, attachmentCategory, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(decimal attachmentId, byte attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            /* 附件列表为空表示不需要更新相关附件 */
            if (upLoadFileInfos == null)
            {
                return;
            }
            IList <PriavteAttachmentInfo> priavteAttachmentInfos = GetModelInfos(attachmentId, attachmentCategory);
            if (upLoadFileInfos.Count == 0)
            {
                DeleteByMailId(attachmentId, attachmentCategory, db, transaction);
            }
            else
            {
                IList<ExtendedUpLoadFileInfo> newUpLoadFileInfos = new List<ExtendedUpLoadFileInfo>();
                foreach (ExtendedUpLoadFileInfo upLoadFileInfo in upLoadFileInfos)
                {
                    /* 附件内容为空表示不需要更新该附件 */
                    if (string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName) && upLoadFileInfo.UpLoadFileData != null)
                    {
                        newUpLoadFileInfos.Add(upLoadFileInfo);
                    }
                }
                if (newUpLoadFileInfos.Count > 0)
                {
                    Insert(attachmentId, attachmentCategory, newUpLoadFileInfos, db, transaction);
                }
                foreach (PriavteAttachmentInfo priavteAttachmentInfo in priavteAttachmentInfos)
                {
                    bool delete = true;
                    foreach (ExtendedUpLoadFileInfo upLoadFileInfo in upLoadFileInfos)
                    {
                        if (!string.IsNullOrWhiteSpace(upLoadFileInfo.UpLoadFileName) && priavteAttachmentInfo.AttachmentName.Equals(upLoadFileInfo.UpLoadFileName))
                        {
                            delete = false;
                            break;
                        }
                    }
                    if (delete)
                    {
                        Delete(priavteAttachmentInfo.AttachmentId, (byte)attachmentCategory, priavteAttachmentInfo.Sorting, db, transaction);
                    }
                }
            }
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 PriavteAttachmentInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>PriavteAttachmentInfo 对象列表</returns>
        private IList<PriavteAttachmentInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<PriavteAttachmentInfo>  priavteAttachmentInfos = new List<PriavteAttachmentInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM PriavteAttachment");
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
                            decimal attachmentId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string attachmentName = DataConvertionHelper.GetString(dataReader[1]);
                            byte attachmentCategory = DataConvertionHelper.GetByte(dataReader[2]);
                            string attachmentSourceName = DataConvertionHelper.GetString(dataReader[3]);
                            string attachmentPath = DataConvertionHelper.GetString(dataReader[4]);
                            byte attachmentType = DataConvertionHelper.GetByte(dataReader[5]);
                            int attachmenSize = DataConvertionHelper.GetInt(dataReader[6]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[7]);
                            //将创建 PriavteAttachmentInfo 对象加入集合中
                            priavteAttachmentInfos.Add(new PriavteAttachmentInfo(attachmentId, attachmentName, attachmentCategory, attachmentSourceName, attachmentPath,
                            attachmentType, attachmenSize, sorting));
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
            
			return priavteAttachmentInfos;
		} 
        
        /// <summary>
		/// 获得 PriavteAttachmentInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>PriavteAttachmentInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM PriavteAttachment");
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
        /// 获得表 PriavteAttachment 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PriavteAttachment ", "AttachmentId", "*", false, false, startPosition, 
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
        /// 获得以表 PriavteAttachment 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PriavteAttachment ", "AttachmentId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 PriavteAttachment 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PriavteAttachment ", "AttachmentId", "*", false, false, startPosition, 
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
        /// 获得以表 PriavteAttachment 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PriavteAttachment ", "AttachmentId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  PriavteAttachmentInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PriavteAttachment");
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
        /// 插入一个附件
        /// </summary>
        /// <param name="priavteAttachmentInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Insert(PriavteAttachmentInfo priavteAttachmentInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO PriavteAttachment(AttachmentId, AttachmentName, AttachmentCategory, AttachmentSourceName, AttachmentPath, AttachmentType, AttachmenSize, ");
            sb.Append("Sorting)");
            sb.Append("VALUES (@AttachmentId, @AttachmentName, @AttachmentCategory, @AttachmentSourceName, @AttachmentPath, @AttachmentType, @AttachmenSize, ");
            sb.Append("@Sorting);");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AttachmentId", DbType.Decimal, priavteAttachmentInfo.AttachmentId);
                    db.AddInParameter(dbCommand, "AttachmentName", DbType.String, priavteAttachmentInfo.AttachmentName);
                    db.AddInParameter(dbCommand, "AttachmentCategory", DbType.String, priavteAttachmentInfo.AttachmentCategory);
                    db.AddInParameter(dbCommand, "AttachmentSourceName", DbType.String, priavteAttachmentInfo.AttachmentSourceName);
                    db.AddInParameter(dbCommand, "AttachmentPath", DbType.String, priavteAttachmentInfo.AttachmentPath);
                    db.AddInParameter(dbCommand, "AttachmentType", DbType.Byte, priavteAttachmentInfo.AttachmentType);
                    db.AddInParameter(dbCommand, "AttachmenSize", DbType.Int32, priavteAttachmentInfo.AttachmenSize);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, priavteAttachmentInfo.Sorting);
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }            
        }

        /// <summary>
        /// 获得保存消息附件的固定部分的相对路径
        /// </summary>
        /// <returns></returns>
        private string GetRelativeSubDirOfSavedMessageFiles()
        {
            StringBuilder sb = new StringBuilder();

            string rootDirectory = AppSettingHelper.DefaultRootDirOfSavedFiles;
            sb.Append(rootDirectory);
            if (!rootDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.AppendFormat(@"{0}\", AppSettingHelper.DefaultSubDirOfSavedMessageFiles);

            return sb.ToString();

        }

        #endregion

        #endregion
    }
}
