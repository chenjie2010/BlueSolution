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
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class QueryItemsForm : Form
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomQueyContract customQueyContract;

        #endregion


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


        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryItemsForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableSelectedItemsForm_Load(object sender, EventArgs e)
        {
            queryDropdownList.CustomGroupContract = customGroupContract;
            queryDropdownList.CustomQueyContract = customQueyContract;
            queryDropdownList.InitCommonNodeContract(customGroupContract);
            queryDropdownList.LoadData();
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
                NodeSelected(queryDropdownList.SelectedNode);
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
            queryDropdownList.Value = null;
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
