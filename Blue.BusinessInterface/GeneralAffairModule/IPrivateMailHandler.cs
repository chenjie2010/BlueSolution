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
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.BusinessInterface.GeneralAffairModule
{
    /// <summary>
    /// PrivateMail 接口
    /// </summary>
    public interface IPrivateMailHandler : IPrincipalBusiness<PrivateMailInfo>
    {
        #region 接口

        /// <summary>
        /// 获得未读邮件数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetUnreadMailCount(decimal userId);

        /// <summary>
        /// 获得 UserAndMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        ///<param name="userId">用户编号</param>
        /// <returns> UserAndMailInfo 对象</returns>
        UserAndMailInfo GetUserAndMailInfo(decimal mailId, decimal userId);

        /// <summary>
        /// 通过邮件编号获取用户编号
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        decimal GetUserId(decimal mailId);

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <returns></returns>
        decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        /// <returns></returns>
        decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos);

        /// <summary>
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新邮件信息，并写入收件人相关信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos);

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
        DataSet GetEmailsInBox(decimal userId, string condition, bool isDraft, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 从回收站中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        DataSet GetEmailsInRecycleBin(decimal userId, string condition, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 从收件箱中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        DataSet GetReceivedEmails(decimal userId, string condition, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        /// <param name="mailBoxType"></param>
        void Delete(decimal userId, decimal mailId, MailBoxType mailBoxType);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        /// <param name="mailBoxType"></param>
        void Delete(decimal userId, IList<decimal> mailIds, MailBoxType mailBoxType);

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        void UpdateReadStatus(decimal mailId, decimal userId, MailState mailState);

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        void UpdateReadStatus(IList<decimal> mailIds, decimal userId, MailState mailState);

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        void RecoverMail(decimal userId, decimal mailId);

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        void RecoverMail(decimal userId, IList<decimal> mailIds);

        #endregion
    }
}
