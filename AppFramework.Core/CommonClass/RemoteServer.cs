//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： RemoteServer.cs
// 描述： 远程服务器类
// 作者：ChenJie 
// 编写日期：2018/10/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格项类
    /// </summary>
    public class RemoteServer
    {
        #region 定义私有变量

        private string _remoteAddress = string.Empty;
        private string _remoteUserName = string.Empty;
        private string _remotePassword = string.Empty;
        private object _tag = null;

        #endregion

        #region 属性

        /// <summary>
        /// 远程数据交换地址
        /// </summary>        
        public string RemoteAddress
        {
            get
            {
                return _remoteAddress;
            }
            set
            {
                if (_remoteAddress == value)
                    return;
                _remoteAddress = value;
            }
        }

        /// <summary>
        /// 远程数据交换用户名
        /// </summary>        
        public string RemoteUserName
        {
            get
            {
                return _remoteUserName;
            }
            set
            {
                if (_remoteUserName == value)
                    return;
                _remoteUserName = value;
            }
        }

        /// <summary>
        /// 远程数据交换密码
        /// </summary>        
        public string RemotePassword
        {
            get
            {
                return _remotePassword;
            }
            set
            {
                if (_remotePassword == value)
                    return;
                _remotePassword = value;
            }
        }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RemoteServer()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteAddress"></param>
        /// <param name="remoteUserName"></param>
        /// <param name="remotePassword"></param>
        /// <param name="tag"></param>
        public RemoteServer(string remoteAddress, string remoteUserName, string remotePassword, object tag)
        {
            _remoteAddress = remoteAddress;
            _remoteUserName = remoteUserName;
            _remotePassword = remotePassword;
            _tag = tag;
        }
        

        #endregion

        #region 重载方法
        
        #endregion
    }
}
