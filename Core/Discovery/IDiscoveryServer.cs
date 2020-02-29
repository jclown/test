using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Discovery
{
    public interface IDiscoveryServer
    {
        int Register(string serviceAddress, List<string> interfaceList);
        void Keep(string serviceAddress);
        void Cancel(string serviceAddress);
        Dictionary<string, string> GetAll();
    }
}
