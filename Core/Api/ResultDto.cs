using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Modobay.Api
{
    /// <summary>
    /// 返回结果，当Success为true时，通过Data返回结果。
    /// </summary>
    /// <typeparam name="T">指定类型的返回结果</typeparam>
    [DataContract]
    public class ResultDto<T>
    {

        public ResultDto()
        {
        }

        /// <summary>
        /// 操作状态:true成功 false失败
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        ///// <summary>
        ///// 代码，前端可根据约定的代码继续处理
        ///// </summary>
        //[DataMember]
        //public string Code { get; set; }

        #region 提示信息设置

        private StringBuilder m_sbMessage = null;

        private StringBuilder MessageBuilder
        {
            get
            {
                if (m_sbMessage == null)
                {
                    m_sbMessage = new StringBuilder();
                }
                return m_sbMessage;
            }
        }

        public void AddMessage(string msg)
        {
            if (MessageBuilder.Length > 0)
            {
                MessageBuilder.Append(",");
            }

            MessageBuilder.Append(msg);
        }

        #endregion

        /// <summary>
        /// 提示信息
        /// </summary>
        [DataMember]
        public string Message
        {
            get
            {
                return MessageBuilder.ToString();
            }
            set
            {
                MessageBuilder.Clear();
                MessageBuilder.Append(value);
            }
        }

        private T _data;

        /// <summary>
        /// 返回数据，仅当Success为true时才有返回数据
        /// </summary>
        [DataMember]
        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;

                if (_data != null)
                {
                    var type = _data.GetType();

                    // 设置分页查询结果的总记录数
                    if (type.FullName.StartsWith("System.PagedList"))
                    { 
                        TotalCount = int.Parse(type.GetProperty("TotalItemCount").GetValue(_data, null).ToString());
                        PageCount = int.Parse(type.GetProperty("TotalPageCount").GetValue(_data, null).ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 扩展的返回数据。此项数据不会生成api文档，建议尽量数据本身说明自己是什么。如{Value=90，Desc="课程成绩"}
        /// </summary>
        [DataMember]
        public object DataExt { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        [DataMember]
        public int TotalCount { get; protected set; }
        
        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int PageCount { get; protected set; }

        /// <summary>
        /// 请求id，可根据请求id查询相关日志信息或异步任务执行进度。
        /// </summary>
        [DataMember]
        public string RequestId { get; set; }
    }

}
