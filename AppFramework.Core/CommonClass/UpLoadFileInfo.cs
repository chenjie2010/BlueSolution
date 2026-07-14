//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ExtendedUpLoadFileInfo.cs
// 描述： 上传文件的信息
// 作者：ChenJie 
// 编写日期：2017/09/22
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 上传文件的信息
    /// </summary>
    [Serializable]
    public class UpLoadFileInfo
    {
        #region 内部成员变量

        private string _upLoadFileName;
        private string _upLoadSourceFileName;
        private byte[] _upLoadFileData;

        #endregion

        #region 属性

        /// <summary>
        /// 附件名称
        /// </summary>
        public string UpLoadFileName
        {
            get
            {
                return _upLoadFileName;
            }
            set
            {
                _upLoadFileName = value;
            }

        }

        /// <summary>
        /// 附件源名称
        /// </summary>
        public string UpLoadSourceFileName
        {
            get
            {
                return _upLoadSourceFileName;
            }
            set
            {
                _upLoadSourceFileName = value;
            }

        }

        /// <summary>
        /// 附件数据
        /// </summary>
        public byte[] UpLoadFileData
        {
            get
            {
                return _upLoadFileData;
            }
            set
            {
                _upLoadFileData = value;
            }
        }

        #endregion

        public UpLoadFileInfo()
        {
            _upLoadFileName = string.Empty;
            _upLoadSourceFileName = string.Empty;            
        }
        public UpLoadFileInfo(string upLoadFileName, string upLoadSourceFileName, byte[] upLoadFileData)
        {
            _upLoadFileName = upLoadFileName;
            _upLoadSourceFileName = upLoadSourceFileName;
            _upLoadFileData = upLoadFileData;
        }

        /// <summary>
        /// 显示源文件名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _upLoadSourceFileName;
        }
    }
}
