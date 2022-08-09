using Contact.API.Infrastructure.Data;
using Contact.API.Infrastructure.Repository.Query.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Caching
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ContactContext _contactContext;
        private const int _absoluteExpirationDay = 30;
        public CacheHelper(IMemoryCache memoryCache, ContactContext contactContext)
        {
            _memoryCache = memoryCache;
            _contactContext=contactContext;
        }

        public async Task<List<TEntity>> GetEntities<TEntity>(string cacheKey) where TEntity : class, new()
        {
            if (!_memoryCache.TryGetValue(cacheKey, out List<TEntity> entities))
            {
                entities = await _contactContext.Set<TEntity>().ToListAsync();
                var cacheExpirationOptions =
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddDays(_absoluteExpirationDay),
                        Priority = CacheItemPriority.Normal
                    };
                _memoryCache.Set(cacheKey, entities, cacheExpirationOptions);
            }
            return entities;
        }
    }
}
