//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ControlHelper.cs
// 描述: 控件帮助类类
// 作者：ChenJie 
// 编写日期：2016/08/24
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// 控件帮助类
    /// </summary>
    public sealed class UserControlHelper
    {
        #region 格式化的名称， 示例：A,B

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetCleanFormattedName(IList<CommonNode> commonNodes)
        {
            if (commonNodes == null || commonNodes.Count == 0)
            {
                return string.Empty;
            }
            
            return GetCleanFormattedName(commonNodes, ",");
        }
        
        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetCleanFormattedName(IList<CommonNode> commonNodes, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommonNode commonNode in commonNodes)
            {
                if (string.IsNullOrWhiteSpace(separator))
                {
                    sb.AppendFormat("{0}", commonNode.NodeName);
                }
                else
                {
                    sb.AppendFormat("{0}{1}", commonNode.NodeName, separator);
                }
            }
            if (commonNodes.Count > 0 && !string.IsNullOrWhiteSpace(separator))
            {
                sb.Remove(sb.Length - separator.Length, separator.Length);
            }
            return sb.ToString();
        }

        #endregion

        #region 格式化的名称， 示例：[A]|[B]

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetFormattedName(IList<CommonNode> commonNodes)
        {
            return GetFormattedName(commonNodes, string.Empty);
        }

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetFormattedName(IList<CommonNode> commonNodes, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommonNode commonNode in commonNodes)
            {
                if (string.IsNullOrWhiteSpace(separator))
                {
                    sb.AppendFormat("[{0}]", commonNode.NodeName);
                }
                else
                {
                    sb.AppendFormat("[{0}]{1} ", commonNode.NodeName, separator);
                }
            }
            if (commonNodes.Count > 0 && !string.IsNullOrWhiteSpace(separator))
            {
                int len = separator.Length + 1;
                sb.Remove(sb.Length - len, len);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static string GetFormattedName(IList<string> names)
        {
            return GetFormattedName(names, string.Empty, string.Empty);
        }

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetFormattedName(IList<string> names, string separator)
        {
            return GetFormattedName(names, string.Empty, separator);
        }

        /// <summary>
        /// 格式化的名称
        /// </summary>
        /// <param name="names"></param>
        /// <param name="nodeName"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetFormattedName(IList<string> names, string nodeName, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                if (string.IsNullOrWhiteSpace(separator))
                {
                    sb.AppendFormat("[{0}]", name);
                }
                else
                {
                    sb.AppendFormat("[{0}]{1} ", name, separator);
                }
            }
            if (names.Count > 0 && !string.IsNullOrWhiteSpace(separator))
            {
                int len = separator.Length + 1;
                sb.Remove(sb.Length - len, len);
            }

            if (!string.IsNullOrWhiteSpace(nodeName))
            {
                if (string.IsNullOrWhiteSpace(separator))
                {
                    sb.AppendFormat("[{0}]", nodeName);
                }
                else
                {
                    sb.AppendFormat(" {1}[{0}]", separator, nodeName);
                }
            }

            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumType"></param>
        public static void InitRepositoryItemImageComboBox(RepositoryItemImageComboBox repositoryItemImageComboBox, Type enumType)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(enumType);
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, i);
                repositoryItemImageComboBox.Items.Add(item);
            }
        }

        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumItems"></param>
        public static void InitRepositoryItemImageComboBox(RepositoryItemImageComboBox repositoryItemImageComboBox, IList<EnumItem> enumItems)
        {
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, i);
                repositoryItemImageComboBox.Items.Add(item);
            }
        }

        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumItem"></param>
        /// <param name="imageIndex"></param>
        public static void AddItemToImageComboBoxEdit(ImageComboBoxEdit imageComboBoxEdit, EnumItem enumItem, int imageIndex)
        {
            ImageComboBoxItem item = new ImageComboBoxItem(enumItem.Text, enumItem.Value, imageIndex);
            imageComboBoxEdit.Properties.Items.Add(item);
        }
        
        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumType"></param>
        public static void InitImageComboBoxEdit(ImageComboBoxEdit imageComboBoxEdit, Type enumType)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(enumType);
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, i);
                imageComboBoxEdit.Properties.Items.Add(item);
            }
            imageComboBoxEdit.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumItems"></param>
        public static void InitImageComboBoxEditWithImage(ImageComboBoxEdit imageComboBoxEdit, IList<EnumItem> enumItems)
        {
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, enumItems[i].Value);
                imageComboBoxEdit.Properties.Items.Add(item);
            }
            imageComboBoxEdit.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化带图片标识的下拉框
        /// </summary>
        /// <param name="imageComboBoxEdit"></param>
        /// <param name="enumItems"></param>
        public static void InitImageComboBoxEdit(ImageComboBoxEdit imageComboBoxEdit, IList<EnumItem> enumItems)
        {
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, i);
                imageComboBoxEdit.Properties.Items.Add(item);
            }
            imageComboBoxEdit.SelectedIndex = 0;
        }


        /// <summary>
        /// 设置控件RadioGroup的选项
        /// </summary>
        /// <param name="authority">字段权限</param>
        /// <param name="authority">字段权限</param>
        public static void InitRadioGroupItems(RadioGroup radioGroup, Type enumType)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(enumType);
            foreach (EnumItem enumItem in enumItems)
            {
                radioGroup.Properties.Items.Add(new RadioGroupItem(enumItem.Value, enumItem.Text));
            }
        }

        /// <summary>
        /// 设置多选框内的选项
        /// </summary>
        /// <param name="authority">字段权限</param>
        /// <param name="authority">字段权限</param>
        public static void InitCheckedListBoxItems(CheckedListBoxControl checkedListBoxControl, Type enumType)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(enumType);
            foreach (var enumItem in enumItems)
            {
                CheckedListBoxItem item = new CheckedListBoxItem(enumItem.Value, enumItem.Text);
                checkedListBoxControl.Items.Add(item);
            }
        }
        
        /// <summary>
        /// 设置多选框内的选项
        /// </summary>
        /// <param name="authority">字段权限</param>
        /// <param name="authority">字段权限</param>
        public static void InitCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit, Type enumType)
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(enumType);
            checkedComboBoxEdit.Properties.Items.AddRange(enumItems.ToArray());
        }

        /// <summary>
        /// 设置多选框内的选项
        /// </summary>
        /// <param name="authority">字段权限</param>
        /// <param name="authority">字段权限</param>
        public static void InitCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit, IList<CommonNode> commonNodes)
        {
            checkedComboBoxEdit.Properties.Items.AddRange(commonNodes.ToArray());
        }

        /// <summary>
        /// 设置多选框内的选项
        /// </summary>
        /// <param name="checkedComboBoxEdit">多选框控件</param>
        public static void SetAllCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit)
        {
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem checkedListBoxItem in checkedComboBoxEdit.Properties.Items)
            {
                checkedListBoxItem.CheckState = CheckState.Checked;
            }
        }

        /// <summary>
        /// 取消多选框内的选项
        /// </summary>
        /// <param name="checkedComboBoxEdit">多选框控件</param>
        public static void CancelAllCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit)
        {
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem checkedListBoxItem in checkedComboBoxEdit.Properties.Items)
            {
                checkedListBoxItem.CheckState = CheckState.Unchecked;
            }
        }

        /// <summary>
        /// 设置多选框内的选项
        /// </summary>
        /// <param name="checkedComboBoxEdit">多选框控件</param>
        /// <param name="authority">字段权限</param>
        public static void SetCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit, Int64 authority)
        {
            foreach (CheckedListBoxItem checkedListBoxItem in checkedComboBoxEdit.Properties.Items)
            {
                EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                var result = authority;
                if (enumItem.Value > 0)
                {
                    result = authority >> enumItem.Value;
                }
                if ((result & 1L) == 1L)
                {
                    checkedListBoxItem.CheckState = CheckState.Checked;
                }
                else
                {
                    checkedListBoxItem.CheckState = CheckState.Unchecked;
                }
            }
        }

        /// <summary>
        /// 获取多选框内的选项
        /// </summary>
        /// <param name="checkedComboBoxEdit"></param>
        /// <returns></returns>
        public static Int64 GetCheckedComboBoxEditItems(CheckedComboBoxEdit checkedComboBoxEdit)
        {
            Int64 authority = 0;
            List<object> checkedValues = checkedComboBoxEdit.Properties.Items.GetCheckedValues();
            foreach (object obj in checkedValues)
            {
                EnumItem enumItem = (EnumItem)obj;
                if (enumItem.Value > 0)
                {
                    authority = (1L << enumItem.Value) | authority;
                }
                else
                {
                    authority = 1L | authority;
                }
            }

            return authority;
        }

        /// <summary>
        /// 枚举类型图片列
        /// </summary>
        /// <param name="enumItems"></param>
        /// <param name="smallImages"></param>
        /// <returns></returns>
        public static RepositoryItemImageComboBox GetImageComboBoxOnColumnEdit(IList<EnumItem> enumItems, DevExpress.Utils.ImageCollection smallImages)
        {
            return GetImageComboBoxOnColumnEdit(enumItems, smallImages, false);
        }

        /// <summary>
        /// 枚举类型图片列
        /// </summary>
        /// <param name="enumItems"></param>
        /// <param name="smallImages"></param>
        /// <param name="firstSkipped"></param>
        /// <returns></returns>
        public static RepositoryItemImageComboBox GetImageComboBoxOnColumnEdit(IList<EnumItem> enumItems, DevExpress.Utils.ImageCollection smallImages, bool firstSkipped)
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();    
            repositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemImageComboBox.LookAndFeel.SkinName = "Money Twins";
            repositoryItemImageComboBox.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemImageComboBox.Name = "repositoryItemImageComboBox";
            repositoryItemImageComboBox.SmallImages = smallImages;          
            for (int i = 0; i < enumItems.Count; i++)
            {
                ImageComboBoxItem imageComboBoxItem = null;
                if (firstSkipped && i == 0)
                {
                    imageComboBoxItem = new ImageComboBoxItem(enumItems[i].Text, (byte)enumItems[i].Value, -1);
                }
                else
                {
                    imageComboBoxItem = new ImageComboBoxItem(enumItems[i].Text, (byte)enumItems[i].Value, i);
                }

                repositoryItemImageComboBox.Items.Add(imageComboBoxItem);
            }

            return repositoryItemImageComboBox;
        }

        /// <summary>
        /// Boolean 类型图片列
        /// </summary>
        /// <param name="firstItem"></param>
        /// <param name="secondItem"></param>
        /// <param name="smallImages"></param>
        /// <returns></returns>
        public static RepositoryItemImageComboBox GetImageComboBoxOnColumnEdit(BooleanItem firstItem, BooleanItem secondItem, object smallImages)
        {
            return GetImageComboBoxOnColumnEdit(firstItem, secondItem, smallImages, false);
        }

        /// <summary>
        /// Boolean 类型图片列
        /// </summary>
        /// <param name="firstItem"></param>
        /// <param name="secondItem"></param>
        /// <param name="smallImages"></param>
        /// <param name="firstSkipped"></param>
        /// <returns></returns>
        public static RepositoryItemImageComboBox GetImageComboBoxOnColumnEdit(BooleanItem firstItem, BooleanItem secondItem, object smallImages, bool firstSkipped)
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = false;
            repositoryItemImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            repositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            repositoryItemImageComboBox.LookAndFeel.SkinName = "Money Twins";
            repositoryItemImageComboBox.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemImageComboBox.Name = "repositoryItemImageComboBox";
            repositoryItemImageComboBox.SmallImages = smallImages;
            ImageComboBoxItem imageComboBoxItem = null;
            if (firstSkipped)
            {
                imageComboBoxItem = new ImageComboBoxItem(firstItem.Text, firstItem.Value, -1);
            }
            else
            {
                imageComboBoxItem = new ImageComboBoxItem(firstItem.Text, firstItem.Value, 0);
            }
            repositoryItemImageComboBox.Items.Add(imageComboBoxItem);

            imageComboBoxItem = new ImageComboBoxItem(secondItem.Text, secondItem.Value, 1);
            repositoryItemImageComboBox.Items.Add(imageComboBoxItem);

            return repositoryItemImageComboBox;
        }

        /// <summary>
        /// 获取多选框内的条件
        /// </summary>
        /// <param name="checkedComboBoxEdit"></param>
        /// <param name="whereConditons"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        public static void GetWhereConditons(CheckedComboBoxEdit checkedComboBoxEdit, IList<WhereConditon> whereConditons, string tableName, string dataFieldName)
        {
            IList<object> checkedValues = checkedComboBoxEdit.Properties.Items.GetCheckedValues();
            if (checkedValues.Count > 0 && checkedValues.Count < checkedComboBoxEdit.Properties.Items.Count)
            {
                for (int i = 0; i < checkedValues.Count; i++)
                {
                    EnumItem enumItem = checkedValues[i] as EnumItem;
                    if (i == 0)
                    {
                        if (checkedValues.Count == 1)
                        {
                            whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.Byte, enumItem.Value,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.Byte, enumItem.Value,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                        }
                    }
                    else if (i == checkedValues.Count - 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.Byte, enumItem.Value,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), System.Data.DbType.Byte, enumItem.Value,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
        }

        /// <summary>
        /// 获取多选框内的条件
        /// </summary>
        /// <param name="checkedComboBoxEdit"></param>
        /// <param name="whereConditons"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        public static void GetWhereConditonsByCommonNodes(CheckedComboBoxEdit checkedComboBoxEdit, IList<WhereConditon> whereConditons, string tableName, string dataFieldName)
        {
            IList<object> checkedValues = checkedComboBoxEdit.Properties.Items.GetCheckedValues();
            if (checkedValues.Count > 0 && checkedValues.Count < checkedComboBoxEdit.Properties.Items.Count)
            {
                for (int i = 0; i < checkedValues.Count; i++)
                {
                    CommonNode commonNode = checkedValues[i] as CommonNode;
                    if (i == 0)
                    {
                        if (checkedValues.Count == 1)
                        {
                            whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNode.NodeId,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNode.NodeId,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                        }
                    }
                    else if (i == checkedValues.Count - 1)
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Byte, commonNode.NodeId,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon(tableName, dataFieldName, string.Format("{0}{1}", dataFieldName, i), DbType.Decimal, commonNode.NodeId,
                            DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
        }


        /// <summary>
        /// 格式化表格中的数据
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="commonDataFieldInfo"></param>
        public static void SetColumnDisplayText(GridColumn gridColumn, CommonDataFieldInfo commonDataFieldInfo)
        {
            if (gridColumn == null)
            {
                return;
            }
            DataFieldProperty dataFieldProperty = (DataFieldProperty)commonDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.SystemPhysicalDataField:
                    SystemDataField systemDataField = (SystemDataField)Convert.ToByte(commonDataFieldInfo.DataFieldId);
                    switch (systemDataField)
                    {
                        case SystemDataField.CreationTime:
                        case SystemDataField.ModificationTime:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridColumn.DisplayFormat.FormatString = "F";
                            gridColumn.Width = 150;
                            break;
                    }
                    break;

                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonDataFieldInfo.DataFieldType;
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridColumn.DisplayFormat.FormatString = "F";
                            gridColumn.Width = 150;
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDay:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                            gridColumn.DisplayFormat.FormatString = "d";
                            break;

                        case PhysicalDataFieldType.YearAndMonth:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridColumn.DisplayFormat.FormatString = "y";
                            break;

                        case PhysicalDataFieldType.MonthAndDay:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridColumn.DisplayFormat.FormatString = "m";
                            break;

                        case PhysicalDataFieldType.Time:
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                            gridColumn.DisplayFormat.FormatString = "HH:mm:ss";
                            break;

                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:
                        case PhysicalDataFieldType.SecondaryAssociation:
                            if (gridColumn.ColumnType == typeof(DateTime))
                            {
                                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                                gridColumn.DisplayFormat.FormatString = "d";
                            }
                            break;
                    }
                    break;

                //case DataFieldProperty.LogicalDataField:
                //    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)commonDataFieldInfo.DataFieldType;
                //    switch (logicalDataFieldType)
                //    {
                //        case LogicalDataFieldType.DateTimeExpression:
                //            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                //            gridColumn.DisplayFormat.FormatString = "F";
                //            break;
                //    }
                //    break;
            }
        }

        ///// <summary>
        ///// 格式化表格中的数据
        ///// </summary>
        //public static void SetColumnDisplayText(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.Column.FieldName == "AuditedStatus")
        //    {
        //        AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(e.Value);
        //        e.DisplayText = UserEnumHelper.GetEnumText(auditedStatus);
        //    }
        //    else if (e.Column.FieldName == "CurrentState")
        //    {
        //        CurrentState currentState = (CurrentState)Convert.ToByte(e.Value);
        //        e.DisplayText = UserEnumHelper.GetEnumText(currentState);
        //    }
        //}
                
    }
}
