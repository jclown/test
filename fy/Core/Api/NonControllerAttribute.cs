using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class NonControllerAttribute : Attribute
    {
    }
}