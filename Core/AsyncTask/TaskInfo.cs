using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.AsyncTask
{
    public class TaskInfo//<T>
    {
        public string TaskId { get; set; }
        public int Done { get; set; }
        public int Count { get; set; }
        public int CurrentIndex { get; set; }
        public string CurrentInfo { get; set; }
        //public T Data { get; set; }
        public object Data { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Remark { get; set; }
    }
}
