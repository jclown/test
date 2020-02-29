using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RemarkAttribute : Attribute
    {
        public RemarkAttribute(string remark)
        {
            Remark = remark;
        }
        public string Remark { get; set; }

    }
}