//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：FormShowStyleSetting.cs
// 描述：表格设置类
// 作者：ChenJie 
// 编写日期：2018/02/14
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格设置类
    /// </summary>
    [Serializable]
    public class FormShowStyleSetting
    {
        #region 属性

        /// <summary>
        /// 表格完整模式
        /// </summary>
        public bool FormCompleted
        {
            get;
            set;
        }

        /// <summary>
        /// 每行控件个数
        /// </summary>
        public int CountForEachRow
        {
            get;
            set;
        }

        /// <summary>
        /// 标签宽度
        /// </summary>
        public int LabelWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 标签字符数
        /// </summary>
        public int CharacterCountOnLabel
        {
            get;
            set;
        }

        /// <summary>
        /// 创建分割符
        /// </summary>
        public bool SeparatorControlCreated
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="formCompleted"></param>
        /// <param name="countForEachRow"></param>
        /// <param name="labelWidth"></param>
        /// <param name="characterCountOnLabel"></param>
        /// <param name="separatorControlCreated"></param>
        public FormShowStyleSetting(bool formCompleted, int countForEachRow, int labelWidth, int characterCountOnLabel, bool separatorControlCreated)
        {
            FormCompleted = formCompleted;
            CountForEachRow = countForEachRow;
            LabelWidth = labelWidth;
            CharacterCountOnLabel = characterCountOnLabel;
            SeparatorControlCreated = separatorControlCreated;
        }

        #endregion
    }
}
