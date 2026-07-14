//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserQueryDropdownList.cs
// 描述: 用户查询类
// 作者：ChenJie 
// 编写日期：2019-06-10
// 版权所有 (C) 四川大学 2019
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
    public class UserQueryDropdownList : ComoboxTreeview
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
        /// 用户查询契约
        /// </summary>
        public IUserQueryContract UserQueryContract
        {
            get; set;
        }        

        #endregion

        #region 私有变量

        private ICommonNodeContract commonNodeContract;

        #endregion       

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserQueryDropdownList()
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
        private void UserQueryDropdownList_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserQueryDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    TreeNodeType treeNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId, CurrentUser.Instance.UserId);
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
        private void UserQueryDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否是表被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserQueryDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetNodeType(e.Node.Level);
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
                IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.QueryStatement, CurrentUser.Instance.UserId);
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* 第一层为分组，第二层为数据查询 */
            switch (treeNodeType)
            {                
                case TreeNodeType.Category:
                    commonNodeContract = CustomGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    commonNodeContract = UserQueryContract;
                    break;
                    
                default:
                    commonNodeContract = null;
                    break;
            }
        }

        /// <summary>
        /// 获得用户查询节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeNodeType GetNodeType(int level)
        {
            TreeNodeType nodeType = TreeNodeType.Root;

            /* (1) 第一层为分类 (2) 第二层为用户查询  */
            switch (level)
            {
                case 0:
                    nodeType = TreeNodeType.Category;
                    break;

                case 1:
                    nodeType = TreeNodeType.Leaf;
                    break;

                default:
                    throw new ArgumentException("不支持节点类型。");

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
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.UserQueryDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.UserQueryDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += UserQueryDropdownList_BeforeTreeNodeSelect;
            this.Load += new System.EventHandler(this.UserQueryDropdownList_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
