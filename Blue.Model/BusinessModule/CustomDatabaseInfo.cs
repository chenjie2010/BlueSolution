//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDatabaseInfo.cs
// 描述：CustomDatabaseInfo 实体类
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
    /// <para>CustomDatabaseInfo 类</para>
    /// <para>自定义逻辑数据库</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomDatabaseInfo
    {
        #region 内部成员变量

        private decimal _databaseId;
        private string _databaseName = string.Empty;
        private string _databaseCode = string.Empty;
        private byte _dataWarehouseId;
        private bool _isLeaf;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDatabaseInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="databaseId">数据库编号</param>
        ///<param name="databaseName">数据库名称</param>
        ///<param name="databaseCode">数据库编码</param>
        ///<param name="dataWarehouseId">数据仓库编号</param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomDatabaseInfo(decimal databaseId, string databaseName, string databaseCode, byte dataWarehouseId, bool isLeaf,
            int sorting, string notes)
        {
            _databaseId = databaseId;
            _databaseName = databaseName;
            _databaseCode = databaseCode;
            _dataWarehouseId = dataWarehouseId;
            _isLeaf = isLeaf;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

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
        /// 数据库名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据库名称不能为空")]
        [StringLengthValidator(1, 16, MessageTemplate = "数据库名称长度范围在1位～16位！")]
        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }
            set
            {
                if (_databaseName == value)
                    return;
                _databaseName = value;
            }
        }

        /// <summary>
        /// 数据库编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 数据库编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "数据库编码长度范围在1位～32位！")]
        public string DatabaseCode
        {
            get
            {
                return _databaseCode;
            }
            set
            {
                if (_databaseCode == value)
                    return;
                _databaseCode = value;
            }
        }

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public byte DataWarehouseId
        {
            get
            {
                return _dataWarehouseId;
            }
            set
            {
                if (_dataWarehouseId == value)
                    return;
                _dataWarehouseId = value;
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