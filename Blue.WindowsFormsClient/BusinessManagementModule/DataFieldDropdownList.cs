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
    /// 字段类
    /// </summary>
    public class DataFieldDropdownList : ComoboxTreeview
    {
        #region 契约

        public ICustomDatabaseContract CustomDatabaseContract
        {
            get;
            set;
        }

        public ICustomCategoryContract CustomCategoryContract
        {
            get;
            set;
        }

        public ICustomTableContract CustomTableContract
        {
            get;
            set;
        }

        public ICustomDataFieldContract CustomDataFieldContract
        {
            get;
            set;
        }

        #endregion

        #region 私有变量
        
        private ICommonNodeContract commonNodeContract;

        #endregion

        #region 属性

        /// <summary>
        /// 字段展现模式
        /// </summary>
        public DataFieldShowMode DataFieldShowMode
        {
            set;
            get;
        }

        /// <summary>
        /// 数据仓库编号
        /// </summary>
        public decimal DataWarehouseId
        {
            set;
            get;
        }

        /// <summary>
        /// 逻辑数据库编号
        /// </summary>
        public decimal DatabaseId
        {
            set;
            get;
        }

        /// <summary>
        /// 按条件加载字段
        /// </summary>
        public DataFieldFilter DataFieldFilter
        {
            get;
            set;
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public byte DataFieldType
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFieldDropdownList()
        {
            InitializeComponent();
            OnlySelectedLeaf = true;
            ShowSearch = false;
            DataFieldFilter = DataFieldFilter.All;
            DatabaseId = decimal.MinusOne;
        }

        #endregion

        #region 控件方法
        
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldDropdownList_Load(object sender, EventArgs e)
        {
            switch (DataFieldShowMode)
            {
                case DataFieldShowMode.DataWarehouse:
                    commonNodeContract = CustomDatabaseContract;
                    break;

                case DataFieldShowMode.Database:
                    commonNodeContract = CustomCategoryContract;
                    break;
            }
        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;                   
                    if (databaseNodeType == DatabaseNodeType.DataField)
                    {
                        if (DataFieldFilter == DataFieldFilter.Custom)
                        {
                            commonNodes = CustomDataFieldContract.GetCommonNodes(commonNode.NodeId, DataFieldType);
                        }
                        else
                        {
                            commonNodes = CustomDataFieldContract.GetCommonNodes(commonNode.NodeId, DataFieldFilter);
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
        private void DataFieldDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否是字段被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level);
            if (databaseNodeType != DatabaseNodeType.DataField)
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
            if (commonNodeContract != null)
            {
                IList<CommonNode> commonNodes = null;
                switch (DataFieldShowMode)
                {
                    case DataFieldShowMode.DataWarehouse:
                        commonNodes = commonNodeContract.GetChildNodes(DataWarehouseId);
                        break;

                    case DataFieldShowMode.Database:
                        commonNodes = commonNodeContract.GetChildNodes(DatabaseId);
                        break;

                    default:
                        throw new ArgumentException("参数错误。");
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
            switch (databaseNode)
            {
                case DatabaseNodeType.Database:
                    commonNodeContract = CustomDatabaseContract; ;
                    break;

                case DatabaseNodeType.Category:
                commonNodeContract = CustomCategoryContract;
                    break;

                case DatabaseNodeType.Table:
                commonNodeContract = CustomTableContract;
                    break;

                case DatabaseNodeType.DataField:
                commonNodeContract = CustomDataFieldContract;
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
            switch (DataFieldShowMode)
            {
                case DataFieldShowMode.DataWarehouse:
                    /* 第一层为数据库，第二层为分组，第三层为数据表，第四、五层为字段 */
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

                        case 3:
                        case 4:
                            nodeType = DatabaseNodeType.DataField;
                            break;
                    }
                    break;

                case DataFieldShowMode.Database:
                    /* 第一层为分组，第二层为数据表，第三、四层为字段 */
                    switch (level)
                    {
                        case 0:
                            nodeType = DatabaseNodeType.Category;
                            break;

                        case 1:
                            nodeType = DatabaseNodeType.Table;
                            break;

                        case 2:
                        case 3:
                            nodeType = DatabaseNodeType.DataField;
                            break;
                    }
                    break;
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
            // DataFieldDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "DataFieldDropdownList";
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.DataFieldDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.DataFieldDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += DataFieldDropdownList_BeforeTreeNodeSelect;
            this.Load += new System.EventHandler(this.DataFieldDropdownList_Load);
            this.ResumeLayout(false);

        }


        #endregion

    }
}
