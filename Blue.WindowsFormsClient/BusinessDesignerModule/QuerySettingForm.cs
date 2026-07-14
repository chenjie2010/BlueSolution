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
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class QuerySettingForm : Form
    {
        #region 私有变量

        /// <summary>
        /// 查询对象
        /// </summary>
        private CustomQueyInfo CurrentCustomQueyInfo;

        #endregion

        #region 契约属性

        /// <summary>
        /// 查询契约
        /// </summary>
        public ICustomQueyContract CustomQueyContract
        {
            get; set;
        }

        /// <summary>
        /// 自定义表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;set;
        }
        
        /// <summary>
        /// 数据字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            get; set;
        }

        /// <summary>
        /// 视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get;
            set;
        }

        #endregion

        #region  自定义属性

        /// <summary>
        /// 查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QuerySettingForm()
        {
            InitializeComponent();
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldMode, typeof(DataFieldMode));
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuerySettingForm_Load(object sender, EventArgs e)
        {
            CurrentCustomQueyInfo = CustomQueyContract.GetModelInfo(DataQueriedId);

        }
                
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataFieldShowForm frmDataFieldShow = new DataFieldShowForm();
            frmDataFieldShow.CustomQueyContract = CustomQueyContract;
            frmDataFieldShow.CustomDataFieldContract = CustomDataFieldContract;
            frmDataFieldShow.DataQueriedId = CurrentCustomQueyInfo.DataQueriedId;
            IList<CommonNode> commonNodes = GetDataFields();
            frmDataFieldShow.LoadDataFields(commonNodes);
            frmDataFieldShow.GetCustomQueyAndDataField = delegate (CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
            {
                CustomQueyAndDataFieldInfo oldCustomQueyAndDataFieldInfo = CustomQueyContract.GetCustomQueyAndDataFieldInfo(customQueyAndDataFieldInfo.DataFieldId, customQueyAndDataFieldInfo.DataQueriedId);
                if (oldCustomQueyAndDataFieldInfo != null)
                {
                    MessageBox.Show("该字段已经存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CustomQueyContract.InsertCustomQueyAndDataFieldInfo(customQueyAndDataFieldInfo);
                LoadData();
            };
            frmDataFieldShow.ShowDialog();
            
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDataFields.FocusedRowHandle >= 0)
            {
                decimal dataFieldId = Convert.ToDecimal(gvDataFields.GetRowCellValue(gvDataFields.FocusedRowHandle, "DataFieldId"));
                DataFieldShowForm frmDataFieldShow = new DataFieldShowForm();
                frmDataFieldShow.CustomQueyContract = CustomQueyContract;
                frmDataFieldShow.CustomDataFieldContract = CustomDataFieldContract;
                frmDataFieldShow.DataQueriedId = CurrentCustomQueyInfo.DataQueriedId;
                frmDataFieldShow.DataFieldId = dataFieldId;
                IList<CommonNode> commonNodes = GetDataFields();
                frmDataFieldShow.LoadDataFields(commonNodes);
                frmDataFieldShow.GetCustomQueyAndDataField = delegate (CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo)
                {
                    CustomQueyContract.UpdateCustomQueyAndDataFieldInfo(customQueyAndDataFieldInfo);
                    LoadData();

                };
                frmDataFieldShow.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先选择需要编辑的字段。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvDataFields.FocusedRowHandle >= 0)
            {
                if (MessageBox.Show("确认删除所选择的记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    decimal dataFieldId = Convert.ToDecimal(gvDataFields.GetRowCellValue(gvDataFields.FocusedRowHandle, "DataFieldId"));
                    CustomQueyContract.DeleteCustomQueyAndDataFieldInfo(dataFieldId, CurrentCustomQueyInfo.DataQueriedId);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("请先选择需要删除的记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSorting(MovedDriection.Top);
        }

        /// <summary>
        /// 上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSorting(MovedDriection.Previous);
        }

        /// <summary>
        /// 下一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSorting(MovedDriection.Next);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBottom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateSorting(MovedDriection.Bottom);
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDataFields_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion

        #region  公有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            DataSet ds = CustomQueyContract.GetCustomQueyAndDataFieldInfos(DataQueriedId);
            gcDataFields.DataSource = ds.Tables[0];
        }

        #endregion

        #region  私有方法

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="movedDriection"></param>
        private void UpdateSorting(MovedDriection movedDriection)
        {
            if (gvDataFields.FocusedRowHandle >= 0)
            {
                decimal dataFieldId = Convert.ToDecimal(gvDataFields.GetRowCellValue(gvDataFields.FocusedRowHandle, "DataFieldId"));
                CustomQueyContract.UpdateCustomQueyAndDataFieldSorting(CurrentCustomQueyInfo.DataQueriedId, dataFieldId, movedDriection);
                LoadData();
            }
            else
            {
                MessageBox.Show("请先选择需要移动的记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        /// <summary>
        /// 获得查询的字段列表
        /// </summary>
        /// <returns></returns>
        private IList<CommonNode> GetDataFields()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            DataQueriedType dataQueriedType = (DataQueriedType)CurrentCustomQueyInfo.DataQueriedType;
            switch (dataQueriedType)
            {
                case DataQueriedType.Table:
                    commonNodes = CustomDataFieldContract.GetChildNodes(CurrentCustomQueyInfo.TableId);
                    break;

                case DataQueriedType.View:
                    commonNodes = CustomViewContract.GetAssociatedDataFields(CurrentCustomQueyInfo.ViewId);
                    break;

                case DataQueriedType.Custom:
                    commonNodes = CustomQueyContract.GetDataFields(CurrentCustomQueyInfo.DataQueriedId);
                    break;
            }

            return commonNodes;
        }
            
        #endregion                
    }
}
