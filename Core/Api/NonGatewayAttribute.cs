﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NonGatewayAttribute : Attribute
    {
    }
}