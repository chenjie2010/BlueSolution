//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ITreeNodeShow.cs
// 描述： 树形节点展示接口
// 作者：ChenJie 
// 编写日期：2016/09/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using AppFramework.Core;

namespace AppFramework.WinFormsLibrary.Common
{
    /// <summary>
    /// 树形节点展示接口
    /// </summary>
    public interface ITreeNodeShow
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        decimal TreeNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        string DefaultCode
        {
            get;
            set;
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        void SetActiveStatesOfControls(bool readOnly);

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        void SetModelInfo(CommonNode commonNode);

        /// <summary>
        /// 清除界面数据
        /// </summary>
        void ClearModelInfo();
      
    }
}
