//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomExpressionService.cs
// 描述：CustomExpression 操作服务类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomExpression.
    /// </summary>
    public class CustomExpressionService : ICustomExpressionContract
    {
        #region 业务实例

        private static readonly ICustomExpressionHandler customExpressionHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomExpressionHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomExpressionService()
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customexpression 表中插入一条新记录
        /// </summary>
        /// <param name="customExpressionInfo"></param>
        public void Insert(CustomExpressionInfo customExpressionInfo)
        {
            customExpressionHandler.Insert(customExpressionInfo);
        }

        /// <summary>
        /// 获得 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        /// <returns> CustomExpressionInfo 对象</returns>
        public CustomExpressionInfo GetModelInfo(decimal parentDataFieldId, int sorting)
        {
            return customExpressionHandler.GetModelInfo(parentDataFieldId, sorting);
        }

        /// <summary>
        /// 删除 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        /// <returns> CustomExpressionInfo 对象</returns>
        public void Delete(decimal parentDataFieldId, int sorting)
        {
            customExpressionHandler.Delete(parentDataFieldId, sorting);
        }

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>	
        ///<param name="parentDataFieldId">字段编号</param>
        /// <returns>CustomExpressionInfo 对象列表</returns>
        public IList<CustomExpressionInfo> GetModelInfos(decimal parentDataFieldId)
        {
            return customExpressionHandler.GetModelInfos(parentDataFieldId);
        }

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        public int GetTotalCount(decimal parentDataFieldId)
        {
            return customExpressionHandler.GetTotalCount(parentDataFieldId);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得表达式相关的字段节点列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId)
        {
            return customExpressionHandler.GetCommonNodes(parentDataFieldId);

        }

        #endregion
    }
}
