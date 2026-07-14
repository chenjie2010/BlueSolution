//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PriavteAttachmentHandler.cs
// 描述：PriavteAttachment 业务处理类
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
    /// PriavteAttachment 接口
    /// </summary>
    public interface IPriavteAttachmentHandler
    {
        #region 接口

        /// <summary>
		/// 向 PriavteAttachment 表中插入一条新记录
		/// </summary>
		/// <param name="priavteAttachmentInfo">priavteAttachmentInfo 对象</param>
		void Insert(PriavteAttachmentInfo priavteAttachmentInfo);

        /// <summary>
        /// 获得 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
		PriavteAttachmentInfo GetModelInfo(decimal attachmentId, byte attachmentCategory, int sorting);

        /// <summary>
        /// 删除某一业务下所有的附件
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        void Delete(decimal attachmentId, byte attachmentCategory);

        /// <summary>
        /// 删除 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        void Delete(decimal attachmentId, byte attachmentCategory, int sorting);

        /// <summary>
        /// 获得邮件的附件列表
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <returns></returns>
        IList<PriavteAttachmentInfo> GetModelInfos(decimal attachmentId, byte attachmentCategory);

        /// <summary>
        /// 获得附件的路径
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        string GetAttachmentPath(decimal attachmentId, byte attachmentCategory, int sorting);

        /// <summary>
        /// 获得附件的数据
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        byte[] GetAttachmentData(decimal attachmentId, byte attachmentCategory, int sorting);

        #endregion
    }
}
