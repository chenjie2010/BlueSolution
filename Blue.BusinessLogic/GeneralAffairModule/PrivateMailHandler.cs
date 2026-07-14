//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PrivateMailHandler.cs
// 描述：PrivateMail 业务处理类
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;
using Blue.BusinessInterface.GeneralAffairModule;

namespace Blue.BusinessLogic.GeneralAffairModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.PrivateMail.
    /// </summary>
    public class PrivateMailHandler : IPrivateMailHandler
    {
        #region 工厂类实例

        private static readonly IPrivateMail dalPrivateMail = GeneralAffairDataAccessFactory.CreatePrivateMail();
        private static readonly IUserAndMail dalUserAndMail = GeneralAffairDataAccessFactory.CreateUserAndMail();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrivateMailHandler()
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 privatemail 表中插入一条新记录
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo)
        {
            //自动增加的关键字的值
            decimal privateMailId = 0;

            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                privateMailId = dalPrivateMail.Insert(privateMailInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailId;
        }

        /// <summary>
        /// 获得 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        /// <returns> PrivateMailInfo 对象</returns>
        public PrivateMailInfo GetModelInfo(decimal mailId)
        {
            PrivateMailInfo privateMailInfo = null;

            // 验证输入
            if (mailId < 0)
            {
                return null;
            }

            try
            {
                privateMailInfo = dalPrivateMail.GetModelInfo(mailId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailInfo;
        }

        /// <summary>
        /// 更新 PrivateMailInfo 对象
        /// </summary>
        /// <param name="privateMailInfo">PrivateMailInfo 对象</param>
        public void Update(PrivateMailInfo privateMailInfo)
        {
            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalPrivateMail.Update(privateMailInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        /// <returns> PrivateMailInfo 对象</returns>
        public void Delete(decimal mailId)
        {
            // 验证输入
            if (mailId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalPrivateMail.Delete(mailId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 PrivateMailInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrivateMailInfo 对象列表</returns>
        public IList<PrivateMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<PrivateMailInfo> privateMailInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                privateMailInfos = dalPrivateMail.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailInfos;
        }

        /// <summary>
        /// 获得 PrivateMail 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>PrivateMailInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalPrivateMail.GetTotalCount(whereConditons);
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
        /// 获得未读邮件数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUnreadMailCount(decimal userId)
        {
            int count = 0;

            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("UserAndMail", "UserId", "UserId", DbType.Decimal, userId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAndMail", "ReadStatus", "ReadStatus", DbType.Byte, (byte)MailState.New,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                count = dalPrivateMail.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
		/// 获得 UserAndMailInfo 对象
		/// </summary>
		///<param name="mailId">邮件编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndMailInfo 对象</returns>
		public UserAndMailInfo GetUserAndMailInfo(decimal mailId, decimal userId)
        {
            UserAndMailInfo userAndMailInfo = null;

            // 验证输入
            if (mailId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                userAndMailInfo = dalUserAndMail.GetModelInfo(mailId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAndMailInfo;
        }

        /// <summary>
        /// 通过邮件编号获取用户编号
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal mailId)
        {
            decimal userId = decimal.MinValue;

            // 验证输入
            if (mailId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                userId = dalPrivateMail.GetUserId(mailId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal privateMailId = 0;

            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                privateMailId = dalPrivateMail.Insert(privateMailInfo, upLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailId;
        }

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos)
        {
            //自动增加的关键字的值
            decimal privateMailId = 0;

            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                privateMailId = dalPrivateMail.Insert(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailId;
        }

        /// <summary>
        /// 更新邮件信息，并写入收件人相关信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        public void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos)
        {
            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }

            try
            {
                dalPrivateMail.Update(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            // 验证输入
            if (privateMailInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }

            try
            {
                dalPrivateMail.Update(privateMailInfo, upLoadFileInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 从发件箱或是草稿箱中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="isDraft">是否是草稿</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public DataSet GetEmailsInBox(decimal userId, string condition, bool isDraft, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                List<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("SendTime", CustomSorting.Descending));

                if (!string.IsNullOrWhiteSpace(condition))
                {
                    string content = Regex.Replace(condition, " {1,}", "%");
                    whereConditons.Add(new WhereConditon("PrivateMail", "MailTitle", "MailTitle", System.Data.DbType.String, content,
                          DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                whereConditons.Add(new WhereConditon("UserId", "UserId", System.Data.DbType.Decimal, userId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("IsDraft", "IsDraft", System.Data.DbType.Boolean, isDraft,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("IsDeleted", "IsDeleted", System.Data.DbType.Boolean, false,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

                ds = dalPrivateMail.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 从收件箱中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public DataSet GetReceivedEmails(decimal userId, string condition, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                List<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("SendTime", CustomSorting.Descending));

                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("UserAndMail", "UserId", "UserId", System.Data.DbType.Decimal, userId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAndMail", "IsDeleted", "IsDeleted", System.Data.DbType.Boolean, false,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

                if (!string.IsNullOrWhiteSpace(condition))
                {
                    string content = Regex.Replace(condition, " {1,}", "%");
                    whereConditons.Add(new WhereConditon("PrivateMail", "MailTitle", "MailTitle", System.Data.DbType.String, content,
                          DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

                }
                ds = dalPrivateMail.GetPageRecordOfMultiTables(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 从回收站中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public DataSet GetEmailsInRecycleBin(decimal userId, string condition, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                List<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("SendTime", CustomSorting.Descending));

                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("PrivateMail", "UserId", "UserId_1", System.Data.DbType.Decimal, userId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("PrivateMail", "IsDeleted", "IsDeleted_1", System.Data.DbType.Boolean, true,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
                whereConditons.Add(new WhereConditon("UserAndMail", "UserId", "UserId_2", System.Data.DbType.Decimal, userId,
                          DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserAndMail", "IsDeleted", "IsDeleted_2", System.Data.DbType.Boolean, true,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));

                if (!string.IsNullOrWhiteSpace(condition))
                {
                    string content = Regex.Replace(condition, " {1,}", "%");
                    whereConditons.Add(new WhereConditon("PrivateMail", "MailTitle", "MailTitle", System.Data.DbType.String, content,
                          DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

                }
                ds = dalPrivateMail.GetPageRecordOfMultiTablesOnFullOuterJoin(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        public void UpdateReadStatus(decimal mailId, decimal userId, MailState mailState)
        {
            if (mailId <= 0)
            {
                throw new ArgumentException("邮件编号不能为空。");
            }
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }
            try
            {
                dalUserAndMail.Update(new CorrelatedModel(mailId, userId, (byte)mailState));
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        public void UpdateReadStatus(IList<decimal> mailIds, decimal userId, MailState mailState)
        {
            if (mailIds.Count == 0)
            {
                throw new ArgumentException("邮件编号不能为空。");
            }
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能为空。");
            }
            try
            {
                dalUserAndMail.Update(mailIds, userId, (byte)mailState);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        /// <param name="mailBoxType"></param>
        public void Delete(decimal userId, decimal mailId, MailBoxType mailBoxType)
        {
            switch (mailBoxType)
            {
                case MailBoxType.RecycleBin:
                    dalPrivateMail.Delete(userId, mailId);
                    break;

                case MailBoxType.InBox:
                    dalUserAndMail.Update(mailId, userId, true);
                    break;

                case MailBoxType.DraftBox:
                case MailBoxType.OutBox:
                    dalPrivateMail.Update(mailId, true);
                    break;
            }
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        /// <param name="mailBoxType"></param>
        public void Delete(decimal userId, IList<decimal> mailIds, MailBoxType mailBoxType)
        {
            switch (mailBoxType)
            {
                case MailBoxType.RecycleBin:
                    dalPrivateMail.Delete(userId, mailIds);
                    break;

                case MailBoxType.InBox:
                    dalUserAndMail.Update(mailIds, userId, true);
                    break;

                case MailBoxType.DraftBox:
                case MailBoxType.OutBox:
                    dalPrivateMail.Update(mailIds, true);
                    break;
            }
        }

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        public void RecoverMail(decimal userId, decimal mailId)
        {
            IList<decimal> mailIds = new List<decimal>();
            mailIds.Add(mailId);
            RecoverMail(userId, mailIds);
        }

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        public void RecoverMail(decimal userId, IList<decimal> mailIds)
        {
            foreach (decimal mailId in mailIds)
            {
                UserAndMailInfo userAndMailInfo = dalUserAndMail.GetModelInfo(mailId, userId);
                if (userAndMailInfo != null && userAndMailInfo.IsDeleted)
                {
                    dalUserAndMail.Update(mailId, userId, false);
                }
                else
                {
                    dalPrivateMail.Update(mailId, false);
                }
            }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
