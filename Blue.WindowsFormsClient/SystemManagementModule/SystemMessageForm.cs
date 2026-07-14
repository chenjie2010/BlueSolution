using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
    public partial class SystemMessageForm : Form
    {
        #region 契约接口

        private readonly IPriavteAttachmentContract priavteAttachmentContract;
        private readonly IUserMessageContract userMessageContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemMessageForm()
        {
            InitializeComponent();
            priavteAttachmentContract = GeneralAffairChannelFactory.CreatePriavteAttachmentContract();
            userMessageContract = SystemChannelFactory.CreateUserMessageContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoticeForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            /* 1. 加载用户数据 */
            this.BeginInvoke(new MethodInvoker(LoadData));
        }
        
        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnBatchDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BatachDelete();
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmWrite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SendNoticeForm frmSendNotice = new SendNoticeForm();
            frmSendNotice.EditState = EditState.Add;
            frmSendNotice.LoadData = LoadData;
            frmSendNotice.ShowDialog();

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                SendNoticeForm frmSendNotice = new SendNoticeForm();
                frmSendNotice.EditState = EditState.Edit;
                frmSendNotice.MessageId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[0]);
                frmSendNotice.LoadData = LoadData;
                frmSendNotice.ShowDialog();
            }
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
            else if (e.Column.FieldName == "IsDraft")
            {
                e.DisplayText = Convert.ToBoolean(e.Value) ? "草稿" : "正式";
            }
            else if (e.Column.FieldName == "IsAttach")
            {
                e.DisplayText = Convert.ToBoolean(e.Value) ? "有附件" : "无附件";
            }
        }

        #endregion     

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            Application.DoEvents();
            int totalCount = 0;
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("UserMessage", "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            //whereConditons.Add(new WhereConditon("UserMessage", "MessageType", "MessageType", DbType.Byte, (byte)MessageType.Notice,
            //                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            devExpressGrid.DataSource = userMessageContract.GetPageRecord(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                    devExpressGrid.PageSize, whereConditons, ref totalCount).Tables[0];
            devExpressGrid.RecordCount = totalCount;
            devExpressGrid.Columns["InitalTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devExpressGrid.Columns["InitalTime"].DisplayFormat.FormatString = "G";
            devExpressGrid.Columns["ExpiredTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devExpressGrid.Columns["ExpiredTime"].DisplayFormat.FormatString = "G";
            devExpressGrid.Columns["DeliveredTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            devExpressGrid.Columns["DeliveredTime"].DisplayFormat.FormatString = "G";
            devExpressGrid.Authority = (byte)(GridViewAuthority.Delete) | DataConvertionHelper.GetConvertedByte(GridViewAuthority.BatchDelete);
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                if (MessageBox.Show("确认删除所选择的公告吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    IList<decimal> messageIds = new List<decimal>(1);
                    messageIds.Add(DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[0]));
                    userMessageContract.DeleteUserMessages(messageIds);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("请先选择需要删除的公告！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        private void BatachDelete()
        {
            if (MessageBox.Show("确认删除所选择的通告吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<decimal> messageIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                {
                    messageIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Value));
                }
                userMessageContract.DeleteUserMessages(messageIds);
                LoadData();
            }
        }

        #endregion        
    }
}
