using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using AppFramework.Core;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class FindAndReplaceForm : Form
    {      
        #region 私有变量    
    
        private bool change = false;       
        //private bool foundJust;
        private int activeRowIndex = 1, activeColumnIndex = 1;//已经被替换到的单元格位置，用在单个替换功能
       // private int replaceRow = 0, replaceCol = 0;
       private bool caseSensitive = false;//区分大小写
        private bool matchCell = false;//单元格匹配

        #endregion

        #region 内部成员变量

        #endregion

        #region 属性

        /// <summary>
        /// 控件
        /// </summary>
        public FpSpread FpSpread
        {
            set;
            get;
        }

        /// <summary>
        /// 查找替换方式
        /// </summary>
        public FindReplaceType FindReplaceType
        {
            set;
            get;
        }

        #endregion        

        public FindAndReplaceForm()
        {
            InitializeComponent();
            InitDropdownListProperty();

        }

        /// <summary>
        /// 初始化下拉框属性
        /// </summary>
        private void InitDropdownListProperty()
        {
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(FindType));
            cmbeSerachType.Properties.Items.AddRange(enumItems.ToArray());
            cmbeSerachType.SelectedIndex = 0;
            switch (FindReplaceType)
            {
                case FindReplaceType.Find:
                    Text = "查找";
                    break;

                case FindReplaceType.Replace:
                    Text = "替换";
                    break;
            }
        }

        private void FindAndReplaceForm_Load(object sender, EventArgs e)
        {
            
        }

        private void chkMatchCase_CheckedChanged(object sender, System.EventArgs e)
        {
            caseSensitive = chkMatchCase.Checked;
            change = true;
            SetChange();
        }

        private void SetChange()
        {
            if (string.IsNullOrWhiteSpace(etxtFind.Text))
            {
                sbtnFindNext.Enabled = true;
                sbtnReplace.Enabled = true;
                sbtnReplaceAll.Enabled = true;
            }
        }

        private void txtFind_TextChanged(object sender, System.EventArgs e)
        {
            change = true;
            SetChange();
        }

        private void txtReplace_TextChanged(object sender, System.EventArgs e)
        {
            change = true;
            SetChange();
        }

        private void chkEntireCell_CheckedChanged(object sender, System.EventArgs e)
        {
            matchCell = chkEntireCell.Checked;
            change = true;
            SetChange();
        }

        private void cboSearch_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            change = true;
            SetChange();
        }

        private void txtFind_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                sbtnFindNext_Click(sender, e);
            }
        }

        private void etxtFind_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void sbtnFindNext_Click(object sender, EventArgs e)
        {
            int rowCnt = (FpSpread.ActiveSheet.RowCount - 1);
            int colCnt = (FpSpread.ActiveSheet.ColumnCount - 1);
            int startRow = 0;
            int startCol = 0; 
            int rowIndex = 0;
            int colIndex = 0;
            bool next = false;
            int findCount = 0;

            if (activeRowIndex < rowCnt)
            {
                startRow = activeRowIndex + 1;
                startCol = 0;
            }
            else if (activeColumnIndex < colCnt)
            {
                startRow = 0;
                startCol = activeColumnIndex + 1;
            }
            do
            {
                findCount = 0;
                if ((activeRowIndex >= 0) && (activeRowIndex <= rowCnt) && (activeColumnIndex >= 0) && (activeColumnIndex <= colCnt))
                {
                    FpSpread.Search(FpSpread.ActiveSheetIndex, etxtFind.Text, chkMatchCase.Checked, chkEntireCell.Checked, false, false, true, false, startRow, startCol, rowCnt,
                        colCnt, ref rowIndex, ref colIndex);
                    if (((rowIndex >= 0) && (colIndex >= 0)))
                    {
                        FpSpread.ActiveSheet.SetActiveCell(rowIndex, colIndex);
                        activeRowIndex = rowIndex;
                        activeColumnIndex = colIndex;
                        next = false;
                    }
                    else
                    {
                        startRow = 0;
                        startCol = 0;
                        next = findCount > 0 ? false : true;
                    }
                    findCount++;
                }
            } while (next);
        }

        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sbtnReplace_Click(object sender, EventArgs e)
        {

        }

        private void sbtnReplaceAll_Click(object sender, EventArgs e)
        {

        }

        private void FindAndReplaceForm_Activated(object sender, EventArgs e)
        {
            activeRowIndex = FpSpread.ActiveSheet.ActiveRowIndex;
            activeColumnIndex = FpSpread.ActiveSheet.ActiveColumnIndex;            
        }
    }
}