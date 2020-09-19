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
        /// <summary>
        /// 自定义根据·StDI·找到接口实现注入管理
        /// </summary>
        /// <param name="services"></param>
        public static void AddDIAllStartUp(this IServiceCollection services)
        {
            services.NotNull(nameof(IServiceCollection));

            // 获取所有方法接口.
            var result = AssemblyHelper.GetAssemblys().SelectMany(op => op.GetTypes())
                .ToArray()
                .Distinct();

            var interfaces = result.Where(op => op.IsInterface && op.HasAttribute<StDIInterfaceAttribute>());// 获取所有需要注入的接口
            var implements = result.Where(op => op.IsClass && interfaces.Any(x => x.IsAssignableFrom(op)));// 获取所有需注入接口的实现类
            var classs = result.Where(op => op.HasAttribute<StDIClassAttribute>());// 获取所有需要单注入的实现类

            /*
             *   摘要：
             *      检查当前需被接口注入的实现类中是否与需单独注入的实现类一致
             */

            classs.ForEach(x =>
            {
                Console.BackgroundColor = ConsoleColor.Red;
                if (implements.Any(op => x == op))
                    Console.WriteLine($"Class => `{ x.Name }` is Not Alone DI! Please check it ! \r\n  ");
            });

            /*
             *   摘要：
             *      开始注入单实现类.
             */
            classs.ForEach(implementType =>
            {
                var attr = implementType.GetCustomAttribute<StDIClassAttribute>();
                services.Add(new ServiceDescriptor(implementType, implementType, attr.ServiceLifetime));
            });

            /*
             *   摘要：
             *      开始注入接口与实现类.
             *   TODO：
             *      可进行工厂注入
             */
            interfaces.ForEach(serviceType =>
            {
                var attr = serviceType.GetCustomAttribute<StDIInterfaceAttribute>();

                // TODO：可扩展工厂注入形式！！！ 一接口多实现.目前只允许单个实现，需手动自行进行多实现注入工厂.
                var implementType = implements.Where(op => op.IsClass && serviceType.IsAssignableFrom(op)).SingleOrDefault();

                Console.BackgroundColor = ConsoleColor.Green;
                if (implementType.IsNotNull())
                    services.Add(new ServiceDescriptor(serviceType, implementType, attr.ServiceLifetime));
                else // 需注入接口若未存在实现提示用户.
                    Console.WriteLine($"Interface => `{ serviceType.Name }` Not have ImplementType  \r\n ServiceLifetime => { attr.ServiceLifetime } \r\n");
            });
        }
    }
}
