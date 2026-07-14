//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomEnumHandler.cs
// 描述：CustomEnum 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/20
// Copyright 2016
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
    /// CustomEnum 接口
    /// </summary>
    public interface ICustomEnumHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomEnumInfo>
    {
        #region 接口

        /// <summary>
        /// 刷新排序
        /// </summary>
        void RefreshSorting();

        /// <summary>
        /// 获得枚举的单独项的值
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        string GetEnumText(decimal enumId, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        string GetEnumText(decimal enumId);

        /// <summary>
        /// 根据父节点编号获得所有子节点数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        DataSet GetEnumData(decimal parentEnumId);

        /// <summary>
        /// 在下拉型枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        KeyValueInfo GetDropDownListItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData);

        /// <summary>
        /// 在树形枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        KeyValueInfo GetTreeviewItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData);

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        object GetTreeData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        object GetDropdownListData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        string GetDropdownListEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        string GetTreeEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        object GetEnumData(decimal enumId, PhysicalDataFieldType physicalDataFieldType);

        /// <summary>
        /// 根据枚举编码获得枚举编号
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        decimal GetEnumId(string enumCode);

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal parentEnumId);

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        IList<string> GetTemplateColumnCaptions();

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得枚举选项列表
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        IList<CustomEnumInfo> GetEnumItems(decimal parentEnumId);

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(decimal parentEnumId, string enumName);

        /// <summary>
        /// 获取枚举的最大层级
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        int GetMaxLevel(decimal enumId);

        /// <summary>
        /// 是否是超大枚举
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        bool GetSuperEnumEnabled(decimal enumId);

        #endregion
    }
}
