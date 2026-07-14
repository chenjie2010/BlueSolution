//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ConfiguredService.cs
// 描述: 服务配置在外部配置文件中的通用操作类
// 作者：ChenJie 
// 编写日期：2010-5-31
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Xml;
using System.Xml.Serialization;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// ConfiguredService类,定义服务属性的类型
    /// </summary>
    public class ConfiguredService
    {
        /// <summary>
        /// 服务属性的类型
        /// </summary>
        [XmlAttribute("type")]
        public string Type;
    }
}
