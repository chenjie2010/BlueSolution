using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;

namespace Blue.WindowsFormsClient
{
    public partial class CommonListControl : UserControl
    {
        #region 定义"增加项"事件

        private event EventHandler<EventArgs> _AddItem;

        /// <summary>
        /// 定义"增加项"事件访问器
        /// </summary>
        [
        Description("增加项"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> AddItem
        {
            add
            {
                _AddItem += value;
            }
            remove
            {
                _AddItem -= value;
            }
        }

        /// <summary>
        /// 定义"增加项"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void Add(EventArgs e)
        {
            if (_AddItem != null) _AddItem(this, e);
        }

        #endregion

        #region 定义"移除项"事件

        private event EventHandler<CommonNodeEventArgs> _RemoveItem;

        /// <summary>
        /// 定义"移除项"事件访问器
        /// </summary>
        [
        Description("移除项"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<CommonNodeEventArgs> RemoveItem
        {
            add
            {
                _RemoveItem += value;
            }
            remove
            {
                _RemoveItem -= value;
            }
        }

        /// <summary>
        /// 定义"移除项"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void Remove(CommonNodeEventArgs e)
        {
            if (_RemoveItem != null) _RemoveItem(this, e);
        }

        #endregion

        #region 定义"移动项"事件

        private event EventHandler<MovedItemEventArgs> _MoveItem;

        /// <summary>
        /// 定义"移动项"事件访问器
        /// </summary>
        [
        Description("移动项"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<MovedItemEventArgs> MoveItem
        {
            add
            {
                _MoveItem += value;
            }
            remove
            {
                _MoveItem -= value;
            }
        }

        /// <summary>
        /// 定义"移动项"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void MoveSelectedItem(MovedItemEventArgs e)
        {
            if (_MoveItem != null) _MoveItem(this, e);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 列表项
        /// </summary>
        public ListBoxItemCollection Items
        {
            get
            {
                return lstItems.Items;
            }
        }

            #endregion

        #region 构造函数

        public CommonListControl()
        {
            InitializeComponent();            
        }

        #endregion

        #region 窗体和控件的方法

        private void ListControl_Load(object sender, EventArgs e)
        {
            SetMoveButtonStates();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            Add(e);            
            SetMoveButtonStates();
        }

        /// <summary>
        /// 移除选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex >= 0 && lstItems.SelectedIndex < lstItems.Items.Count)
            {
                lstItems.Items.RemoveAt(lstItems.SelectedIndex);
            }
            CommonNode commonNode = lstItems.Items[lstItems.SelectedIndex] as CommonNode;
            if (commonNode != null)
            {
                CommonNodeEventArgs args = new CommonNodeEventArgs(commonNode);
                Remove(args);
            };
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            MoveNode(MovedDriection.Top, lstItems);
        }

        /// <summary>
        /// 上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            MoveNode(MovedDriection.Previous, lstItems);
        }
        
        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveNode(MovedDriection.Next, lstItems);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBottom_Click(object sender, EventArgs e)
        {
            MoveNode(MovedDriection.Bottom, lstItems);
        }

        /// <summary>
        /// 选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMoveButtonStates();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            lstItems.Items.Clear();
        }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            lstItems.Enabled = !readOnly;
            btnAdd.Enabled = !readOnly;
            btnRemove.Enabled = !readOnly;
            btnTop.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnBottom.Enabled = false;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="movedDriection"></param>
        /// <param name="lstItems"></param>
        private void MoveNode(MovedDriection movedDriection, ListBoxControl lstItems)
        {
            if (lstItems.SelectedIndex < 0 || lstItems.SelectedIndex >= lstItems.Items.Count)
            {
                return;
            }

            switch (movedDriection)
            {
                case MovedDriection.Top:
                    if (lstItems.SelectedIndex == 0)
                    {
                        return;
                    }
                    object objTop = lstItems.Items[lstItems.SelectedIndex];
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                    lstItems.Items.Insert(0, objTop);
                    lstItems.SelectedIndex = 0;
                    break;

                case MovedDriection.Previous:
                    if (lstItems.SelectedIndex == 0)
                    {
                        return;
                    }
                    int previousIndex = lstItems.SelectedIndex;
                    object objPrevious = lstItems.Items[lstItems.SelectedIndex];
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                    if (previousIndex == lstItems.Items.Count)
                    {
                        lstItems.Items.Insert(lstItems.SelectedIndex, objPrevious);
                    }
                    else
                    {
                        lstItems.Items.Insert(lstItems.SelectedIndex - 1, objPrevious);
                    }
                    lstItems.SelectedIndex = previousIndex - 1;
                    break;

                case MovedDriection.Next:
                    if (lstItems.SelectedIndex == lstItems.Items.Count - 1)
                    {
                        return;
                    }
                    int nextIndex = lstItems.SelectedIndex;
                    object objNext = lstItems.Items[lstItems.SelectedIndex];
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                    lstItems.Items.Insert(lstItems.SelectedIndex + 1, objNext);
                    lstItems.SelectedIndex = nextIndex + 1;
                    break;

                case MovedDriection.Bottom:
                    if (lstItems.SelectedIndex == lstItems.Items.Count - 1)
                    {
                        return;
                    }
                    object objBottom = lstItems.Items[lstItems.SelectedIndex];
                    lstItems.Items.RemoveAt(lstItems.SelectedIndex);
                    lstItems.Items.Add(objBottom);
                    lstItems.SelectedIndex = lstItems.Items.Count - 1;
                    break;
            }
            MovedItemEventArgs movedItemEventArgs = new MovedItemEventArgs(movedDriection);
            MoveSelectedItem(movedItemEventArgs);
        }

        /// <summary>
        /// 设置按钮状态
        /// </summary>
        private void SetMoveButtonStates()
        {
            if (lstItems.SelectedIndex < 0 && lstItems.Items.Count > 0)
            {
                lstItems.SelectedIndex = 0;
            }
            if ((lstItems.Items.Count > 1) && ((lstItems.SelectedIndex > 0) && (lstItems.SelectedIndex < lstItems.Items.Count - 1)))
            {
                btnTop.Enabled = true;
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnBottom.Enabled = true;
            }
            else if ((lstItems.Items.Count > 1) && ((lstItems.SelectedIndex == 0) || (lstItems.SelectedIndex == lstItems.Items.Count - 1)))
            {
                if (lstItems.SelectedIndex == 0)
                {
                    btnTop.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                }

                if (lstItems.SelectedIndex == lstItems.Items.Count - 1)
                {
                    btnTop.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                }
            }
            else
            {
                btnTop.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnBottom.Enabled = false;
            }
        }

        #endregion      
    }
}
