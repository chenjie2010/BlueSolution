//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFilledModuleDelegate.cs
// 描述: 数据填报的代理定义
// 作者：ChenJie 
// 编写日期：2018-02-28
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using DevExpress.XtraEditors;
using AppFramework.WinFormsControls;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 设置提示
    /// </summary>
    /// <param name="extendedCustomDataFieldInfo"></param>
    public delegate void SetToolTipOnControlDelegate(ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo);

    /// <summary>
    /// 更新当前状态操作
    /// </summary>
    public delegate void UpdateCurretStateDelegate(decimal recordId);

    /// <summary>
    /// 数据加载操作
    /// </summary>
    public delegate void LoadDataHandlerDelegate(DevExpressGrid devExpressGrid);

    /// <summary>
    /// 设置权限操作
    /// </summary>
    /// <param name="devExpressGrid"></param>
    /// <param name="btnAdd"></param>
    /// <param name="btnDelete"></param>
    public delegate void SetAuthorityHandlerDelegate(DevExpressGrid devExpressGrid, SimpleButton btnAdd, SimpleButton btnDelete);

}
