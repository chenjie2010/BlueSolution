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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient
{
    public partial class CommonListItemsForm : Form
    {

        #region 属性

        /// <summary>
        /// 通用节点契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 内容提示
        /// </summary>
        public string ToolTip
        {
            get
            {
                return gcItems.Text;
            }
            set
            {
                gcItems.Text = value;
            }
        }

        #region 委托属性

        /// <summary>
        /// 获得列表项
        /// </summary>
        public GetItemsDelegate GetItems
        {
            set;
            get;
        }

        /// <summary>
        /// 创建列表项
        /// </summary>
        public CreateItmesDelegate CreateItmes
        {
            get;
            set;
        }

        /// <summary>
        /// 移除列表项
        /// </summary>
        public RemoveItmeDelegate RemoveItem
        {
            get;
            set;
        }


        #endregion


        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonListItemsForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件的方法 

        /// <summary>
        /// 窗体的加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonListItemsForm_Load(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CreateItmes != null)
            {
                CreateItmes(lstItems);
            }
        }

        private void bbiRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstItems.SelectedIndex >= 0)
            {
                CommonNode node = lstItems.Items[lstItems.SelectedIndex] as CommonNode;
                RemoveItem?.Invoke(node.NodeId);
                lstItems.Items.RemoveAt(lstItems.SelectedIndex);                
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            foreach (object obj in lstItems.Items)
            {
                CommonNode commonNode = obj as CommonNode;
                commonNodes.Add(commonNode);
            }
            if (GetItems != null)
            {
                GetItems(commonNodes);
            }
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToTop(lstItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToPrevious(lstItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToNext(lstItems);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBottom_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToBottom(lstItems);
        }

        #region 公有方法

        /// <summary>
        /// 加载并设置字段
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadItems(IList<CommonNode> commonNodes)
        {
            if (commonNodes != null && commonNodes.Count > 0)
            {
                lstItems.Items.AddRange(commonNodes.ToArray());
            }
        }

        #endregion
    }
}
