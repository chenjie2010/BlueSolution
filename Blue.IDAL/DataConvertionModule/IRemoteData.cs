//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IRemoteData.cs
// 描述: RemoteData 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/10/27
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
    /// RemoteData 接口
    /// </summary>
    public interface IRemoteData : ICommonNode, IPrincipalTable<RemoteDataInfo>
    {
		#region 接口
		

        #endregion
    }
}