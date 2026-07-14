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
    public class PrintDropdownList : ComoboxTreeview
    {
        #region 契约属性

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 打印契约
        /// </summary>
        public ICustomPrintContract CustomPrintContract
        {
            get;
            set;
        }

        #endregion

        #region 私有变量

        private ICommonNodeContract commonNodeContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PrintDropdownList()
        {
            InitializeComponent();
            OnlySelectedLeaf = true;
            ShowSearch = false;
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDropdownList_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    TreeNodeType treeNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId);
                    if (treeNodeType == TreeNodeType.Leaf)
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
        private void PrintDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否打印被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetNodeType(e.Node.Level + 1);
            if (treeNodeType != TreeNodeType.Leaf)
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
                IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.Print);
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        private void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* 第一层为分类，第二层为打印 */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    commonNodeContract = CustomGroupContract;
                    break;

                case TreeNodeType.Category:
                    commonNodeContract = CustomPrintContract;
                    break;

                default:
                    commonNodeContract = null;
                    break;
            }
        }

        /// <summary>
        /// 获得视图节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeNodeType GetNodeType(int level)
        {
            TreeNodeType nodeType = TreeNodeType.Root;

            /* 第一层为分类，第二层为打印 */
            switch (level)
            {
                case 0:
                    nodeType = TreeNodeType.Root;
                    break;

                case 1:
                    nodeType = TreeNodeType.Category;
                    break;

                case 2:
                    nodeType = TreeNodeType.Leaf;
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
            // PrintDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PrintDropdownList";
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.PrintDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.PrintDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.PrintDropdownList_BeforeTreeNodeSelect);
            this.Load += new System.EventHandler(this.PrintDropdownList_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
    }
}
