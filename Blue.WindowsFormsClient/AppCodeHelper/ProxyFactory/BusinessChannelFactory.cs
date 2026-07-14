//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessChannelFactory.cs
// 描述: 业务管理模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2016/08/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 业务管理模块类
    /// </summary>
    public sealed class BusinessChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static BusinessChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建枚举管理的 ICustomEnumContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomEnumContract CreateCustomEnumContract()
        {
            ICustomEnumContract customEnumContract = ServiceProxyFactory.Create<ICustomEnumContract>("CustomEnumService");

            return customEnumContract;
        }

        /// <summary>
        /// 根据默认地址来创建枚举管理的 ICustomAssociationContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomAssociationContract CreateCustomAssociationContract()
        {
            ICustomAssociationContract customAssociationContract = ServiceProxyFactory.Create<ICustomAssociationContract>("CustomAssociationService");

            return customAssociationContract;
        }

        /// <summary>
        /// 根据默认地址来创建枚举管理的 IAssociatedDataFieldContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static IAssociatedDataFieldContract CreateAssociatedDataFieldContract()
        {
            IAssociatedDataFieldContract associatedDataFieldContract = ServiceProxyFactory.Create<IAssociatedDataFieldContract>("AssociatedDataFieldService");

            return associatedDataFieldContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomDatabaseService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDatabaseContract CreateCustomDatabaseContract()
        {
            ICustomDatabaseContract customRoleContract = ServiceProxyFactory.Create<ICustomDatabaseContract>("CustomDatabaseService");

            return customRoleContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomGroupService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomCategoryContract CreateCustomCategoryContract()
        {
            ICustomCategoryContract customCategoryContract = ServiceProxyFactory.Create<ICustomCategoryContract>("CustomCategoryService");

            return customCategoryContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomTableService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomTableContract CreateCustomTableContract()
        {
            ICustomTableContract customRoleContract = ServiceProxyFactory.Create<ICustomTableContract>("CustomTableService");

            return customRoleContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomDataFieldService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDataFieldContract CreateCustomDataFieldContract()
        {
            ICustomDataFieldContract customRoleContract = ServiceProxyFactory.Create<ICustomDataFieldContract>("CustomDataFieldService");

            return customRoleContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomExpressionService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomExpressionContract CreateCustomExpressionContract()
        {
            ICustomExpressionContract customRoleContract = ServiceProxyFactory.Create<ICustomExpressionContract>("CustomExpressionService");

            return customRoleContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomGroupService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomGroupContract CreateCustomGroupContract()
        {
            ICustomGroupContract customRoleContract = ServiceProxyFactory.Create<ICustomGroupContract>("CustomGroupService");

            return customRoleContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CombinedTableService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICombinedTableContract CreateCombinedTableContract()
        {
            ICombinedTableContract combinedTableContract = ServiceProxyFactory.Create<ICombinedTableContract>("CombinedTableService");

            return combinedTableContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomViewService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomViewContract CreateCustomViewContract()
        {
            ICustomViewContract customViewContract = ServiceProxyFactory.Create<ICustomViewContract>("CustomViewService");

            return customViewContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomQueyService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomQueyContract CreateCustomQueyContract()
        {
            ICustomQueyContract customQueyContract = ServiceProxyFactory.Create<ICustomQueyContract>("CustomQueyService");

            return customQueyContract;
        }        

        /// <summary>
        /// 根据默认地址来创建 CustomWorkflowService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowContract CreateCustomWorkflowContract()
        {
            ICustomWorkflowContract customWorkflowContract = ServiceProxyFactory.Create<ICustomWorkflowContract>("CustomWorkflowService");

            return customWorkflowContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomWorkflowProcessService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowProcessContract CreateCustomWorkflowProcessContract()
        {
            ICustomWorkflowProcessContract customWorkflowProcessContract = ServiceProxyFactory.Create<ICustomWorkflowProcessContract>("CustomWorkflowProcessService");

            return customWorkflowProcessContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomWorkflowInstanceService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomWorkflowInstanceContract CreateCustomWorkflowInstanceContract()
        {
            ICustomWorkflowInstanceContract customWorkflowInstanceContract = ServiceProxyFactory.Create<ICustomWorkflowInstanceContract>("CustomWorkflowInstanceService");

            return customWorkflowInstanceContract;
        }

        /// <summary>
        /// 根据默认地址来创建 WorkflowInstanceStepService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IWorkflowInstanceStepContract CreateWorkflowInstanceStepContract()
        {
            IWorkflowInstanceStepContract workflowInstanceStepContract = ServiceProxyFactory.Create<IWorkflowInstanceStepContract>("WorkflowInstanceStepService");

            return workflowInstanceStepContract;
        }


        /// <summary>
        /// 根据默认地址来创建 CustomDataService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDataContract CreateCustomDataContract()
        {
            ICustomDataContract customViewAndDataFieldContract = ServiceProxyFactory.Create<ICustomDataContract>("CustomDataService");

            return customViewAndDataFieldContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomSectionService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSectionContract CreateCustomSectionContract()
        {
            ICustomSectionContract customSectionContract = ServiceProxyFactory.Create<ICustomSectionContract>("CustomSectionService");

            return customSectionContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomFormService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomFormContract CreateCustomFormContract()
        {
            ICustomFormContract customViewAndDataFieldContract = ServiceProxyFactory.Create<ICustomFormContract>("CustomFormService");

            return customViewAndDataFieldContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomFormService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomMenuContract CreateCustomMenuContract()
        {
            ICustomMenuContract customMenuContract = ServiceProxyFactory.Create<ICustomMenuContract>("CustomMenuService");

            return customMenuContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomBusinessService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomBusinessContract CreateCustomBusinessContract()
        {
            ICustomBusinessContract customBusinessContract = ServiceProxyFactory.Create<ICustomBusinessContract>("CustomBusinessService");

            return customBusinessContract;
        }

        /// <summary>
        /// 根据默认地址来创建 AppointmentInstanceService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IAppointmentInstanceContract CreateAppointmentInstanceContract()
        {
            IAppointmentInstanceContract customBusinessContract = ServiceProxyFactory.Create<IAppointmentInstanceContract>("AppointmentInstanceService");

            return customBusinessContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomPrintService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomPrintContract CreateCustomPrintContract()
        {
            ICustomPrintContract customPrintContract = ServiceProxyFactory.Create<ICustomPrintContract>("CustomPrintService");

            return customPrintContract;
        }

        /// <summary>
        /// 根据默认地址来创建 DataBussinessService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IDataBussinessContract CreateDataBussinessContract()
        {
            IDataBussinessContract dataBussinessContract = ServiceProxyFactory.Create<IDataBussinessContract>("DataBussinessService");

            return dataBussinessContract;
        }

        /// <summary>
        /// 根据默认地址来创建 UserQueryService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IUserQueryContract CreateUserQueryContract()
        {
            IUserQueryContract userQueryContract = ServiceProxyFactory.Create<IUserQueryContract>("UserQueryService");

            return userQueryContract;
        }

        #endregion
    }
}
