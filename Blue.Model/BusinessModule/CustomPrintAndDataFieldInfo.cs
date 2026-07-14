//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrintAndDataFieldInfo.cs
// 描述: CustomPrintAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/2/2
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomPrintAndDataFieldInfo 类</para>
    /// <para>数据打印与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomPrintAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _printId;
        private decimal _dataFieldId;
        private byte _dataFieldPrintType;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomPrintAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="printId">数据打印编号</param>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="dataFieldPrintType">打印类型</param>
        ///<param name="sorting">排序</param>
        public CustomPrintAndDataFieldInfo(decimal printId, decimal dataFieldId, byte dataFieldPrintType, int sorting)
        {
            _printId = printId;
            _dataFieldId = dataFieldId;
            _dataFieldPrintType = dataFieldPrintType;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

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
        /// 打印类型
        /// </summary>
        public byte DataFieldPrintType
        {
            get
            {
                return _dataFieldPrintType;
            }
            set
            {
                if (_dataFieldPrintType == value)
                    return;
                _dataFieldPrintType = value;
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

        #endregion

    }
}