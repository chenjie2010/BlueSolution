//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：SystemDataAccessFactory.cs
// 描述：系统模块抽象工厂类
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
using Blue.IDAL.SystemModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 系统模块抽象工厂类
    /// </summary>
    public sealed class SystemDataAccessFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemDataAccessFactory()
        {
            nameSpace = "SystemModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 CustomDepartment 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDepartment CreateCustomDepartment()
        {
            return DALObjectHelper.CreateIDAL<ICustomDepartment>(nameSpace, "CustomDepartment");
        }

        /// <summary>
        ///  创建 UserType 对象
        /// </summary>
        /// <returns></returns>
        public static IUserType CreateUserType()
        {
            return DALObjectHelper.CreateIDAL<IUserType>(nameSpace, "UserType");
        }

        /// <summary>
        ///  创建 SystemConfig 对象
        /// </summary>
        /// <returns></returns>
        public static ISystemConfig CreateSystemConfig()
        {
            return DALObjectHelper.CreateIDAL<ISystemConfig>(nameSpace, "SystemConfig");
        }

        /// <summary>
        ///  创建 DepartmentScope 对象
        /// </summary>
        /// <returns></returns>
        public static IDepartmentScope CreateDepartmentScope()
        {
            return DALObjectHelper.CreateIDAL<IDepartmentScope>(nameSpace, "DepartmentScope");
        }

        /// <summary>
        ///  创建 UserTypeScope 对象
        /// </summary>
        /// <returns></returns>
        public static IUserTypeScope CreateUserTypeScope()
        {
            return DALObjectHelper.CreateIDAL<IUserTypeScope>(nameSpace, "UserTypeScope");
        }


        /// <summary>
        ///  创建 UserLog 对象
        /// </summary>
        /// <returns></returns>
        public static IUserLog CreateUserLog()
        {
            return DALObjectHelper.CreateIDAL<IUserLog>(nameSpace, "UserLog");
        }

        /// <summary>
        ///  创建 RoleAndUser 对象
        /// </summary>
        /// <returns></returns>
        public static IRoleAndUser CreateRoleAndUser()
        {
            return DALObjectHelper.CreateIDAL<IRoleAndUser>(nameSpace, "RoleAndUser");
        }

        /// <summary>
        ///  创建 RoleAndPrint 对象
        /// </summary>
        /// <returns></returns>
        public static IRoleAndPrint CreateRoleAndPrint()
        {
            return DALObjectHelper.CreateIDAL<IRoleAndPrint>(nameSpace, "RoleAndPrint");
        }

        /// <summary>
        ///  创建 CustomRole 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomRole CreateCustomRole()
        {
            return DALObjectHelper.CreateIDAL<ICustomRole>(nameSpace, "CustomRole");
        }

        /// <summary>
        ///  创建 UserMessage 对象
        /// </summary>
        /// <returns></returns>
        public static IUserMessage CreateUserMessage()
        {
            return DALObjectHelper.CreateIDAL<IUserMessage>(nameSpace, "UserMessage");
        }

        /// <summary>
        ///  创建 MessageAndRole 对象
        /// </summary>
        /// <returns></returns>
        public static IMessageAndRole CreateMessageAndRole()
        {
            return DALObjectHelper.CreateIDAL<IMessageAndRole>(nameSpace, "MessageAndRole");
        }

        /// <summary>
        ///  创建 RoleAndBusiness 对象
        /// </summary>
        /// <returns></returns>
        public static IRoleAndBusiness CreateRoleAndBusiness()
        {
            return DALObjectHelper.CreateIDAL<IRoleAndBusiness>(nameSpace, "RoleAndBusiness");
        }

        /// <summary>
        ///  创建 RoleAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static IRoleAndDataField CreateRoleAndDataField()
        {
            return DALObjectHelper.CreateIDAL<IRoleAndDataField>(nameSpace, "RoleAndDataField");
        }

        /// <summary>
        ///  创建 RoleAndTable 对象
        /// </summary>
        /// <returns></returns>
        public static IRoleAndTable CreateRoleAndTable()
        {
            return DALObjectHelper.CreateIDAL<IRoleAndTable>(nameSpace, "RoleAndTable");
        }

        /// <summary>
        ///  创建 CustomInterface 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomInterface CreateCustomInterface()
        {
            return DALObjectHelper.CreateIDAL<ICustomInterface>(nameSpace, "CustomInterface");
        }

        /// <summary>
        ///  创建 CustomInterfaceAndDep 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomInterfaceAndDep CreateCustomInterfaceAndDep()
        {
            return DALObjectHelper.CreateIDAL<ICustomInterfaceAndDep>(nameSpace, "CustomInterfaceAndDep");
        }

        /// <summary>
        ///  创建 CustomInterfaceAndUserType 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomInterfaceAndUserType CreateCustomInterfaceAndUserType()
        {
            return DALObjectHelper.CreateIDAL<ICustomInterfaceAndUserType>(nameSpace, "CustomInterfaceAndUserType");
        }

        #endregion
    }
}
