using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Win.Spread.CellType;
using AppFramework.Core;

namespace Blue.WindowsFormsClient
{
    public partial class CellFormatForm : Form
    {
        #region 自定义格式

        private string[] currency = {
                                        "￥#,##0;￥-#,##0",
                                        "￥#,##0.00;￥-#,##0.00",
                                        "￥* _-#,##0;￥* -#,##0;￥* _-" + "\" - \"" + ";@",
                                        "￥* _-#,##0.00;￥* -#,##0.00;￥* _-" + "\" - \"" + "??;@"
                                        };

        private string[] fixedValue = {
                                    "0",
                                    "0.0",
                                    "0.00",
                                    "0.000",
                                    "0.0000",
                                    "0.00000",
                                    "0.000000",
                                    "#,##0",
                                    "#,##0.0",
                                    "#,##0.00",
                                    "#,##0.000",
                                    "#,##0.0000",
                                    "#,##0.00000",
                                    "#,##0.000000",
                                    "￥#,##0_);(￥#,##0)",
                                    "￥#,##0.00_);(￥#,##0.00)",
                                    "#,##0;-#,##0",
                                    "#,##0.00;-#,##0.00",
                                    "* #,##0;* -#,##0;* " + "\" - \"" + ";@",
                                    "* #,##0.00;* -#,##0.00;* " + "\" - \"" + "??;@",
                                    };

        private string[] percent = {
                                        "0%",
                                        "0.00%",
                                      };
        private string[] scientific = {
                                            "0.00E+00",
                                            "##0.0E+0",
                                         };
        private string[] datetimes = {
                                    "yyyy年MM月dd日",
                                    "yyyy-MM-dd",
                                    "yyyy年MM月dd日 HH:mm",
                                    "yyyy-MM-dd HH:mm",
                                    "hh:mm AM/PM",
                                    "hh:mm:ss AM/PM",
                                    "HH:mm",
                                    "hh:mm:ss",
                                    "yyyy-MM-d HH:mm",
                                    "mm:ss",
                                    "mm:ss.0",
                                   };
        #endregion

        #region 属性

        /// <summary>
        /// 设置单元格类型
        /// </summary>
        public SetCellTypeDelegate SetCellType
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CellFormatForm()
        {
            InitializeComponent();
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(CellDataType));
            lstCellType.Items.AddRange(enumItems.ToArray());                        
        }

        #endregion


        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellFormatForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单元格格式变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstCellType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstFormat.Items.Clear();
            EnumItem enumItem = lstCellType.SelectedItem as EnumItem;
            CellDataType cellType = (CellDataType)enumItem.Value;
            switch (cellType)
            {
                case CellDataType.GeneralCellType:
                    lstFormat.Items.Add("通用格式");
                    break;

                case CellDataType.TextCellType:
                    lstFormat.Items.Add("文本格式");
                    break;

                case CellDataType.NumberCellType:
                    foreach (string s in fixedValue)
                    {
                        lstFormat.Items.Add(s);
                    }
                    break;

                case CellDataType.PercentCellType:
                    foreach (string s in percent)
                    {
                        lstFormat.Items.Add(s);
                    }
                    break;

                case CellDataType.CurrencyCellType:
                    foreach (string s in currency)
                    {
                        lstFormat.Items.Add(s);
                    }
                    break;

                case CellDataType.DateTimeCellType:
                    foreach (string s in datetimes)
                    {
                        lstFormat.Items.Add(s);
                    }
                    break;
            }
        }

        /// <summary>
        /// 重新设置例子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFormat.SelectedItem != null)
            {
                txtExample.Text = lstFormat.SelectedItem.ToString().Trim();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            EnumItem enumItem = lstCellType.SelectedItem as EnumItem;
            CellDataType cellType = (CellDataType)enumItem.Value;
            ICellType cellFormat = null;
            string format = lstFormat.SelectedItem.ToString().Trim();
            switch (cellType)
            {
                case CellDataType.GeneralCellType:
                    GeneralCellType generalCellType = new GeneralCellType();
                    cellFormat = generalCellType;
                    break;

                case CellDataType.TextCellType:
                    TextCellType textCellType = new TextCellType();
                    textCellType.Multiline = true;
                    cellFormat = textCellType;
                    break;

                case CellDataType.NumberCellType:
                    NumberCellType numberCellType = new NumberCellType();
                    int pos = format.LastIndexOf(".");
                    int length = 0;
                    if (pos > 0)
                    {
                        length = format.Substring(pos).Length - 1;
                    }
                    numberCellType.DecimalPlaces = length;
                    cellFormat = numberCellType;
                    break;

                case CellDataType.PercentCellType:
                    PercentCellType percentCellType = new PercentCellType();
                    cellFormat = percentCellType;
                    break;

                case CellDataType.CurrencyCellType:
                    CurrencyCellType currencyCellType = new CurrencyCellType();
                    cellFormat = currencyCellType;
                    break;

                case CellDataType.DateTimeCellType:
                    DateTimeCellType dateTimeCellType = new DateTimeCellType();
                    dateTimeCellType.DateTimeFormat = DateTimeFormat.UserDefined;
                    dateTimeCellType.UserDefinedFormat = format;
                    cellFormat = dateTimeCellType;
                    break;
            }
            SetCellType?.Invoke(cellFormat);
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
