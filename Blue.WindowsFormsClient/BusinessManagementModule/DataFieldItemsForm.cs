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
    public partial class DataFieldItemsForm : Form
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
        /// 选择节点
        /// </summary>
        public MultiNodeSelectedDelegate MultiNodeSelected
        {
            set;
            get;
        }

        /// <summary>
        /// 是否允许选择多个节点
        /// </summary>
        public bool MultiNodeAllowed
        {
            set
            {
                dataFieldDropdownList.CheckBoxes = value;
            }
            get
            {
                return dataFieldDropdownList.CheckBoxes;
            }
        }

        /// <summary>
        /// 字段展现模式
        /// </summary>
        public DataFieldShowMode DataFieldShowMode
        {
            set
            {
                dataFieldDropdownList.DataFieldShowMode = value;
            }
            get
            {
                return dataFieldDropdownList.DataFieldShowMode;
            }
        }

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public decimal DataWarehouseId
        {
            set
            {
                dataFieldDropdownList.DataWarehouseId = value;
            }
            get
            {
                return dataFieldDropdownList.DataWarehouseId;
            }
        }

        /// <summary>
        /// 逻辑数据库编号
        /// </summary>
        public decimal DatabaseId
        {
            set
            {
                dataFieldDropdownList.DatabaseId = value;
            }
            get
            {
                return dataFieldDropdownList.DatabaseId;
            }
        }

        /// <summary>
        /// 按条件加载字段
        /// </summary>
        public DataFieldFilter DataFieldFilter
        {
            set
            {
                dataFieldDropdownList.DataFieldFilter = value;
            }
            get
            {
                return dataFieldDropdownList.DataFieldFilter;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public byte DataFieldType
        {
            set
            {
                dataFieldDropdownList.DataFieldType = value;
            }
            get
            {
                return dataFieldDropdownList.DataFieldType;
            }
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFieldItemsForm()
        {
            InitializeComponent();
            dataFieldDropdownList.CustomDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            dataFieldDropdownList.CustomCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            dataFieldDropdownList.CustomTableContract = BusinessChannelFactory.CreateCustomTableContract();
            dataFieldDropdownList.CustomDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            MultiNodeAllowed = false;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldSelectedItemsForm_Load(object sender, EventArgs e)
        {
            dataFieldDropdownList.LoadData();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (MultiNodeAllowed)
            {
                if (MultiNodeSelected != null)
                {
                    IList<CommonNode> commonNodes = new List<CommonNode>();
                    foreach (TreeNode tn in dataFieldDropdownList.CheckedTreeNodes)
                    {
                        if (tn.Nodes.Count == 0)
                        {
                            CommonNode commonNode = tn.Tag as CommonNode;
                            commonNodes.Add(commonNode);
                        }
                    }
                    MultiNodeSelected(commonNodes);
                }
            }
            else
            {
                if (NodeSelected != null)
                {
                    NodeSelected(dataFieldDropdownList.SelectedNode);
                }
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
            dataFieldDropdownList.Value = null;
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
