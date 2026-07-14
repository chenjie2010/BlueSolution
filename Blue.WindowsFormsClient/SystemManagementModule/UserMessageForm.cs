using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsControls;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.GeneralAffairModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserMessageForm : Form
    {
        #region 契约接口
        
        private readonly IUserMessageContract userMessageContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserMessageForm()
        {
            InitializeComponent();
            userMessageContract = SystemChannelFactory.CreateUserMessageContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserMessageForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            /* 1. 加载用户数据 */
            this.BeginInvoke(new MethodInvoker(LoadData));
        }

        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowDoubleClick(object sender, RowEvent e)
        {
            decimal messageId = DataConvertionHelper.GetDecimal(e.Value);
            ShowMessage(messageId);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            LoadData();
        }
        
        /// <summary>
        /// 自定义显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "MessageType")
            {
                MessageType messageType = (MessageType)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(messageType);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="messageId"></param>
        private void ShowMessage(decimal messageId)
        {
            /* 1. 邮件内容 */
            UserMessageInfo userMessageInfo = userMessageContract.GetModelInfo(messageId);
            ReadMessageForm readMessageForm = new ReadMessageForm();
            readMessageForm.UserMessageInfo = userMessageInfo;
            readMessageForm.ShowDialog();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            Application.DoEvents();
            int totalCount = 0;
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("RoleAndUser", "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("UserMessage", "IsDraft", "IsDraft", DbType.Boolean, false,
                              DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("UserMessage", "InitalTime", "InitalTime_0", DbType.DateTime, DateTime.Now,
                               DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
            whereConditons.Add(new WhereConditon("UserMessage", "InitalTime", "InitalTime_1", DbType.DateTime, null,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            whereConditons.Add(new WhereConditon("UserMessage", "ExpiredTime", "ExpiredTime", DbType.DateTime, DateTime.Now,
                                           DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
            whereConditons.Add(new WhereConditon("UserMessage", "ExpiredTime", "ExpiredTime", DbType.DateTime, null,
                                          DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("UserMessage", "MessagePriority", CustomSorting.Descending));
            sortingCondtions.Add(new SortingCondtion("UserMessage", "DeliveredTime", CustomSorting.Ascending));
            devExpressGrid.DataSource = userMessageContract.GetPageRecordOfMultiTables(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                    devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            devExpressGrid.RecordCount = totalCount;
            devExpressGrid.Columns["DeliveredTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devExpressGrid.Columns["DeliveredTime"].DisplayFormat.FormatString = "G";            
            InsertViewColumn();
        }

        /// <summary>
        /// 插入查看列
        /// </summary>
        /// <returns></returns>
        private void InsertViewColumn()
        {
            GridColumn gcView = devExpressGrid.Columns.Add();
            gcView.Caption = "查看";
            gcView.Name = "View";
            gcView.Width = 40;
            gcView.MinWidth = 40;
            gcView.VisibleIndex = 0;
            gcView.Visible = true;
            gcView.Fixed = FixedStyle.Left;
            gcView.OptionsColumn.ReadOnly = true;
            gcView.OptionsFilter.AllowAutoFilter = false;
            gcView.OptionsFilter.AllowFilter = false;
            gcView.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gcView.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit.NullText = "查看";
            gcView.ColumnEdit = repositoryItemHyperLinkEdit;
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
            {
                if (devExpressGrid.FocusedRowHandle >= 0)
                {
                    decimal messageId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                    ShowMessage(messageId);
                }
            };
        }

        #endregion

    }
}
