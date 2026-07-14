//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomCategoryInfo.cs
// 描述：CustomCategoryInfo 实体类
// 作者：ChenJie 
// 编写日期：2016/9/17
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomCategoryInfo 类</para>
    /// <para>自定义分类</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomCategoryInfo
    {
        #region 内部成员变量

        private decimal _categoryId;
        private decimal _databaseId;
        private string _categoryName = string.Empty;
        private string _categoryCode = string.Empty;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomCategoryInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="categoryId">分类编号</param>
        ///<param name="databaseId">数据库编号</param>
        ///<param name="categoryName">分类名称</param>
        ///<param name="categoryCode">分类编码</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomCategoryInfo(decimal categoryId, decimal databaseId, string categoryName, string categoryCode, bool isLeaf,
            int sorting, string notes)
        {
            _categoryId = categoryId;
            _databaseId = databaseId;
            _categoryName = categoryName;
            _categoryCode = categoryCode;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 分类编号
        /// </summary>
        public decimal CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId == value)
                    return;
                _categoryId = value;
            }
        }

        /// <summary>
        /// 数据库编号
        /// </summary>
        public decimal DatabaseId
        {
            get
            {
                return _databaseId;
            }
            set
            {
                if (_databaseId == value)
                    return;
                _databaseId = value;
            }
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 分类名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "分类名称长度范围在1位～64位！")]
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                if (_categoryName == value)
                    return;
                _categoryName = value;
            }
        }

        /// <summary>
        /// 分类编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 分类编码不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "分类编码长度范围在1位～64位！")]
        public string CategoryCode
        {
            get
            {
                return _categoryCode;
            }
            set
            {
                if (_categoryCode == value)
                    return;
                _categoryCode = value;
            }
        }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            set
            {
                if (_isLeaf == value)
                    return;
                _isLeaf = value;
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
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                if (_notes == value)
                    return;
                _notes = value;
            }
        }

        #endregion

    }
}