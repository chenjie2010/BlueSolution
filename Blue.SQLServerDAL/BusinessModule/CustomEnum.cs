//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomEnum.cs
// 描述：CustomEnum 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Blue.CustomLibrary.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomEnum 表的数据层访问类
    /// </summary>
    public class CustomEnum : CommonNodeDataAccess, ICustomEnum
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomEnum() : base("CustomEnum", "EnumId", "ParentEnumId", "EnumName", "EnumCode", true, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomEnum 表中插入一条新记录
        /// </summary>
        /// <param name="customEnumInfo">customEnumInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomEnumInfo customEnumInfo)
        {
            //自动增加的关键字的值
            decimal customEnumId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customEnumInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomEnum", "Sorting", "ParentEnumId", customEnumInfo.ParentEnumId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomEnum(ParentEnumId, EnumName, EnumCode, EnumValue, FirstCode, SecondCode, ");
            sb.Append("FstAdditionalString, ScdAdditionalString, TrdAdditionalString, FourthAdditionalString, FifthAdditionalString, ");
            sb.Append("SixthAdditionalString, FstAdditionalInteger, ScdAdditionalInteger, FstAdditionalDecimal, ScdAdditionalDecimal, SuperEnumEnabled, ");
            sb.Append("IsLeaf, Sorting, Notes)");
            sb.Append("VALUES (@ParentEnumId, @EnumName, @EnumCode, @EnumValue, @FirstCode, @SecondCode, ");
            sb.Append("@FstAdditionalString, @ScdAdditionalString, @TrdAdditionalString, @FourthAdditionalString, @FifthAdditionalString, ");
            sb.Append("@SixthAdditionalString, @FstAdditionalInteger, @ScdAdditionalInteger, @FstAdditionalDecimal, @ScdAdditionalDecimal, @SuperEnumEnabled, ");
            sb.Append("@IsLeaf, @Sorting, @Notes);");
            sb.Append("SET @EnumId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "EnumId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "ParentEnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.ParentEnumId));
                        db.AddInParameter(dbCommand, "EnumName", DbType.String, customEnumInfo.EnumName);
                        db.AddInParameter(dbCommand, "EnumCode", DbType.String, customEnumInfo.EnumCode);
                        db.AddInParameter(dbCommand, "EnumValue", DbType.String, customEnumInfo.EnumValue);
                        db.AddInParameter(dbCommand, "FirstCode", DbType.String, customEnumInfo.FirstCode);
                        db.AddInParameter(dbCommand, "SecondCode", DbType.String, customEnumInfo.SecondCode);
                        db.AddInParameter(dbCommand, "FstAdditionalString", DbType.String, customEnumInfo.FstAdditionalString);
                        db.AddInParameter(dbCommand, "ScdAdditionalString", DbType.String, customEnumInfo.ScdAdditionalString);
                        db.AddInParameter(dbCommand, "TrdAdditionalString", DbType.String, customEnumInfo.TrdAdditionalString);
                        db.AddInParameter(dbCommand, "FourthAdditionalString", DbType.String, customEnumInfo.FourthAdditionalString);
                        db.AddInParameter(dbCommand, "FifthAdditionalString", DbType.String, customEnumInfo.FifthAdditionalString);
                        db.AddInParameter(dbCommand, "SixthAdditionalString", DbType.String, customEnumInfo.SixthAdditionalString);
                        db.AddInParameter(dbCommand, "FstAdditionalInteger", DbType.Int32, DataConvertionHelper.SetInt(customEnumInfo.FstAdditionalInteger));
                        db.AddInParameter(dbCommand, "ScdAdditionalInteger", DbType.Int32, DataConvertionHelper.SetInt(customEnumInfo.ScdAdditionalInteger));
                        db.AddInParameter(dbCommand, "FstAdditionalDecimal", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.FstAdditionalDecimal));
                        db.AddInParameter(dbCommand, "ScdAdditionalDecimal", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.ScdAdditionalDecimal));
                        db.AddInParameter(dbCommand, "SuperEnumEnabled", DbType.Boolean, customEnumInfo.SuperEnumEnabled);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customEnumInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customEnumInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customEnumId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@EnumId"].Value, 0);
                    }
                    if (customEnumInfo.ParentEnumId > 0)
                    {
                        UpdateLeafOfParentNode(customEnumInfo.ParentEnumId, false, db, transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customEnumId;
        }

        /// <summary>
		/// 获得 CustomEnumInfo 对象
		/// </summary>
		///<param name="enumId">枚举编号</param>
		/// <returns> CustomEnumInfo 对象</returns>
		public CustomEnumInfo GetModelInfo(decimal enumId)
        {
            CustomEnumInfo customEnumInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("EnumId", "EnumId", System.Data.DbType.Decimal, enumId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomEnumInfo> customEnumInfos = GetModelInfos(whereConditons, null, true);
            if (customEnumInfos != null && customEnumInfos.Count > 0)
            {
                customEnumInfo = customEnumInfos[0];
            }

            return customEnumInfo;
        }

        /// <summary>
        /// 更新 CustomEnumInfo 对象
        /// </summary>
        /// <param name="customEnumInfo">CustomEnumInfo 对象</param>
        public void Update(CustomEnumInfo customEnumInfo)
        {
            CustomEnumInfo oldCustomEnumInfo = GetModelInfo(customEnumInfo.EnumId);
            if (!oldCustomEnumInfo.EnumName.Equals(customEnumInfo.EnumName) ||
                !oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.EnumValue) ||
                !oldCustomEnumInfo.FirstCode.Equals(customEnumInfo.FirstCode) ||
                !oldCustomEnumInfo.SecondCode.Equals(customEnumInfo.SecondCode) ||
                !oldCustomEnumInfo.EnumName.Equals(customEnumInfo.FstAdditionalString) ||
                !oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.ScdAdditionalString) ||
                !oldCustomEnumInfo.FirstCode.Equals(customEnumInfo.TrdAdditionalString) ||
                !oldCustomEnumInfo.SecondCode.Equals(customEnumInfo.FourthAdditionalString) ||
                !oldCustomEnumInfo.EnumName.Equals(customEnumInfo.FifthAdditionalString) ||
                !oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.SixthAdditionalString) ||
                !oldCustomEnumInfo.FirstCode.Equals(customEnumInfo.FstAdditionalInteger) ||
                !oldCustomEnumInfo.SecondCode.Equals(customEnumInfo.ScdAdditionalInteger) ||
                !oldCustomEnumInfo.EnumName.Equals(customEnumInfo.FstAdditionalDecimal) ||
                !oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.ScdAdditionalDecimal))
            {
                CustomDataField customDataField = new CustomDataField();
                //1. 根据当前枚举，递归获得当前枚举的所有父枚举编号，然后更新以其父枚举为类型的字段需要更新的值
                //因为字段可能是父枚举或是祖先枚举编号为其类型
                IList<decimal> parentIds = GetParentEnumIds(customEnumInfo.EnumId);
                /* 1.1 下拉型枚举 */
                if (parentIds.Count > 0)
                {
                    decimal parentId = parentIds[0];
                    IList<CommonNode> commonNodes = customDataField.GetCommonNodesByEnumId(parentId);
                    UpdateEnumResult(commonNodes, oldCustomEnumInfo, customEnumInfo);
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonNode.NodeType;
                        string oldValue = string.Empty;
                        string newValue = string.Empty;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.EnumNameDependency:
                                if (!oldCustomEnumInfo.EnumName.Equals(customEnumInfo.EnumName))
                                {
                                    oldValue = oldCustomEnumInfo.EnumName;
                                    newValue = customEnumInfo.EnumName;
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.EnumValue:
                                if (!oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.EnumValue))
                                {
                                    oldValue = oldCustomEnumInfo.EnumValue;
                                    newValue = customEnumInfo.EnumValue;
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.FstAdditionalCode:
                                if (!oldCustomEnumInfo.FirstCode.Equals(customEnumInfo.FirstCode))
                                {
                                    oldValue = oldCustomEnumInfo.FirstCode;
                                    newValue = customEnumInfo.FirstCode;
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            case PhysicalDataFieldType.ScdAdditionalCode:
                                if (!oldCustomEnumInfo.SecondCode.Equals(customEnumInfo.SecondCode))
                                {
                                    oldValue = oldCustomEnumInfo.SecondCode;
                                    newValue = customEnumInfo.SecondCode;
                                }
                                break;
                        }
                        if (!string.IsNullOrWhiteSpace(oldValue))
                        {
                            UpdateEnumResult(commonNode.ParentNodeId, commonNode.NodeCode, newValue, oldValue);
                        }
                    }
                }

                /* 1.2 树形枚举 */
                foreach (decimal parentId in parentIds)
                {
                    IList<CommonNode> commonNodes = customDataField.GetCommonNodesByEnumId(parentId);
                    UpdateEnumResult(commonNodes, oldCustomEnumInfo, customEnumInfo);
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonNode.NodeType;
                        string oldValue = string.Empty;
                        string newValue = string.Empty;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.EnumNameDependency:
                                if (!oldCustomEnumInfo.EnumName.Equals(customEnumInfo.EnumName))
                                {
                                    oldValue = oldCustomEnumInfo.EnumName;
                                    newValue = customEnumInfo.EnumName;
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.EnumValue:
                                if (!oldCustomEnumInfo.EnumValue.Equals(customEnumInfo.EnumValue))
                                {
                                    oldValue = oldCustomEnumInfo.EnumValue;
                                    newValue = customEnumInfo.EnumValue;
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.FstAdditionalCode:
                                if (!oldCustomEnumInfo.FirstCode.Equals(customEnumInfo.FirstCode))
                                {
                                    oldValue = oldCustomEnumInfo.FirstCode;
                                    newValue = customEnumInfo.FirstCode;
                                }
                                break;

                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            case PhysicalDataFieldType.ScdAdditionalCode:
                                if (!oldCustomEnumInfo.SecondCode.Equals(customEnumInfo.SecondCode))
                                {
                                    oldValue = oldCustomEnumInfo.SecondCode;
                                    newValue = customEnumInfo.SecondCode;
                                }
                                break;
                        }
                        if (!string.IsNullOrWhiteSpace(oldValue))
                        {
                            UpdateEnumResult(commonNode.ParentNodeId, commonNode.NodeCode, newValue, oldValue);
                        }
                    }
                }
            }

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomEnum SET EnumName = @EnumName, EnumCode= @EnumCode, EnumValue = @EnumValue, ");
            sb.Append("FirstCode = @FirstCode, SecondCode = @SecondCode, FstAdditionalString = @FstAdditionalString, ScdAdditionalString = @ScdAdditionalString, ");
            sb.Append("TrdAdditionalString = @TrdAdditionalString, FourthAdditionalString = @FourthAdditionalString, FifthAdditionalString = @FifthAdditionalString, ");
            sb.Append("SixthAdditionalString = @SixthAdditionalString, FstAdditionalInteger = @FstAdditionalInteger, ScdAdditionalInteger = @ScdAdditionalInteger, ");
            sb.Append("FstAdditionalDecimal = @FstAdditionalDecimal, ScdAdditionalDecimal = @ScdAdditionalDecimal, SuperEnumEnabled = @SuperEnumEnabled, Notes = @Notes ");
            sb.Append("WHERE EnumId = @EnumId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.EnumId));
                    db.AddInParameter(dbCommand, "EnumName", DbType.String, customEnumInfo.EnumName);
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, customEnumInfo.EnumCode);
                    db.AddInParameter(dbCommand, "EnumValue", DbType.String, customEnumInfo.EnumValue);
                    db.AddInParameter(dbCommand, "FirstCode", DbType.String, customEnumInfo.FirstCode);
                    db.AddInParameter(dbCommand, "SecondCode", DbType.String, customEnumInfo.SecondCode);
                    db.AddInParameter(dbCommand, "FstAdditionalString", DbType.String, customEnumInfo.FstAdditionalString);
                    db.AddInParameter(dbCommand, "ScdAdditionalString", DbType.String, customEnumInfo.ScdAdditionalString);
                    db.AddInParameter(dbCommand, "TrdAdditionalString", DbType.String, customEnumInfo.TrdAdditionalString);
                    db.AddInParameter(dbCommand, "FourthAdditionalString", DbType.String, customEnumInfo.FourthAdditionalString);
                    db.AddInParameter(dbCommand, "FifthAdditionalString", DbType.String, customEnumInfo.FifthAdditionalString);
                    db.AddInParameter(dbCommand, "SixthAdditionalString", DbType.String, customEnumInfo.SixthAdditionalString);
                    db.AddInParameter(dbCommand, "FstAdditionalInteger", DbType.Int32, DataConvertionHelper.SetInt(customEnumInfo.FstAdditionalInteger));
                    db.AddInParameter(dbCommand, "ScdAdditionalInteger", DbType.Int32, DataConvertionHelper.SetInt(customEnumInfo.ScdAdditionalInteger));
                    db.AddInParameter(dbCommand, "FstAdditionalDecimal", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.FstAdditionalDecimal));
                    db.AddInParameter(dbCommand, "ScdAdditionalDecimal", DbType.Decimal, DataConvertionHelper.SetDecimal(customEnumInfo.ScdAdditionalDecimal));
                    db.AddInParameter(dbCommand, "SuperEnumEnabled", DbType.Boolean, customEnumInfo.SuperEnumEnabled);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customEnumInfo.Notes);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomEnumInfo 对象
        /// </summary>
        ///<param name="enumId">枚举编号</param>
        public void Delete(decimal enumId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomEnum ");
            sb.Append("WHERE EnumId = @EnumId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            bool updateLeaf = true;
            decimal parentEnumId = GetParentNodeId(enumId);
            int count = GetTotalCountOfChildNode(parentEnumId);
            if (count > 1)
            {
                updateLeaf = false;
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }
                    if (updateLeaf)
                    {
                        UpdateLeafOfParentNode(parentEnumId, true, db, transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 获得 CustomEnumInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomEnumInfo 对象列表</returns>
        public IList<CustomEnumInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomEnum 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomEnumInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomEnum ", "EnumId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 刷新排序
        /// </summary>
        public void RefreshSorting()
        {
            RefreshSorting(decimal.MinValue);
        }

        /// <summary>
        /// 获得枚举的单独项的值
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public string GetEnumText(decimal enumId, PhysicalDataFieldType physicalDataFieldType)
        {
            string enumText = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");

            try
            {
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.EnumValue:
                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                        sb.Append("EnumValue ");
                        break;

                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.FstAdditionalCode:
                        sb.Append("FirstCode ");
                        break;

                    case PhysicalDataFieldType.ScdAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        sb.Append("SecondCode ");
                        break;


                    case PhysicalDataFieldType.FstAdditionalString:
                        sb.Append("FstAdditionalString ");
                        break;

                    case PhysicalDataFieldType.ScdAdditionalString:
                        sb.Append("ScdAdditionalString ");
                        break;

                    case PhysicalDataFieldType.TrdAdditionalString:
                        sb.Append("TrdAdditionalString ");
                        break;

                    case PhysicalDataFieldType.FourthAdditionalString:
                        sb.Append("FourthAdditionalString ");
                        break;

                    case PhysicalDataFieldType.FifthAdditionalString:
                        sb.Append("FifthAdditionalString ");
                        break;

                    case PhysicalDataFieldType.SixthAdditionalString:
                        sb.Append("SixthAdditionalString ");
                        break;

                    default:
                        throw new ArgumentException("不支持该枚举类型。");
                }
                sb.Append(" FROM CustomEnum WHERE EnumId = @EnumId");
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(enumId));
                    enumText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumText;
        }

        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public string GetEnumText(decimal enumId)
        {
            string enumText = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT (EnumName + '~' + EnumValue + '~' + FirstCode + '~' + SecondCode + '~' + FstAdditionalString + '~' + ScdAdditionalString + '~' + TrdAdditionalString + '~' +");
            sb.Append("FourthAdditionalString + '~' + FifthAdditionalString + '~' + SixthAdditionalString + '~' + ISNULL(CONVERT(NVARCHAR(12), FstAdditionalInteger),'') + '~' + ");
            sb.Append("ISNULL(CONVERT(NVARCHAR(12), ScdAdditionalInteger),'') + '~' + ISNULL(CONVERT(NVARCHAR(14), FstAdditionalDecimal),'')+ '~' + ");
            sb.Append("ISNULL(CONVERT(NVARCHAR(14), ScdAdditionalDecimal),'')) AS EnumText FROM CustomEnum WHERE EnumId = @EnumId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(enumId));
                    enumText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumText;
        }

        /// <summary>
        /// 在下拉型枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        public KeyValueInfo GetDropDownListItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData)
        {
            KeyValueInfo keyValueInfo = null;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EnumId, EnumName FROM CustomEnum");
            sb.Append(" WHERE ParentEnumId = @ParentEnumId");
            sb.Append(" And ");
            string name = string.Empty;
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    name = "EnumValue";
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    name = "FirstCode";
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    name = "SecondCode";
                    break;

            }
            sb.AppendFormat("{0} = @{0}", name);

            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentEnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentEnumId));
                    db.AddInParameter(dbCommand, name, DbType.String, enumData);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal enumId = Convert.ToDecimal(dataReader[0]);
                            string depName = Convert.ToString(dataReader[1]);
                            //将创建 UserTypeInfo 对象加入集合中
                            keyValueInfo = new KeyValueInfo(enumId, depName);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return keyValueInfo;
        }

        /// <summary>
        /// 在树形枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        public KeyValueInfo GetTreeviewItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData)
        {
            KeyValueInfo keyValueInfo = null;

            string enumCode = GetEnumCode(parentEnumId);
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            string name = string.Empty;
            sb.Append("SELECT EnumId, EnumName FROM CustomEnum WHERE EnumCode LIKE @EnumCode AND ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    name = "EnumValue";
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    name = "FirstCode";
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    name = "SecondCode";
                    break;

            }
            sb.AppendFormat("{0} = @{0}", name);

            try
            {
                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}%", enumCode));
                    db.AddInParameter(dbCommand, name, DbType.String, enumData);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal enumId = Convert.ToDecimal(dataReader[0]);
                            string depName = Convert.ToString(dataReader[1]);
                            //将创建 UserTypeInfo 对象加入集合中
                            keyValueInfo = new KeyValueInfo(enumId, depName);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return keyValueInfo;
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            IList<string> columnCaptions = new List<string>();

            string[] columnNames = new string[] {"EnumName", "EnumCode", "EnumValue", "FirstCode", "SecondCode", "FstAdditionalString", "ScdAdditionalString",
                "TrdAdditionalString", "FourthAdditionalString", "FifthAdditionalString", "SixthAdditionalString", "FstAdditionalInteger", "ScdAdditionalInteger",
                "FstAdditionalDecimal", "ScdAdditionalDecimal"};


            foreach (string columnName in columnNames)
            {
                columnCaptions.Add(ColumnCaptionHelper.GetColumnCaption(columnName));
            }

            return columnCaptions;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentEnumId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string enumCode = GetEnumCode(parentEnumId);
                string sqlSelect = "SELECT EnumName, EnumCode, EnumValue, FirstCode, SecondCode, FstAdditionalString, ScdAdditionalString, TrdAdditionalString, FourthAdditionalString, FifthAdditionalString, SixthAdditionalString, FstAdditionalInteger, ScdAdditionalInteger, FstAdditionalDecimal, ScdAdditionalDecimal FROM CustomEnum WHERE EnumCode LIKE @EnumCode";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}_%", enumCode));
                    ds = db.ExecuteDataSet(dbCommand);
                }

                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 根据父节点编号获得所有子节点数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public DataSet GetEnumData(decimal parentEnumId)
        {
            DataSet ds = null;

            string enumCode = GetEnumCode(parentEnumId);
            string sqlSelect = "SELECT EnumId, ParentEnumId, EnumName FROM CustomEnum WHERE EnumCode LIKE @EnumCode";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}_%", enumCode));
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomEnum ", "EnumId", "EnumName, EnumCode, EnumValue, FirstCode, SecondCode, FstAdditionalString, ScdAdditionalString, TrdAdditionalString, FourthAdditionalString, FifthAdditionalString, SixthAdditionalString, FstAdditionalInteger, ScdAdditionalInteger, FstAdditionalDecimal, ScdAdditionalDecimal",
                    false, false, startPosition, count, null, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得枚举对象
        /// </summary>
        /// <param name="parentEnumCode"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(string parentEnumCode, string enumName)
        {
            CommonNode commonNode = null;

            string sqlSelect = "SELECT EnumId, ParentEnumId, EnumCode, IsLeaf FROM CustomEnum WHERE EnumCode LIKE @EnumCode AND EnumName = @EnumName";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}%", parentEnumCode));
                    db.AddInParameter(dbCommand, "EnumName", DbType.String, enumName);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentEnumId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string enumValue = DataConvertionHelper.GetString(dataReader[2]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[3]);
                            commonNode = new CommonNode(enumId, parentEnumId, enumName, enumValue, isLeaf);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 根据枚举编码获得枚举编号
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public decimal GetEnumId(string enumCode)
        {
            decimal enumId = 0;

            string sqlSelect = "SELECT EnumId FROM CustomEnum WHERE EnumCode = @EnumCode";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, enumCode);
                    enumId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumId;
        }

        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <param name="parentEnumCode"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public decimal GetEnumId(string parentEnumCode, string enumName)
        {
            decimal enumId = 0;

            string sqlSelect = "SELECT EnumId FROM CustomEnum WHERE EnumCode LIKE @EnumCode AND EnumName = @EnumName";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}%", parentEnumCode));
                    db.AddInParameter(dbCommand, "EnumName", DbType.String, enumName);
                    enumId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumId;
        }

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetEnumData(decimal enumId, PhysicalDataFieldType physicalDataFieldType)
        {
            object obj = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    sb.Append("EnumValue");
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    sb.Append("FirstCode");
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    sb.Append("SecondCode");
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型。");

            }
            sb.Append(" FROM CustomEnum WHERE EnumId = @EnumId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                    obj = db.ExecuteScalar(dbCommand);
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
        }

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetDropdownListData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType)
        {
            object obj = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    sb.Append("EnumValue");
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    sb.Append("FirstCode");
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    sb.Append("SecondCode");
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型。");

            }
            sb.Append(" FROM CustomEnum WHERE ParentEnumId = @ParentEnumId AND EnumName = @EnumName ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentEnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentEnumId));
                    db.AddInParameter(dbCommand, "EnumName", DbType.String, enumName);
                    obj = db.ExecuteScalar(dbCommand);
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
        }

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetTreeData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType)
        {
            object obj = null;

            string enumCode = GetEnumCode(parentEnumId);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    sb.Append("EnumValue");
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    sb.Append("FirstCode");
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    sb.Append("SecondCode");
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型。");

            }
            sb.Append(" FROM CustomEnum WHERE EnumCode LIKE @EnumCode AND EnumName = @EnumName ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}%", enumCode));
                    db.AddInParameter(dbCommand, "EnumName", DbType.String, enumName);
                    obj = db.ExecuteScalar(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
        }

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId">父编号</param>
        /// <param name="value">当前节点的值</param>
        /// <param name="physicalDataFieldType">当前节点的类型</param>
        /// <returns></returns>
        public string GetDropdownListEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType)
        {
            string enumName = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EnumName FROM CustomEnum WHERE ParentEnumId = @ParentEnumId AND ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnumValue:
                    sb.Append("EnumValue = @EnumValue");
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    sb.Append("FirstCode = @FirstCode");
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    sb.Append("SecondCode = @SecondCode");
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型。");

            }

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentEnumId", DbType.Decimal, parentEnumId);
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.DropdownListEnumValue:
                            db.AddInParameter(dbCommand, "EnumValue", DbType.String, DataConvertionHelper.GetString(value));
                            break;

                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            db.AddInParameter(dbCommand, "FirstCode", DbType.String, DataConvertionHelper.GetString(value));
                            break;

                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            db.AddInParameter(dbCommand, "SecondCode", DbType.String, DataConvertionHelper.GetString(value));
                            break;
                    }
                    enumName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumName;
        }

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public string GetTreeEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType)
        {
            string enumName = string.Empty;

            string result = DataConvertionHelper.GetString(value);
            if (string.IsNullOrWhiteSpace(result))
            {
                return enumName;
            }

            string enumCode = GetEnumCode(parentEnumId);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT EnumName FROM CustomEnum WHERE EnumCode LIKE @EnumCode AND ");
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.TreeViewEnumValue:
                    sb.Append("EnumValue = @EnumValue");
                    break;

                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    sb.Append("FirstCode = @FirstCode");
                    break;

                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    sb.Append("SecondCode = @SecondCode");
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型。");

            }

            try
            {                
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumCode", DbType.String, string.Format("{0}%", enumCode));
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.TreeViewEnumValue:
                            db.AddInParameter(dbCommand, "EnumValue", DbType.String, result);
                            break;

                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            db.AddInParameter(dbCommand, "FirstCode", DbType.String, result);
                            break;

                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            db.AddInParameter(dbCommand, "SecondCode", DbType.String, result);
                            break;
                    }
                    enumName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumName;
        }

        /// <summary>
        /// 获得枚举编码
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public string GetEnumCode(decimal enumId)
        {
            string enumCode = string.Empty;

            string sqlSelect = "SELECT EnumCode FROM CustomEnum WHERE EnumId = @EnumId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                    enumCode = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumCode;
        }

        /// <summary>
        /// 获得枚举选项列表
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public IList<CustomEnumInfo> GetEnumItems(decimal parentEnumId)
        {
            IList<CustomEnumInfo> customEnumInfos = null;

            string parentEnumCode = GetEnumCode(parentEnumId);

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("EnumCode", "EnumCode", DbType.String, string.Format("{0}_%", parentEnumCode), DataFieldCondition.Like));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("EnumCode", CustomSorting.Ascending));
            customEnumInfos = GetModelInfos(whereConditons, sortingCondtions);

            return customEnumInfos;
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(decimal parentEnumId, string enumName)
        {
            CommonItemList<decimal, CommonNode> commonItemList = new CommonItemList<decimal, CommonNode>(decimal.MinusOne, string.Empty);

            string parentEnumCode = GetEnumCode(parentEnumId);
            CommonNode commonNode = GetCommonNode(parentEnumCode, enumName);
            if (commonNode != null)
            {
                commonItemList.Text = commonNode.NodeName;
                commonItemList.Value = commonNode.NodeId;
                if (commonNode.NodeCode.Length > AppSettingHelper.EnumCodeLength)
                {
                    string code = commonNode.NodeCode;
                    do
                    {
                        code = code.Substring(0, code.Length - AppSettingHelper.EnumCodeLength);
                        IList<CommonNode> commonNodes = GetChildNodesByParentNodeCode(code, AppSettingHelper.EnumCodeLength);
                        foreach (CommonNode node in commonNodes)
                        {
                            commonItemList.CommonList.Add(node);
                        }
                    } while (code.Length > parentEnumCode.Length);
                }
            }

            return commonItemList;
        }

        /// <summary>
        /// 获取枚举的最大层级
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetMaxLevel(decimal enumId)
        {
            int level = 0;

            string sqlSelect = "SELECT TOP 1 EnumId FROM CustomEnum WHERE ParentEnumId = @ParentEnumId ORDER BY Sorting ASC";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                decimal parentEnumId = enumId;
                while (parentEnumId > 0)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ParentEnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentEnumId));
                        parentEnumId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand), 0);
                    }
                    if (parentEnumId > 0) level++;
                    if (level > 5)
                    {
                        throw new ArgumentException("不允许超过5层。");
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return level;
        }

        /// <summary>
        /// 是否是超大枚举
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public bool GetSuperEnumEnabled(decimal enumId)
        {
            bool superEnumEnabled = false;

            string sqlSelect = "SELECT SuperEnumEnabled FROM CustomEnum WHERE EnumId = @EnumId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(enumId));
                    superEnumEnabled = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return superEnumEnabled;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 枚举转换
        /// </summary>
        /// <param name="customEnumInfo"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetEnumResult(CustomEnumInfo customEnumInfo, PhysicalDataFieldType physicalDataFieldType)
        {
            object result = null;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.EnumNameDependency:
                    result = customEnumInfo.EnumName;
                    break;

                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.EnumValue:
                    result = customEnumInfo.EnumValue;
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalCode:
                    result = customEnumInfo.FirstCode;
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                    result = customEnumInfo.SecondCode;
                    break;

                case PhysicalDataFieldType.FstAdditionalString:
                    result = customEnumInfo.FstAdditionalString;
                    break;

                case PhysicalDataFieldType.ScdAdditionalString:
                    result = customEnumInfo.ScdAdditionalString;
                    break;

                case PhysicalDataFieldType.TrdAdditionalString:
                    result = customEnumInfo.TrdAdditionalString;
                    break;

                case PhysicalDataFieldType.FourthAdditionalString:
                    result = customEnumInfo.FourthAdditionalString;
                    break;

                case PhysicalDataFieldType.FifthAdditionalString:
                    result = customEnumInfo.FifthAdditionalString;
                    break;

                case PhysicalDataFieldType.SixthAdditionalString:
                    result = customEnumInfo.SixthAdditionalString;
                    break;

                case PhysicalDataFieldType.FstAdditionalInteger:
                    result = customEnumInfo.FstAdditionalInteger;
                    break;

                case PhysicalDataFieldType.ScdAdditionalInteger:
                    result = customEnumInfo.ScdAdditionalInteger;
                    break;

                case PhysicalDataFieldType.FstAdditionalDecimal:
                    result = customEnumInfo.FstAdditionalDecimal;
                    break;

                case PhysicalDataFieldType.ScdAdditionalDecimal:
                    result = customEnumInfo.ScdAdditionalDecimal;
                    break;
            }

            return result;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
		/// 获得 CustomEnumInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomEnumInfo 对象列表</returns>
		private IList<CustomEnumInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomEnumInfo> customEnumInfos = new List<CustomEnumInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomEnum");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if ((sortingCondtions != null) && (sortingCondtions.Count > 0))
            {
                sb.Append(" ORDER BY ");
                sb.Append(DataAccessHandler.GetSortingSentence(sortingCondtions));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentEnumId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string enumName = DataConvertionHelper.GetString(dataReader[2]);
                            string enumCode = DataConvertionHelper.GetString(dataReader[3]);
                            string enumValue = DataConvertionHelper.GetString(dataReader[4]);
                            string firstCode = DataConvertionHelper.GetString(dataReader[5]);
                            string secondCode = DataConvertionHelper.GetString(dataReader[6]);
                            string fstAdditionalString = DataConvertionHelper.GetString(dataReader[7]);
                            string scdAdditionalString = DataConvertionHelper.GetString(dataReader[8]);
                            string trdAdditionalString = DataConvertionHelper.GetString(dataReader[9]);
                            string fourthAdditionalString = DataConvertionHelper.GetString(dataReader[10]);
                            string fifthAdditionalString = DataConvertionHelper.GetString(dataReader[11]);
                            string sixthAdditionalString = DataConvertionHelper.GetString(dataReader[12]);
                            int fstAdditionalInteger = DataConvertionHelper.GetInt(dataReader[13]);
                            int scdAdditionalInteger = DataConvertionHelper.GetInt(dataReader[14]);
                            decimal fstAdditionalDecimal = DataConvertionHelper.GetDecimal(dataReader[15]);
                            decimal scdAdditionalDecimal = DataConvertionHelper.GetDecimal(dataReader[16]);
                            bool superEnumEnabled = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[18]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[19]);
                            string notes = DataConvertionHelper.GetString(dataReader[20]);
                            //将创建 CustomEnumInfo 对象加入集合中
                            customEnumInfos.Add(new CustomEnumInfo(enumId, parentEnumId, enumName, enumCode, enumValue,
                            firstCode, secondCode, fstAdditionalString, scdAdditionalString, trdAdditionalString,
                            fourthAdditionalString, fifthAdditionalString, sixthAdditionalString, fstAdditionalInteger, scdAdditionalInteger,
                            fstAdditionalDecimal, scdAdditionalDecimal, superEnumEnabled, isLeaf, sorting, notes));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customEnumInfos;
        }

        /// <summary>
        /// 获得 CustomEnumInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomEnumInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomEnum");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomEnum 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomEnum ", "EnumId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "CustomEnum ", "EnumId", "*", false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得以表 CustomEnum 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "CustomEnum ", "EnumId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 删除满足条件的所有  CustomEnumInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomEnum");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            else
            {
                throw new ArgumentNullException("批量删除的条件不许未空，即不允许删除该表中所有的数据.");
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 获得节点的所有的上级节点编号
        /// </summary>
        /// <param name="parentEnumId">父节点编号</param>
        /// <returns>上级节点编号列表</returns>
        private IList<decimal> GetParentEnumIds(decimal enumId)
        {
            IList<decimal> parentIds = new List<decimal>();
            do
            {
                decimal enumParentId = GetParentNodeId(enumId);
                if (enumParentId > 0)
                {
                    parentIds.Add(enumParentId);
                }
                enumId = enumParentId;
            } while (enumId > 0);

            return parentIds;
        }

        /// <summary>
        ///  刷新排序
        /// </summary>
        /// <param name="enumId"></param>
        private void RefreshSorting(decimal enumId)
        {
            IList<decimal> commonNodeIds = GetChildNodeIds(enumId);
            int sorting = 1;
            foreach (var commonNodeId in commonNodeIds)
            {
                UpdateSorting(commonNodeId, sorting++);
                RefreshSorting(commonNodeId);
            }
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="sorting"></param>
        private void UpdateSorting(decimal enumId, int sorting)
        {
            //生成更新语句
            string update = "UPDATE CustomEnum SET Sorting = @Sorting WHERE EnumId = @EnumId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新枚举中非唯一值
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <param name="oldCustomEnumInfo"></param>
        /// <param name="customEnumInfo"></param>
        private void UpdateEnumResult(IList<CommonNode> commonNodes, CustomEnumInfo oldCustomEnumInfo, CustomEnumInfo customEnumInfo)
        {
            if (commonNodes == null || commonNodes.Count == 0) return;
            foreach (CommonNode commonNode in commonNodes)
            {
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)commonNode.NodeType;
                object oldValue = null;
                object newValue = null;
                string key = string.Empty;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.FstAdditionalString:
                        if (!oldCustomEnumInfo.FstAdditionalString.Equals(customEnumInfo.FstAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.FstAdditionalString;
                            newValue = customEnumInfo.FstAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.ScdAdditionalString:
                        if (!oldCustomEnumInfo.ScdAdditionalString.Equals(customEnumInfo.ScdAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.ScdAdditionalString;
                            newValue = customEnumInfo.ScdAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.TrdAdditionalString:
                        if (!oldCustomEnumInfo.TrdAdditionalString.Equals(customEnumInfo.TrdAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.TrdAdditionalString;
                            newValue = customEnumInfo.TrdAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.FourthAdditionalString:
                        if (!oldCustomEnumInfo.FourthAdditionalString.Equals(customEnumInfo.FourthAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.FourthAdditionalString;
                            newValue = customEnumInfo.FourthAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.FifthAdditionalString:
                        if (!oldCustomEnumInfo.FifthAdditionalString.Equals(customEnumInfo.FifthAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.FifthAdditionalString;
                            newValue = customEnumInfo.FifthAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.SixthAdditionalString:
                        if (!oldCustomEnumInfo.SixthAdditionalString.Equals(customEnumInfo.SixthAdditionalString))
                        {
                            oldValue = oldCustomEnumInfo.SixthAdditionalString;
                            newValue = customEnumInfo.SixthAdditionalString;
                        }
                        break;

                    case PhysicalDataFieldType.FstAdditionalInteger:
                        if (!oldCustomEnumInfo.FstAdditionalInteger.Equals(customEnumInfo.FstAdditionalInteger))
                        {
                            oldValue = oldCustomEnumInfo.FstAdditionalInteger;
                            newValue = customEnumInfo.FstAdditionalInteger;
                        }
                        break;

                    case PhysicalDataFieldType.ScdAdditionalInteger:
                        if (!oldCustomEnumInfo.ScdAdditionalInteger.Equals(customEnumInfo.ScdAdditionalInteger))
                        {
                            oldValue = oldCustomEnumInfo.ScdAdditionalInteger;
                            newValue = customEnumInfo.ScdAdditionalInteger;
                        }
                        break;

                    case PhysicalDataFieldType.FstAdditionalDecimal:
                        if (!oldCustomEnumInfo.FstAdditionalInteger.Equals(customEnumInfo.FstAdditionalInteger))
                        {
                            oldValue = oldCustomEnumInfo.FstAdditionalInteger;
                            newValue = customEnumInfo.FstAdditionalInteger;
                        }
                        break;

                    case PhysicalDataFieldType.ScdAdditionalDecimal:
                        if (!oldCustomEnumInfo.ScdAdditionalDecimal.Equals(customEnumInfo.ScdAdditionalDecimal))
                        {
                            oldValue = oldCustomEnumInfo.ScdAdditionalDecimal;
                            newValue = customEnumInfo.ScdAdditionalDecimal;
                        }
                        break;
                }
                if (oldValue != null)
                {
                    UpdateEnumResult(commonNode.ParentNodeId, commonNode.NodeId, oldCustomEnumInfo, newValue, oldValue);
                }
            }
        }

        /// <summary>
        /// 更新数据库中对应非唯一的枚举属性值
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="oldCustomEnumInfo"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        private void UpdateEnumResult(decimal tableId, decimal dataFieldId, CustomEnumInfo oldCustomEnumInfo, object newValue, object oldValue)
        {
            string key = string.Empty;
            CustomDataField customDataField = new CustomDataField();
            CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(dataFieldId);
            PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataField.GetDataFieldType(customDataFieldInfo.ParentDataFieldId);
            string parentDataFieldName = customDataField.GetPhysicalName(customDataFieldInfo.ParentDataFieldId);
            switch (dataFieldType)
            {
                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                    key = oldCustomEnumInfo.EnumName;
                    break;

                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                    key = oldCustomEnumInfo.EnumValue;
                    break;

                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    key = oldCustomEnumInfo.FirstCode;
                    break;

                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    key = oldCustomEnumInfo.SecondCode;
                    break;

                default:
                    throw new ArgumentException("不支持该枚举类型。");
            }
            string physcialDataFieldName = customDataFieldInfo.PhysicalName;
            CustomTable customTable = new CustomTable();
            int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            string sqlUpdate = string.Format("UPDATE {0} SET {1} = @new_{1} WHERE {1} = @old_{1} AND {2} = @{2}", tablePhysicalName, physcialDataFieldName,
                parentDataFieldName);
            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
            {
                //给参数赋值
                dbBusiness.AddInParameter(dbCommand, physcialDataFieldName, DbType.String, key);
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.FstAdditionalString:
                    case PhysicalDataFieldType.ScdAdditionalString:
                    case PhysicalDataFieldType.TrdAdditionalString:
                    case PhysicalDataFieldType.FourthAdditionalString:
                    case PhysicalDataFieldType.FifthAdditionalString:
                    case PhysicalDataFieldType.SixthAdditionalString:
                        dbBusiness.AddInParameter(dbCommand, string.Format("new_{0}", physcialDataFieldName), DbType.String, DataConvertionHelper.GetString(newValue));
                        dbBusiness.AddInParameter(dbCommand, string.Format("old_{0}", physcialDataFieldName), DbType.String, DataConvertionHelper.GetString(oldValue));
                        break;

                    case PhysicalDataFieldType.FstAdditionalInteger:
                    case PhysicalDataFieldType.ScdAdditionalInteger:
                        dbBusiness.AddInParameter(dbCommand, string.Format("new_{0}", physcialDataFieldName), DbType.Int32, DataConvertionHelper.SetInt(DataConvertionHelper.GetConvertedInt(newValue)));
                        dbBusiness.AddInParameter(dbCommand, string.Format("old_{0}", physcialDataFieldName), DbType.Int32, DataConvertionHelper.SetInt(DataConvertionHelper.GetConvertedInt(oldValue)));
                        break;

                    case PhysicalDataFieldType.FstAdditionalDecimal:
                    case PhysicalDataFieldType.ScdAdditionalDecimal:
                        dbBusiness.AddInParameter(dbCommand, string.Format("new_{0}", physcialDataFieldName), DbType.Decimal, DataConvertionHelper.SetDecimal(DataConvertionHelper.GetConvertedDecimal(newValue)));
                        dbBusiness.AddInParameter(dbCommand, string.Format("old_{0}", physcialDataFieldName), DbType.Decimal, DataConvertionHelper.SetDecimal(DataConvertionHelper.GetConvertedDecimal(oldValue)));
                        break;

                    default:
                        throw new ArgumentException("不支持该枚举类型");
                }
                dbBusiness.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// 更新数据库中对应唯一的枚举属性值
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="physcialDataFieldName"></param>
        /// <param name="newValue"></param>
        /// <param name="oldValue"></param>
        private void UpdateEnumResult(decimal tableId, string physcialDataFieldName, string newValue, string oldValue)
        {
            CustomTable customTable = new CustomTable();
            int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            string sqlUpdate = string.Format("UPDATE {0} SET {1} = '{2}' WHERE {1} = '{3}'", tablePhysicalName, physcialDataFieldName, newValue, oldValue);
            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
            {
                dbBusiness.ExecuteNonQuery(dbCommand);
            }
        }


        #endregion

        #endregion
    }
}
