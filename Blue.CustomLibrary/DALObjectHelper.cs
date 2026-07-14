//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DALFactory.cs
// 描述: DAL层抽象工厂类
// 作者：ChenJie 
// 编写日期：2016/07/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// DAL层抽象工厂类
    /// </summary>
    public sealed class DALObjectHelper
    {
        #region 只读变量

        private static readonly string assemblyName;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 默认的静态构造函数
        /// </summary>
        static DALObjectHelper()
        {
            assemblyName = AppSettingHelper.DALAssemblyName;
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 创建 类为 T 的对象
        /// </summary>
        /// <returns></returns>
        public static T CreateIDAL<T>(string nameSpace, string className)
        {
            T dalObject = (T)DynamicObjCreater.CreateInstance(assemblyName, nameSpace, className);

            return dalObject;
        }

        #endregion

    }
}
