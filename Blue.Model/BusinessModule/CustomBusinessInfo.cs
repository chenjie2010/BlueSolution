//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomBusinessInfo.cs
// 描述: CustomBusinessInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/22
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomBusinessInfo 类</para>
    /// <para>自定义业务</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomBusinessInfo
    {
        #region 内部成员变量

        private decimal _businessId;
        private decimal _menuId;
        private decimal _workflowId;
        private decimal _reportId;
        private decimal _dataAuditingId;
        private decimal _dataId;
        private decimal _dataQueriedId;
        private string _businessName = string.Empty;
        private string _businessCode = string.Empty;
        private byte _businessMenu;
        private byte _customBusinessName;
        private string _businessIntro = string.Empty;
        private bool _enableHelp;
        private string _helpContent = string.Empty;
        private byte _iconType;
        private byte _businessIcon;
        private string _iconName = string.Empty;
        private string _iconPath = string.Empty;
        private string _businessURL = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomBusinessInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="businessId">业务编号</param>
        ///<param name="menuId">菜单编号</param>
        ///<param name="workflowId">工作流编号</param>
        ///<param name="reportId">报表编号</param>
        ///<param name="dataAuditingId">数据审核编号</param>
        ///<param name="dataId">数据填报编号</param>
        ///<param name="dataQueriedId">数据查询编号</param>
        ///<param name="businessName">业务名称</param>
        ///<param name="businessCode">业务编码</param>
        ///<param name="businessMenu">业务类型</param>
        ///<param name="customBusinessName">自定义业务</param>
        ///<param name="businessIntro">业务介绍</param>
        ///<param name="enableHelp">启用帮助</param>
        ///<param name="helpContent">帮助内容</param>
        ///<param name="iconType">图标类型</param>
        ///<param name="businessIcon">业务图标</param>
        ///<param name="iconName">自定义图标名称</param>
        ///<param name="iconPath">自定义图标路径</param>
        ///<param name="businessURL">业务URL地址</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomBusinessInfo(decimal businessId, decimal menuId, decimal workflowId, decimal reportId, decimal dataAuditingId,
            decimal dataId, decimal dataQueriedId, string businessName, string businessCode, byte businessMenu,
            byte name, string businessIntro, bool enableHelp, string helpContent, byte iconType,
            byte businessIcon, string iconName, string iconPath, string businessURL, int sorting,
            string notes)
        {
            _businessId = businessId;
            _menuId = menuId;
            _workflowId = workflowId;
            _reportId = reportId;
            _dataAuditingId = dataAuditingId;
            _dataId = dataId;
            _dataQueriedId = dataQueriedId;
            _businessName = businessName;
            _businessCode = businessCode;
            _businessMenu = businessMenu;
            _customBusinessName = name;
            _businessIntro = businessIntro;
            _enableHelp = enableHelp;
            _helpContent = helpContent;
            _iconType = iconType;
            _businessIcon = businessIcon;
            _iconName = iconName;
            _iconPath = iconPath;
            _businessURL = businessURL;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 业务编号
        /// </summary>
        public decimal BusinessId
        {
            get
            {
                return _businessId;
            }
            set
            {
                if (_businessId == value)
                    return;
                _businessId = value;
            }
        }

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
        /// 工作流编号
        /// </summary>
        public decimal WorkflowId
        {
            get
            {
                return _workflowId;
            }
            set
            {
                if (_workflowId == value)
                    return;
                _workflowId = value;
            }
        }

        /// <summary>
        /// 报表编号
        /// </summary>
        public decimal ReportId
        {
            get
            {
                return _reportId;
            }
            set
            {
                if (_reportId == value)
                    return;
                _reportId = value;
            }
        }

        /// <summary>
        /// 数据审核编号
        /// </summary>
        public decimal DataAuditingId
        {
            get
            {
                return _dataAuditingId;
            }
            set
            {
                if (_dataAuditingId == value)
                    return;
                _dataAuditingId = value;
            }
        }

        /// <summary>
        /// 数据填报编号
        /// </summary>
        public decimal DataId
        {
            get
            {
                return _dataId;
            }
            set
            {
                if (_dataId == value)
                    return;
                _dataId = value;
            }
        }

        /// <summary>
        /// 数据查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get
            {
                return _dataQueriedId;
            }
            set
            {
                if (_dataQueriedId == value)
                    return;
                _dataQueriedId = value;
            }
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 业务名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "业务名称长度范围在1位～256位！")]
        public string BusinessName
        {
            get
            {
                return _businessName;
            }
            set
            {
                if (_businessName == value)
                    return;
                _businessName = value;
            }
        }

        /// <summary>
        /// 业务编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 业务编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "业务编码长度范围在1位～32位！")]
        public string BusinessCode
        {
            get
            {
                return _businessCode;
            }
            set
            {
                if (_businessCode == value)
                    return;
                _businessCode = value;
            }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public byte BusinessMenu
        {
            get
            {
                return _businessMenu;
            }
            set
            {
                if (_businessMenu == value)
                    return;
                _businessMenu = value;
            }
        }

        /// <summary>
        /// 自定义业务
        /// </summary>
        public byte CustomBusinessName
        {
            get
            {
                return _customBusinessName;
            }
            set
            {
                if (_customBusinessName == value)
                    return;
                _customBusinessName = value;
            }
        }

        /// <summary>
        /// 业务介绍
        /// </summary>
        [NotNullValidator(MessageTemplate = " 业务介绍不能为空")]
        [StringLengthValidator(1, 1024, MessageTemplate = "业务介绍长度范围在1位～1024位！")]
        public string BusinessIntro
        {
            get
            {
                return _businessIntro;
            }
            set
            {
                if (_businessIntro == value)
                    return;
                _businessIntro = value;
            }
        }

        /// <summary>
        /// 启用帮助
        /// </summary>
        public bool EnableHelp
        {
            get
            {
                return _enableHelp;
            }
            set
            {
                if (_enableHelp == value)
                    return;
                _enableHelp = value;
            }
        }

        /// <summary>
        /// 帮助内容
        /// </summary>
        [StringLengthValidator(0, 4000, MessageTemplate = "帮助内容长度不能超过4000位！")]
        public string HelpContent
        {
            get
            {
                return _helpContent;
            }
            set
            {
                if (_helpContent == value)
                    return;
                _helpContent = value;
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
        /// 业务图标
        /// </summary>
        public byte BusinessIcon
        {
            get
            {
                return _businessIcon;
            }
            set
            {
                if (_businessIcon == value)
                    return;
                _businessIcon = value;
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
        /// 业务URL地址
        /// </summary>
        [StringLengthValidator(0, 1024, MessageTemplate = "业务URL地址长度不能超过1024位！")]
        public string BusinessURL
        {
            get
            {
                return _businessURL;
            }
            set
            {
                if (_businessURL == value)
                    return;
                _businessURL = value;
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