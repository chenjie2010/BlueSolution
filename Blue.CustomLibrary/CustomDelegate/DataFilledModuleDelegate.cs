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
using Blue.Model.BusinessModule;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 设置提示
    /// </summary>
    /// <param name="extendedCustomDataFieldInfo"></param>
    public delegate void SetToolTipOnControlDelegate(ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo);

    /// <summary>
    /// 
    /// </summary>
    public delegate void LoadDataHanlerDelegate(AppFramework.WinFormsControls.DevExpressGrid devExpressGrid);
}
