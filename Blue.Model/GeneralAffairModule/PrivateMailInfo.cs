//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PrivateMailInfo.cs
// 描述：PrivateMailInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/9/26
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.GeneralAffairModule
{
    /// <summary>
    /// <para>PrivateMailInfo 类</para>
    /// <para>内部邮件</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class PrivateMailInfo
    {
        #region 内部成员变量

        private decimal _mailId;
        private decimal _userId;
        private string _mailTitle = string.Empty;
        private string _mailContent = string.Empty;
        private byte _mailPriority;
        private bool _hasAttachment;
        private DateTime _sendTime;
        private bool _isDraft;
        private string _receiver = string.Empty;
        private string _copyer = string.Empty;
        private string _blind = string.Empty;
        private bool _isDeleted;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrivateMailInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="mailTitle">邮件标题</param>
        ///<param name="mailContent">邮件内容</param>
        ///<param name="mailPriority">优先级</param>
        ///<param name="hasAttachment">附件</param>
        ///<param name="sendTime">发送时间</param>
        ///<param name="isDraft">草稿</param>
        ///<param name="receiver">收件人文本</param>
        ///<param name="copyer">抄送人文本</param>
        ///<param name="blind">密送人文本</param>
        ///<param name="isDeleted">删除</param>
        public PrivateMailInfo(decimal mailId, decimal userId, string mailTitle, string mailContent, byte mailPriority,
            bool hasAttachment, DateTime sendTime, bool isDraft, string receiver, string copyer,
            string blind, bool isDeleted)
        {
            _mailId = mailId;
            _userId = userId;
            _mailTitle = mailTitle;
            _mailContent = mailContent;
            _mailPriority = mailPriority;
            _hasAttachment = hasAttachment;
            _sendTime = sendTime;
            _isDraft = isDraft;
            _receiver = receiver;
            _copyer = copyer;
            _blind = blind;
            _isDeleted = isDeleted;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 邮件编号
        /// </summary>
        public decimal MailId
        {
            get
            {
                return _mailId;
            }
            set
            {
                if (_mailId == value)
                    return;
                _mailId = value;
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId == value)
                    return;
                _userId = value;
            }
        }

        /// <summary>
        /// 邮件标题
        /// </summary>
        [NotNullValidator(MessageTemplate = " 邮件标题不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "邮件标题长度范围在1位～256位！")]
        public string MailTitle
        {
            get
            {
                return _mailTitle;
            }
            set
            {
                if (_mailTitle == value)
                    return;
                _mailTitle = value;
            }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        [StringLengthValidator(0, Int32.MaxValue, MessageTemplate = "邮件内容长度超过规定长度(2147483647个字符)！")]
        public string MailContent
        {
            get
            {
                return _mailContent;
            }
            set
            {
                if (_mailContent == value)
                    return;
                _mailContent = value;
            }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public byte MailPriority
        {
            get
            {
                return _mailPriority;
            }
            set
            {
                if (_mailPriority == value)
                    return;
                _mailPriority = value;
            }
        }

        /// <summary>
        /// 附件
        /// </summary>
        public bool HasAttachment
        {
            get
            {
                return _hasAttachment;
            }
            set
            {
                if (_hasAttachment == value)
                    return;
                _hasAttachment = value;
            }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get
            {
                return _sendTime;
            }
            set
            {
                if (_sendTime == value)
                    return;
                _sendTime = value;
            }
        }

        /// <summary>
        /// 草稿
        /// </summary>
        public bool IsDraft
        {
            get
            {
                return _isDraft;
            }
            set
            {
                if (_isDraft == value)
                    return;
                _isDraft = value;
            }
        }

        /// <summary>
        /// 收件人文本
        /// </summary>
        [NotNullValidator(MessageTemplate = " 收件人文本不能为空")]
        [StringLengthValidator(1, 1024, MessageTemplate = "收件人文本长度范围在1位～1024位！")]
        public string Receiver
        {
            get
            {
                return _receiver;
            }
            set
            {
                if (_receiver == value)
                    return;
                _receiver = value;
            }
        }

        /// <summary>
        /// 抄送人文本
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "抄送人文本长度不能超过512位！")]
        public string Copyer
        {
            get
            {
                return _copyer;
            }
            set
            {
                if (_copyer == value)
                    return;
                _copyer = value;
            }
        }

        /// <summary>
        /// 密送人文本
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "密送人文本长度不能超过512位！")]
        public string Blind
        {
            get
            {
                return _blind;
            }
            set
            {
                if (_blind == value)
                    return;
                _blind = value;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                if (_isDeleted == value)
                    return;
                _isDeleted = value;
            }
        }

        #endregion

    }
}