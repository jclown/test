using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Config
{
    public interface IConfigServer
    {
        dynamic GetConfig(string environment);
    }
}
