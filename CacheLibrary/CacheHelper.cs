using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLibrary
{
    public class CacheHelper:ICacheHelper
    {
        private readonly IMemoryCache _memoryCache;

        public CacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache=memoryCache;
        }
        public async Task<List<TEntity>> Get<TEntity>(string cacheKey) where TEntity : class, new()
        {

            if (!_memoryCache.TryGetValue(cacheKey, out TEntity entity))
            {
                entityList = await dbContext.Set<TEntity>().ToListAsync();
                var cacheExpirationOptions =
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddDays(30),
                        Priority = CacheItemPriority.Normal
                    };
                _memoryCache.Set(cacheKey, entityList, cacheExpirationOptions);
            }
            return entityList;
        }
    }
}
