using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using St.Common.MemoryCaChe;
using St.Extensions;

namespace St.ServiceExtensions.Configuration
{
    public static class StMemoryCaCheInit
    {
        /// <summary>
        /// 注入内存缓存
        /// </summary>
        /// <param name="services"></param>
        public static void AddMemoryCaCheStartUp(this IServiceCollection services)
        {
            services.NotNull(nameof(IServiceCollection));

            services.AddScoped<IMemoryCaChe, MemoryCaChe>();
            services.AddSingleton<IMemoryCache>(op =>
            {//Microsoft.Extensions.Caching.Memory
                return new MemoryCache(new MemoryCacheOptions());
            });
        }
    }
}
