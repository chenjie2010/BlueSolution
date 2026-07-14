//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DataSumbittedInfo.cs
// 描述：DataSumbittedInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/02/07
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 数据填充类
    /// </summary>
    [Serializable]
    public class DataSumbittedInfo
    {
        #region 属性

        /// <summary>
        /// 业务编号
        /// </summary>
        public decimal BusinessId
        {
            get;
            set;
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName
        {
            get;
            set;
        }

        /// <summary>
        /// 业务介绍
        /// </summary>
        public string BusinessIntro
        {
            get;
            set;
        }

        /// <summary>
        /// 业务填报类型
        /// </summary>
        public DataFilledType DataFilledType
        {
            get;
            set;
        }

        /// <summary>
        /// 启用业务
        /// </summary>
        public bool BusinessEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime InitialTime
        {
            get;
            set;
        }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime ExpiredTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// 启用帮助
        /// </summary>
        public bool HelpEnabled
        {
            set;
            get;
        }

        /// <summary>
        /// 启用条件
        /// </summary>
        public bool ConditionEnabled
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessName"></param>
        /// <param name="businessIntro"></param>
        /// <param name="dataFilledType"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="initialTime"></param>
        /// <param name="expiredTime"></param>
        /// <param name="helpEnabled"></param>
        /// <param name="conditionEnabled"></param>
        public DataSumbittedInfo(decimal businessId, string businessName, string businessIntro, DataFilledType dataFilledType, bool businessEnabled, DateTime initialTime, DateTime expiredTime,
            bool helpEnabled, bool conditionEnabled)
        {
            BusinessId = businessId;
            BusinessName = businessName;
            BusinessIntro = businessIntro;
            DataFilledType = dataFilledType;
            BusinessEnabled = businessEnabled;
            InitialTime = initialTime;
            ExpiredTime = expiredTime;
            HelpEnabled = helpEnabled;
            ConditionEnabled = conditionEnabled;
        }

        #endregion
    }
}
