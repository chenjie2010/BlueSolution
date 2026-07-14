using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using Blue.Model.SystemModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class SystemLogForm : Form
    {
        #region 私有变量

        private IList<WhereConditon> whereConditons = new List<WhereConditon>();

        #endregion

        #region 契约接口

        private readonly IUserLogContract userLogContract = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemLogForm()
        {
            InitializeComponent();
            userLogContract = SystemChannelFactory.CreateUserLogContract();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemLogForm_Load(object sender, EventArgs e)
        {
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbCategories, typeof(SoftwarePlatform));
            UserControlHelper.InitImageComboBoxEdit(icmbParentCategories, typeof(SoftwarePlatform));
            UserControlHelper.InitImageComboBoxEdit(icmbOperation, typeof(LogAction));
            UserControlHelper.InitImageComboBoxEdit(icmbLevel, typeof(LogLevel));
            this.WindowState = FormWindowState.Maximized;
            dtStart.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            dtEnd.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            LoadData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            whereConditons.Clear();
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClear_Click(object sender, EventArgs e)
        {
            whereConditons.Clear();
            txtUserInfo.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbCategories);
            dtStart.EditValue = null;
            dtEnd.EditValue = null;
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 删除选中日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (devExpressGrid.MultiSelectedValues.Count == 0)
                {
                    MessageBox.Show("请先选择需要批量删除的日志。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认批量删除所选择的日志吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    IList<decimal> logIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                    foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                    {
                        logIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Value));
                    }
                    Cursor = Cursors.WaitCursor;
                    userLogContract.Delete(logIds);
                    devExpressGrid.CurrentPageIndex = 0;
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("成功批量删除所选择的日志。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 删除查询日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDeleteByCondition_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (whereConditons.Count == 0)
                {
                    if (MessageBox.Show("确认删除所有日志吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("确认删除查询出来的日志吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    {
                        return;
                    }
                }
                Cursor = Cursors.WaitCursor;
                int count = userLogContract.Delete(whereConditons);
                devExpressGrid.CurrentPageIndex = 0;
                LoadData();
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("成功删除{0}条日志。", count), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 自定义值显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "LogEnumName")
            {
                LogTitle logTitle = (LogTitle)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(logTitle);
            }
        }

        /// <summary>
        /// 行选择显示日志信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                decimal logId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues["LogId"]);
                UserLogInfo userLogInfo = userLogContract.GetModelInfo(logId);
                if (userLogInfo != null)
                {
                    txtLogName.Text = UserEnumHelper.GetEnumText((LogTitle)userLogInfo.LogEnumName);
                    txtUserName.Text = DataConvertionHelper.GetString(devExpressGrid.GetRowCellValue(devExpressGrid.FocusedRowHandle, "UserName"));
                    txtUserActualName.Text = DataConvertionHelper.GetString(devExpressGrid.GetRowCellValue(devExpressGrid.FocusedRowHandle, "UserActualName"));
                    icmbParentCategories.EditValue = (byte)userLogInfo.LogClass;
                    txtBusinessName.Text = userLogInfo.BusinessName;
                    icmbOperation.EditValue = (byte)userLogInfo.LogAction;
                    icmbLevel.EditValue = (byte)userLogInfo.LogLevel;
                    deLogTime.EditValue = userLogInfo.LogDate;
                    txtDescription.Text = string.Format("{0}（{1}）于{2}执行{3}操作，操作类型为{4}，该日志的级别为{5}(6)。",
                        txtUserName.Text, txtUserActualName.Text, userLogInfo.LogDate, txtLogName.Text,
                        UserEnumHelper.GetEnumText((LogAction)userLogInfo.LogAction), UserEnumHelper.GetEnumText((LogLevel)userLogInfo.LogLevel), userLogInfo.LogLevel);
                }
            }
            else
            {
                txtLogName.Text = string.Empty;
                txtUserName.Text = string.Empty;
                txtUserActualName.Text = string.Empty;
                icmbParentCategories.SelectedIndex = 0;
                txtBusinessName.Text = string.Empty;
                icmbOperation.SelectedIndex = 0;
                icmbLevel.SelectedIndex = 0;
                deLogTime.EditValue = null;
                txtDescription.Text = string.Empty;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 数据加载
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string userName = txtUserInfo.Text.Trim();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    userName = Regex.Replace(userName, " {1,}", "%");
                    whereConditons.Add(new WhereConditon("UserName", "UserName", System.Data.DbType.String, userName,
                       DataFieldCondition.Like, DataFieldInnerRealtion.None, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("UserActualName", "UserActualName", System.Data.DbType.String, userName,
                       DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                UserControlHelper.GetWhereConditons(ccmbCategories, whereConditons, "UserLog", "LogClass");
                if (dtStart.EditValue != null)
                {
                    whereConditons.Add(new WhereConditon("LogDate", "LogDate_0", System.Data.DbType.DateTime, dtStart.DateTime,
                                   DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                if (dtEnd.EditValue != null)
                {
                    whereConditons.Add(new WhereConditon("LogDate", "LogDate_1", System.Data.DbType.DateTime, dtStart.DateTime,
                                   DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }

                int totalCount = 0;
                DataTable dt = userLogContract.GetPageRecordOfMultiTables(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                    devExpressGrid.PageSize, whereConditons, ref totalCount).Tables[0];
                devExpressGrid.DataSource = dt;
                devExpressGrid.RecordCount = totalCount;
                IList<EnumItem> softwarePlatforms = UserEnumHelper.GetEnumItems(typeof(SoftwarePlatform));
                devExpressGrid.DevExpressGridView.Columns["LogClass"].ColumnEdit = UserControlHelper.GetImageComboBoxOnColumnEdit(softwarePlatforms, icCategories);
                devExpressGrid.DevExpressGridView.Columns["LogDate"].DisplayFormat.FormatString = "d";

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
