//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataAutoBackup.cs
// 描述： 数据自动备份
// 作者：ChenJie 
// 编写日期：2019/05/18
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using FarPoint.Win.Spread;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.BusinessLogic.BusinessModule;

namespace Blue.DataCommutation
{
    /// <summary>
    /// 数据自动备份
    /// </summary>
    public class DataAutoBackup
    {
        #region 私有常量

        /* 每个表最大导出的记录数 */
        private const int MAX_RECORD_COUNT = 2000000;

        #endregion

        #region 私有变量

        private readonly DataBusinessHandler dataBusinessHandler = null;
        private readonly CustomAssociationHandler customAssociationHandler = null;
        private readonly CustomTableHandler customTableHandler = null;
        private readonly CustomDatabaseHandler customDatabaseHandler = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataAutoBackup()
        {
            dataBusinessHandler = new DataBusinessHandler();
            customAssociationHandler = new CustomAssociationHandler();
            customTableHandler = new CustomTableHandler();
            customDatabaseHandler = new CustomDatabaseHandler();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 备份方法
        /// </summary>
        /// <param name="dataRange"></param>
        public void Backup(Int64 dataRange)
        {
            try
            {
                string directoryPath = AppSettingHelper.DataBackupDirectory;
                //directoryPath = @"E:\4.0测试数据";
                if (string.IsNullOrWhiteSpace(directoryPath) || !Directory.Exists(directoryPath))
                {
                    AppSettingHelper.BackupException = "数据自动备份目录为空，备份失败。请设置数据自动备份目录后重启服务。";
                    return;
                }
                string directoryName = string.Format("数据自动备份_{0}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                string filePath = string.Empty;
                List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(BackupDataRange));
                foreach (var enumItem in enumItems)
                {
                    if (!AuthorityHelper.CheckAuthority(dataRange, enumItem.Value)) continue;
                    BackupDataRange backupDataRange = (BackupDataRange)enumItem.Value;
                    switch (backupDataRange)
                    {
                        case BackupDataRange.System:
                            filePath = string.Format(@"{0}\{1}\系统表", directoryPath, directoryName);
                            List<EnumItem> systemTables = UserEnumHelper.GetEnumItems(typeof(SystemTable));
                            foreach (var systemTable in systemTables)
                            {
                                SystemTable systemTableName = (SystemTable)systemTable.Value;
                                string name = UserEnumHelper.GetEnumText(systemTableName);
                                SaveExcel(systemTableName, filePath, name);
                            }
                            break;

                        case BackupDataRange.UserPhoto:
                            break;

                        case BackupDataRange.ReportTemplate:
                            break;

                        case BackupDataRange.First:
                            filePath = string.Format(@"{0}\{1}\{2}", directoryPath, directoryName, UserEnumHelper.GetEnumText(DataWarehouse.FirstWarehouse));
                            SaveExcel(DataWarehouse.FirstWarehouse, filePath);
                            break;

                        case BackupDataRange.Second:
                            filePath = string.Format(@"{0}\{1}\{2}", directoryPath, directoryName, UserEnumHelper.GetEnumText(DataWarehouse.SecondWarehouse));
                            SaveExcel(DataWarehouse.SecondWarehouse, filePath);
                            break;

                        case BackupDataRange.Third:
                            filePath = string.Format(@"{0}\{1}\{2}", directoryPath, directoryName, UserEnumHelper.GetEnumText(DataWarehouse.ThirdWarehouse));
                            SaveExcel(DataWarehouse.ThirdWarehouse, filePath);
                            break;

                        case BackupDataRange.Fourth:
                            filePath = string.Format(@"{0}\{1}\{2}", directoryPath, directoryName, UserEnumHelper.GetEnumText(DataWarehouse.FourthWarehouse));
                            SaveExcel(DataWarehouse.FourthWarehouse, filePath);
                            break;

                        case BackupDataRange.Fifth:
                            filePath = string.Format(@"{0}\{1}\{2}", directoryPath, directoryName, UserEnumHelper.GetEnumText(DataWarehouse.FifthWarehouse));
                            SaveExcel(DataWarehouse.FifthWarehouse, filePath);
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="systemTableName"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        private void SaveExcel(SystemTable systemTableName, string filePath, string fileName)
        {
            int totalRecordCount = 0;
            int pageSize = 20000, index = 0;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (FpSpread fsExcel = new FpSpread())
            {
                IList<decimal> associationIds = new List<decimal>();
                string fileFullName = string.Format(@"{0}\{1}.xlsx", filePath, fileName);
                do
                {
                    SheetView sheet = sheet = new SheetView();
                    sheet.SheetName = string.Format("Sheet{0}", fsExcel.Sheets.Count + 1);
                    fsExcel.Sheets.Add(sheet);
                    DataSet ds = dataBusinessHandler.GetAuthorizedData(systemTableName, pageSize * index, pageSize,
                        null, null, ref totalRecordCount);
                    if (systemTableName == SystemTable.Association)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            associationIds.Add(Convert.ToDecimal(dr[0]));
                        }
                    }
                    if (ds.Tables[0].Columns.Contains("RecordId"))
                    {
                        ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["RecordId"]);
                    }
                    sheet.DataSource = ds;
                    for (int i = 0; i < sheet.ColumnCount; i++)
                    {
                        sheet.ColumnHeader.SetClip(0, i, 1, 1, ds.Tables[0].Columns[i].Caption);
                    }
                    index++;
                } while ((pageSize * index) < totalRecordCount);
                if (systemTableName == SystemTable.Association)
                {
                    int idx = fsExcel.Sheets.Count;
                    foreach (var associationId in associationIds)
                    {
                        SheetView sheetView = new SheetView();
                        sheetView.SheetName = string.Format("Sheet{0}", ++idx);
                        fsExcel.Sheets.Add(sheetView);
                        DataTable dtAssociation = customAssociationHandler.GetAssociationData(associationId);
                        sheetView.DataSource = dtAssociation;
                    }
                }
                fsExcel.SaveExcel(fileFullName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat | FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dataWarehouse"></param>
        /// <param name="filePath"></param>
        private void SaveExcel(DataWarehouse dataWarehouse, string filePath)
        {
            int totalRecordCount = 0;
            int pageSize = 20000;            
            IList<decimal> databaseIds = customDatabaseHandler.GetChildNodeIds((byte)dataWarehouse);
            using (FpSpread fsExcel = new FpSpread())
            {
                foreach (var databaseId in databaseIds)
                {
                    string databaseName = customDatabaseHandler.GetDatabaseName(databaseId);
                    List<CommonNode> commonNodes = customTableHandler.GetCommonNodesByDatabaseId(databaseId);
                    foreach (var commonNode in commonNodes)
                    {
                        string newFilePath = string.Format(@"{0}\{1}", filePath, databaseName);
                        if (!Directory.Exists(newFilePath))
                        {
                            Directory.CreateDirectory(newFilePath);
                        }
                        string fileFullName = string.Format(@"{0}\{1}.xlsx", newFilePath, commonNode.NodeName);
                        try
                        {
                            int index = 0;
                            do
                            {
                                SheetView sheet = sheet = new SheetView();
                                sheet.SheetName = string.Format("Sheet{0}", fsExcel.Sheets.Count + 1);
                                fsExcel.Sheets.Add(sheet);
                                DataSet ds = dataBusinessHandler.GetPageRecord(commonNode.NodeId, pageSize * index, pageSize, null, ref totalRecordCount);
                                if (ds.Tables[0].Rows.Count == 0) break;
                                if (ds.Tables[0].Columns.Contains("RecordId"))
                                {
                                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["RecordId"]);
                                }
                                sheet.DataSource = ds;
                                for (int i = 0; i < sheet.ColumnCount; i++)
                                {
                                    sheet.ColumnHeader.SetClip(0, i, 1, 1, ds.Tables[0].Columns[i].Caption);
                                }
                                index++;
                                if (pageSize * index > MAX_RECORD_COUNT)
                                {
                                    break;
                                }
                            } while ((pageSize * index) < totalRecordCount);
                            if (totalRecordCount > 0)
                            {
                                fsExcel.SaveExcel(fileFullName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat | FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
                            }
                            totalRecordCount = 0;
                            fsExcel.Sheets.Clear();
                        }
                        catch (Exception ex)
                        {
                            AppSettingHelper.BackupException = ex.Message;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
