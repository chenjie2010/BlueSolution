//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAccountInfo.cs
// 描述: UserAccountInfo 实体类
// 作者：ChenJie 
// 编写日期：2011/3/14
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;

namespace Blue.WebAPI
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
        private decimal _userTypeId;
        private decimal _depID;
        private string _userName = string.Empty;
        private string _userPwd = string.Empty;
        private string _userActualName = string.Empty;
        private string _emailAddress = string.Empty;
        private string _userIdentity = string.Empty;
        private DateTime _lastLogonTime;
        private string _lastLogonIP = string.Empty;
        private string _photoSuffix = string.Empty;
        private bool _isLockedOut;
        private int _dataFieldPower;
        private string _creater = string.Empty;
        private DateTime _creationTime;
        private string _modifier = string.Empty;
        private DateTime _modificationTime;
        private string _notes = string.Empty;

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
        ///<param name="userTypeId">用户类型编号</param>
        ///<param name="depID">部门编号</param>
        ///<param name="userName">用户名</param>
        ///<param name="userPwd">用户密码</param>
        ///<param name="userActualName">用户姓名</param>
        ///<param name="emailAddress"></param>
        ///<param name="userIdentity">身份证号</param>
        ///<param name="lastLogonTime">上次登录时间</param>
        ///<param name="lastLogonIP">上次登录地址</param>
        ///<param name="photoSuffix">照片后缀名</param>
        ///<param name="isLockedOut">是否锁定</param>
        ///<param name="dataFieldPower">修改权限字段修改权限</param>
        ///<param name="creater">创建人</param>
        ///<param name="creationTime">创建时间</param>
        ///<param name="modifier">修改人</param>
        ///<param name="modificationTime">修改时间</param>
        ///<param name="notes">注释</param>
        public UserAccountInfo(decimal userId, decimal userTypeId, decimal depID, string userName, string userPwd,
            string userActualName, string emailAddress, string userIdentity, DateTime lastLogonTime, string lastLogonIP,
            string photoSuffix, bool isLockedOut, int dataFieldPower, string creater, DateTime creationTime,
            string modifier, DateTime modificationTime, string notes)
        {
            _userId = userId;
            _userTypeId = userTypeId;
            _depID = depID;
            _userName = userName;
            _userPwd = userPwd;
            _userActualName = userActualName;
            _emailAddress = emailAddress;
            _userIdentity = userIdentity;
            _lastLogonTime = lastLogonTime;
            _lastLogonIP = lastLogonIP;
            _photoSuffix = photoSuffix;
            _isLockedOut = isLockedOut;
            _dataFieldPower = dataFieldPower;
            _creater = creater;
            _creationTime = creationTime;
            _modifier = modifier;
            _modificationTime = modificationTime;
            _notes = notes;
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
        /// 部门编号
        /// </summary>
        public decimal DepID
        {
            get
            {
                return _depID;
            }
            set
            {
                if (_depID == value)
                    return;
                _depID = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
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
        /// 身份证号
        /// </summary>
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
        public string PhotoSuffix
        {
            get
            {
                return _photoSuffix;
            }
            set
            {
                if (_photoSuffix == value)
                    return;
                _photoSuffix = value;
            }
        }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLockedOut
        {
            get
            {
                return _isLockedOut;
            }
            set
            {
                if (_isLockedOut == value)
                    return;
                _isLockedOut = value;
            }
        }

        /// <summary>
        /// 修改权限字段修改权限
        /// </summary>
        public int DataFieldPower
        {
            get
            {
                return _dataFieldPower;
            }
            set
            {
                if (_dataFieldPower == value)
                    return;
                _dataFieldPower = value;
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater
        {
            get
            {
                return _creater;
            }
            set
            {
                if (_creater == value)
                    return;
                _creater = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return _creationTime;
            }
            set
            {
                if (_creationTime == value)
                    return;
                _creationTime = value;
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        public string Modifier
        {
            get
            {
                return _modifier;
            }
            set
            {
                if (_modifier == value)
                    return;
                _modifier = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModificationTime
        {
            get
            {
                return _modificationTime;
            }
            set
            {
                if (_modificationTime == value)
                    return;
                _modificationTime = value;
            }
        }

        /// <summary>
        /// 注释
        /// </summary>
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

        #endregion

    }
}