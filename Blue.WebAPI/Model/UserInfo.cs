//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAccountInfo.cs
// 描述：UserAccountInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/04/19
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;

namespace Blue.WebAPI
{
 
     /// <summary>
     /// 用户对象
     /// </summary>
             
    [Serializable]
    public class UserInfo
    {
        #region 内部成员变量

        private decimal _userId;
        private string _userName = string.Empty;
        private string _userActualName = string.Empty;
        private decimal _depId;
        private string _depName;
        private decimal _userTypeId;
        private string _userTypeName;       
        private string _emailAddress = string.Empty;
        private byte _identificationType;
        private string _userIdentity = string.Empty;
        private string _telephoneNumber = string.Empty;
        private bool _lockedOut;
        private Guid _uniqueUserIdentity = Guid.Empty;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="userId">用户编号</param>
         ///<param name="userName">用户名</param>
        ///<param name="userActualName">用户姓名</param>
        ///<param name="depId">部门编号</param>
        ///<param name="depName">部门名称</param>
        ///<param name="userTypeId">用户类型编号</param>
        ///<param name="userTypeName">用户类型名称</param>       
        ///<param name="emailAddress">电子邮件</param>
        ///<param name="identificationType">证件类型</param>
        ///<param name="userIdentity">身份证号</param>
        ///<param name="telephoneNumber">手机号码</param>
        ///<param name="lockedOut">是否锁定</param>
        ///<param name="uniqueUserIdentity">用户唯一标示符</param>
        ///<param name="createdTime">创建时间</param>
        ///<param name="updatedTime">更新时间</param>
        public UserInfo(decimal userId, string userName, string userActualName, decimal depId, string depName, decimal userTypeId, string userTypeName, 
            string emailAddress, byte identificationType, string userIdentity, string telephoneNumber, bool lockedOut, 
            Guid uniqueUserIdentity, DateTime createdTime, DateTime updatedTime)
        {
            _userId = userId;
            _userName = userName;
            _userActualName = userActualName;
            _depId = depId;
            _depName = depName;
            _userTypeId = userTypeId;
            _userTypeName = userTypeName;            
            _emailAddress = emailAddress;
            _identificationType = identificationType;
            _userIdentity = userIdentity;
            _telephoneNumber = telephoneNumber;
            _lockedOut = lockedOut;
            _uniqueUserIdentity = uniqueUserIdentity;
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
        /// 部门名称
        /// </summary>
        public string DepName
        {
            get
            {
                return _depName;
            }
            set
            {
                if (_depName == value)
                    return;
                _depName = value;
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
        /// 用户类型名称
        /// </summary>
        public string UserTypeName
        {
            get
            {
                return _userTypeName;
            }
            set
            {
                if (_userTypeName == value)
                    return;
                _userTypeName = value;
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