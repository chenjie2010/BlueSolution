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
    public partial class ImportedModeForm : Form
    {
        #region 属性

        /// <summary>
        /// 导入属性
        /// </summary>
        public long ImportedModeValue
        {
            get;
            set;
        }

        /// <summary>
        /// 数据导入代理
        /// </summary>
        public DataImportedDelegate DataImported
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
        public ImportedModeForm()
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
        private void ImportedModeForm_Load(object sender, EventArgs e)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(ImportedMode));
            if (ImportedModeValue > 0)
            {
                IList<EnumItem> items = new List<EnumItem>();
                foreach (EnumItem enumItem in enumItems)
                {
                    bool result = AuthorityHelper.CheckAuthority(ImportedModeValue, enumItem.Value);
                    if (result)
                    {
                        items.Add(enumItem);
                    }
                }
                UserControlHelper.InitImageComboBoxEdit(icmbImportedMode, items);
            }
            else
            {
                UserControlHelper.InitImageComboBoxEdit(icmbImportedMode, enumItems);
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ImportedMode importedMode = (ImportedMode)Convert.ToByte(icmbImportedMode.EditValue);
            DataImported?.Invoke(importedMode);
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
