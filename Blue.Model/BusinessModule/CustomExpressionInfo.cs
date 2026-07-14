//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomExpressionInfo.cs
// 描述：CustomExpressionInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/3/1
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomExpressionInfo 类</para>
    /// <para>表达式类型</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomExpressionInfo
    {
        #region 内部成员变量

        private decimal _parentDataFieldId;
        private int _sorting;
        private decimal _dataFieldId;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomExpressionInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        ///<param name="dataFieldId">字段编号</param>
        public CustomExpressionInfo(decimal parentDataFieldId, int sorting, decimal dataFieldId)
        {
            _parentDataFieldId = parentDataFieldId;
            _sorting = sorting;
            _dataFieldId = dataFieldId;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 字段编，逻辑字段编号
        /// </summary>
        public decimal ParentDataFieldId
        {
            get
            {
                return _parentDataFieldId;
            }
            set
            {
                if (_parentDataFieldId == value)
                    return;
                _parentDataFieldId = value;
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

        /// <summary>
        /// 字段编号，关联的物理字段的编号
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

        #endregion

    }
}