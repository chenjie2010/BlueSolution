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
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 表类
    /// </summary>
    public class ReportDropdownList : ComoboxTreeview
    {
        #region 契约

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 报表契约
        /// </summary>
        public ICustomReportContract CustomReportContract
        {
            get; set;
        }        

        #endregion

        #region 私有变量

        private ICommonNodeContract commonNodeContract;

        #endregion       

        #region 属性

        /// <summary>
        /// 报表分类
        /// </summary>
        public ReportCategory ReportCategory
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportDropdownList()
        {
            InitializeComponent();            
            OnlySelectedLeaf = true;
            ShowSearch = false;
            ReportCategory = ReportCategory.Query;
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportDropdownList_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;
                    if (combinedNodeType == CombinedNodeType.Leaf)
                    {
                        commonNodes = CustomReportContract.GetCommonNodes(commonNode.NodeId, ReportCategory);
                    }
                    else
                    {
                        commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId);
                    }
                    
                    if (combinedNodeType == CombinedNodeType.Leaf)
                    {
                        foreach (CommonNode node in commonNodes)
                        {
                            node.IsLeaf = true;
                        }
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
        private void ReportDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否是表被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);
            if (combinedNodeType != CombinedNodeType.Leaf)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化通用节点契约
        /// </summary>
        /// <param name="customGroupContract"></param>
        public void InitCommonNodeContract(ICustomGroupContract customGroupContract)
        {
            commonNodeContract = customGroupContract;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            if (!DesignMode && commonNodeContract != null)
            {               
                IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.QueryReport);
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetCommonNodeContract(CombinedNodeType combinedNodeType)
        {
            /* 第一层为分组，第二层为数据表 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    commonNodeContract = CustomGroupContract;
                    break;

                case CombinedNodeType.Leaf:
                    commonNodeContract = CustomReportContract;
                    break;
                    
                default:
                    commonNodeContract = null;
                    break;
            }
        }

        /// <summary>
        /// 获得报表节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType nodeType = CombinedNodeType.ParentCategory;

            /* 第一层为大类分组，第二层为小类分组，第三层为报表 */
            switch (level)
            {
                case 0:
                    nodeType = CombinedNodeType.ParentCategory;
                    break;

                case 1:
                    nodeType = CombinedNodeType.ChildCategory;
                    break;

                case 2:
                    nodeType = CombinedNodeType.Leaf;
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
            // ReportDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ReportDropdownList";
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.ReportDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.ReportDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += ReportDropdownList_BeforeTreeNodeSelect;
            this.Load += new System.EventHandler(this.ReportDropdownList_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
