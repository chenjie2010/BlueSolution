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
    public partial class CheckedSelectedItemsForm : Form
    {
        #region 属性

        /// <summary>
        /// 选择节点
        /// </summary>
        public MultiNodeSelectedDelegate MultiNodeSelected
        {
            set;
            get;
        }

        /// <summary>
        /// 设置全选，默认不可见
        /// </summary>
        public bool SelectAllItemVisible
        {
            set
            {
                ccmbItems.Properties.SelectAllItemVisible = value;
            }
            get
            {
                return ccmbItems.Properties.SelectAllItemVisible;
            }
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckedSelectedItemsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownSelectedItemsForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MultiNodeSelected != null)
            {
                IList<CommonNode> commonNodes = new List<CommonNode>();
                List<object> checkedValues = ccmbItems.Properties.Items.GetCheckedValues();
                foreach (object obj in checkedValues)
                {
                    CommonNode commonNode = (CommonNode)obj;
                    commonNodes.Add(commonNode);
                }
                MultiNodeSelected(commonNodes);
            }
            this.Close();
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

        #region 公有方法

        /// <summary>
        /// 加载下拉选择节点
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadCommonNodes(IList<CommonNode> commonNodes)
        {
            ccmbItems.Properties.Items.AddRange(commonNodes.ToArray());
        }


        /// <summary>
        /// 加载并设置下拉选择节点
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadAndSetCommonNodes(IList<CommonNode> commonNodes)
        {
            if (commonNodes == null)
            {
                return;
            }
            ccmbItems.Properties.Items.AddRange(commonNodes.ToArray());
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem checkedListBoxItem in ccmbItems.Properties.Items)
            {
                CommonNode commonNode = checkedListBoxItem.Value as CommonNode;
                if (!commonNode.IsLeaf)
                {
                    checkedListBoxItem.CheckState = CheckState.Checked;
                }
            }
        }

        #endregion
    }
}
