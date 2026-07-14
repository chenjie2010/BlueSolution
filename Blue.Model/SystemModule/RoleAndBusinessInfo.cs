//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleAndBusinessInfo.cs
// 描述: RoleAndBusinessInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/14
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>RoleAndBusinessInfo 类</para>
    /// <para>角色与业务</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class RoleAndBusinessInfo
    {
        #region 内部成员变量

        private decimal _roleId;
        private decimal _businessId;
        private bool _businessEnabled;
        private bool _thirdModeEnabled;
        private DateTime _initializedDate;
        private DateTime _expiredDate;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndBusinessInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="businessId">业务编号</param>
        ///<param name="businessEnabled">启用业务</param>
        ///<param name="thirdModeEnabled">启用第三方模式</param>
        ///<param name="initializedDate">起始时间</param>
        ///<param name="expiredDate">结束时间</param>
        ///<param name="notes">备注</param>
        public RoleAndBusinessInfo(decimal roleId, decimal businessId, bool businessEnabled, bool thirdModeEnabled, DateTime initializedDate,
            DateTime expiredDate, string notes)
        {
            _roleId = roleId;
            _businessId = businessId;
            _businessEnabled = businessEnabled;
            _thirdModeEnabled = thirdModeEnabled;
            _initializedDate = initializedDate;
            _expiredDate = expiredDate;
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
        /// 业务编号
        /// </summary>
        public decimal BusinessId
        {
            get
            {
                return _businessId;
            }
            set
            {
                if (_businessId == value)
                    return;
                _businessId = value;
            }
        }

        /// <summary>
        /// 启用业务
        /// </summary>
        public bool BusinessEnabled
        {
            get
            {
                return _businessEnabled;
            }
            set
            {
                if (_businessEnabled == value)
                    return;
                _businessEnabled = value;
            }
        }

        /// <summary>
        /// 启用第三方模式
        /// </summary>
        public bool ThirdModeEnabled
        {
            get
            {
                return _thirdModeEnabled;
            }
            set
            {
                if (_thirdModeEnabled == value)
                    return;
                _thirdModeEnabled = value;
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