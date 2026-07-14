//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DepartmentScopeInfo.cs
// 描述：DepartmentScopeInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/1/16
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>DepartmentScopeInfo 类</para>
    /// <para>用户管理单位范围</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class DepartmentScopeInfo
    {
        #region 内部成员变量

        private decimal _depId;
        private decimal _userId;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DepartmentScopeInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="depId">部门编号</param>
        ///<param name="userId">用户编号</param>
        public DepartmentScopeInfo(decimal depId, decimal userId)
        {
            _depId = depId;
            _userId = userId;

        }

        #endregion

        #region 字段属性

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

        #endregion

    }
}