//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAndMailInfo.cs
// 描述：UserAndMailInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/9/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.GeneralAffairModule
{
    /// <summary>
    /// <para>UserAndMailInfo 类</para>
    /// <para>用户与邮件</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class UserAndMailInfo
    {
        #region 内部成员变量

        private decimal _mailId;
        private decimal _userId;
        private byte _readStatus;
        private byte _deliveryMode;
        private bool _isDeleted;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserAndMailInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="readStatus"></param>
        ///<param name="deliveryMode">发送方式</param>
        ///<param name="isDeleted">是否删除</param>
        public UserAndMailInfo(decimal mailId, decimal userId, byte readStatus, byte deliveryMode, bool isDeleted)
        {
            _mailId = mailId;
            _userId = userId;
            _readStatus = readStatus;
            _deliveryMode = deliveryMode;
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
        /// 
        /// </summary>
        public byte ReadStatus
        {
            get
            {
                return _readStatus;
            }
            set
            {
                if (_readStatus == value)
                    return;
                _readStatus = value;
            }
        }

        /// <summary>
        /// 发送方式
        /// </summary>
        public byte DeliveryMode
        {
            get
            {
                return _deliveryMode;
            }
            set
            {
                if (_deliveryMode == value)
                    return;
                _deliveryMode = value;
            }
        }

        /// <summary>
        /// 是否删除
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