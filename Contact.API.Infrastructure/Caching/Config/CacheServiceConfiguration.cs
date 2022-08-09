using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure.Caching.Config
{
    public static class CacheServiceConfiguration
    {
        public static IServiceCollection AddCacheHelper(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services.AddScoped<ICacheHelper, CacheHelper>();
        }
    }
}
