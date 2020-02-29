using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay
{
    public class ExtList<T> : List<T>
    {
        public object DataExt { get; set; }
    }
}
