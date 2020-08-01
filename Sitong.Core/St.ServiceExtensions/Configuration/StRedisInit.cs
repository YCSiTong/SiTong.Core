using Microsoft.Extensions.DependencyInjection;
using St.Common.RedisCaChe;
using St.Extensions;

namespace St.ServiceExtensions.Configuration
{
    /// <summary>
    /// 注入使用Redis组件
    /// </summary>
    public static class StRedisInit
    {
        /// <summary>
        /// 开启Redis组件
        /// </summary>
        /// <param name="services"></param>
        public static void AddRedisStartUp(this IServiceCollection services)
        {
            services.NotNull(nameof(IServiceCollection));

            services.AddScoped<IRedisCaChe, RedisCaChe>();
        }
    }
}
