//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PriavteAttachmentService.cs
// 描述：PriavteAttachment 操作服务类
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
    /// 操作服务类，对于的表： dbo.PriavteAttachment.
    /// </summary>
    public class PriavteAttachmentService : IPriavteAttachmentContract
    {
        #region 业务实例
        
        private static readonly IPriavteAttachmentHandler priavteAttachmentHandler = BusinessLogicContainer.Instance.GeneralAffairModuleContainer.Resolve<IPriavteAttachmentHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public PriavteAttachmentService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 priavteattachment 表中插入一条新记录
		/// </summary>
		/// <param name="priavteAttachmentInfo"></param>
		public void Insert(PriavteAttachmentInfo priavteAttachmentInfo)
		{
            priavteAttachmentHandler.Insert(priavteAttachmentInfo);
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
            return priavteAttachmentHandler.GetModelInfo(attachmentId, attachmentCategory, sorting);           
		}

        /// <summary>
        /// 删除 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        public void Delete(decimal attachmentId, byte attachmentCategory, int sorting)       
		{	
            priavteAttachmentHandler.Delete(attachmentId, attachmentCategory, sorting);
        }
        
        #endregion

        #region 实现自定义接口       

        /// <summary>
        /// 获得邮件的附件列表
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <returns></returns>
        public IList<PriavteAttachmentInfo> GetModelInfos(decimal attachmentId, byte attachmentCategory)
        {
            return priavteAttachmentHandler.GetModelInfos(attachmentId, attachmentCategory);
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
            return priavteAttachmentHandler.GetAttachmentPath(attachmentId, attachmentCategory, sorting);
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
            return priavteAttachmentHandler.GetAttachmentData(attachmentId, attachmentCategory, sorting);
        }

        #endregion
    }
}
