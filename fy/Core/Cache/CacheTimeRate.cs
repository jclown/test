using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Cache
{
    public class CacheTimeRate
    {
        public double HttpCacheTime { get; set; }
        public double AppCacheTime { get; set; }
        public double RedisCacheTime { get; set; }
    }
}
