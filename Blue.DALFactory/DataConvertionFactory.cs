//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessDataAccessFactory.cs
// 描述：业务模块抽象工厂类
// 作者：ChenJie 
// 编写日期：2016/08/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.CustomLibrary;
using Blue.IDAL.DataConvertionModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 数据转换模块抽象工厂类
    /// </summary>
    public sealed class DataConvertionFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataConvertionFactory()
        {
            nameSpace = "DataConvertionModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 DataRelation 对象
        /// </summary>
        /// <returns></returns>
        public static IDataRelation CreateDataRelation()
        {
            return DALObjectHelper.CreateIDAL<IDataRelation>(nameSpace, "DataRelation");
        }

        /// <summary>
        ///  创建 DataFieldRelation 对象
        /// </summary>
        /// <returns></returns>
        public static IDataFieldRelation CreateDataFieldRelation()
        {
            return DALObjectHelper.CreateIDAL<IDataFieldRelation>(nameSpace, "DataFieldRelation");
        }

        /// <summary>
        ///  创建 RemoteData 对象
        /// </summary>
        /// <returns></returns>
        public static IRemoteData CreateRemoteData()
        {
            return DALObjectHelper.CreateIDAL<IRemoteData>(nameSpace, "RemoteData");
        }

        /// <summary>
        ///  创建 RemoteDataAndField 对象
        /// </summary>
        /// <returns></returns>
        public static IRemoteDataAndField CreateRemoteDataAndField()
        {
            return DALObjectHelper.CreateIDAL<IRemoteDataAndField>(nameSpace, "RemoteDataAndField");
        }
        
        #endregion
    }
}
