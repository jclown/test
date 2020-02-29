using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NonActionAttribute : Attribute
    {
    }
}
