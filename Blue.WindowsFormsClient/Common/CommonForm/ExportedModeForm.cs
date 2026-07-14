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

namespace Blue.WindowsFormsClient
{
    public partial class ExportedModeForm : Form
    {
        #region 属性

        /// <summary>
        /// 导出属性
        /// </summary>
        public Int64 ExportedModeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 数据导出代理
        /// </summary>
        public DataExportedDelegate DataExported
        {
            get;
            set;
        }

        /// <summary>
        ///提示
        /// </summary>
        public string Tip
        {
            get
            {
                return gcMain.Text;
            }
            set
            {
                gcMain.Text = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExportedModeForm()
        {
            InitializeComponent();            
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportedModeForm_Load(object sender, EventArgs e)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(ExportedMode));
            if (ExportedModeValue > 0)
            {
                IList<EnumItem> items = new List<EnumItem>();
                foreach (EnumItem enumItem in enumItems)
                {
                    bool result = AuthorityHelper.CheckAuthority(ExportedModeValue, enumItem.Value);
                    if (result)
                    {
                        items.Add(enumItem);
                    }
                }
                UserControlHelper.InitImageComboBoxEdit(icmbExportedMode, items);
            }
            else
            {
                UserControlHelper.InitImageComboBoxEdit(icmbExportedMode, enumItems);
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ExportedMode exportedMode = (ExportedMode)Convert.ToByte(icmbExportedMode.EditValue);
            DataExported?.Invoke(exportedMode);
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

        #endregion

    }
}
