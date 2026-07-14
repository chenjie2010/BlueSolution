//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNode.cs
// 描述： CommonNode 操作服务类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 注册信息
    /// </summary>
    [Serializable]
    public class RegisterInfo
    {
        #region 内部成员变量

        private string _departmentName;
        private string _keyName;
        private SoftwareVersion _softwareVersion;
        private DateTime _registerTime;


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RegisterInfo()
        {
            _departmentName = "未知";
            _keyName = "default";
            _softwareVersion = SoftwareVersion.UnKown;
            _registerTime = Convert.ToDateTime("2012-1-1");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="departmentName"></param>
        /// <param name="keyName"></param>
        /// <param name="softwareVersion"></param>
        /// <param name="registerTime"></param>
        public RegisterInfo(string departmentName, string keyName, SoftwareVersion softwareVersion, DateTime registerTime)
        {
            _departmentName = departmentName;
            _keyName = keyName;
            _softwareVersion = softwareVersion;
            _registerTime = registerTime;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 单位名称
        /// </summary>
        public string DepartmentName
        {
            get { return _departmentName; }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyName
        {
            get { return _keyName; }
        }

        /// <summary>
        /// 软件版本
        /// </summary>
        public SoftwareVersion SoftwareVersion
        {
            get { return _softwareVersion; }
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime
        {
            get { return _registerTime; }
        }

        #endregion
    }
}
