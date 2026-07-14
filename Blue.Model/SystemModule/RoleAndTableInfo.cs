//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleAndTableInfo.cs
// 描述: RoleAndTableInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/9/4
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>RoleAndTableInfo 类</para>
    /// <para>角色与表</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class RoleAndTableInfo
    {
        #region 内部成员变量

        private decimal _tableId;
        private decimal _roleId;
        private byte _dataAuthorityType;
        private long _tableAuthority;
        private long _systemDataFieldAuthority;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndTableInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="tableId">表编号</param>
        ///<param name="roleId">角色编号</param>
        ///<param name="dataAuthorityType">功能类型</param>
        ///<param name="tableAuthority">表权限类型</param>
        ///<param name="systemDataFieldAuthority">系统字段权限</param>
        public RoleAndTableInfo(decimal tableId, decimal roleId, byte dataAuthorityType, long tableAuthority, long systemDataFieldAuthority)
        {
            _tableId = tableId;
            _roleId = roleId;
            _dataAuthorityType = dataAuthorityType;
            _tableAuthority = tableAuthority;
            _systemDataFieldAuthority = systemDataFieldAuthority;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 表编号
        /// </summary>
        public decimal TableId
        {
            get
            {
                return _tableId;
            }
            set
            {
                if (_tableId == value)
                    return;
                _tableId = value;
            }
        }

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
        /// 功能类型
        /// </summary>
        public byte DataAuthorityType
        {
            get
            {
                return _dataAuthorityType;
            }
            set
            {
                if (_dataAuthorityType == value)
                    return;
                _dataAuthorityType = value;
            }
        }

        /// <summary>
        /// 表权限类型
        /// </summary>
        public long TableAuthority
        {
            get
            {
                return _tableAuthority;
            }
            set
            {
                if (_tableAuthority == value)
                    return;
                _tableAuthority = value;
            }
        }

        /// <summary>
        /// 系统字段权限
        /// </summary>
        public long SystemDataFieldAuthority
        {
            get
            {
                return _systemDataFieldAuthority;
            }
            set
            {
                if (_systemDataFieldAuthority == value)
                    return;
                _systemDataFieldAuthority = value;
            }
        }

        #endregion

    }
}