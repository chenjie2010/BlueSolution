//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IPriavteAttachmentContract.cs
// 描述： PriavteAttachment 契约层接口
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
using AppFramework.Reference.WCFLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.WCFContracts.GeneralAffairModule
{
    /// <summary>
    /// PriavteAttachment 契约接口
    /// </summary>
    [ServiceContract(Name = "IPriavteAttachmentContract", Namespace = "http://www.scu.edu.cn/GeneralAffairModule/")]
    public interface IPriavteAttachmentContract
    {
        #region 自定义接口

        /// <summary>
        /// 获得邮件的附件列表
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPriavteAttachmentInfos")]
        IList<PriavteAttachmentInfo> GetModelInfos(decimal attachmentId, byte attachmentCategory);

        /// <summary>
        /// 获得附件的路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAttachmentPath")]
        string GetAttachmentPath(decimal attachmentId, byte attachmentCategory, int sorting);

        /// <summary>
        /// 获得附件的数据
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetAttachmentData")]
        byte[] GetAttachmentData(decimal attachmentId, byte attachmentCategory, int sorting);
        
        #endregion
    }
}