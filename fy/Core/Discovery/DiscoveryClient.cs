using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modobay.Discovery
{
    [System.ComponentModel.Description("服务发现-客户端")]
    public class DiscoveryClient : IDiscoveryClient
    {
        private readonly IDiscoveryServer discoveryServer;

        public DiscoveryClient(IDiscoveryServer discoveryServer)
        {
            this.discoveryServer = discoveryServer;
        }

        public string Get(string name)
        {
            var service = ConfigManager.Configuration["ServiceConfig:DependentServiceUrl"];

            // todo 不在一个局域网，暂不启用redis服务中心
            //var key = $"ServiceCenter:DiscoveryClient:{name}";
            //service = memoryCache.Get<string>(key);
            //if (string.IsNullOrEmpty(service))
            //{
            //    var serviceList = discoveryServer.GetAll();
            //    foreach (var item in serviceList)
            //    {
            //        memoryCache.Set($"ServiceCenter:DiscoveryClient:{item.Key}", item.Value, new TimeSpan(0, 3, 0));
            //    }
            //    service = memoryCache.Get<string>(key);
            //}

            if (string.IsNullOrEmpty(service)) throw new AppException(name + "服务不存在");
            return service;
        }
    }
}
