//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ServerAddressConfigParentNode.cs
// 描述： 配置文件父节点类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 配置文件父节点类
    /// </summary>
    public class ServerAddressConfigParentNode : ConfigurationElementCollection
    {
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ServerAddressConfigParentNode()
        {
        }

        /// <summary>
        /// 创建新节点
        /// </summary>
        /// <returns>ConfigurationElement类型的对象</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerAddressConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ServerAddressConfig(elementName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerAddressConfig)element).ServerName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ServerAddressConfig this[int index]
        {
            get
            { return (ServerAddressConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public int IndexOf(ServerAddressConfig userConfig)
        {
            return BaseIndexOf(userConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ServerAddressConfig this[string name]
        {
            get
            {
                return (ServerAddressConfig)BaseGet(name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        #region 添加/移除/清除节点
        /// <summary>
        /// 
        /// </summary>
        public new string AddElementName
        {
            get
            {
                return base.AddElementName;
            }

            set
            {
                base.AddElementName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientConfig"></param>
        public void Add(ServerAddressConfig clientConfig)
        {
            BaseAdd(clientConfig);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientConfig"></param>
        public void Remove(ServerAddressConfig clientConfig)
        {
            if (BaseIndexOf(clientConfig) >= 0)
                BaseRemove(clientConfig.ServerName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        public new string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public new string ClearElementName
        {
            get
            {
                return base.ClearElementName;
            }

            set
            {
                base.AddElementName = value;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        #endregion

        #region 集合数量
        /// <summary>
        /// 
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }
        #endregion

    }
}
