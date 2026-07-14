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
    public partial class TreeSelectedItemsForm : Form
    {
        #region 属性

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
        /// 节点类型
        /// </summary>
        public byte NodeType
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
        /// 移除节点
        /// </summary>
        public NodeRemovedDelegate NodeRemoved
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

        /// <summary>
        /// 显示搜索
        /// </summary>
        public bool ShowSearch
        {
            set
            {
                treeDropdownList.ShowSearch = value;
            }
            get
            {
                return treeDropdownList.ShowSearch;
            }
        }
        
        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeSelectedItemsForm()
        {
            InitializeComponent();
            NodeType = 0;
            ParentNodeId = decimal.MinValue;
            ShowSearch = false;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeSelectedItemsForm_Load(object sender, EventArgs e)
        {
            if (NodeType > 0)
            {
                treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(CommonNodeContract, ParentNodeId, NodeType);
            }
            else
            {
                treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(CommonNodeContract, ParentNodeId);
            }
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
            if (NodeRemoved != null)
            {
                NodeRemoved();
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
