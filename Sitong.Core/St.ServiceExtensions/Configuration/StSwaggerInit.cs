﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using St.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace St.ServiceExtensions.Configuration
{
    public static partial class StSwaggerInit
    {
        /// <summary>
        /// 注入Swagger服务
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="options">Swagger配置信息</param>
        public static void AddSwaggerStartUp(this IServiceCollection services, Action<SwaggerOptions> options)
        {
            services.NotNull(nameof(IServiceCollection));

            var model = new SwaggerOptions();
            options(model);

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc(model.Version, new OpenApiInfo
                {
                    Version = model.Version,
                    Title = model.Name,
                    Description = model.Description,
                    Contact = new OpenApiContact { Name = model.Name, Email = model.Email, Url = new Uri(model.Url) },
                });
                a.OrderActionsBy(o => o.RelativePath);


                var BasePath = AppDomain.CurrentDomain.BaseDirectory;
                //var xmlPath = Path.Combine(BasePath, "Sitong.Core.xml");
                //a.IncludeXmlComments(xmlPath, true);
                //var ModelsxmlPath = Path.Combine(BasePath, "Sitong.Models.xml");
                //a.IncludeXmlComments(ModelsxmlPath);


                // 开启加权小锁
                a.OperationFilter<AddResponseHeadersFilter>();
                a.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                a.OperationFilter<SecurityRequirementsOperationFilter>();


                // 必须是 oauth2
                a.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

            });
        }
    }

    /// <summary>
    /// Swagger配置信息
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 详细说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Url { get; set; }
    }
}
