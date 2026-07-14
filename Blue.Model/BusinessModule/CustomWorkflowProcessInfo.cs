//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomWorkflowProcessInfo.cs
// 描述: CustomWorkflowProcessInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/23
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomWorkflowProcessInfo 类</para>
    /// <para>工作流流程</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomWorkflowProcessInfo
    {
        #region 内部成员变量

        private decimal _processId;
        private decimal _userId;
        private decimal _workflowId;
        private decimal _roleId;
        private decimal _viewId;
        private decimal _tableId;
        private string _processName = string.Empty;
        private string _processCode = string.Empty;
        private byte _dataInputMode;
        private byte _processCategory;
        private byte _processType;
        private byte _conditionType;
        private byte _handlerType;
        private byte _handlerMode;
        private long _processSetting;
        private bool _enableHelp;
        private string _helpContent = string.Empty;
        private int _leftDays;
        private bool _isRootNode;
        private string _toolTip = string.Empty;
        private int _sorting;
        private string _notes = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowProcessInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="userId">用户编号</param>
        ///<param name="workflowId">工作流编号</param>
        ///<param name="roleId">角色编号</param>
        ///<param name="viewId">视图编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="processName">流程名称</param>
        ///<param name="processCode">流程编码</param>
        ///<param name="dataInputMode">录入模式</param>
        ///<param name="processCategory">流程类别</param>
        ///<param name="processType">流向类型</param>
        ///<param name="conditionType">条件类型</param>
        ///<param name="handlerType">处理人类型</param>
        ///<param name="handlerMode">处理人模式</param>
        ///<param name="processSetting">配置信息</param>
        ///<param name="enableHelp">启用帮助</param>
        ///<param name="helpContent">帮助内容</param>
        ///<param name="leftDays">限定天数</param>
        ///<param name="isRootNode">是否根节点</param>
        ///<param name="toolTip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        public CustomWorkflowProcessInfo(decimal processId, decimal userId, decimal workflowId, decimal roleId, decimal viewId,
            decimal tableId, string processName, string processCode, byte dataInputMode, byte processCategory,
            byte processType, byte conditionType, byte handlerType, byte handlerMode, long processSetting,
            bool enableHelp, string helpContent, int leftDays, bool isRootNode, string toolTip,
            int sorting, string notes)
        {
            _processId = processId;
            _userId = userId;
            _workflowId = workflowId;
            _roleId = roleId;
            _viewId = viewId;
            _tableId = tableId;
            _processName = processName;
            _processCode = processCode;
            _dataInputMode = dataInputMode;
            _processCategory = processCategory;
            _processType = processType;
            _conditionType = conditionType;
            _handlerType = handlerType;
            _handlerMode = handlerMode;
            _processSetting = processSetting;
            _enableHelp = enableHelp;
            _helpContent = helpContent;
            _leftDays = leftDays;
            _isRootNode = isRootNode;
            _toolTip = toolTip;
            _sorting = sorting;
            _notes = notes;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 流程编号
        /// </summary>
        public decimal ProcessId
        {
            get
            {
                return _processId;
            }
            set
            {
                if (_processId == value)
                    return;
                _processId = value;
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
        /// 角色编号
        /// </summary>
        public decimal RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (_roleId == value)
                    return;
                _roleId = value;
            }
        }

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
        /// 流程名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 流程名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "流程名称长度范围在1位～64位！")]
        public string ProcessName
        {
            get
            {
                return _processName;
            }
            set
            {
                if (_processName == value)
                    return;
                _processName = value;
            }
        }

        /// <summary>
        /// 流程编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 流程编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "流程编码长度范围在1位～32位！")]
        public string ProcessCode
        {
            get
            {
                return _processCode;
            }
            set
            {
                if (_processCode == value)
                    return;
                _processCode = value;
            }
        }

        /// <summary>
        /// 录入模式
        /// </summary>
        public byte DataInputMode
        {
            get
            {
                return _dataInputMode;
            }
            set
            {
                if (_dataInputMode == value)
                    return;
                _dataInputMode = value;
            }
        }

        /// <summary>
        /// 流程类别
        /// </summary>
        public byte ProcessCategory
        {
            get
            {
                return _processCategory;
            }
            set
            {
                if (_processCategory == value)
                    return;
                _processCategory = value;
            }
        }

        /// <summary>
        /// 流向类型
        /// </summary>
        public byte ProcessType
        {
            get
            {
                return _processType;
            }
            set
            {
                if (_processType == value)
                    return;
                _processType = value;
            }
        }

        /// <summary>
        /// 条件类型
        /// </summary>
        public byte ConditionType
        {
            get
            {
                return _conditionType;
            }
            set
            {
                if (_conditionType == value)
                    return;
                _conditionType = value;
            }
        }

        /// <summary>
        /// 处理人类型
        /// </summary>
        public byte HandlerType
        {
            get
            {
                return _handlerType;
            }
            set
            {
                if (_handlerType == value)
                    return;
                _handlerType = value;
            }
        }

        /// <summary>
        /// 处理人模式
        /// </summary>
        public byte HandlerMode
        {
            get
            {
                return _handlerMode;
            }
            set
            {
                if (_handlerMode == value)
                    return;
                _handlerMode = value;
            }
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        public long ProcessSetting
        {
            get
            {
                return _processSetting;
            }
            set
            {
                if (_processSetting == value)
                    return;
                _processSetting = value;
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
        /// 限定天数
        /// </summary>
        public int LeftDays
        {
            get
            {
                return _leftDays;
            }
            set
            {
                if (_leftDays == value)
                    return;
                _leftDays = value;
            }
        }

        /// <summary>
        /// 是否根节点
        /// </summary>
        public bool IsRootNode
        {
            get
            {
                return _isRootNode;
            }
            set
            {
                if (_isRootNode == value)
                    return;
                _isRootNode = value;
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