//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IPrivateMail.cs
// 描述：PrivateMail 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.IDAL.GeneralAffairModule
{
    /// <summary>
    /// PrivateMail 接口
    /// </summary>
    public interface IPrivateMail : IPrincipalTable<PrivateMailInfo>
    {
        #region 接口
        
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
        /// 更新邮件信息，并写入收件人相关信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos);

        /// <summary>
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 获得表 PrivateMail 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得以表 PrivateMail 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得以表 PrivateMail 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecordOfMultiTablesOnFullOuterJoin(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 更新邮件状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="isDeleted"></param>
        void Update(decimal mailId, bool isDeleted);

        /// <summary>
        /// 更新邮件状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="isDeleted"></param>
        void Update(IList<decimal> mailIds, bool isDeleted);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        void Delete(decimal userId, decimal mailId);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        void Delete(decimal userId, IList<decimal> mailIds);

        #endregion
    }
}