//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessDesignerDataAccessFactory.cs
// 描述：业务设计模块抽象工厂类
// 作者：ChenJie 
// 编写日期：2018/09/07
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.CustomLibrary;
using Blue.IDAL.BusinessDesignerModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 业务设计模块抽象工厂类
    /// </summary>
    public sealed class BusinessDesignerDataAccessFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static BusinessDesignerDataAccessFactory()
        {
            nameSpace = "BusinessDesignerModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 DataAuditing 对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditing CreateDataAuditing()
        {
            return DALObjectHelper.CreateIDAL<IDataAuditing>(nameSpace, "DataAuditing");
        }

        /// <summary>
        ///  创建 CustomCell 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomCell CreateCustomCell()
        {
            return DALObjectHelper.CreateIDAL<ICustomCell>(nameSpace, "CustomCell");
        }

        /// <summary>
        ///  创建 CellStyle 对象
        /// </summary>
        /// <returns></returns>
        public static ICellStyle CreateCellStyle()
        {
            return DALObjectHelper.CreateIDAL<ICellStyle>(nameSpace, "CellStyle");
        }

        /// <summary>
        ///  创建 CustomReport 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomReport CreateCustomReport()
        {
            return DALObjectHelper.CreateIDAL<ICustomReport>(nameSpace, "CustomReport");
        }

        /// <summary>
        ///  创建 CustomSheet 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSheet CreateCustomSheet()
        {
            return DALObjectHelper.CreateIDAL<ICustomSheet>(nameSpace, "CustomSheet");
        }

        /// <summary>
        ///  创建 CustomSnapshot 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSnapshot CreateCustomSnapshot()
        {
            return DALObjectHelper.CreateIDAL<ICustomSnapshot>(nameSpace, "CustomSnapshot");
        }

        /// <summary>
        ///  创建 AppointmentBusiness 对象
        /// </summary>
        /// <returns></returns>
        public static IAppointmentBusiness CreateAppointmentBusiness()
        {
            return DALObjectHelper.CreateIDAL<IAppointmentBusiness>(nameSpace, "AppointmentBusiness");
        }        

        /// <summary>
        ///  创建 DataAuditingAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingAndDataField CreateDataAuditingAndDataField()
        {
            return DALObjectHelper.CreateIDAL<IDataAuditingAndDataField>(nameSpace, "DataAuditingAndDataField");
        }
        
        /// <summary>
        ///  创建 DataAuditingLog 对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingLog CreateDataAuditingLog()
        {
            return DALObjectHelper.CreateIDAL<IDataAuditingLog>(nameSpace, "DataAuditingLog");
        }

        /// <summary>
        ///  创建 DataAuditingStep 对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingStep CreateDataAuditingStep()
        {
            return DALObjectHelper.CreateIDAL<IDataAuditingStep>(nameSpace, "DataAuditingStep");
        }


        #endregion
    }
}
