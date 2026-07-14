//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewAndDataFieldInfo.cs
// 描述：CustomViewAndDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/10/21
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomViewAndDataFieldInfo 类</para>
    /// <para>自定义视图与字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomViewAndDataFieldInfo
    {
        #region 内部成员变量

        private decimal _viewId;
        private decimal _dataFieldId;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewAndDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="viewId">视图编号</param>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        public CustomViewAndDataFieldInfo(decimal viewId, decimal dataFieldId, int sorting)
        {
            _viewId = viewId;
            _dataFieldId = dataFieldId;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 视图编号
        /// </summary>
        public decimal ViewId
        {
            get
            {
                return _viewId;
            }
            set
            {
                if (_viewId == value)
                    return;
                _viewId = value;
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