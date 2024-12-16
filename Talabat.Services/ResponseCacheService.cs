using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis) 
        {
            _database=redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null) return;

            var optionSerializ = new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

            var responseSerializ=JsonSerializer.Serialize(response, optionSerializ);

            await _database.StringSetAsync(key, responseSerializ, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var cachedResponse = await _database.StringGetAsync(key);
            if (cachedResponse.IsNullOrEmpty) return null;
            return cachedResponse;
        }
    }
}
