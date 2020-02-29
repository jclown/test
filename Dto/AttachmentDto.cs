using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dto
{
    public class AttachmentDto
    {
        /// <summary>
        /// 文件唯一主键
        /// </summary>
        public string FileKey { get; set; } = "";
        public int AttachmentID { get; set; }
        public string AttachmentKind { get; set; } = "";


        /// <summary>
        /// 文件类型
        /// </summary>
        public string ContentType { get; set; } = "";
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = "";

        public string Url { get; set; } = "";

        [Newtonsoft.Json.JsonIgnore]
        public Stream Data { get; set; }
    }
}
