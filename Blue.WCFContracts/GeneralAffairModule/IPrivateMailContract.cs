//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IPrivateMailContract.cs
// 描述： PrivateMail 契约层接口
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.WCFContracts.GeneralAffairModule
{
    /// <summary>
    /// PrivateMail 契约接口
    /// </summary>
    [ServiceContract(Name = "IPrivateMailContract", Namespace = "http://www.scu.edu.cn/CommonModule/")]
    public interface IPrivateMailContract :  IPrincipalContracts<PrivateMailInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        [OperationContract(Name = "UpdateReadStatus")]
        void UpdateReadStatus(decimal mailId, decimal userId, MailState mailState);

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <returns></returns>
        [OperationContract(Name = "InsertPrivateMailInfo")]
        decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        /// <returns></returns>
        [OperationContract(Name = "InsertPrivateMailInfoAndUserInfos")]
        decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos);

        /// <summary>
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        [OperationContract(Name = "UpdatePrivateMailInfo")]
        void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新邮件信息，并写入收件人相关信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        [OperationContract(Name = "UpdatePrivateMailInfoAndInsertUserInfos")]
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
        [OperationContract(Name = "GetEmailsInBox")]
        DataSet GetEmailsInBox(decimal userId, string condition, bool isDraft, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 从收件箱中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract(Name = "GetReceivedEmails")]
        DataSet GetReceivedEmails(decimal userId, string condition, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 从回收站中获得表 PrivateMail 的分页数据集
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="condition">查询条件</param>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        [OperationContract(Name = "GetEmailsInRecycleBin")]
        DataSet GetEmailsInRecycleBin(decimal userId, string condition, int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 更新邮件的阅读状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="mailState"></param>
        [OperationContract(Name = "BatchUpdateReadStatus")]
        void UpdateReadStatus(IList<decimal> mailIds, decimal userId, MailState mailState);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        /// <param name="mailBoxType"></param>
        [OperationContract(Name = "DeleteMail")]
        void Delete(decimal userId, decimal mailId, MailBoxType mailBoxType);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        /// <param name="mailBoxType"></param>
        [OperationContract(Name = "DeleteMails")]
        void Delete(decimal userId, IList<decimal> mailIds, MailBoxType mailBoxType);

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        [OperationContract(Name = "RecoverMail")]
        void RecoverMail(decimal userId, decimal mailId);

        /// <summary>
        /// 还原邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        [OperationContract(Name = "RecoverMails")]
        void RecoverMail(decimal userId, IList<decimal> mailIds);

        #endregion
    }
}