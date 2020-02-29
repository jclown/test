using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Modobay.Cache;

namespace Modobay.Discovery
{
    [Modobay.Api.NonToken, Modobay.Api.AllowAllAppid]
    [Modobay.Api.NonController]
    [System.ComponentModel.Description("服务发现-服务端")]
    public class DiscoveryServer : IDiscoveryServer
    {
        const int ServiceExpireSeconds = 60;
        private readonly string _environment;
        private readonly string _serviceListKey;

        public DiscoveryServer()
        {
            _environment = ConfigManager.Configuration["ServiceConfig:ServiceCenterName"];
            _serviceListKey = $"ServiceCenter:{_environment}:0ServiceList";
        }

        private string GetKey(string hostPort)
        {
            return $"ServiceCenter:{_environment}:{hostPort}";
        }

        public void Cancel(string serviceAddress)
        {
            var key = GetKey(serviceAddress);
            CacheManager.RedisCache.Remove(key);
        }

        public void Keep(string serviceAddress)
        {
            var key = GetKey(serviceAddress.Replace(":", "_"));
            CacheManager.RedisCache.KeyExpire(key, TimeSpan.FromSeconds(ServiceExpireSeconds));
        }

        public Dictionary<string, string> GetAll()
        {
            var list = new Dictionary<string, string>();
            var serviceList = CacheManager.RedisCache.SetGet<string>(_serviceListKey);
            serviceList = serviceList.OrderBy(i => Guid.NewGuid()).ToList();

            foreach (var service in serviceList)
            {
                var interfaceList = CacheManager.RedisCache.SetGet<string>(GetKey(service));
                if (interfaceList == null)
                {
                    // 找不到视为过期服务，清除
                    CacheManager.RedisCache.SetRemove(_serviceListKey, service);
                    continue;
                }

                foreach (var item in interfaceList)
                {
                    if (list.ContainsKey(item)) continue;
                    list.Add(item, service.Replace("_", ":"));
                }
            }

            return list;
        }

        public int Register(string serviceAddress, List<string> interfaceList)
        {
            var key = GetKey(serviceAddress);
            CacheManager.RedisCache.SetAdd(key, interfaceList);
            CacheManager.RedisCache.KeyExpire(key, TimeSpan.FromSeconds(ServiceExpireSeconds));

            var serviceList = CacheManager.RedisCache.SetGet<string>(_serviceListKey);
            if (!serviceList.Contains(serviceAddress)) serviceList.Add(serviceAddress);
            CacheManager.RedisCache.SetAdd(_serviceListKey, serviceAddress);

            return ServiceExpireSeconds / 2;
        }
    }
}
