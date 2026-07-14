//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PriavteAttachmentInfo.cs
// 描述：PriavteAttachmentInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/11/29
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.GeneralAffairModule
{
    /// <summary>
    /// <para>PriavteAttachmentInfo 类</para>
    /// <para>附件</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class PriavteAttachmentInfo
    {
        #region 内部成员变量

        private decimal _attachmentId;
        private string _attachmentName = string.Empty;
        private byte _attachmentCategory;
        private string _attachmentSourceName = string.Empty;
        private string _attachmentPath = string.Empty;
        private byte _attachmentType;
        private int _attachmenSize;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PriavteAttachmentInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="attachmentId">附件编号</param>
        ///<param name="attachmentName">附件新名称</param>
        ///<param name="attachmentCategory">附件分类</param>
        ///<param name="attachmentSourceName">附件源名称</param>
        ///<param name="attachmentPath">附件路径</param>
        ///<param name="attachmentType">附件类型</param>
        ///<param name="attachmenSize">附件大小</param>
        ///<param name="sorting">排序</param>
        public PriavteAttachmentInfo(decimal attachmentId, string attachmentName, byte attachmentCategory, string attachmentSourceName, string attachmentPath,
            byte attachmentType, int attachmenSize, int sorting)
        {
            _attachmentId = attachmentId;
            _attachmentName = attachmentName;
            _attachmentCategory = attachmentCategory;
            _attachmentSourceName = attachmentSourceName;
            _attachmentPath = attachmentPath;
            _attachmentType = attachmentType;
            _attachmenSize = attachmenSize;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 附件编号
        /// </summary>
        public decimal AttachmentId
        {
            get
            {
                return _attachmentId;
            }
            set
            {
                if (_attachmentId == value)
                    return;
                _attachmentId = value;
            }
        }

        /// <summary>
        /// 附件新名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 附件新名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "附件新名称长度范围在1位～64位！")]
        public string AttachmentName
        {
            get
            {
                return _attachmentName;
            }
            set
            {
                if (_attachmentName == value)
                    return;
                _attachmentName = value;
            }
        }

        /// <summary>
        /// 附件分类
        /// </summary>
        public byte AttachmentCategory
        {
            get
            {
                return _attachmentCategory;
            }
            set
            {
                if (_attachmentCategory == value)
                    return;
                _attachmentCategory = value;
            }
        }

        /// <summary>
        /// 附件源名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 附件源名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "附件源名称长度范围在1位～256位！")]
        public string AttachmentSourceName
        {
            get
            {
                return _attachmentSourceName;
            }
            set
            {
                if (_attachmentSourceName == value)
                    return;
                _attachmentSourceName = value;
            }
        }

        /// <summary>
        /// 附件路径
        /// </summary>
        [NotNullValidator(MessageTemplate = " 附件路径不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "附件路径长度范围在1位～256位！")]
        public string AttachmentPath
        {
            get
            {
                return _attachmentPath;
            }
            set
            {
                if (_attachmentPath == value)
                    return;
                _attachmentPath = value;
            }
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public byte AttachmentType
        {
            get
            {
                return _attachmentType;
            }
            set
            {
                if (_attachmentType == value)
                    return;
                _attachmentType = value;
            }
        }

        /// <summary>
        /// 附件大小
        /// </summary>
        public int AttachmenSize
        {
            get
            {
                return _attachmenSize;
            }
            set
            {
                if (_attachmenSize == value)
                    return;
                _attachmenSize = value;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sorting
        {
            get
            {
                return _sorting;
            }
            set
            {
                if (_sorting == value)
                    return;
                _sorting = value;
            }
        }

        #endregion

    }
}