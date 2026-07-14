//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFieldDropdownList.cs
// 描述: 字段类
// 作者：ChenJie 
// 编写日期：2016-08-23
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary.Utility;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 表类
    /// </summary>
    public class DataTableDropdownList : ComoboxTreeview
    {
        #region 契约接口

        private ICustomDatabaseContract _customDatabaseContract;

        #endregion

        #region 契约属性

        /// <summary>
        /// 
        /// </summary>
        public ICustomDatabaseContract CustomDatabaseContract
        {
            get
            {
                return _customDatabaseContract;
            }
            set
            {
                _customDatabaseContract = value;
                commonNodeContract = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomCategoryContract CustomCategoryContract
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            set;
            get;
        }

        #endregion

        #region 私有变量

        private ICommonNodeContract commonNodeContract;

        #endregion

        #region 属性

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public byte DataWarehouseId
        {
            set;
            get;
        }

        /// <summary>
        /// 表类型过滤
        /// </summary>
        public TableFilter TableFilter
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableDropdownList()
        {
            InitializeComponent();            
            OnlySelectedLeaf = true;
            ShowSearch = false;
            DataWarehouseId = 0;
            TableFilter = TableFilter.All;
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableDropdownList_Load(object sender, EventArgs e)
        {
            commonNodeContract = CustomDatabaseContract;
        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;
                    if (databaseNodeType == DatabaseNodeType.Table)
                    {
                        commonNodes = CustomTableContract.GetCommonNodes(commonNode.NodeId, TableFilter);
                        foreach (CommonNode node in commonNodes)
                        {
                            node.IsLeaf = true;
                        }
                    }
                    else
                    {
                        commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId);
                    }
                    TreeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }            
        }

        /// <summary>
        /// 树形结构展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否是表被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level);
            if (databaseNodeType != DatabaseNodeType.Table)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            if (!DesignMode && commonNodeContract != null)
            {
                IList<CommonNode> commonNodes = null;
                if (DataWarehouseId > 0)
                {
                    commonNodes = commonNodeContract.GetChildNodes(DataWarehouseId);
                }
                else
                {
                    commonNodes = new List<CommonNode>();
                    IList<EnumItem> dataWarehouses = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
                    foreach (EnumItem enumItem in dataWarehouses)
                    {
                        commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, string.Empty, false));
                    }
                }
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
            }
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="databaseNode"></param>
        private void SetCommonNodeContract(DatabaseNodeType databaseNode)
        {
            /* 第一层为分组，第二层为数据表 */
            switch (databaseNode)
            {
                case DatabaseNodeType.Database:
                    commonNodeContract = CustomDatabaseContract;
                    break;

                case DatabaseNodeType.Category:
                    commonNodeContract = CustomCategoryContract;
                    break;

                case DatabaseNodeType.Table:
                    commonNodeContract = CustomTableContract;
                    break;

                default:
                    commonNodeContract = null;
                    break;
            }
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;

            if (DataWarehouseId > 0)
            {
                /* 第一层为数据库，第二层为分组，第三层为数据表 */
                switch (level)
                {
                    case 0:
                        nodeType = DatabaseNodeType.Database;
                        break;

                    case 1:
                        nodeType = DatabaseNodeType.Category;
                        break;

                    case 2:
                        nodeType = DatabaseNodeType.Table;
                        break;
                }
            }
            else
            {
                /* 第一层为数据仓库，第二层为数据库，第三层为分组，第四层为数据表 */
                switch (level)
                {
                    case 0:
                        nodeType = DatabaseNodeType.DataWarehouse;
                        break;

                    case 1:
                        nodeType = DatabaseNodeType.Database;
                        break;

                    case 2:
                        nodeType = DatabaseNodeType.Category;
                        break;

                    case 3:
                        nodeType = DatabaseNodeType.Table;
                        break;
                }
            }

            return nodeType;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DataTableDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "DataTableDropdownList";
            this.OnlySelectedLeaf = true;
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.DataTableDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.DataTableDropdownList_AfterTreeNodeExpand);
            this.Load += new System.EventHandler(this.DataTableDropdownList_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
