using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Cache
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public string CacheProfileName { get; set; }
    }
}