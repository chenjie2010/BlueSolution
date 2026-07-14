using System;
//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserDataInterface.cs
// 描述: 用户数据接口
// 作者：ChenJie 
// 编写日期：2018/09/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarPoint.Win.Spread;
using AppFramework.Core;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 数据导出接口
    /// </summary>
    public interface IUserDataInterface
    {      
        #region 方法       

        /// <summary>
        /// 是否允许子表数据导入
        /// </summary>
        /// <param name="treeCode"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>
        bool AllowDataTableImported(string treeCode, ImportedMode importedMode);

        #endregion
    }
}
