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
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;
using Blue.BusinessInterface.GeneralAffairModule;

namespace Blue.BusinessLogic.GeneralAffairModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.PriavteAttachment.
    /// </summary>
    public class PriavteAttachmentHandler : IPriavteAttachmentHandler
    {
        #region 工厂类实例
        
        private static readonly IPriavteAttachment dalPriavteAttachment = GeneralAffairDataAccessFactory.CreatePriavteAttachment(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public PriavteAttachmentHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 priavteattachment 表中插入一条新记录
		/// </summary>
		/// <param name="priavteAttachmentInfo"></param>
		public void Insert(PriavteAttachmentInfo priavteAttachmentInfo)
		{
			// 验证输入
			if (priavteAttachmentInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                dalPriavteAttachment.Insert(priavteAttachmentInfo);                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
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
			PriavteAttachmentInfo  priavteAttachmentInfo = null;
            
			// 验证输入
			if(attachmentId < 0)
            {
				return null;
            }

            try
            {
                priavteAttachmentInfo =  dalPriavteAttachment.GetModelInfo(attachmentId, attachmentCategory, sorting);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return priavteAttachmentInfo;
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
            // 验证输入
            if (attachmentId < 0)
            {
                return;
            }

            try
            {
                dalPriavteAttachment.Delete(attachmentId, attachmentCategory);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 PriavteAttachmentInfo 对象
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="sorting"></param>
        public void Delete(decimal attachmentId, byte attachmentCategory, int sorting)
        {
            // 验证输入
            if (attachmentId < 0)
            {
                return;
            }

            try
            {
                dalPriavteAttachment.Delete(attachmentId, attachmentCategory, sorting);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 获得邮件的附件列表
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="attachmentCategory"></param>
        /// <returns></returns>
        public IList<PriavteAttachmentInfo> GetModelInfos(decimal attachmentId, byte attachmentCategory)
        {
            IList<PriavteAttachmentInfo> priavteAttachmentInfos = null;

            // 验证输入
            if (attachmentId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                priavteAttachmentInfos = dalPriavteAttachment.GetModelInfos(attachmentId, attachmentCategory);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

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
            string path = string.Empty;

            // 验证输入
            if (attachmentId <= 0)
            {
                throw new ArgumentException("附件编号错误.");
            }

            try
            {
                path = dalPriavteAttachment.GetAttachmentPath(attachmentId, attachmentCategory, sorting);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return path;
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

            // 验证输入
            if (attachmentId <= 0)
            {
                throw new ArgumentException("附件编号错误。");
            }

            try
            {
                data = dalPriavteAttachment.GetAttachmentData(attachmentId, attachmentCategory, sorting);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
