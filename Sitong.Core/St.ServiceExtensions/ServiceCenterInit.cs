using Microsoft.Extensions.DependencyInjection;
using St.Extensions;
using System;

namespace St.ServiceExtensions
{
    public static class ServiceCenterInit
    {
        /// <summary>
        /// TODO：实现一体式服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddStService(this IServiceCollection services, Action<ServiceCenterOptions> options)
        {
            services.NotNull(nameof(IServiceCollection));
            options.NotNull(nameof(Action<ServiceCenterOptions>));

            var model = new ServiceCenterOptions();
            options(model);

        }
    }
}
