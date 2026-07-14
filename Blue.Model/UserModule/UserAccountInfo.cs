//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAccountInfo.cs
// 描述：UserAccountInfo 实体类
// 作者：ChenJie 
// 编写日期：2016/8/27
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.UserModule
{
    /// <summary>
    /// <para>UserAccountInfo 类</para>
    /// <para>用户帐号</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class UserAccountInfo
    {
        #region 内部成员变量

        private decimal _userId;
        private decimal _depId;
        private decimal _userTypeId;
        private string _userName = string.Empty;
        private string _userPwd = string.Empty;
        private string _userActualName = string.Empty;
        private string _emailAddress = string.Empty;
        private byte _identificationType;
        private string _userIdentity = string.Empty;
        private string _telephoneNumber = string.Empty;
        private DateTime _lastLogonTime;
        private string _lastLogonIP = string.Empty;
        private string _photoSuffixName = string.Empty;
        private bool _lockedOut;
        private long _dataFieldAuthority;
        private long _departmentAuthority;
        private Guid _uniqueUserIdentity = Guid.Empty;
        private string _notes = string.Empty;
        private int _retryTimes;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserAccountInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="userId">用户编号</param>
        ///<param name="depId">部门编号</param>
        ///<param name="userTypeId">用户类型编号</param>
        ///<param name="userName">用户名</param>
        ///<param name="userPwd">用户密码</param>
        ///<param name="userActualName">用户姓名</param>
        ///<param name="emailAddress">电子邮件</param>
        ///<param name="identificationType">证件类型</param>
        ///<param name="userIdentity">身份证号</param>
        ///<param name="telephoneNumber">手机号码</param>
        ///<param name="lastLogonTime">上次登录时间</param>
        ///<param name="lastLogonIP">上次登录地址</param>
        ///<param name="photoSuffixName">照片</param>
        ///<param name="lockedOut">是否锁定</param>
        ///<param name="dataFieldAuthority">字段修改权限</param>
        ///<param name="departmentAuthority">单位管理权限</param>
        ///<param name="uniqueUserIdentity">用户唯一标示符</param>
        ///<param name="notes">备注</param>
        ///<param name="retryTimes">重试次数</param>
        ///<param name="createdTime">创建时间</param>
        ///<param name="updatedTime">更新时间</param>
        public UserAccountInfo(decimal userId, decimal depId, decimal userTypeId, string userName, string userPwd,
            string userActualName, string emailAddress, byte identificationType, string userIdentity, string telephoneNumber,
            DateTime lastLogonTime, string lastLogonIP, string photoSuffixName, bool lockedOut, long dataFieldAuthority,
            long departmentAuthority, Guid uniqueUserIdentity, string notes, int retryTimes, DateTime createdTime, DateTime updatedTime)
        {
            _userId = userId;
            _depId = depId;
            _userTypeId = userTypeId;
            _userName = userName;
            _userPwd = userPwd;
            _userActualName = userActualName;
            _emailAddress = emailAddress;
            _identificationType = identificationType;
            _userIdentity = userIdentity;
            _telephoneNumber = telephoneNumber;
            _lastLogonTime = lastLogonTime;
            _lastLogonIP = lastLogonIP;
            _photoSuffixName = photoSuffixName;
            _lockedOut = lockedOut;
            _dataFieldAuthority = dataFieldAuthority;
            _departmentAuthority = departmentAuthority;
            _uniqueUserIdentity = uniqueUserIdentity;
            _notes = notes;
            _retryTimes = retryTimes;
            _createdTime = createdTime;
            _updatedTime = updatedTime;
        }

        #endregion

        #region 字段属性

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
        /// 部门编号
        /// </summary>
        public decimal DepId
        {
            get
            {
                return _depId;
            }
            set
            {
                if (_depId == value)
                    return;
                _depId = value;
            }
        }

        /// <summary>
        /// 用户类型编号
        /// </summary>
        public decimal UserTypeId
        {
            get
            {
                return _userTypeId;
            }
            set
            {
                if (_userTypeId == value)
                    return;
                _userTypeId = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [NotNullValidator(MessageTemplate = " 用户名不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "用户名长度范围在1位～32位！")]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName == value)
                    return;
                _userName = value;
            }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd
        {
            get
            {
                return _userPwd;
            }
            set
            {
                if (_userPwd == value)
                    return;
                _userPwd = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [NotNullValidator(MessageTemplate = " 用户姓名不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "用户姓名长度范围在1位～128位！")]
        public string UserActualName
        {
            get
            {
                return _userActualName;
            }
            set
            {
                if (_userActualName == value)
                    return;
                _userActualName = value;
            }
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        [NotNullValidator(MessageTemplate = " 邮件地址不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "邮件地址长度范围在1位～64位！")]
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                if (_emailAddress == value)
                    return;
                _emailAddress = value;
            }
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public byte IdentificationType
        {
            get
            {
                return _identificationType;
            }
            set
            {
                if (_identificationType == value)
                    return;
                _identificationType = value;
            }
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        [NotNullValidator(MessageTemplate = " 身份证号不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "身份证号长度范围在1位～64位！")]
        public string UserIdentity
        {
            get
            {
                return _userIdentity;
            }
            set
            {
                if (_userIdentity == value)
                    return;
                _userIdentity = value;
            }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLengthValidator(0, 16, MessageTemplate = "手机号码长度不能超过16位！")]
        public string TelephoneNumber
        {
            get
            {
                return _telephoneNumber;
            }
            set
            {
                if (_telephoneNumber == value)
                    return;
                _telephoneNumber = value;
            }
        }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LastLogonTime
        {
            get
            {
                return _lastLogonTime;
            }
            set
            {
                if (_lastLogonTime == value)
                    return;
                _lastLogonTime = value;
            }
        }

        /// <summary>
        /// 上次登录地址
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "上次登录地址长度不能超过32位！")]
        public string LastLogonIP
        {
            get
            {
                return _lastLogonIP;
            }
            set
            {
                if (_lastLogonIP == value)
                    return;
                _lastLogonIP = value;
            }
        }

        /// <summary>
        /// 照片后缀名
        /// </summary>
        [StringLengthValidator(0, 8, MessageTemplate = "照片后缀名长度不能超过8位！")]
        public string PhotoSuffixName
        {
            get
            {
                return _photoSuffixName;
            }
            set
            {
                if (_photoSuffixName == value)
                    return;
                _photoSuffixName = value;
            }
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool LockedOut
        {
            get
            {
                return _lockedOut;
            }
            set
            {
                if (_lockedOut == value)
                    return;
                _lockedOut = value;
            }
        }

        /// <summary>
        /// 字段修改权限
        /// </summary>
        public Int64 DataFieldAuthority
        {
            get
            {
                return _dataFieldAuthority;
            }
            set
            {
                if (_dataFieldAuthority == value)
                    return;
                _dataFieldAuthority = value;
            }
        }

        /// <summary>
        /// 单位管理权限
        /// </summary>
        public Int64 DepartmentAuthority
        {
            get
            {
                return _departmentAuthority;
            }
            set
            {
                if (_departmentAuthority == value)
                    return;
                _departmentAuthority = value;
            }
        }

        /// <summary>
        /// 用户唯一标示符
        /// </summary>
        public Guid UniqueUserIdentity
        {
            get
            {
                return _uniqueUserIdentity;
            }
            set
            {
                if (_uniqueUserIdentity == value)
                    return;
                _uniqueUserIdentity = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (_notes == value)
                    return;
                _notes = value;
            }
        }

        /// <summary>
        /// 重试次数
        /// </summary>
		public int RetryTimes
        {
            get
            {
                return _retryTimes;
            }
            set
            {
                if (_retryTimes == value)
                    return;
                _retryTimes = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return _createdTime;
            }
            set
            {
                _createdTime = value;
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedTime
        {
            get
            {
                return _updatedTime;
            }
            set
            {
                _updatedTime = value;
            }
        }

        #endregion
    }
}