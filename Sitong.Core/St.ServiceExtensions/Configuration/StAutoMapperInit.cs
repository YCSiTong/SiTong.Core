using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using St.AutoMapper;
using St.AutoMapper.Extensions;
using St.AutoMapper.Identity;
using St.Extensions;

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

            services.AddAutoMapper(typeof(StAutoMapperProFile), typeof(StAutoMapperIdentityProFile));
            AutoMapperExtension.InitMapper(services.BuildServiceProvider().GetService<IMapper>());//初始化IMapper扩展

        }
    }
}
