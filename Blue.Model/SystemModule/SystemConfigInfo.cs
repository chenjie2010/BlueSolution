//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：SystemConfigInfo.cs
// 描述：SystemConfigInfo 实体类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>SystemConfigInfo 类</para>
    /// <para>系统配置信息</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class SystemConfigInfo
    {
        #region 内部成员变量

        private int _systemConfigName;
        private string _systemConfigValue = string.Empty;
        private SystemConfigCategory _systemConfigCategory;
        private DateTime _updatedTime;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public SystemConfigInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="systemConfigName">系统配置名称</param>
        ///<param name="systemConfigValue">系统配置值</param>
        ///<param name="systemConfigCategory">所属分类</param>
        ///<param name="updatedTime">更新时间</param>
        public SystemConfigInfo(int name, string systemConfigValue, SystemConfigCategory systemConfigCategory, DateTime updatedTime)
        {
            _systemConfigName = name;
            _systemConfigValue = systemConfigValue;
            _systemConfigCategory = systemConfigCategory;
            _updatedTime = updatedTime;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 系统配置名称
        /// </summary>
        public int SystemConfigName
        {
            get
            {
                return _systemConfigName;
            }
            set
            {
                if (_systemConfigName == value)
                    return;
                _systemConfigName = value;
            }
        }

        /// <summary>
        /// 系统配置值
        /// </summary>
        [NotNullValidator(MessageTemplate = " 系统配置值不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "系统配置值长度范围在1位～256位！")]
        public string SystemConfigValue
        {
            get
            {
                return _systemConfigValue;
            }
            set
            {
                if (_systemConfigValue == value)
                    return;
                _systemConfigValue = value;
            }
        }

        /// <summary>
        /// 所属分类
        /// </summary>
        public SystemConfigCategory SystemConfigCategory
        {
            get
            {
                return _systemConfigCategory;
            }
            set
            {
                if (_systemConfigCategory == value)
                    return;
                _systemConfigCategory = value;
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
                if (_updatedTime == value)
                    return;
                _updatedTime = value;
            }
        }

        #endregion

    }
}