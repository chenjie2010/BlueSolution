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
    public class ExtendedUpLoadFileInfo : UpLoadFileInfo
    {
        #region 内部成员变量

        private AttachmentType _attachmentType;
        private string _filePath;

        #endregion

        #region 属性              

        /// <summary>
        /// 附件类型
        /// </summary>
        public AttachmentType AttachmentType
        {
            get
            {
                return _attachmentType;
            }
            set
            {
                _attachmentType = value;
            }
        }


        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExtendedUpLoadFileInfo() : this(string.Empty, null, AttachmentType.InLine)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="upLoadSourceFileName"></param>
        /// <param name="upLoadFileData"></param>
        /// <param name="attachmentType"></param>
        public ExtendedUpLoadFileInfo(string upLoadSourceFileName, byte[] upLoadFileData, AttachmentType attachmentType) :
            this(string.Empty, upLoadSourceFileName, upLoadFileData, string.Empty, attachmentType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="upLoadSourceFileName"></param>
        /// <param name="upLoadFileData"></param>
        /// <param name="filePath"></param>
        /// <param name="attachmentType"></param>
        public ExtendedUpLoadFileInfo(string upLoadSourceFileName, byte[] upLoadFileData, string filePath, AttachmentType attachmentType) :
            this(string.Empty, upLoadSourceFileName, upLoadFileData, filePath, attachmentType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="upLoadFileName"></param>
        /// <param name="upLoadSourceFileName"></param>
        /// <param name="upLoadFileData"></param>
        /// <param name="attachmentType"></param>
        public ExtendedUpLoadFileInfo(string upLoadFileName, string upLoadSourceFileName, byte[] upLoadFileData, AttachmentType attachmentType) :
            this(upLoadFileName, upLoadSourceFileName, upLoadFileData, string.Empty, attachmentType)
        {
            _attachmentType = attachmentType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="upLoadFileName"></param>
        /// <param name="upLoadSourceFileName"></param>
        /// <param name="upLoadFileData"></param>
        /// <param name="filePath"></param>
        /// <param name="attachmentType"></param>
        public ExtendedUpLoadFileInfo(string upLoadFileName, string upLoadSourceFileName, byte[] upLoadFileData, string filePath, AttachmentType attachmentType) :
            base(upLoadFileName, upLoadSourceFileName, upLoadFileData)
        {
            _filePath = filePath;
            _attachmentType = attachmentType;
        }
    }
}
