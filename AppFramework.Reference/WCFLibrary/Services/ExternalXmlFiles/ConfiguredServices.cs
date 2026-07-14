//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ConfiguredServices.cs
// 描述: 服务配置在外部配置文件中的通用操作类
// 作者：ChenJie 
// 编写日期：2010-5-31
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("configuredServices")]
    public class ConfiguredServices
    {
        /// <summary>
        /// 加载外部配置文件
        /// </summary>
        /// <param name="filename">外部配置文件名称</param>
        /// <returns></returns>
        public static ConfiguredServices LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return new ConfiguredServices();

            XmlSerializer ser = new XmlSerializer(typeof(ConfiguredServices));
            using (FileStream fs = File.OpenRead(filename))
            {
                return (ConfiguredServices)ser.Deserialize(fs);
            }
        }

        /// <summary>
        /// 服务节点集合
        /// </summary>
        [XmlElement("service", typeof(ConfiguredService))]
        public List<ConfiguredService> configuredServices = new List<ConfiguredService>();
    }
}
