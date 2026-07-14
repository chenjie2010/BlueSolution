//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LoginModuleDelegate.cs
// 描述: 登录模块的代理定义
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 获得排序字段
    /// </summary>
    /// <param name="sortingCondtions"></param>
    /// <param name="description"></param>
    public delegate void GetSortingCondtionsDelegate(IList<SortingCondtion> sortingCondtions, string description);

    /// <summary>
    /// 获得查询属性
    /// </summary>
    /// <param name="distinct"></param>
    /// <param name="pageSize"></param>
    public delegate void GetQueryPropertiesDelegate(bool distinct, int pageSize);

    public delegate void LoadDataFieldCallback(decimal nodeId);

}
