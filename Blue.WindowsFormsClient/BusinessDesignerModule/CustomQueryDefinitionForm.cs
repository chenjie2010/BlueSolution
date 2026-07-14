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
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class CustomQueryDefinitionForm : Form
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
        /// 
        /// </summary>
        public GetEntityDelegate GetEntity
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CustomQueryName
        {
            get
            {
                return txtCustomViewName.Text.Trim();
            }
            set
            {
                txtCustomViewName.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Conditions
        {
            get
            {
                return meConditions.Text.Trim();
            }
            set
            {
                meConditions.Text = value;
            }
        }
        
        /// <summary>
        /// 数据库编号
        /// </summary>
        public byte DataWarehouseId
        {           
            get
            {
                return DataConvertionHelper.GetConvertedByte(icmbDataWarehouse.EditValue, 0);
            }
            set
            {
                icmbDataWarehouse.EditValue = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomQueryDefinitionForm()
        {
            InitializeComponent();
            UserControlHelper.InitImageComboBoxEdit(icmbDataWarehouse, typeof(DataWarehouse));            
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

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string warning = string.Empty;
            bool result = ValidateSQL(out warning);
            if (!result)
            {
                MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            byte dataWarehouseId = DataConvertionHelper.GetConvertedByte(icmbDataWarehouse.EditValue, 0);
            string conditions = meConditions.Text.Trim();
            CommonNode node = new CommonNode(dataWarehouseId, conditions);
            GetEntity?.Invoke(node);
            this.Close();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string warning = string.Empty;
            bool result = ValidateSQL(out warning);
            if (result)
            {
                MessageBox.Show("校验成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #endregion

        #region 私有方法

        /// <summary>
        /// SQL 语句校验
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool ValidateSQL(out string warning)
        {
            bool result = false;
            warning = string.Empty;


            byte dataWarehouseId = DataConvertionHelper.GetConvertedByte(icmbDataWarehouse.EditValue, 0);
            if (dataWarehouseId <= 0)
            {
                warning = "请选择数据仓库。";               
            }

            string conditions = meConditions.Text.Trim();
            if (string.IsNullOrWhiteSpace(conditions))
            {
                warning = "查询语句不能为空。";                
            }
            if (DataAccessHandler.HasDangerousContents(conditions))
            {
                warning = "查询语句含有非法字符，请检查。";
            }
            if (!CustomQueyContract.ValidateSQL(dataWarehouseId, conditions))
            {
                warning = "校验失败，请检查。";
            }
          
            if (string.IsNullOrWhiteSpace(warning))
            {
                result = true;
            }
            
            return result;
        }


        #endregion
    }
}
