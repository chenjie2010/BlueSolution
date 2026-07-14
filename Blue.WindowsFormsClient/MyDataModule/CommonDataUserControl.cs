using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WindowsFormsClient.MyDataModule
{
    public partial class CommonDataUserControl : UserControl
    {       
        #region 属性

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 数据填充对象
        /// </summary>
        public CustomDataInfo CustomDataInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 业务实例契约
        /// </summary>
        public IBusinessInstanceContract BusinessInstanceContract
        {
            get;
            set;
        }

        /// <summary>
        /// 返回主界面
        /// </summary>
        public GoBackDelegate GoBack
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonDataUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonDataUserControl_Load(object sender, EventArgs e)
        {            
            ShowStatisticData();
        }        

        /// <summary>
        /// 填报操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            Create(false);
        }

        /// <summary>
        /// 第三方创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (ExtendedCustomBusinessInfo.ThirdModeEnabled)
            {
                Create(true);
            }            
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkRefresh_Click(object sender, EventArgs e)
        {
            ShowStatisticData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkTimeSubmittedValue_Click(object sender, EventArgs e)
        {
            ShowDataFilledListForm(DataSumbittedState.Review);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkDraftValue_Click(object sender, EventArgs e)
        {
            ShowDataFilledListForm(DataSumbittedState.Drfat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCompletedValue_Click(object sender, EventArgs e)
        {
            ShowDataFilledListForm(DataSumbittedState.Completed);
        }

        /// <summary>
        /// 查看所有的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkViewAll_Click(object sender, EventArgs e)
        {
            DataFilledListForm frmDataFilledList = new DataFilledListForm()
            {
                AllLoaded = true,
                ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo,
                CustomDataInfo = CustomDataInfo
            };
            frmDataFilledList.ShowDialog();
        }

        /// <summary>
        /// 返回数据填报主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkBack_Click(object sender, EventArgs e)
        {
            GoBack?.Invoke();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 显示业务基本信息
        /// </summary>
        /// <param name="dataSumbittedInfo"></param>
        public void SetDataOnControls(DataSumbittedInfo dataSumbittedInfo)
        {
            gcMain.Text = dataSumbittedInfo.BusinessName;
            meContent.Text = dataSumbittedInfo.BusinessIntro;
            lblInitialTimeValue.Text = DataConvertionHelper.EndowStringOfDate(dataSumbittedInfo.InitialTime);
            lblExpiredTimeValue.Text = DataConvertionHelper.EndowStringOfDate(dataSumbittedInfo.ExpiredTime);
            hlnkCondition.Enabled = dataSumbittedInfo.ConditionEnabled;
            hlnkHelp.Enabled = dataSumbittedInfo.HelpEnabled;
            btnSumbit.Enabled = dataSumbittedInfo.BusinessEnabled;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示数据填报窗体
        /// </summary>
        /// <param name="thirdParty"></param>
        private void ShowDataFilledInstanceForm(bool thirdParty)
        {
            try
            {
                decimal parentUserId = decimal.MinValue;
                if (thirdParty)
                {
                    UserListForm frmUserList = new UserListForm();
                    frmUserList.GetIdentifier = (userId) =>
                    {
                        parentUserId = userId;
                    };
                    frmUserList.ShowDialog();
                }
                else
                {
                    parentUserId = CurrentUser.Instance.UserId;
                }
                if (parentUserId > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    DataFilledInstanceForm frmDataTemplateTab = new DataFilledInstanceForm()
                    {
                        ParentUserId = parentUserId,
                        Text = ExtendedCustomBusinessInfo.BusinessName,
                        CustomDataInfo = CustomDataInfo,
                        InstanceId = 0
                    };
                    frmDataTemplateTab.CloseForm = () =>
                    {
                        ShowStatisticData();
                    };
                    Cursor = Cursors.Default;
                    frmDataTemplateTab.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 显示统计数据
        /// </summary>
        private void ShowStatisticData()
        {
            hlnkTimeSubmittedValue.Text = string.Format("共有{0}件",
                BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.DataId, DataSumbittedState.Review));

            hlnkDraftValue.Text = string.Format("共有{0}件",
                BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.DataId, DataSumbittedState.Drfat));

            hlnkCompletedValue.Text = string.Format("共有{0}件",
                BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.DataId, DataSumbittedState.Completed));
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dataSumbittedState"></param>
        private void ShowDataFilledListForm(DataSumbittedState dataSumbittedState)
        {
            DataFilledListForm frmDataFilledList = new DataFilledListForm();
            frmDataFilledList.AllLoaded = false;
            frmDataFilledList.ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo;
            frmDataFilledList.CustomDataInfo = CustomDataInfo;
            frmDataFilledList.DataSumbittedState = dataSumbittedState;
            frmDataFilledList.ShowDialog();
        }


        private void Create(bool thirdParty)
        {
            if (ExtendedCustomBusinessInfo != null)
            {
                BusinessMenu businessMenu = (BusinessMenu)ExtendedCustomBusinessInfo.BusinessMenu;
                switch (businessMenu)
                {
                    case BusinessMenu.UserData:
                        if (CustomDataInfo.EnableGuidance)
                        {
                            DataGuidanceForm frmDataGuidance = new DataGuidanceForm()
                            {
                                AttachmentId = CustomDataInfo.DataId,
                                AttachmentCategory = AttachmentCategory.DataFilled,
                                Content = CustomDataInfo.Guidance,
                                BottomVisible = true
                            };
                            frmDataGuidance.DataSumbitted = (hasAlreadyRead) =>
                            {
                                if (!hasAlreadyRead) return;
                                frmDataGuidance.Hide();
                                ShowDataFilledInstanceForm(thirdParty);
                            };
                            frmDataGuidance.ShowDialog();
                        }
                        else
                        {
                            ShowDataFilledInstanceForm(thirdParty);
                        }
                        break;
                }
            }
        }

        #endregion       
    }
}
