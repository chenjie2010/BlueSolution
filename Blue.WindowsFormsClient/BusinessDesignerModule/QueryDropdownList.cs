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
    public class QueryDropdownList : ComoboxTreeview
    {
        #region 契约

        /// <summary>
        /// 分组
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询
        /// </summary>
        public ICustomQueyContract CustomQueyContract
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
        public QueryDropdownList()
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
        private void QueryDropdownList_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId);                    
                    TreeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 树形结构展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 选择节点前检查是否是表被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
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
                IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.Query);
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
            /* 第一层为大类，第二层为小类，第三层为查询 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    commonNodeContract = CustomGroupContract;
                    break;

                case CombinedNodeType.Leaf:
                    commonNodeContract = CustomQueyContract;
                    break;

                default:
                    throw new ArgumentException("不支持该查询节点类型。");
            }
        }

        /// <summary>
        /// 获得视图节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType nodeType = CombinedNodeType.Root;

            /* 第一层为大类，第二层为小类，第三层为视图 */
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
            // QueryDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "QueryDropdownList";
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.QueryDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.QueryDropdownList_AfterTreeNodeExpand);
            this.BeforeTreeNodeSelect += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.QueryDropdownList_BeforeTreeNodeSelect);
            this.Load += new System.EventHandler(this.QueryDropdownList_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
    }
}
