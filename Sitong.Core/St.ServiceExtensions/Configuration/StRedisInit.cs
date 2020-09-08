using Microsoft.Extensions.DependencyInjection;
using St.Common.RedisCaChe;
using St.Extensions;
using StackExchange.Redis;

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
        public static void AddRedisStartUp(this IServiceCollection services, string redisConnectionStr)
        {
            services.NotNull(nameof(IServiceCollection));
            redisConnectionStr.NotEmptyOrNull(nameof(redisConnectionStr));

            services.AddScoped<IRedisCaChe, RedisCaChe>();
            services.AddSingleton(op =>
            {
                var redisConfiguration = ConfigurationOptions.Parse(redisConnectionStr, true); // 忽略链接字符串中无法识别
                redisConfiguration.ResolveDns = true; // 连接前解析DNS,连接失败会重新链接.
                return ConnectionMultiplexer.Connect(redisConnectionStr);
            });
        }
    }
}
