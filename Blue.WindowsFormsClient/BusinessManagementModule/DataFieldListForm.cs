using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary.Utility;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class DataFieldListForm : Form
    {
        #region 契约属性

        /// <summary>
        /// 数据字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set;
            get;
        }

        #endregion

        #region 赋值属性

        /// <summary>
        /// 数据表编号
        /// </summary>
        public decimal TableId
        {
            get;
            set;
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DataFieldFilter DataFieldFilter
        {
            get;
            set;
        }

        #endregion

        #region 委托属性

        /// <summary>
        /// 获得字段
        /// </summary>
        public GetDataFieldDelegate GetDataFields
        {
            set;
            get;
        }


        #endregion

        #region 构造函数

        public DataFieldListForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldListForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            foreach (object obj in lstDataFields.Items)
            {
                CommonNode commonNode = obj as CommonNode;
                commonNodes.Add(commonNode);
            }
            if (GetDataFields != null)
            {
                GetDataFields(commonNodes);
            }
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<CommonNode> commonNodes = CustomDataFieldContract.GetCommonNodes(TableId, DataFieldFilter);
            IList<CommonNode> nodes = new List<CommonNode>();
            foreach (CommonNode commonNode in commonNodes)
            {
                bool add = true;
                foreach (object obj in lstDataFields.Items)
                {
                    CommonNode node = obj as CommonNode;
                    if (node.NodeId == commonNode.NodeId && node.NodeType == commonNode.NodeType)
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    nodes.Add(commonNode);
                }
            }
            CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
            frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
            {
                lstDataFields.Items.AddRange(selectedNodes.ToArray());
                lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
            };
            frmCheckedSelectedItems.LoadAndSetCommonNodes(nodes);
            frmCheckedSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 移除字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDataFields.SelectedIndex >= 0)
            {                
                lstDataFields.Items.RemoveAt(lstDataFields.SelectedIndex);
                if (lstDataFields.SelectedIndex > 0)
                {
                    lstDataFields.SelectedIndex -= 1;
                }            
                lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
            }
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToTop(lstDataFields);
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToPrevious(lstDataFields);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToNext(lstDataFields);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBottom_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToBottom(lstDataFields);
        }

        #endregion

        #region 私有方法

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载并设置字段
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadAndSetDataFields(IList<CommonNode> commonNodes)
        {            
            if (commonNodes != null && commonNodes.Count > 0)
            {
                lstDataFields.Items.AddRange(commonNodes.ToArray());
            }
            lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
        }

        #endregion       
    }
}
