using Microsoft.AspNetCore.Builder;
using St.Extensions;
using System;

namespace St.ServiceExtensions.MiddleWare.Configuration
{
    public static partial class StSwaggerInit
    {
        /// <summary>
        /// 开启Swagger中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options">Swagger中间件所需配置信息</param>
        public static void UseSwagger(this IApplicationBuilder app, Action<SwaggerMiddleWareOptions> options)
        {
            app.NotNull(nameof(IApplicationBuilder));

            var model = new SwaggerMiddleWareOptions();
            options(model);

            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint($"/swagger/{model.Version}/swagger.json", $"{model.Name} {model.Version}");
                op.RoutePrefix = "";
                // 是否开启MiniProfiler性能测试
                if (model.IsOpenMiniProfiler)
                {
                    op.HeadContent = @"<script async='async' id='mini-profiler' src='/profiler/includes.min.js?v=4.1.0+c940f0f28d' 
                                        data-version='4.1.0+c940f0f28d' data-path='/profiler/' data-current-id='4ec7c742-49d4-4eaf-8281-3cle0efa8888' 
                                        data-ids='4ec7c742-49d4-4eaf-8281-3cle0efa8888' data-position='Left' data-authorized='true'  data-max-traces='5' 
                                        data-toggle-shortcut='Alt+P' data-trivial-milliseconds='2.0'  data-ignored-duplicate-execute-types='Open,OpenAsync,Close,CloseAsync'></script>";
                }
            });
        }
    }

    /// <summary>
    /// Swagger中间件配置信息
    /// </summary>
    public class SwaggerMiddleWareOptions
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 是否开启MiniProfiler性能测试
        /// </summary>
        public bool IsOpenMiniProfiler { get; set; }
    }
}
