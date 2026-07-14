//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomFormAndDataFieldInfo.cs
// 描述：CustomFormAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/11/30
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomFormAndDataFieldInfo 类</para>
    /// <para>数据填报业务与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomFormAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _formId;
        private decimal _dataFieldId;
        private byte _dataFieldAuthority;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomFormAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="formId">业务编号</param>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataFieldAuthority">字段权限</param>
        public CustomFormAndDataFieldInfo(decimal formId, decimal dataFieldId, byte dataFieldAuthority)
        {
            _formId = formId;
            _dataFieldId = dataFieldId;
            _dataFieldAuthority = dataFieldAuthority;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 业务编号
        /// </summary>
        public decimal FormId
        {
            get
            {
                return _formId;
            }
            set
            {
                if (_formId == value)
                    return;
                _formId = value;
            }
        }

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
        /// 字段权限
        /// </summary>
        public byte DataFieldAuthority
        {
            get
            {
                return _dataFieldAuthority;
            }
            set
            {
                if (_dataFieldAuthority == value)
                    return;
                _dataFieldAuthority = value;
            }
        }

        #endregion

    }
}