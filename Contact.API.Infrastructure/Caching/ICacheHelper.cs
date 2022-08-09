using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Caching
{
    public interface ICacheHelper
    {
        Task<List<TEntity>> GetEntities<TEntity>(string cacheKey) where TEntity : class, new();
    }
}
