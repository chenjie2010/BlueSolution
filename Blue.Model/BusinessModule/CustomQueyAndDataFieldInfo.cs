//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomQueyAndDataFieldInfo.cs
// 描述：CustomQueyAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/11/8
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomQueyAndDataFieldInfo 类</para>
    /// <para>自定义查询与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomQueyAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _dataFieldId;
        private decimal _dataQueriedId;
        private byte _dataFieldMode;
        private string _dataFieldFormat = string.Empty;
        private bool _queryAllowed;
        private string _conditions = string.Empty;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomQueyAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataQueriedId">数据查询编号</param>
        ///<param name="dataFieldMode">字段模式</param>
        ///<param name="dataFieldFormat">字段格式</param>
        ///<param name="queryAllowed">查询字段</param>
        ///<param name="conditions">查询条件</param>
        ///<param name="sorting"></param>
        public CustomQueyAndDataFieldInfo(decimal dataFieldId, decimal dataQueriedId, byte dataFieldMode, string dataFieldFormat, bool queryAllowed,
            string conditions, int sorting)
        {
            _dataFieldId = dataFieldId;
            _dataQueriedId = dataQueriedId;
            _dataFieldMode = dataFieldMode;
            _dataFieldFormat = dataFieldFormat;
            _queryAllowed = queryAllowed;
            _conditions = conditions;
            _sorting = sorting;

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
        /// 数据查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get
            {
                return _dataQueriedId;
            }
            set
            {
                if (_dataQueriedId == value)
                    return;
                _dataQueriedId = value;
            }
        }

        /// <summary>
        /// 字段模式
        /// </summary>
        public byte DataFieldMode
        {
            get
            {
                return _dataFieldMode;
            }
            set
            {
                if (_dataFieldMode == value)
                    return;
                _dataFieldMode = value;
            }
        }

        /// <summary>
        /// 字段格式
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "字段格式长度不能超过512位！")]
        public string DataFieldFormat
        {
            get
            {
                return _dataFieldFormat;
            }
            set
            {
                if (_dataFieldFormat == value)
                    return;
                _dataFieldFormat = value;
            }
        }

        /// <summary>
        /// 查询字段
        /// </summary>
        public bool QueryAllowed
        {
            get
            {
                return _queryAllowed;
            }
            set
            {
                if (_queryAllowed == value)
                    return;
                _queryAllowed = value;
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "查询条件长度不能超过512位！")]
        public string Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                if (_conditions == value)
                    return;
                _conditions = value;
            }
        }

        /// <summary>
        /// 
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

        #endregion

    }
}