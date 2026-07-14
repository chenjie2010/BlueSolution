using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class UserListControl : UserControl
    {
        #region 私有变量
        
        private AuthorityCondition authorityCondition = null;

        #endregion

        #region 契约属性

        /// <summary>
        /// 用户契约
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public IUserTypeContract UserTypeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public ICustomDepartmentContract CustomDepartmentContract
        {
            get;
            set;
        }


        #endregion

        #region 私有成员变量

        private Dictionary<string, string> _selecedUsers = null;

        #endregion

        #region 属性

        /// <summary>
        /// 当前选择的行的用户编号
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public decimal SelectedUserId
        {
            get
            {
                if (grdUsers.DataKeyValues != null)
                {
                    return Convert.ToDecimal(grdUsers.DataKeyValues[0]);
                }

                return decimal.MinValue;
            }
        }

        /// <summary>
        /// 当前多选的行的用户、编号
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public IList<RowEvent> MultiSelectedValues
        {
            get
            {
                return grdUsers.MultiSelectedValues;
            }
        }

        /// <summary>
        /// 当前多选的行的用户名、姓名
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public Dictionary<string, string> SelecedUsers
        {
            get
            {
                if (_selecedUsers == null)
                {
                    _selecedUsers = new Dictionary<string, string>();
                }
                else
                {
                    _selecedUsers.Clear();
                }
                for (int i = 0; i < grdUsers.RowCount; i++)
                {
                    if (grdUsers.GetCheckBoxValue(i))
                    {
                        _selecedUsers.Add(grdUsers.GetRowCellValue(i, "UserName").ToString(), grdUsers.GetRowCellValue(i, "UserActualName").ToString());
                    }
                }

                return _selecedUsers;
            }
        }

        /// <summary>
        /// 是否现在 CheckBox
        /// </summary>
        public bool IsShowCheckBox
        {
            set
            {
                grdUsers.IsShowCheckBox = value;
            }
            get
            {
                return grdUsers.IsShowCheckBox;
            }
        }

        /// <summary>
        /// 是否显示照片
        /// </summary>
        public bool IsPhotoShowed
        {
            get
            {
                return grdUsers.IsPhotoShowed;
            }
            set
            {
                grdUsers.IsPhotoShowed = value;
            }
        }

        /// <summary>
        /// 用户照片数据
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public byte[] ImageData
        {
            get
            {
                string userName = grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserName").ToString();
                return UserAccountContract.DownLoadPhoto(userName);
            }
        }
        #endregion

        #region 事件

        #region 定义"行单点击"事件

        /// <summary>
        /// 定义"行单点击"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowClick;

        /// <summary>
        /// 定义"行单点击"事件访问器
        /// </summary>
        [
        Description(@"点击""行单点击""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowEvent> OnRowClick
        {
            add
            {
                _OnRowClick += value;
            }
            remove
            {
                _OnRowClick -= value;
            }
        }

        /// <summary>
        /// 定义"行单点击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowClick(RowEvent e)
        {
            if (_OnRowClick != null) _OnRowClick(this, e);
        }

        #endregion

        #region 定义"行发生变化"事件
        /// <summary>
        /// 定义"行发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs> _OnFocusedRowChanged;

        /// <summary>
        /// 定义"行发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""行发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs> OnFocusedRowChanged
        {
            add
            {
                _OnFocusedRowChanged += value;
            }
            remove
            {
                _OnFocusedRowChanged -= value;
            }
        }

        /// <summary>
        /// 定义"行发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void FocusedRowChanged(DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_OnFocusedRowChanged != null) _OnFocusedRowChanged(this, e);
        }

        #endregion

        #region 定义"选择列发生变化"事件

        /// <summary>
        /// 定义"行发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs> _OnFocusedColumnChanged;

        /// <summary>
        /// 定义"行发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""行发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs> OnFocusedColumnChanged
        {
            add
            {
                _OnFocusedColumnChanged += value;
            }
            remove
            {
                _OnFocusedColumnChanged -= value;
            }
        }

        /// <summary>
        /// 定义"行发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void FocusedColumnChanged(DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (_OnFocusedColumnChanged != null) _OnFocusedColumnChanged(this, e);
        }

        #endregion

        #region 定义"数据加载"事件

        /// <summary>
        /// 定义"数据加载"事件
        /// </summary>
        private event EventHandler<EventArgs> _OnDataLoad;

        /// <summary>
        /// 定义"数据加载"事件访问器
        /// </summary>
        [
        Description(@"点击""数据加载""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnDataLoad
        {
            add
            {
                _OnDataLoad += value;
            }
            remove
            {
                _OnDataLoad -= value;
            }
        }

        /// <summary>
        /// 定义"数据加载"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void DataLoad(EventArgs e)
        {
            if (_OnDataLoad != null) _OnDataLoad(this, e);
        }

        #endregion

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserListControl()
        {
            InitializeComponent();            
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                authorityCondition = new AuthorityCondition(CustomDepartmentContract, UserTypeContract);
                IAsyncResult result = this.BeginInvoke(new MethodInvoker(() =>
                {
                    /* 1. 管理的用户类型 */
                    if (authorityCondition.RelatedUserTypeCommonNodes != null && authorityCondition.RelatedUserTypeCommonNodes.Count > 0)
                    {
                        cmbQueriedUserType.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedUserTypeCommonNodes);
                    }
                    else
                    {
                        cmbQueriedUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(CustomGroupContract, UserTypeContract);
                        cmbQueriedUserType.InitalizeTreeView();
                    }

                    /* 2. 管理的单位 */
                    if (authorityCondition.RelatedDepartmentCommonNodes != null && authorityCondition.RelatedDepartmentCommonNodes.Count > 0)
                    {
                        cmbQueriedDepartment.ShowSearch = false;
                        cmbQueriedDepartment.TreeViewHandler.InitFirstLevelNodes(authorityCondition.RelatedDepartmentCommonNodes);
                    }
                    else
                    {
                        cmbQueriedDepartment.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract);
                        cmbQueriedDepartment.InitalizeTreeView();
                    }
                }));
                //this.BeginInvoke(new MethodInvoker(LoadData));
            }
        }
             
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearQueriedOnControls();
            Query();
        }

        /// <summary>
        /// 行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e);
        }

        /// <summary>
        /// 列变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnFocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            FocusedColumnChanged(e);
        }

        /// <summary>
        /// 翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            grdUsers.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 回车查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Query();
                e.Handled = true;// 指示 KeyPress 事件已处理，去掉 Windows 缺省的叮当声。
            }
        }
           
        /// <summary>
        /// 行单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnRowClick(object sender, RowEvent e)
        {
            if (IsPhotoShowed)
            {
                LoadUserPhoto();
            }
            RowClick(e);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载用户图片
        /// </summary>
        private void LoadUserPhoto()
        {
            if (grdUsers.FocusedRowHandle >= 0)
            {
                string userName = DataConvertionHelper.GetString(grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserName"));
                byte[] data = UserAccountContract.DownLoadPhoto(userName);
                if (data != null)
                {
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        Image img = Image.FromStream(ms);
                        grdUsers.UserPhoto = img;
                    }
                }
                else
                {
                    grdUsers.UserPhoto = null;
                }
            }
        }

        /// <summary>
        /// 获得 WHERE 查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            List<WhereConditon> whereConditons = new List<WhereConditon>();
            string condition = txtCondition.Text;
            if (!string.IsNullOrWhiteSpace(condition))
            {
                string conditionValue = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, conditionValue,
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserAccount", "EmailAddress", "EmailAddress", DbType.String, conditionValue,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAccount", "UserIdentity", "UserIdentity", DbType.String, conditionValue,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                if (UserDataHelper.StartWithMobilePhoneNumber(condition))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "TelephoneNumber", "TelephoneNumber", DbType.String, conditionValue,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }                
                whereConditons.Add(new WhereConditon("UserAccount", "UserActualName", "UserActualName", DbType.String, conditionValue,
                   DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }
            List<WhereConditon> userWhereConditons = authorityCondition.GetWhereConditons(cmbQueriedUserType.Value as CommonNode, cmbQueriedDepartment.Value as CommonNode);
            if (userWhereConditons != null)
            {
                whereConditons.AddRange(userWhereConditons);
            }
            
            return whereConditons;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                progressPanel.Show();

                grdUsers.FocusedRowHandle = -1;
                /* 加载用户数据 */
                IList<WhereConditon> whereConditons = GetWhereConditons();
                int totalCount = 0;
                grdUsers.DataSource = UserAccountContract.GetUserList(grdUsers.PageSize * grdUsers.CurrentPageIndex,
                    grdUsers.PageSize, whereConditons, ref totalCount).Tables[0];
                grdUsers.RecordCount = totalCount;               
                if (grdUsers.RowCount > 0)
                {
                    grdUsers.FocusedRowHandle = 0;
                    if (IsPhotoShowed)
                    {
                        LoadUserPhoto();
                    }
                }
                DataLoad(new EventArgs());
                progressPanel.Hide();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                progressPanel.Hide();
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            grdUsers.CurrentPageIndex = 0;
            this.BeginInvoke(new MethodInvoker(LoadData));
        }

        /// <summary>
        /// 清除窗体左侧的控件中的数据
        /// </summary>
        private void ClearQueriedOnControls()
        {
            txtCondition.Text = string.Empty;
            cmbQueriedUserType.Value = null;
            cmbQueriedDepartment.Value = null;
        }

        #endregion

    }
}
