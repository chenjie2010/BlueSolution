//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataRelation.cs
// 描述: DataRelation 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.DataConvertionModule;

namespace Blue.IDAL.DataConvertionModule
{
    /// <summary>
    /// DataRelation 接口
    /// </summary>
    public interface IDataRelation : ICommonNode, IPrincipalTable<DataRelationInfo>
    {
		#region 接口
		

        #endregion
    }
}