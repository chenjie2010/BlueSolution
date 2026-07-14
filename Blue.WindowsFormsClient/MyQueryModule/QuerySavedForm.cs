using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QuerySavedForm : Form
    {

        #region 契约接口

        private readonly IUserQueryContract userQueryContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有变量
        
        #endregion

        #region 属性
        
        /// <summary>
        /// 查询
        /// </summary>
        public QueryBuilder CurrentQueryBuilder
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QuerySavedForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userQueryContract = BusinessChannelFactory.CreateUserQueryContract();
            IList<CommonNode> commonNodes = customGroupContract.GetChildNodes(decimal.MinValue, (byte)GroupType.QueryStatement,CurrentUser.Instance.UserId);
            cmbQueryCategory.Properties.Items.AddRange(commonNodes.ToArray());
            cmbQueryCategory.SelectedIndex = 0;
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuerySavedForm_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string queryName = txtQueryName.Text.Trim();
                string note = txtNote.Text.Trim();
                if (cmbQueryCategory.EditValue == null)
                {
                    MessageBox.Show("请选中查询语句分类，如无查询语句分类，请先创建查询语句分类！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonNode commonNode = cmbQueryCategory.EditValue as CommonNode;
                Cursor = Cursors.WaitCursor;
                long tableNameRelation = 0L;
                byte pos = 0;
                foreach (DictionaryEntry keyValue in CurrentQueryBuilder.CurrentTableNames)
                {
                    long currentRealtion = 0L;
                    string value = keyValue.Value.ToString();
                    if (value.StartsWith("Inner"))
                    {
                        currentRealtion = 1 << pos;
                    }
                    else if (value.StartsWith("Left"))
                    {
                        currentRealtion = 2 << pos;
                    }
                    else if (value.StartsWith("Right"))
                    {
                        currentRealtion = 4 << pos;
                    }
                    else if (value.StartsWith("Full"))
                    {
                        currentRealtion = 8 << pos;
                    }
                    else
                    {
                        throw new ArgumentException("错误的参数");
                    }
                    pos += 4;
                    tableNameRelation = tableNameRelation  | currentRealtion;
                }
                if (tableNameRelation == 0)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("请先选择查询字段，然后才能保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                string nodeCode = customGroupContract.GetNodeCodeByNodeId(commonNode.NodeId);
                IList<string> childNodeCodes = userQueryContract.GetChildNodeCodes(commonNode.NodeId);
                int index = 1;
                do
                {
                    nodeCode = DataFieldHelper.GetNewCode(commonNode.NodeCode, index);
                    index++;
                } while (childNodeCodes.Contains(nodeCode));               
                UserQueryInfo userQueryInfo = new UserQueryInfo(0, commonNode.NodeId, CurrentUser.Instance.UserId, queryName, nodeCode, 0, 0, 
                    tableNameRelation, CurrentQueryBuilder.GroupBy, CurrentQueryBuilder.Distinct, CurrentQueryBuilder.ValidWhere, note, DateTime.Now, 0);
                string verifyResult = string.Empty;
                bool result = ValidationHelper.Validate<UserQueryInfo>(userQueryInfo, out verifyResult);
                if (result)
                {

                    IList<QueryAndDataFieldInfo> queryAndDataFieldInfos = GetQueryAndDataFieldInfo();     
                    userQueryContract.Insert(userQueryInfo, queryAndDataFieldInfos);
                    Cursor = Cursors.Default;
                    MessageBox.Show("查询保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 获得查询字段
        /// </summary>
        /// <returns></returns>
        private IList<QueryAndDataFieldInfo> GetQueryAndDataFieldInfo()
        {
            IList<QueryAndDataFieldInfo> queryAndDataFieldInfos = new List<QueryAndDataFieldInfo>(CurrentQueryBuilder.QueryFields.Count);
            foreach (QueryField queryField in CurrentQueryBuilder.QueryFields)
            {
                byte systemDataField = (byte)SystemDataField.UserName;
                decimal dataFieldId = queryField.DataFieldId;
                decimal tableId = queryField.DataTableId;
                if (queryField.DataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                {
                    systemDataField = Convert.ToByte(queryField.DataFieldId);
                    dataFieldId = decimal.MinValue;
                }
                QueryAndDataFieldInfo queryAndDataFieldInfo = new QueryAndDataFieldInfo(0, dataFieldId, 0, tableId, (byte)queryField.DataFieldProperty, systemDataField, queryField.DataFieldType,
                    queryField.Output, (byte)queryField.CustomAggregate, (byte)queryField.Sorting, queryField.Criteria, (byte)queryField.QueryDataFieldRealtion, 0);
                queryAndDataFieldInfos.Add(queryAndDataFieldInfo);
            }

            return queryAndDataFieldInfos;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询语句分类管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkUserQuery_Click(object sender, EventArgs e)
        {
            QueryManagementForm frmQueryManagement = new QueryManagementForm();
            frmQueryManagement.RefreshForm += () =>
                {
                    cmbQueryCategory.Properties.Items.Clear();
                    IList<CommonNode> commonNodes = customGroupContract.GetChildNodes(decimal.MinValue, (byte)GroupType.QueryStatement, CurrentUser.Instance.UserId);
                    cmbQueryCategory.Properties.Items.AddRange(commonNodes.ToArray());
                    cmbQueryCategory.SelectedIndex = 0;
                };
            frmQueryManagement.ShowDialog();
        }

        #endregion
    }
}
