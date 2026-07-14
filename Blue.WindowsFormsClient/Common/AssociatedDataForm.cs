using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.WinFormsLibrary;
using AppFramework.Core;
using Blue.WCFContracts.BusinessModule;
using AppFramework.Reference.DataFieldLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class AssociatedDataForm : Form
    {
        #region 私有变量

        private IList<AssociatedDataFieldInfo> associatedDataFieldInfos = null;

        #endregion

        #region 契约接口

        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion
        
        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        public decimal AssociationId
        {
            get;
            set;
        }

        /// <summary>
        /// 数据行确认
        /// </summary>
        public DataRowConfrimedDelegate DataRowConfrimed
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociatedDataForm()
        {
            InitializeComponent();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListForm_Load(object sender, EventArgs e)
        {            
            string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            grdAssociation.DataKeyNames = new string[] { key };
            LoadData();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataRowConfrimed?.Invoke(grdAssociation.FocusedDataRow);
            this.Close();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClear_Click(object sender, EventArgs e)
        {
            DataRowConfrimed?.Invoke(null);
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scCondition_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grdAssociation.CurrentPageIndex = 0;
            LoadData();
        }

        private void scCondition_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grdAssociation.CurrentPageIndex = 0;
            LoadData();
        }

        private void scCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                grdAssociation.CurrentPageIndex = 0;
                LoadData();
            }
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssociation_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 字段的物理名称
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public string GetDataFieldPhysicalName(decimal associatedDataFieldId)
        {
            string dataFieldPhysicalName = string.Empty;

            foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
            {
                if (associatedDataFieldInfo.AssociatedDataFieldId == associatedDataFieldId)
                {
                    dataFieldPhysicalName = associatedDataFieldInfo.PhysicalName;
                    break;
                }
            }

            return dataFieldPhysicalName;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                progressPanel.Show();

                /* 加载数据 */
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                string condition = scCondition.Text.Trim();
                if (!string.IsNullOrWhiteSpace(condition))
                {
                    if (associatedDataFieldInfos == null)
                    {
                        associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(AssociationId);
                    }
                    foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                    {
                        BasedDataType basedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                        if (basedDataType == BasedDataType.String)
                        {
                            whereConditons.Add(new WhereConditon(associatedDataFieldInfo.PhysicalName, associatedDataFieldInfo.PhysicalName, DbType.String, string.Format("%{0}%", condition),
                              DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                        }
                    }
                }
                int totalCount = 0;
                grdAssociation.DataSource = customAssociationContract.GetAssociationData(AssociationId, grdAssociation.PageSize * grdAssociation.CurrentPageIndex,
                    grdAssociation.PageSize, whereConditons, ref totalCount).Tables[0];
                grdAssociation.RecordCount = totalCount;
                if (grdAssociation.RowCount > 0)
                {
                    grdAssociation.FocusedRowHandle = 0;
                }
                progressPanel.Hide();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        
    }
}
