//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ListItemHandler.cs
// 描述:  列表框项移动
// 作者：ChenJie 
// 编写日期：2016-10-25
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using DevExpress.XtraEditors;

namespace AppFramework.WinFormsLibrary.Utility
{
    /// <summary>
    /// 列表框项移动
    /// </summary>
    public sealed class ListItemHandler
    {
        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="lstBox"></param>
        public static void MoveToTop(ListBoxControl lstBox)
        {
            if (lstBox.SelectedIndex > 0)
            {
                object item = lstBox.SelectedItem;
                lstBox.Items.RemoveAt(lstBox.SelectedIndex);
                lstBox.Items.Insert(0, item);
                lstBox.SelectedItem = item;
            }
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="lstBox"></param>
        public static void MoveToPrevious(ListBoxControl lstBox)
        {
            if (lstBox.SelectedIndex > 0)
            {
                int index = lstBox.SelectedIndex;
                object item = lstBox.SelectedItem;
                lstBox.Items.RemoveAt(lstBox.SelectedIndex);
                lstBox.Items.Insert(index - 1, item);
                lstBox.SelectedItem = item;
            }
        }


        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="lstBox"></param>
        public static void MoveToNext(ListBoxControl lstBox)
        {
            if (lstBox.SelectedIndex >= 0 && lstBox.SelectedIndex < lstBox.Items.Count - 1)
            {
                int index = lstBox.SelectedIndex;
                object item = lstBox.SelectedItem;
                lstBox.Items.RemoveAt(lstBox.SelectedIndex);
                lstBox.Items.Insert(index + 1, item);
                lstBox.SelectedItem = item;                
            }
        }


        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="lstBox"></param>
        public static void MoveToBottom(ListBoxControl lstBox)
        {
            if (lstBox.SelectedIndex >= 0 && lstBox.SelectedIndex < lstBox.Items.Count - 1)
            {
                object item = lstBox.SelectedItem;
                lstBox.Items.RemoveAt(lstBox.SelectedIndex);
                lstBox.Items.Add(item);
                lstBox.SelectedItem = item;                
            }
        }

        /// <summary>
        /// 刷新文本显示
        /// </summary>
        /// <param name="lstBox"></param>
        public static void RefreshItemText(ListBoxControl lstBox)
        {
            for (int i = 0; i < lstBox.Items.Count; i++)
            {
                CommonNode commonNode = lstBox.Items[i] as CommonNode;
                int pos = commonNode.NodeName.LastIndexOf('}');
                if (pos > 0)
                {
                    commonNode.NodeName = string.Format("{{{0}}}{1}", i, commonNode.NodeName.Substring(pos + 1));
                    lstBox.Items[i] = commonNode;
                }
            }
        }
    }
}
