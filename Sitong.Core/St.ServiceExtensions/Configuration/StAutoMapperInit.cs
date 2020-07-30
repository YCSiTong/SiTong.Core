﻿using Microsoft.Extensions.DependencyInjection;
using St.Extensions;
using St.Application.AutoMapper;

namespace St.ServiceExtensions.Configuration
{
    public static class StAutoMapperInit
    {
        /// <summary>
        /// 注入AutoMapper映射
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperStartUp(this IServiceCollection services)
        {
            services.NotNull(nameof(IServiceCollection));

            //services.AddAutoMapper(typeof(StAutoMapperProFile));
        }
    }
}
