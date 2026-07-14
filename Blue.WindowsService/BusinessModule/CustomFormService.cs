//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomFormService.cs
// 描述：CustomForm 操作服务类
// 作者：ChenJie 
// 编写日期：2017/11/27
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomForm.
    /// </summary>
    public class CustomFormService : CommonNodeServices, ICustomFormContract
    {
        #region 业务实例

        private static readonly ICustomFormHandler customFormHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomFormHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomFormService() : base(customFormHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customform 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomFormInfo customFormInfo)
        {
            return customFormHandler.Insert(customFormInfo);
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象
        /// </summary>
        ///<param name="formId">业务编号</param>
        /// <returns> CustomFormInfo 对象</returns>
        public CustomFormInfo GetModelInfo(decimal formId)
        {
            return customFormHandler.GetModelInfo(formId);
        }

        /// <summary>
        /// 更新 CustomFormInfo 对象
        /// </summary>
        /// <param name="customFormInfo">CustomFormInfo 对象</param>
        public void Update(CustomFormInfo customFormInfo)
        {
            customFormHandler.Update(customFormInfo);
        }

        /// <summary>
        /// 删除 CustomFormInfo 对象
        /// </summary>
        ///<param name="formId">业务编号</param>
        /// <returns> CustomFormInfo 对象</returns>
        public void Delete(decimal formId)
        {
            customFormHandler.Delete(formId);
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomFormInfo 对象列表</returns>
        public IList<CustomFormInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customFormHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomForm 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomFormInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customFormHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 通过组合表编号查询数据填报的窗体数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        public int GetTotalCountByCombinedTableId(decimal combinedTableId)
        {
            return customFormHandler.GetTotalCountByCombinedTableId(combinedTableId);
        }

        /// <summary>
        /// 向 CustomForm 表中插入一条新记录
        /// </summary>
        /// <param name="customFormInfo">customFormInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            return customFormHandler.Insert(customFormInfo, upLoadFileInfos);
        }

        /// <summary>
        /// 更新数据表格和附件信息
        /// </summary>
        /// <param name="customFormInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(CustomFormInfo customFormInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            customFormHandler.Update(customFormInfo, upLoadFileInfos);
        }

        /// <summary>
        /// 获得 CustomFormInfo 对象的列表
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public IList<CustomFormInfo> GetModelInfos(decimal sectionId)
        {
            return customFormHandler.GetModelInfos(sectionId);
        }
        #endregion
    }
}
