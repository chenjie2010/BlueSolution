//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CommonNodeDataAccess.cs
// 描述：CustomDepartment 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;

namespace AppFramework.Reference.DataAccessLibrary
{
    /// <summary>
    /// 在数据库中处理节点访问
    /// </summary>
    public abstract class CommonNodeDataAccess : ICommonNode
    {
        #region 私有变量

        private readonly string tableName;
        private readonly string dataFieldIdName;
        private readonly string parentFieldIdName;
        private readonly string dataFieldName;
        private readonly string dataFieldCodeName;
        private readonly bool hasLeaf;
        private readonly bool defaultValueOfLeaf;
        private readonly string nodeTypeName;
        private readonly string keyIdCaption;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldIdName"></param>
        /// <param name="parentFieldIdName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="dataFieldCodeName"></param>
        public CommonNodeDataAccess(string tableName, string dataFieldIdName, string parentFieldIdName, string dataFieldName, string dataFieldCodeName)
            : this(tableName, dataFieldIdName, parentFieldIdName, dataFieldName, dataFieldCodeName, true, true, string.Empty)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldIdName"></param>
        /// <param name="parentFieldIdName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="dataFieldCodeName"></param>
        /// <param name="hasLeaf"></param>
        /// <param name="defaultValueOfLeaf"></param>
        public CommonNodeDataAccess(string tableName, string dataFieldIdName, string parentFieldIdName, string dataFieldName, string dataFieldCodeName,
            bool hasLeaf, bool defaultValueOfLeaf)
            : this(tableName, dataFieldIdName, parentFieldIdName, dataFieldName, dataFieldCodeName, hasLeaf, defaultValueOfLeaf, string.Empty)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldIdName"></param>
        /// <param name="parentFieldIdName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="dataFieldCodeName"></param>
        /// <param name="hasLeaf"></param>
        /// <param name="defaultValueOfLeaf"></param>
        /// <param name="nodeTypeName"></param>
        public CommonNodeDataAccess(string tableName, string dataFieldIdName, string parentFieldIdName, string dataFieldName,
            string dataFieldCodeName, bool hasLeaf, bool defaultValueOfLeaf, string nodeTypeName)
        {
            this.tableName = tableName;
            this.dataFieldName = dataFieldName;
            this.dataFieldIdName = dataFieldIdName;
            this.parentFieldIdName = parentFieldIdName;
            this.dataFieldCodeName = dataFieldCodeName;
            this.hasLeaf = hasLeaf;
            this.defaultValueOfLeaf = defaultValueOfLeaf;
            this.nodeTypeName = nodeTypeName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldIdName"></param>
        /// <param name="parentFieldIdName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="dataFieldCodeName"></param>
        /// <param name="hasLeaf"></param>
        /// <param name="defaultValueOfLeaf"></param>
        /// <param name="nodeTypeName"></param>
        /// <param name="keyIdCaption"></param>
        public CommonNodeDataAccess(string tableName, string dataFieldIdName, string parentFieldIdName, string dataFieldName,
            string dataFieldCodeName, bool hasLeaf, bool defaultValueOfLeaf, string nodeTypeName, string keyIdCaption)
        {
            this.tableName = tableName;
            this.dataFieldName = dataFieldName;
            this.dataFieldIdName = dataFieldIdName;
            this.parentFieldIdName = parentFieldIdName;
            this.dataFieldCodeName = dataFieldCodeName;
            this.hasLeaf = hasLeaf;
            this.defaultValueOfLeaf = defaultValueOfLeaf;
            this.nodeTypeName = nodeTypeName;
            this.keyIdCaption = keyIdCaption;
        }

        #endregion

        #region 实现 ICommonNode 接口

        #region 公有方法

        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(decimal nodeId)
        {
            CommonNode commonNode = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>(1);
            whereConditons.Add(new WhereConditon(dataFieldIdName, dataFieldIdName, System.Data.DbType.String, nodeId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            IList<CommonNode> commonNodes = GetCommonNodesByWhereConditon(whereConditons);
            if (commonNodes.Count > 0)
            {
                commonNode = commonNodes[0];
            }

            return commonNode;
        }

        /// <summary>
        /// 获得扩展的 CommonNode
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public ExtendedCommonNode GetExtendedCommonNode(decimal nodeId)
        {
            ExtendedCommonNode extendedCommonNode = null;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1}, {2}, Notes", parentFieldIdName, dataFieldName, dataFieldCodeName);

            if (hasLeaf)
            {
                sb.Append(", IsLeaf ");
            }

            sb.AppendFormat(" FROM {0}  WHERE {1} = @{1}", tableName, dataFieldIdName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal parentNodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string name = DataConvertionHelper.GetString(dataReader[1]);
                            string code = DataConvertionHelper.GetString(dataReader[2]);
                            string notes = DataConvertionHelper.GetString(dataReader[3]);
                            bool isLeaf = defaultValueOfLeaf;
                            if (hasLeaf)
                            {
                                isLeaf = DataConvertionHelper.GetBoolean(dataReader[3]);
                            }
                            extendedCommonNode = new ExtendedCommonNode(nodeId, parentNodeId, name, code, isLeaf, notes);
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

            return extendedCommonNode;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary> 
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetCommonNodes()
        {
            return GetCommonNodesByWhereConditon(null);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="nodeName">单位名称</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string nodeName,IList<string> nodeCodes)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(dataFieldName, dataFieldName, System.Data.DbType.String, string.Format("%{0}%", nodeName),
                           DataFieldCondition.Like, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            IList<WhereConditon> conditons = DataAccessHandler.GetWhereConditons(nodeCodes, tableName, dataFieldCodeName);
            foreach (WhereConditon condition in conditons)
            {
                whereConditons.Add(condition);
            }
            IList<CommonNode> commonNodes = GetCommonNodesByWhereConditon(whereConditons);
            foreach (CommonNode commonNode in commonNodes)
            {
                commonNode.IsLeaf = true;
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="rootNodeId">根节点编号</param>
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>        
        public IList<CommonNode> GetChildNodes(decimal rootNodeId, string condition)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(3);
            whereConditons.Add(new WhereConditon(parentFieldIdName, parentFieldIdName, DbType.Decimal, rootNodeId,
                           DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon(dataFieldName, dataFieldName, DbType.String, string.Format("%{0}%", condition),
                           DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
            whereConditons.Add(new WhereConditon(dataFieldCodeName, dataFieldCodeName, DbType.String, string.Format("{0}%", condition),
               DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));

            IList<CommonNode> commonNodes = GetCommonNodesByWhereConditon(whereConditons);
            foreach (CommonNode commonNode in commonNodes)
            {
                commonNode.IsLeaf = true;
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据节点名称或是编号查询 CommonNode 对象的列表
        /// </summary>	
        /// <param name="condition">条件</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(string condition)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(dataFieldName, dataFieldName, System.Data.DbType.String, string.Format("%{0}%", condition),
                           DataFieldCondition.Like, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon(dataFieldCodeName, dataFieldCodeName, System.Data.DbType.String, string.Format("{0}%", condition),
               DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));

            IList<CommonNode> commonNodes = GetCommonNodesByWhereConditon(whereConditons);
            foreach (CommonNode commonNode in commonNodes)
            {
                commonNode.IsLeaf = true;
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得所有子节点的 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编码</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodesByParentNodeCode(string parentNodeCode)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(dataFieldCodeName, dataFieldCodeName, System.Data.DbType.String, string.Format("{0}%", parentNodeCode),
               DataFieldCondition.Like, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));


            return GetCommonNodesByWhereConditon(whereConditons);
        }


        /// <summary>
        /// 获得下一级的 CommonNode 对象的列表
        /// </summary>
        /// <param name="parentNodeCode">父节点编码</param>
        /// <param name="length"></param>
        /// <returns>对象的列表</returns>
        public IList<CommonNode> GetChildNodesByParentNodeCode(string parentNodeCode, int length)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            StringBuilder sb = new StringBuilder();
            sb.Append(parentNodeCode);
            while (length > 0)
            {
                sb.Append("_");
                length--;
            };
            whereConditons.Add(new WhereConditon(dataFieldCodeName, dataFieldCodeName, System.Data.DbType.String, sb.ToString(),
               DataFieldCondition.Like, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));


            return GetCommonNodesByWhereConditon(whereConditons);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(parentFieldIdName, parentFieldIdName, System.Data.DbType.Decimal, parentNodeId,
               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));


            return GetCommonNodesByWhereConditon(whereConditons);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(IList<decimal> nodeIds)
        {
            if (nodeIds == null || nodeIds.Count == 0)
            {
                return new List<CommonNode>();
            }
            IList<WhereConditon> whereConditons = DataAccessHandler.GetWhereConditons(nodeIds, tableName, dataFieldIdName);

            return GetCommonNodesByWhereConditon(whereConditons);
        }

        /// <summary>
        /// 由父节点编号获得下一级子节点的数目
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <returns>下一级子节点的数目</returns>
        public int GetTotalCountOfChildNode(decimal parentNodeId)
        {
            int count = 0;
            try
            {
                string sqlSelect = string.Format("SELECT COUNT({0}) FROM {1} WHERE {2} = @{2} ", dataFieldIdName, tableName, parentFieldIdName);

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="nodeId">移动的节点编号</param>
        /// <param name="otherNodeId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal nodeId, decimal otherNodeId, MovedDriection movedDriectionOfNode)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sqlFirstSelect = string.Empty;
                    string sqlSecondSelect = string.Empty;
                    string sqlFirstUpdate = string.Empty;
                    string sqlSecondUpdate = string.Empty;
                    decimal parentNodeId = 0;
                    int sorting = 0;
                    int otherSorting = 0;

                    /* 获得父节点编号和当前节点的排序值 */
                    sqlFirstSelect = string.Format("SELECT {0}, Sorting FROM {1} WHERE {2} = @{2}", parentFieldIdName, tableName, dataFieldIdName);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstSelect))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            if (dataReader.Read())
                            {
                                parentNodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                                sorting = DataConvertionHelper.GetInt(dataReader[1]);
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }

                    switch (movedDriectionOfNode)
                    {
                        case MovedDriection.Top:
                            StringBuilder sbTop = new StringBuilder();
                            sbTop.AppendFormat("UPDATE {0} SET Sorting = Sorting + 1 WHERE {1} ", tableName, parentFieldIdName);
                            if (DataConvertionHelper.IsNullValue(parentNodeId))
                            {
                                sbTop.Append("IS NULL AND Sorting < @Sorting");
                            }
                            else
                            {
                                sbTop.AppendFormat("= @{0} AND Sorting < @Sorting", parentFieldIdName);
                            }
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbTop.ToString()))
                            {
                                //给参数赋值
                                if (!DataConvertionHelper.IsNullValue(parentNodeId))
                                {
                                    db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                                }
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = 1 WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Previous:
                        case MovedDriection.Next:
                            sqlSecondSelect = string.Format("SELECT Sorting FROM {0} WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSecondSelect))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, otherNodeId);
                                otherSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, otherSorting);
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, sorting);
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, otherNodeId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Bottom:
                            StringBuilder sbBottom = new StringBuilder();
                            sbBottom.AppendFormat("SELECT MAX(Sorting) FROM {0} WHERE {1} ", tableName, parentFieldIdName);
                            if (!DataConvertionHelper.IsNullValue(parentNodeId))
                            {
                                sbBottom.AppendFormat("= @{0}", parentFieldIdName);
                            }
                            else
                            {
                                sbBottom.Append("IS NULL");
                            }
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                if (!DataConvertionHelper.IsNullValue(parentNodeId))
                                {
                                    db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                                }
                                otherSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }

                            sbBottom.Clear();
                            sbBottom.AppendFormat("UPDATE {0} SET Sorting = Sorting - 1 WHERE {1} ", tableName, parentFieldIdName);
                            if (!DataConvertionHelper.IsNullValue(parentNodeId))
                            {
                                sbBottom.AppendFormat("= @{0} AND Sorting > @Sorting", parentFieldIdName);
                            }
                            else
                            {
                                sbBottom.Append("IS NULL AND Sorting > @Sorting");
                            }
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                if (!DataConvertionHelper.IsNullValue(parentNodeId))
                                {
                                    db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                                }
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET Sorting = @Sorting WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, otherSorting);
                                db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }

                            break;
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
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName)
        {
            bool exist = false;

            //查询语句
            string sqlSelect = string.Format("SELECT 1 FROM {0} WHERE {0} collate Chinese_PRC_CS_AS_WS = @{0}", dataFieldName);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, dataFieldName, DbType.String, nodeName);
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode)
        {
            bool exist = false;

            //查询语句
            string sqlSelect = string.Format("SELECT 1 FROM {0} WHERE {1} collate Chinese_PRC_CS_AS_WS = @{1}", tableName, dataFieldCodeName);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, nodeCode);
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的名称
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(string nodeName, byte nodeType)
        {
            bool exist = false;

            if (string.IsNullOrWhiteSpace(nodeTypeName))
            {
                throw new ArgumentNullException("节点类型名称为空。");
            }

            //查询语句
            string sqlSelect = string.Format("SELECT 1 FROM {0} WHERE {0} collate Chinese_PRC_CS_AS_WS = @{0} AND {1} = @{1}", dataFieldName, nodeTypeName);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, dataFieldName, DbType.String, nodeName);
                    db.AddInParameter(dbCommand, nodeTypeName, DbType.Byte, nodeType);
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 是否存在相同的编码
        /// </summary>
        /// <param name="nodeName">名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalCode(string nodeCode, byte nodeType)
        {
            bool exist = false;

            if (string.IsNullOrWhiteSpace(nodeTypeName))
            {
                throw new ArgumentNullException("节点类型名称为空。");
            }

            //查询语句
            string sqlSelect = string.Format("SELECT 1 FROM {0} WHERE {1} collate Chinese_PRC_CS_AS_WS = @{1} AND {2} = @{2}", 
                tableName, dataFieldCodeName, nodeTypeName);            

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, nodeCode);
                    db.AddInParameter(dbCommand, nodeTypeName, DbType.Byte, nodeType);
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父编号</param>
        /// <param name="nodeName">名称</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(decimal parentNodeId, string nodeName)
        {
            bool exist = false;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT 1 FROM {0}", tableName);
            if (DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat(" WHERE {0} IS NULL", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat(" WHERE {0} = @{0}", parentFieldIdName);
            }
            sb.AppendFormat(" AND {0} collate Chinese_PRC_CS_AS_WS = @{0}", dataFieldName);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, parentNodeId);
                    }
                    db.AddInParameter(dbCommand, dataFieldName, DbType.String, nodeName);
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }


