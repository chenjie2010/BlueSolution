//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserMessageContract.cs
// 描述: UserMessage 契约层接口
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.SystemModule;

namespace Blue.WCFContracts.SystemModule
{
    /// <summary>
    /// UserMessage 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserMessageContract", Namespace = "http://www.scu.edu.cn/SystemModule/")]
    public interface IUserMessageContract :  IPrincipalContracts<UserMessageInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得以表 UserMessage 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecordOfMultiTables")]
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);
        
        /// <summary>
        /// 批量插入消息
        /// </summary>
        /// <param name="userMessageInfo"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [OperationContract(Name = "InsertUserMessage")]
        decimal InsertUserMessage(UserMessageInfo userMessageInfo, AttachmentCategory attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<decimal> roleIds);

        /// <summary>
        /// 批量更新信息
        /// </summary>
        /// <param name="userMessageInfo"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="roleIds"></param>
        [OperationContract(Name = "UpdateUserMessageInfo")]
        void UpdateUserMessageInfo(UserMessageInfo userMessageInfo, AttachmentCategory attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<decimal> roleIds);

        /// <summary>
        /// 根据公告编号获得角色列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRoles")]
        IList<CommonNode> GetRoles(decimal messageId);
        
        /// <summary>
        /// 批量删除消息列表
        /// </summary>
        /// <param name="messageIds"></param>
        [OperationContract(Name = "DeleteUserMessages")]
        void DeleteUserMessages(IList<decimal> messageIds);

        /// <summary>
        /// 获得表 UserMessage 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 是否授权读取该通知
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "IsAuthoritiedNotice")]
        bool IsAuthoritiedNotice(decimal messageId, decimal userId);

        #endregion
    }
}