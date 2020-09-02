using Microsoft.Extensions.DependencyInjection;
using St.Extensions;
using StackExchange.Profiling;
using System;

namespace St.ServiceExtensions.Configuration
{
    public static class StMiniProfilerInit
    {
        /// <summary>
        /// 注入MiniProfiler性能测试,需同时开启中间件,请在Swagger中间件中设置开启IsOpenMiniProfiler为True
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">开启后在Swagger上显示的地方</param>
        public static void AddMiniProfilerStartUp(this IServiceCollection services, Action<RenderPosition> options)
        {
            services.NotNull(nameof(IServiceCollection));
            services.NotNull(nameof(Action<RenderPosition>));
            var model = new RenderPosition();
            options(model);

            services.AddMiniProfiler(op =>
            {
                op.RouteBasePath = "/profiler";
                op.PopupRenderPosition = RenderPosition.Left;
                op.PopupShowTimeWithChildren = true;
            });
        }
    }
}
