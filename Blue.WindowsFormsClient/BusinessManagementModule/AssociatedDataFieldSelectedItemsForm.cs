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
    public partial class AssociatedDataFieldSelectedItemsForm : Form
    {
        #region 属性

        /// <summary>
        /// 选择节点
        /// </summary>
        public NodeSelectedDelegate NodeSelected
        {
            set;
            get;
        }

        /// <summary>
        /// 关联字段类型
        /// </summary>
        public AssociatedDataFieldCategory AssociatedDataFieldCategory
        {
            get
            {
                return dropdownList.AssociatedDataFieldCategory;
            }
            set
            {
                dropdownList.AssociatedDataFieldCategory = value;
            }
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociatedDataFieldSelectedItemsForm()
        {
            InitializeComponent();
            dropdownList.CustomGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            dropdownList.CustomAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            dropdownList.AssociatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedDataFieldSelectedItemsForm_Load(object sender, EventArgs e)
        {
            dropdownList.LoadData();
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
                NodeSelected(dropdownList.SelectedNode);
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
            dropdownList.Value = null;
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
