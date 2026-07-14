//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterfaceInfo.cs
// 描述: CustomInterfaceInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
    /// <summary>
    /// <para>CustomInterfaceInfo 类</para>
    /// <para>接口管理</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomInterfaceInfo
    {
        #region 内部成员变量

        private decimal _interfaceId;
        private decimal _userId;
        private decimal _combinedTableId;
        private decimal _tableId;
        private decimal _groupId;
        private string _interfaceName = string.Empty;
        private string _interfaceCode = string.Empty;
        private string _interfaceIdentifier = string.Empty;
        private byte _tableType;
        private bool _userTypeContained;
        private bool _depContained;
        private bool _actived;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomInterfaceInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="interfaceId">接口编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="combinedTableId">组合表编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="groupId">分组编号</param>
        ///<param name="interfaceName">接口名称</param>
        ///<param name="interfaceCode">接口编码</param>
        ///<param name="interfaceIdentifier">接口标识符</param>
        ///<param name="tableType">表格类型</param>
        ///<param name="userTypeContained">含子用户类型</param>
        ///<param name="depContained">含子单位类型</param>
        ///<param name="actived">启用</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomInterfaceInfo(decimal interfaceId, decimal userId, decimal combinedTableId, decimal tableId, decimal groupId,
            string interfaceName, string interfaceCode, string interfaceIdentifier, byte tableType, bool userTypeContained,
            bool depContained, bool actived, int sorting, string notes)
        {
            _interfaceId = interfaceId;
            _userId = userId;
            _combinedTableId = combinedTableId;
            _tableId = tableId;
            _groupId = groupId;
            _interfaceName = interfaceName;
            _interfaceCode = interfaceCode;
            _interfaceIdentifier = interfaceIdentifier;
            _tableType = tableType;
            _userTypeContained = userTypeContained;
            _depContained = depContained;
            _actived = actived;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 接口编号
        /// </summary>
        public decimal InterfaceId
        {
            get
            {
                return _interfaceId;
            }
            set
            {
                if (_interfaceId == value)
                    return;
                _interfaceId = value;
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
        /// 组合表编号
        /// </summary>
        public decimal CombinedTableId
        {
            get
            {
                return _combinedTableId;
            }
            set
            {
                if (_combinedTableId == value)
                    return;
                _combinedTableId = value;
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
        /// 接口名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 接口名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "接口名称长度范围在1位～64位！")]
        public string InterfaceName
        {
            get
            {
                return _interfaceName;
            }
            set
            {
                if (_interfaceName == value)
                    return;
                _interfaceName = value;
            }
        }

        /// <summary>
        /// 接口编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 接口编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "接口编码长度范围在1位～32位！")]
        public string InterfaceCode
        {
            get
            {
                return _interfaceCode;
            }
            set
            {
                if (_interfaceCode == value)
                    return;
                _interfaceCode = value;
            }
        }

        /// <summary>
        /// 接口标识符
        /// </summary>
        [NotNullValidator(MessageTemplate = " 接口标识符不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "接口标识符长度范围在1位～32位！")]
        public string InterfaceIdentifier
        {
            get
            {
                return _interfaceIdentifier;
            }
            set
            {
                if (_interfaceIdentifier == value)
                    return;
                _interfaceIdentifier = value;
            }
        }

        /// <summary>
        /// 表格类型
        /// </summary>
        public byte TableType
        {
            get
            {
                return _tableType;
            }
            set
            {
                if (_tableType == value)
                    return;
                _tableType = value;
            }
        }

        /// <summary>
        /// 含子用户类型
        /// </summary>
        public bool UserTypeContained
        {
            get
            {
                return _userTypeContained;
            }
            set
            {
                if (_userTypeContained == value)
                    return;
                _userTypeContained = value;
            }
        }

        /// <summary>
        /// 含子单位类型
        /// </summary>
        public bool DepContained
        {
            get
            {
                return _depContained;
            }
            set
            {
                if (_depContained == value)
                    return;
                _depContained = value;
            }
        }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Actived
        {
            get
            {
                return _actived;
            }
            set
            {
                if (_actived == value)
                    return;
                _actived = value;
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