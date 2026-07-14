//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserTypeInfo.cs
// 描述: UserTypeInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/4/19
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>UserTypeInfo 类</para>
    /// <para>用户类型</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class UserTypeInfo
    {
        #region 内部成员变量

        private decimal _userTypeId;
        private decimal _groupId;
        private string _userTypeName = string.Empty;
        private string _userTypeCode = string.Empty;
        private string _firstCode = string.Empty;
        private string _secondCode = string.Empty;
        private bool _isSystemUserType;
        private bool _isVisibleForInterface;
        private int _sorting;
        private string _notes = string.Empty;
        private DateTime _createdTime;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserTypeInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="userTypeId">用户类型编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="userTypeName">用户类型名称</param>
        ///<param name="userTypeCode">用户类型编码</param>
        ///<param name="firstCode">附加编码一</param>
        ///<param name="secondCode">附加编码二</param>
        ///<param name="isSystemUserType">是否系统用户类型</param>
        ///<param name="isVisibleForInterface">是否接口可见</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        ///<param name="createdTime">增加时间</param>
        ///<param name="updatedTime">修改时间</param>
        public UserTypeInfo(decimal userTypeId, decimal groupId, string name, string userType, string firstCode,
            string secondCode, bool isSystemUserType, bool isVisibleForInterface, int sorting, string notes, DateTime createdTime,
            DateTime updatedTime)
        {
            _userTypeId = userTypeId;
            _groupId = groupId;
            _userTypeName = name;
            _userTypeCode = userType;
            _firstCode = firstCode;
            _secondCode = secondCode;
            _isSystemUserType = isSystemUserType;
            _isVisibleForInterface = isVisibleForInterface;
            _sorting = sorting;
            _notes = notes;
            _createdTime = createdTime;
            _updatedTime = updatedTime;

        }

        #endregion

        #region 字段属性

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
        /// 分组编号
        /// </summary>
        public decimal GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (_groupId == value)
                    return;
                _groupId = value;
            }
        }

        /// <summary>
        /// 用户类型名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 用户类型名称不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "用户类型名称长度范围在1位～32位！")]
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
        [NotNullValidator(MessageTemplate = " 用户类型编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "用户类型编码长度范围在1位～32位！")]
        public string UserTypeCode
        {
            get
            {
                return _userTypeCode;
            }
            set
            {
                if (_userTypeCode == value)
                    return;
                _userTypeCode = value;
            }
        }

        /// <summary>
        /// 附加编码一
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "附加编码一长度不能超过32位！")]
        public string FirstCode
        {
            get
            {
                return _firstCode;
            }
            set
            {
                if (_firstCode == value)
                    return;
                _firstCode = value;
            }
        }

        /// <summary>
        /// 附加编码二
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "附加编码二长度不能超过32位！")]
        public string SecondCode
        {
            get
            {
                return _secondCode;
            }
            set
            {
                if (_secondCode == value)
                    return;
                _secondCode = value;
            }
        }

        /// <summary>
        /// 是否系统用户类型
        /// </summary>
        public bool IsSystemUserType
        {
            get
            {
                return _isSystemUserType;
            }
            set
            {
                if (_isSystemUserType == value)
                    return;
                _isSystemUserType = value;
            }
        }

        /// <summary>
        /// 是否接口可见
        /// </summary>
        public bool IsVisibleForInterface
        {
            get
            {
                return _isVisibleForInterface;
            }
            set
            {
                if (_isVisibleForInterface == value)
                    return;
                _isVisibleForInterface = value;
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
        /// 增加时间
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return _createdTime;
            }
            set
            {
                if (_createdTime == value)
                    return;
                _createdTime = value;
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime
        {
            get
            {
                return _updatedTime;
            }
            set
            {
                if (_updatedTime == value)
                    return;
                _updatedTime = value;
            }
        }

        #endregion

    }
}