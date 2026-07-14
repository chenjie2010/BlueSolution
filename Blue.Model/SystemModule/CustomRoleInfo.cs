//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomRoleInfo.cs
// 描述: CustomRoleInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/4
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>CustomRoleInfo 类</para>
    /// <para>自定义角色</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomRoleInfo
    {
        #region 内部成员变量

        private decimal _roleId;
        private decimal _groupId;
        private string _roleName = string.Empty;
        private string _roleCode = string.Empty;
        private DateTime _initializedDate;
        private DateTime _expiredDate;
        private long _roleProperty;
        private bool _isSystemRole;
        private long _menuAuthority;
        private long _menuSubAuthority;
        private long _systemAuthority;
        private long _systemSubAuthority;
        private bool _isLockedOut;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomRoleInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="roleName">角色名称</param>
        ///<param name="roleCode">角色编码</param>
        ///<param name="initializedDate">起始时间</param>
        ///<param name="expiredDate">结束时间</param>
        ///<param name="roleProperty">角色属性</param>
        ///<param name="isSystemRole">系统角色</param>
        ///<param name="menuAuthority">业务菜单权限</param>
        ///<param name="menuSubAuthority"></param>
        ///<param name="systemAuthority">系统权限</param>
        ///<param name="systemSubAuthority">SystemAuthority</param>
        ///<param name="isLockedOut">是否锁定</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomRoleInfo(decimal roleId, decimal groupId, string roleName, string roleCode, DateTime initializedDate,
            DateTime expiredDate, long roleProperty, bool isSystemRole, long menuAuthority, long menuSubAuthority,
            long systemAuthority, long systemSubAuthority, bool isLockedOut, int sorting, string notes)
        {
            _roleId = roleId;
            _groupId = groupId;
            _roleName = roleName;
            _roleCode = roleCode;
            _initializedDate = initializedDate;
            _expiredDate = expiredDate;
            _roleProperty = roleProperty;
            _isSystemRole = isSystemRole;
            _menuAuthority = menuAuthority;
            _menuSubAuthority = menuSubAuthority;
            _systemAuthority = systemAuthority;
            _systemSubAuthority = systemSubAuthority;
            _isLockedOut = isLockedOut;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 角色编号
        /// </summary>
        public decimal RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (_roleId == value)
                    return;
                _roleId = value;
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
        /// 角色名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 角色名称不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "角色名称长度范围在1位～32位！")]
        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                if (_roleName == value)
                    return;
                _roleName = value;
            }
        }

        /// <summary>
        /// 角色编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 角色编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "角色编码长度范围在1位～32位！")]
        public string RoleCode
        {
            get
            {
                return _roleCode;
            }
            set
            {
                if (_roleCode == value)
                    return;
                _roleCode = value;
            }
        }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime InitializedDate
        {
            get
            {
                return _initializedDate;
            }
            set
            {
                if (_initializedDate == value)
                    return;
                _initializedDate = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ExpiredDate
        {
            get
            {
                return _expiredDate;
            }
            set
            {
                if (_expiredDate == value)
                    return;
                _expiredDate = value;
            }
        }

        /// <summary>
        /// 角色属性
        /// </summary>
        public long RoleProperty
        {
            get
            {
                return _roleProperty;
            }
            set
            {
                if (_roleProperty == value)
                    return;
                _roleProperty = value;
            }
        }

        /// <summary>
        /// 系统角色
        /// </summary>
        public bool IsSystemRole
        {
            get
            {
                return _isSystemRole;
            }
            set
            {
                if (_isSystemRole == value)
                    return;
                _isSystemRole = value;
            }
        }

        /// <summary>
        /// 业务菜单权限
        /// </summary>
        public long MenuAuthority
        {
            get
            {
                return _menuAuthority;
            }
            set
            {
                if (_menuAuthority == value)
                    return;
                _menuAuthority = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long MenuSubAuthority
        {
            get
            {
                return _menuSubAuthority;
            }
            set
            {
                if (_menuSubAuthority == value)
                    return;
                _menuSubAuthority = value;
            }
        }

        /// <summary>
        /// 系统权限
        /// </summary>
        public long SystemAuthority
        {
            get
            {
                return _systemAuthority;
            }
            set
            {
                if (_systemAuthority == value)
                    return;
                _systemAuthority = value;
            }
        }

        /// <summary>
        /// SystemAuthority
        /// </summary>
        public long SystemSubAuthority
        {
            get
            {
                return _systemSubAuthority;
            }
            set
            {
                if (_systemSubAuthority == value)
                    return;
                _systemSubAuthority = value;
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

        #endregion

    }
}