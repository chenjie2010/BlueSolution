//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomGridViewPageEventArgs.cs
// 描述: CustomGridViewPageEventArgs 分页事件类
// 作者：ChenJie 
// 编写日期：2007-6-3
// Copyright 2007
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    ///  分页事件类
    /// </summary>
    public class CustomGridViewPageEventArgs : CancelEventArgs
    {
        private int _newPageIndex;

        public CustomGridViewPageEventArgs(int newPageIndex)
        {
            this._newPageIndex = newPageIndex;
        }

        public int NewPageIndex
        {
            get
            {
                return this._newPageIndex;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this._newPageIndex = value;
            }
        }
    }
}
