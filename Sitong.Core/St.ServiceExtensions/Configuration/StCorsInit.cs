using Microsoft.Extensions.DependencyInjection;
using St.Extensions;
using System;

namespace St.ServiceExtensions.Configuration
{
    public static class StCorsInit
    {
        /// <summary>
        /// 开启Cors跨域
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">Cors配置信息</param>
        public static void AddCorsStartUp(this IServiceCollection services, Action<CorsOptions> options)
        {
            services.NotNull(nameof(IServiceCollection));

            var model = new CorsOptions();
            options(model);

            if (model.Type == CorsOpenType.Custom) model.Origins.CheckArray(nameof(model.Origins));


            services.AddCors(op =>
            {
                op.AddPolicy(model.Name, policy =>
                {
                    switch (model.Type)
                    {
                        case CorsOpenType.All:
                            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                            break;
                        case CorsOpenType.Custom:
                            policy.WithOrigins(model.Origins)
                                  .AllowAnyHeader()
                                  .AllowAnyMethod();
                            break;
                    }
                });
            });
        }
    }

    /// <summary>
    /// Cors配置
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// 默认方案名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 选择打开类型
        /// </summary>
        public CorsOpenType Type { get; set; }
        /// <summary>
        /// 当打开类型为自定义时,需填入对应通过地址
        /// </summary>
        public string[] Origins { get; set; }
    }

    /// <summary>
    /// Cors运行开启方式
    /// </summary>
    public enum CorsOpenType
    {
        /// <summary>
        /// 全部打开
        /// </summary>
        All,
        /// <summary>
        /// 自定义允许打开
        /// </summary>
        Custom,
    }
}
