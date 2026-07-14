//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomMenu.cs
// 描述：CustomMenu 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/12/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using AppFramework.Core;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomMenu 表的数据层访问类
    /// </summary>
    public class CustomMenu : CommonNodeDataAccess, ICustomMenu
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomMenu() : base("CustomMenu", "MenuId", "ParentMenuId", "MenuName", "MenuCode", true, true, "MenuType")
        {
		}

        #endregion        

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomMenu 表中插入一条新记录
		/// </summary>
		/// <param name="customMenuInfo">customMenuInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomMenuInfo customMenuInfo)
		{
            return Insert(customMenuInfo, null);
        }

        /// <summary>
		/// 获得 CustomMenuInfo 对象
		/// </summary>
		///<param name="menuId">菜单编号</param>
		/// <returns> CustomMenuInfo 对象</returns>
		public CustomMenuInfo GetModelInfo(decimal menuId)
		{			
			CustomMenuInfo customMenuInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("MenuId", "MenuId", DbType.Decimal, menuId, DataFieldCondition.Equal));
            
            //创建集合对象
			IList<CustomMenuInfo> customMenuInfos = GetModeInfos(whereConditons, null, true);
            if (customMenuInfos != null && customMenuInfos.Count > 0)
            {
                customMenuInfo = customMenuInfos[0];
            }          

            return customMenuInfo;
		}
        
        /// <summary>
		/// 更新 CustomMenuInfo 对象
		/// </summary>
		/// <param name="customMenuInfo">CustomMenuInfo 对象</param>
		public void Update(CustomMenuInfo customMenuInfo)
		{
            Update(customMenuInfo, null);
        }        
        
        /// <summary>
		///  删除 CustomMenuInfo 对象
		/// </summary>
	    ///<param name="menuId">菜单编号</param>
		public void Delete(decimal menuId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomMenu ");
			sb.Append("WHERE MenuId = @MenuId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, menuId);
					//执行删除操作
					if (db.ExecuteNonQuery(dbCommand) != 1)
					{
						throw new Exception("删除失败！");
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
		/// 获得 CustomMenuInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomMenuInfo 对象列表</returns>
		public IList<CustomMenuInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModeInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 CustomMenu 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomMenuInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomMenu ", "MenuId", false, whereConditons);
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
        /// 根据菜单类型获得一级菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public CustomMenuInfo GetCustomMenu(byte menuType)
        {
            CustomMenuInfo customMenuInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("MenuType", "MenuType", DbType.Byte, menuType, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("ParentMenuId", "ParentMenuId", DbType.Decimal, DBNull.Value, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<CustomMenuInfo> customMenuInfos = GetModelInfos(whereConditons, null);
            if (customMenuInfos != null && customMenuInfos.Count > 0)
            {
                customMenuInfo = customMenuInfos[0];
            }

            return customMenuInfo;
        }

        /// <summary>
        /// 最大的菜单类型
        /// </summary>
        /// <returns></returns>
        public byte GetMaxMenuType()
        {
            byte maxMenuType = 0;

            try
            {
                string sqlSelect = "SELECT MAX(MenuType) FROM CustomMenu";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    maxMenuType = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return maxMenuType;
        }

        /// <summary>
		/// 向 CustomMenu 表中插入一条新记录
		/// </summary>
		/// <param name="customMenuInfo">customMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            //自动增加的关键字的值
            decimal customMenuId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customMenuInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomMenu", "Sorting", "ParentMenuId", customMenuInfo.ParentMenuId, 0) + 1;
            customMenuInfo.IconName = GetFormmattedIconName(customMenuInfo.ParentMenuId < 0 ? 0 : customMenuInfo.ParentMenuId, customMenuInfo.Sorting, customMenuInfo.IconName);
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomMenu(ParentMenuId, MenuName, MenuCode, IconType, MenuIcon, ");
            sb.Append("IconName, IconPath, MenuURL, MenuType, MenuIconName, IsLeaf, ");
            sb.Append("ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@ParentMenuId, @MenuName, @MenuCode, @IconType, @MenuIcon, ");
            sb.Append("@IconName, @IconPath, @MenuURL, @MenuType, @MenuIconName, @IsLeaf, ");
            sb.Append("@ToolTip, @Sorting, @Notes);");
            sb.Append("SET @MenuId = SCOPE_IDENTITY()");            

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "MenuId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "ParentMenuId", DbType.Decimal, DataConvertionHelper.SetDecimal(customMenuInfo.ParentMenuId));
                        db.AddInParameter(dbCommand, "MenuName", DbType.String, customMenuInfo.MenuName);
                        db.AddInParameter(dbCommand, "MenuCode", DbType.String, customMenuInfo.MenuCode);
                        db.AddInParameter(dbCommand, "IconType", DbType.Byte, customMenuInfo.IconType);
                        db.AddInParameter(dbCommand, "MenuIcon", DbType.Byte, customMenuInfo.MenuIcon);
                        db.AddInParameter(dbCommand, "IconName", DbType.String, customMenuInfo.IconName);
                        db.AddInParameter(dbCommand, "IconPath", DbType.String, customMenuInfo.IconPath);
                        db.AddInParameter(dbCommand, "MenuURL", DbType.String, customMenuInfo.MenuURL);
                        db.AddInParameter(dbCommand, "MenuType", DbType.Byte, customMenuInfo.MenuType);
                        db.AddInParameter(dbCommand, "MenuIconName", DbType.String, customMenuInfo.MenuIconName);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customMenuInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customMenuInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customMenuInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customMenuId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@MenuId"].Value, 0);
                    }
                    if (customMenuInfo.ParentMenuId > 0)
                    {
                        UpdateLeafOfParentNode(customMenuInfo.ParentMenuId, false, db, transaction);
                    }
                    if (imageData != null)
                    {
                        FileSavedHelper.SaveIcons(customMenuInfo.IconName, imageData);
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

            return customMenuId;
        }

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomMenu SET ParentMenuId = @ParentMenuId, MenuName = @MenuName, MenuCode = @MenuCode, IconType = @IconType, MenuIconName = @MenuIconName, ");
            sb.Append("MenuIcon = @MenuIcon, IconName = @IconName, IconPath = @IconPath, MenuURL = @MenuURL, ToolTip = @ToolTip, Notes = @Notes ");
            sb.Append("WHERE MenuId = @MenuId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            customMenuInfo.Sorting = DataAccessHandler.GetValueOfDataField(db, "CustomMenu", "Sorting", "MenuId", customMenuInfo.MenuId, 0);
            customMenuInfo.IconName = GetFormmattedIconName(customMenuInfo.ParentMenuId < 0 ? 0 : customMenuInfo.ParentMenuId, customMenuInfo.Sorting, customMenuInfo.IconName);            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, customMenuInfo.MenuId);
                    db.AddInParameter(dbCommand, "ParentMenuId", DbType.Decimal, DataConvertionHelper.SetDecimal(customMenuInfo.ParentMenuId));
                    db.AddInParameter(dbCommand, "MenuName", DbType.String, customMenuInfo.MenuName);
                    db.AddInParameter(dbCommand, "MenuCode", DbType.String, customMenuInfo.MenuCode);
                    db.AddInParameter(dbCommand, "IconType", DbType.Byte, customMenuInfo.IconType);
                    db.AddInParameter(dbCommand, "MenuIconName", DbType.String, customMenuInfo.MenuIconName);
                    db.AddInParameter(dbCommand, "MenuIcon", DbType.Byte, customMenuInfo.MenuIcon);
                    db.AddInParameter(dbCommand, "IconName", DbType.String, customMenuInfo.IconName);
                    db.AddInParameter(dbCommand, "IconPath", DbType.String, customMenuInfo.IconPath);
                    db.AddInParameter(dbCommand, "MenuURL", DbType.String, customMenuInfo.MenuURL);
                    db.AddInParameter(dbCommand, "ToolTip", DbType.String, customMenuInfo.ToolTip);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customMenuInfo.Notes);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                    if (imageData != null)
                    {
                        FileSavedHelper.SaveIcons(customMenuInfo.IconName, imageData);
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
        /// 检查子菜单权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool CheckSubMenuAuthority(decimal userId, decimal menuId)
        {
            bool result = false;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM RoleAndBusiness ");
            sb.Append("INNER JOIN CustomBusiness ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndBusiness.RoleId ");
            sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND CustomBusiness.MenuId = @MenuId AND RoleAndBusiness.BusinessEnabled = @BusinessEnabled");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, menuId);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, true);
                    int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得菜单分类对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CustomMenuInfo> GetMenuClasses(decimal userId)
        {
            IList<CustomMenuInfo> customMenuInfos = new List<CustomMenuInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT CustomMenu.MenuId, CustomMenu.ParentMenuId, CustomMenu.MenuName, CustomMenu.MenuCode, CustomMenu.IconType, ");
            sb.Append("CustomMenu.MenuIcon, CustomMenu.IconName, CustomMenu.IconPath, CustomMenu.MenuURL, ");
            sb.Append("CustomMenu.MenuType, CustomMenu.MenuIconName, CustomMenu.IsLeaf, CustomMenu.ToolTip, CustomMenu.Sorting, CustomMenu.Notes FROM RoleAndBusiness ");
            sb.Append("INNER JOIN CustomBusiness ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN CustomRole ON CustomRole.RoleId = RoleAndBusiness.RoleId ");
            sb.Append("INNER JOIN CustomMenu ON CustomMenu.MenuId = CustomBusiness.MenuId ");
            sb.Append("INNER JOIN RoleAndUser ON RoleAndUser.RoleId = CustomRole.RoleId ");
            sb.Append("WHERE RoleAndUser.UserId = @UserId AND RoleAndBusiness.BusinessEnabled = @BusinessEnabled ");
            sb.Append("AND CustomRole.IsLockedOut = 0 AND (CustomRole.InitializedDate IS NULL OR CustomRole.InitializedDate <= @CurrentTime) AND (CustomRole.ExpiredDate IS NULL OR CustomRole.ExpiredDate >= @CurrentTime) ");
            sb.Append("ORDER BY CustomMenu.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "BusinessEnabled", DbType.Boolean, true);
                    db.AddInParameter(dbCommand, "CurrentTime", DbType.DateTime, DateTime.Now);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal menuId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentMenuId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string menuName = DataConvertionHelper.GetString(dataReader[2]);
                            string menuCode = DataConvertionHelper.GetString(dataReader[3]);
                            byte iconType = DataConvertionHelper.GetByte(dataReader[4]);
                            byte menuIcon = DataConvertionHelper.GetByte(dataReader[5]);
                            string iconName = DataConvertionHelper.GetString(dataReader[6]);
                            string iconPath = DataConvertionHelper.GetString(dataReader[7]);
                            string menuURL = DataConvertionHelper.GetString(dataReader[8]);
                            byte menuType = DataConvertionHelper.GetByte(dataReader[9]);
                            string menuIconName = DataConvertionHelper.GetString(dataReader[10]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[11]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            string notes = DataConvertionHelper.GetString(dataReader[14]);
                            //将创建 CustomMenuInfo 对象加入集合中
                            customMenuInfos.Add(new CustomMenuInfo(menuId, parentMenuId, menuName, menuCode, iconType,
                            menuIcon, iconName, iconPath, menuURL, menuType, menuIconName, isLeaf, toolTip, sorting, notes));
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

            return customMenuInfos;
        }

        /// <summary>
        /// 获得菜单的图标名称
        /// </summary>
        ///<param name="menuId">菜单编号</param>
        /// <returns> 图标名称</returns>
        public string GetIconName(decimal menuId)
        {
            string iconName = string.Empty;

            try
            {
                string sqlSelect = "SELECT IconName FROM CustomMenu WHERE MenuId = @MenuId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MenuId", DbType.Decimal, DataConvertionHelper.SetDecimal(menuId));
                    iconName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return iconName;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomMenuInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomMenuInfo 对象列表</returns>
        private IList<CustomMenuInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomMenuInfo>  customMenuInfos = new List<CustomMenuInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM CustomMenu");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if((sortingCondtions != null) && (sortingCondtions.Count > 0))
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
                            decimal menuId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentMenuId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string menuName = DataConvertionHelper.GetString(dataReader[2]);
                            string menuCode = DataConvertionHelper.GetString(dataReader[3]);
                            byte iconType = DataConvertionHelper.GetByte(dataReader[4]);
                            byte menuIcon = DataConvertionHelper.GetByte(dataReader[5]);
                            string iconName = DataConvertionHelper.GetString(dataReader[6]);
                            string iconPath = DataConvertionHelper.GetString(dataReader[7]);
                            string menuURL = DataConvertionHelper.GetString(dataReader[8]);
                            byte menuType = DataConvertionHelper.GetByte(dataReader[9]);
                            string menuIconName = DataConvertionHelper.GetString(dataReader[10]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[11]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            string notes = DataConvertionHelper.GetString(dataReader[14]);
                            //将创建 CustomMenuInfo 对象加入集合中
                            customMenuInfos.Add(new CustomMenuInfo(menuId, parentMenuId, menuName, menuCode, iconType,
                            menuIcon, iconName, iconPath, menuURL, menuType, menuIconName, isLeaf, toolTip, sorting, notes));
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
            
			return customMenuInfos;
		} 
        
        /// <summary>
		/// 获得 CustomMenuInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomMenuInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomMenu");
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
        /// 获得表 CustomMenu 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int  startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {             
                ds =  DataAccessHandler.GetPageRecord(db, "CustomMenu ", "MenuId", "*", false, false, startPosition, 
                    count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得以表 CustomMenu 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomMenu ", "MenuId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomMenu 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomMenu ", "MenuId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomMenu 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomMenu ", "MenuId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomMenuInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomMenu");
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
        /// 获得格式化后的图片名称
        /// </summary>
        /// <param name="parentMenuId"></param>
        /// <param name="sorting"></param>
        /// <param name="originalIconName"></param>
        /// <returns></returns>
        private string GetFormmattedIconName(decimal parentMenuId, int sorting, string originalIconName)
        {
            /* Path.GetExtension返回的后缀名带 '.' */
            return string.Format("Menu_{0}_{1}{2}", parentMenuId, sorting, Path.GetExtension(originalIconName));
        }

        #endregion

        #endregion
    }
}