        /// <summary>
        /// 同一级是否存在相同的名称
        /// </summary>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>是否存在</returns>
        public bool IsExistIdenticalName(decimal parentNodeId, string nodeName, byte nodeType)
        {
            bool exist = false;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT 1 FROM {0} ", tableName);
            if (DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat("WHERE {0} IS NULL ", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat("WHERE {0} = @{0} ", parentFieldIdName);
            }
            sb.AppendFormat("AND {0} collate Chinese_PRC_CS_AS_WS = @{0} ", dataFieldName);
            if (!string.IsNullOrWhiteSpace(nodeTypeName))
            {
                sb.AppendFormat("AND {0} = @{0} ", nodeTypeName);
            }
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, parentNodeId);
                    }
                    db.AddInParameter(dbCommand, dataFieldName, DbType.String, nodeName);
                    if (!string.IsNullOrWhiteSpace(nodeTypeName))
                    {
                        db.AddInParameter(dbCommand, nodeTypeName, DbType.Byte, nodeType);
                    }
                    //执行查询操作
                    if (DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand)) > 0)
                    {
                        exist = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }
        
        /// <summary>
        /// 更新节点的父编号
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="newCode">节点的新编码</param>
        public void UpdateParentNodeId(decimal nodeId, decimal parentNodeId, string newCode)
        {
            string sqlUpdate = string.Format("UPDATE {0} SET {1} = @{1}, {2} = @{2}, Sorting = @Sorting WHERE {3} = @{3}", tableName, parentFieldIdName, dataFieldCodeName, dataFieldIdName);  //获得系统数据库对象

            SqlDatabase db = DataAccessHelper.GetDatabase();
            decimal oldParentNodeId = GetParentNodeId(nodeId);
            int count = GetTotalCountOfChildNode(oldParentNodeId);
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, tableName, "Sorting", parentFieldIdName, parentNodeId, 0) + 1;            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    RefreshCode(nodeId, newCode, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                        db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, newCode);                        
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    if (hasLeaf)
                    {
                        /* 更新新的父节点的叶子为 false */
                        UpdateLeafOfParentNode(parentNodeId, false, db, transaction);
                        /* 更新旧的父节点的叶子节点 */
                        if (count <= 1)
                        {
                            UpdateLeafOfParentNode(oldParentNodeId, true, db, transaction);
                        }
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
        /// 获得父节点编号
        /// </summary>
        ///<param name="nodeId">编号</param>        
        /// <returns> 父节点编号 </returns>
        public decimal GetParentNodeId(decimal nodeId)
        {
            decimal parentNodeId = decimal.MinValue;

            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", parentFieldIdName, tableName, dataFieldIdName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    parentNodeId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentNodeId;
        }

        /// <summary>
        /// 获得子节点编号列表
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>子节点编号列表</returns>
        public IList<decimal> GetChildNodeIds(decimal parentNodeId)
        {
            //创建集合对象
            IList<decimal> childNodeIds = new List<decimal>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0} FROM {1} WHERE ", dataFieldIdName, tableName);
            if (!DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat("{0} = @{0}", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat("{0} IS NULL", parentFieldIdName);
            }
            sb.Append(" ORDER BY Sorting ASC");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal childNodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            //将创建 UserTypeInfo 对象加入集合中
                            childNodeIds.Add(childNodeId);
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

            return childNodeIds;
        }

        /// <summary>
        /// 获得编号与编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<KeyValueInfo> GetChildNodeIdAndCodes(decimal parentNodeId)
        {
            //创建集合对象
            IList<KeyValueInfo> childNodeIdAndCodes = new List<KeyValueInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1} FROM {2} WHERE ", dataFieldIdName, dataFieldCodeName, tableName);
            if (!DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat("{0} = @{0}", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat("{0} IS NULL", parentFieldIdName);
            }
            sb.Append(" ORDER BY Sorting ASC");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal childNodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string childNodeCode = DataConvertionHelper.GetString(dataReader[1]);
                            //将创建 UserTypeInfo 对象加入集合中
                            childNodeIdAndCodes.Add(new KeyValueInfo(childNodeId, childNodeCode));
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

            return childNodeIdAndCodes;
        }

        /// <summary>
        /// 获得编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId)
        {
            //创建集合对象
            IList<string> childNodeCodes = new List<string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0} FROM {1} WHERE ", dataFieldCodeName, tableName);
            if (!DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat("{0} = @{0} ", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat("{0} IS NULL ", parentFieldIdName);
            }
            sb.Append("ORDER BY Sorting ASC");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string childNodeCode = DataConvertionHelper.GetString(dataReader[0]);
                            //将创建 UserTypeInfo 对象加入集合中
                            childNodeCodes.Add(childNodeCode);
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

            return childNodeCodes;
        }

        /// <summary>
        /// 获得编码
        /// </summary>	
        /// <param name="parentNodeId">父编号</param>
        /// <returns>编码</returns>
        public IList<string> GetChildNodeCodes(decimal parentNodeId, byte nodeType)
        {
            //创建集合对象
            IList<string> childNodeCodes = new List<string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0} FROM {1} WHERE ", dataFieldCodeName, tableName);
            if (!DataConvertionHelper.IsNullValue(parentNodeId))
            {
                sb.AppendFormat("{0} = @{0} ", parentFieldIdName);
            }
            else
            {
                sb.AppendFormat("{0} IS NULL ", parentFieldIdName);
            }
            if (string.IsNullOrWhiteSpace(nodeTypeName))
            {
                throw new ArgumentNullException("类型字段名称为空。");
            }
            sb.AppendFormat(" AND {0} = @{0} ", nodeTypeName);
            sb.Append("ORDER BY Sorting ASC");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(parentNodeId))
                    {
                        db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(parentNodeId));                        
                    }
                    if (!string.IsNullOrWhiteSpace(nodeTypeName))
                    {
                        db.AddInParameter(dbCommand, nodeTypeName, DbType.Byte, DataConvertionHelper.SetByte(nodeType));
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string childNodeCode = DataConvertionHelper.GetString(dataReader[0]);
                            //将创建 UserTypeInfo 对象加入集合中
                            childNodeCodes.Add(childNodeCode);
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

            return childNodeCodes;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">节点类型</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(parentFieldIdName, parentFieldIdName, System.Data.DbType.Decimal, parentNodeId,
               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            
            if (string.IsNullOrWhiteSpace(nodeTypeName))
            {
                throw new ArgumentNullException("类型字段名称为空。");
            }
            whereConditons.Add(new WhereConditon(nodeTypeName, nodeTypeName, System.Data.DbType.Byte, nodeType,
               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            return GetCommonNodesByWhereConditon(whereConditons);
        }

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public string GetNodeNameByNodeId(decimal nodeId)
        {
            string nodeName = string.Empty;

            //查询语句
            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", dataFieldName, tableName, dataFieldIdName);
            try
            {

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    nodeName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeName;
        }

        /// <summary>
        /// 获得节点编码
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public string GetNodeCode(decimal nodeId)
        {
            string nodeCode = string.Empty;

            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", dataFieldCodeName, tableName, dataFieldIdName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, nodeId);
                    nodeCode = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeCode;
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode)
        {
            decimal nodeId = decimal.MinValue;

            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", dataFieldIdName, tableName, dataFieldCodeName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, nodeCode);
                    nodeId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand), decimal.MinValue);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeId;
        }

        /// <summary>
        /// 通过节点编码获得节点编号
        /// </summary>
        ///<param name="nodeCode">节点编码</param>
        ///<param name="nodeType">节点类型</param>
        /// <returns> 编号 </returns>
        public decimal GetNodeIdByNodeCode(string nodeCode, byte nodeType)
        {
            decimal nodeId = decimal.MinValue;

            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2} AND {3} = @{3}", dataFieldIdName, tableName, dataFieldCodeName, nodeTypeName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, nodeCode);
                    db.AddInParameter(dbCommand, nodeTypeName, DbType.Byte, nodeType);
                    nodeId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeId;
        }        

        /// <summary>
        /// 获得节点编码
        /// </summary>
        ///<param name="nodeId">编号</param>
        /// <returns> 节点编码 </returns>
        public string GetNodeCodeByNodeId(decimal nodeId)
        {
            string nodeCode = string.Empty;

            string sqlSelect = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2}", dataFieldCodeName, tableName, dataFieldIdName);
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    nodeCode = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nodeCode;
        }
        
        /// <summary>
        /// 刷新所有子节点编码
        /// </summary>
        /// <param name="parentNodeId"></param>
        /// <returns></returns>
        public bool RefreshCode(decimal parentNodeId)
        {
            bool result = true;

            SqlDatabase db = DataAccessHelper.GetDatabase();
            string parentNodeCode = GetNodeCodeByNodeId(parentNodeId);            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    RefreshCode(parentNodeId, parentNodeCode, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            IList<KeyValueInfo> keyValueInfos = GetChildNodeIdAndCodes(parentNodeId);
            foreach (KeyValueInfo keyValueInfo in keyValueInfos)
            {

            }

            return result;
        }

        #endregion



        #region 虚拟方法

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public virtual IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> parentNames = new List<string>();

            //查询语句
            string sqlSelect = string.Format("SELECT {0}, {1} FROM {2} WHERE {3} = @{3}", dataFieldName, parentFieldIdName, tableName, dataFieldIdName);
            try
            {
                IList<decimal> parentNodeIds = new List<decimal>();
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                while (nodeId > 0)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            if (dataReader.Read())
                            {
                                nodeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                                parentNames.Insert(0, DataConvertionHelper.GetString(dataReader[0]));
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentNames;
        }

        /// <summary>
        /// 更新节点是否为叶子节点的状态
        /// </summary>        
        /// <param name="nodeId">编号</param>
        /// <param name="isLeaf">是否为叶子节点</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务对象</param>
        public virtual void UpdateLeafOfParentNode(decimal nodeId, bool isLeaf, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = string.Format("UPDATE {0} SET IsLeaf = @IsLeaf WHERE {1} = @{1} AND IsLeaf != @IsLeaf", tableName, dataFieldIdName);
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, isLeaf);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        #endregion

        #endregion


        #region 公有方法 

        #endregion

        #region 受保护方法        

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CommonNode 对象列表</returns>
        protected IList<CommonNode> GetCommonNodesByWhereConditon(IList<WhereConditon> whereConditons)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1}, {2}, {3}", dataFieldIdName, parentFieldIdName, dataFieldName, dataFieldCodeName);

            if (hasLeaf)
            {
                sb.Append(", IsLeaf ");
            }
            if (!string.IsNullOrWhiteSpace(nodeTypeName))
            {
                sb.AppendFormat(", {0} ", nodeTypeName);
            }
            sb.AppendFormat(" FROM {0} ", tableName);
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.AppendFormat(" ORDER BY {0} ASC, Sorting ASC", parentFieldIdName);
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
                        byte nodeType = 0;
                        while (dataReader.Read())
                        {
                            decimal nodeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentNodeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string name = DataConvertionHelper.GetString(dataReader[2]);
                            string code = DataConvertionHelper.GetString(dataReader[3]);
                            bool isLeaf = defaultValueOfLeaf;                            
                            if (hasLeaf)
                            {
                                isLeaf = DataConvertionHelper.GetBoolean(dataReader[4]);
                            }
                            if (!string.IsNullOrWhiteSpace(nodeTypeName))
                            {
                                if (hasLeaf)
                                {
                                    nodeType = DataConvertionHelper.GetByte(dataReader[5]);
                                }
                                else
                                {
                                    nodeType = DataConvertionHelper.GetByte(dataReader[4]);
                                }
                            }
                            commonNodes.Add(new CommonNode(nodeId, parentNodeId, name, code, isLeaf, nodeType));
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

            return commonNodes;
        }

        /// <summary>
        /// 获得子节点编号集合
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected IList<decimal> GetCommonNodeIds(decimal parentId, SqlDatabase db, DbTransaction transaction)
        {
            //创建集合对象
            IList<decimal> commonNodeIds = new List<decimal>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0} FROM {1} WHERE {2} = @{2} ", dataFieldIdName, tableName, parentFieldIdName);
            sb.Append(" ORDER BY Sorting ASC");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, parentFieldIdName, DbType.Decimal, parentId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
                    {
                        while (dataReader.Read())
                        {
                            commonNodeIds.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
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

            return commonNodeIds;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public IList<CommonNode> GetChildNodes(decimal parentNodeId, decimal keyId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon(parentFieldIdName, parentFieldIdName, DbType.Decimal, parentNodeId,
               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon(keyIdCaption, keyIdCaption, DbType.Decimal, keyId,
                          DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));


            return GetCommonNodesByWhereConditon(whereConditons);
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentNodeId">父节点编号</param>
        /// <param name="nodeType">父节点编号</param>
        /// <param name="keyId">关键字编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public virtual IList<CommonNode> GetChildNodes(decimal parentNodeId, byte nodeType, decimal keyId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(3);
            whereConditons.Add(new WhereConditon(parentFieldIdName, parentFieldIdName, DbType.Decimal, parentNodeId,
               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon(nodeTypeName, nodeTypeName, DbType.Byte, nodeType,
                          DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon(keyIdCaption, keyIdCaption, DbType.Decimal, keyId,
                          DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));


            return GetCommonNodesByWhereConditon(whereConditons);
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 刷新所有节点的编码
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="nodeCode"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void RefreshCode(decimal nodeId, string nodeCode, SqlDatabase db, DbTransaction transaction)
        {
            /* 更新编码 */
            string codeUpdate = string.Format("UPDATE {0} SET {1} = @{1} WHERE {2} = @{2} AND  {1} collate Chinese_PRC_CS_AS_WS != @{1}", tableName, dataFieldCodeName, dataFieldIdName);
            IList<decimal> commonNodeIds = GetCommonNodeIds(nodeId, db, transaction);
            for (int idx = 0; idx < commonNodeIds.Count; idx++)
            {
                string newCode = string.Empty;
                if (idx.ToString().Length == 1)
                {
                    newCode = string.Format("{0}00{1}", nodeCode, idx + 1);
                }
                else if (idx.ToString().Length == 2)
                {
                    newCode = string.Format("{0}0{1}", nodeCode, idx + 1);
                }
                else if (idx.ToString().Length == 3)
                {
                    newCode = string.Format("{0}{1}", nodeCode, idx + 1);
                }
                else
                {
                    throw new ArgumentException("编码个数不能超过3位数。");
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(codeUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, commonNodeIds[idx]);
                    db.AddInParameter(dbCommand, dataFieldCodeName, DbType.String, newCode);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
                RefreshCode(commonNodeIds[idx], newCode, db, transaction);
            }
        }

        #endregion
    }
}
