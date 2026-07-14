//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CustomConfigure.cs
// 描述： 配置文件常量类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 节点段类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomConfigure<T> : ConfigurationSection
    {
        public CustomConfigure()
        {
        }

        /// <summary>
        /// 默认字符串选项
        /// </summary>
        [ConfigurationProperty("DefaultValue")]
        public string DefaultValue
        {
            get { return (string)this["DefaultValue"]; }
            set { this["DefaultValue"] = value; }
        }

        /// <summary>
        /// 子节点
        /// </summary>
        [ConfigurationProperty("Group", IsRequired = true)]
        public T Group
        {
            get { return (T)this["Group"]; }
            set { this["Group"] = value; }
        }

        /// <summary>
        /// 表示当前对象的 String。 
        /// </summary>
        /// <returns>当前对象的 String</returns>
        public override string ToString()
        {
            return string.Format("DefaultValue:{0}", DefaultValue);
        }

    }
}
