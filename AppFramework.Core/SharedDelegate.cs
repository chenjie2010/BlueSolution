//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： EditState.cs
// 描述： 编辑状态枚举
// 作者：ChenJie 
// 编写日期：2016/08/18
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;

namespace AppFramework.Core
{
    /// <summary>
    /// 弹出消息对话框
    /// </summary>
    /// <param name="message"></param>
    public delegate void PopupMessageDelegate(SystemMessage message);

    /// <summary>
    /// 显示提示消息框
    /// </summary>
    /// <param name="alterMessage"></param>
    public delegate void ShowAlterMessageDelegate(SystemMessage alterMessage);

    /// <summary>
    /// 初始化下拉树形结构
    /// </summary>
    public delegate void InitPartialTreeDelegate();

    /// <summary>
    /// 方法调用委托
    /// </summary>
    public delegate void ThreadMethodInvoker();

    /// <summary>
    /// 线程调用
    /// </summary>
    /// <param name="state"></param>
    public delegate void AsynFlashCaller(out bool state);

    /// <summary>
    /// 获得下拉树形结构中被选择的节点
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void GetTreeNodeDelegate(CommonNode commonNode);

    /// <summary>
    /// 获得下拉树形结构中被选择的节点
    /// </summary>
    /// <param name="commonNode"></param>
    /// <param name="text"></param>
    public delegate void GetAlternativeTreeNodeDelegate(CommonNode commonNode, string text);

    /// <summary>
    /// 获得下拉树形结构中被选择的节点列表
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void GetTreeNodeListDelegate(IList<CommonNode> commonNodes);

    /// <summary>
    /// 设置物理数据类型的默认值与取值范围
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="physicalDataFieldType"></param>
    public delegate void SetDefaultItemDelegate(object tag, PhysicalDataFieldType physicalDataFieldType);

    /// <summary>
    /// 关闭窗体
    /// </summary>
    public delegate void CloseBreakFormDelegate();

    /// <summary>
    /// 节点选择
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void NodeSelectedDelegate(CommonNode commonNode);

    /// <summary>
    /// 节点清除
    /// </summary>
    public delegate void NodeRemovedDelegate();

    /// <summary>
    /// 多个节点选择
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void MultiNodeSelectedDelegate(IList<CommonNode> commonNode);

    /// <summary>
    /// 获得实体内容
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void GetEntityDelegate(CommonNode commonNode);

    /// <summary>
    /// 获得文本内容与附件
    /// </summary>
    /// <param name="richText"></param>
    /// <param name="upLoadFileInfos"></param>
    public delegate void GetRichTextAndAttachmentsDelegate(string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

    /// <summary>
    /// 获得编号
    /// </summary>
    /// <param name="identifier"></param>
    public delegate void GetIdentifierDelegate(decimal identifier);

    /// <summary>
    /// 数据提交
    /// </summary>
    /// <param name="hasAlreadyRead"></param>
    public delegate void DataSumbittedDelegate(bool hasAlreadyRead);

    /// <summary>
    /// 数据行
    /// </summary>
    /// <param name="dataRow"></param>
    public delegate void DataRowConfrimedDelegate(DataRow dataRow);

    /// <summary>
    /// 级联节点选择
    /// </summary>
    /// <param name="commonNode"></param>
    public delegate void CscadeNodeSelectedDelegate(IList<CommonNode> commonNode);

    /// <summary>
    /// 关闭窗体
    /// </summary>
    public delegate void CloseFormDelegate();

    /// <summary>
    /// 增加操作
    /// </summary>
    /// <param name="dataRow"></param>
    public delegate void AddHandlerDelegate(DataRow dataRow);

    /// <summary>
    /// 编辑操作
    /// </summary>
    /// <param name="dataRow"></param>
    /// <param name="readOnly"></param>
    public delegate void EditHandlerDelegate(DataRow dataRow, bool readOnly);
    
    /// <summary>
    /// 删除操作
    /// </summary>
    /// <param name="recordId"></param>
    public delegate void DeleteHandlerDelegate(IList<decimal> recordIds);

    ///// <summary>
    ///// 编辑操作
    ///// </summary>
    ///// <param name="recordId"></param>
    //public delegate void BulkEditHandlerDelegate(IList<decimal> recordIds);

    /// <summary>
    /// 移动记录操作
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tableId"></param>
    /// <param name="recordId"></param>
    /// <param name="movedDriection"></param>
    /// <returns></returns>
    public delegate void MoveRecordHandlerDelegate(decimal userId, decimal tableId, decimal recordId, MovedDriection movedDriection);

    /// <summary>
    /// 提交操作
    /// </summary>
    public delegate void SumbittedHandlerDelegate();

    /// <summary>
    /// 加载数据
    /// </summary>
    public delegate void LoadDataDelegate();

    /// <summary>
    /// 编辑操作
    /// </summary>
    /// <param name="bluk"></param>
    public delegate void BulkEditedHandlerDelegate(decimal tableId, bool bluk);

    /// <summary>
    /// 提交操作
    /// </summary>
    /// <param name="text"></param>
    public delegate void TextSumbittedHandlerDelegate(string text);

    /// <summary>
    /// 提交操作
    /// </summary>
    /// <param name="reviewerId"></param>
    /// <param name="text"></param>
    public delegate void DataSumbittedHandlerDelegate(decimal reviewerId, string text);

    /// <summary>
    /// 刷新数据
    /// </summary>
    public delegate void RefreshFormDelegate();

    /// <summary>
    /// 返回操作
    /// </summary>
    public delegate void GoBackDelegate();

    /// <summary>
    /// 数据导入模式
    /// </summary>
    /// <param name="importedMode"></param>
    public delegate void DataImportedDelegate(ImportedMode importedMode);

    /// <summary>
    /// 数据导出模式
    /// </summary>
    /// <param name="importedMode"></param>
    public delegate void DataExportedDelegate(ExportedMode exportedMode);    

    /// <summary>
    /// 查询条件
    /// </summary>
    /// <param name="whereConditons"></param>
    public delegate void SetWhereConditonDelegate(IList<WhereConditon> whereConditons);

}
