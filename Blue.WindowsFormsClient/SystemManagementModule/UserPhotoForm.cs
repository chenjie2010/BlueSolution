using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WindowsFormsClient;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserPhotoForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 私有变量

        private DataTable dataTable = null;
        private CommonProgressForm frmCommonProgress;
        private bool hasLoaded = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserPhotoForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            dataTable = new DataTable();
            dataTable.Columns.Add("FileName", Type.GetType("System.String"));
            dataTable.Columns.Add("FilePath", Type.GetType("System.String"));
            dataTable.Columns.Add("FileDescription", Type.GetType("System.String"));
            dataTable.Columns[0].Caption = "图片名称";
            dataTable.Columns[1].Caption = "图片路径";
            dataTable.Columns[2].Caption = "上传结果描述";
            gridControl.DataSource = dataTable;
            gridView.Columns[1].Visible = false;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserPhotoForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "请选择用户照片所在文件夹";
            folderBrowserDialog.SelectedPath = WinPlatformHelper.GetFileFloder();                        
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    txtUserPhotoDir.Text = folderBrowserDialog.SelectedPath;
                    WinPlatformHelper.LastestFilePath = folderBrowserDialog.SelectedPath;
                    if (!bgwLoadUserPhoto.CancellationPending)
                    {
                        bgwLoadUserPhoto.RunWorkerAsync();
                        frmCommonProgress = new CommonProgressForm();
                        frmCommonProgress.CancelToTest = delegate ()
                        {
                            bgwLoadUserPhoto.CancelAsync();
                            lblTip.Text = "正在加载用户图片操作取消。";
                        };
                        frmCommonProgress.Text = "正在加载用户图片，请稍后.......";
                        frmCommonProgress.ShowDialog();
                        frmCommonProgress.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("取消加载用户图片正在进行中，请稍后重试。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception exception)
                {
                    if (!bgwLoadUserPhoto.CancellationPending)
                    {
                        bgwLoadUserPhoto.CancelAsync();
                    }
                    CloseProgressForm(frmCommonProgress);
                    Cursor = Cursors.Default;
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView.RowCount == 0)
                {
                    MessageBox.Show("用户图片列表为空，请选择用户图片目录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认上传图片？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    try
                    {
                        Dictionary<int, string> errorRows = new Dictionary<int, string>();
                        worker.WorkerSupportsCancellation = true;
                        worker.WorkerReportsProgress = true;
                        worker.DoWork += (workSender, ea) =>
                        {                            
                            for(int idx = 0; idx < gridView.RowCount; idx++)
                            {
                                try
                                {
                                    string fileName = Convert.ToString((gridView.GetDataRow(idx)["FileName"]));
                                    string filePath = Convert.ToString((gridView.GetDataRow(idx)["FilePath"]));
                                    string keyName = Path.GetFileNameWithoutExtension(fileName);
                                    string photoSuffixName = filePath.Substring(filePath.LastIndexOf('.') + 1).ToUpper();
                                    byte[] imageData = null;
                                    if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                                    {
                                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                        {
                                            BinaryReader r = new BinaryReader(fs);
                                            imageData = r.ReadBytes((int)fs.Length);
                                        }
                                        string userName = string.Empty;
                                        if (UserDataHelper.MatchIdentity(keyName))
                                        {
                                            userName = userAccountContract.GetUserNameByUserIdentity(keyName);
                                        }
                                        else
                                        {
                                            userName = keyName;
                                        }
                                        if (string.IsNullOrWhiteSpace(userName))
                                        {
                                            errorRows.Add(idx, "图片名称异常。");
                                        }
                                        else if (imageData.Length < AppSettingHelper.DefaultUserPhotoMinLength || imageData.Length > AppSettingHelper.DefaultUserPhotoMaxLength)
                                        {
                                            errorRows.Add(idx, string.Format("图片大小不符合要求（大小不能小于{0}K或是大于{1}MB）。", AppSettingHelper.DefaultUserPhotoMinLength /1024,
                                                AppSettingHelper.DefaultUserPhotoMaxLength / (1024 * 1024)));
                                        }
                                        else if (!userAccountContract.IsExistUserName(userName))
                                        {
                                            errorRows.Add(idx, "该图片名称对应的用户名不存在。");
                                        }
                                        else
                                        {
                                            userAccountContract.UpLoadPhoto(userName, photoSuffixName, fileName, imageData);
                                        }
                                        worker.ReportProgress(idx + 1);
                                    }
                                }
                                catch(Exception ex)
                                {
                                    errorRows.Add(idx, ex.Message);
                                }
                            }                                
                        };
                        worker.ProgressChanged += (workSender, ea) =>
                        {
                            lblTip.Text = string.Format("导入提示：正在上传第{0}张图片...", ea.ProgressPercentage);
                        };
                        worker.RunWorkerCompleted += (workSender, ea) =>
                        {
                            foreach (KeyValuePair<int, string> errorRow in errorRows)
                            {
                                gridView.SetRowCellValue(errorRow.Key, "FileDescription", errorRow.Value);
                            }
                            if (errorRows.Count == 0)
                            {
                                lblTip.Text = string.Format("导入提示：用户图片全部上传成功，总共上传{0}张。", gridView.RowCount);
                            }
                            else
                            {
                                lblTip.Text = string.Format("导入提示：用户图片部分上传成功，成功上传{0}张，失败：{1}张。", gridView.RowCount - errorRows.Count, errorRows.Count);
                            }
                            CloseProgressForm(frmCommonProgress);
                        };
                        if (!worker.CancellationPending)
                        {
                            worker.RunWorkerAsync();
                            frmCommonProgress = new CommonProgressForm();
                            frmCommonProgress.CancelToTest = delegate ()
                            {
                                worker.CancelAsync();
                                lblTip.Text = "上传用户图片操作取消。";
                            };
                            frmCommonProgress.Text = "正在上传用户图片，请稍后.......";
                            frmCommonProgress.ShowDialog();
                            frmCommonProgress.BringToFront();
                        }
                        else
                        {
                            MessageBox.Show("取消上传用户图片正在进行中，请稍后重试。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (!worker.CancellationPending)
                        {
                            worker.CancelAsync();
                        }
                        CloseProgressForm(frmCommonProgress);
                        Cursor = Cursors.Default;
                        WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                    }
        }
                hasLoaded = false;
            }
            catch (Exception exception)
            {                
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (hasLoaded && MessageBox.Show("确认放弃上传图片？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 读取照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLoadUserPhoto_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    var worker = sender as BackgroundWorker;                    
                    var files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".JPG", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith(".JPEG", StringComparison.CurrentCultureIgnoreCase)
                    || s.EndsWith(".BMP", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith("PNG", StringComparison.CurrentCultureIgnoreCase) || s.EndsWith("GIF", StringComparison.CurrentCultureIgnoreCase));
                    int idx = 1;
                    dataTable.Rows.Clear();
                    foreach (var file in files)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = Path.GetFileName(file);
                        dataRow[1] = file;
                        dataTable.Rows.Add(dataRow);
                        worker.ReportProgress(idx++);                        
                        if ((idx % 100) == 0)
                        {
                            Thread.Sleep(100);
                        }
                    }
                    hasLoaded = true;
                }
            }
            catch
            {                
                bgwLoadUserPhoto.CancelAsync();
            }
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLoadUserPhoto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gridControl.DataSource = dataTable;
            gridControl.RefreshDataSource();
            lblTip.Text = string.Format("导入提示：图片加载完成，总共加载{0}张图片。", gridView.RowCount);
            CloseProgressForm(frmCommonProgress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwLoadUserPhoto_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblTip.Text = string.Format("导入提示：正在加载第{0}张图片...", e.ProgressPercentage);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 关闭进度条窗口
        /// </summary>
        /// <param name="form"></param>
        private void CloseProgressForm(CommonProgressForm form)
        {
            if (form != null && !form.IsDisposed)
            {
                form.FormalClosed = true;
                form.Dispose();
            }
        }

        #endregion
    }
}
