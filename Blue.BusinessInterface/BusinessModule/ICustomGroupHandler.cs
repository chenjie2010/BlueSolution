//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomGroupHandler.cs
// 描述：CustomGroup 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
/// <summary>
    /// CustomGroup 接口
    /// </summary>
    public interface ICustomGroupHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomGroupInfo>
    {
        #region 接口

        /// <summary>
        /// 获得数据集(获得节点自身数据)
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal groupId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentGroupId"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal parentGroupId, GroupType groupType);

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, byte groupType, ref int totalCount);

        #endregion
    }
}
