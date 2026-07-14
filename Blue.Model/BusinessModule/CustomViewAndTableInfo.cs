//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewAndTableInfo.cs
// 描述：CustomViewAndTableInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/3/5
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomViewAndTableInfo 类</para>
    /// <para>自定义视图与表</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomViewAndTableInfo
    {
        #region 内部成员变量

        private decimal _viewId;
        private decimal _tableId;
        private byte _tableRelation;
        private byte _tableJoin;
        private byte _primaryDataField;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewAndTableInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="viewId">视图编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="tableRelation">关系表名</param>
        ///<param name="tableJoin">链接方式</param>
        ///<param name="primaryDataField">关系字段</param>
        ///<param name="sorting">排序</param>
        public CustomViewAndTableInfo(decimal viewId, decimal tableId, byte tableRelation, byte tableJoin, byte primaryDataField,
            int sorting)
        {
            _viewId = viewId;
            _tableId = tableId;
            _tableRelation = tableRelation;
            _tableJoin = tableJoin;
            _primaryDataField = primaryDataField;
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
        /// 表编号
        /// </summary>
        public decimal TableId
        {
            get
            {
                return _tableId;
            }
            set
            {
                if (_tableId == value)
                    return;
                _tableId = value;
            }
        }

        /// <summary>
        /// 关系表名
        /// </summary>
        public byte TableRelation
        {
            get
            {
                return _tableRelation;
            }
            set
            {
                if (_tableRelation == value)
                    return;
                _tableRelation = value;
            }
        }

        /// <summary>
        /// 链接方式
        /// </summary>
        public byte TableJoin
        {
            get
            {
                return _tableJoin;
            }
            set
            {
                if (_tableJoin == value)
                    return;
                _tableJoin = value;
            }
        }

        /// <summary>
        /// 关系字段
        /// </summary>
        public byte PrimaryDataField
        {
            get
            {
                return _primaryDataField;
            }
            set
            {
                if (_primaryDataField == value)
                    return;
                _primaryDataField = value;
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