using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiSettingAttribute : Attribute
    {
        
        public bool NonToken { get; set; } = false;


    }
}