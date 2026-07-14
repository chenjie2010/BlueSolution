//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PrintRecordInfo.cs
// 描述: PrintRecordInfo 实体类
// 作者：ChenJie 
// 编写日期：2022/11/13
// Copyright 2022
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>PrintRecordInfo 类</para>
    /// <para>打印记录</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class PrintRecordInfo
    {
        #region 内部成员变量

        private decimal _printRecordId;
        private decimal _printId;
        private decimal _userId;
        private DateTime _printTime;
        private byte _printType;
        private int _printAddition;
        private string _commments = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrintRecordInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="printRecordId">用户打印编号</param>
        ///<param name="printId">数据打印编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="printTime">打印日期</param>
        ///<param name="printType">打印类型</param>
        ///<param name="printAddition">打印附加字段</param>
        ///<param name="commments">打印备注</param>
        public PrintRecordInfo(decimal printRecordId, decimal printId, decimal userId, DateTime printTime, byte printType,
            int printAddition, string commments)
        {
            _printRecordId = printRecordId;
            _printId = printId;
            _userId = userId;
            _printTime = printTime;
            _printType = printType;
            _printAddition = printAddition;
            _commments = commments;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 用户打印编号
        /// </summary>
        public decimal PrintRecordId
        {
            get
            {
                return _printRecordId;
            }
            set
            {
                if (_printRecordId == value)
                    return;
                _printRecordId = value;
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

        /// <summary>
        /// 打印日期
        /// </summary>
        public DateTime PrintTime
        {
            get
            {
                return _printTime;
            }
            set
            {
                if (_printTime == value)
                    return;
                _printTime = value;
            }
        }

        /// <summary>
        /// 打印类型
        /// </summary>
        public byte PrintType
        {
            get
            {
                return _printType;
            }
            set
            {
                if (_printType == value)
                    return;
                _printType = value;
            }
        }

        /// <summary>
        /// 打印附加字段
        /// </summary>
        public int PrintAddition
        {
            get
            {
                return _printAddition;
            }
            set
            {
                if (_printAddition == value)
                    return;
                _printAddition = value;
            }
        }

        /// <summary>
        /// 打印备注
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "打印备注长度不能超过256位！")]
        public string Commments
        {
            get
            {
                return _commments;
            }
            set
            {
                if (_commments == value)
                    return;
                _commments = value;
            }
        }

        #endregion

    }
}