//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessLogicContainer.cs
// 描述: 业务层IOC容器，该业务独立于模块
// 作者：ChenJie 
// 编写日期：2016/07/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 业务层IOC容器，该业务独立于模块，属于单件模式（Singleton Pattern）
    /// </summary>
    public class BusinessLogicContainer
    {

        #region 内部成员变量

        private readonly IUnityContainer _loginModuleContainer;
        private readonly IUnityContainer _userModuleContainer;
        private readonly IUnityContainer _businessModuleContainer;
        private readonly IUnityContainer _systemModuleContainer;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        BusinessLogicContainer()
        {
            _loginModuleContainer = UnityHelper.GetUnityContainer("LoginModule");
            _userModuleContainer = UnityHelper.GetUnityContainer("UserModule");
            _systemModuleContainer = UnityHelper.GetUnityContainer("SystemModule");
            _businessModuleContainer = UnityHelper.GetUnityContainer("BusinessModule");
        }

        #endregion

        #region 嵌套类

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly BusinessLogicContainer instance = new BusinessLogicContainer();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static BusinessLogicContainer Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        /// <summary>
        /// an Unity Container of Login Module
        /// </summary>
        public IUnityContainer LoginModuleContainer
        { 
            get
            {
                return _loginModuleContainer;
            }
        }


        /// <summary>
        /// an Unity Container of User Module
        /// </summary>
        public IUnityContainer UserModuleContainer
        {
            get
            {
                return _userModuleContainer;
            }
        }

        /// <summary>
        /// an Unity Container of System Module
        /// </summary>
        public IUnityContainer SystemModuleContainer
        {
            get
            {
                return _systemModuleContainer;
            }
        }

        /// <summary>
        /// an Unity Container of Business Module
        /// </summary>
        public IUnityContainer BusinessModuleContainer
        {
            get
            {
                return _businessModuleContainer;
            }
        }   
             

        #endregion
    }
}
