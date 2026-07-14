using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class PrintSettingForm : Form
    {
        public decimal CustomSheetId
        {
            get;
            set;
        }

        public ICustomSheetContract CustomSheetContract
        {
            get;
            set;
        }

        private PrintInfo _printInfo;

        public PrintInfo PrintInfo
        {
            get
            {
                return _printInfo;
            }
            set
            {
                _printInfo = value;
                /* 打印页面范围 */
                switch (_printInfo.PrintType)
                {
                    case PrintType.All:
                        radioGroup.SelectedIndex = 0;
                        break;

                    case PrintType.CellRange:
                        radioGroup.SelectedIndex = 1;
                        break;

                    case PrintType.CurrentPage:
                        radioGroup.SelectedIndex = 2;
                        break;

                    case PrintType.PageRange:
                        radioGroup.SelectedIndex = 3;
                        etxtStart.Text = _printInfo.PageStart.ToString();
                        etxtEnd.Text = _printInfo.PageEnd.ToString();
                        break;
                }

                /* 打印页面设置 */
                chkColumnHeader.Checked = _printInfo.ShowColumnHeaders;
                chkRowHeader.Checked = _printInfo.ShowRowHeaders;
                chkGridLine.Checked = _printInfo.ShowGrid;
                chkBorder.Checked = _printInfo.ShowBorder;
                chkShadow.Checked = _printInfo.ShowShadows;
                chkColor.Checked = _printInfo.ShowColor;
                chkOnlyDataCells.Checked = _printInfo.UseMax;
                chkBestFit.Checked = _printInfo.BestFitCols;

                /* 页面边距设置 */
                if (CustomSheetId > 0 && CustomSheetContract != null)
                {
                    Cursor = Cursors.WaitCursor;
                    CustomMargin customMargin = CustomSheetContract.GetMargin(CustomSheetId);
                    Cursor = Cursors.Default;
                    if (customMargin.Top > 0)
                    {
                        etxtTop.Text = customMargin.Top.ToString();
                    }
                    else
                    {
                        etxtTop.Text = value.Margin.Top.ToString();
                    }
                    if (customMargin.Left > 0)
                    {
                        etxtLeft.Text = customMargin.Left.ToString();
                    }
                    else
                    {
                        etxtLeft.Text = value.Margin.Left.ToString();
                    }
                    if (customMargin.Bottom > 0)
                    {
                        etxtBottom.Text = customMargin.Bottom.ToString();
                    }
                    else
                    {
                        etxtBottom.Text = value.Margin.Bottom.ToString();
                    }
                    if (customMargin.Right > 0)
                    {
                        etxtRight.Text = customMargin.Right.ToString();
                    }
                    else
                    {
                        etxtRight.Text = value.Margin.Right.ToString();
                    }
                }                

                /* 打印方向 */
                switch (_printInfo.Orientation)
                {
                    case PrintOrientation.Auto:
                        rgOrientation.SelectedIndex = 0;
                        break;

                    case PrintOrientation.Portrait:
                        rgOrientation.SelectedIndex = 1;
                        break;

                    case PrintOrientation.Landscape:
                        rgOrientation.SelectedIndex = 2;
                        break;
                }        

            }
        }

        public PrintSettingForm()
        {
            InitializeComponent();
        }

        private void PrintSettingForm_Load(object sender, EventArgs e)
        {

        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            if (PrintInfo != null)
            {
                /* 打印页面范围 */
                switch (radioGroup.SelectedIndex)
                {
                    case 0:
                        _printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
                        break;
                    case 1:
                        _printInfo.PrintType = FarPoint.Win.Spread.PrintType.CellRange;
                        break;

                    case 2:
                        _printInfo.PrintType = FarPoint.Win.Spread.PrintType.CurrentPage;
                        break;

                    case 3:
                        string start = etxtStart.Text.Trim();
                        string end = etxtEnd.Text.Trim();
                        if (string.IsNullOrWhiteSpace(start))
                        {
                            etxtStart.Focus();
                            MessageBox.Show("起始页不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(end))
                        {
                            etxtEnd.Focus();
                            MessageBox.Show("末页不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        Regex r = new Regex(@"^\d+$");
                        int startPage = 0;
                        int endPage = 0;
                        if (!r.IsMatch(start))
                        {
                            etxtStart.Focus();
                            MessageBox.Show("起始页只能为整数！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!r.IsMatch(end))
                        {
                            etxtEnd.Focus();
                            MessageBox.Show("末页只能为整数！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        startPage = Int32.Parse(start);
                        endPage = Int32.Parse(end);
                        _printInfo.PrintType = FarPoint.Win.Spread.PrintType.PageRange;
                        _printInfo.PageStart = Math.Min(startPage, endPage);
                        _printInfo.PageEnd = Math.Max(startPage, endPage);
                        break;
                }

                /* 打印页面设置 */
                _printInfo.ShowColumnHeaders = chkColumnHeader.Checked;
                _printInfo.ShowRowHeaders = chkRowHeader.Checked;
                _printInfo.ShowGrid = chkGridLine.Checked;
                _printInfo.ShowBorder = chkBorder.Checked;
                _printInfo.ShowShadows = chkShadow.Checked;
                _printInfo.ShowColor = chkColor.Checked;
                _printInfo.UseMax = chkOnlyDataCells.Checked;
                _printInfo.BestFitCols = chkBestFit.Checked;

                /* 页面边距设置 */
                string left = etxtLeft.Text.Trim();
                string right = etxtRight.Text.Trim();
                string top = etxtTop.Text.Trim();
                string bottom = etxtBottom.Text.Trim();
                if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right) ||
                    string.IsNullOrWhiteSpace(top) || string.IsNullOrWhiteSpace(bottom))
                {
                    etxtStart.Focus();
                    MessageBox.Show("页边距数字不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Regex regex = new Regex(@"^\d+$");
                if (!regex.IsMatch(left) || !regex.IsMatch(right) || !regex.IsMatch(top) || !regex.IsMatch(bottom))
                {
                    MessageBox.Show("页边距数字只能为整数！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _printInfo.Margin.Top = Int32.Parse(top);
                _printInfo.Margin.Left = Int32.Parse(left);
                _printInfo.Margin.Right = Int32.Parse(right);
                _printInfo.Margin.Bottom = Int32.Parse(bottom);
                if (CustomSheetId > 0 && CustomSheetContract != null)
                {
                    CustomSheetContract.UpdateMargin(CustomSheetId, new CustomMargin(_printInfo.Margin.Top, _printInfo.Margin.Bottom, 
                        _printInfo.Margin.Left, _printInfo.Margin.Right));
                }

                /* 打印方向 */
                switch (rgOrientation.SelectedIndex)
                {
                    case 0:
                        _printInfo.Orientation = PrintOrientation.Auto;
                        break;

                    case 1:
                        _printInfo.Orientation = PrintOrientation.Portrait;
                        break;

                    case 2:
                        _printInfo.Orientation = PrintOrientation.Landscape;
                        break;
                }


            }
            this.Close();
        }

        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
