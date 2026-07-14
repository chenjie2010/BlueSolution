//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleAndDataFieldInfo.cs
// 描述: RoleAndDataFieldInfo 实体类
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
    /// <para>RoleAndDataFieldInfo 类</para>
    /// <para>角色与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class RoleAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _dataFieldId;
        private decimal _roleId;
        private byte _dataAuthorityType;
        private byte _authorityType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="roleId">角色编号</param>
        ///<param name="dataAuthorityType">功能类型</param>
        ///<param name="authorityType">权限类型</param>
        public RoleAndDataFieldInfo(decimal dataFieldId, decimal roleId, byte dataAuthorityType, byte authorityType)
        {
            _dataFieldId = dataFieldId;
            _roleId = roleId;
            _dataAuthorityType = dataAuthorityType;
            _authorityType = authorityType;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get
            {
                return _dataFieldId;
            }
            set
            {
                if (_dataFieldId == value)
                    return;
                _dataFieldId = value;
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
        /// 权限类型
        /// </summary>
        public byte AuthorityType
        {
            get
            {
                return _authorityType;
            }
            set
            {
                if (_authorityType == value)
                    return;
                _authorityType = value;
            }
        }

        #endregion

    }
}