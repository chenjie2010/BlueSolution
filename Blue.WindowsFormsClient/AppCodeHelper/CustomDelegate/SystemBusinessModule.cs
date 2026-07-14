//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LoginModuleDelegate.cs
// 描述: 登录模块的代理定义
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 获得字段
    /// </summary>
    /// <param name="commonNodes"></param>
    /// <param name="text"></param>
    public delegate void GetDataFieldDelegate(IList<CommonNode> commonNodes);

    /// <summary>
    /// 获得列表项
    /// </summary>
    /// <param name="commonNodes"></param>
    /// <param name="text"></param>
    public delegate void GetItemsDelegate(IList<CommonNode> commonNodes);

    /// <summary>
    /// 创建列表项
    /// </summary>
    public delegate void CreateItmesDelegate(DevExpress.XtraEditors.ListBoxControl listBoxControl);

    /// <summary>
    /// 移除列表项
    /// </summary>
    /// <param name="tableId"></param>
    public delegate void RemoveItmeDelegate(decimal tableId);

    /// <summary>
    /// 获得表达式
    /// </summary>
    /// <param name="commonNodes"></param>
    /// <param name="text"></param>
    public delegate void GetExpressionDataFieldDelegate(IList<CommonNode> commonNodes, string text);

    /// <summary>
    /// 验证表达式
    /// </summary>
    /// <param name="commonNodes"></param>
    /// <param name="text"></param>
    /// <param name="warning"></param>
    /// <returns></returns>
    public delegate bool ValidateExpressionDataFieldDelegate(IList<CommonNode> commonNodes, string text, out string warning);

    /// <summary>
    /// 获得视图中的表的关系对象
    /// </summary>
    /// <param name="customViewAndTableInfo"></param>
    public delegate void GetCustomViewAndTableDataDelegate(CustomViewAndTableInfo customViewAndTableInfo);

    /// <summary>
    /// 获得查询中的字段
    /// </summary>
    /// <param name="customQueyAndDataFieldInfo"></param>
    public delegate void GetCustomQueyAndDataFieldDelegate(CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo);

    /// <summary>
    /// 获得自定义查询对应的数据仓库编号和内容
    /// </summary>
    /// <param name="dataWarehouseId"></param>
    /// <param name="sqlSencetence"></param>
    public delegate void GetCustomQueryNameAndContentDelegate(byte dataWarehouseId, string sqlSencetence);

    /// <summary>
    /// 设置工作流节点关系
    /// </summary>
    /// <param name="processRelationship"></param>
    public delegate void SetWorkflowProcessDelegate(IList<KeyValueItem> processRelationship);

    /// <summary>
    /// 设置工作流节点字段选择条件
    /// </summary>
    /// <param name="workflowProcessAndDataFieldInfo"></param>
    public delegate void SetWorkflowProcessDataFieldDelegate(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo);

    /// <summary>
    /// 检查文本内容
    /// </summary>
    /// <param name="warning"></param>
    /// <returns></returns>
    public delegate bool CheckTextContentDelegate(ref string warning);

}
