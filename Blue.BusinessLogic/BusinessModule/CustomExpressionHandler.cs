//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomExpressionHandler.cs
// 描述：CustomExpression 业务处理类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomExpression.
    /// </summary>
    public class CustomExpressionHandler : ICustomExpressionHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomExpression dalCustomExpression = BusinessDataAccessFactory.CreateCustomExpression(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomExpressionHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customexpression 表中插入一条新记录
		/// </summary>
		/// <param name="customExpressionInfo"></param>
		public void Insert(CustomExpressionInfo customExpressionInfo)
		{
            
			// 验证输入
			if (customExpressionInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                dalCustomExpression.Insert(customExpressionInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
		}

        /// <summary>
        /// 获得 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        /// <returns> CustomExpressionInfo 对象</returns>
        public CustomExpressionInfo GetModelInfo(decimal parentDataFieldId, int sorting)
        {
            CustomExpressionInfo  customExpressionInfo = null;
            
			// 验证输入
			if(parentDataFieldId < 0)
            {
				return null;
            }

            try
            {
                customExpressionInfo =  dalCustomExpression.GetModelInfo(parentDataFieldId, sorting);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customExpressionInfo;
		}        
        
       /// <summary>
        ///  删除 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        public void Delete(decimal parentDataFieldId, int sorting)
        { 
            // 验证输入
			if(parentDataFieldId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomExpression.Delete(parentDataFieldId, sorting);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        public IList<CustomExpressionInfo> GetModelInfos(decimal parentDataFieldId)
        {
            //创建集合对象
            IList<CustomExpressionInfo>  customExpressionInfos = null;

            // 验证输入
            if (parentDataFieldId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                customExpressionInfos = dalCustomExpression.GetModelInfos(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customExpressionInfos;
		}

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        public int GetTotalCount(decimal parentDataFieldId)
        {
            int count = 0;

            // 验证输入
            if (parentDataFieldId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                count = dalCustomExpression.GetTotalCount(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
		}

        #endregion

        #region 自定义方法

        /// <summary>
        /// 获得表达式相关的字段节点列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId)
        {
            IList<CommonNode> nodes = null;

            // 验证输入
            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("父编号不能小于或是等于0。");
            }

            try
            {
                nodes = dalCustomExpression.GetCommonNodes(parentDataFieldId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodes;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
