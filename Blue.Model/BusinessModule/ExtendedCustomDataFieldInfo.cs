//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataFieldInfo.cs
// 描述：CustomDataFieldInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/1/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
    /// <summary>
    /// <para>CustomDataFieldInfo 类</para>
    /// <para>自定义字段</para>
    /// <para><see cref="member"/></para>
    /// <remarks></remarks>
    /// </summary>
    [Serializable]
    public class ExtendedCustomDataFieldInfo : CustomDataFieldInfo
    {
        #region 内部成员变量

        private string _name;
        private byte _authorityType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ExtendedCustomDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        ///<param name="tableId">表编号</param>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="enumId">枚举编号</param>
        ///<param name="logicalName">字段逻辑名称</param>
        ///<param name="physicalName">字段物理名称</param>
        ///<param name="dataFieldCode">字段编码</param>
        ///<param name="dataFieldProperty">字段属性</param>
        ///<param name="dataFieldType">字段类型</param>
        ///<param name="dataFieldLength">字段长度</param>
        ///<param name="basedDataType">基础类型</param>
        ///<param name="regexExpression">正则表达式</param>
        ///<param name="expressionText">表达式文本</param>
        ///<param name="dataFieldSetting">字段设置</param>
        ///<param name="requiredDataField">是否必填</param>
        ///<param name="autoComplete">自动完成</param>
        ///<param name="indexCreated">创建索引</param>
        ///<param name="helpEnabled">启用帮助</param>
        ///<param name="helpContent">帮助内容</param>
        ///<param name="tooltip">提示信息</param>
        ///<param name="sorting">排序</param>
        ///<param name="notes">备注</param>
        ///<param name="name">物理名称</param>
        ///<param name="authorityType">权限类型</param>
        public ExtendedCustomDataFieldInfo(decimal dataFieldId, decimal enumId, decimal parentDataFieldId, decimal associatedDataFieldId, decimal tableId,
            string logicalName, string physicalName, string dataFieldCode, byte dataFieldProperty, byte dataFieldType,
            int dataFieldLength, byte basedDataType, string regexExpression, string expressionText, long dataFieldSetting, bool requiredDataField,
            bool autoComplete, bool indexCreated, bool helpEnabled, string helpContent, string tooltip,
            int sorting, string notes, string name, byte authorityType) : base(dataFieldId, enumId, parentDataFieldId, associatedDataFieldId, tableId, 
                logicalName, physicalName, dataFieldCode, dataFieldProperty, dataFieldType, dataFieldLength, basedDataType, regexExpression, expressionText, dataFieldSetting, 
                requiredDataField, autoComplete, indexCreated, helpEnabled, helpContent, tooltip, sorting, notes)
        {
            _name = name;
            _authorityType = authorityType;
        }



        #endregion

        #region 字段属性

        /// <summary>
        /// 默认名称为物理字段名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                    return;
                _name = value;
            }
        }

        /// <summary>
        /// 权限类型
        /// </summary>
        public byte AuthorityType
        {
            get
            {
                return _authorityType;
            }
            set
            {
                if (_authorityType == value)
                    return;
                _authorityType = value;
            }
        }

        #endregion

    }
}