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
using AppFramework.Reference.DataFieldLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowDataFieldForm : Form
    {
        #region 私有变量

        #endregion

        #region 契约接口

        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;

        #endregion

        #region  自定义属性

        /// <summary>
        /// 业务流程编号
        /// </summary>
        public decimal ProcessId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowDataFieldForm()
        {
            InitializeComponent();
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldCondition, typeof(DataFieldCondition));
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbNextRelation, typeof(NextTableRelation));
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowDataFieldForm_Load(object sender, EventArgs e)
        {
            LoadData();
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
            WorkflowAndDataFieldForm frmWorkflowAndDataField = new WorkflowAndDataFieldForm();
            frmWorkflowAndDataField.ProcessId = ProcessId;
            frmWorkflowAndDataField.SetWorkflowProcessDataFieldHandler = (workflowProcessAndDataFieldInfo) =>
            {
                customWorkflowProcessContract.InsertWorkflowProcessAndDataFieldInfo(workflowProcessAndDataFieldInfo);
                LoadData();
            };
            frmWorkflowAndDataField.ShowDialog();            
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
                WorkflowAndDataFieldForm frmWorkflowAndDataField = new WorkflowAndDataFieldForm();
                frmWorkflowAndDataField.ProcessId = ProcessId;
                frmWorkflowAndDataField.DataFieldId = dataFieldId;
                frmWorkflowAndDataField.SetWorkflowProcessDataFieldHandler = (workflowProcessAndDataFieldInfo) =>
                {
                    customWorkflowProcessContract.UpdateWorkflowProcessAndDataFieldInfo(workflowProcessAndDataFieldInfo);
                    LoadData();
                }; 
                frmWorkflowAndDataField.ShowDialog();
                
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
                    customWorkflowProcessContract.Delete(ProcessId, dataFieldId);
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
            DataSet ds = customWorkflowProcessContract.GetPageRecordByProcessId(ProcessId);
            ds.Tables[0].Columns.Add("FstConditionValue", Type.GetType("System.String"));
            ds.Tables[0].Columns.Add("ScdConditionValue", Type.GetType("System.String"));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                BasedDataType dataFieldBase = BasedDataType.Boolean;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(dr["DataFieldProperty"]);
                byte dataFieldType = Convert.ToByte(dr["DataFieldType"]);
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldType;
                        if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                        {
                            decimal dataFieldId = Convert.ToDecimal(dr["DataFieldId"]);
                            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);
                            BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                            physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                        }
                        dataFieldBase = DataFieldHelper.GetBasedDataType(physicalDataFieldType);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        dataFieldBase = DataFieldHelper.GetBasedDataType((LogicalDataFieldType)dataFieldType);
                        break;
                }
                switch (dataFieldBase)
                {
                    case BasedDataType.Boolean:
                        dr["FstConditionValue"] = Convert.ToBoolean(dr["BoolValue"]) ? "是" : "否";
                        break;

                    case BasedDataType.DateTime:
                        dr["FstConditionValue"] = Convert.ToDateTime(dr["FstTimeValue"]);
                        dr["ScdConditionValue"] = Convert.ToDateTime(dr["ScdTimeValue"]);
                        break;

                    case BasedDataType.Decimal:
                        dr["FstConditionValue"] = Convert.ToDecimal(dr["FstDecimalValue"]);
                        dr["ScdConditionValue"] = Convert.ToDecimal(dr["ScdDecimalValue"]);
                        break;

                    case BasedDataType.Int32:
                        dr["FstConditionValue"] = Convert.ToInt32(dr["FstIntegerValue"]);
                        dr["ScdConditionValue"] = Convert.ToInt32(dr["ScdIntegerValue"]);
                        break;

                    case BasedDataType.String:
                        dr["FstConditionValue"] = Convert.ToString(dr["StringValue"]);
                        break;
                }
            }
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
                customWorkflowProcessContract.UpdateWorkflowProcessAndDataFieldSorting(ProcessId, dataFieldId, movedDriection);
                LoadData();
            }
            else
            {
                MessageBox.Show("请先选择需要移动的记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }       
            
        #endregion                
    }
}
