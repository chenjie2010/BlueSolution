//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CorrelatedTableBusiness.cs
// 描述：关联实体业务类
// 作者：ChenJie 
// 编写日期：2016/08/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.DataAccessLibrary;

namespace AppFramework.Reference.BusinessLibrary
{
    /// <summary>
    /// 关联实体业务类
    /// </summary>
    public class CorrelatedTableBusiness : ICorrelatedBusiness
    {
        #region 工厂类实例

        private ICorrelatedTable dalCorrelatedTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="correlatedTable"></param>
        public CorrelatedTableBusiness(ICorrelatedTable correlatedTable)
        {
            this.dalCorrelatedTable = correlatedTable;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据外键编号获得另外一个外键编号列表
        /// </summary>
        /// <param name="firstForeignKey"></param>
        /// <returns></returns>
        public IList<decimal> GetSecondIds(decimal firstForeignKey)
        {
            // 验证输入
            if (firstForeignKey <= 0)
            {
                throw new ArgumentException("参数错误。");
            }
            IList<decimal> secondIds = dalCorrelatedTable.GetSecondIds(firstForeignKey);

            return secondIds;
        }

        #endregion

    }
}
