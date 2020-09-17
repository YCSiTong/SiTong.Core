using Microsoft.Extensions.DependencyInjection;
using St.Common.Attributes;
using St.Common.Helper;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace St.ServiceExtensions.Configuration
{
    /// <summary>
    /// 自定义全局依赖注入
    /// </summary>
    public static class StCustomDIInit
    {
        public static void AddDIAllStartUp(this IServiceCollection services)
        {
            services.NotNull(nameof(IServiceCollection));

            // 获取所有方法接口.
            var result = AssemblyHelper.GetAssemblys().SelectMany(op => op.GetTypes())
                .ToArray()
                .Distinct();
            var abstracts = result.Where(op => op.IsInterface && op.GetCustomAttribute<StDIAttribute>() != null).ToArray();
            abstracts.ForEach(serviceType =>
            {
                var attr = serviceType.GetCustomAttribute<StDIAttribute>();
                // TODO：可扩展工厂注入形式！！！ 一接口多实现.
                var implementType = result.Where(op => op.IsClass && !op.IsAbstract && !op.IsInterface && serviceType.IsAssignableFrom(op)).SingleOrDefault();
                if (implementType.IsNotNull())
                {
                    //Console.WriteLine($"Interface => { serviceType.Name } \r\n realize => { implementType.Name } \r\n ServiceLifetime => { attr.ServiceLifetime } \r\r");
                    services.Add(new ServiceDescriptor(serviceType, implementType, attr.ServiceLifetime));
                }
                else
                    Console.WriteLine($"Interface => `{ serviceType.Name }` Not have ImplementType  \r\n ServiceLifetime => { attr.ServiceLifetime } \r\r");

            });
        }
    }
}
