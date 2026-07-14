//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserInfoConfig.cs
// 描述： 用户名登录类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;
using System.ComponentModel;
using AppFramework.Core;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 用户配置类
    /// </summary>
    [Serializable]
    public class UserInfoConfig : ConfigurationElement
    {
        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserInfoConfig()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>  
        public UserInfoConfig(string userName)
        {
            UserName = userName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// <param name="remeberPassword">是否记住密码</param>
        /// <param name="autoLogon">是否自动登录</param>
        /// <param name="logonState">登录状态</param>
        public UserInfoConfig(string userName, string userPassword, bool remeberPassword, bool autoLogon, UserLogonState logonState)
        {
            UserName = userName;
            UserPassword = userPassword;
            RemeberPassword = remeberPassword;
            AutoLogon = autoLogon;
            LogonState = logonState;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 用户名
        /// </summary>
        [ConfigurationProperty("UserName", IsRequired = true, IsKey = true)]
        public string UserName
        {
            get
            {
                return (string)this["UserName"];
            }
            set
            {
                this["UserName"] = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("UserPassword", IsRequired = true)]
        public string UserPassword
        {
            get
            {
                return (string)this["UserPassword"];
            }
            set
            {
                this["UserPassword"] = value;
            }
        }

        /// <summary>
        /// 是否记住密码
        /// </summary>
        [ConfigurationProperty("RemeberPassword", IsRequired = true)]
        public bool RemeberPassword
        {
            get
            {
                return (bool)this["RemeberPassword"];
            }
            set
            {
                this["RemeberPassword"] = value;
            }
        }

        /// <summary>
        /// 是否自动登录
        /// </summary>
        [ConfigurationProperty("AutoLogon", IsRequired = true)]
        public bool AutoLogon
        {
            get
            {
                return (bool)this["AutoLogon"];
            }
            set
            {
                this["AutoLogon"] = value;
            }
        }



        /// <summary>
        /// 登录状态
        /// </summary>
        [ConfigurationProperty("LogonState", IsRequired = true)]
        [TypeConverter(typeof(UserLogonStateConvert))]
        public UserLogonState LogonState
        {
            get
            {
                return (UserLogonState)this["LogonState"];
            }
            set
            {
                this["LogonState"] = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 更新对象的值
        /// </summary>
        /// <param name="channelConfig">用户配置</param>
        public void Update(UserInfoConfig userConfig)
        {
            UserName = userConfig.UserName;
            UserPassword = userConfig.UserPassword;
            RemeberPassword = userConfig.RemeberPassword;
            AutoLogon = userConfig.AutoLogon;
            LogonState = userConfig.LogonState;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UserName;
        }

        /// <summary>
        /// 是否相等
        /// </summary>
        /// <param name="userConfig"></param>
        /// <returns></returns>        
        public bool Equals(UserInfoConfig userConfig)
        {
            if ((UserName == userConfig.UserName) && (UserPassword == userConfig.UserPassword)
                && (RemeberPassword == userConfig.RemeberPassword) && (AutoLogon == userConfig.AutoLogon)
                && (LogonState == userConfig.LogonState))
            {
                return true;
            }

            return false;
        }
        #endregion

    }
}
