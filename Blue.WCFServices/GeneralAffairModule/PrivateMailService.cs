//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PrivateMailService.cs
// 描述：PrivateMail 操作服务类
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.GeneralAffairModule;
using Blue.BusinessInterface.GeneralAffairModule;
using Blue.WCFContracts.GeneralAffairModule;

namespace Blue.WCFServices.GeneralAffairModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.PrivateMail.
    /// </summary>
    public class PrivateMailService : IPrivateMailContract
    {
        #region 业务实例

        private static readonly IPrivateMailHandler privateMailHandler = BusinessLogicContainer.Instance.GeneralAffairModuleContainer.Resolve<IPrivateMailHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrivateMailService()
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 privatemail 表中插入一条新记录
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo)
        {
            return privateMailHandler.Insert(privateMailInfo);
        }

        /// <summary>
        /// 获得 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        /// <returns> PrivateMailInfo 对象</returns>
        public PrivateMailInfo GetModelInfo(decimal mailId)
        {
            return privateMailHandler.GetModelInfo(mailId);
        }

        /// <summary>
        /// 更新 PrivateMailInfo 对象
        /// </summary>
        /// <param name="privateMailInfo">PrivateMailInfo 对象</param>
        public void Update(PrivateMailInfo privateMailInfo)
        {
            privateMailHandler.Update(privateMailInfo);
        }

        /// <summary>
        /// 删除 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        /// <returns> PrivateMailInfo 对象</returns>
        public void Delete(decimal mailId)
        {
            privateMailHandler.Delete(mailId);
        }

        /// <summary>
        /// 获得 PrivateMailInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrivateMailInfo 对象列表</returns>
        public IList<PrivateMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return privateMailHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 PrivateMail 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>PrivateMailInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return privateMailHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            return privateMailHandler.Insert(privateMailInfo, upLoadFileInfos);
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
            return privateMailHandler.Insert(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);
        }

        /// <summary>
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            privateMailHandler.Update(privateMailInfo, upLoadFileInfos);
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
            privateMailHandler.Update(privateMailInfo, upLoadFileInfos, userInfos, roleInfos);
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
            return privateMailHandler.GetEmailsInBox(userId, condition, isDraft, startPosition, count, ref totalCount);
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
            return privateMailHandler.GetReceivedEmails(userId, condition, startPosition, count, ref totalCount);
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
            return privateMailHandler.GetEmailsInRecycleBin(userId, condition, startPosition, count, ref totalCount);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        /// <param name="mailBoxType"></param>
        public void Delete(decimal userId, decimal mailId, MailBoxType mailBoxType)
        {
            privateMailHandler.Delete(userId, mailId, mailBoxType);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        /// <param name="mailBoxType"></param>
        public void Delete(decimal userId, IList<decimal> mailIds, MailBoxType mailBoxType)
        {
            privateMailHandler.Delete(userId, mailIds, mailBoxType);
        }

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        public void UpdateReadStatus(decimal mailId, decimal userId, MailState mailState)
        {
            privateMailHandler.UpdateReadStatus(mailId, userId, mailState);
        }
        
        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        public void UpdateReadStatus(IList<decimal> mailIds, decimal userId, MailState mailState)
        {
            privateMailHandler.UpdateReadStatus(mailIds, userId, mailState);
        }

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        public void RecoverMail(decimal userId, decimal mailId)
        {
            privateMailHandler.RecoverMail(userId, mailId);
        }

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        public void RecoverMail(decimal userId, IList<decimal> mailIds)
        {
            privateMailHandler.RecoverMail(userId, mailIds);
        }

        #endregion
    }
}
