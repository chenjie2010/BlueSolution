using System;

namespace Blue.WebAPI
{
    /// <summary>
    /// 字段对象描述
    /// </summary>
    public class UserDataField
    {
        #region 属性

        /// <summary>
        /// 字段物理名称(英文)
        /// </summary>
        public string DataFieldName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段逻辑名称(中文)
        /// </summary>
        public string DataFieldCaption
        {
            get;
            set;
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public byte DataFieldType
        {
            get;
            set;
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int DataFieldLength
        {
            get;
            set;
        }

        /// <summary>
        /// 字段备注
        /// </summary>
        public string Notes
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserDataField()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName"></param>
        /// <param name="dataFieldCaption"></param>
        /// <param name="dataFieldType"></param>
        /// <param name="dataFieldLength"></param>
        /// <param name="notes"></param>
        public UserDataField(string dataFieldName, string dataFieldCaption, byte dataFieldType, 
            int dataFieldLength, string notes)
        {
            DataFieldName = dataFieldName;
            DataFieldCaption = dataFieldCaption;
            DataFieldType = dataFieldType;
            DataFieldLength = dataFieldLength;
            Notes = notes;
        }

        #endregion

    }
}
