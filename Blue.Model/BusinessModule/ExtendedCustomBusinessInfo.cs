//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ExtendedCustomBusinessInfo.cs
// 描述：ExtendedCustomBusinessInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/02/08
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomBusinessInfo 类</para>
    /// <para>自定义业务</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class ExtendedCustomBusinessInfo : CustomBusinessInfo
    {
        #region 内部成员变量
          
        private bool _businessEnabled;
        private bool _thirdModeEnabled;
        private DateTime _initializedDate;
        private DateTime _expiredDate;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ExtendedCustomBusinessInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="businessId">业务编号</param>
        ///<param name="menuId">菜单编号</param>
        ///<param name="workflowId">工作流编号</param>
        ///<param name="reportId">报表编号</param>
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
        ///<param name="businessEnabled">启用业务</param>
        ///<param name="thirdModeEnabled">启用第三方模式</param>
        ///<param name="initializedDate">开始时间</param>
        ///<param name="expiredDate">截止时间</param>
        public ExtendedCustomBusinessInfo(decimal businessId, decimal menuId, decimal workflowId, decimal reportId, decimal dataAuditingId, decimal dataId, decimal dataQueriedId, 
            string businessName, string businessCode, byte businessMenu, byte name, string businessIntro, bool enableHelp, string helpContent, byte iconType, 
            byte businessIcon, string iconName, string iconPath, string businessURL, int sorting, string notes, bool businessEnabled, bool thirdModeEnabled, 
            DateTime initializedDate, DateTime expiredDate) : base(businessId, menuId, workflowId, reportId, dataAuditingId, dataId, dataQueriedId, businessName, businessCode, 
                businessMenu, name, businessIntro, enableHelp, helpContent, iconType, businessIcon, iconName, iconPath, businessURL, sorting, notes)
        {
            _businessEnabled = businessEnabled;
            _thirdModeEnabled = thirdModeEnabled;
            _initializedDate = initializedDate;
            _expiredDate = expiredDate;
        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 业务是否启用
        /// </summary>
        public bool BusinessEnabled
        {
            get
            {
                return _businessEnabled;
            }
            set
            {
                if (_businessEnabled == value)
                    return;
                _businessEnabled = value;
            }
        }

        /// <summary>
        /// 启用第三方模式
        /// </summary>
        public bool ThirdModeEnabled
        {
            get
            {
                return _thirdModeEnabled;
            }
            set
            {
                if (_thirdModeEnabled == value)
                    return;
                _thirdModeEnabled = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime InitializedDate
        {
            get
            {
                return _initializedDate;
            }
            set
            {
                if (_initializedDate == value)
                    return;
                _initializedDate = value;
            }
        }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime ExpiredDate
        {
            get
            {
                return _expiredDate;
            }
            set
            {
                if (_expiredDate == value)
                    return;
                _expiredDate = value;
            }
        }

        #endregion

    }
}