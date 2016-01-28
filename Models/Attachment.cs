using System;

namespace MVCStudy.Models
{
    /// <summary>
    /// 附件
    /// <remarks>
    /// 创建：2014.02.27
    /// </remarks>
    /// </summary>
    public class Attachment
    {
        public int AttachmentID { get; set; }

        /// <summary>
        /// 模型ID
        /// </summary>
        public Nullable<int> ModelID { get; set; }


        /// <summary>
        /// 拥有者
        /// 对应UserName
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 附件类型 image,flash,media,file 四种类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileParth { get; set; }

        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime UploadDate { get; set; }
    }
}
