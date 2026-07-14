using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;

namespace Blue.WindowsFormsClient
{
    public partial class ExtendedTreeSelectedItemsForm : Form
    {
        #region 属性

        /// <summary>
        /// 通用的下拉选择项操作接口
        /// </summary>
        public ITreeDropdownHandler TreeDropdownHandler
        {
            get
            {
                return treeDropdownList.TreeDropdownHandler;
            }
            set
            {
                treeDropdownList.TreeDropdownHandler = value;
            }
        }

        /// <summary>
        /// 节点操作接口契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            get; set;
        }


        /// <summary>
        /// 编号
        /// </summary>
        public decimal ParentNodeId
        {
            set;
            get;
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        public NodeSelectedDelegate NodeSelected
        {
            set;
            get;
        }

        /// <summary>
        /// 提示
        /// </summary>
        public string ToolTip
        {
            get
            {
                return gcMain.Text;
            }
            set
            {
                gcMain.Text = value;
            }
        }

        /// <summary>
        /// 只能选择叶子节点
        /// </summary>
        public bool OnlySelectedLeaf
        {
            set
            {
                treeDropdownList.OnlySelectedLeaf = value;
            }
            get
            {
                return treeDropdownList.OnlySelectedLeaf;
            }
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExtendedTreeSelectedItemsForm()
        {
            InitializeComponent();
            ParentNodeId = decimal.MinValue;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeSelectedItemsForm_Load(object sender, EventArgs e)
        {
            treeDropdownList.InitalizeTreeView();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (NodeSelected != null)
            {
                NodeSelected(treeDropdownList.SelectedNode);
            }
            this.Close();
        }

        /// <summary>
        /// 移除选择的节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDropdownList_OnNodeRemoved(object sender, EventArgs e)
        {
            if (NodeSelected != null)
            {
                NodeSelected(null);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion        
    }
}
