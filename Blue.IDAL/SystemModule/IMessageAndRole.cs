//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IMessageAndRole.cs
// 描述: MessageAndRole 数据访问层接口
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.SystemModule;

namespace Blue.IDAL.SystemModule
{
    /// <summary>
    /// MessageAndRole 接口
    /// </summary>
    public interface IMessageAndRole : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 根据公告编号获得角色列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRoles(decimal messageId);

        /// <summary>
        /// 是否授权读取该通知
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool IsAuthoritiedNotice(decimal messageId, decimal userId);

        #endregion
    }
}