//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataInfo.cs
// 描述: RemoteDataInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.DataConvertionModule
{
    /// <summary>
    /// <para>RemoteDataInfo 类</para>
    /// <para>远程数据交换</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class RemoteDataInfo
    {
        #region 内部成员变量

        private decimal _remoteDataId;
        private decimal _databaseId;
        private decimal _groupId;
        private string _remoteDataName = string.Empty;
        private string _remoteDataCode = string.Empty;
        private long _remoteProperty;
        private string _remoteAddress = string.Empty;
        private string _remoteUserName = string.Empty;
        private string _remotePassword = string.Empty;
        private decimal _remoteDatabaseId;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RemoteDataInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="remoteDataId">远程数据交换编号</param>
        ///<param name="databaseId">数据库编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="remoteDataName">远程数据交换名称</param>
        ///<param name="remoteDataCode">远程数据交换编码</param>
        ///<param name="remoteProperty">远程数据交换属性</param>
        ///<param name="remoteAddress">远程数据交换地址</param>
        ///<param name="remoteUserName">远程数据交换用户名</param>
        ///<param name="remotePassword">远程数据交换密码</param>
        ///<param name="remoteDatabaseId">远程数据库编号</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public RemoteDataInfo(decimal remoteDataId, decimal databaseId, decimal groupId, string name, string remoteDataCode,
            long remoteProperty, string remoteAddress, string remoteUserName, string remotePassword, decimal remoteDatabaseId,
            int sorting, string notes)
        {
            _remoteDataId = remoteDataId;
            _databaseId = databaseId;
            _groupId = groupId;
            _remoteDataName = name;
            _remoteDataCode = remoteDataCode;
            _remoteProperty = remoteProperty;
            _remoteAddress = remoteAddress;
            _remoteUserName = remoteUserName;
            _remotePassword = remotePassword;
            _remoteDatabaseId = remoteDatabaseId; 
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 远程数据交换编号
        /// </summary>
        public decimal RemoteDataId
        {
            get
            {
                return _remoteDataId;
            }
            set
            {
                if (_remoteDataId == value)
                    return;
                _remoteDataId = value;
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
        /// 分组编号
        /// </summary>
        public decimal GroupId
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (_groupId == value)
                    return;
                _groupId = value;
            }
        }

        /// <summary>
        /// 远程数据交换名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 远程数据交换名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "远程数据交换名称长度范围在1位～64位！")]
        public string RemoteDataName
        {
            get
            {
                return _remoteDataName;
            }
            set
            {
                if (_remoteDataName == value)
                    return;
                _remoteDataName = value;
            }
        }

        /// <summary>
        /// 远程数据交换编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 远程数据交换编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "远程数据交换编码长度范围在1位～32位！")]
        public string RemoteDataCode
        {
            get
            {
                return _remoteDataCode;
            }
            set
            {
                if (_remoteDataCode == value)
                    return;
                _remoteDataCode = value;
            }
        }

        /// <summary>
        /// 远程数据交换属性
        /// </summary>
        public long RemoteProperty
        {
            get
            {
                return _remoteProperty;
            }
            set
            {
                if (_remoteProperty == value)
                    return;
                _remoteProperty = value;
            }
        }

        /// <summary>
        /// 远程数据交换地址
        /// </summary>
        [NotNullValidator(MessageTemplate = " 远程数据交换地址不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "远程数据交换地址长度范围在1位～64位！")]
        public string RemoteAddress
        {
            get
            {
                return _remoteAddress;
            }
            set
            {
                if (_remoteAddress == value)
                    return;
                _remoteAddress = value;
            }
        }

        /// <summary>
        /// 远程数据交换用户名
        /// </summary>
        [NotNullValidator(MessageTemplate = " 远程数据交换用户名不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "远程数据交换用户名长度范围在1位～64位！")]
        public string RemoteUserName
        {
            get
            {
                return _remoteUserName;
            }
            set
            {
                if (_remoteUserName == value)
                    return;
                _remoteUserName = value;
            }
        }

        /// <summary>
        /// 远程数据交换密码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 远程数据交换密码不能为空")]
        [StringLengthValidator(1, 128, MessageTemplate = "远程数据交换密码长度范围在1位～128位！")]
        public string RemotePassword
        {
            get
            {
                return _remotePassword;
            }
            set
            {
                if (_remotePassword == value)
                    return;
                _remotePassword = value;
            }
        }

        /// <summary>
        /// 远程数据库编号
        /// </summary>
        public decimal RemoteDatabaseId
        {
            get
            {
                return _remoteDatabaseId;
            }
            set
            {
                if (_remoteDatabaseId == value)
                    return;
                _remoteDatabaseId = value;
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