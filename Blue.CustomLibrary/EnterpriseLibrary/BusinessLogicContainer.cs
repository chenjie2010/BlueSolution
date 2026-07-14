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
using Unity;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.CustomLibrary.EnterpriseLibrary
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
        private readonly IUnityContainer _generalAffairModuleContainer;
        private readonly IUnityContainer _dataFilledModuleContainer;
        private readonly IUnityContainer _businessDesignerModuleContainer;
        private readonly IUnityContainer _dataConvertionModuleContainer ;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        BusinessLogicContainer()
        {
            try
            {
                _loginModuleContainer = UnityHelper.GetUnityContainer("LoginModule");
                _userModuleContainer = UnityHelper.GetUnityContainer("UserModule");
                _systemModuleContainer = UnityHelper.GetUnityContainer("SystemModule");
                _businessModuleContainer = UnityHelper.GetUnityContainer("BusinessModule");
                _generalAffairModuleContainer = UnityHelper.GetUnityContainer("GeneralAffairModule");
                _dataFilledModuleContainer = UnityHelper.GetUnityContainer("DataFilledModule");
                _businessDesignerModuleContainer = UnityHelper.GetUnityContainer("BusinessDesignerModule");
                _dataConvertionModuleContainer = UnityHelper.GetUnityContainer("DataConvertionModule");
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常
                ExceptionHelper.NoExceptionPolicyWithLog(exception);
            }
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

        /// <summary>
        /// an Unity Container of GeneralAffair Module
        /// </summary>
        public IUnityContainer GeneralAffairModuleContainer
        {
            get
            {
                return _generalAffairModuleContainer;
            }
        }

        /// <summary>
        /// an Unity Container of DataFilled Module
        /// </summary>
        public IUnityContainer DataFilledModuleContainer
        {
            get
            {
                return _dataFilledModuleContainer;
            }
        }

        /// <summary>
        /// an Unity Container of BusinessDesigner Module
        /// </summary>
        public IUnityContainer BusinessDesignerModuleContainer
        {
            get
            {
                return _businessDesignerModuleContainer;
            }
        }


        /// <summary>
        /// an Unity Container of DataConvertion Module
        /// </summary>
        public IUnityContainer DataConvertionModuleContainer
        {
            get
            {
                return _dataConvertionModuleContainer;
            }
        }

        #endregion
    }
}
