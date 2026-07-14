//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataImportedError.cs
// 描述： 数据导入校验错误
// 作者：ChenJie 
// 编写日期：2018-07-19
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据导入校验错误
    /// </summary>
    [Description("数据导入校验错误")]
    public enum DataImportedError
    {
        /// <summary>
        ///  无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///  数据不能为空
        /// </summary>
        [Description("数据不能为空")]
        DataEmpty = 1,

        /// <summary>
        /// 数据重复
        /// </summary>
        [Description("数据重复")]
        DuplicatedData = 2,

        /// <summary>
        /// 数据长度超过限制
        /// </summary>
        [Description("数据长度超过限制")]
        DataLenth = 3,

        /// <summary>
        /// 数据已存在
        /// </summary>
        [Description("数据已存在")]
        DataExisted = 4,

        /// <summary>
        /// 数据不存在
        /// </summary>
        [Description("数据不存在")]
        DataNotExisted = 5,

        /// <summary>
        /// 数据格式错误
        /// </summary>
        [Description("数据格式错误")]
        ErrorFormat = 6,

        /// <summary>
        /// 父节点不存在
        /// </summary>
        [Description("父节点不存在")]
        ParentEmpty = 7,

        /// <summary>
        /// 节点编码前缀与根节点编码不一致
        /// </summary>
        [Description("节点编码前缀与根节点编码不一致")]
        CodeNotCoincide = 8,

        /// <summary>
        /// 字段类型错误
        /// </summary>
        [Description("字段类型错误")]
        DataTypeError = 9,

        /// <summary>
        /// 对应的数据表不存在
        /// </summary>
        [Description("对应的数据表不存在")]
        SheetNotExisted = 10,

        /// <summary>
        /// 正在使用中
        /// </summary>
        [Description("正在使用中")]
        InService = 11

    }
}
