//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomWorkflowMapInfo.cs
// 描述: CustomWorkflowMapInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/23
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomWorkflowMapInfo 类</para>
    /// <para>流程关系</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class CustomWorkflowMapInfo
    {
        #region 内部成员变量

        private decimal _parentProcessId;
        private decimal _processId;
        private int _sorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowMapInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        ///<param name="sorting">排序</param>
        public CustomWorkflowMapInfo(decimal parentProcessId, decimal processId, int sorting)
        {
            _parentProcessId = parentProcessId;
            _processId = processId;
            _sorting = sorting;

        }

        #endregion

        #region 字段属性

        /// <summary>
        /// 流程编号
        /// </summary>
        public decimal ParentProcessId
        {
            get
            {
                return _parentProcessId;
            }
            set
            {
                if (_parentProcessId == value)
                    return;
                _parentProcessId = value;
            }
        }

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