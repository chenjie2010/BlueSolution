using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using FarPoint.Win;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using DevExpress.XtraEditors.Controls;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataBackupForm : Form
    {
        #region 契约接口
        
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomCategoryContract customCategoryContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IDataBussinessContract dataBussinessContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly ISystemConfigContract systemConfigContract;
        private readonly ISystemPrvoiderContract systemPrvoiderContract;
        private readonly IUserMessageContract userMessageContract = null;

        #endregion

        #region 私有变量

        private readonly TreeViewHandler<TreeNode> treeViewHandler;
        private ICommonNodeContract commonNodeContract;
        private IList<byte> systemTables = null;
        private IList<decimal> customTables = null;
        private string backupName = string.Empty;
        private bool fullBackup = false;
        private string backupDir= string.Empty;
        private ProgressForm frmProgress = null;        

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataBackupForm()
        {
            InitializeComponent();
            systemPrvoiderContract = CommonFactory.CreateSystemPrvoiderContract();
            customCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            dataBussinessContract = BusinessChannelFactory.CreateDataBussinessContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userMessageContract = SystemChannelFactory.CreateUserMessageContract();
            commonNodeContract = customDatabaseContract;
            systemTables = new List<byte>();
            customTables = new List<decimal>();
            treeViewHandler = new TreeViewHandler<TreeNode>(trvTable);            
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBackupForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            UserControlHelper.InitImageComboBoxEdit(icmbPeriod, typeof(BackupPeriod));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataRange, typeof(BackupDataRange));
            string backupException = systemPrvoiderContract.GetBackupException();
            if (!string.IsNullOrWhiteSpace(backupException))
            {
                meWarning.Text = backupException;
            }
            string lastestBackupTime = systemPrvoiderContract.GetLastestBackupTime();
            if (!string.IsNullOrWhiteSpace(lastestBackupTime))
            {
                txtBackupTime.Text = lastestBackupTime;
            }
            byte autoBackup = DataConvertionHelper.GetConvertedByte(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.AutoBackup), 0);
            chkAutoBackup.Checked = Convert.ToBoolean(autoBackup);
            icmbPeriod.EditValue = DataConvertionHelper.GetConvertedByte(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.Period), 0);
            dtBackupDateTime.EditValue = DataConvertionHelper.GetConvertedDateTime(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.BackupDateTime), DateTime.Now.AddDays(1).Date);
            long range = DataConvertionHelper.GetConvertedLong(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.DataRange), 0L);
            UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataRange, range);
            btnApply.Enabled = false;

            txtBackupName.Text = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), CurrentUser.Instance.UserActualName);
            UserControlHelper.InitCheckedListBoxItems(chklstSystemTable, typeof(SystemTable));
            IList<CommonNode> commonNodes = new List<CommonNode>();
            IList<EnumItem> dataWarehouses = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
            foreach (EnumItem enumItem in dataWarehouses)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, string.Empty, false));
            }
            treeViewHandler.InitFirstLevelNodes(commonNodes);
        }
                
        /// <summary>
        /// 属性结构展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvTable_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;
                    if (databaseNodeType == DatabaseNodeType.Table)
                    {
                        commonNodes = customTableContract.GetCommonNodes(commonNode.NodeId, TableFilter.All);
                        foreach (CommonNode node in commonNodes)
                        {
                            node.IsLeaf = true;
                        }
                    }
                    else
                    {
                        commonNodes = commonNodeContract.GetChildNodes(commonNode.NodeId);
                    }
                    treeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 完全备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFullBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFullBackup.Checked)
            {
                chklstSystemTable.Enabled = false;
                trvTable.Enabled = false;
            }
            else
            {
                chklstSystemTable.Enabled = true;
                trvTable.Enabled = true;
            }
        }

        /// <summary>
        /// 开始备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnBackup_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认开始备份操作？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            systemTables.Clear();
            customTables.Clear();
            backupName = txtBackupName.Text.Trim();
            fullBackup = chkFullBackup.Checked;
            backupDir = txtBackupDir.Text.Trim();
            if (string.IsNullOrWhiteSpace(backupName))
            {
                MessageBox.Show("备份名称不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(backupDir))
            {
                MessageBox.Show("备份目录不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.IO.Directory.Exists(backupDir))
            {
                MessageBox.Show("备份目录并不存在，请重新选择！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string path = string.Format(@"{0}\{1}", backupDir, backupName);
            if (System.IO.Directory.Exists(path))
            {
                if (MessageBox.Show("该备份已经存在，继续操作会清除该备份数据，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    Directory.Delete(path, true);
                }
                catch
                {
                    MessageBox.Show("该备份删除失败，请手工删除该备份！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!fullBackup)
            {
                foreach (CheckedListBoxItem item in chklstSystemTable.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        systemTables.Add(Convert.ToByte(item.Value));
                    }
                }
                foreach (TreeNode tnFirstLevel in trvTable.Nodes)
                {
                    foreach (TreeNode tnScdLevel in tnFirstLevel.Nodes)
                    {
                        foreach (TreeNode tnTrdLevel in tnScdLevel.Nodes)
                        {
                            foreach (TreeNode treeNode in tnTrdLevel.Nodes)
                            {
                                if (!string.IsNullOrWhiteSpace(treeNode.Text) && treeNode.Checked)
                                {
                                    CommonNode commonNode = treeNode.Tag as CommonNode;
                                    customTables.Add(commonNode.NodeId);
                                }
                            }
                        }
                    }
                }
                if (systemTables.Count == 0 && customTables.Count == 0)
                {
                    MessageBox.Show("请先选择需要备份的表！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemTable));
                    foreach (EnumItem enumItem in enumItems)
                    {
                        systemTables.Add((byte)enumItem.Value);
                    }
                    IList<CommonNode> commonNodes = customTableContract.GetCommonNodes();
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        customTables.Add(commonNode.NodeId);
                    }
                    Cursor = Cursors.Default;
                }
                catch (Exception exception)
                {
                    Cursor = Cursors.Default;
                    //记录日志, 不抛出异常, 包装异常
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }            
            Backup();            
            MessageBox.Show("备份成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       
        /// <summary>
        /// 浏览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnExplorer_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtBackupDir.Text = folderBrowserDialog.SelectedPath;
            }
        }

       /// <summary>
       /// 查看
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void sbtnView_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
            {
                openFileDialog.InitialDirectory = Application.StartupPath;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(openFileDialog.FileName) && Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExcel.Text = openFileDialog.FileName;
                if (!string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {
                    fpSpreadView.OpenExcel(openFileDialog.FileName, FarPoint.Excel.ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                }
            }
        }

        /// <summary>
        /// 清除表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认清除正在浏览的数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            txtExcel.Text = string.Empty;
            fpSpreadView.Reset();
        }

       /// <summary>
       /// 选择表
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void trvTable_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                e.Node.ExpandAll();
            }
            foreach(TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
        }

        /// <summary>
        /// 树形结构展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvTable_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }


        /// <summary>
        /// 自动备份变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 备份周期变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 首次备份时间变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBackupDateTime_EditValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
        
        /// <summary>
        /// 备份数据范围变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbDataRange_EditValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }


        /// <summary>
        /// 保存值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, SystemConfigInfo> systemConfigInfosUpdated = new Dictionary<int, SystemConfigInfo>();
                string autoBackup = chkAutoBackup.Checked ? "1" : "0";
                systemConfigInfosUpdated.Add((int)SystemConfigKeyName.AutoBackup, 
                    new SystemConfigInfo((int)SystemConfigKeyName.AutoBackup, autoBackup, SystemConfigCategory.Backup, DateTime.Now));
                BackupPeriod backupPeriod = (BackupPeriod)Convert.ToByte(icmbPeriod.EditValue);
                string period = DataConvertionHelper.GetConvertedString(icmbPeriod.EditValue);
                systemConfigInfosUpdated.Add((int)SystemConfigKeyName.Period,
                    new SystemConfigInfo((int)SystemConfigKeyName.Period, period, SystemConfigCategory.Backup, DateTime.Now));

                if (dtBackupDateTime.EditValue == null)
                {
                    MessageBox.Show("首次备份时间不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime backupDateTime = Convert.ToDateTime(dtBackupDateTime.EditValue);
                DateTime serverTime = systemPrvoiderContract.GetServerTime();
                if (backupDateTime <= serverTime.Add(new TimeSpan(0, 1, 0)))
                {
                    MessageBox.Show("请将首次备份时间设置为比当前时间晚1分钟以上。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string backupDateTimeValue = DataConvertionHelper.GetConvertedString(backupDateTime);
                systemConfigInfosUpdated.Add((int)SystemConfigKeyName.BackupDateTime,
                    new SystemConfigInfo((int)SystemConfigKeyName.BackupDateTime, backupDateTimeValue, SystemConfigCategory.Backup, DateTime.Now));

                Int64 range = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataRange);
                string rangeValue = range.ToString();
                systemConfigInfosUpdated.Add((int)SystemConfigKeyName.DataRange,
                    new SystemConfigInfo((int)SystemConfigKeyName.DataRange, rangeValue, SystemConfigCategory.Backup, DateTime.Now));
                
                systemConfigContract.UpdateSystemConfigInfos(systemConfigInfosUpdated);
                systemPrvoiderContract.ExecuteAutoBackupData(chkAutoBackup.Checked, backupDateTime, backupPeriod, range);
                btnApply.Enabled = false;
            }
            catch (Exception exception)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 备份
        /// </summary>
        private void Backup()
        {
            try
            {
                frmProgress = new ProgressForm();
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = systemTables.Count + customTables.Count + 1;
                frmProgress.Show();
                frmProgress.Tip = "正准备导出数据...";
                frmProgress.IncreaseStep();
                Application.DoEvents();
                string filePath = string.Empty;
                using (FpSpread fsExcel = new FpSpread())
                {
                    bool exit = false;
                    foreach (byte systemTable in systemTables)
                    {
                        SystemTable systemTableName = (SystemTable)systemTable;
                        filePath = string.Format(@"{0}\{1}\系统表", backupDir, backupName);
                        string name = UserEnumHelper.GetEnumText(systemTableName);
                        frmProgress.Tip = string.Format("正在导出{0}的数据...", name);
                        Application.DoEvents();
                        SaveExcel(fsExcel, 0, systemTableName, filePath, name);
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break; ;
                        }
                        frmProgress.IncreaseStep();
                    }
                    if (exit)
                    {
                        frmProgress.CloseFrom();
                        frmProgress = null;
                        return;
                    }
                    foreach (decimal tableId in customTables)
                    {
                        CustomTableInfo customTableInfo = customTableContract.GetModelInfo(tableId);
                        decimal databaseId = customTableContract.GetDatabaseId(tableId);
                        CustomDatabaseInfo customDatabaseInfo = customDatabaseContract.GetModelInfo(databaseId);
                        string dataWarehouseName = UserEnumHelper.GetEnumText((DataWarehouse)customDatabaseInfo.DataWarehouseId);
                        filePath = string.Format(@"{0}\{1}\{2}\{3}", backupDir, backupName, dataWarehouseName, customDatabaseInfo.DatabaseName);
                        frmProgress.Tip = string.Format("正在导出{0}的数据...", customTableInfo.LogicalName);
                        Application.DoEvents();
                        SaveExcel(fsExcel, tableId, SystemTable.User, filePath, customTableInfo.LogicalName);
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break;
                        }
                        frmProgress.IncreaseStep();
                    }
                    frmProgress.CloseFrom();
                    frmProgress = null;
                }
            }
            catch (Exception exception)
            {
                if (frmProgress != null && !frmProgress.IsDisposed)
                {
                    frmProgress.CloseFrom();
                }
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="fsExcel"></param>
        /// <param name="tableId"></param>
        /// <param name="systemTableName"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        private void SaveExcel(FpSpread fsExcel, decimal tableId, SystemTable systemTableName, string filePath, string fileName)
        {
            int totalRecordCount = 0;
            int pageSize = 20000, index = 0;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            IList<decimal> associationIds = new List<decimal>();
            fileName = string.Format(@"{0}\{1}.xlsx", filePath, fileName);
            do
            {
                SheetView sheet = null;
                if (index >= fsExcel.Sheets.Count)
                {
                    sheet = new SheetView();
                    sheet.SheetName = string.Format("Sheet{0}", fsExcel.Sheets.Count + 1);                    
                    fsExcel.Sheets.Add(sheet);
                }
                else
                {
                    sheet = fsExcel.Sheets[index];
                }
                DataSet ds = null;
                if (tableId <= 0)
                {
                    ds = dataBussinessContract.GetAuthorizedData(systemTableName, pageSize * index, pageSize,
                        null, null, ref totalRecordCount);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        associationIds.Add(Convert.ToDecimal(dr[0]));
                    }
                }
                else
                {
                    ds = dataBussinessContract.GetPageRecord(tableId, pageSize * index, pageSize, null, ref totalRecordCount);
                }
                if (ds.Tables[0].Columns.Contains("RecordId"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["RecordId"]);
                }
                sheet.DataSource = ds;
                Application.DoEvents();
                for (int i = 0; i < sheet.ColumnCount; i++)
                {
                    sheet.ColumnHeader.SetClip(0, i, 1, 1, ds.Tables[0].Columns[i].Caption);
                }
                index++;
            } while ((pageSize * index) < totalRecordCount);
            for (; index < fsExcel.Sheets.Count; index++)
            {
                fsExcel.Sheets.RemoveAt(index);
            }
            if (tableId <= 0 && systemTableName == SystemTable.Association)
            {
                int idx = fsExcel.Sheets.Count;
                foreach (var associationId in associationIds)
                {
                    SheetView sheetView = new SheetView();
                    sheetView.SheetName = string.Format("Sheet{0}", ++idx);
                    fsExcel.Sheets.Add(sheetView);
                    DataTable dtAssociation = customAssociationContract.GetAssociationData(associationId);
                    sheetView.DataSource = dtAssociation;
                }
            }
            fsExcel.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat | FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            Application.DoEvents();
            fsExcel.Reset();
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="databaseNode"></param>
        private void SetCommonNodeContract(DatabaseNodeType databaseNode)
        {
            /* 第一层为分组，第二层为数据表 */
            switch (databaseNode)
            {
                case DatabaseNodeType.Database:
                    commonNodeContract = customDatabaseContract;
                    break;

                case DatabaseNodeType.Category:
                    commonNodeContract = customCategoryContract;
                    break;

                case DatabaseNodeType.Table:
                    commonNodeContract = customTableContract;
                    break;

                default:
                    commonNodeContract = null;
                    break;
            }
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;

            /* 第一层为数据仓库，第二层为数据库，第三层为分组，第四层为数据表 */
            switch (level)
            {
                case 0:
                    nodeType = DatabaseNodeType.DataWarehouse;
                    break;

                case 1:
                    nodeType = DatabaseNodeType.Database;
                    break;

                case 2:
                    nodeType = DatabaseNodeType.Category;
                    break;

                case 3:
                    nodeType = DatabaseNodeType.Table;
                    break;
            }

            return nodeType;
        }

        /// <summary>
        /// 清除备份异常信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnlClearException_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认清除备份异常信息？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                systemPrvoiderContract.ClearBackupException();
                meWarning.Text = systemPrvoiderContract.GetBackupException();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(ex);
            }           
        }

        #endregion
    }
}
