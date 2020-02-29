using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Discovery
{
    public interface IDiscoveryClient
    {
        string Get(string name);
        //List<string> Get(string name);
        //List<string> GetAll();
        //string GetVersion();
    }
}
