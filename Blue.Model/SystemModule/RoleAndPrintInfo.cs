//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RoleAndPrintInfo.cs
// 描述: RoleAndPrintInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/11/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>RoleAndPrintInfo 类</para>
    /// <para>角色与打印</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class RoleAndPrintInfo
    {
        #region 内部成员变量

        private decimal _roleId;
        private decimal _printId;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RoleAndPrintInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="roleId">角色编号</param>
        ///<param name="printId">数据打印编号</param>
        public RoleAndPrintInfo(decimal roleId, decimal printId)
        {
            _roleId = roleId;
            _printId = printId;

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
        /// 数据打印编号
        /// </summary>
        public decimal PrintId
        {
            get
            {
                return _printId;
            }
            set
            {
                if (_printId == value)
                    return;
                _printId = value;
            }
        }

        #endregion

    }
}