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
    /// 关联字段类
    /// </summary>
    public class AssociatedDataFieldDropdownList : ComoboxTreeview
    {
        #region 契约接口

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 关联表契约
        /// </summary>
        public ICustomAssociationContract CustomAssociationContract
        {
            get;
            set;
        }

        /// <summary>
        /// 关联字段契约
        /// </summary>
        public IAssociatedDataFieldContract AssociatedDataFieldContract
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
        /// 关联字段类型
        /// </summary>
        public AssociatedDataFieldCategory AssociatedDataFieldCategory
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociatedDataFieldDropdownList()
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
        private void AssociatedDataFieldDropdownList_Load(object sender, EventArgs e)
        {
            commonNodeContract = CustomGroupContract;
        }

        /// <summary>
        /// 树形结构展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedDataFieldDropdownList_BeforeTreeNodeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level+1));
        }
       
        /// <summary>
        /// 树形结构展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedDataFieldDropdownList_AfterTreeNodeExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    AssociationNodeType associationNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;
                    if (associationNodeType == AssociationNodeType.AssociationDataField)
                    {
                        commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId, (byte)AssociatedDataFieldCategory);
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
        /// 选择节点前检查是否是关联字段被选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedDataFieldDropdownList_BeforeTreeNodeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            AssociationNodeType associationNodeType = GetNodeType(e.Node.Level);
            if (associationNodeType != AssociationNodeType.AssociationDataField)
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
                IList<CommonNode> commonNodes = commonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.Association);
                TreeViewHandler.InitFirstLevelNodes(commonNodes);
            }
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="associationNodeType"></param>
        private void SetCommonNodeContract(AssociationNodeType associationNodeType)
        {
            /* 第一层为大分类节点，第二为小分类节点，第三层为关联，第四层为关联字段 */
            switch (associationNodeType)
            {
                case AssociationNodeType.ParentCategory:
                case AssociationNodeType.ChildCategory:
                    commonNodeContract = CustomGroupContract;
                    break;

                case AssociationNodeType.AssociationName:
                    commonNodeContract = CustomAssociationContract;
                    break;

                case AssociationNodeType.AssociationDataField:
                    commonNodeContract = AssociatedDataFieldContract;
                    break;
            }
        }


        /// <summary>
        /// 获得树形结构节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private AssociationNodeType GetNodeType(int level)
        {
            AssociationNodeType associationNodeType = AssociationNodeType.Root;

            /* 第一层为大分类节点，第二为小分类节点，第三层为关联，第四层为关联字段 */
            switch (level)
            {
                case 0:
                    associationNodeType = AssociationNodeType.ParentCategory;
                    break;

                case 1:
                    associationNodeType = AssociationNodeType.ChildCategory;
                    break;

                case 2:
                    associationNodeType = AssociationNodeType.AssociationName;
                    break;

                case 3:
                    associationNodeType = AssociationNodeType.AssociationDataField;
                    break;
            }

            return associationNodeType;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AssociatedDataFieldDropdownList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "AssociatedDataFieldDropdownList";
            this.BeforeTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewCancelEventArgs>(this.AssociatedDataFieldDropdownList_BeforeTreeNodeExpand);
            this.AfterTreeNodeExpand += new System.EventHandler<System.Windows.Forms.TreeViewEventArgs>(this.AssociatedDataFieldDropdownList_AfterTreeNodeExpand);
            this.Load += new System.EventHandler(this.AssociatedDataFieldDropdownList_Load);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
