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
using Blue.IDAL.BusinessModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 业务模块抽象工厂类
    /// </summary>
    public sealed class BusinessDataAccessFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static BusinessDataAccessFactory()
        {
            nameSpace = "BusinessModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 CustomEnum 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomEnum CreateCustomEnum()
        {
            return DALObjectHelper.CreateIDAL<ICustomEnum>(nameSpace, "CustomEnum");
        }

        /// <summary>
        ///  创建 CustomAssociation 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomAssociation CreateCustomAssociation()
        {
            return DALObjectHelper.CreateIDAL<ICustomAssociation>(nameSpace, "CustomAssociation");
        }

        /// <summary>
        ///  创建 AssociatedDataField 对象
        /// </summary>
        /// <returns></returns>
        public static IAssociatedDataField CreateAssociatedDataField()
        {
            return DALObjectHelper.CreateIDAL<IAssociatedDataField>(nameSpace, "AssociatedDataField");
        }

        /// <summary>
        ///  创建 CombinedDataField 对象
        /// </summary>
        /// <returns></returns>
        public static ICombinedDataField CreateCombinedDataField()
        {
            return DALObjectHelper.CreateIDAL<ICombinedDataField>(nameSpace, "CombinedDataField");
        }

        /// <summary>
        ///  创建 CombinedTableRelation 对象
        /// </summary>
        /// <returns></returns>
        public static ICombinedTableRelation CreateCombinedTableRelation()
        {
            return DALObjectHelper.CreateIDAL<ICombinedTableRelation>(nameSpace, "CombinedTableRelation");
        }
                
        /// <summary>
        ///  创建 CustomDatabase 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDatabase CreateCustomDatabase()
        {
            return DALObjectHelper.CreateIDAL<ICustomDatabase>(nameSpace, "CustomDatabase");
        }

        /// <summary>
        ///  创建 CustomCategory 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomCategory CreateCustomCategory()
        {
            return DALObjectHelper.CreateIDAL<ICustomCategory>(nameSpace, "CustomCategory");
        }

        /// <summary>
        /// 创建 CombinedTable 对象
        /// </summary>
        /// <returns></returns>
        public static ICombinedTable CreateCombinedTable()
        {
            return DALObjectHelper.CreateIDAL<ICombinedTable>(nameSpace, "CombinedTable");
        }

        /// <summary>
        ///  创建 CustomTable 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomTable CreateCustomTable()
        {
            return DALObjectHelper.CreateIDAL<ICustomTable>(nameSpace, "CustomTable");
        }

        /// <summary>
        ///  创建 CustomDataField 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDataField CreateCustomDataField()
        {
            return DALObjectHelper.CreateIDAL<ICustomDataField>(nameSpace, "CustomDataField");
        }

        /// <summary>
        ///  创建 CustomExpression 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomExpression CreateCustomExpression()
        {
            return DALObjectHelper.CreateIDAL<ICustomExpression>(nameSpace, "CustomExpression");
        }

        /// <summary>
        ///  创建 CustomGroup 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomGroup CreateCustomGroup()
        {
            return DALObjectHelper.CreateIDAL<ICustomGroup>(nameSpace, "CustomGroup");
        }

        /// <summary>
        ///  创建 CustomQuey 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomQuey CreateCustomQuey()
        {
            return DALObjectHelper.CreateIDAL<ICustomQuey>(nameSpace, "CustomQuey");
        }

        /// <summary>
        ///  创建 CustomQueyAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomQueyAndDataField CreateCustomQueyAndDataField()
        {
            return DALObjectHelper.CreateIDAL<ICustomQueyAndDataField>(nameSpace, "CustomQueyAndDataField");
        }

        /// <summary>
        ///  创建 CustomWorkflow 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflow CreateCustomWorkflow()
        {
            return DALObjectHelper.CreateIDAL<ICustomWorkflow>(nameSpace, "CustomWorkflow");
        }

        /// <summary>
        ///  创建 CustomWorkflowDirection 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowDirection CreateCustomWorkflowDirection()
        {
            return DALObjectHelper.CreateIDAL<ICustomWorkflowDirection>(nameSpace, "CustomWorkflowDirection");
        }

        /// <summary>
        ///  创建 CustomWorkflowInstance 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowInstance CreateCustomWorkflowInstance()
        {
            return DALObjectHelper.CreateIDAL<ICustomWorkflowInstance>(nameSpace, "CustomWorkflowInstance");
        }

        /// <summary>
        ///  创建 CustomWorkflowProcess 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowProcess CreateCustomWorkflowProcess()
        {
            return DALObjectHelper.CreateIDAL<ICustomWorkflowProcess>(nameSpace, "CustomWorkflowProcess");
        }

        /// <summary>
        ///  创建 CustomView 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomView CreateCustomView()
        {
            return DALObjectHelper.CreateIDAL<ICustomView>(nameSpace, "CustomView");
        }

        /// <summary>
        ///  创建 CustomViewAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomViewAndDataField CreateCustomViewAndDataField()
        {
            return DALObjectHelper.CreateIDAL<ICustomViewAndDataField>(nameSpace, "CustomViewAndDataField");
        }

        /// <summary>
        ///  创建 CustomViewAndTable 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomViewAndTable CreateCustomViewAndTable()
        {
            return DALObjectHelper.CreateIDAL<ICustomViewAndTable>(nameSpace, "CustomViewAndTable");
        }

        /// <summary>
        ///  创建 CustomData 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomData CreateCustomData()
        {
            return DALObjectHelper.CreateIDAL<ICustomData>(nameSpace, "CustomData");
        }

        /// <summary>
        ///  创建 CustomSection 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSection CreateCustomSection()
        {
            return DALObjectHelper.CreateIDAL<ICustomSection>(nameSpace, "CustomSection");
        }

        /// <summary>
        ///  创建 CustomForm 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomForm CreateCustomForm()
        {
            return DALObjectHelper.CreateIDAL<ICustomForm>(nameSpace, "CustomForm");
        }   

        /// <summary>
        ///  创建 WorkflowProcessAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static IWorkflowProcessAndDataField CreateWorkflowProcessAndDataField()
        {
            return DALObjectHelper.CreateIDAL<IWorkflowProcessAndDataField>(nameSpace, "WorkflowProcessAndDataField");
        }

        /// <summary>
        ///  创建 CustomMenu 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomMenu CreateCustomMenu()
        {
            return DALObjectHelper.CreateIDAL<ICustomMenu>(nameSpace, "CustomMenu");
        }

        /// <summary>
        ///  创建 CustomBusiness 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomBusiness CreateCustomBusiness()
        {
            return DALObjectHelper.CreateIDAL<ICustomBusiness>(nameSpace, "CustomBusiness");
        }

        /// <summary>
        ///  创建 DataFieldRelationship 对象
        /// </summary>
        /// <returns></returns>
        public static IDataFieldRelationship CreateDataFieldRelationship()
        {
            return DALObjectHelper.CreateIDAL<IDataFieldRelationship>(nameSpace, "DataFieldRelationship");
        }

        /// <summary>
        ///  创建 CustomWorkflowMap 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowMap CreateCustomWorkflowMap()
        {
            return DALObjectHelper.CreateIDAL<ICustomWorkflowMap>(nameSpace, "CustomWorkflowMap");
        }

        /// <summary>
        ///  创建 WorkflowInstanceStep 对象
        /// </summary>
        /// <returns></returns>
        public static IWorkflowInstanceStep CreateWorkflowInstanceStep()
        {
            return DALObjectHelper.CreateIDAL<IWorkflowInstanceStep>(nameSpace, "WorkflowInstanceStep");
        }

        /// <summary>
        ///  创建 WorkflowInstanceLog 对象
        /// </summary>
        /// <returns></returns>
        public static IWorkflowInstanceLog CreateWorkflowInstanceLog()
        {
            return DALObjectHelper.CreateIDAL<IWorkflowInstanceLog>(nameSpace, "WorkflowInstanceLog");
        }          

        /// <summary>
        ///  创建 AppointmentInstance 对象
        /// </summary>
        /// <returns></returns>
        public static IAppointmentInstance CreateAppointmentInstance()
        {
            return DALObjectHelper.CreateIDAL<IAppointmentInstance>(nameSpace, "AppointmentInstance");
        }

        /// <summary>
        ///  创建 CustomPrint 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomPrint CreateCustomPrint()
        {
            return DALObjectHelper.CreateIDAL<ICustomPrint>(nameSpace, "CustomPrint");
        }

        /// <summary>
        ///  创建 CustomPrintAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static ICustomPrintAndDataField CreateCustomPrintAndDataField()
        {
            return DALObjectHelper.CreateIDAL<ICustomPrintAndDataField>(nameSpace, "CustomPrintAndDataField");
        }

        /// <summary>
        ///  创建 PrintRecord 对象
        /// </summary>
        /// <returns></returns>
        public static IPrintRecord CreatePrintRecord()
        {
            return DALObjectHelper.CreateIDAL<IPrintRecord>(nameSpace, "PrintRecord");
        }

        /// <summary>
        ///  创建 DataBussiness 对象
        /// </summary>
        /// <returns></returns>
        public static IDataBussiness CreateDataBussiness()
        {
            return DALObjectHelper.CreateIDAL<IDataBussiness>(nameSpace, "DataBussiness");
        }

        /// <summary>
        ///  创建 QueryAndDataField 对象
        /// </summary>
        /// <returns></returns>
        public static IQueryAndDataField CreateQueryAndDataField()
        {
            return DALObjectHelper.CreateIDAL<IQueryAndDataField>(nameSpace, "QueryAndDataField");
        }

        /// <summary>
        ///  创建 UserQuery 对象
        /// </summary>
        /// <returns></returns>
        public static IUserQuery CreateUserQuery()
        {
            return DALObjectHelper.CreateIDAL<IUserQuery>(nameSpace, "UserQuery");
        }

        /// <summary>
        ///  创建 WorkflowInstanceDetail 对象
        /// </summary>
        /// <returns></returns>
        public static IWorkflowInstanceDetail CreateWorkflowInstanceDetail()
        {
            return DALObjectHelper.CreateIDAL<IWorkflowInstanceDetail>(nameSpace, "WorkflowInstanceDetail");
        }

        #endregion
    }
}
