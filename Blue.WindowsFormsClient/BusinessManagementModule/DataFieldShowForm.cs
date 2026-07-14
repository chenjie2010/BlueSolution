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
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class DataFieldShowForm : Form
    {
        #region 属性

        /// <summary>
        /// 查询编号
        /// </summary>
        public decimal DataQueriedId
        {
            get;
            set;
        }

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get;
            set;
        }

        /// <summary>
        /// 获得查询中的字段
        /// </summary>
        public GetCustomQueyAndDataFieldDelegate GetCustomQueyAndDataField
        {
            set;
            get;
        }

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
            get; set;
        }

        /// <summary>
        /// 数据字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            get; set;
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFieldShowForm()
        {
            InitializeComponent();
            DataQueriedId = 0;
            DataFieldId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbDataFieldMode, typeof(DataFieldMode));

        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableSelectedItemsForm_Load(object sender, EventArgs e)
        {
            if (DataQueriedId > 0 && DataFieldId > 0)
            {
                cmbDataField.Properties.ReadOnly = true;
                CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo = CustomQueyContract.GetCustomQueyAndDataFieldInfo(DataFieldId, DataQueriedId);
                if (customQueyAndDataFieldInfo != null)
                {
                    cmbDataField.EditValue = CustomDataFieldContract.GetCommonNode(customQueyAndDataFieldInfo.DataFieldId);
                    icmbDataFieldMode.EditValue = customQueyAndDataFieldInfo.DataFieldMode;
                    chkQueryAllowed.Checked = customQueyAndDataFieldInfo.QueryAllowed;
                    meDataFieldFormat.Text = customQueyAndDataFieldInfo.DataFieldFormat;
                    meConditions.Text = customQueyAndDataFieldInfo.Conditions;
                }
            }
            else
            {
                cmbDataField.Properties.ReadOnly = false;
            }
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
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DataQueriedId <= 0)
            {
                throw new ArgumentException("查询编号不能为空。");
            }
            if (GetCustomQueyAndDataField != null)
            {
                if (cmbDataField.EditValue == null)
                {
                    MessageBox.Show("请先选择字段。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CustomQueyAndDataFieldInfo customQueyAndDataFieldInfo = null;
                byte dataFieldMode = Convert.ToByte(icmbDataFieldMode.EditValue);
                bool queryAllowed = chkQueryAllowed.Checked;
                string dataFieldFormat = meDataFieldFormat.Text.Trim();
                string conditions = meConditions.Text.Trim();
                if (DataFieldId > 0)
                {
                    customQueyAndDataFieldInfo = new CustomQueyAndDataFieldInfo(DataFieldId, DataQueriedId, dataFieldMode, dataFieldFormat, queryAllowed, conditions, 0);
                }
                else
                {
                    CommonNode commonNode = cmbDataField.EditValue as CommonNode;
                    customQueyAndDataFieldInfo = new CustomQueyAndDataFieldInfo(commonNode.NodeId, DataQueriedId, dataFieldMode, dataFieldFormat, queryAllowed, conditions, 0);
                }
                GetCustomQueyAndDataField(customQueyAndDataFieldInfo);
            }
            this.Close();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiVerfiy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        #endregion


        #region 公有方法

        /// <summary>
        /// 加载字段
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadDataFields(IList<CommonNode> commonNodes)
        {
            IList<CommonNode> oldCommonNodes = CustomQueyContract.GetAssociatedDataFields(DataQueriedId);
            foreach (var commonNode in commonNodes)
            {
                if (commonNode.NodeId <= 0) continue;
                bool find = false;
                foreach (var oldCommonNode in oldCommonNodes)
                {
                    if (commonNode.NodeId == oldCommonNode.NodeId)
                    {
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    cmbDataField.Properties.Items.Add(commonNode);
                }
            }            
        }

        #endregion
    }
}
