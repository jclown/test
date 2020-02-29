using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Modobay
{
    public interface IRedisApiClient : Modobay.ICache
    {
        ITransaction CreateTransaction();
        IDatabase GetDatabase();
        IServer GetServer(string hostAndPort);
        long HashDelete(string key, List<RedisValue> dataKeys);
        Task<long> HashDeleteAsync(string key, List<RedisValue> dataKeys);
        Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues);
        void Subscribe(string subChannel, Action<RedisChannel, RedisValue> handler = null);
        bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues);
        RedisValue[] StringGet(List<string> listKey);
        Task<RedisValue[]> StringGetAsync(List<string> listKey);
        T ConvertObj<T>(RedisValue value);
    }
}