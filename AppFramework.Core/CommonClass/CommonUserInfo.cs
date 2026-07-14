//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonUserInfo.cs
// 描述： 通用用户信息
// 作者：ChenJie 
// 编写日期：2018/02/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用用户信息
    /// </summary>
    [Serializable]
    public class CommonUserInfo
    {
        #region 内部成员变量

        private decimal _userId;
        private string _userName;
        private string _userActualName;
        private decimal _userTypeId;
        private string _userTypeName;
        private string _userTypeCode;
        private decimal _depId;
        private string _depName;
        private string _depCode;
        private string _depValue;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CommonUserInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userActualName"></param>
        /// <param name="userTypeName"></param>
        /// <param name="userDepartmentName"></param>
        public CommonUserInfo(decimal userId, string userName, string userActualName)
        {
            _userId = userId;
            _userName = userName;
            _userActualName = userActualName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userActualName"></param>
        /// <param name="userTypeId"></param>
        /// <param name="userTypeName"></param>
        /// <param name="userTypeCode"></param>
        /// <param name="depId"></param>
        /// <param name="depName"></param>
        /// <param name="depCode"></param>
        /// <param name="depValue"></param>
        public CommonUserInfo(decimal userId, string userName, string userActualName, decimal userTypeId, string userTypeName, 
            string userTypeCode, decimal depId, string depName, string depCode, string depValue)
        {
            _userId = userId;
            _userName = userName;
            _userActualName = userActualName;
            _userTypeId = userTypeId;
            _userTypeName = userTypeName;
            _userTypeCode = userTypeCode;
            _depId = depId;
            _depName = depName;
            _depCode = depCode;
            _depValue = depValue;
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
        /// 用户真实姓名
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
        /// 用户类型编码
        /// </summary>
        public string UserTypeCode
        {
            get { return _userTypeCode; }
            set
            {
                if (_userTypeCode == value)
                    return;
                _userTypeCode = value;
            }
        }

        /// <summary>
        /// 用户单位编号
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
        /// 用户单位名称
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
        /// 用户单位编码
        /// </summary>
        public string DepCode
        {
            get
            {
                return _depCode;
            }
            set
            {
                if (_depCode == value)
                    return;
                _depCode = value;
            }
        }

        /// <summary>
        /// 用户单位值
        /// </summary>
        public string DepValue
        {
            get
            {
                return _depValue;
            }
            set
            {
                if (_depValue == value)
                    return;
                _depValue = value;
            }
        }

        #endregion
    }
}
