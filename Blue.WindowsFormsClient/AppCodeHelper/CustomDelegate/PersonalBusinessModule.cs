//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PersonalBusinessModule.cs
// 描述: 工作流的委托定义
// 作者：ChenJie 
// 编写日期：2018-06-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 获得下一步节点的处理人
    /// </summary>
    /// <param name="commonNodes"></param>
    /// <param name="text"></param>
    public delegate void GetReviewersDelegate(Dictionary<decimal, decimal> reivewers);
    

}
