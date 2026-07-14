//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomMenuInfo.cs
// 描述: CustomMenuInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/11/3
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomMenuInfo 类</para>
    /// <para>自定义菜单</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomMenuInfo
    {
        #region 内部成员变量

        private decimal _menuId;
        private decimal _parentMenuId;
        private string _menuName = string.Empty;
        private string _menuCode = string.Empty;
        private byte _iconType;
        private byte _menuIcon;
        private string _iconName = string.Empty;
        private string _iconPath = string.Empty;
        private string _menuURL = string.Empty;
        private byte _menuType;
        private string _menuIconName = string.Empty;
        private bool _isLeaf;
        private string _toolTip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomMenuInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="menuId">菜单编号</param>
        ///<param name="parentMenuId">菜单编号</param>
        ///<param name="menuName">菜单名称</param>
        ///<param name="menuCode">菜单编码</param>
        ///<param name="iconType">图标类型</param>
        ///<param name="menuIcon">菜单图标</param>
        ///<param name="iconName">自定义图标名称</param>
        ///<param name="iconPath">自定义图标路径</param>
        ///<param name="menuURL">菜单URL地址</param>
        ///<param name="menuType">菜单类型</param>
        ///<param name="menuIconName"></param>
        ///<param name="isLeaf">叶子节点</param>
        ///<param name="toolTip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomMenuInfo(decimal menuId, decimal parentMenuId, string menuName, string menuCode, byte iconType,
            byte menuIcon, string iconName, string iconPath, string menuURL, byte menuType,
            string menuIconName, bool isLeaf, string toolTip, int sorting, string notes)
        {
            _menuId = menuId;
            _parentMenuId = parentMenuId;
            _menuName = menuName;
            _menuCode = menuCode;
            _iconType = iconType;
            _menuIcon = menuIcon;
            _iconName = iconName;
            _iconPath = iconPath;
            _menuURL = menuURL;
            _menuType = menuType;
            _menuIconName = menuIconName;
            _isLeaf = isLeaf;
            _toolTip = toolTip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 菜单编号
        /// </summary>
        public decimal MenuId
        {
            get
            {
                return _menuId;
            }
            set
            {
                if (_menuId == value)
                    return;
                _menuId = value;
            }
        }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public decimal ParentMenuId
        {
            get
            {
                return _parentMenuId;
            }
            set
            {
                if (_parentMenuId == value)
                    return;
                _parentMenuId = value;
            }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 菜单名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "菜单名称长度范围在1位～64位！")]
        public string MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                if (_menuName == value)
                    return;
                _menuName = value;
            }
        }

        /// <summary>
        /// 菜单编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 菜单编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "菜单编码长度范围在1位～32位！")]
        public string MenuCode
        {
            get
            {
                return _menuCode;
            }
            set
            {
                if (_menuCode == value)
                    return;
                _menuCode = value;
            }
        }

        /// <summary>
        /// 图标类型
        /// </summary>
        public byte IconType
        {
            get
            {
                return _iconType;
            }
            set
            {
                if (_iconType == value)
                    return;
                _iconType = value;
            }
        }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public byte MenuIcon
        {
            get
            {
                return _menuIcon;
            }
            set
            {
                if (_menuIcon == value)
                    return;
                _menuIcon = value;
            }
        }

        /// <summary>
        /// 自定义图标名称
        /// </summary>
        [StringLengthValidator(0, 32, MessageTemplate = "自定义图标名称长度不能超过32位！")]
        public string IconName
        {
            get
            {
                return _iconName;
            }
            set
            {
                if (_iconName == value)
                    return;
                _iconName = value;
            }
        }

        /// <summary>
        /// 自定义图标路径
        /// </summary>
        [StringLengthValidator(0, 128, MessageTemplate = "自定义图标路径长度不能超过128位！")]
        public string IconPath
        {
            get
            {
                return _iconPath;
            }
            set
            {
                if (_iconPath == value)
                    return;
                _iconPath = value;
            }
        }

        /// <summary>
        /// 菜单URL地址
        /// </summary>
        [StringLengthValidator(0, 1024, MessageTemplate = "菜单URL地址长度不能超过1024位！")]
        public string MenuURL
        {
            get
            {
                return _menuURL;
            }
            set
            {
                if (_menuURL == value)
                    return;
                _menuURL = value;
            }
        }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public byte MenuType
        {
            get
            {
                return _menuType;
            }
            set
            {
                if (_menuType == value)
                    return;
                _menuType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [StringLengthValidator(0, 64, MessageTemplate = "长度不能超过64位！")]
        public string MenuIconName
        {
            get
            {
                return _menuIconName;
            }
            set
            {
                if (_menuIconName == value)
                    return;
                _menuIconName = value;
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
        /// 提示信息
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "提示信息长度不能超过256位！")]
        public string ToolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if (_toolTip == value)
                    return;
                _toolTip = value;
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