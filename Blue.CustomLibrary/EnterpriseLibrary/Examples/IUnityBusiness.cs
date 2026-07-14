//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUnityBusiness.cs
// 描述: 使用 Unity 实现 AOP 操作的接口
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.CustomLibrary.EnterpriseLibrary
{
    /// <summary>
    /// 使用 Unity 实现 AOP 操作的接口
    /// </summary>
    public interface IUnityBusiness
    {
        void Print(string message);
    }
}
