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
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class SortingConditionForm : Form
    {
        #region 属性

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
            get;
            set;
        }

        /// <summary>
        /// 自定义视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get;
            set;
        }

        /// <summary>
        /// 查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get; set;
        }

        /// <summary>
        /// 系统字段条件
        /// </summary>
        public long SystemDataFieldCondition
        {
            get; set;
        }

        /// <summary>
        /// 获得排序字段
        /// </summary>
        public GetSortingCondtionsDelegate GetSortingCondtions
        {
            get; set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public SortingConditionForm()
        {
            InitializeComponent();
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataSorting, typeof(CustomSorting));
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortingConditionForm_Load(object sender, EventArgs e)
        {
            CustomQueyInfo customQueyInfo = CustomQueyContract.GetModelInfo(DataQueriedId);
            DataQueriedType tableType = (DataQueriedType)customQueyInfo.DataQueriedType;
            string tableName = string.Empty;
            switch (tableType)
            {
                case DataQueriedType.Custom:
                    tableName = customQueyInfo.CustomViewName;
                    break;

                case DataQueriedType.Table:
                    tableName = CustomTableContract.GetTablePhysicalName(customQueyInfo.TableId);
                    break;

                case DataQueriedType.View:
                    tableName = CustomViewContract.GetViewPhysicalName(customQueyInfo.ViewId);
                    break;
            }

            IList<CustomDataFieldInfo> customDataFieldInfos = CustomQueyContract.GetConditionalCustomDataFieldInfos(DataQueriedId);
            IList<CommonNode> commonNodes = new List<CommonNode>();

            /*1.用户条件*/
            bool user = AuthorityHelper.CheckAuthority(SystemDataFieldCondition, (byte)SystemCondition.User);
            if (user)
            {
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserName), 
                     string.Format("{0}.{1}", tableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName), (byte)CustomSorting.None)));
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalDataFieldName(SystemDataField.UserActualName),
                    string.Format("{0}.{1}", tableName, DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserActualName), (byte)CustomSorting.None)));
            }

            /*2.用户类型条件*/
            bool userType = AuthorityHelper.CheckAuthority(SystemDataFieldCondition, (byte)SystemCondition.UserTypeId);
            if (userType)
            {
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserTypeId),
                    string.Format("{0}.{1}", tableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId), (byte)CustomSorting.None)));
            }

            /*3.单位条件*/
            bool dep = AuthorityHelper.CheckAuthority(SystemDataFieldCondition, (byte)SystemCondition.DepId);
            if (dep)
            {
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.DepId),
                    string.Format("{0}.{1}", tableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId), (byte)CustomSorting.None)));
            }

            /*4.单位属性条件*/
            bool departmentProperty = AuthorityHelper.CheckAuthority(SystemDataFieldCondition, (byte)SystemCondition.DepartmentProperty);
            if (departmentProperty)
            {
                commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalDataFieldName(SystemDataField.DepProperty),
                    string.Format("{0}.{1}", tableName, DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.DepProperty), (byte)CustomSorting.None)));
            }

            foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
            {
                commonNodes.Add(new CommonNode(customDataFieldInfo.DataFieldId, customDataFieldInfo.TableId, customDataFieldInfo.LogicalName, customDataFieldInfo.PhysicalName, (byte)CustomSorting.None));
            }

            gcDataFields.DataSource = commonNodes;
        }

        #endregion

        /// <summary>
        /// 确认操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gvDataFields.RowCount; i++)
            {
                string nodeName = DataConvertionHelper.GetString(gvDataFields.GetRowCellValue(i, "NodeName"));
                string nodeValue = DataConvertionHelper.GetString(gvDataFields.GetRowCellValue(i, "NodeCode"));
                CustomSorting customSorting = (CustomSorting)DataConvertionHelper.GetByte(gvDataFields.GetRowCellValue(i, "NodeType"));
                switch (customSorting)
                {
                    case CustomSorting.Descending:
                        sortingCondtions.Add(new SortingCondtion(nodeValue, CustomSorting.Descending));
                        sb.AppendFormat("{0}[{1}], ", nodeName, UserEnumHelper.GetEnumText(customSorting));
                        break;

                    case CustomSorting.Ascending:
                        sortingCondtions.Add(new SortingCondtion(nodeValue, CustomSorting.Ascending));
                        sb.AppendFormat("{0}[{1}], ", nodeName, UserEnumHelper.GetEnumText(customSorting));
                        break;
                }                
            }
            if (sortingCondtions.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            GetSortingCondtions?.Invoke(sortingCondtions, sb.ToString());
            this.Close();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
